﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{    
    public class YMDeltat
    {
        public struct YM_DELTAT
        {     /* AT: rearranged and tigntened structure */
            public int output_pointer;/* pointer of output pointers   */
            public int pan_offset;			/* pan : &output_pointer[pan]   */
            public double freqbase;
            public int memory_size;
            public int output_range;
            public int now_addr;		/* current address      */
            public int now_step;		/* currect step         */
            public int step;			/* step                 */
            public int start;			/* start address        */
            public int limit;			/* limit address        */
            public int end;			/* end address          */
            public int delta;			/* delta scale          */
            public int volume;			/* current volume       */
            public int acc;			/* shift Measurement value*/
            public int adpcmd;			/* next Forecast        */
            public int adpcml;			/* current value        */
            public int prev_acc;		/* leveling value       */
            public byte now_data;		/* current rom data     */
            public byte CPU_data;		/* current data from reg 08 */
            public byte portstate;		/* port status          */
            public byte control2;		/* control reg: SAMPLE, DA/AD, RAM TYPE (x8bit / x1bit), ROM/RAM */
            public byte portshift;		/* address bits shift-left:
                            ** 8 for YM2610,
                            ** 5 for Y8950 and YM2608 */
            public byte DRAMportshift;	/* address bits shift-right:
                            ** 0 for ROM and x8bit DRAMs,
                            ** 3 for x1 DRAMs */
            public byte memread;		/* needed for reading/writing external memory */
            /* handlers and parameters for the status flags support */
            public status_callback status_set_handler;
            public status_callback status_reset_handler;
            public byte status_change_EOS_bit;		/* 1 on End Of Sample (record/playback/cycle time of AD/DA converting has passed)*/
            public byte status_change_BRDY_bit;		/* 1 after recording 2 datas (2x4bits) or after reading/writing 1 data */
            public byte status_change_ZERO_bit;		/* 1 if silence lasts for more than 290 miliseconds on ADPCM recording */

            /* neither Y8950 nor YM2608 can generate IRQ when PCMBSY bit changes, so instead of above,
            ** the statusflag gets ORed with PCM_BSY (below) (on each read of statusflag of Y8950 and YM2608)
            */
            public byte PCM_BSY;		/* 1 when ADPCM is playing; Y8950/YM2608 only */
            public byte[] reg;		/* adpcm registers      */
            public byte emulation_mode;	/* which chip we're emulating */
        }
        public delegate void status_callback(byte b1);
        public static YM_DELTAT DELTAT;
        public static byte[] ymsnddeltatrom;
        private static int YM_DELTAT_DELTA_MAX = 24576;
        private static int YM_DELTAT_DELTA_MIN = 127;
        private static int YM_DELTAT_DELTA_DEF = 127;
        private static int YM_DELTAT_DECODE_RANGE = 32768;
        private static int YM_DELTAT_DECODE_MIN = -YM_DELTAT_DECODE_RANGE;
        private static int YM_DELTAT_DECODE_MAX = YM_DELTAT_DECODE_RANGE - 1;        
        /* Forecast to next Forecast (rate = *8) */
        /* 1/8 , 3/8 , 5/8 , 7/8 , 9/8 , 11/8 , 13/8 , 15/8 */
        private static int[] ym_deltat_decode_tableB1 = new int[16]{
          1,   3,   5,   7,   9,  11,  13,  15,
          -1,  -3,  -5,  -7,  -9, -11, -13, -15,
        };
        /* delta to next delta (rate= *64) */
        /* 0.9 , 0.9 , 0.9 , 0.9 , 1.2 , 1.6 , 2.0 , 2.4 */
        private static int[] ym_deltat_decode_tableB2 = new int[16]{
          57,  57,  57,  57, 77, 102, 128, 153,
          57,  57,  57,  57, 77, 102, 128, 153
        };
        /* 0-DRAM x1, 1-ROM, 2-DRAM x8, 3-ROM (3 is bad setting - not allowed by the manual) */
        private static byte[] dram_rightshift = new byte[4] { 3, 0, 0, 0 };
        public static void YM_DELTAT_ADPCM_Write(int r, byte v)
        {
            if (r >= 0x10)
                return;
            DELTAT.reg[r] = v;
            switch (r)
            {
                case 0x00:
                    if (DELTAT.emulation_mode == 1)
                    {
                        v |= 0x20;
                    }
                    DELTAT.portstate = (byte)(v & (0x80 | 0x40 | 0x20 | 0x10 | 0x01));
                    if ((DELTAT.portstate & 0x80) != 0)
                    {
                        DELTAT.PCM_BSY = 1;
                        DELTAT.now_step = 0;
                        DELTAT.acc = 0;
                        DELTAT.prev_acc = 0;
                        DELTAT.adpcml = 0;
                        DELTAT.adpcmd = YM_DELTAT_DELTA_DEF;
                        DELTAT.now_data = 0;
                    }
                    if ((DELTAT.portstate & 0x20) != 0)
                    {
                        DELTAT.now_addr = DELTAT.start << 1;
                        DELTAT.memread = 2;
                        if (ymsnddeltatrom == null)
                        {
                            DELTAT.portstate = 0x00;
                            DELTAT.PCM_BSY = 0;
                        }
                        else
                        {
                            if (DELTAT.end >= DELTAT.memory_size)
                            {
                                DELTAT.end = DELTAT.memory_size - 1;
                            }
                            if (DELTAT.start >= DELTAT.memory_size)
                            {
                                DELTAT.portstate = 0x00;
                                DELTAT.PCM_BSY = 0;
                            }
                        }
                    }
                    else
                    {
                        DELTAT.now_addr = 0;
                    }
                    if ((DELTAT.portstate & 0x01) != 0)
                    {
                        DELTAT.portstate = 0x00;
                        DELTAT.PCM_BSY = 0;
                        if(DELTAT.status_set_handler!=null)
                        	if(DELTAT.status_change_BRDY_bit!=0)
                        		DELTAT.status_set_handler(DELTAT.status_change_BRDY_bit);
                    }
                    break;
                case 0x01:
                    /* handle emulation mode */
                    if (DELTAT.emulation_mode == 1)
                    {
                        v |= 0x01;
                    }
                    DELTAT.pan_offset = (v >> 6) & 0x03;
                    if ((DELTAT.control2 & 3) != (v & 3))
                    {
                        if (DELTAT.DRAMportshift != dram_rightshift[v & 3])
                        {
                            DELTAT.DRAMportshift = dram_rightshift[v & 3];
                            DELTAT.start = (DELTAT.reg[0x3] * 0x0100 | DELTAT.reg[0x2]) << (DELTAT.portshift - DELTAT.DRAMportshift);
                            DELTAT.end = (DELTAT.reg[0x5] * 0x0100 | DELTAT.reg[0x4]) << (DELTAT.portshift - DELTAT.DRAMportshift);
                            DELTAT.end += (1 << (DELTAT.portshift - DELTAT.DRAMportshift)) - 1;
                            DELTAT.limit = (DELTAT.reg[0xd] * 0x0100 | DELTAT.reg[0xc]) << (DELTAT.portshift - DELTAT.DRAMportshift);
                        }
                    }
                    DELTAT.control2 = v;
                    break;
                case 0x02:
                case 0x03:
                    DELTAT.start = (DELTAT.reg[0x3] * 0x0100 | DELTAT.reg[0x2]) << (DELTAT.portshift - DELTAT.DRAMportshift);
                    break;
                case 0x04:
                case 0x05:
                    DELTAT.end = (DELTAT.reg[0x5] * 0x0100 | DELTAT.reg[0x4]) << (DELTAT.portshift - DELTAT.DRAMportshift);
                    DELTAT.end += (1 << (DELTAT.portshift - DELTAT.DRAMportshift)) - 1;
                    break;
                case 0x06:
                case 0x07:
                    break;
                case 0x08:
                    if ((DELTAT.portstate & 0xe0) == 0x60)
                    {
                        if (DELTAT.memread != 0)
                        {
                            DELTAT.now_addr = DELTAT.start << 1;
                            DELTAT.memread = 0;
                        }
                        if (DELTAT.now_addr != (DELTAT.end << 1))
                        {
                            ymsnddeltatrom[DELTAT.now_addr >> 1] = v;
                            DELTAT.now_addr += 2;
                            if(DELTAT.status_reset_handler!=null)
                            	if(DELTAT.status_change_BRDY_bit!=0)
                            	    DELTAT.status_reset_handler(DELTAT.status_change_BRDY_bit);
                            if(DELTAT.status_set_handler!=null)
                            	if(DELTAT.status_change_BRDY_bit!=0)
                            	    DELTAT.status_set_handler(DELTAT.status_change_BRDY_bit);
                        }
                        else
                        {
                            if(DELTAT.status_set_handler!=null)
                            	if(DELTAT.status_change_EOS_bit!=0)
                            		DELTAT.status_set_handler(DELTAT.status_change_EOS_bit);
                        }
                        return;
                    }
                    if ((DELTAT.portstate & 0xe0) == 0x80)
                    {
                        DELTAT.CPU_data = v;
                        if(DELTAT.status_reset_handler!=null)
                        	if(DELTAT.status_change_BRDY_bit!=0)
                            	DELTAT.status_reset_handler(DELTAT.status_change_BRDY_bit);
                        return;
                    }
                    break;
                case 0x09:
                case 0x0a:
                    DELTAT.delta = (DELTAT.reg[0xa] * 0x0100 | DELTAT.reg[0x9]);
                    DELTAT.step = (int)(DELTAT.delta * DELTAT.freqbase);
                    break;
                case 0x0b:
                    {
                        int oldvol = DELTAT.volume;
                        DELTAT.volume = (v & 0xff) * (DELTAT.output_range / 256) / YM_DELTAT_DECODE_RANGE;
                        if (oldvol != 0)
                        {
                            DELTAT.adpcml = (int)((double)DELTAT.adpcml / (double)oldvol * (double)DELTAT.volume);
                        }
                    }
                    break;
                case 0x0c:
                case 0x0d:
                    DELTAT.limit = (DELTAT.reg[0xd] * 0x0100 | DELTAT.reg[0xc]) << (DELTAT.portshift - DELTAT.DRAMportshift);
                    break;
            }
        }
        public static void YM_DELTAT_ADPCM_Reset(int pan, int emulation_mode)
        {
            DELTAT.now_addr = 0;
            DELTAT.now_step = 0;
            DELTAT.step = 0;
            DELTAT.start = 0;
            DELTAT.end = 0;
            DELTAT.limit = -1;
            DELTAT.volume = 0;
            DELTAT.pan_offset = 0;
            DELTAT.acc = 0;
            DELTAT.prev_acc = 0;
            DELTAT.adpcmd = 127;
            DELTAT.adpcml = 0;
            DELTAT.emulation_mode = (byte)emulation_mode;
            DELTAT.portstate = (emulation_mode == 1) ? (byte)0x20 : (byte)0;
            DELTAT.control2 = (emulation_mode == 1) ? (byte)0x01 : (byte)0;
            DELTAT.DRAMportshift = dram_rightshift[DELTAT.control2 & 3];
            if(DELTAT.status_set_handler!=null)
            	if(DELTAT.status_change_BRDY_bit!=0)
            	    DELTAT.status_set_handler(DELTAT.status_change_BRDY_bit);
        }
        public static void YM_DELTAT_postload(byte[] regs, int offset)
        {
            int r;
            DELTAT.volume = 0;
            for (r = 1; r < 16; r++)
                YM_DELTAT_ADPCM_Write(r, regs[offset + r]);
            DELTAT.reg[0] = regs[offset];
            if (ymsnddeltatrom != null)
                DELTAT.now_data = ymsnddeltatrom[DELTAT.now_addr >> 1];
        }
        private static int Limit(int val, int max, int min)
        {
            if (val > max)
            {
                return max;
            }
            else if (val < min)
            {
                return min;
            }
            else
            {
                return val;
            }
        }
        private static void YM_DELTAT_synthesis_from_external_memory()
        {
            int step;
            int data;
            DELTAT.now_step += DELTAT.step;
            if (DELTAT.now_step >= (1 << 16))
            {
                step = DELTAT.now_step >> 16;
                DELTAT.now_step &= (1 << 16) - 1;
                do
                {
                    if (DELTAT.now_addr == (DELTAT.limit << 1))
                        DELTAT.now_addr = 0;
                    if (DELTAT.now_addr == (DELTAT.end << 1))
                    {
                        if ((DELTAT.portstate & 0x10) != 0)
                        {
                            DELTAT.now_addr = DELTAT.start << 1;
                            DELTAT.acc = 0;
                            DELTAT.adpcmd = YM_DELTAT_DELTA_DEF;
                            DELTAT.prev_acc = 0;
                        }
                        else
                        {
                            if(DELTAT.status_set_handler!=null)
                            	if(DELTAT.status_change_EOS_bit!=0)
                            		DELTAT.status_set_handler(DELTAT.status_change_EOS_bit);
                            DELTAT.PCM_BSY = 0;
                            DELTAT.portstate = 0;
                            DELTAT.adpcml = 0;
                            DELTAT.prev_acc = 0;
                            return;
                        }
                    }
                    if ((DELTAT.now_addr & 1) != 0)
                        data = DELTAT.now_data & 0x0f;
                    else
                    {
                        DELTAT.now_data = ymsnddeltatrom[DELTAT.now_addr >> 1];
                        data = DELTAT.now_data >> 4;
                    }
                    DELTAT.now_addr++;
                    DELTAT.now_addr &= ((1 << (24 + 1)) - 1);
                    DELTAT.prev_acc = DELTAT.acc;
                    DELTAT.acc += (ym_deltat_decode_tableB1[data] * DELTAT.adpcmd / 8);
                    DELTAT.acc=Limit(DELTAT.acc, YM_DELTAT_DECODE_MAX, YM_DELTAT_DECODE_MIN);
                    DELTAT.adpcmd = (DELTAT.adpcmd * ym_deltat_decode_tableB2[data]) / 64;
                    DELTAT.adpcmd=Limit(DELTAT.adpcmd, YM_DELTAT_DELTA_MAX, YM_DELTAT_DELTA_MIN);
                } while ((--step) != 0);
            }
            DELTAT.adpcml = DELTAT.prev_acc * (int)((1 << 16) - DELTAT.now_step);
            DELTAT.adpcml += (DELTAT.acc * (int)DELTAT.now_step);
            DELTAT.adpcml = (DELTAT.adpcml >> 16) * (int)DELTAT.volume;
            FM.out_delta[DELTAT.pan_offset] += DELTAT.adpcml;
        }
        private static void YM_DELTAT_synthesis_from_CPU_memory()
        {
            int step;
            int data;
            DELTAT.now_step += DELTAT.step;
            if (DELTAT.now_step >= (1 << 16))
            {
                step = DELTAT.now_step >> 16;
                DELTAT.now_step &= (1 << 16) - 1;
                do
                {
                    if ((DELTAT.now_addr & 1) != 0)
                    {
                        data = DELTAT.now_data & 0x0f;
                        DELTAT.now_data = DELTAT.CPU_data;
                        if(DELTAT.status_set_handler!=null)
                            if(DELTAT.status_change_BRDY_bit!=0)
                                DELTAT.status_set_handler(DELTAT.status_change_BRDY_bit);
                    }
                    else
                    {
                        data = DELTAT.now_data >> 4;
                    }
                    DELTAT.now_addr++;
                    DELTAT.prev_acc = DELTAT.acc;
                    DELTAT.acc += (ym_deltat_decode_tableB1[data] * DELTAT.adpcmd / 8);
                    DELTAT.acc=Limit(DELTAT.acc, YM_DELTAT_DECODE_MAX, YM_DELTAT_DECODE_MIN);
                    DELTAT.adpcmd = (DELTAT.adpcmd * ym_deltat_decode_tableB2[data]) / 64;
                    DELTAT.adpcmd=Limit(DELTAT.adpcmd, YM_DELTAT_DELTA_MAX, YM_DELTAT_DELTA_MIN);
                } while ((--step) != 0);
            }
            DELTAT.adpcml = DELTAT.prev_acc * (int)((1 << 16) - DELTAT.now_step);
            DELTAT.adpcml += (DELTAT.acc * (int)DELTAT.now_step);
            DELTAT.adpcml = (DELTAT.adpcml >> 16) * (int)DELTAT.volume;
            FM.out_delta[DELTAT.pan_offset] += DELTAT.adpcml;
        }
        public static void YM_DELTAT_ADPCM_CALC()
        {
            if ((DELTAT.portstate & 0xe0) == 0xa0)
            {
                YM_DELTAT_synthesis_from_external_memory();
                return;
            }
            if ((DELTAT.portstate & 0xe0) == 0x80)
            {
                YM_DELTAT_synthesis_from_CPU_memory();
                return;
            }
            return;
        }
    }
}

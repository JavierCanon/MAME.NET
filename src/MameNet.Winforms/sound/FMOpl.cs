using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public class FMOpl
    {
        public struct OPL_SLOT
        {
            public uint ar;
            public uint dr;
            public uint rr;
            public byte KSR;
            public byte ksl;
            public byte ksr;
            public byte mul;

            public uint Cnt;
            public uint Incr;
            public byte FB;
            public int iconnect;
            public int[] op1_out;
            public byte CON;

            public byte eg_type;
            public byte state;
            public uint TL;
            public int TLL;
            public int volume;
            public uint sl;
            public byte eg_sh_ar;
            public byte eg_sel_ar;
            public byte eg_sh_dr;
            public byte eg_sel_dr;
            public byte eg_sh_rr;
            public byte eg_sel_rr;
            public uint key;

            public uint AMmask;
            public byte vib;

            public ushort wavetable;
        }
        public struct OPL_CH
        {
            public OPL_SLOT[] SLOT;
	        public uint block_fnum;
	        public uint fc;
	        public uint ksl_base;
	        public byte kcode;
        }
        public struct FM_OPL
        {
	        public OPL_CH[] P_CH;

	        public uint eg_cnt;
	        public uint eg_timer;
	        public uint eg_timer_add;
	        public uint eg_timer_overflow;

	        public byte rhythm;

	        public uint[] fn_tab;

	        public byte lfo_am_depth;
	        public byte lfo_pm_depth_range;
	        public uint lfo_am_cnt;
	        public uint lfo_am_inc;
	        public uint lfo_pm_cnt;
	        public uint lfo_pm_inc;

	        public uint noise_rng;
	        public uint noise_p;
	        public uint noise_f;

	        public byte wavesel;

	        public uint[] T;
	        public byte[] st;

	        public OPL_TIMERHANDLER timer_handler;
            //public void* TimerParam;
            public OPL_IRQHANDLER IRQHandler;
            //public void* IRQParam;
            public OPL_UPDATEHANDLER UpdateHandler;
            //public void* UpdateParam;

            public byte type;
            public byte address;
            public byte status;
            public byte statusmask;
            public byte mode;

	        public int clock;
	        public int rate;
	        public double freqbase;
            public Atime TimerBase;
        }
        public static FM_OPL OPL;
        public static int[] slot_array=new int[32]
        {
	         0, 2, 4, 1, 3, 5,-1,-1,
	         6, 8,10, 7, 9,11,-1,-1,
	        12,14,16,13,15,17,-1,-1,
	        -1,-1,-1,-1,-1,-1,-1,-1
        };
        public static uint[] ksl_tab=new uint[8*16]
        {
	        0,0,0,0,
	        0,0,0,0,
	        0,0,0,0,
	        0,0,0,0,

	        0,0,0,0,
	        0,0,0,0,
	        0,8,12,16,
	        20,24,28,32,

	        0,0,0,0,
	        0,12,20,28,
	        32,40,44,48,
	        52,56,60,64,

	        0,0,0,20,
	        32,44,52,60,
	        64,72,76,80,
	        84,88,92,96,

	        0,0,32,52,
	        64,76,84,92,
	        96,104,108,112,
	        116,120,124,128,

	        0,32,64,84,
	        96,108,116,124,
	        128,136,140,144,
	        148,152,156,160,

	        0,64,96,116,
	        128,140,148,156,
	        160,168,172,176,
	        180,184,188,192,

	        0,96,128,148,
	        160,172,180,188,
	        192,200,204,208,
	        212,216,220,224
        };
        public static uint[] sl_tab=new uint[16]
        {
            0*16, 1*16, 2*16,3 *16,4 *16,5 *16,6 *16, 7*16,
            8*16, 9*16,10*16,11*16,12*16,13*16,14*16,31*16
        };
        public static byte[] eg_inc=new byte[15*8]
        {
            0,1, 0,1, 0,1, 0,1,
            0,1, 0,1, 1,1, 0,1,
            0,1, 1,1, 0,1, 1,1,
            0,1, 1,1, 1,1, 1,1,

            1,1, 1,1, 1,1, 1,1,
            1,1, 1,2, 1,1, 1,2,
            1,2, 1,2, 1,2, 1,2,
            1,2, 2,2, 1,2, 2,2,

            2,2, 2,2, 2,2, 2,2,
            2,2, 2,4, 2,2, 2,4,
            2,4, 2,4, 2,4, 2,4,
            2,4, 4,4, 2,4, 4,4,

            4,4, 4,4, 4,4, 4,4,
            8,8, 8,8, 8,8, 8,8,
            0,0, 0,0, 0,0, 0,0,
        };
        public static byte[] eg_rate_select=new byte[16+64+16]
        {
            14*8,14*8,14*8,14*8,14*8,14*8,14*8,14*8,
            14*8,14*8,14*8,14*8,14*8,14*8,14*8,14*8,

            0, 1*8, 2*8, 3*8,
            0, 1*8, 2*8, 3*8,
            0, 1*8, 2*8, 3*8,
            0, 1*8, 2*8, 3*8,
            0, 1*8, 2*8, 3*8,
            0, 1*8, 2*8, 3*8,
            0, 1*8, 2*8, 3*8,
            0, 1*8, 2*8, 3*8,
            0, 1*8, 2*8, 3*8,
            0, 1*8, 2*8, 3*8,
            0, 1*8, 2*8, 3*8,
            0, 1*8, 2*8, 3*8,
            0, 1*8, 2*8, 3*8,

            4*8, 5*8, 6*8, 7*8,

            8*8, 9*8,10*8,11*8,

            12*8,12*8,12*8,12*8,

            12*8,12*8,12*8,12*8,12*8,12*8,12*8,12*8,
            12*8,12*8,12*8,12*8,12*8,12*8,12*8,12*8,
        };
        public static byte[] eg_rate_shift=new byte[16+64+16]
        {
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,

            12,12,12,12,
            11,11,11,11,
            10,10,10,10,
            9, 9, 9, 9,
            8, 8, 8, 8,
            7, 7, 7, 7,
            6, 6, 6, 6,
            5, 5, 5, 5,
            4, 4, 4, 4,
            3, 3, 3, 3,
            2, 2, 2, 2,
            1, 1, 1, 1,
            0, 0, 0, 0,

            0, 0, 0, 0,

            0, 0, 0, 0,

            0, 0, 0, 0,

            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
        };
        public static byte[] mul_tab=new byte[16]
        {
            1, 2, 4, 6, 8, 10, 12, 14,
            16, 18,20,20,24,24,30,30
        };
        public static int[] tl_tab;
        public static uint[] sin_tab;
        public static byte[] lfo_am_table=new byte[210]
        {
            0,0,0,0,0,0,0,
            1,1,1,1,
            2,2,2,2,
            3,3,3,3,
            4,4,4,4,
            5,5,5,5,
            6,6,6,6,
            7,7,7,7,
            8,8,8,8,
            9,9,9,9,
            10,10,10,10,
            11,11,11,11,
            12,12,12,12,
            13,13,13,13,
            14,14,14,14,
            15,15,15,15,
            16,16,16,16,
            17,17,17,17,
            18,18,18,18,
            19,19,19,19,
            20,20,20,20,
            21,21,21,21,
            22,22,22,22,
            23,23,23,23,
            24,24,24,24,
            25,25,25,25,
            26,26,26,
            25,25,25,25,
            24,24,24,24,
            23,23,23,23,
            22,22,22,22,
            21,21,21,21,
            20,20,20,20,
            19,19,19,19,
            18,18,18,18,
            17,17,17,17,
            16,16,16,16,
            15,15,15,15,
            14,14,14,14,
            13,13,13,13,
            12,12,12,12,
            11,11,11,11,
            10,10,10,10,
            9,9,9,9,
            8,8,8,8,
            7,7,7,7,
            6,6,6,6,
            5,5,5,5,
            4,4,4,4,
            3,3,3,3,
            2,2,2,2,
            1,1,1,1
        };
        public static sbyte[] lfo_pm_table=new sbyte[8*8*2]
        {
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,

            0, 0, 0, 0, 0, 0, 0, 0,
            1, 0, 0, 0,-1, 0, 0, 0,

            1, 0, 0, 0,-1, 0, 0, 0,
            2, 1, 0,-1,-2,-1, 0, 1,

            1, 0, 0, 0,-1, 0, 0, 0,
            3, 1, 0,-1,-3,-1, 0, 1,

            2, 1, 0,-1,-2,-1, 0, 1,
            4, 2, 0,-2,-4,-2, 0, 2,

            2, 1, 0,-1,-2,-1, 0, 1,
            5, 2, 0,-2,-5,-2, 0, 2,

            3, 1, 0,-1,-3,-1, 0, 1,
            6, 3, 0,-3,-6,-3, 0, 3,

            3, 1, 0,-1,-3,-1, 0, 1,
            7, 3, 0,-3,-7,-3, 0, 3
        };
        public static int num_lock = 0;
        public static int phase_modulation;
        public static int output0;
        public static uint LFO_AM;
        public static int LFO_PM;
        public delegate void OPL_TIMERHANDLER(int timer, Atime period);
        public delegate void OPL_IRQHANDLER(int irq);
        public delegate void OPL_UPDATEHANDLER(int min_interval_us);
        private static int limit(int val, int max, int min)
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
        private static void OPL_STATUS_SET(int flag)
        {
            OPL.status |= (byte)flag;
            if ((OPL.status & 0x80)==0)
            {
                if ((OPL.status & OPL.statusmask)!=0)
                {
                    OPL.status |= 0x80;
                    if (OPL.IRQHandler!=null)
                    {
                        //(OPL.IRQHandler)(OPL.IRQParam, 1);
                    }
                }
            }
        }
        private static void OPL_STATUS_RESET(int flag)
        {
            OPL.status &= (byte)(~flag);
            if ((OPL.status & 0x80) != 0)
            {
                if ((OPL.status & OPL.statusmask) == 0)
                {
                    OPL.status &= 0x7f;
                    if (OPL.IRQHandler != null)
                    {
                        //(OPL.IRQHandler)(OPL.IRQParam,0);
                    }
                }
            }
        }
        private static void OPL_STATUSMASK_SET(int flag)
        {
            OPL.statusmask = (byte)flag;
            OPL_STATUS_SET(0);
            OPL_STATUS_RESET(0);
        }
        private static void advance_lfo()
        {
            byte tmp;
            OPL.lfo_am_cnt += OPL.lfo_am_inc;
            if (OPL.lfo_am_cnt >= ((uint)210 << 24))
                OPL.lfo_am_cnt -= ((uint)210 << 24);
            tmp = lfo_am_table[OPL.lfo_am_cnt >> 24];
            if (OPL.lfo_am_depth!=0)
                LFO_AM = tmp;
            else
                LFO_AM = (uint)(tmp >> 2);
            OPL.lfo_pm_cnt += OPL.lfo_pm_inc;
            LFO_PM = (int)(((OPL.lfo_pm_cnt >> 24) & 7) | OPL.lfo_pm_depth_range);
        }
        private static void advance()
        {
            int i;
            OPL.eg_timer += OPL.eg_timer_add;
            while (OPL.eg_timer >= OPL.eg_timer_overflow)
            {
                OPL.eg_timer -= OPL.eg_timer_overflow;
                OPL.eg_cnt++;
                for (i = 0; i < 9 * 2; i++)
                {
                    switch (OPL.P_CH[i / 2].SLOT[i & 1].state)
                    {
                        case 4:
                            if ((OPL.eg_cnt & ((1 << OPL.P_CH[i / 2].SLOT[i & 1].eg_sh_ar) - 1)) == 0)
                            {
                                OPL.P_CH[i / 2].SLOT[i & 1].volume += (~OPL.P_CH[i / 2].SLOT[i & 1].volume * (eg_inc[OPL.P_CH[i / 2].SLOT[i & 1].eg_sel_ar + ((OPL.eg_cnt >> OPL.P_CH[i / 2].SLOT[i & 1].eg_sh_ar) & 7)])) >> 3;
                                if (OPL.P_CH[i / 2].SLOT[i & 1].volume <= 0)
                                {
                                    OPL.P_CH[i / 2].SLOT[i & 1].volume = 0;
                                    OPL.P_CH[i / 2].SLOT[i & 1].state = 3;
                                }
                            }
                            break;
                        case 3:
                            if ((OPL.eg_cnt & ((1 << OPL.P_CH[i / 2].SLOT[i & 1].eg_sh_dr) - 1)) == 0)
                            {
                                OPL.P_CH[i / 2].SLOT[i & 1].volume += eg_inc[OPL.P_CH[i / 2].SLOT[i & 1].eg_sel_dr + ((OPL.eg_cnt >> OPL.P_CH[i / 2].SLOT[i & 1].eg_sh_dr) & 7)];
                                if (OPL.P_CH[i / 2].SLOT[i & 1].volume >= OPL.P_CH[i / 2].SLOT[i & 1].sl)
                                    OPL.P_CH[i / 2].SLOT[i & 1].state = 2;
                            }
                            break;
                        case 2:
                            if (OPL.P_CH[i / 2].SLOT[i & 1].eg_type != 0)
                            {

                            }
                            else
                            {
                                if ((OPL.eg_cnt & ((1 << OPL.P_CH[i / 2].SLOT[i & 1].eg_sh_rr) - 1)) == 0)
                                {
                                    OPL.P_CH[i / 2].SLOT[i & 1].volume += eg_inc[OPL.P_CH[i / 2].SLOT[i & 1].eg_sel_rr + ((OPL.eg_cnt >> OPL.P_CH[i / 2].SLOT[i & 1].eg_sh_rr) & 7)];
                                    if (OPL.P_CH[i / 2].SLOT[i & 1].volume >= 0x1ff)
                                        OPL.P_CH[i / 2].SLOT[i & 1].volume = 0x1ff;
                                }
                            }
                            break;
                        case 1:
                            if ((OPL.eg_cnt & ((1 << OPL.P_CH[i / 2].SLOT[i & 1].eg_sh_rr) - 1)) == 0)
                            {
                                OPL.P_CH[i / 2].SLOT[i & 1].volume += eg_inc[OPL.P_CH[i / 2].SLOT[i & 1].eg_sel_rr + ((OPL.eg_cnt >> OPL.P_CH[i / 2].SLOT[i & 1].eg_sh_rr) & 7)];
                                if (OPL.P_CH[i / 2].SLOT[i & 1].volume >= 0x1ff)
                                {
                                    OPL.P_CH[i / 2].SLOT[i & 1].volume = 0x1ff;
                                    OPL.P_CH[i / 2].SLOT[i & 1].state = 0;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            for (i = 0; i < 9 * 2; i++)
            {
                if (OPL.P_CH[i / 2].SLOT[i & 1].vib != 0)
                {
                    byte block;
                    uint block_fnum = OPL.P_CH[i / 2].block_fnum;
                    uint fnum_lfo = (block_fnum & 0x0380) >> 7;
                    int lfo_fn_table_index_offset = lfo_pm_table[LFO_PM + 16 * fnum_lfo];
                    if (lfo_fn_table_index_offset != 0)
                    {
                        block_fnum += (uint)lfo_fn_table_index_offset;
                        block = (byte)((block_fnum & 0x1c00) >> 10);
                        OPL.P_CH[i / 2].SLOT[i & 1].Cnt += (OPL.fn_tab[block_fnum & 0x03ff] >> (7 - block)) * OPL.P_CH[i / 2].SLOT[i & 1].mul;
                    }
                    else
                    {
                        OPL.P_CH[i / 2].SLOT[i & 1].Cnt += OPL.P_CH[i / 2].SLOT[i & 1].Incr;
                    }
                }
                else
                {
                    OPL.P_CH[i / 2].SLOT[i & 1].Cnt += OPL.P_CH[i / 2].SLOT[i & 1].Incr;
                }
            }
            OPL.noise_p += OPL.noise_f;
            i = (int)(OPL.noise_p >> 16);
            OPL.noise_p &= 0xffff;
            while (i != 0)
            {
                if ((OPL.noise_rng & 1) != 0)
                    OPL.noise_rng ^= 0x800302;
                OPL.noise_rng >>= 1;
                i--;
            }
        }
        private static int op_calc(uint phase, int env, int pm, int wave_tab)
        {
            uint p;
            p = (uint)((env << 4) + sin_tab[wave_tab + ((((int)((phase & ~0xffff) + (pm << 16))) >> 16) & 0x3ff)]);
            if (p >= 0x1800)
                return 0;
            return tl_tab[p];
        }

        private static int op_calc1(uint phase, int env, int pm, int wave_tab)
        {
            uint p;
            p = (uint)((env << 4) + sin_tab[wave_tab + ((((int)((phase & ~0xffff) + pm)) >> 16) & 0x3ff)]);
            if (p >= 0x1800)
                return 0;
            return tl_tab[p];
        }
        private static uint volume_calc(int chan, int slot)
        {
            uint i1;
            i1 = (uint)(OPL.P_CH[chan].SLOT[slot].TLL + (uint)OPL.P_CH[chan].SLOT[slot].volume + (LFO_AM & OPL.P_CH[chan].SLOT[slot].AMmask));
            return i1;
        }
        private static void OPL_CALC_CH(int chan)
        {
            uint env;
            int out1;
            phase_modulation = 0;
            env = volume_calc(chan, 0);
            out1 = OPL.P_CH[chan].SLOT[0].op1_out[0] + OPL.P_CH[chan].SLOT[0].op1_out[1];
            OPL.P_CH[chan].SLOT[0].op1_out[0] = OPL.P_CH[chan].SLOT[0].op1_out[1];
            if (OPL.P_CH[chan].SLOT[0].iconnect == 1)
            {
                output0 += OPL.P_CH[chan].SLOT[0].op1_out[0];
            }
            else if (OPL.P_CH[chan].SLOT[0].iconnect == 2)
            {
                phase_modulation += OPL.P_CH[chan].SLOT[0].op1_out[0];
            }
            else
            {
                int i1 = 1;
            }
            OPL.P_CH[chan].SLOT[0].op1_out[1] = 0;
            if (env < 0x180)
            {
                if (OPL.P_CH[chan].SLOT[0].FB == 0)
                    out1 = 0;
                OPL.P_CH[chan].SLOT[0].op1_out[1] = op_calc1(OPL.P_CH[chan].SLOT[0].Cnt, (int)env, (out1 << OPL.P_CH[chan].SLOT[0].FB), OPL.P_CH[chan].SLOT[0].wavetable);
            }
            env = volume_calc(chan, 1);
            if (env < 0x180)
                output0 += op_calc(OPL.P_CH[chan].SLOT[1].Cnt, (int)env, phase_modulation, OPL.P_CH[chan].SLOT[1].wavetable);
        }
        private static void OPL_CALC_RH(uint noise)
        {
            int out1;
            uint env;
            phase_modulation = 0;
            env = volume_calc(6, 0);
            out1 = OPL.P_CH[6].SLOT[0].op1_out[0] + OPL.P_CH[6].SLOT[0].op1_out[1];
            OPL.P_CH[6].SLOT[0].op1_out[0] = OPL.P_CH[6].SLOT[0].op1_out[1];
            if (OPL.P_CH[6].SLOT[0].CON == 0)
                phase_modulation = OPL.P_CH[6].SLOT[0].op1_out[0];
            OPL.P_CH[6].SLOT[0].op1_out[1] = 0;
            if (env < 0x180)
            {
                if (OPL.P_CH[6].SLOT[0].FB == 0)
                    out1 = 0;
                OPL.P_CH[6].SLOT[0].op1_out[1] = op_calc1(OPL.P_CH[6].SLOT[0].Cnt, (int)env, (out1 << OPL.P_CH[6].SLOT[0].FB), OPL.P_CH[6].SLOT[0].wavetable);
            }
            env = volume_calc(6, 1);
            if (env < 0x180)
                output0 += op_calc(OPL.P_CH[6].SLOT[1].Cnt, (int)env, phase_modulation, OPL.P_CH[6].SLOT[1].wavetable) * 2;
            env = volume_calc(7, 0);
            if (env < 0x180)
            {
                byte bit7 = (byte)(((OPL.P_CH[7].SLOT[0].Cnt >> 16) >> 7) & 1);
                byte bit3 = (byte)(((OPL.P_CH[7].SLOT[0].Cnt >> 16) >> 3) & 1);
                byte bit2 = (byte)(((OPL.P_CH[7].SLOT[0].Cnt >> 16) >> 2) & 1);
                byte res1 = (byte)((bit2 ^ bit7) | bit3);
                uint phase = (uint)(res1 != 0 ? (0x200 | (0xd0 >> 2)) : 0xd0);
                byte bit5e = (byte)(((OPL.P_CH[8].SLOT[1].Cnt >> 16) >> 5) & 1);
                byte bit3e = (byte)(((OPL.P_CH[8].SLOT[1].Cnt >> 16) >> 3) & 1);
                byte res2 = (byte)(bit3e ^ bit5e);
                if (res2 != 0)
                    phase = (0x200 | (0xd0 >> 2));
                if ((phase & 0x200) != 0)
                {
                    if (noise != 0)
                        phase = 0x200 | 0xd0;
                }
                else
                {
                    if (noise != 0)
                        phase = 0xd0 >> 2;
                }
                output0 += op_calc(phase << 16, (int)env, 0, OPL.P_CH[7].SLOT[0].wavetable) * 2;
            }
            env = volume_calc(7, 1);
            if (env < 0x180)
            {
                byte bit8 = (byte)(((OPL.P_CH[7].SLOT[0].Cnt >> 16) >> 8) & 1);
                uint phase = (uint)(bit8 != 0 ? 0x200 : 0x100);
                if (noise != 0)
                    phase ^= 0x100;
                output0 += op_calc(phase << 16, (int)env, 0, OPL.P_CH[7].SLOT[1].wavetable) * 2;
            }
            env = volume_calc(8, 0);
            if (env < 0x180)
                output0 += op_calc(OPL.P_CH[8].SLOT[0].Cnt, (int)env, 0, OPL.P_CH[8].SLOT[0].wavetable) * 2;
            env = volume_calc(8, 1);
            if (env < 0x180)
            {
                byte bit7 = (byte)(((OPL.P_CH[7].SLOT[0].Cnt >> 16) >> 7) & 1);
                byte bit3 = (byte)(((OPL.P_CH[7].SLOT[0].Cnt >> 16) >> 3) & 1);
                byte bit2 = (byte)(((OPL.P_CH[7].SLOT[0].Cnt >> 16) >> 2) & 1);
                byte res1 = (byte)((bit2 ^ bit7) | bit3);
                uint phase = (uint)(res1 != 0 ? 0x300 : 0x100);
                byte bit5e = (byte)(((OPL.P_CH[8].SLOT[1].Cnt >> 16) >> 5) & 1);
                byte bit3e = (byte)(((OPL.P_CH[8].SLOT[1].Cnt >> 16) >> 3) & 1);
                byte res2 = (byte)(bit3e ^ bit5e);
                if (res2 != 0)
                    phase = 0x300;
                output0 += op_calc(phase << 16, (int)env, 0, OPL.P_CH[8].SLOT[1].wavetable) * 2;
            }
        }
        private static int init_tables()
        {
            int i, x;
            int n;
            double o, m;
            for (x = 0; x < 0x100; x++)
            {
                m = (1 << 16) / Math.Pow(2, (x + 1) * (1.0 / 32) / 8.0);
                m = Math.Floor(m);
                n = (int)m;
                n >>= 4;
                if ((n & 1) != 0)
                    n = (n >> 1) + 1;
                else
                    n = n >> 1;
                n <<= 1;
                tl_tab[x * 2 + 0] = n;
                tl_tab[x * 2 + 1] = -tl_tab[x * 2 + 0];
                for (i = 1; i < 12; i++)
                {
                    tl_tab[x * 2 + 0 + i * 2 * 0x100] = tl_tab[x * 2 + 0] >> i;
                    tl_tab[x * 2 + 1 + i * 2 * 0x100] = -tl_tab[x * 2 + 0 + i * 2 * 0x100];
                }
            }
            for (i = 0; i < 0x400; i++)
            {
                m = Math.Sin(((i * 2) + 1) * Math.PI / 0x400);
                if (m > 0.0)
                    o = 8 * Math.Log(1.0 / m) / Math.Log(2);
                else
                    o = 8 * Math.Log(-1.0 / m) / Math.Log(2);
                o = o * 32;
                n = (int)(2.0 * o);
                if ((n & 1) != 0)
                    n = (n >> 1) + 1;
                else
                    n = n >> 1;
                sin_tab[i] = (uint)(n * 2 + (m >= 0.0 ? 0 : 1));
            }
            for (i = 0; i < 0x400; i++)
            {
                if ((i & 0x200) != 0)
                    sin_tab[0x400 + i] = 0x1800;
                else
                    sin_tab[0x400 + i] = sin_tab[i];
                sin_tab[2 * 0x400 + i] = sin_tab[i & (0x3ff >> 1)];
                if ((i & 0x100) != 0)
                    sin_tab[3 * 0x400 + i] = 0x1800;
                else
                    sin_tab[3 * 0x400 + i] = sin_tab[i & (0x3ff >> 2)];
            }
            return 1;
        }
        private static void OPL_initalize()
        {
            int i;
            OPL.freqbase = (OPL.rate!=0) ? ((double)OPL.clock / 72.0) / OPL.rate : 0;
            OPL.TimerBase =Attotime.attotime_mul(Attotime.ATTOTIME_IN_HZ((int)OPL.clock), 72);
            for (i = 0; i < 1024; i++)
            {
                OPL.fn_tab[i] = (uint)((double)i * 64 * OPL.freqbase * (1 << (16 - 10)));
            }
            OPL.lfo_am_inc = (uint)(0x40000 * OPL.freqbase);
            OPL.lfo_pm_inc = (uint)(0x4000 * OPL.freqbase);
            OPL.noise_f = (uint)(0x10000 * OPL.freqbase);
            OPL.eg_timer_add = (uint)(0x10000 * OPL.freqbase);
            OPL.eg_timer_overflow = 0x10000;
        }
        private static void FM_KEYON(int chan, int slot, uint key_set)
        {
            if (OPL.P_CH[chan].SLOT[slot].key == 0)
            {
                OPL.P_CH[chan].SLOT[slot].Cnt = 0;
                OPL.P_CH[chan].SLOT[slot].state = 4;
            }
            OPL.P_CH[chan].SLOT[slot].key |= key_set;
        }
        private static void FM_KEYOFF(int chan, int slot, uint key_clr)
        {
            if (OPL.P_CH[chan].SLOT[slot].key != 0)
            {
                OPL.P_CH[chan].SLOT[slot].key &= key_clr;
                if (OPL.P_CH[chan].SLOT[slot].key == 0)
                {
                    if (OPL.P_CH[chan].SLOT[slot].state > 1)
                        OPL.P_CH[chan].SLOT[slot].state = 1;
                }
            }
        }
        private static void CALC_FCSLOT(int chan, int slot)
        {
            int ksr;
            OPL.P_CH[chan].SLOT[slot].Incr = OPL.P_CH[chan].fc * OPL.P_CH[chan].SLOT[slot].mul;
            ksr = OPL.P_CH[chan].kcode >> OPL.P_CH[chan].SLOT[slot].KSR;
            if (OPL.P_CH[chan].SLOT[slot].ksr != ksr)
            {
                OPL.P_CH[chan].SLOT[slot].ksr = (byte)ksr;
                if ((OPL.P_CH[chan].SLOT[slot].ar + OPL.P_CH[chan].SLOT[slot].ksr) < 78)
                {
                    OPL.P_CH[chan].SLOT[slot].eg_sh_ar = eg_rate_shift[OPL.P_CH[chan].SLOT[slot].ar + OPL.P_CH[chan].SLOT[slot].ksr];
                    OPL.P_CH[chan].SLOT[slot].eg_sel_ar = eg_rate_select[OPL.P_CH[chan].SLOT[slot].ar + OPL.P_CH[chan].SLOT[slot].ksr];
                }
                else
                {
                    OPL.P_CH[chan].SLOT[slot].eg_sh_ar = 0;
                    OPL.P_CH[chan].SLOT[slot].eg_sel_ar = 13 * 8;
                }
                OPL.P_CH[chan].SLOT[slot].eg_sh_dr = eg_rate_shift[OPL.P_CH[chan].SLOT[slot].dr + OPL.P_CH[chan].SLOT[slot].ksr];
                OPL.P_CH[chan].SLOT[slot].eg_sel_dr = eg_rate_select[OPL.P_CH[chan].SLOT[slot].dr + OPL.P_CH[chan].SLOT[slot].ksr];
                OPL.P_CH[chan].SLOT[slot].eg_sh_rr = eg_rate_shift[OPL.P_CH[chan].SLOT[slot].rr + OPL.P_CH[chan].SLOT[slot].ksr];
                OPL.P_CH[chan].SLOT[slot].eg_sel_rr = eg_rate_select[OPL.P_CH[chan].SLOT[slot].rr + OPL.P_CH[chan].SLOT[slot].ksr];
            }
        }
        private static void set_mul(int slot, int v)
        {
            OPL.P_CH[slot / 2].SLOT[slot & 1].mul = mul_tab[v & 0x0f];
            OPL.P_CH[slot / 2].SLOT[slot & 1].KSR = (byte)((v & 0x10) != 0 ? 0 : 2);
            OPL.P_CH[slot / 2].SLOT[slot & 1].eg_type = (byte)(v & 0x20);
            OPL.P_CH[slot / 2].SLOT[slot & 1].vib = (byte)(v & 0x40);
            OPL.P_CH[slot / 2].SLOT[slot & 1].AMmask = (uint)((v & 0x80) != 0 ? ~0 : 0);
            CALC_FCSLOT(slot / 2, slot & 1);
        }
        private static void set_ksl_tl(int slot, int v)
        {
            int ksl = v >> 6;
            OPL.P_CH[slot / 2].SLOT[slot & 1].ksl = (byte)(ksl != 0 ? 3 - ksl : 31);
            OPL.P_CH[slot / 2].SLOT[slot & 1].TL = (uint)((v & 0x3f) << 2);
            OPL.P_CH[slot / 2].SLOT[slot & 1].TLL = (int)(OPL.P_CH[slot / 2].SLOT[slot & 1].TL + (OPL.P_CH[slot / 2].ksl_base >> OPL.P_CH[slot / 2].SLOT[slot & 1].ksl));
        }
        private static void set_ar_dr(int slot, int v)
        {
            OPL.P_CH[slot / 2].SLOT[slot & 1].ar = (uint)((v >> 4) != 0 ? 16 + ((v >> 4) << 2) : 0);
            if ((OPL.P_CH[slot / 2].SLOT[slot & 1].ar + OPL.P_CH[slot / 2].SLOT[slot & 1].ksr) < 16 + 62)
            {
                OPL.P_CH[slot / 2].SLOT[slot & 1].eg_sh_ar = eg_rate_shift[OPL.P_CH[slot / 2].SLOT[slot & 1].ar + OPL.P_CH[slot / 2].SLOT[slot & 1].ksr];
                OPL.P_CH[slot / 2].SLOT[slot & 1].eg_sel_ar = eg_rate_select[OPL.P_CH[slot / 2].SLOT[slot & 1].ar + OPL.P_CH[slot / 2].SLOT[slot & 1].ksr];
            }
            else
            {
                OPL.P_CH[slot / 2].SLOT[slot & 1].eg_sh_ar = 0;
                OPL.P_CH[slot / 2].SLOT[slot & 1].eg_sel_ar = 13 * 8;
            }
            OPL.P_CH[slot / 2].SLOT[slot & 1].dr = (uint)((v & 0x0f) != 0 ? 16 + ((v & 0x0f) << 2) : 0);
            OPL.P_CH[slot / 2].SLOT[slot & 1].eg_sh_dr = eg_rate_shift[OPL.P_CH[slot / 2].SLOT[slot & 1].dr + OPL.P_CH[slot / 2].SLOT[slot & 1].ksr];
            OPL.P_CH[slot / 2].SLOT[slot & 1].eg_sel_dr = eg_rate_select[OPL.P_CH[slot / 2].SLOT[slot & 1].dr + OPL.P_CH[slot / 2].SLOT[slot & 1].ksr];
        }
        private static void set_sl_rr(int slot, int v)
        {
            OPL.P_CH[slot / 2].SLOT[slot & 1].sl = sl_tab[v >> 4];
            OPL.P_CH[slot / 2].SLOT[slot & 1].rr = (uint)((v & 0x0f) != 0 ? 16 + ((v & 0x0f) << 2) : 0);
            OPL.P_CH[slot / 2].SLOT[slot & 1].eg_sh_rr = eg_rate_shift[OPL.P_CH[slot / 2].SLOT[slot & 1].rr + OPL.P_CH[slot / 2].SLOT[slot & 1].ksr];
            OPL.P_CH[slot / 2].SLOT[slot & 1].eg_sel_rr = eg_rate_select[OPL.P_CH[slot / 2].SLOT[slot & 1].rr + OPL.P_CH[slot / 2].SLOT[slot & 1].ksr];
        }
        static void OPLWriteReg(int r, int v)
        {
            int slot;
            int block_fnum;
            r &= 0xff;
            v &= 0xff;
            switch (r & 0xe0)
            {
                case 0x00:
                    switch (r & 0x1f)
                    {
                        case 0x01:
                            if ((OPL.type & 0x01) != 0)
                            {
                                OPL.wavesel = (byte)(v & 0x20);
                            }
                            break;
                        case 0x02:
                            OPL.T[0] = (uint)((256 - v) * 4);
                            break;
                        case 0x03:
                            OPL.T[1] = (uint)((256 - v) * 16);
                            break;
                        case 0x04:
                            if ((v & 0x80) != 0)
                            {
                                OPL_STATUS_RESET(0x7f - 0x08);
                            }
                            else
                            {
                                byte st1 = (byte)(v & 1);
                                byte st2 = (byte)((v >> 1) & 1);
                                OPL_STATUS_RESET(v & (0x78 - 0x08));
                                OPL_STATUSMASK_SET((~v) & 0x78);
                                if (OPL.st[1] != st2)
                                {
                                    Atime period = st2 != 0 ? Attotime.attotime_mul(OPL.TimerBase, OPL.T[1]) : Attotime.ATTOTIME_ZERO;
                                    OPL.st[1] = st2;
                                    if (OPL.timer_handler != null)
                                    {
                                        //(OPL.timer_handler)(OPL.TimerParam,1,period);
                                    }
                                }
                                if (OPL.st[0] != st1)
                                {
                                    Atime period = st1 != 0 ? Attotime.attotime_mul(OPL.TimerBase, OPL.T[0]) : Attotime.ATTOTIME_ZERO;
                                    OPL.st[0] = st1;
                                    if (OPL.timer_handler != null)
                                    {
                                        //(OPL.timer_handler)(OPL.TimerParam,0,period);
                                    }
                                }
                            }
                            break;
                        case 0x08:
                            OPL.mode = (byte)v;
                            break;
                        default:
                            break;
                    }
                    break;
                case 0x20:
                    slot = slot_array[r & 0x1f];
                    if (slot < 0)
                        return;
                    set_mul(slot, v);
                    break;
                case 0x40:
                    slot = slot_array[r & 0x1f];
                    if (slot < 0)
                        return;
                    set_ksl_tl(slot, v);
                    break;
                case 0x60:
                    slot = slot_array[r & 0x1f];
                    if (slot < 0)
                        return;
                    set_ar_dr(slot, v);
                    break;
                case 0x80:
                    slot = slot_array[r & 0x1f];
                    if (slot < 0)
                        return;
                    set_sl_rr(slot, v);
                    break;
                case 0xa0:
                    if (r == 0xbd)
                    {
                        OPL.lfo_am_depth = (byte)(v & 0x80);
                        OPL.lfo_pm_depth_range = (byte)((v & 0x40) != 0 ? 8 : 0);
                        OPL.rhythm = (byte)(v & 0x3f);
                        if ((OPL.rhythm & 0x20) != 0)
                        {
                            if ((v & 0x10) != 0)
                            {
                                FM_KEYON(6, 0, 2);
                                FM_KEYON(6, 0, 2);
                            }
                            else
                            {
                                FM_KEYOFF(6, 0, unchecked((uint)(~2)));
                                FM_KEYOFF(6, 0, unchecked((uint)(~2)));
                            }
                            if ((v & 0x01) != 0)
                                FM_KEYON(7, 0, 2);
                            else
                                FM_KEYOFF(7, 0, unchecked((uint)(~2)));
                            if ((v & 0x08) != 0)
                                FM_KEYON(7, 1, 2);
                            else
                                FM_KEYOFF(7, 1, unchecked((uint)(~2)));
                            if ((v & 0x04) != 0)
                                FM_KEYON(8, 0, 2);
                            else
                                FM_KEYOFF(8, 0, unchecked((uint)(~2)));
                            if ((v & 0x02) != 0)
                                FM_KEYON(8, 1, 2);
                            else
                                FM_KEYOFF(8, 1, unchecked((uint)(~2)));
                        }
                        else
                        {
                            FM_KEYOFF(6, 0, unchecked((uint)(~2)));
                            FM_KEYOFF(6, 1, unchecked((uint)(~2)));
                            FM_KEYOFF(7, 0, unchecked((uint)(~2)));
                            FM_KEYOFF(7, 1, unchecked((uint)(~2)));
                            FM_KEYOFF(8, 0, unchecked((uint)(~2)));
                            FM_KEYOFF(8, 1, unchecked((uint)(~2)));
                        }
                        return;
                    }
                    if ((r & 0x0f) > 8)
                        return;
                    if ((r & 0x10) == 0)
                    {
                        block_fnum = (int)((OPL.P_CH[r & 0x0f].block_fnum & 0x1f00) | (uint)v);
                    }
                    else
                    {
                        block_fnum = (int)((uint)((v & 0x1f) << 8) | (OPL.P_CH[r & 0x0f].block_fnum & 0xff));
                        if ((v & 0x20) != 0)
                        {
                            FM_KEYON(r & 0x0f, 0, 1);
                            FM_KEYON(r & 0x0f, 1, 1);
                        }
                        else
                        {
                            FM_KEYOFF(r & 0x0f, 0, unchecked((uint)(~1)));
                            FM_KEYOFF(r & 0x0f, 1, unchecked((uint)(~1)));
                        }
                    }
                    if (OPL.P_CH[r & 0x0f].block_fnum != block_fnum)
                    {
                        byte block = (byte)(block_fnum >> 10);
                        OPL.P_CH[r & 0x0f].block_fnum = (uint)block_fnum;
                        OPL.P_CH[r & 0x0f].ksl_base = ksl_tab[block_fnum >> 6];
                        OPL.P_CH[r & 0x0f].fc = OPL.fn_tab[block_fnum & 0x03ff] >> (7 - block);
                        OPL.P_CH[r & 0x0f].kcode = (byte)((OPL.P_CH[r & 0x0f].block_fnum & 0x1c00) >> 9);
                        if ((OPL.mode & 0x40) != 0)
                            OPL.P_CH[r & 0x0f].kcode |= (byte)((OPL.P_CH[r & 0x0f].block_fnum & 0x100) >> 8);
                        else
                            OPL.P_CH[r & 0x0f].kcode |= (byte)((OPL.P_CH[r & 0x0f].block_fnum & 0x200) >> 9);
                        OPL.P_CH[r & 0x0f].SLOT[0].TLL = (int)(OPL.P_CH[r & 0x0f].SLOT[0].TL + (OPL.P_CH[r & 0x0f].ksl_base >> OPL.P_CH[r & 0x0f].SLOT[0].ksl));
                        OPL.P_CH[r & 0x0f].SLOT[1].TLL = (int)(OPL.P_CH[r & 0x0f].SLOT[1].TL + (OPL.P_CH[r & 0x0f].ksl_base >> OPL.P_CH[r & 0x0f].SLOT[1].ksl));
                        CALC_FCSLOT(r & 0x0f, 0);
                        CALC_FCSLOT(r & 0x0f, 1);
                    }
                    break;
                case 0xc0:
                    if ((r & 0x0f) > 8)
                        return;
                    OPL.P_CH[r & 0x0f].SLOT[0].FB = (byte)(((v >> 1) & 7) != 0 ? ((v >> 1) & 7) + 7 : 0);
                    OPL.P_CH[r & 0x0f].SLOT[0].CON = (byte)(v & 1);
                    OPL.P_CH[r & 0x0f].SLOT[0].iconnect = OPL.P_CH[r & 0x0f].SLOT[0].CON != 0 ? 1 : 2;
                    break;
                case 0xe0:
                    if (OPL.wavesel != 0)
                    {
                        slot = slot_array[r & 0x1f];
                        if (slot < 0)
                            return;
                        OPL.P_CH[slot / 2].SLOT[slot & 1].wavetable = (ushort)((v & 0x03) * 0x400);
                    }
                    break;
            }
        }
        private static int OPL_LockTable()
        {
            num_lock++;
            if (num_lock > 1)
                return 0;
            if (init_tables()==0)
            {
                num_lock--;
                return -1;
            }
            return 0;
        }
        private static void OPL_UnLockTable()
        {
            if (num_lock!=0)
                num_lock--;
        }
        private static void OPLResetChip()
        {
            int c, s;
            int i;
            OPL.eg_timer = 0;
            OPL.eg_cnt = 0;
            OPL.noise_rng = 1;
            OPL.mode = 0;
            OPL_STATUS_RESET(0x7f);
            OPLWriteReg(0x01, 0);
            OPLWriteReg(0x02, 0);
            OPLWriteReg(0x03, 0);
            OPLWriteReg(0x04, 0);
            for (i = 0xff; i >= 0x20; i--)
                OPLWriteReg(i, 0);
            for (c = 0; c < 9; c++)
            {
                for (s = 0; s < 2; s++)
                {
                    OPL.P_CH[c].SLOT[s].wavetable = 0;
                    OPL.P_CH[c].SLOT[s].state = 0;
                    OPL.P_CH[c].SLOT[s].volume = 0x1ff;
                }
            }
        }
        private static FM_OPL OPLCreate(int type, int clock, int rate)
        {
            int i;
            OPL_LockTable();
            OPL = new FM_OPL();
            OPL.P_CH = new OPL_CH[9];
            OPL.fn_tab = new uint[1024];
            OPL.T = new uint[2];
            OPL.st = new byte[2];
            for (i = 0; i < 9; i++)
            {
                OPL.P_CH[i].SLOT = new OPL_SLOT[2];
                OPL.P_CH[i].SLOT[0].op1_out = new int[2];
                OPL.P_CH[i].SLOT[1].op1_out = new int[2];
            }
            OPL.type = (byte)type;
            OPL.clock = clock;
            OPL.rate = rate;
            OPL_initalize();
            return OPL;
        }
        private static void OPLDestroy()
        {
            OPL_UnLockTable();
        }
        private static void OPLSetTimerHandler(OPL_TIMERHANDLER timer_handler)
        {
            OPL.timer_handler = timer_handler;
        }
        private static void OPLSetIRQHandler(OPL_IRQHANDLER IRQHandler)
        {
            OPL.IRQHandler = IRQHandler;
        }
        private static void OPLSetUpdateHandler(OPL_UPDATEHANDLER UpdateHandler)
        {
            OPL.UpdateHandler = UpdateHandler;
        }
        private static int OPLWrite(int a, int v)
        {
            if ((a & 1) == 0)
            {
                OPL.address = (byte)(v & 0xff);
            }
            else
            {
                if (OPL.UpdateHandler != null)
                {
                    OPL.UpdateHandler(0);
                }
                OPLWriteReg(OPL.address, v);
            }
            return OPL.status >> 7;
        }
        private static byte OPLRead(int a)
        {
            if ((a & 1) == 0)
            {
                return (byte)(OPL.status & (OPL.statusmask | 0x80));
            }
            return 0xff;
        }
        private static void CSMKeyControll(int chan)
        {
            FM_KEYON(chan, 0, 4);
            FM_KEYON(chan, 1, 4);
            FM_KEYOFF(chan, 0, 4);
            FM_KEYOFF(chan, 1, 4);
        }
        private static int OPLTimerOver(int c)
        {
            if (c != 0)
            {
                OPL_STATUS_SET(0x20);
            }
            else
            {
                OPL_STATUS_SET(0x40);
                if ((OPL.mode & 0x80) != 0)
                {
                    int ch;
                    if (OPL.UpdateHandler != null)
                    {
                        //OPL.UpdateHandler(OPL.UpdateParam,0);
                    }
                    for (ch = 0; ch < 9; ch++)
                        CSMKeyControll(ch);
                }
            }
            if (OPL.timer_handler != null)
            {
                //(OPL.timer_handler)(OPL.TimerParam,c,attotime_mul(OPL.TimerBase, OPL.T[c]));
            }
            return OPL.status >> 7;
        }
        public static void ym3812_init(int sndindex, int clock, int rate)
        {
            OPLCreate(1, clock, rate);
            //OPL_save_state("YM3812", sndindex);
            ym3812_reset_chip();
        }
        private static void ym3812_shutdown()
        {
            OPLDestroy();
        }
        private static void ym3812_reset_chip()
        {
            OPLResetChip();
        }
        public static int ym3812_write(int a, int v)
        {
            return OPLWrite(a, v);
        }
        private static byte ym3812_read(int a)
        {
            return (byte)(OPLRead(a) | 0x06);
        }
        public static int ym3812_timer_over(int c)
        {
            return OPLTimerOver(c);
        }
        public static void ym3812_set_timer_handler(OPL_TIMERHANDLER timer_handler)
        {
	        OPLSetTimerHandler(timer_handler);
        }
        public static void ym3812_set_irq_handler(OPL_IRQHANDLER IRQHandler)
        {
            OPLSetIRQHandler(IRQHandler);
        }
        public static void ym3812_set_update_handler(OPL_UPDATEHANDLER UpdateHandler)
        {
	        OPLSetUpdateHandler(UpdateHandler);
        }
        public static void ym3812_update_one(int offset, int length)
        {
            byte rhythm = (byte)(OPL.rhythm & 0x20);
            int i;
            for (i = 0; i < length; i++)
            {
                int lt;
                output0 = 0;
                advance_lfo();
                OPL_CALC_CH(0);
                OPL_CALC_CH(1);
                OPL_CALC_CH(2);
                OPL_CALC_CH(3);
                OPL_CALC_CH(4);
                OPL_CALC_CH(5);
                if (rhythm==0)
                {
                    OPL_CALC_CH(6);
                    OPL_CALC_CH(7);
                    OPL_CALC_CH(8);
                }
                else
                {
                    OPL_CALC_RH((OPL.noise_rng >> 0) & 1);
                }
                lt = output0;
                lt = limit(lt, 32767, -32768);
                Sound.ym3812stream.streamoutput[0][offset + i] = lt;
                advance();
            }
        }
        private static void ym3526_init(int sndindex, int clock, int rate)
        {
	        OPLCreate(0,clock,rate);
		    //OPL_save_state("YM3526", sndindex);
		    ym3526_reset_chip();
        }
        private static void ym3526_shutdown()
        {
	        OPLDestroy();
        }
        private static void ym3526_reset_chip()
        {
	        OPLResetChip();
        }
        private static int ym3526_write(int a, int v)
        {
	        return OPLWrite(a, v);
        }
        private static byte ym3526_read(int a)
        {
	        return (byte)(OPLRead(a) | 0x06);
        }
        private static int ym3526_timer_over(int c)
        {
	        return OPLTimerOver(c);
        }
        /*private static void ym3526_set_timer_handler(OPL_TIMERHANDLER timer_handler, void *param)
        {
	        OPLSetTimerHandler(timer_handler, param);
        }
        private static void ym3526_set_irq_handler(OPL_IRQHANDLER IRQHandler,void *param)
        {
	        OPLSetIRQHandler(IRQHandler, param);
        }
        private static void ym3526_set_update_handler(OPL_UPDATEHANDLER UpdateHandler,void *param)
        {
	        OPLSetUpdateHandler(UpdateHandler, param);
        }*/
        private static void ym3526_update_one(int offset, int length)
        {
            byte rhythm = (byte)(OPL.rhythm & 0x20);
            //OPLSAMPLE	*buf = buffer;
            int i;
            for (i = 0; i < length; i++)
            {
                int lt;
                output0 = 0;
                advance_lfo();
                OPL_CALC_CH(0);
                OPL_CALC_CH(1);
                OPL_CALC_CH(2);
                OPL_CALC_CH(3);
                OPL_CALC_CH(4);
                OPL_CALC_CH(5);
                if (rhythm == 0)
                {
                    OPL_CALC_CH(6);
                    OPL_CALC_CH(7);
                    OPL_CALC_CH(8);
                }
                else
                {
                    OPL_CALC_RH((OPL.noise_rng >> 0) & 1);
                }
                lt = output0;
                lt = limit(lt, 32767, -32768);
                //buf[i] = lt;
                Sound.ym3526stream.streamoutput[0][offset + i] = lt;
                advance();
            }
        }
    }
}

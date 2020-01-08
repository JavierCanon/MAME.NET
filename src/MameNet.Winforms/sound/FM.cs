using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mame
{
    public class FM
    {
        public struct FM_SLOT
        {
            public byte KSR;		/* key scale rate  :3-KSR */
            public int ar;			/* attack rate  */
            public int d1r;		/* decay rate   */
            public int d2r;		/* sustain rate */
            public int rr;			/* release rate */
            public byte ksr;		/* key scale rate  :kcode>>(3-KSR) */
            public int mul;		/* multiple        :ML_TABLE[ML] */
            /* Phase Generator */
            public uint phase;		/* phase counter */
            public int Incr;		/* phase step */
            /* Envelope Generator */
            public byte state;		/* phase type */
            public int tl;			/* total level: TL << 3 */
            public int volume;		/* envelope counter */
            public int sl;			/* sustain level:sl_table[SL] */
            public uint vol_out;	/* current output from EG circuit (without AM from LFO) */
            public byte eg_sh_ar;	/*  (attack state) */
            public byte eg_sel_ar;	/*  (attack state) */
            public byte eg_sh_d1r;	/*  (decay state) */
            public byte eg_sel_d1r;	/*  (decay state) */
            public byte eg_sh_d2r;	/*  (sustain state) */
            public byte eg_sel_d2r;	/*  (sustain state) */
            public byte eg_sh_rr;	/*  (release state) */
            public byte eg_sel_rr;	/*  (release state) */
            public byte ssg;		/* SSG-EG waveform */
            public byte ssgn;		/* SSG-EG negated output */
            public uint key;		/* 0=last key was KEY OFF, 1=KEY ON */
            /* LFO */
            public uint AMmask;		/* AM enable flag */
        }
        public struct FM_CH
        {
            public byte ALGO;		/* algorithm */
            public byte FB;			/* feedback shift */
            public int op1_out0, op1_out1;	/* op1 output for feedback */
            public int mem_value;	/* delayed sample (MEM) value */
            public int pms;		/* channel PMS */
            public byte ams;		/* channel AMS */
            public uint fc;			/* fnum,blk:adjusted to sample rate */
            public byte kcode;		/* key code:                        */
            public uint block_fnum;	/* current blk/fnum value for this slot (can be different betweeen slots of one channel in 3slot mode) */
        }
        public struct FM_ST
        {
            public double freqbase;			/* frequency base       */
            public int timer_prescaler;	/* timer prescaler      */
            public Atime busy_expiry_time;	/* expiry time of the busy status */
            public byte address;			/* address register     */
            public byte irq;				/* interrupt level      */
            public byte irqmask;			/* irq mask             */
            public byte status;				/* status flag          */
            public byte mode;				/* mode  CSM / 3SLOT    */
            public byte prescaler_sel;		/* prescaler selector   */
            public byte fn_h;				/* freq latch           */
            public int TA;					/* timer a              */
            public int TAC;				/* timer a counter      */
            public byte TB;					/* timer b              */
            public int TBC;				/* timer b counter      */
        }
        public struct FM_3SLOT
        {
            public uint[] fc;			/* fnum3,blk3: calculated */
            public byte fn_h;			/* freq3 latch */
            public byte[] kcode;		/* key code */
            public uint[] block_fnum;	/* current fnum value for this slot (can be different betweeen slots of one channel in 3slot mode) */
        }
        public struct FM_OPN
        {
            public uint[] pan;	/* fm channels output masks (0xffffffff = enable) */
            public uint eg_cnt;			/* global envelope generator counter */
            public uint eg_timer;		/* global envelope generator counter works at frequency = chipclock/64/3 */
            public uint eg_timer_add;	/* step of eg_timer */
            public uint eg_timer_overflow;/* envelope generator timer overlfows every 3 samples (on real chip) */
            /* there are 2048 FNUMs that can be generated using FNUM/BLK registers
                but LFO works with one more bit of a precision so we really need 4096 elements */
            public uint[] fn_table;	/* fnumber->increment counter */
            /* LFO */
            public int lfo_cnt;
            public int lfo_inc;
            public int[] lfo_freq;	/* LFO FREQ table */
        }
        public struct ADPCM_CH
        {
            public byte flag;			/* port state               */
            public byte flagMask;		/* arrived flag mask        */
            public byte now_data;		/* current ROM data         */
            public uint now_addr;		/* current ROM address      */
            public uint now_step;
            public uint step;
            public uint start;			/* sample data start address*/
            public uint end;			/* sample data end address  */
            public byte IL;				/* Instrument Level         */
            public int adpcm_acc;		/* accumulator              */
            public int adpcm_step;		/* step                     */
            public int adpcm_out;		/* (speedup) hiro-shi!!     */
            public sbyte vol_mul;		/* volume in "0.75dB" steps */
            public byte vol_shift;		/* volume in "-6dB" steps   */
        }
        public struct YM2610chip
        {
            public byte[] REGS;             /* registers            */
            public byte addr_A1;			/* address line A1      */
            /* ADPCM-A unit */
            public byte adpcmTL;			/* adpcmA total level   */
            public byte[] adpcmreg;		/* registers            */
            public byte adpcm_arrivedEndAddress;
        }
        private static int[] ipan = new int[6];
        private static int[] tl_tab = new int[6656];
        /* sin waveform table in 'decibel' scale */
        private static int[] sin_tab = new int[1024];
        /* 0 - 15: 0, 3, 6, 9,12,15,18,21,24,27,30,33,36,39,42,93 (dB)*/
        private static int[] sl_table = new int[16]{
         ( 0*32),( 1*32),( 2*32),(3*32 ),(4*32 ),(5*32 ),(6*32 ),( 7*32),
         ( 8*32),( 9*32),(10*32),(11*32),(12*32),(13*32),(14*32),(31*32)
        };
        private static byte[] eg_inc = new byte[19 * 8]{
        /*cycle:0 1  2 3  4 5  6 7*/

        /* 0 */ 0,1, 0,1, 0,1, 0,1, /* rates 00..11 0 (increment by 0 or 1) */
        /* 1 */ 0,1, 0,1, 1,1, 0,1, /* rates 00..11 1 */
        /* 2 */ 0,1, 1,1, 0,1, 1,1, /* rates 00..11 2 */
        /* 3 */ 0,1, 1,1, 1,1, 1,1, /* rates 00..11 3 */

        /* 4 */ 1,1, 1,1, 1,1, 1,1, /* rate 12 0 (increment by 1) */
        /* 5 */ 1,1, 1,2, 1,1, 1,2, /* rate 12 1 */
        /* 6 */ 1,2, 1,2, 1,2, 1,2, /* rate 12 2 */
        /* 7 */ 1,2, 2,2, 1,2, 2,2, /* rate 12 3 */

        /* 8 */ 2,2, 2,2, 2,2, 2,2, /* rate 13 0 (increment by 2) */
        /* 9 */ 2,2, 2,4, 2,2, 2,4, /* rate 13 1 */
        /*10 */ 2,4, 2,4, 2,4, 2,4, /* rate 13 2 */
        /*11 */ 2,4, 4,4, 2,4, 4,4, /* rate 13 3 */

        /*12 */ 4,4, 4,4, 4,4, 4,4, /* rate 14 0 (increment by 4) */
        /*13 */ 4,4, 4,8, 4,4, 4,8, /* rate 14 1 */
        /*14 */ 4,8, 4,8, 4,8, 4,8, /* rate 14 2 */
        /*15 */ 4,8, 8,8, 4,8, 8,8, /* rate 14 3 */

        /*16 */ 8,8, 8,8, 8,8, 8,8, /* rates 15 0, 15 1, 15 2, 15 3 (increment by 8) */
        /*17 */ 16,16,16,16,16,16,16,16, /* rates 15 2, 15 3 for attack */
        /*18 */ 0,0, 0,0, 0,0, 0,0, /* infinity rates for attack and decay(s) */
        };
        /*note that there is no O(17) in this table - it's directly in the code */
        public static byte[] eg_rate_select = new byte[32 + 64 + 32]{	/* Envelope Generator rates (32 + 64 rates + 32 RKS) */
            /* 32 dummy (infinite time) rates */
            (18*8),(18*8),(18*8),(18*8),(18*8),(18*8),(18*8),(18*8),
            (18*8),(18*8),(18*8),(18*8),(18*8),(18*8),(18*8),(18*8),
            (18*8),(18*8),(18*8),(18*8),(18*8),(18*8),(18*8),(18*8),
            (18*8),(18*8),(18*8),(18*8),(18*8),(18*8),(18*8),(18*8),

            /* rates 00-11 */
            ( 0*8),( 1*8),( 2*8),( 3*8),
            ( 0*8),( 1*8),( 2*8),( 3*8),
            ( 0*8),( 1*8),( 2*8),( 3*8),
            ( 0*8),( 1*8),( 2*8),( 3*8),
            ( 0*8),( 1*8),( 2*8),( 3*8),
            ( 0*8),( 1*8),( 2*8),( 3*8),
            ( 0*8),( 1*8),( 2*8),( 3*8),
            ( 0*8),( 1*8),( 2*8),( 3*8),
            ( 0*8),( 1*8),( 2*8),( 3*8),
            ( 0*8),( 1*8),( 2*8),( 3*8),
            ( 0*8),( 1*8),( 2*8),( 3*8),
            ( 0*8),( 1*8),( 2*8),( 3*8),

            /* rate 12 */
            ( 4*8),( 5*8),( 6*8),( 7*8),

            /* rate 13 */
            ( 8*8),( 9*8),(10*8),(11*8),

            /* rate 14 */
            (12*8),(13*8),(14*8),(15*8),

            /* rate 15 */
            (16*8),(16*8),(16*8),(16*8),

            /* 32 dummy rates (same as 15 3) */
            (16*8),(16*8),(16*8),(16*8),(16*8),(16*8),(16*8),(16*8),
            (16*8),(16*8),(16*8),(16*8),(16*8),(16*8),(16*8),(16*8),
            (16*8),(16*8),(16*8),(16*8),(16*8),(16*8),(16*8),(16*8),
            (16*8),(16*8),(16*8),(16*8),(16*8),(16*8),(16*8),(16*8)
        };
        public static byte[] eg_rate_shift = new byte[32 + 64 + 32]{	/* Envelope Generator counter shifts (32 + 64 rates + 32 RKS) */
            /* 32 infinite time rates */
            (0),(0),(0),(0),(0),(0),(0),(0),
            (0),(0),(0),(0),(0),(0),(0),(0),
            (0),(0),(0),(0),(0),(0),(0),(0),
            (0),(0),(0),(0),(0),(0),(0),(0),

            /* rates 00-11 */
            (11),(11),(11),(11),
            (10),(10),(10),(10),
            ( 9),( 9),( 9),( 9),
            ( 8),( 8),( 8),( 8),
            ( 7),( 7),( 7),( 7),
            ( 6),( 6),( 6),( 6),
            ( 5),( 5),( 5),( 5),
            ( 4),( 4),( 4),( 4),
            ( 3),( 3),( 3),( 3),
            ( 2),( 2),( 2),( 2),
            ( 1),( 1),( 1),( 1),
            ( 0),( 0),( 0),( 0),

            /* rate 12 */
            ( 0),( 0),( 0),( 0),

            /* rate 13 */
            ( 0),( 0),( 0),( 0),

            /* rate 14 */
            ( 0),( 0),( 0),( 0),

            /* rate 15 */
            ( 0),( 0),( 0),( 0),

            /* 32 dummy rates (same as 15 3) */
            ( 0),( 0),( 0),( 0),( 0),( 0),( 0),( 0),
            ( 0),( 0),( 0),( 0),( 0),( 0),( 0),( 0),
            ( 0),( 0),( 0),( 0),( 0),( 0),( 0),( 0),
            ( 0),( 0),( 0),( 0),( 0),( 0),( 0),( 0)
        };
        public static byte[] dt_tab = new byte[4 * 32]{
        /* this is YM2151 and YM2612 phase increment data (in 10.10 fixed point format)*/
        /* FD=0 */
	        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
	        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        /* FD=1 */
	        0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2,
	        2, 3, 3, 3, 4, 4, 4, 5, 5, 6, 6, 7, 8, 8, 8, 8,
        /* FD=2 */
	        1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5,
	        5, 6, 6, 7, 8, 8, 9,10,11,12,13,14,16,16,16,16,
        /* FD=3 */
	        2, 2, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 6, 6, 7,
	        8 , 8, 9,10,11,12,13,14,16,17,19,20,22,22,22,22
        };
        private static byte[] opn_fktable = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 3, 3, 3, 3, 3, 3 };
        private static uint[] lfo_samples_per_step = new uint[8] { 108, 77, 71, 67, 62, 44, 8, 5 };
        private static byte[] lfo_ams_depth_shift = new byte[4] { 8, 3, 1, 0 };
        private static byte[,] lfo_pm_output = new byte[56, 8]{ /* 7 bits meaningful (of F-NUMBER), 8 LFO output levels per one depth (out of 32), 8 LFO depths */
        /* FNUM BIT 4: 000 0001xxxx */
        /* DEPTH 0 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 1 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 2 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 3 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 4 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 5 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 6 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 7 */ {0,   0,   0,   0,   1,   1,   1,   1},

        /* FNUM BIT 5: 000 0010xxxx */
        /* DEPTH 0 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 1 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 2 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 3 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 4 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 5 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 6 */ {0,   0,   0,   0,   1,   1,   1,   1},
        /* DEPTH 7 */ {0,   0,   1,   1,   2,   2,   2,   3},

        /* FNUM BIT 6: 000 0100xxxx */
        /* DEPTH 0 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 1 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 2 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 3 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 4 */ {0,   0,   0,   0,   0,   0,   0,   1},
        /* DEPTH 5 */ {0,   0,   0,   0,   1,   1,   1,   1},
        /* DEPTH 6 */ {0,   0,   1,   1,   2,   2,   2,   3},
        /* DEPTH 7 */ {0,   0,   2,   3,   4,   4,   5,   6},

        /* FNUM BIT 7: 000 1000xxxx */
        /* DEPTH 0 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 1 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 2 */ {0,   0,   0,   0,   0,   0,   1,   1},
        /* DEPTH 3 */ {0,   0,   0,   0,   1,   1,   1,   1},
        /* DEPTH 4 */ {0,   0,   0,   1,   1,   1,   1,   2},
        /* DEPTH 5 */ {0,   0,   1,   1,   2,   2,   2,   3},
        /* DEPTH 6 */ {0,   0,   2,   3,   4,   4,   5,   6},
        /* DEPTH 7 */ {0,   0,   4,   6,   8,   8, 0xa, 0xc},

        /* FNUM BIT 8: 001 0000xxxx */
        /* DEPTH 0 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 1 */ {0,   0,   0,   0,   1,   1,   1,   1},
        /* DEPTH 2 */ {0,   0,   0,   1,   1,   1,   2,   2},
        /* DEPTH 3 */ {0,   0,   1,   1,   2,   2,   3,   3},
        /* DEPTH 4 */ {0,   0,   1,   2,   2,   2,   3,   4},
        /* DEPTH 5 */ {0,   0,   2,   3,   4,   4,   5,   6},
        /* DEPTH 6 */ {0,   0,   4,   6,   8,   8, 0xa, 0xc},
        /* DEPTH 7 */ {0,   0,   8, 0xc,0x10,0x10,0x14,0x18},

        /* FNUM BIT 9: 010 0000xxxx */
        /* DEPTH 0 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 1 */ {0,   0,   0,   0,   2,   2,   2,   2},
        /* DEPTH 2 */ {0,   0,   0,   2,   2,   2,   4,   4},
        /* DEPTH 3 */ {0,   0,   2,   2,   4,   4,   6,   6},
        /* DEPTH 4 */ {0,   0,   2,   4,   4,   4,   6,   8},
        /* DEPTH 5 */ {0,   0,   4,   6,   8,   8, 0xa, 0xc},
        /* DEPTH 6 */ {0,   0,   8, 0xc,0x10,0x10,0x14,0x18},
        /* DEPTH 7 */ {0,   0,0x10,0x18,0x20,0x20,0x28,0x30},

        /* FNUM BIT10: 100 0000xxxx */
        /* DEPTH 0 */ {0,   0,   0,   0,   0,   0,   0,   0},
        /* DEPTH 1 */ {0,   0,   0,   0,   4,   4,   4,   4},
        /* DEPTH 2 */ {0,   0,   0,   4,   4,   4,   8,   8},
        /* DEPTH 3 */ {0,   0,   4,   4,   8,   8, 0xc, 0xc},
        /* DEPTH 4 */ {0,   0,   4,   8,   8,   8, 0xc,0x10},
        /* DEPTH 5 */ {0,   0,   8, 0xc,0x10,0x10,0x14,0x18},
        /* DEPTH 6 */ {0,   0,0x10,0x18,0x20,0x20,0x28,0x30},
        /* DEPTH 7 */ {0,   0,0x20,0x30,0x40,0x40,0x50,0x60},
        };
        /* usual ADPCM table (16 * 1.1^N) */
        private static int[] steps = new int[49]
        {
	         16,  17,   19,   21,   23,   25,   28,
	         31,  34,   37,   41,   45,   50,   55,
	         60,  66,   73,   80,   88,   97,  107,
	        118, 130,  143,  157,  173,  190,  209,
	        230, 253,  279,  307,  337,  371,  408,
	        449, 494,  544,  598,  658,  724,  796,
	        876, 963, 1060, 1166, 1282, 1411, 1552
        };
        /* different from the usual ADPCM table */
        private static int[] step_inc = new int[8] { -1 * 16, -1 * 16, -1 * 16, -1 * 16, 2 * 16, 5 * 16, 7 * 16, 9 * 16 };
        /* speedup purposes only */
        private static int[] jedi_table;
        public static FM_SLOT[,] SLOT;
        private static int[,] idt_tab;
        public static FM_CH[] CH;
        public static FM_ST ST;
        private static int[,] dt_tab2;
        public static FM_3SLOT SL3;
        public static FM_OPN OPN;
        public static ADPCM_CH[] adpcm;
        public static YM2610chip F2610;
        /* all 128 LFO PM waveforms */
        private static int[] lfo_pm_table = new int[128 * 8 * 32]; /* 128 combinations of 7 bits meaningful (of F-NUMBER), 8 LFO depths, 32 LFO output levels per one depth */
        private static int[] iconnect1 = new int[8], iconnect2 = new int[8], iconnect3 = new int[8], iconnect4 = new int[6], imem = new int[13];
        private static int[] out_fm = new int[13];		/* outputs of working channels */ //[8];m2=8,c1=9,c2=10,mem=11,null=12
        private static int[] out_adpcm = new int[4];	/* channel output NONE,LEFT,RIGHT or CENTER for YM2608/YM2610 ADPCM */
        public static int[] out_delta = new int[4];
        public static byte[] ymsndrom;
        private static int LFO_AM;			/* runtime LFO calculations helper */
        private static int LFO_PM;			/* runtime LFO calculations helper */
        private static int fn_max;    /* maximal phase increment (used for phase overflow) */
        public static void FM_init()
        {
            jedi_table = new int[49 * 16];
            SLOT = new FM_SLOT[6, 4];
            idt_tab = new int[6, 4];
            CH = new FM_CH[6];
            dt_tab2 = new int[8, 32];
            adpcm = new ADPCM_CH[6];
            SL3.fc = new uint[3];
            SL3.kcode = new byte[3];
            SL3.block_fnum = new uint[3];
            OPN.pan = new uint[12];
            OPN.fn_table = new uint[4096];
            OPN.lfo_freq = new int[8];
            F2610.REGS = new byte[512];
            F2610.adpcmreg = new byte[0x30];            
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
        /* status set and IRQ handling */
        private static void FM_STATUS_SET(byte flag)
        {
            /* set status flag */
            ST.status |= flag;
            if ((ST.irq == 0) && ((ST.status & ST.irqmask) != 0))
            {
                ST.irq = 1;
                /* callback user interrupt handler (IRQ is OFF to ON) */
                Cpuint.cpunum_set_input_line(1, 0, LineState.ASSERT_LINE);
            }
        }
        /* status reset and IRQ handling */
        private static void FM_STATUS_RESET(byte flag)
        {
            /* reset status flag */
            ST.status &= (byte)~flag;
            if ((ST.irq != 0) && ((ST.status & ST.irqmask) == 0))
            {
                ST.irq = 0;
                /* callback user interrupt handler (IRQ is ON to OFF) */
                Cpuint.cpunum_set_input_line(1, 0, LineState.CLEAR_LINE);
            }
        }
        /* IRQ mask set */
        private static void FM_IRQMASK_SET(byte flag)
        {
            ST.irqmask = flag;
            /* IRQ handling check */
            FM_STATUS_SET(0);
            FM_STATUS_RESET(0);
        }
        /* OPN Mode Register Write */
        private static void set_timers(byte v)
        {
            /* b7 = CSM MODE */
            /* b6 = 3 slot mode */
            /* b5 = reset b */
            /* b4 = reset a */
            /* b3 = timer enable b */
            /* b2 = timer enable a */
            /* b1 = load b */
            /* b0 = load a */
            ST.mode = v;

            /* reset Timer b flag */
            if ((v & 0x20) != 0)
                FM_STATUS_RESET(0x02);
            /* reset Timer a flag */
            if ((v & 0x10) != 0)
                FM_STATUS_RESET(0x01);
            /* load b */
            if ((v & 0x02) != 0)
            {
                if (ST.TBC == 0)
                {
                    ST.TBC = (256 - ST.TB) << 4;
                    /* External timer handler */
                    YM2610.timer_handler1(ST.TBC * ST.timer_prescaler);
                }
            }
            else
            {	/* stop timer b */
                if (ST.TBC != 0)
                {
                    ST.TBC = 0;
                    YM2610.timer_handler1(0);
                }
            }
            /* load a */
            if ((v & 0x01) != 0)
            {
                if (ST.TAC == 0)
                {
                    ST.TAC = (1024 - ST.TA);
                    /* External timer handler */
                    YM2610.timer_handler0(ST.TAC * ST.timer_prescaler);
                }
            }
            else
            {	/* stop timer a */
                if (ST.TAC != 0)
                {
                    ST.TAC = 0;
                    YM2610.timer_handler0(0);
                }
            }
        }
        /* Timer A Overflow */
        private static void TimerAOver()
        {
            /* set status (if enabled) */
            if ((ST.mode & 0x04) != 0)
                FM_STATUS_SET(0x01);
            /* clear or reload the counter */
            ST.TAC = (1024 - ST.TA);
            YM2610.timer_handler0(ST.TAC * ST.timer_prescaler);
        }
        /* Timer B Overflow */
        private static void TimerBOver()
        {
            /* set status (if enabled) */
            if ((ST.mode & 0x08) != 0)
                FM_STATUS_SET(0x02);
            /* clear or reload the counter */
            ST.TBC = (256 - ST.TB) << 4;
            YM2610.timer_handler1(ST.TBC * ST.timer_prescaler);
        }
        private static byte FM_STATUS_FLAG()
        {
            if (Attotime.attotime_compare(ST.busy_expiry_time, Attotime.ATTOTIME_ZERO) != 0)
            {
                if (Attotime.attotime_compare(ST.busy_expiry_time, Timer.get_current_time()) > 0)
                    return (byte)(ST.status | 0x80);	/* with busy */
                /* expire */
                ST.busy_expiry_time = Attotime.ATTOTIME_ZERO;
            }
            return ST.status;
        }
        private static void FM_KEYON(int c, int s)
        {
            if (SLOT[c, s].key == 0)
            {
                SLOT[c, s].key = 1;
                SLOT[c, s].phase = 0;		/* restart Phase Generator */
                SLOT[c, s].state = 4;
            }
        }
        private static void FM_KEYOFF(int c, int s)
        {
            if (SLOT[c, s].key != 0)
            {
                SLOT[c, s].key = 0;
                if (SLOT[c, s].state > 1)
                    SLOT[c, s].state = 1;/* phase -> Release */
            }
        }
        private static void set_value1(int c)
        {
            if (iconnect1[c] == 12)//"null")
            {
                //mem = c1 = c2 = CH[c].op1_out[0];
                out_fm[11] = out_fm[9] = out_fm[10] = CH[c].op1_out0;
            }
            else
            {
                out_fm[iconnect1[c]] = CH[c].op1_out0;
            }
        }
        private static void set_mem(int c)
        {
            if (imem[c] == 8 || imem[c] == 10 || imem[c] == 11)
            {
                out_fm[imem[c]] = CH[c].mem_value;
            }
        }
        /* set algorithm connection */
        private static void setup_connection(int ch)
        {
            switch (CH[ch].ALGO)
            {
                case 0:
                    /* M1---C1---MEM---M2---C2---OUT */
                    //*om1 = &c1;
                    //*oc1 = &mem;
                    //*om2 = &c2;
                    //*memc= &m2;
                    iconnect1[ch] = 9;
                    iconnect2[ch] = 11;
                    iconnect3[ch] = 10;
                    imem[ch] = 8;
                    break;
                case 1:
                    /* M1------+-MEM---M2---C2---OUT */
                    /*      C1-+                     */
                    //*om1 = &mem;
                    //*oc1 = &mem;
                    //*om2 = &c2;
                    //*memc= &m2;
                    iconnect1[ch] = 11;
                    iconnect2[ch] = 11;
                    iconnect3[ch] = 10;
                    imem[ch] = 8;
                    break;
                case 2:
                    /* M1-----------------+-C2---OUT */
                    /*      C1---MEM---M2-+          */
                    //*om1 = &c2;
                    //*oc1 = &mem;
                    //*om2 = &c2;
                    //*memc= &m2;
                    iconnect1[ch] = 10;
                    iconnect2[ch] = 11;
                    iconnect3[ch] = 10;
                    imem[ch] = 8;
                    break;
                case 3:
                    /* M1---C1---MEM------+-C2---OUT */
                    /*                 M2-+          */
                    //*om1 = &c1;
                    //*oc1 = &mem;
                    //*om2 = &c2;
                    //*memc= &c2;
                    iconnect1[ch] = 9;
                    iconnect2[ch] = 11;
                    iconnect3[ch] = 10;
                    imem[ch] = 10;
                    break;
                case 4:
                    /* M1---C1-+-OUT */
                    /* M2---C2-+     */
                    /* MEM: not used */
                    //*om1 = &c1;
                    //*oc1 = carrier;
                    //*om2 = &c2;
                    //*memc= &mem;	/* store it anywhere where it will not be used */
                    iconnect1[ch] = 9;
                    iconnect2[ch] = ch;
                    iconnect3[ch] = 10;
                    imem[ch] = 11;
                    break;
                case 5:
                    /*    +----C1----+     */
                    /* M1-+-MEM---M2-+-OUT */
                    /*    +----C2----+     */
                    //*om1 = 0;	/* special mark */
                    //*oc1 = carrier;
                    //*om2 = carrier;
                    //*memc= &m2;
                    iconnect1[ch] = 12;
                    iconnect2[ch] = ch;
                    iconnect3[ch] = ch;
                    imem[ch] = 8;
                    break;
                case 6:
                    /* M1---C1-+     */
                    /*      M2-+-OUT */
                    /*      C2-+     */
                    /* MEM: not used */
                    //*om1 = &c1;
                    //*oc1 = carrier;
                    //*om2 = carrier;
                    //*memc= &mem;	/* store it anywhere where it will not be used */
                    iconnect1[ch] = 9;
                    iconnect2[ch] = ch;
                    iconnect3[ch] = ch;
                    imem[ch] = 11;
                    break;
                case 7:
                    /* M1-+     */
                    /* C1-+-OUT */
                    /* M2-+     */
                    /* C2-+     */
                    /* MEM: not used*/
                    //*om1 = carrier;
                    //*oc1 = carrier;
                    //*om2 = carrier;
                    //*memc= &mem;	/* store it anywhere where it will not be used */
                    iconnect1[ch] = ch;
                    iconnect2[ch] = ch;
                    iconnect3[ch] = ch;
                    imem[ch] = 11;
                    break;
            }
            //CH[ch].connect4 = carrier;
            iconnect4[ch] = ch;
        }
        /* set detune & multiple */
        private static void set_det_mul(int c, int s, byte v)
        {
            SLOT[c, s].mul = ((v & 0x0f) != 0) ? (v & 0x0f) * 2 : 1;
            idt_tab[c, s] = (v >> 4) & 7;
            SLOT[c, 0].Incr = -1;
        }
        /* set total level */
        private static void set_tl(int c, int s, byte v)
        {
            SLOT[c, s].tl = (v & 0x7f) << (10 - 7); /* 7bit TL */
        }
        /* set attack rate & key scale  */
        private static void set_ar_ksr(int c, int s, byte v)
        {
            byte old_KSR = SLOT[c, s].KSR;
            SLOT[c, s].ar = ((v & 0x1f) != 0) ? 32 + ((v & 0x1f) << 1) : 0;
            SLOT[c, s].KSR = (byte)(3 - (v >> 6));
            if (SLOT[c, s].KSR != old_KSR)
            {
                SLOT[c, 0].Incr = -1;
            }
            /* refresh Attack rate */
            if ((SLOT[c, s].ar + SLOT[c, s].ksr) < 32 + 62)
            {
                SLOT[c, s].eg_sh_ar = eg_rate_shift[SLOT[c, s].ar + SLOT[c, s].ksr];
                SLOT[c, s].eg_sel_ar = eg_rate_select[SLOT[c, s].ar + SLOT[c, s].ksr];
            }
            else
            {
                SLOT[c, s].eg_sh_ar = 0;
                SLOT[c, s].eg_sel_ar = 17 * 8;
            }
        }
        /* set decay rate */
        private static void set_dr(int c, int s, byte v)
        {
            SLOT[c, s].d1r = ((v & 0x1f) != 0) ? 32 + ((v & 0x1f) << 1) : 0;
            SLOT[c, s].eg_sh_d1r = eg_rate_shift[SLOT[c, s].d1r + SLOT[c, s].ksr];
            SLOT[c, s].eg_sel_d1r = eg_rate_select[SLOT[c, s].d1r + SLOT[c, s].ksr];
        }
        /* set sustain rate */
        private static void set_sr(int c, int s, byte v)
        {
            SLOT[c, s].d2r = ((v & 0x1f) != 0) ? 32 + ((v & 0x1f) << 1) : 0;
            SLOT[c, s].eg_sh_d2r = eg_rate_shift[SLOT[c, s].d2r + SLOT[c, s].ksr];
            SLOT[c, s].eg_sel_d2r = eg_rate_select[SLOT[c, s].d2r + SLOT[c, s].ksr];
        }
        /* set release rate */
        private static void set_sl_rr(int c, int s, byte v)
        {
            SLOT[c, s].sl = sl_table[v >> 4];
            SLOT[c, s].rr = 34 + ((v & 0x0f) << 2);
            SLOT[c, s].eg_sh_rr = eg_rate_shift[SLOT[c, s].rr + SLOT[c, s].ksr];
            SLOT[c, s].eg_sel_rr = eg_rate_select[SLOT[c, s].rr + SLOT[c, s].ksr];
        }
        private static int op_calc(uint phase, uint env, int pm)
        {
            uint p;
            p = (uint)((env << 3) + sin_tab[(((int)((phase & 0xffff0000) + (pm << 15))) >> 16) & 0x3ff]);
            if (p >= 6656)
                return 0;
            return tl_tab[p];
        }
        private static int op_calc1(uint phase, uint env, int pm)
        {
            uint p;
            p = (uint)((env << 3) + sin_tab[(((int)((phase & 0xffff0000) + pm)) >> 16) & 0x3ff]);
            if (p >= 6656)
                return 0;
            return tl_tab[p];
        }
        /* advance LFO to next sample */
        private static void advance_lfo()
        {
            byte pos;
            byte prev_pos;
            if (OPN.lfo_inc != 0)	/* LFO enabled ? */
            {
                prev_pos = (byte)(OPN.lfo_cnt >> 24 & 127);
                OPN.lfo_cnt += OPN.lfo_inc;
                pos = (byte)((OPN.lfo_cnt >> 24) & 127);
                /* update AM when LFO output changes */
                /*if (prev_pos != pos)*/
                /* actually I can't optimize is this way without rewritting chan_calc()
                to use chip->lfo_am instead of global lfo_am */
                /* triangle */
                /* AM: 0 to 126 step +2, 126 to 0 step -2 */
                if (pos < 64)
                    LFO_AM = (pos & 63) * 2;
                else
                    LFO_AM = 126 - ((pos & 63) * 2);
                /* PM works with 4 times slower clock */
                prev_pos >>= 2;
                pos >>= 2;
                /* update PM when LFO output changes */
                /*if (prev_pos != pos)*/
                /* can't use global lfo_pm for this optimization, must be chip->lfo_pm instead*/
                LFO_PM = pos;
            }
            else
            {
                LFO_AM = 0;
                LFO_PM = 0;
            }
        }
        private static void advance_eg_channel(int c)
        {
            uint out1;
            byte swap_flag = 0;
            int i;
            for (i = 0; i < 4; i++)
            {
                switch (SLOT[c, i].state)
                {
                    case 4:		/* attack phase */
                        if ((OPN.eg_cnt & ((1 << SLOT[c, i].eg_sh_ar) - 1)) == 0)
                        {
                            SLOT[c, i].volume += (~SLOT[c, i].volume *
                                              (eg_inc[SLOT[c, i].eg_sel_ar + ((OPN.eg_cnt >> SLOT[c, i].eg_sh_ar) & 7)])
                                            ) >> 4;
                            if (SLOT[c, i].volume <= 0)
                            {
                                SLOT[c, i].volume = 0;
                                SLOT[c, i].state = 3;
                            }
                        }
                        break;

                    case 3:	/* decay phase */
                        if ((SLOT[c, i].ssg & 0x08) != 0)	/* SSG EG type envelope selected */
                        {
                            if ((OPN.eg_cnt & ((1 << SLOT[c, i].eg_sh_d1r) - 1)) == 0)
                            {
                                SLOT[c, i].volume += 4 * eg_inc[SLOT[c, i].eg_sel_d1r + ((OPN.eg_cnt >> SLOT[c, i].eg_sh_d1r) & 7)];
                                if (SLOT[c, i].volume >= SLOT[c, i].sl)
                                    SLOT[c, i].state = 2;
                            }
                        }
                        else
                        {
                            if ((OPN.eg_cnt & ((1 << SLOT[c, i].eg_sh_d1r) - 1)) == 0)
                            {
                                SLOT[c, i].volume += eg_inc[SLOT[c, i].eg_sel_d1r + ((OPN.eg_cnt >> SLOT[c, i].eg_sh_d1r) & 7)];
                                if (SLOT[c, i].volume >= SLOT[c, i].sl)
                                    SLOT[c, i].state = 2;
                            }
                        }
                        break;
                    case 2:	/* sustain phase */
                        if ((SLOT[c, i].ssg & 0x08) != 0)	/* SSG EG type envelope selected */
                        {
                            if ((OPN.eg_cnt & ((1 << SLOT[c, i].eg_sh_d2r) - 1)) == 0)
                            {
                                SLOT[c, i].volume += 4 * eg_inc[SLOT[c, i].eg_sel_d2r + ((OPN.eg_cnt >> SLOT[c, i].eg_sh_d2r) & 7)];
                                if (SLOT[c, i].volume >= 512)
                                {
                                    SLOT[c, i].volume = 0x3ff;
                                    if ((SLOT[c, i].ssg & 0x01) != 0)	/* bit 0 = hold */
                                    {
                                        if ((SLOT[c, i].ssgn & 1) != 0)	/* have we swapped once ??? */
                                        {
                                            /* yes, so do nothing, just hold current level */
                                        }
                                        else
                                            swap_flag = (byte)((SLOT[c, i].ssg & 0x02) | 1); /* bit 1 = alternate */
                                    }
                                    else
                                    {
                                        /* same as KEY-ON operation */
                                        /* restart of the Phase Generator should be here,
                                            only if AR is not maximum ??? */
                                        SLOT[c, i].phase = 0;
                                        /* phase -> Attack */
                                        SLOT[c, i].volume = 511;
                                        SLOT[c, i].state = 4;
                                        swap_flag = (byte)(SLOT[c, i].ssg & 0x02); /* bit 1 = alternate */
                                    }
                                }
                            }
                        }
                        else
                        {
                            if ((OPN.eg_cnt & ((1 << SLOT[c, i].eg_sh_d2r) - 1)) == 0)
                            {
                                SLOT[c, i].volume += eg_inc[SLOT[c, i].eg_sel_d2r + ((OPN.eg_cnt >> SLOT[c, i].eg_sh_d2r) & 7)];
                                if (SLOT[c, i].volume >= 0x3ff)
                                {
                                    SLOT[c, i].volume = 0x3ff;
                                    /* do not change SLOT->state (verified on real chip) */
                                }
                            }
                        }
                        break;
                    case 1:	/* release phase */
                        if ((OPN.eg_cnt & ((1 << SLOT[c, i].eg_sh_rr) - 1)) == 0)
                        {
                            SLOT[c, i].volume += eg_inc[SLOT[c, i].eg_sel_rr + ((OPN.eg_cnt >> SLOT[c, i].eg_sh_rr) & 7)];
                            if (SLOT[c, i].volume >= 0x3ff)
                            {
                                SLOT[c, i].volume = 0x3ff;
                                SLOT[c, i].state = 0;
                            }
                        }
                        break;
                }
                out1 = (uint)(SLOT[c, i].tl + ((uint)SLOT[c, i].volume));
                if (((SLOT[c, i].ssg & 0x08) != 0) && ((SLOT[c, i].ssgn & 2) != 0) && (SLOT[c, i].state != 0))	/* negate output (changes come from alternate bit, init comes from attack bit) */
                    out1 ^= 511;
                /* we need to store the result here because we are going to change ssgn
                    in next instruction */
                SLOT[c, i].vol_out = out1;
                SLOT[c, i].ssgn ^= swap_flag;
            }
        }
        private static uint volume_calc(int c, int s)
        {
            int AM = LFO_AM >> CH[c].ams;
            return (uint)(SLOT[c, s].vol_out + (AM & SLOT[c, s].AMmask));
        }
        private static void update_phase_lfo_slot(int s, int pms, uint block_fnum)
        {
            uint fnum_lfo = ((block_fnum & 0x7f0) >> 4) * 32 * 8;
            int lfo_fn_table_index_offset = lfo_pm_table[fnum_lfo + pms + LFO_PM];
            if (lfo_fn_table_index_offset != 0)    /* LFO phase modulation active */
            {
                byte blk;
                uint fn;
                int kc, fc;
                block_fnum = (uint)(block_fnum * 2 + lfo_fn_table_index_offset);
                blk = (byte)((block_fnum & 0x7000) >> 12);
                fn = block_fnum & 0xfff;
                /* keyscale code */
                kc = (blk << 2) | opn_fktable[fn >> 8];
                /* phase increment counter */
                fc = (int)((OPN.fn_table[fn] >> (7 - blk)) + dt_tab2[idt_tab[2, 0], kc]);
                /* detects frequency overflow (credits to Nemesis) */
                if (fc < 0)
                    fc += fn_max;
                /* update phase */
                SLOT[2, s].phase += (uint)((fc * SLOT[2, s].mul) >> 1);
            }
            else    /* LFO phase modulation  = zero */
            {
                SLOT[2, s].phase += (uint)SLOT[2, s].Incr;
            }
        }
        private static void update_phase_lfo_channel(int c)
        {
            uint block_fnum = CH[c].block_fnum;
            uint fnum_lfo = ((block_fnum & 0x7f0) >> 4) * 32 * 8;
            int lfo_fn_table_index_offset = lfo_pm_table[fnum_lfo + CH[c].pms + LFO_PM];
            if (lfo_fn_table_index_offset != 0)    /* LFO phase modulation active */
            {
                byte blk;
                uint fn;
                int kc, fc, finc;
                block_fnum = (uint)(block_fnum * 2 + lfo_fn_table_index_offset);
                blk = (byte)((block_fnum & 0x7000) >> 12);
                fn = block_fnum & 0xfff;
                /* keyscale code */
                kc = (blk << 2) | opn_fktable[fn >> 8];
                /* phase increment counter */
                fc = (int)(OPN.fn_table[fn] >> (7 - blk));
                /* detects frequency overflow (credits to Nemesis) */
                finc = fc + dt_tab2[idt_tab[c, 0], kc];
                if (finc < 0)
                    finc += fn_max;
                SLOT[c, 0].phase += (uint)((finc * SLOT[c, 0].mul) >> 1);
                finc = fc + dt_tab2[idt_tab[c, 2], kc];
                if (finc < 0)
                    finc += fn_max;
                SLOT[c, 2].phase += (uint)((finc * SLOT[c, 2].mul) >> 1);
                finc = fc + dt_tab2[idt_tab[c, 1], kc];
                if (finc < 0)
                    finc += fn_max;
                SLOT[c, 1].phase += (uint)((finc * SLOT[c, 1].mul) >> 1);
                finc = fc + dt_tab2[idt_tab[c, 3], kc];
                if (finc < 0)
                    finc += fn_max;
                SLOT[c, 3].phase += (uint)((finc * SLOT[c, 3].mul) >> 1);
            }
            else    /* LFO phase modulation  = zero */
            {
                SLOT[c, 0].phase += (uint)SLOT[c, 0].Incr;
                SLOT[c, 2].phase += (uint)SLOT[c, 2].Incr;
                SLOT[c, 1].phase += (uint)SLOT[c, 1].Incr;
                SLOT[c, 3].phase += (uint)SLOT[c, 3].Incr;
            }
        }
        private static void chan_calc(int c)
        {
            uint eg_out;
            out_fm[8] = out_fm[9] = out_fm[10] = out_fm[11] = 0;//m2 = c1 = c2 = mem = 0;
            set_mem(c);
            eg_out = volume_calc(c, 0);
            int out1 = CH[c].op1_out0 + CH[c].op1_out1;
            CH[c].op1_out0 = CH[c].op1_out1;
            set_value1(c);
            CH[c].op1_out1 = 0;
            if (eg_out < 832)	/* SLOT 1 */
            {
                if (CH[c].FB == 0)
                    out1 = 0;
                CH[c].op1_out1 = op_calc1(SLOT[c, 0].phase, eg_out, (out1 << CH[c].FB));
            }
            eg_out = volume_calc(c, 1);
            if (eg_out < 832)		/* SLOT 3 */
            {
                out_fm[iconnect3[c]] += op_calc(SLOT[c, 1].phase, eg_out, out_fm[8]);
            }
            eg_out = volume_calc(c, 2);
            if (eg_out < 832)		/* SLOT 2 */
            {
                out_fm[iconnect2[c]] += op_calc(SLOT[c, 2].phase, eg_out, out_fm[9]);
            }
            eg_out = volume_calc(c, 3);
            if (eg_out < 832)		/* SLOT 4 */
            {
                out_fm[iconnect4[c]] += op_calc(SLOT[c, 3].phase, eg_out, out_fm[10]);
            }
            /* store current MEM */
            CH[c].mem_value = out_fm[11];//mem;
            /* update phase counters AFTER output calculations */
            if (CH[c].pms != 0)
            {
                /* add support for 3 slot mode */
                if (((ST.mode & 0xC0) != 0) && (c == 2))
                {
                    update_phase_lfo_slot(0, CH[c].pms, SL3.block_fnum[1]);
                    update_phase_lfo_slot(2, CH[c].pms, SL3.block_fnum[2]);
                    update_phase_lfo_slot(1, CH[c].pms, SL3.block_fnum[0]);
                    update_phase_lfo_slot(3, CH[c].pms, CH[c].block_fnum);
                }
                else
                    update_phase_lfo_channel(c);
            }
            else	/* no LFO phase modulation */
            {
                SLOT[c, 0].phase += (uint)SLOT[c, 0].Incr;
                SLOT[c, 2].phase += (uint)SLOT[c, 2].Incr;
                SLOT[c, 1].phase += (uint)SLOT[c, 1].Incr;
                SLOT[c, 3].phase += (uint)SLOT[c, 3].Incr;
            }
        }
        /* update phase increment and envelope generator */
        private static void refresh_fc_eg_slot(int c, int s, int fc, int kc)
        {
            int ksr = kc >> SLOT[c, s].KSR;
            fc += dt_tab2[idt_tab[c, s], kc];
            /* detects frequency overflow (credits to Nemesis) */
            if (fc < 0) fc += fn_max;
            /* (frequency) phase increment counter */
            SLOT[c, s].Incr = (fc * SLOT[c, s].mul) >> 1;
            if (SLOT[c, s].ksr != ksr)
            {
                SLOT[c, s].ksr = (byte)ksr;
                /* calculate envelope generator rates */
                if ((SLOT[c, s].ar + SLOT[c, s].ksr) < 32 + 62)
                {
                    SLOT[c, s].eg_sh_ar = eg_rate_shift[SLOT[c, s].ar + SLOT[c, s].ksr];
                    SLOT[c, s].eg_sel_ar = eg_rate_select[SLOT[c, s].ar + SLOT[c, s].ksr];
                }
                else
                {
                    SLOT[c, s].eg_sh_ar = 0;
                    SLOT[c, s].eg_sel_ar = 17 * 8;
                }
                SLOT[c, s].eg_sh_d1r = eg_rate_shift[SLOT[c, s].d1r + SLOT[c, s].ksr];
                SLOT[c, s].eg_sh_d2r = eg_rate_shift[SLOT[c, s].d2r + SLOT[c, s].ksr];
                SLOT[c, s].eg_sh_rr = eg_rate_shift[SLOT[c, s].rr + SLOT[c, s].ksr];
                SLOT[c, s].eg_sel_d1r = eg_rate_select[SLOT[c, s].d1r + SLOT[c, s].ksr];
                SLOT[c, s].eg_sel_d2r = eg_rate_select[SLOT[c, s].d2r + SLOT[c, s].ksr];
                SLOT[c, s].eg_sel_rr = eg_rate_select[SLOT[c, s].rr + SLOT[c, s].ksr];
            }
        }
        /* update phase increment counters */
        /* Changed from INLINE to static to work around gcc 4.2.1 codegen bug */
        private static void refresh_fc_eg_chan(int c)
        {
            if (SLOT[c, 0].Incr == -1)
            {
                int fc = (int)CH[c].fc;
                int kc = CH[c].kcode;
                refresh_fc_eg_slot(c, 0, fc, kc);
                refresh_fc_eg_slot(c, 2, fc, kc);
                refresh_fc_eg_slot(c, 1, fc, kc);
                refresh_fc_eg_slot(c, 3, fc, kc);
            }
        }
        /* initialize time tables */
        private static void init_timetables()
        {
            int i, d;
            double rate;
            /* DeTune table */
            for (d = 0; d <= 3; d++)
            {
                for (i = 0; i <= 31; i++)
                {
                    rate = ((double)dt_tab[d * 32 + i]) * 1024 * ST.freqbase * (1 << 16) / ((double)(1 << 20));
                    dt_tab2[d, i] = (int)rate;
                    dt_tab2[d + 4, i] = -dt_tab2[d, i];
                }
            }
        }
        private static void reset_channels()
        {
            int c, s;
            ST.mode = 0;	/* normal mode */
            ST.TA = 0;
            ST.TAC = 0;
            ST.TB = 0;
            ST.TBC = 0;
            for (c = 0; c < 6; c++)
            {
                CH[c].fc = 0;
                for (s = 0; s < 4; s++)
                {
                    SLOT[c, s].ssg = 0;
                    SLOT[c, s].ssgn = 0;
                    SLOT[c, s].state = 0;
                    SLOT[c, s].volume = 0x3ff;
                    SLOT[c, s].vol_out = 0x3ff;
                }
            }
        }
        /* initialize generic tables */
        private static int init_tables()
        {
            int i, x;
            int n;
            double o, m;
            for (x = 0; x < 256; x++)
            {
                m = (1 << 16) / Math.Pow(2, (x + 1) * (1.0 / 32) / 8.0);
                m = Math.Floor(m);
                /* we never reach (1<<16) here due to the (x+1) */
                /* result fits within 16 bits at maximum */
                n = (int)m;		/* 16 bits here */
                n >>= 4;		/* 12 bits here */
                if ((n & 1) != 0)		/* round to nearest */
                    n = (n >> 1) + 1;
                else
                    n = n >> 1;
                /* 11 bits here (rounded) */
                n <<= 2;		/* 13 bits here (as in real chip) */
                tl_tab[x * 2 + 0] = n;
                tl_tab[x * 2 + 1] = -tl_tab[x * 2 + 0];
                for (i = 1; i < 13; i++)
                {
                    tl_tab[x * 2 + 0 + i * 2 * 256] = tl_tab[x * 2 + 0] >> i;
                    tl_tab[x * 2 + 1 + i * 2 * 256] = -tl_tab[x * 2 + 0 + i * 2 * 256];
                }
            }
            for (i = 0; i < 1024; i++)
            {
                /* non-standard sinus */
                m = Math.Sin(((i * 2) + 1) * Math.PI / 1024); /* checked against the real chip */
                /* we never reach zero here due to ((i*2)+1) */
                if (m > 0.0)
                    o = 8 * Math.Log(1.0 / m) / Math.Log(2);	/* convert to 'decibels' */
                else
                    o = 8 * Math.Log(-1.0 / m) / Math.Log(2);	/* convert to 'decibels' */
                o = o / (1.0 / 32);
                n = (int)(2.0 * o);
                if ((n & 1) != 0)						/* round to nearest */
                    n = (n >> 1) + 1;
                else
                    n = n >> 1;
                sin_tab[i] = n * 2 + (m >= 0.0 ? 0 : 1);
            }
            /* build LFO PM modulation table */
            for (i = 0; i < 8; i++) /* 8 PM depths */
            {
                byte fnum;
                for (fnum = 0; fnum < 128; fnum++) /* 7 bits meaningful of F-NUMBER */
                {
                    byte value;
                    byte step;
                    int offset_depth = i;
                    int offset_fnum_bit;
                    int bit_tmp;
                    for (step = 0; step < 8; step++)
                    {
                        value = 0;
                        for (bit_tmp = 0; bit_tmp < 7; bit_tmp++) /* 7 bits */
                        {
                            if ((fnum & (1 << bit_tmp)) != 0) /* only if bit "bit_tmp" is set */
                            {
                                offset_fnum_bit = bit_tmp * 8;
                                value += lfo_pm_output[offset_fnum_bit + offset_depth, step];
                            }
                        }
                        lfo_pm_table[(fnum * 32 * 8) + (i * 32) + step + 0] = value;
                        lfo_pm_table[(fnum * 32 * 8) + (i * 32) + (step ^ 7) + 8] = value;
                        lfo_pm_table[(fnum * 32 * 8) + (i * 32) + step + 16] = -value;
                        lfo_pm_table[(fnum * 32 * 8) + (i * 32) + (step ^ 7) + 24] = -value;
                    }
                }
            }
            return 1;
        }
        /* CSM Key Controll */
        private static void CSMKeyControll()
        {
            /* this is wrong, atm */

            /* all key on */
            FM_KEYON(2, 0);
            FM_KEYON(2, 2);
            FM_KEYON(2, 1);
            FM_KEYON(2, 3);
        }
        /* prescaler set (and make time tables) */
        private static void OPNSetPres(int pres, int timer_prescaler)
        {
            int i;
            /* frequency base */
            ST.freqbase = ((double)8000000 / 111111) / pres;
            OPN.eg_timer_add = (uint)((1 << 16) * ST.freqbase);
            OPN.eg_timer_overflow = (3) * (1 << 16);
            /* Timer base time */
            ST.timer_prescaler = timer_prescaler;
            /* make time tables */
            init_timetables();
            /* there are 2048 FNUMs that can be generated using FNUM/BLK registers
                but LFO works with one more bit of a precision so we really need 4096 elements */
            /* calculate fnumber -> increment counter table */
            for (i = 0; i < 4096; i++)
            {
                /* freq table for octave 7 */
                /* OPN phase increment counter = 20bit */
                OPN.fn_table[i] = (uint)((double)i * 32 * ST.freqbase * (1 << (16 - 10))); /* -10 because chip works with 10.10 fixed point, while we use 16.16 */
            }

            /* maximal frequency, used for overflow, best setting with BLOCK=5 (notaz) */
            fn_max = 2096127;//((UINT32)((double)OPN.fn_table[0x7ff*2] / ST.freqbase) >> 2);

            /* LFO freq. table */
            for (i = 0; i < 8; i++)
            {
                /* Amplitude modulation: 64 output levels (triangle waveform); 1 level lasts for one of "lfo_samples_per_step" samples */
                /* Phase modulation: one entry from lfo_pm_output lasts for one of 4 * "lfo_samples_per_step" samples  */
                OPN.lfo_freq[i] = (int)((1.0 / lfo_samples_per_step[i]) * (1 << 24) * ST.freqbase);
            }
        }
        /* write a OPN mode register 0x20-0x2f */
        private static void OPNWriteMode(int r, byte v)
        {
            byte c;
            switch (r)
            {
                case 0x21:	/* Test */
                    break;
                case 0x22:	/* LFO FREQ (YM2608/YM2610/YM2610B/YM2612) */
                    if ((v & 0x08) != 0) /* LFO enabled ? */
                    {
                        OPN.lfo_inc = OPN.lfo_freq[v & 7];
                    }
                    else
                    {
                        OPN.lfo_inc = 0;
                    }
                    break;
                case 0x24:	/* timer A High 8*/
                    ST.TA = (ST.TA & 0x03) | (((int)v) << 2);
                    break;
                case 0x25:	/* timer A Low 2*/
                    ST.TA = (ST.TA & 0x3fc) | (v & 3);
                    break;
                case 0x26:	/* timer B */
                    ST.TB = v;
                    break;
                case 0x27:	/* mode, timer control */
                    set_timers(v);
                    break;
                case 0x28:	/* key on / off */
                    c = (byte)(v & 0x03);
                    if (c == 3)
                        break;
                    if ((v & 0x04) != 0)
                        c += 3;
                    if ((v & 0x10) != 0)
                        FM_KEYON(c, 0);
                    else
                        FM_KEYOFF(c, 0);
                    if ((v & 0x20) != 0)
                        FM_KEYON(c, 2);
                    else
                        FM_KEYOFF(c, 2);
                    if ((v & 0x40) != 0)
                        FM_KEYON(c, 1);
                    else
                        FM_KEYOFF(c, 1);
                    if ((v & 0x80) != 0)
                        FM_KEYON(c, 3);
                    else
                        FM_KEYOFF(c, 3);
                    break;
            }
        }
        /* write a OPN register (0x30-0xff) */
        private static void OPNWriteReg(int r, byte v)
        {
            byte c = (byte)(r & 3);
            int s = (r >> 2) & 3;
            if (c == 3)
                return; /* 0xX3,0xX7,0xXB,0xXF */
            if (r >= 0x100)
                c += 3;
            switch (r & 0xf0)
            {
                case 0x30:	/* DET , MUL */
                    set_det_mul(c, s, v);
                    break;
                case 0x40:	/* TL */
                    set_tl(c, s, v);
                    break;
                case 0x50:	/* KS, AR */
                    set_ar_ksr(c, s, v);
                    break;
                case 0x60:	/* bit7 = AM ENABLE, DR */
                    set_dr(c, s, v);
                    SLOT[c, s].AMmask = ((v & 0x80) != 0) ? 0xffffffff : 0;
                    break;
                case 0x70:	/*     SR */
                    set_sr(c, s, v);
                    break;
                case 0x80:	/* SL, RR */
                    set_sl_rr(c, s, v);
                    break;
                case 0x90:	/* SSG-EG */
                    SLOT[c, s].ssg = (byte)(v & 0x0f);
                    SLOT[c, s].ssgn = (byte)((v & 0x04) >> 1); /* bit 1 in ssgn = attack */
                    break;
                case 0xa0:
                    switch ((r >> 2) & 3)
                    {
                        case 0:		/* 0xa0-0xa2 : FNUM1 */
                            {
                                uint fn = (((uint)((ST.fn_h) & 7)) << 8) + v;
                                byte blk = (byte)(ST.fn_h >> 3);
                                /* keyscale code */
                                CH[c].kcode = (byte)((blk << 2) | opn_fktable[fn >> 7]);
                                /* phase increment counter */
                                CH[c].fc = OPN.fn_table[fn * 2] >> (7 - blk);
                                /* store fnum in clear form for LFO PM calculations */
                                CH[c].block_fnum = (uint)(blk << 11) | fn;
                                SLOT[c, 0].Incr = -1;
                            }
                            break;
                        case 1:		/* 0xa4-0xa6 : FNUM2,BLK */
                            ST.fn_h = (byte)(v & 0x3f);
                            break;
                        case 2:		/* 0xa8-0xaa : 3CH FNUM1 */
                            if (r < 0x100)
                            {
                                uint fn = (((uint)(SL3.fn_h & 7)) << 8) + v;
                                byte blk = (byte)(SL3.fn_h >> 3);
                                /* keyscale code */
                                SL3.kcode[c] = (byte)((blk << 2) | opn_fktable[fn >> 7]);
                                /* phase increment counter */
                                SL3.fc[c] = OPN.fn_table[fn * 2] >> (7 - blk);
                                SL3.block_fnum[c] = (uint)(blk << 11) | fn;
                                SLOT[c + 2, 0].Incr = -1;
                            }
                            break;
                        case 3:		/* 0xac-0xae : 3CH FNUM2,BLK */
                            if (r < 0x100)
                                SL3.fn_h = (byte)(v & 0x3f);
                            break;
                    }
                    break;
                case 0xb0:
                    switch ((r >> 2) & 3)
                    {
                        case 0:		/* 0xb0-0xb2 : FB,ALGO */
                            {
                                int feedback = (v >> 3) & 7;
                                CH[c].ALGO = (byte)(v & 7);
                                CH[c].FB = (feedback != 0) ? (byte)(feedback + 6) : (byte)0;
                                setup_connection(c);
                            }
                            break;
                        case 1:		/* 0xb4-0xb6 : L , R , AMS , PMS (YM2612/YM2610B/YM2610/YM2608) */
                            /* b0-2 PMS */
                            CH[c].pms = (v & 7) * 32; /* CH->pms = PM depth * 32 (index in lfo_pm_table) */

                            /* b4-5 AMS */
                            CH[c].ams = lfo_ams_depth_shift[(v >> 4) & 0x03];

                            /* PAN :  b7 = L, b6 = R */
                            OPN.pan[c * 2] = ((v & 0x80) != 0) ? 0xffffffff : 0;
                            OPN.pan[c * 2 + 1] = ((v & 0x40) != 0) ? 0xffffffff : 0;
                            break;
                    }
                    break;
            }
        }
        private static void Init_ADPCMATable()
        {
            int step, nib;
            for (step = 0; step < 49; step++)
            {
                /* loop over all nibbles and compute the difference */
                for (nib = 0; nib < 16; nib++)
                {
                    int value = (2 * (nib & 0x07) + 1) * steps[step] / 8;
                    jedi_table[step * 16 + nib] = ((nib & 0x08) != 0) ? -value : value;
                }
            }
        }
        /* ADPCM A (Non control type) : calculate one channel output */
        private static void ADPCMA_calc_chan(int c)
        {
            uint step;
            byte data;
            int i;
            adpcm[c].now_step += adpcm[c].step;
            if (adpcm[c].now_step >= (1 << 16))
            {
                step = adpcm[c].now_step >> 16;
                adpcm[c].now_step &= (1 << 16) - 1;
                for (i = 0; i < step; i++)
                {
                    /* end check */
                    /* 11-06-2001 JB: corrected comparison. Was > instead of == */
                    /* YM2610 checks lower 20 bits only, the 4 MSB bits are sample bank */
                    /* Here we use 1<<21 to compensate for nibble calculations */
                    if ((adpcm[c].now_addr & ((1 << 21) - 1)) == ((adpcm[c].end << 1) & ((1 << 21) - 1)))
                    {
                        adpcm[c].flag = 0;
                        F2610.adpcm_arrivedEndAddress |= adpcm[c].flagMask;
                        return;
                    }
                    if ((adpcm[c].now_addr & 1) != 0)
                        data = (byte)(adpcm[c].now_data & 0x0f);
                    else
                    {
                        adpcm[c].now_data = ymsndrom[adpcm[c].now_addr >> 1];
                        data = (byte)((adpcm[c].now_data >> 4) & 0x0f);
                    }
                    adpcm[c].now_addr++;
                    adpcm[c].adpcm_acc += jedi_table[adpcm[c].adpcm_step + data];
                    /* extend 12-bit signed int */
                    if ((adpcm[c].adpcm_acc & ~0x7ff) != 0)
                        adpcm[c].adpcm_acc |= ~0xfff;
                    else
                        adpcm[c].adpcm_acc &= 0xfff;
                    adpcm[c].adpcm_step += step_inc[data & 7];
                    adpcm[c].adpcm_step = Limit(adpcm[c].adpcm_step, 48 * 16, 0);
                }
                /* calc pcm * volume data */
                adpcm[c].adpcm_out = ((adpcm[c].adpcm_acc * adpcm[c].vol_mul) >> adpcm[c].vol_shift) & ~3;	/* multiply, shift and mask out 2 LSB bits */
            }
            /* output for work of output channels (out_adpcm[OPNxxxx])*/
            out_adpcm[ipan[c]] += adpcm[c].adpcm_out;
        }
        /* ADPCM type A Write */
        private static void FM_ADPCMAWrite(int r, byte v)
        {
            byte c = (byte)(r & 0x07);
            F2610.adpcmreg[r] = (byte)(v & 0xff); /* stock data */
            switch (r)
            {
                case 0x00: /* DM,--,C5,C4,C3,C2,C1,C0 */
                    if ((v & 0x80) == 0)
                    {
                        /* KEY ON */
                        for (c = 0; c < 6; c++)
                        {
                            if (((v >> c) & 1) != 0)
                            {
                                /**** start adpcm ****/
                                adpcm[c].step = (uint)((float)(1 << 16) * ((float)ST.freqbase) / 3.0);
                                adpcm[c].now_addr = adpcm[c].start << 1;
                                adpcm[c].now_step = 0;
                                adpcm[c].adpcm_acc = 0;
                                adpcm[c].adpcm_step = 0;
                                adpcm[c].adpcm_out = 0;
                                adpcm[c].flag = 1;
                            }
                        }
                    }
                    else
                    {
                        /* KEY OFF */
                        for (c = 0; c < 6; c++)
                            if (((v >> c) & 1) != 0)
                                adpcm[c].flag = 0;
                    }
                    break;
                case 0x01:	/* B0-5 = TL */
                    F2610.adpcmTL = (byte)((v & 0x3f) ^ 0x3f);
                    for (c = 0; c < 6; c++)
                    {
                        int volume = F2610.adpcmTL + adpcm[c].IL;
                        if (volume >= 63)	/* This is correct, 63 = quiet */
                        {
                            adpcm[c].vol_mul = 0;
                            adpcm[c].vol_shift = 0;
                        }
                        else
                        {
                            adpcm[c].vol_mul = (sbyte)(15 - (volume & 7));		/* so called 0.75 dB */
                            adpcm[c].vol_shift = (byte)(1 + (volume >> 3));	/* Yamaha engineers used the approximation: each -6 dB is close to divide by two (shift right) */
                        }
                        /* calc pcm * volume data */
                        adpcm[c].adpcm_out = ((adpcm[c].adpcm_acc * adpcm[c].vol_mul) >> adpcm[c].vol_shift) & ~3;	/* multiply, shift and mask out low 2 bits */
                    }
                    break;
                default:
                    c = (byte)(r & 0x07);
                    if (c >= 0x06)
                        return;
                    switch (r & 0x38)
                    {
                        case 0x08:	/* B7=L,B6=R, B4-0=IL */
                            {
                                int volume;
                                adpcm[c].IL = (byte)((v & 0x1f) ^ 0x1f);
                                volume = F2610.adpcmTL + adpcm[c].IL;
                                if (volume >= 63)	/* This is correct, 63 = quiet */
                                {
                                    adpcm[c].vol_mul = 0;
                                    adpcm[c].vol_shift = 0;
                                }
                                else
                                {
                                    adpcm[c].vol_mul = (sbyte)(15 - (volume & 7));		/* so called 0.75 dB */
                                    adpcm[c].vol_shift = (byte)(1 + (volume >> 3));	/* Yamaha engineers used the approximation: each -6 dB is close to divide by two (shift right) */
                                }
                                ipan[c] = (v >> 6) & 0x03;
                                /* calc pcm * volume data */
                                adpcm[c].adpcm_out = ((adpcm[c].adpcm_acc * adpcm[c].vol_mul) >> adpcm[c].vol_shift) & ~3;	/* multiply, shift and mask out low 2 bits */
                            }
                            break;
                        case 0x10:
                        case 0x18:
                            adpcm[c].start = (uint)((F2610.adpcmreg[0x18 + c] * 0x0100 | F2610.adpcmreg[0x10 + c]) << 8);
                            break;
                        case 0x20:
                        case 0x28:
                            adpcm[c].end = (uint)((F2610.adpcmreg[0x28 + c] * 0x0100 | F2610.adpcmreg[0x20 + c]) << 8);
                            adpcm[c].end += (1 << 8) - 1;
                            break;
                    }
                    break;
            }
        }
        /* Generate samples for one of the YM2610s */
        public static void ym2610_update_one(int offset, int length)
        {
            int i, j;
            if (offset == 0x25e2)
            {
                int i1 = 1;
            }
            //StreamWriter sw1 = new StreamWriter(@"E:\VS2008\compare1\compare1\bin\Debug\f2.txt",true, Encoding.GetEncoding("GB2312"));
            //sw1.WriteLine(offset.ToString("X") + "\t" + length.ToString("X"));
            //sw1.Close();

            /* refresh PG and EG */
            refresh_fc_eg_chan(1);
            if ((ST.mode & 0xc0)!=0)
            {
                /* 3SLOT MODE */
                if (SLOT[2,0].Incr == -1)
                {
                    refresh_fc_eg_slot(2, 0, (int)SL3.fc[1], SL3.kcode[1]);
                    refresh_fc_eg_slot(2, 2, (int)SL3.fc[2], SL3.kcode[2]);
                    refresh_fc_eg_slot(2, 1, (int)SL3.fc[0], SL3.kcode[0]);
                    refresh_fc_eg_slot(2, 3, (int)CH[2].fc, CH[2].kcode);
                }
            }
            else
            {
                refresh_fc_eg_chan(2);
            }
            refresh_fc_eg_chan(4);
            refresh_fc_eg_chan(5);
            /* buffering */
            for (i = 0; i < length; i++)
            {
                advance_lfo();
                /* clear output acc. */
                out_adpcm[2] = out_adpcm[1] = out_adpcm[3] = 0;
                out_delta[2] = out_delta[1] = out_delta[3] = 0;
                /* clear outputs */
                out_fm[1] = 0;
                out_fm[2] = 0;
                out_fm[4] = 0;
                out_fm[5] = 0;
                /* advance envelope generator */
                OPN.eg_timer += OPN.eg_timer_add;
                while (OPN.eg_timer >= OPN.eg_timer_overflow)
                {
                    OPN.eg_timer -= OPN.eg_timer_overflow;
                    OPN.eg_cnt++;
                    advance_eg_channel(1);
                    advance_eg_channel(2);
                    advance_eg_channel(4);
                    advance_eg_channel(5);
                }
                /* calculate FM */
                chan_calc(1);	/*remapped to 1*/
                chan_calc(2);	/*remapped to 2*/
                chan_calc(4);	/*remapped to 4*/
                chan_calc(5);	/*remapped to 5*/
                if ((YMDeltat.DELTAT.portstate & 0x80)!=0)
                    YMDeltat.YM_DELTAT_ADPCM_CALC();
                /* ADPCMA */
                for (j = 0; j < 6; j++)
                {
                    if (adpcm[j].flag != 0)
                        ADPCMA_calc_chan(j);
                }
                /* buffering */
                int lt, rt;
                lt = out_adpcm[2] + out_adpcm[3];
                rt = out_adpcm[1] + out_adpcm[3];
                lt += (out_delta[2] + out_delta[3]) >> 9;
                rt += (out_delta[1] + out_delta[3]) >> 9;
                lt += (int)((out_fm[1] >> 1) & OPN.pan[2]);	/* the shift right was verified on real chip */
                rt += (int)((out_fm[1] >> 1) & OPN.pan[3]);
                lt += (int)((out_fm[2] >> 1) & OPN.pan[4]);
                rt += (int)((out_fm[2] >> 1) & OPN.pan[5]);
                lt += (int)((out_fm[4] >> 1) & OPN.pan[8]);
                rt += (int)((out_fm[4] >> 1) & OPN.pan[9]);
                lt += (int)((out_fm[5] >> 1) & OPN.pan[10]);
                rt += (int)((out_fm[5] >> 1) & OPN.pan[11]);
                lt = Limit(lt, 32767, -32768);
                rt = Limit(rt, 32767, -32768);
                /* buffering */
                //Sound.streamoutputY60[offset + i] = lt;
                //Sound.streamoutputY61[offset + i] = rt;
                Sound.ym2610stream.streamoutput[0][offset + i] = lt;
                Sound.ym2610stream.streamoutput[1][offset + i] = rt;
            }
        }
        public static void ym2610_postload()
        {
            byte r;
            /* SSG registers */
            for (r = 0; r < 16; r++)
            {
                AY8910.ay8910_write_ym(0, r);
                AY8910.ay8910_write_ym(1, F2610.REGS[r]);
            }
            /* OPN registers */
            /* DT / MULTI , TL , KS / AR , AMON / DR , SR , SL / RR , SSG-EG */
            for (r = 0x30; r < 0x9e; r++)
                if ((r & 3) != 3)
                {
                    OPNWriteReg(r, F2610.REGS[r]);
                    OPNWriteReg(r | 0x100, F2610.REGS[r | 0x100]);
                }
            /* FB / CONNECT , L / R / AMS / PMS */
            for (r = 0xb0; r < 0xb6; r++)
                if ((r & 3) != 3)
                {
                    OPNWriteReg(r, F2610.REGS[r]);
                    OPNWriteReg(r | 0x100, F2610.REGS[r | 0x100]);
                }
            /* FM channels */
            /*FM_channel_postload(F2610->CH,6);*/

            /* rhythm(ADPCMA) */
            FM_ADPCMAWrite(1, F2610.REGS[0x101]);
            for (r = 0; r < 6; r++)
            {
                FM_ADPCMAWrite(r + 0x08, F2610.REGS[r + 0x108]);
                FM_ADPCMAWrite(r + 0x10, F2610.REGS[r + 0x110]);
                FM_ADPCMAWrite(r + 0x18, F2610.REGS[r + 0x118]);
                FM_ADPCMAWrite(r + 0x20, F2610.REGS[r + 0x120]);
                FM_ADPCMAWrite(r + 0x28, F2610.REGS[r + 0x128]);
            }
            /* Delta-T ADPCM unit */
            YMDeltat.YM_DELTAT_postload(F2610.REGS, 0x010);
        }
        private static void YM2610_deltat_status_set(byte changebits)
        {
            F2610.adpcm_arrivedEndAddress |= changebits;
        }
        private static void YM2610_deltat_status_reset(byte changebits)
        {
            F2610.adpcm_arrivedEndAddress &= (byte)(~changebits);
        }
        public static void ym2610_init()
        {
            FM_init();
            /* allocate total level table (128kb space) */
            init_tables();
            YMDeltat.DELTAT.reg = new byte[16];
            if (YMDeltat.ymsnddeltatrom == null)
            {
                YMDeltat.ymsnddeltatrom = ymsndrom;
            }
            YMDeltat.DELTAT.memory_size = YMDeltat.ymsnddeltatrom.Length;
            YMDeltat.DELTAT.status_set_handler = YM2610_deltat_status_set;
            YMDeltat.DELTAT.status_reset_handler = YM2610_deltat_status_reset;
            YMDeltat.DELTAT.status_change_EOS_bit = 0x80;	
            ym2610_reset_chip();
            Init_ADPCMATable();
        }
        /* reset one of chip */
        public static void ym2610_reset_chip()
        {
            int i;
            /* Reset Prescaler */
            OPNSetPres(6 * 24, 6 * 24); /* OPN 1/6 , SSG 1/4 */
            /* reset SSG section */
            AY8910.ay8910_reset_ym();
            /* status clear */
            FM_IRQMASK_SET(0x03);
            ST.busy_expiry_time = Attotime.ATTOTIME_ZERO;
            OPNWriteMode(0x27, 0x30); /* mode 0 , timer reset */
            OPN.eg_timer = 0;
            OPN.eg_cnt = 0;
            FM_STATUS_RESET(0xff);
            reset_channels();
            /* reset OPerator paramater */
            for (i = 0xb6; i >= 0xb4; i--)
            {
                OPNWriteReg(i, 0xc0);
                OPNWriteReg(i | 0x100, 0xc0);
            }
            for (i = 0xb2; i >= 0x30; i--)
            {
                OPNWriteReg(i, 0);
                OPNWriteReg(i | 0x100, 0);
            }
            for (i = 0x26; i >= 0x20; i--)
                OPNWriteReg(i, 0);
            /**** ADPCM work initial ****/
            for (i = 0; i < 6; i++)
            {
                adpcm[i].step = (uint)((float)(1 << 16) * ((float)ST.freqbase) / 3.0);
                adpcm[i].now_addr = 0;
                adpcm[i].now_step = 0;
                adpcm[i].start = 0;
                adpcm[i].end = 0;
                adpcm[i].vol_mul = 0;
                ipan[i] = 3;
                adpcm[i].flagMask = (byte)(1 << i);
                adpcm[i].flag = 0;
                adpcm[i].adpcm_acc = 0;
                adpcm[i].adpcm_step = 0;
                adpcm[i].adpcm_out = 0;
            }
            F2610.adpcmTL = 0x3f;
            F2610.adpcm_arrivedEndAddress = 0;
            YMDeltat.DELTAT.freqbase = ST.freqbase;
            YMDeltat.DELTAT.output_pointer = 0;
            YMDeltat.DELTAT.portshift = 8;		/* allways 8bits shift */
            YMDeltat.DELTAT.output_range = 1 << 23;
            YMDeltat.YM_DELTAT_ADPCM_Reset(3, 1);
        }
        /* YM2610 write */
        /* n = number  */
        /* a = address */
        /* v = value   */
        public static void ym2610_write(int a, byte v)
        {
            int addr;
            int ch;
            v &= 0xff;	/* adjust to 8 bit bus */
            switch (a & 3)
            {
                case 0:	/* address port 0 */
                    ST.address = v;
                    F2610.addr_A1 = 0;
                    /* Write register to SSG emulator */
                    if (v < 16)
                    {
                        AY8910.ay8910_write_ym(0, v);
                    }
                    break;

                case 1:	/* data port 0    */
                    if (F2610.addr_A1 != 0)
                        break;	/* verified on real YM2608 */
                    addr = ST.address;
                    F2610.REGS[addr] = v;
                    switch (addr & 0xf0)
                    {
                        case 0x00:	/* SSG section */
                            /* Write data to SSG emulator */
                            AY8910.ay8910_write_ym(a, v);
                            break;
                        case 0x10: /* DeltaT ADPCM */
                            YM2610.ym2610_update_request();
                            switch (addr)
                            {
                                case 0x10:	/* control 1 */
                                case 0x11:	/* control 2 */
                                case 0x12:	/* start address L */
                                case 0x13:	/* start address H */
                                case 0x14:	/* stop address L */
                                case 0x15:	/* stop address H */
                                case 0x19:	/* delta-n L */
                                case 0x1a:	/* delta-n H */
                                case 0x1b:	/* volume */
                                    YMDeltat.YM_DELTAT_ADPCM_Write(addr - 0x10, v);
                                    break;
                                case 0x1c: /*  FLAG CONTROL : Extend Status Clear/Mask */
                                    {
                                        byte statusmask = (byte)~v;
                                        /* set arrived flag mask */
                                        for (ch = 0; ch < 6; ch++)
                                            adpcm[ch].flagMask = (byte)(statusmask & (1 << ch));
                                        /* clear arrived flag */
                                        F2610.adpcm_arrivedEndAddress &= statusmask;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 0x20:	/* Mode Register */
                            YM2610.ym2610_update_request();
                            OPNWriteMode(addr, v);
                            break;
                        default:	/* OPN section */
                            YM2610.ym2610_update_request();
                            /* write register */
                            OPNWriteReg(addr, v);
                            break;
                    }
                    break;
                case 2:	/* address port 1 */
                    ST.address = v;
                    F2610.addr_A1 = 1;
                    break;
                case 3:	/* data port 1    */
                    if (F2610.addr_A1 != 1)
                        break;	/* verified on real YM2608 */
                    YM2610.ym2610_update_request();
                    addr = ST.address;
                    F2610.REGS[addr | 0x100] = v;
                    if (addr < 0x30)
                        /* 100-12f : ADPCM A section */
                        FM_ADPCMAWrite(addr, v);
                    else
                        OPNWriteReg(addr | 0x100, v);
                    break;
            }
        }
        public static byte ym2610_read(int a)
        {
            int addr = ST.address;
            byte ret = 0;
            switch (a & 3)
            {
                case 0:	/* status 0 : YM2203 compatible */
                    ret = (byte)(FM_STATUS_FLAG() & 0x83);
                    break;
                case 1:	/* data 0 */
                    if (addr < 16)
                    {
                        ret = AY8910.ay8910_read_ym();
                    }
                    if (addr == 0xff) ret = 0x01;
                    break;
                case 2:	/* status 1 : ADPCM status */
                    /* ADPCM STATUS (arrived End Address) */
                    /* B,--,A5,A4,A3,A2,A1,A0 */
                    /* B     = ADPCM-B(DELTA-T) arrived end address */
                    /* A0-A5 = ADPCM-A          arrived end address */
                    ret = F2610.adpcm_arrivedEndAddress;
                    break;
                case 3:
                    ret = 0;
                    break;
            }
            return ret;
        }
        public static int ym2610_timer_over(int c)
        {
            if (c!=0)
            {	/* Timer B */
                TimerBOver();
            }
            else
            {	/* Timer A */
                YM2610.ym2610_update_request();
                /* timer update */
                TimerAOver();
                /* CSM mode key,TL controll */
                if ((ST.mode & 0x80)!=0)
                {	/* CSM mode total level latch and auto key on */
                    CSMKeyControll();
                }
            }
            return ST.irq;
        }
    }
}
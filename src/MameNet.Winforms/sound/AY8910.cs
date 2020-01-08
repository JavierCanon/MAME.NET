using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public class AY8910
    {
        public struct ay8910_context
        {
            public int register_latch;
            public byte[] regs;
            public int[] count;
            public byte[] output;
            public byte output_noise;
            public int count_noise;
            public int count_env;
            public sbyte env_step;
            public int env_volume;
            public byte hold, alternate, attack, holding;
            public int rng;
            public byte[] vol_enabled;
            public int[] vol3d_table;
        }
        public static ay8910_context ay8910info;
        private static double[] ym2149_param_res, ym2149_param_env_res;
        private static int NOISE_ENABLEQ(int chan)
        {
            return (ay8910info.regs[7] >> (3 + chan)) & 1;
        }
        private static int TONE_ENABLEQ(int chan)
        {
            return (ay8910info.regs[7] >> chan) & 1;
        }
        private static int TONE_PERIOD(int chan)
        {
            return ay8910info.regs[chan << 1] | ((ay8910info.regs[(chan << 1) | 1] & 0x0f) << 8);
        }
        private static int NOISE_PERIOD()
        {
            return ay8910info.regs[6] & 0x1f;
        }
        private static int TONE_VOLUME(int chan)
        {
            return ay8910info.regs[8 + chan] & 0x0f;
        }
        private static int TONE_ENVELOPE(int chan)
        {
            return (ay8910info.regs[8 + chan] >> 4) & 1;
        }
        private static int ENVELOPE_PERIOD()
        {
            return ay8910info.regs[11] | (ay8910info.regs[12] << 8);
        }
        public static void ay8910_start_ym()
        {
            ym2149_param_res = new double[]
            { 73770, 37586, 27458, 21451, 15864, 12371, 8922,  6796,
               4763,  3521,  2403,  1737,  1123,   762,  438,   251,
                  0,     0,     0,     0,     0,     0,    0,     0,
                  0,     0,     0,     0,     0,     0,    0,     0,};
            ym2149_param_env_res=new double[]
            { 103350, 73770, 52657, 37586, 32125, 27458, 24269, 21451,
               18447, 15864, 14009, 12371, 10506,  8922,  7787,  6796,
                5689,  4763,  4095,  3521,  2909,  2403,  2043,  1737,
                1397,  1123,   925,   762,   578,   438,   332,   251 };
            ay8910info.regs = new byte[16];
            ay8910info.count = new int[3];
            ay8910info.output = new byte[3];
            ay8910info.vol_enabled = new byte[3];
            ay8910info.vol3d_table = new int[8 * 32 * 32 * 32];
            build_3D_table();
        }
        private static void build_3D_table()
        {
            int j, j1, j2, j3, e, indx;
            double rt, rw, n;
            double min = 10.0, max = 0.0;
            double[] temp = new double[8 * 32 * 32 * 32];
            for (e = 0; e < 8; e++)
                for (j1 = 0; j1 < 32; j1++)
                    for (j2 = 0; j2 < 32; j2++)
                        for (j3 = 0; j3 < 32; j3++)
                        {
                            n = 3.0;
                            rt = n / 630 + 3.0 / 801 + 1.0 / 1000.0;
                            rw = n / 630;
                            rw += 1.0 / (((e & 0x01) != 0) ? ym2149_param_env_res[j1] : ym2149_param_res[j1]);
                            rt += 1.0 / (((e & 0x01) != 0) ? ym2149_param_env_res[j1] : ym2149_param_res[j1]);
                            rw += 1.0 / (((e & 0x02) != 0) ? ym2149_param_env_res[j2] : ym2149_param_res[j2]);
                            rt += 1.0 / (((e & 0x02) != 0) ? ym2149_param_env_res[j2] : ym2149_param_res[j2]);
                            rw += 1.0 / (((e & 0x04) != 0) ? ym2149_param_env_res[j3] : ym2149_param_res[j3]);
                            rt += 1.0 / (((e & 0x04) != 0) ? ym2149_param_env_res[j3] : ym2149_param_res[j3]);
                            indx = (e << 15) | (j3 << 10) | (j2 << 5) | j1;
                            temp[indx] = rw / rt;
                            if (temp[indx] < min)
                                min = temp[indx];
                            if (temp[indx] > max)
                                max = temp[indx];
                        }

            for (j = 0; j < 32 * 32 * 32 * 8; j++)
            {
                ay8910info.vol3d_table[j] = (int)(0x7fff * (((temp[j] - min) / (max - min))) * 3.0);
            }
        }
        private static int mix_3D()
        {
            int indx = 0, chan;
            for (chan = 0; chan < 3; chan++)
            {
                if (TONE_ENVELOPE(chan)!=0)
                {
                    indx |= ((1 << (chan + 15)) | ((ay8910info.vol_enabled[chan] != 0) ? ay8910info.env_volume << (chan * 5) : 0));
                }
                else
                {
                    indx |= ((ay8910info.vol_enabled[chan]!=0) ? TONE_VOLUME(chan) << (chan * 5) : 0);
                }
            }
            return ay8910info.vol3d_table[indx];
        }
        private static void ay8910_write_reg(int r, byte v)
        {
            ay8910info.regs[r] = v;
            switch (r)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                    break;
                case 7:
                    break;
                case 13:
                    ay8910info.attack = ((ay8910info.regs[13] & 0x04) != 0) ? (byte)0x1F : (byte)0x00;
                    if ((ay8910info.regs[13] & 0x08) == 0)
                    {
                        /* if Continue = 0, map the shape to the equivalent one which has Continue = 1 */
                        ay8910info.hold = 1;
                        ay8910info.alternate = ay8910info.attack;
                    }
                    else
                    {
                        ay8910info.hold = (byte)(ay8910info.regs[13] & 0x01);
                        ay8910info.alternate = (byte)(ay8910info.regs[13] & 0x02);
                    }
                    ay8910info.env_step = 0x1F;
                    ay8910info.holding = 0;
                    ay8910info.env_volume = (ay8910info.env_step ^ ay8910info.attack);
                    break;
                case 14:
                    break;
                case 15:
                    break;
            }
        }
        public static void ay8910_update(int offset, int length)
        {
            int chan,i;
            /* buffering loop */
            for(i=0;i<length;i++)
            {
                for (chan = 0; chan < 3; chan++)
                {
                    ay8910info.count[chan]++;
                    if (ay8910info.count[chan] >= TONE_PERIOD(chan))
                    {
                        ay8910info.output[chan] ^= 1;
                        ay8910info.count[chan] = 0; ;
                    }
                }
                ay8910info.count_noise++;
                if (ay8910info.count_noise >= NOISE_PERIOD())
                {
                    if (((ay8910info.rng + 1) & 2)!=0)	/* (bit0^bit1)? */
                    {
                        ay8910info.output_noise ^= 1;
                    }
                    if ((ay8910info.rng & 1)!=0)
                        ay8910info.rng ^= 0x24000; /* This version is called the "Galois configuration". */
                    ay8910info.rng >>= 1;
                    ay8910info.count_noise = 0;
                }
                for (chan = 0; chan < 3; chan++)
                {
                    ay8910info.vol_enabled[chan] = (byte)((ay8910info.output[chan] | TONE_ENABLEQ(chan)) & (ay8910info.output_noise | NOISE_ENABLEQ(chan)));
                }
                if (ay8910info.holding == 0)
                {
                    ay8910info.count_env++;
                    if (ay8910info.count_env >= ENVELOPE_PERIOD())
                    {
                        ay8910info.count_env = 0;
                        ay8910info.env_step--;
                        if (ay8910info.env_step < 0)
                        {
                            if (ay8910info.hold!=0)
                            {
                                if (ay8910info.alternate!=0)
                                    ay8910info.attack ^= 0x1F;
                                ay8910info.holding = 1;
                                ay8910info.env_step = 0;
                            }
                            else
                            {
                                if (ay8910info.alternate!=0 && (ay8910info.env_step & 0x20)!=0)
                                    ay8910info.attack ^= 0x1F;
                                ay8910info.env_step &= 0x1F;
                            }
                        }
                    }
                }
                ay8910info.env_volume = (ay8910info.env_step ^ ay8910info.attack);
                Sound.ay8910stream.streamoutput[0][offset] = mix_3D();
                offset++;
            }
        }
        public static void ay8910_reset_ym()
        {
            int i;
            ay8910info.register_latch = 0;
            ay8910info.rng = 1;
            ay8910info.output[0] = 0;
            ay8910info.output[1] = 0;
            ay8910info.output[2] = 0;
            ay8910info.count[0] = 0;
            ay8910info.count[1] = 0;
            ay8910info.count[2] = 0;
            ay8910info.count_noise = 0;
            ay8910info.count_env = 0;
            ay8910info.output_noise = 0x01;
            for (i = 0; i < 14; i++)
                ay8910_write_reg(i, 0);
        }
        public static void ay8910_write_ym(int addr, byte data)
        {
            if ((addr & 1) != 0)
            {	/* Data port */
                int r = ay8910info.register_latch;
                if (r > 15)
                    return;
                if (r == 13 || ay8910info.regs[r] != data)
                {
                    /* update the output buffer before changing the register */
                    Sound.ay8910stream.stream_update();
                }
                ay8910_write_reg(r, data);
            }
            else
            {	/* Register port */
                ay8910info.register_latch = data & 0x0f;
            }
        }
        public static byte ay8910_read_ym()
        {
            int r = ay8910info.register_latch;
            if (r > 15)
                return 0;
            return ay8910info.regs[r];
        }
    }
}

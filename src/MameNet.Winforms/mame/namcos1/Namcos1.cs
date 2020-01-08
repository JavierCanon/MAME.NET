using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mame
{
    public partial class Namcos1
    {
        public static int dac0_value, dac1_value, dac0_gain, dac1_gain;
        public static byte[] gfx1rom, gfx2rom, gfx3rom, user1rom,mcurom;
        public static byte[] audiorom, voicerom, bank_ram20, bank_ram30;
        public static int namcos1_pri;
        public static byte dipsw;
        public static byte[] ByteTo2byte(byte[] bb1)
        {
            byte[] bb2 = null;
            int i1,n1;
            if (bb1 != null)
            {
                n1 = bb1.Length;
                bb2 = new byte[n1*2];
                for (i1 = 0; i1 < n1; i1++)
                {
                    bb2[i1 * 2] = (byte)(bb1[i1] >> 4);
                    bb2[i1 * 2 + 1] = (byte)(bb1[i1] & 0x0f);
                }
            }
            return bb2;
        }
        public static void Namcos1Init()
        {
            Machine.bRom = true;            
            user1rom_offset = new int[2, 8];
            audiorom = Machine.GetRom("audiocpu.rom");
            gfx1rom = Machine.GetRom("gfx1.rom");
            gfx2rom = Machine.GetRom("gfx2.rom");
            gfx3rom = ByteTo2byte(Machine.GetRom("gfx3.rom"));
            user1rom = Machine.GetRom("user1.rom");
            mcurom = Properties.Resources.mcu;
            voicerom = new byte[0xc0000];
            byte[] bb1 = Machine.GetRom("voice.rom");
            Array.Copy(bb1, voicerom, bb1.Length);
            bank_ram20 = new byte[0x2000];
            bank_ram30 = new byte[0x80];
            Namco.namco_wavedata = new byte[0x400];
            Generic.generic_nvram = new byte[0x800];
            cus117_offset = new int[2, 8];
            key = new byte[8];
            if (audiorom == null || gfx1rom == null || gfx2rom == null || gfx3rom == null || user1rom == null || voicerom == null)
            {
                Machine.bRom = false;
            }
            if (Machine.bRom)
            {
                switch (Machine.sName)
                {
                    case "quester":
                    case "questers":
                        dipsw = 0xfb;
                        break;
                    default:
                        dipsw = 0xff;
                        break;
                }
            }
        }
        public static void namcos1_sub_firq_w()
        {
            Cpuint.cpunum_set_input_line(1, 1, LineState.ASSERT_LINE);
        }
        public static void irq_ack_w(int cpunum)
        {
            Cpuint.cpunum_set_input_line(cpunum, 0, LineState.CLEAR_LINE);
        }
        public static void firq_ack_w(int cpunum)
        {
            Cpuint.cpunum_set_input_line(cpunum, 1, LineState.CLEAR_LINE);
        }
        public static byte dsw_r(int offset)
        {
            int ret = dipsw;// 0xff;// input_port_read(machine, "DIPSW");
            if ((offset & 2) == 0)
            {
                ret >>= 4;
            }
            return (byte)(0xf0 | ret);
        }
        public static void namcos1_coin_w(byte data)
        {
            Generic.coin_lockout_global_w(~data & 1);
            Generic.coin_counter_w(0, data & 2);
            Generic.coin_counter_w(1, data & 4);
        }
        public static void namcos1_update_DACs()
        {
            DAC.dac_signed_data_16_w(0, (ushort)(0x8000 + (dac0_value * dac0_gain) + (dac1_value * dac1_gain)));
        }
        public static void namcos1_init_DACs()
        {
            dac0_value = 0;
            dac1_value = 0;
            dac0_gain = 0x80;
            dac1_gain = 0x80;
        }
        public static void namcos1_dac_gain_w(byte data)
        {
            int value;
            value = (data & 1) | ((data >> 1) & 2);
            dac0_gain = 0x20 * (value + 1);
            value = (data >> 3) & 3;
            dac1_gain = 0x20 * (value + 1);
            namcos1_update_DACs();
        }
        public static void namcos1_dac0_w(byte data)
        {
            dac0_value = data - 0x80;
            namcos1_update_DACs();
        }
        public static void namcos1_dac1_w(byte data)
        {
            dac1_value = data - 0x80;
            namcos1_update_DACs();
        }
        public static void nvram_handler_load_namcos1()
        {
            if (File.Exists("nvram\\" + Machine.sName + ".nv"))
            {
                FileStream fs1 = new FileStream("nvram\\" + Machine.sName + ".nv", FileMode.Open);
                int n = (int)fs1.Length;
                fs1.Read(Generic.generic_nvram, 0, n);
                fs1.Close();
            }
            else
            {
                Array.Clear(Generic.generic_nvram, 0, 0x800);
            }
        }
        public static void nvram_handler_save_namcos1()
        {
            FileStream fs1 = new FileStream("nvram\\" + Machine.sName + ".nv", FileMode.Create);
            fs1.Write(Generic.generic_nvram, 0, 0x800);
            fs1.Close();
        }
    }
}

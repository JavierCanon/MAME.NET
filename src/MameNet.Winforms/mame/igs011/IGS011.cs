using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class IGS011
    {
        public static ushort[] priority_ram, paletteram16;
        public static byte prot1, prot2, prot1_swap;
        public static uint prot1_addr;
        public static ushort[] igs003_reg, vbowl_trackball;
        public static ushort priority, igs_dips_sel,igs_input_sel, lhb_irq_enable;
        public static byte igs012_prot, igs012_prot_swap;
        private static bool igs012_prot_mode;
        public static byte[] gfx1rom, gfx2rom;
        public static byte dsw1, dsw2, dsw3, dsw4, dsw5;
        public static void IGS011Init()
        {
            Machine.bRom = true;
            Generic.generic_nvram = new byte[0x4000];
            priority_ram = new ushort[0x800];
            paletteram16 = new ushort[0x1000];
            igs003_reg = new ushort[2];
            vbowl_trackball = new ushort[2];
            switch (Machine.sName)
            {
                case "drgnwrld":
                case "drgnwrldv30":
                case "drgnwrldv21":
                case "drgnwrldv21j":
                case "drgnwrldv20j":
                case "drgnwrldv10c":
                case "drgnwrldv11h":
                case "drgnwrldv40k":
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    gfx1rom = Machine.GetRom("gfx1.rom");
                    OKI6295.okirom = Machine.GetRom("oki.rom");
                    dsw1 = 0xff;
                    dsw2 = 0xff;
                    dsw3 = 0xff;
                    if (Memory.mainrom == null || gfx1rom == null || OKI6295.okirom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "lhb":
                case "lhbv33c":
                case "dbc":
                case "ryukobou":
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    gfx1rom = Machine.GetRom("gfx1.rom");
                    OKI6295.okirom = Machine.GetRom("oki.rom");
                    dsw1 = 0xf7;
                    dsw2 = 0xff;
                    dsw3 = 0xff;
                    dsw4 = 0xf0;
                    dsw5 = 0xff;
                    if (Memory.mainrom == null || gfx1rom == null || OKI6295.okirom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "lhb2":                    
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    gfx1rom = Machine.GetRom("gfx1.rom");
                    gfx2rom = Machine.GetRom("gfx2.rom");

                    break;
            }            
        }
        public static void machine_reset_igs011()
        {

        }
        private static void igs_dips_w(int offset, byte data)
        {
            if (offset % 2 == 0)
            {
                igs_dips_sel = (ushort)((data << 8) | (igs_dips_sel & 0xff));
            }
            else if (offset % 2 == 1)
            {
                igs_dips_sel = (ushort)((igs_dips_sel & 0xff00) | data);
            }
        }
        private static void igs_dips_w(ushort data)
        {
            igs_dips_sel = data;
        }
        private static byte igs_dips_r(int num)
        {
            int i;
            byte ret = 0;
            byte[] dip = new byte[] { dsw1, dsw2, dsw3, dsw4, dsw5 };
            for (i = 0; i < num; i++)
            {
                if (((~igs_dips_sel) & (1 << i)) != 0)
                {
                    ret = dip[i];
                }
            }
            return ret;
        }
        private static byte igs_3_dips_r()
        {
            return igs_dips_r(3);
        }
        private static byte igs_4_dips_r()
        {
            return igs_dips_r(4);
        }
        private static byte igs_5_dips_r()
        {
            return igs_dips_r(5);
        }
        public static void igs011_prot1_w1(int offset, byte data)
        {
            switch (offset)
            {
                case 0: // COPY ACCESSING_BITS_8_15
                    if ((data & 0xff) == 0x33)
                    {
                        prot1 = prot1_swap;
                        return;
                    }
                    break;
                case 2: // INC
                    if ((data & 0xff) == 0xff)
                    {
                        prot1++;
                        return;
                    }
                    break;
                case 4: // DEC
                    if ((data & 0xff) == 0xaa)
                    {
                        prot1--;
                        return;
                    }
                    break;
                case 6: // SWAP
                    if ((data & 0xff) == 0x55)
                    {
                        byte x = prot1;
                        prot1_swap = (byte)((BIT(x, 1) << 3) | ((BIT(x, 2) | BIT(x, 3)) << 2) | (BIT(x, 2) << 1) | (BIT(x, 0) & BIT(x, 3)));
                        return;
                    }
                    break;
            }
        }
        public static void igs011_prot1_w(int offset, ushort data)
        {
            offset *= 2;
            switch (offset)
            {
                case 0: // COPY ACCESSING_BITS_8_15
                    if ((data & 0xff00) == 0x3300)
                    {
                        prot1 = prot1_swap;
                        return;
                    }
                    break;
                case 2: // INC
                    if ((data & 0xff00) == 0xff00)
                    {
                        prot1++;
                        return;
                    }
                    break;
                case 4: // DEC
                    if ((data & 0xff00) == 0xaa00)
                    {
                        prot1--;
                        return;
                    }
                    break;
                case 6: // SWAP
                    if ((data & 0xff00) == 0x5500)
                    {
                        byte x = prot1;
                        prot1_swap = (byte)((BIT(x, 1) << 3) | ((BIT(x, 2) | BIT(x, 3)) << 2) | (BIT(x, 2) << 1) | (BIT(x, 0) & BIT(x, 3)));
                        return;
                    }
                    break;
            }
        }
        public static byte igs011_prot1_r()
        {
            byte x = prot1;
            return (byte)((((BIT(x, 1) & BIT(x, 2)) ^ 1) << 5) | ((BIT(x, 0) ^ BIT(x, 3)) << 2));
        }
        public static void igs011_prot_addr_w(ushort data)
        {
            prot1 = 0x00;
            prot1_swap = 0x00;
            prot1_addr = (uint)((data << 4) ^ 0x8340);
        }
        public static void igs011_prot2_reset_w()
        {
            prot2 = 0x00;
        }
        public static int igs011_prot2_reset_r()
        {
            prot2 = 0x00;
            return 0;
        }
        public static void igs011_prot2_inc_w()
        {
            prot2++;
        }
        public static void igs011_prot2_dec_w()
        {
            prot2--;
        }
        public static void chmplst2_interrupt()
        {
            switch (Cpuexec.iloops)
            {
                case 0:
                    Cpuint.cpunum_set_input_line(0, 6, LineState.HOLD_LINE);
                    break;
                case 1:
                default:
                    Cpuint.cpunum_set_input_line(0, 5, LineState.HOLD_LINE);
                    break;
            }
        }
        public static void drgnwrld_igs011_prot2_swap_w()
        {
            byte x = prot2;
            prot2 = (byte)(((BIT(x, 3) & BIT(x, 0)) << 4) | (BIT(x, 2) << 3) | ((BIT(x, 0) | BIT(x, 1)) << 2) | ((BIT(x, 2) ^ BIT(x, 4) ^ 1) << 1) | (BIT(x, 1) ^ 1 ^ BIT(x, 3)));
        }
        public static void lhb_igs011_prot2_swap_w(int offset)
        {
            offset *= 2;
            {
                byte x = prot2;
                prot2 = (byte)((((BIT(x, 0) ^ 1) | BIT(x, 1)) << 2) | (BIT(x, 2) << 1) | (BIT(x, 0) & BIT(x, 1)));
            }
        }
        public static void wlcc_igs011_prot2_swap_w(int offset)
        {
            offset *= 2;
            {
                byte x = prot2;
                prot2 = (byte)(((BIT(x, 3) ^ BIT(x, 2)) << 4) | ((BIT(x, 2) ^ BIT(x, 1)) << 3) | ((BIT(x, 1) ^ BIT(x, 0)) << 2) | ((BIT(x, 4) ^ BIT(x, 0) ^ 1) << 1) | (BIT(x, 4) ^ BIT(x, 3) ^ 1));
            }
        }
        private static void vbowl_igs011_prot2_swap_w(int offset)
        {
            offset *= 2;
            {
                byte x = prot2;
                prot2 = (byte)(((BIT(x, 3) ^ BIT(x, 2)) << 4) | ((BIT(x, 2) ^ BIT(x, 1)) << 3) | ((BIT(x, 1) ^ BIT(x, 0)) << 2) | ((BIT(x, 4) ^ BIT(x, 0)) << 1) | (BIT(x, 4) ^ BIT(x, 3)));
            }
        }
        private static ushort drgnwrldv21_igs011_prot2_r()
        {
            byte x = prot2;
            byte b9 = (byte)((BIT(x, 4) ^ 1) | ((BIT(x, 0) ^ 1) & BIT(x, 2)) | ((BIT(x, 3) ^ BIT(x, 1) ^ 1) & ((((BIT(x, 4) ^ 1) & BIT(x, 0)) | BIT(x, 2)) ^ 1)));
            return (ushort)(b9 << 9);
        }
        private static ushort drgnwrldv20j_igs011_prot2_r()
        {
            byte x = prot2;
            byte b9 = (byte)(((BIT(x, 4) ^ 1) | (BIT(x, 0) ^ 1)) | ((BIT(x, 3) | BIT(x, 1)) ^ 1) | ((BIT(x, 2) & BIT(x, 0)) ^ 1));
            return (ushort)(b9 << 9);
        }
        private static ushort lhb_igs011_prot2_r()
        {
            byte x = prot2;
            byte b9 = (byte)((BIT(x, 2) ^ 1) | (BIT(x, 1) & BIT(x, 0)));
            return (ushort)(b9 << 9);
        }
        private static ushort dbc_igs011_prot2_r()
        {
            byte x = prot2;
            byte b9 = (byte)((BIT(x, 1) ^ 1) | ((BIT(x, 0) ^ 1) & BIT(x, 2)));
            return (ushort)(b9 << 9);
        }
        private static ushort ryukobou_igs011_prot2_r()
        {
            byte x = prot2;
            byte b9 = (byte)(((BIT(x, 1) ^ 1) | BIT(x, 2)) & BIT(x, 0));
            return (ushort)(b9 << 9);
        }
        private static ushort lhb2_igs011_prot2_r()
        {
            byte x = prot2;
            byte b3 = (byte)((BIT(x, 2) ^ 1) | (BIT(x, 1) ^ 1) | BIT(x, 0));
            return (ushort)(b3 << 3);
        }
        private static ushort vbowl_igs011_prot2_r()
        {
            byte x = prot2;
            byte b9 = (byte)(((BIT(x, 4) ^ 1) & (BIT(x, 3) ^ 1)) | ((BIT(x, 2) & BIT(x, 1)) ^ 1) | ((BIT(x, 4) | BIT(x, 0)) ^ 1));
            return (ushort)(b9 << 9);
        }
        private static void igs012_prot_reset_w()
        {
            igs012_prot = 0x00;
            igs012_prot_swap = 0x00;
            igs012_prot_mode = false;
        }
        private static bool MODE_AND_DATA(bool _MODE, byte _DATA, byte data)
        {
            bool b1;
            b1 = ((igs012_prot_mode == _MODE) && (data == _DATA));
            return b1;
        }
        private static bool MODE_AND_DATA(bool _MODE, byte _DATA, ushort data)
        {
            bool b1;
            b1 = (igs012_prot_mode == _MODE) && (((data & 0xff00) == (_DATA << 8)) || ((data & 0xff) == _DATA));
            return b1;
        }
        private static void igs012_prot_mode_w(ushort data)
        {
            if (MODE_AND_DATA(false, 0xcc, data) || MODE_AND_DATA(true, 0xdd, data))
            {
                igs012_prot_mode = igs012_prot_mode ^ true;
            }
        }
        private static void igs012_prot_inc_w(ushort data)
        {
            if (MODE_AND_DATA(false, 0xff, data))
            {
                igs012_prot = (byte)((igs012_prot + 1) & 0x1f);
            }
        }
        private static void igs012_prot_dec_inc_w(byte data)
        {
            if (MODE_AND_DATA(false, 0xaa, data))
            {
                igs012_prot = (byte)((igs012_prot - 1) & 0x1f);
            }
            else if (MODE_AND_DATA(true, 0xfa, data))
            {
                igs012_prot = (byte)((igs012_prot + 1) & 0x1f);
            }
        }
        private static void igs012_prot_dec_inc_w(ushort data)
        {
            if (MODE_AND_DATA(false, 0xaa, data))
            {
                igs012_prot = (byte)((igs012_prot - 1) & 0x1f);
            }
            else if (MODE_AND_DATA(true, 0xfa, data))
            {
                igs012_prot = (byte)((igs012_prot + 1) & 0x1f);
            }
        }
        private static void igs012_prot_dec_copy_w(ushort data)
        {
            if (MODE_AND_DATA(false, 0x33, data))
            {
                igs012_prot = igs012_prot_swap;
            }
            else if (MODE_AND_DATA(true, 0x5a, data))
            {
                igs012_prot = (byte)((igs012_prot - 1) & 0x1f);
            }
        }
        private static void igs012_prot_copy_w(ushort data)
        {
            if (MODE_AND_DATA(true, 0x22, data))
            {
                igs012_prot = igs012_prot_swap;
            }
        }
        private static void igs012_prot_swap_w(ushort data)
        {
            if (MODE_AND_DATA(false, 0x55, data) || MODE_AND_DATA(true, 0xa5, data))
            {
                byte x = igs012_prot;
                igs012_prot_swap = (byte)((((BIT(x, 3) | BIT(x, 1)) ^ 1) << 3) | ((BIT(x, 2) & BIT(x, 1)) << 2) | ((BIT(x, 3) ^ BIT(x, 0)) << 1) | (BIT(x, 2) ^ 1));
            }
        }
        private static byte igs012_prot_r()
        {
            byte x = igs012_prot;
            byte b1 = (byte)((BIT(x, 3) | BIT(x, 1)) ^ 1);
            byte b0 = (byte)(BIT(x, 3) ^ BIT(x, 0));
            return (byte)((b1 << 1) | (b0 << 0));
        }
        public static void drgnwrld_igs003_w(int offset, byte data)
        {
            if ((offset & 1) == 0)
            {
                igs003_reg[offset / 2] = (ushort)((data << 8) | (igs003_reg[offset / 2] & 0xff));
            }
            else if ((offset & 1) == 1)
            {
                igs003_reg[offset / 2] = (ushort)((igs003_reg[offset / 2] & 0xff00) | data);
            }
            if ((offset / 2) == 0)
            {
                return;
            }
            switch (igs003_reg[0])
            {
                case 0x00:
                    if ((offset & 1) == 1)
                    {
                        Generic.coin_counter_w(0, data & 2);
                    }
                    break;
            }
        }
        public static void drgnwrld_igs003_w(int offset, ushort data)
        {
            igs003_reg[offset] = data;
            if (offset == 0)
            {
                return;
            }
            switch (igs003_reg[0])
            {
                case 0x00:
                    Generic.coin_counter_w(0, data & 2);
                    break;
                default:
                    break;
            }
        }
        public static byte drgnwrld_igs003_r()
        {
            switch (igs003_reg[0])
            {
                case 0x00:
                    /*if (Video.screenstate.frame_number >= 70 && Video.screenstate.frame_number <= 71)
                    {
                        return 0xfe;
                    }
                    else if (Video.screenstate.frame_number >= 80 && Video.screenstate.frame_number <= 81)
                    {
                        return 0xfb;
                    }
                    else*/
                    {
                        return (byte)sbyte0;
                    }
                case 0x01: return (byte)sbyte1;
                case 0x02:
                    /*if (Video.screenstate.frame_number >= 90 && Video.screenstate.frame_number <= 91)
                    {
                        return 0xfb;
                    }
                    else*/
                    {
                        return (byte)sbyte2;
                    }
                case 0x20: return 0x49;
                case 0x21: return 0x47;
                case 0x22: return 0x53;
                case 0x24: return 0x41;
                case 0x25: return 0x41;
                case 0x26: return 0x7f;
                case 0x27: return 0x41;
                case 0x28: return 0x41;
                case 0x2a: return 0x3e;
                case 0x2b: return 0x41;
                case 0x2c: return 0x49;
                case 0x2d: return 0xf9;
                case 0x2e: return 0x0a;
                case 0x30: return 0x26;
                case 0x31: return 0x49;
                case 0x32: return 0x49;
                case 0x33: return 0x49;
                case 0x34: return 0x32;

                default:
                    break;
            }
            return 0;
        }
        private static void lhb_inputs_w(int offset,byte data)
        {
            if (offset == 0)
            {
                igs_input_sel = (ushort)((data << 8) | (igs_input_sel & 0xff));
            }
            else if (offset == 1)
            {
                igs_input_sel = (ushort)((igs_input_sel & 0xff00) | data);
                Generic.coin_counter_w(0, data & 0x20);
            }
        }
        private static void lhb_inputs_w(ushort data)
        {
            igs_input_sel = data;
            Generic.coin_counter_w(0, data & 0x20);
        }
        private static ushort lhb_inputs_r(int offset)
        {
            switch (offset)
            {
                case 0:
                    return igs_input_sel;
                case 1:
                    if ((~igs_input_sel & 0x01) != 0)
                    {
                        return bkey0;
                    }
                    if ((~igs_input_sel & 0x02) != 0)
                    {
                        return bkey1;
                    }
                    if ((~igs_input_sel & 0x04) != 0)
                    {
                        return bkey2;
                    }
                    if ((~igs_input_sel & 0x08) != 0)
                    {
                        return bkey3;
                    }
                    if ((~igs_input_sel & 0x10) != 0)
                    {
                        return bkey4;
                    }
                    break;
            }
            return 0;
        }
        private static void lhb2_igs003_w1(int offset, byte data)
        {
            igs003_reg[offset] = (ushort)((data << 8) | (igs003_reg[offset] & 0xff));
            if (offset == 0)
            {
                return;
            }
            switch (igs003_reg[0])
            {
                case 0x00:
                    igs_input_sel = (ushort)((data << 8) | (igs_input_sel & 0xff));
                    break;
            }
        }
        private static void lhb2_igs003_w2(int offset, byte data)
        {
            igs003_reg[offset] = (ushort)((igs003_reg[offset] & 0xff00) | data);
            if (offset == 0)
            {
                return;
            }
            switch (igs003_reg[0])
            {
                case 0x00:
                    igs_input_sel = (ushort)((igs_input_sel & 0xff00) | data);
                    //if (ACCESSING_BITS_0_7)
                    {
                        Generic.coin_counter_w(0, data & 0x20);
                    }
                    break;
                case 0x02:
                    //if (ACCESSING_BITS_0_7)
                    {
                        lhb2_pen_hi = (byte)(data & 0x07);
                        OKI6295.okim6295_set_bank_base((data & 0x08) != 0 ? 0x40000 : 0);
                    }
                    break;
            }
        }
        private static void lhb2_igs003_w(int offset, ushort data)
        {
            igs003_reg[offset] = data;
            if (offset == 0)
            {
                return;
            }
            switch (igs003_reg[0])
            {
                case 0x00:
                    igs_input_sel = data;
                    //if (ACCESSING_BITS_0_7)
                    {
                        Generic.coin_counter_w(0, data & 0x20);
                    }
                    break;
                case 0x02:
                    //if (ACCESSING_BITS_0_7)
                    {
                        lhb2_pen_hi = (byte)(data & 0x07);
                        OKI6295.okim6295_set_bank_base((data & 0x08) != 0 ? 0x40000 : 0);
                    }
                    break;
            }
        }
        private static ushort lhb2_igs003_r()
        {
            switch (igs003_reg[0])
            {
                case 0x01:
                    if ((~igs_input_sel & 0x01) != 0)
                    {
                        //return input_port_read(machine, "KEY0");
                    }
                    if ((~igs_input_sel & 0x02) != 0)
                    {
                        //return input_port_read(machine, "KEY1");
                    }
                    if ((~igs_input_sel & 0x04) != 0)
                    {
                        //return input_port_read(machine, "KEY2");
                    }
                    if ((~igs_input_sel & 0x08) != 0)
                    {
                        //return input_port_read(machine, "KEY3");
                    }
                    if ((~igs_input_sel & 0x10) != 0)
                    {
                        //return input_port_read(machine, "KEY4");
                    }
                    break;
                case 0x03: return 0xff;

                case 0x20: return 0x49;
                case 0x21: return 0x47;
                case 0x22: return 0x53;

                case 0x24: return 0x41;
                case 0x25: return 0x41;
                case 0x26: return 0x7f;
                case 0x27: return 0x41;
                case 0x28: return 0x41;

                case 0x2a: return 0x3e;
                case 0x2b: return 0x41;
                case 0x2c: return 0x49;
                case 0x2d: return 0xf9;
                case 0x2e: return 0x0a;

                case 0x30: return 0x26;
                case 0x31: return 0x49;
                case 0x32: return 0x49;
                case 0x33: return 0x49;
                case 0x34: return 0x32;
            }
            return 0;
        }
        private static void wlcc_igs003_w1(int offset, byte data)
        {
            igs003_reg[offset] = (ushort)((data << 8) | (igs003_reg[offset] & 0xff));
            if (offset == 0)
            {
                return;
            }
        }
        private static void wlcc_igs003_w2(int offset, byte data)
        {
            igs003_reg[offset] = (ushort)((igs003_reg[offset] & 0xff00) | data);
            if (offset == 0)
            {
                return;
            }
            switch (igs003_reg[0])
            {
                case 0x02:
                    //if (ACCESSING_BITS_0_7)
                    {
                        Generic.coin_counter_w(0, data & 0x01);
                        OKI6295.okim6295_set_bank_base((data & 0x10) != 0 ? 0x40000 : 0);
                    }
                    break;
            }
        }
        private static void wlcc_igs003_w(int offset, ushort data)
        {
            igs003_reg[offset] = data;
            if (offset == 0)
            {
                return;
            }
            switch (igs003_reg[0])
            {
                case 0x02:
                    //if (ACCESSING_BITS_0_7)
                    {
                        Generic.coin_counter_w(0, data & 0x01);
                        OKI6295.okim6295_set_bank_base((data & 0x10) != 0 ? 0x40000 : 0);
                    }
                    break;
            }
        }
        private static byte wlcc_igs003_r()
        {
            switch (igs003_reg[0])
            {
                case 0x00: return (byte)sbyte0;

                case 0x20: return 0x49;
                case 0x21: return 0x47;
                case 0x22: return 0x53;

                case 0x24: return 0x41;
                case 0x25: return 0x41;
                case 0x26: return 0x7f;
                case 0x27: return 0x41;
                case 0x28: return 0x41;

                case 0x2a: return 0x3e;
                case 0x2b: return 0x41;
                case 0x2c: return 0x49;
                case 0x2d: return 0xf9;
                case 0x2e: return 0x0a;

                case 0x30: return 0x26;
                case 0x31: return 0x49;
                case 0x32: return 0x49;
                case 0x33: return 0x49;
                case 0x34: return 0x32;
            }
            return 0;
        }
        private static void xymg_igs003_w(int offset, ushort data)
        {
            igs003_reg[offset] = data;
            if (offset == 0)
                return;
            switch (igs003_reg[0])
            {
                case 0x01:
                    igs_input_sel = data;
                    //if (ACCESSING_BITS_0_7)
                    {
                        Generic.coin_counter_w(0, data & 0x20);
                    }
                    break;
            }
        }
        private static byte xymg_igs003_r()
        {
            switch (igs003_reg[0])
            {
                case 0x00:
                    return (byte)sbytec;
                case 0x02:
                    if ((~igs_input_sel & 0x01) != 0)
                    {
                        //return input_port_read(machine, "KEY0");
                    }
                    if ((~igs_input_sel & 0x02) != 0)
                    {
                        //return input_port_read(machine, "KEY1");
                    }
                    if ((~igs_input_sel & 0x04) != 0)
                    {
                        //return input_port_read(machine, "KEY2");
                    }
                    if ((~igs_input_sel & 0x08) != 0)
                    {
                        //return input_port_read(machine, "KEY3");
                    }
                    if ((~igs_input_sel & 0x10) != 0)
                    {
                        //return input_port_read(machine, "KEY4");
                    }
                    break;
                case 0x20: return 0x49;
                case 0x21: return 0x47;
                case 0x22: return 0x53;

                case 0x24: return 0x41;
                case 0x25: return 0x41;
                case 0x26: return 0x7f;
                case 0x27: return 0x41;
                case 0x28: return 0x41;

                case 0x2a: return 0x3e;
                case 0x2b: return 0x41;
                case 0x2c: return 0x49;
                case 0x2d: return 0xf9;
                case 0x2e: return 0x0a;

                case 0x30: return 0x26;
                case 0x31: return 0x49;
                case 0x32: return 0x49;
                case 0x33: return 0x49;
                case 0x34: return 0x32;
            }
            return 0;
        }
        private static void vbowl_igs003_w(int offset, ushort data)
        {
            igs003_reg[offset] = data;
            if (offset == 0)
                return;
            switch (igs003_reg[0])
            {
                case 0x02:
                    //if (ACCESSING_BITS_0_7)
                    {
                        Generic.coin_counter_w(0, data & 1);
                        Generic.coin_counter_w(1, data & 2);
                    }
                    break;
            }
        }
        private static byte vbowl_igs003_r()
        {
            switch (igs003_reg[0])
            {
                case 0x00:
                    return (byte)sbyte0;
                case 0x01:
                    return (byte)sbyte1;
                case 0x20: return 0x49;
                case 0x21: return 0x47;
                case 0x22: return 0x53;

                case 0x24: return 0x41;
                case 0x25: return 0x41;
                case 0x26: return 0x7f;
                case 0x27: return 0x41;
                case 0x28: return 0x41;

                case 0x2a: return 0x3e;
                case 0x2b: return 0x41;
                case 0x2c: return 0x49;
                case 0x2d: return 0xf9;
                case 0x2e: return 0x0a;

                case 0x30: return 0x26;
                case 0x31: return 0x49;
                case 0x32: return 0x49;
                case 0x33: return 0x49;
                case 0x34: return 0x32;
            }
            return 0;
        }
        private static void igs_YM3812_control_port_0_w(byte data)
        {
            //if (ACCESSING_BITS_0_7)
            YM3812.ym3812_control_port_0_w(data);
        }
        private static void igs_YM3812_write_port_0_w(byte data)
        {
            //if (ACCESSING_BITS_0_7)
            YM3812.ym3812_write_port_0_w(data);
        }
        private static void lhb_irq_enable_w(int offset,byte data)
        {
            if ((offset & 1) == 0)
            {
                lhb_irq_enable = (ushort)((data << 8) | (lhb_irq_enable & 0xff));
            }
            else if ((offset & 1) == 1)
            {
                lhb_irq_enable = (ushort)((lhb_irq_enable & 0xff00) | data);
            }
        }
        private static void lhb_irq_enable_w(ushort data)
        {
            lhb_irq_enable = data;
        }
        private static void lhb_okibank_w(byte data)
        {
            //ACCESSING_BITS_8_15
            OKI6295.okim6295_set_bank_base((data & 0x2) != 0 ? 0x40000 : 0);
        }
        private static void lhb_okibank_w(ushort data)
        {
            OKI6295.okim6295_set_bank_base((data & 0x200) != 0 ? 0x40000 : 0);
        }
        private static byte ics2115_0_word_r1(int offset)
        {
            switch (offset)
            {
                case 0:
                    return 0;
                case 1:
                    return 0;
                case 2:
                    return ICS2115.ics2115_r(3);
            }
            return 0;
        }
        private static byte ics2115_0_word_r2(int offset)
        {
            switch (offset)
            {
                case 0:
                    return ICS2115.ics2115_r(0);
                case 1:
                    return ICS2115.ics2115_r(1);
                case 2:
                    return ICS2115.ics2115_r(2);
            }
            return 0xff;
        }
        private static ushort ics2115_0_word_r(int offset)
        {
            switch (offset)
            {
                case 0:
                    return ICS2115.ics2115_r(0);
                case 1:
                    return ICS2115.ics2115_r(1);
                case 2:
                    return (ushort)((ICS2115.ics2115_r(3) << 8) | ICS2115.ics2115_r(2));
            }
            return 0xff;
        }
        private static void ics2115_0_word_w1(int offset, byte data)
        {
            switch (offset)
            {
                case 1:
                    break;
                case 2:
                    ICS2115.ics2115_w(3, data);
                    break;
            }
        }
        private static void ics2115_0_word_w2(int offset, byte data)
        {
            switch (offset)
            {
                case 1:
                    ICS2115.ics2115_w(1, data);
                    break;
                case 2:
                    ICS2115.ics2115_w(2, data);
                    break;
            }
        }
        private static void ics2115_0_word_w(int offset, ushort data)
        {
            switch (offset)
            {
                case 1:
                    ICS2115.ics2115_w(1, (byte)data);
                    break;
                case 2:
                    ICS2115.ics2115_w(2, (byte)data);
                    ICS2115.ics2115_w(3, (byte)(data >> 8));
                    break;
            }
        }
        private static byte vbowl_unk_r1()
        {
            return 0xff;
        }
        private static ushort vbowl_unk_r()
        {
            return 0xffff;
        }
        public static void video_eof_vbowl()
        {
            vbowl_trackball[0] = vbowl_trackball[1];
            //vbowl_trackball[1] = (input_port_read(machine, "AN1") << 8) | input_port_read(machine, "AN0");
        }
        private static void vbowl_pen_hi_w(byte data)
        {
            //if (ACCESSING_BITS_0_7)
            {
                lhb2_pen_hi = (byte)(data & 0x07);
            }
        }
        private static void vbowl_link_0_w()
        {

        }
        private static void vbowl_link_1_w()
        {

        }
        private static void vbowl_link_2_w()
        {

        }
        private static void vbowl_link_3_w()
        {

        }
    }
}

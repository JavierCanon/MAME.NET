using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class IGS011
    {
        public static sbyte MReadByte_igs012(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address >= 0 && address <= 0x1ffff)
            {
                int address2 = address & ~0x1c000;
                if (address2 >= 0x001610 && address2 <= 0x00161f)
                {
                    int i1 = 1;
                }
                else if (address2 >= 0x001660 && address2 <= 0x00166f)
                {
                    int i1 = 1;
                }
                else if (address >= 0x00d4c0 && address <= 0x00d4ff)
                {
                    int i1 = 1;
                }
                else
                {
                    result = MReadByte(address);
                }
            }
            else
            {
                result = MReadByte(address);
            }
            return result;
        }
        public static short MReadWord_igs012(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address >= 0 && address + 1 <= 0x1ffff)
            {
                int address2 = address & ~0x1c000;
                if (address2 >= 0x001610 && address2 + 1 <= 0x00161f)
                {
                    result = (short)igs012_prot_r();
                }
                else if (address2 >= 0x001660 && address2 + 1 <= 0x00166f)
                {
                    result = (short)igs012_prot_r();
                }
                else if (address >= 0x00d4c0 && address + 1 <= 0x00d4ff)
                {
                    result = (short)drgnwrldv20j_igs011_prot2_r();
                }
                else
                {
                    result = MReadWord(address);
                }
            }
            else
            {
                result = MReadWord(address);
            }
            return result;
        }
        public static int MReadLong_igs012(int address)
        {
            address &= 0xffffff;
            int result = 0;
            if (address >= 0 && address + 3 <= 0x1ffff)
            {
                int address2 = address & ~0x1c000;
                if (address2 >= 0x001610 && address2 + 3 <= 0x00161f)
                {
                    int i1 = 1;
                }
                else if (address2 >= 0x001660 && address2 + 3 <= 0x00166f)
                {
                    int i1 = 1;
                }
                else if (address >= 0x00d4c0 && address + 3 <= 0x00d4ff)
                {
                    int i1 = 1;
                }
                else
                {
                    result = MReadLong(address);
                }
            }
            else
            {
                result = MReadLong(address);
            }
            return result;
        }
        public static void MWriteByte_igs012(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0 && address <= 0x1ffff)
            {
                int address2 = address & ~0x1c000;
                if (address2 >= 0x001600 && address2 <= 0x00160f)
                {
                    igs012_prot_swap_w((byte)value);
                }
                else if (address2 >= 0x001620 && address2 <= 0x00162f)
                {
                    igs012_prot_dec_inc_w((byte)value);
                }
                else if (address2 >= 0x001630 && address2 <= 0x00163f)
                {
                    igs012_prot_inc_w((byte)value);
                }
                else if (address2 >= 0x001640 && address2 <= 0x00164f)
                {
                    igs012_prot_copy_w((byte)value);
                }
                else if (address2 >= 0x001650 && address2 <= 0x00165f)
                {
                    igs012_prot_dec_copy_w((byte)value);
                }
                else if (address2 >= 0x001670 && address2 <= 0x00167f)
                {
                    igs012_prot_mode_w((byte)value);
                }
                else if (address >= 0x00d400 && address <= 0x00d43f)
                {
                    igs011_prot2_dec_w();
                }
                else if (address >= 0x00d440 && address <= 0x00d47f)
                {
                    drgnwrld_igs011_prot2_swap_w();
                }
                else if (address >= 0x00d480 && address <= 0x00d4bf)
                {
                    igs011_prot2_reset_w();
                }
                else
                {
                    MWriteByte(address, value);
                }
            }
            else if (address >= 0x902000 && address <= 0x902fff)
            {
                igs012_prot_reset_w();
            }
            else
            {
                MWriteByte(address, value);
            }
        }
        public static void MWriteWord_igs012(int address, short value)
        {
            address &= 0xffffff;
            if (address >= 0 && address <= 0x1ffff)
            {
                int address2 = address & ~0x1c000;
                if (address2 >= 0x001600 && address2 + 1 <= 0x00160f)
                {
                    igs012_prot_swap_w((ushort)value);
                }
                else if (address2 >= 0x001620 && address2 + 1 <= 0x00162f)
                {
                    igs012_prot_dec_inc_w((ushort)value);
                }
                else if (address2 >= 0x001630 && address2 + 1 <= 0x00163f)
                {
                    igs012_prot_inc_w((ushort)value);
                }
                else if (address2 >= 0x001640 && address2 + 1 <= 0x00164f)
                {
                    igs012_prot_copy_w((ushort)value);
                }
                else if (address2 >= 0x001650 && address2 + 1 <= 0x00165f)
                {
                    igs012_prot_dec_copy_w((ushort)value);
                }
                else if (address2 >= 0x001670 && address2 + 1 <= 0x00167f)
                {
                    igs012_prot_mode_w((ushort)value);
                }
                else if (address >= 0x00d400 && address + 1 <= 0x00d43f)
                {
                    igs011_prot2_dec_w();
                }
                else if (address >= 0x00d440 && address + 1 <= 0x00d47f)
                {
                    drgnwrld_igs011_prot2_swap_w();
                }
                else if (address >= 0x00d480 && address + 1 <= 0x00d4bf)
                {
                    igs011_prot2_reset_w();
                }
                else
                {
                    MWriteWord(address, value);
                }
            }
            else if (address >= 0x902000 && address + 1 <= 0x902fff)
            {
                igs012_prot_reset_w();
            }
            else
            {
                MWriteWord(address, value);
            }
        }
        public static void MWriteLong_igs012(int address, int value)
        {
            address &= 0xffffff;
            if (address >= 0 && address <= 0x1ffff)
            {
                int address2 = address & ~0x1c000;
                if (address2 >= 0x001600 && address2 + 3 <= 0x00160f)
                {
                    int i1 = 1;
                }
                else if (address2 >= 0x001620 && address2 + 3 <= 0x00162f)
                {
                    int i1 = 1;
                }
                else if (address2 >= 0x001630 && address2 + 3 <= 0x00163f)
                {
                    int i1 = 1;
                }
                else if (address2 >= 0x001640 && address2 + 3 <= 0x00164f)
                {
                    int i1 = 1;
                }
                else if (address2 >= 0x001650 && address2 + 3 <= 0x00165f)
                {
                    int i1 = 1;
                }
                else if (address2 >= 0x001670 && address2 + 3 <= 0x00167f)
                {
                    int i1 = 1;
                }
                else if (address >= 0x00d400 && address + 3 <= 0x00d43f)
                {
                    int i1 = 1;
                }
                else if (address >= 0x00d440 && address + 3 <= 0x00d47f)
                {
                    int i1 = 1;
                }
                else if (address >= 0x00d480 && address + 3 <= 0x00d4bf)
                {
                    int i1 = 1;
                }
                else
                {
                    MWriteLong(address, value);
                }
            }
            else if (address >= 0x902000 && address + 3 <= 0x902fff)
            {
                int i1 = 1;
            }
            else
            {
                MWriteLong(address, value);
            }
        }
        public static short MReadWord_igs012_drgnwrldv21(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address >= 0 && address + 1 <= 0x1ffff)
            {
                int address2 = address & ~0x1c000;
                if (address2 >= 0x001610 && address2 + 1 <= 0x00161f)
                {
                    result = (short)igs012_prot_r();
                }
                else if (address2 >= 0x001660 && address2 + 1 <= 0x00166f)
                {
                    result = (short)igs012_prot_r();
                }
                else if (address >= 0x00d4c0 && address + 1 <= 0x00d4ff)
                {
                    result = (short)drgnwrldv21_igs011_prot2_r();
                }
                else
                {
                    result = MReadWord(address);
                }
            }
            else
            {
                result = MReadWord(address);
            }
            return result;
        }
        public static sbyte MReadByte_lhb(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                result = (sbyte)Memory.mainrom[address];
            }
            else if (address >= 0x100000 && address <= 0x103fff)
            {
                int offset = address - 0x100000;
                result = (sbyte)Generic.generic_nvram[offset];
            }
            else if (address >= 0x200000 && address <= 0x200fff)
            {
                int offset = (address - 0x200000) / 2;
                if ((address & 1) == 0)
                {
                    result = (sbyte)(priority_ram[offset] >> 8);
                }
                else if ((address & 1) == 1)
                {
                    result = (sbyte)priority_ram[offset];
                }
            }
            else if (address >= 0x300000 && address <= 0x3fffff)
            {
                int offset = address - 0x300000;
                if ((offset & 1) == 0)
                {
                    result = (sbyte)igs011_layers_r1(offset / 2);
                }
                else if ((offset & 1) == 1)
                {
                    result = (sbyte)igs011_layers_r2(offset / 2);
                }
            }
            else if (address >= 0x600000 && address <= 0x600001)
            {
                //if (address == 0x600001)
                {
                    result = (sbyte)OKI6295.okim6295_status_0_lsb_r();
                }
            }
            else if (address >= 0x700000 && address <= 0x700001)
            {
                if (address == 0x700001)
                {
                    result = sbytec;
                }
            }
            else if (address >= 0x700002 && address <= 0x700005)
            {
                int offset = (address - 0x700002) / 2;
                if ((offset & 1) == 0)
                {
                    int i1 = 1;
                }
                else if ((offset & 1) == 1)
                {
                    result = (sbyte)lhb_inputs_r(offset);
                }
            }
            else if (address >= 0x888000 && address <= 0x888001)
            {
                result = (sbyte)igs_5_dips_r();
            }
            return result;
        }
        public static short MReadWord_lhb(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address >= 0x000000 && address + 1 <= 0x07ffff)
            {
                result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
            }
            else if (address >= 0x100000 && address + 1 <= 0x103fff)
            {
                int offset = address - 0x100000;
                result = (short)(Generic.generic_nvram[offset] * 0x100 + Generic.generic_nvram[offset + 1]);
            }
            else if (address >= 0x200000 && address + 1 <= 0x200fff)
            {
                int offset = (address - 0x200000) / 2;
                result = (short)priority_ram[offset];
            }
            else if (address >= 0x300000 && address + 1 <= 0x3fffff)
            {
                int offset = (address - 0x300000) / 2;
                result = (short)igs011_layers_r(offset);
            }
            else if (address >= 0x600000 && address + 1 <= 0x600001)
            {
                result = (short)OKI6295.okim6295_status_0_lsb_r();
            }
            else if (address >= 0x700000 && address + 1 <= 0x700001)
            {
                result = sbytec;
            }
            else if (address >= 0x700002 && address + 1 <= 0x700005)
            {
                int offset = (address - 0x700002) / 2;
                result = (short)lhb_inputs_r(offset);
            }
            else if (address >= 0x888000 && address + 1 <= 0x888001)
            {
                result = (short)((byte)igs_5_dips_r());
            }
            return result;
        }
        public static int MReadLong_lhb(int address)
        {
            address &= 0xffffff;
            int result = 0;
            if (address >= 0x000000 && address + 3 <= 0x07ffff)
            {
                result = Memory.mainrom[address] * 0x1000000 + Memory.mainrom[address + 1] * 0x10000 + Memory.mainrom[address + 2] * 0x100 + Memory.mainrom[address + 3];
            }
            else if (address >= 0x100000 && address + 3 <= 0x103fff)
            {
                int offset = address - 0x100000;
                result = Generic.generic_nvram[offset] * 0x1000000 + Generic.generic_nvram[offset + 1] * 0x10000 + Generic.generic_nvram[offset + 2] * 0x100 + Generic.generic_nvram[offset + 3];
            }
            else if (address >= 0x200000 && address + 3 <= 0x200fff)
            {
                int offset = (address - 0x200000) / 2;
                result = priority_ram[offset] * 0x10000 + priority_ram[offset + 1];
            }
            else if (address >= 0x300000 && address + 3 <= 0x3fffff)
            {
                int offset = (address - 0x300000) / 2;
                result = igs011_layers_r(offset) * 0x10000 + igs011_layers_r(offset + 1);
            }
            else if (address >= 0x700002 && address + 3 <= 0x700005)
            {
                int offset = (address - 0x700002) / 2;
                result = lhb_inputs_r(offset) * 0x10000 + lhb_inputs_r(offset + 1);
            }
            return result;
        }
        public static void MWriteByte_lhb(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x010000 && address <= 0x010001)
            {
                if ((address & 1) == 0)
                {
                    lhb_okibank_w((byte)value);
                }
            }
            else if (address >= 0x100000 && address <= 0x103fff)
            {
                int offset = address - 0x100000;
                Generic.generic_nvram[offset] = (byte)value;
            }
            else if (address >= 0x200000 && address <= 0x200fff)
            {
                int offset = (address - 0x200000) / 2;
                if ((address & 1) == 0)
                {
                    priority_ram[offset] = (ushort)((value << 8) | (priority_ram[offset] & 0xff));
                }
                else if ((address & 1) == 1)
                {
                    priority_ram[offset] = (ushort)((priority_ram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x300000 && address <= 0x3fffff)
            {
                int offset = address - 0x300000;
                igs011_layers_w(offset, (byte)value);
            }
            else if (address >= 0x400000 && address <= 0x401fff)
            {
                int offset = address - 0x400000;
                igs011_palette(offset, (byte)value);
            }
            else if (address >= 0x600000 && address <= 0x600001)
            {
                if (address == 0x600001)
                {
                    OKI6295.okim6295_data_0_lsb_w((byte)value);
                }
            }
            else if (address >= 0x700002 && address <= 0x700003)
            {
                int offset = address - 0x700002;
                lhb_inputs_w(offset, (byte)value);
            }
            else if (address >= 0x820000 && address <= 0x820001)
            {
                int offset = address - 0x820000;
                igs011_priority_w(offset, (byte)value);
            }
            else if (address >= 0x838000 && address <= 0x838001)
            {
                int offset = address - 0x838000;
                lhb_irq_enable_w(offset, (byte)value);
            }
            else if (address >= 0x840000 && address <= 0x840001)
            {
                int offset = address - 0x840000;
                igs_dips_w(offset, (byte)value);
            }
            else if (address >= 0x858000 && address <= 0x858001)
            {
                int offset = address - 0x858000;
                igs011_blit_x_w(offset, (byte)value);
            }
            else if (address >= 0x858800 && address <= 0x858801)
            {
                int offset = address - 0x858800;
                igs011_blit_y_w(offset, (byte)value);
            }
            else if (address >= 0x859000 && address <= 0x859001)
            {
                int offset = address - 0x859000;
                igs011_blit_w_w(offset, (byte)value);
            }
            else if (address >= 0x859800 && address <= 0x859801)
            {
                int offset = address - 0x859800;
                igs011_blit_h_w(offset, (byte)value);
            }
            else if (address >= 0x85a000 && address <= 0x85a001)
            {
                int offset = address - 0x85a000;
                igs011_blit_gfx_lo_w(offset, (byte)value);
            }
            else if (address >= 0x85a800 && address <= 0x85a801)
            {
                int offset = address - 0x85a800;
                igs011_blit_gfx_hi_w(offset, (byte)value);
            }
            else if (address >= 0x85b000 && address <= 0x85b001)
            {
                int i1 = 1;
                //igs011_blit_flags_w((byte)value);
            }
            else if (address >= 0x85b800 && address <= 0x85b801)
            {
                int offset = address - 0x85b800;
                igs011_blit_pen_w(offset, (byte)value);
            }
            else if (address >= 0x85c000 && address <= 0x85c001)
            {
                int offset = address - 0x85c000;
                igs011_blit_depth_w(offset, (byte)value);
            }
        }
        public static void MWriteWord_lhb(int address, short value)
        {
            address &= 0xffffff;
            if (address >= 0x010000 && address + 1 <= 0x010001)
            {
                lhb_okibank_w((ushort)value);
            }
            else if (address >= 0x100000 && address + 1 <= 0x103fff)
            {
                int offset = address - 0x100000;
                Generic.generic_nvram[offset] = (byte)(value >> 8);
                Generic.generic_nvram[offset + 1] = (byte)value;
            }
            else if (address >= 0x200000 && address + 1 <= 0x200fff)
            {
                int offset = (address - 0x200000) / 2;
                priority_ram[offset] = (ushort)value;
            }
            else if (address >= 0x300000 && address + 1 <= 0x3fffff)
            {
                int offset = (address - 0x300000)/2;
                igs011_layers_w(offset,(ushort)value);
            }
            else if (address >= 0x400000 && address + 1 <= 0x401fff)
            {
                int offset = (address - 0x400000)/2;
                igs011_palette(offset,(ushort)value);
            }
            else if (address >= 0x600000 && address + 1 <= 0x600001)
            {
                OKI6295.okim6295_data_0_lsb_w((byte)value);
            }
            else if (address >= 0x700002 && address + 1 <= 0x700003)
            {
                lhb_inputs_w((ushort)value);
            }
            else if (address >= 0x820000 && address + 1 <= 0x820001)
            {
                igs011_priority_w((ushort)value);
            }
            else if (address >= 0x838000 && address + 1 <= 0x838001)
            {
                lhb_irq_enable_w((ushort)value);
            }
            else if (address >= 0x840000 && address + 1 <= 0x840001)
            {
                igs_dips_w((ushort)value);
            }
            else if (address >= 0x858000 && address + 1 <= 0x858001)
            {
                igs011_blit_x_w((ushort)value);
            }
            else if (address >= 0x858800 && address + 1 <= 0x858801)
            {
                igs011_blit_y_w((ushort)value);
            }
            else if (address >= 0x859000 && address + 1 <= 0x859001)
            {
                igs011_blit_w_w((ushort)value);
            }
            else if (address >= 0x859800 && address + 1 <= 0x859801)
            {
                igs011_blit_h_w((ushort)value);
            }
            else if (address >= 0x85a000 && address + 1 <= 0x85a001)
            {
                igs011_blit_gfx_lo_w((ushort)value);
            }
            else if (address >= 0x85a800 && address + 1 <= 0x85a801)
            {
                igs011_blit_gfx_hi_w((ushort)value);
            }
            else if (address >= 0x85b000 && address + 1 <= 0x85b001)
            {
                igs011_blit_flags_w((ushort)value);
            }
            else if (address >= 0x85b800 && address + 1 <= 0x85b801)
            {
                igs011_blit_pen_w((ushort)value);
            }
            else if (address >= 0x85c000 && address + 1 <= 0x85c001)
            {
                igs011_blit_depth_w((ushort)value);
            }
        }
        public static void MWriteLong_lhb(int address, int value)
        {
            address &= 0xffffff;
            if (address >= 0x100000 && address + 1 <= 0x103fff)
            {
                int offset = address - 0x100000;
                Generic.generic_nvram[offset] = (byte)(value >> 24);
                Generic.generic_nvram[offset + 1] = (byte)(value >> 16);
                Generic.generic_nvram[offset + 2] = (byte)(value >> 8);
                Generic.generic_nvram[offset + 3] = (byte)value;
            }
            else if (address >= 0x200000 && address + 1 <= 0x200fff)
            {
                int offset = (address - 0x200000) / 2;
                priority_ram[offset] = (ushort)(value >> 16);
                priority_ram[offset + 1] = (ushort)value;
            }
            else if (address >= 0x300000 && address + 1 <= 0x3fffff)
            {
                int offset = (address - 0x300000) / 2;
                igs011_layers_w(offset, (ushort)(value>>16));
                igs011_layers_w(offset + 1, (ushort)value);
            }
            else if (address >= 0x400000 && address + 1 <= 0x401fff)
            {
                int offset = (address - 0x400000) / 2;
                igs011_palette(offset, (ushort)(value>>16));
                igs011_palette(offset + 1, (ushort)value);
            }            
        }
    }
}

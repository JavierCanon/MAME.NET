using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace mame
{
    public partial class CPS
    {
        public static ushort[] cps_a_regs, cps_b_regs, cps2_objram1, cps2_objram2, cps2_output;
        public static byte[] mainromop, gfxrom, gfx1rom, audioromop, starsrom, user1rom;
        public static byte[] gfxram;
        public static byte[] qsound_sharedram1, qsound_sharedram2;
        public static byte[] mainram2, mainram3;
        public static byte dswa, dswb, dswc;
        public static int cps_version;
        public static int basebanksnd;
        public static int sf2ceblp_prot;
        public static int dial0, dial1;
        public static int scrollxoff, scrollyoff;
        public static int cps2networkpresent, cps2_objram_bank;
        public static int scancount, cps1_scanline1, cps1_scanline2, cps1_scancalls;
        public static List<gfx_range> lsRange0, lsRange1, lsRange2, lsRangeS;
        public class gfx_range
        {
            public int start;
            public int end;
            public int add;
            public gfx_range(int i1, int i2, int i3)
            {
                start = i1;
                end = i2;
                add = i3;
            }
        }
        public static sbyte[] ByteToSbyte(byte[] bb1)
        {
            sbyte[] bb2 = null;
            int n1;
            if (bb1 != null)
            {
                n1 = bb1.Length;
                bb2 = new sbyte[n1];
                Buffer.BlockCopy(bb1, 0, bb2, 0, n1);
            }
            return bb2;
        }
        public static void CPSInit()
        {
            int i, n;
            cps_a_regs = new ushort[0x20];
            cps_b_regs = new ushort[0x20];
            gfxram = new byte[0x30000];
            Memory.mainram = new byte[0x10000];
            Memory.audioram = new byte[0x800];
            Machine.bRom = true;
            Memory.mainrom = Machine.GetRom("maincpu.rom");
            gfxrom = Machine.GetRom("gfx.rom");
            n = gfxrom.Length;
            gfx1rom = new byte[n * 2];
            for (i = 0; i < n; i++)
            {
                gfx1rom[i * 2] = (byte)(gfxrom[i] & 0x0f);
                gfx1rom[i * 2 + 1] = (byte)(gfxrom[i] >> 4);
            }
            total_elements = n / 0x80;
            Memory.audiorom = Machine.GetRom("audiocpu.rom");
            switch (Machine.sBoard)
            {
                case "CPS-1":
                    cps_version = 1;
                    starsrom = Machine.GetRom("stars.rom");
                    OKI6295.okirom = Machine.GetRom("oki.rom");
                    if (Memory.mainrom == null || gfxrom == null || Memory.audiorom == null || OKI6295.okirom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "CPS-1(QSound)":
                    cps_version = 1;
                    qsound_sharedram1 = new byte[0x1000];
                    qsound_sharedram2 = new byte[0x1000];
                    audioromop = Machine.GetRom("audiocpuop.rom");
                    user1rom = Machine.GetRom("user1.rom");
                    QSound.qsoundrom = ByteToSbyte(Machine.GetRom("qsound.rom"));
                    if (Memory.mainrom == null || audioromop == null || gfxrom == null || Memory.audiorom == null || QSound.qsoundrom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "CPS2":
                    cps_version = 2;
                    cps2_objram1 = new ushort[0x1000];
                    cps2_objram2 = new ushort[0x1000];
                    cps2_output = new ushort[0x06];
                    cps2networkpresent = 0;
                    cps2_objram_bank = 0;
                    scancount = 0;
                    cps1_scanline1 = 262;
                    cps1_scanline2 = 262;
                    cps1_scancalls = 0;
                    qsound_sharedram1 = new byte[0x1000];
                    qsound_sharedram2 = new byte[0x1000];
                    if (Machine.sManufacturer != "bootleg")
                    {
                        mainromop = Machine.GetRom("maincpuop.rom");
                    }
                    audioromop = Machine.GetRom("audiocpu.rom");
                    QSound.qsoundrom = ByteToSbyte(Machine.GetRom("qsound.rom"));
                    if (Memory.mainrom == null || (Machine.sManufacturer != "bootleg" && mainromop == null) || audioromop == null || gfxrom == null || Memory.audiorom == null || QSound.qsoundrom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
            }
            if (Machine.bRom)
            {
                scrollxoff = 0x00;
                scrollyoff = 0x100;
                switch (Machine.sName)
                {
                    case "forgottn":
                    case "forgottna":
                    case "forgottnu":
                    case "forgottnue":
                    case "forgottnuc":
                    case "forgottnua":
                    case "forgottnuaa":
                    case "lostwrld":
                    case "lostwrldo":
                        cpsb_addr = -1;
                        cpsb_value = 0x0000;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x7fff, 0));
                        lsRange0.Add(new gfx_range(0x8000, 0xffff, -0x8000));
                        lsRange0.Add(new gfx_range(0x10000, 0x17fff, -0x10000));
                        lsRange0.Add(new gfx_range(0x18000, 0x1ffff, -0x18000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x3fff, 0x4000));
                        lsRange1.Add(new gfx_range(0x4000, 0x7fff, 0));
                        lsRange1.Add(new gfx_range(0x8000, 0xbfff, -0x4000));
                        lsRange1.Add(new gfx_range(0xc000, 0xffff, -0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x0fff, 0x1000));
                        lsRange2.Add(new gfx_range(0x1000, 0x1fff, 0));
                        lsRange2.Add(new gfx_range(0x2000, 0x2fff, -0x1000));
                        lsRange2.Add(new gfx_range(0x3000, 0x3fff, -0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x3fff, 0));
                        break;
                    case "ghouls":
                    case "ghoulsu":
                        cpsb_addr = -1;
                        cpsb_value = 0x0000;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x7fff, 0));
                        lsRange0.Add(new gfx_range(0x8000, 0xffff, -0x8000));
                        lsRange0.Add(new gfx_range(0x10000, 0x17fff, -0x10000));
                        lsRange0.Add(new gfx_range(0x18000, 0x1ffff, -0x18000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x3fff, 0));
                        lsRange1.Add(new gfx_range(0x4000, 0x7fff, -0x4000));
                        lsRange1.Add(new gfx_range(0x8000, 0xbfff, -0x8000));
                        lsRange1.Add(new gfx_range(0xc000, 0xffff, -0xc000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x13ff, 0));
                        lsRange2.Add(new gfx_range(0x1400, 0x17ff, -0x400));
                        lsRange2.Add(new gfx_range(0x1800, 0x1fff, -0x1000));
                        lsRange2.Add(new gfx_range(0x2000, 0x2fff, -0x2000));
                        lsRange2.Add(new gfx_range(0x3000, 0x3fff, -0x3000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x0fff, 0));
                        lsRangeS.Add(new gfx_range(0x1000, 0x1fff, 0x4000));
                        lsRangeS.Add(new gfx_range(0x2000, 0x3fff, 0));
                        lsRangeS.Add(new gfx_range(0x4000, 0x7fff, -0x4000));
                        lsRangeS.Add(new gfx_range(0x8000, 0xbfff, -0x8000));
                        lsRangeS.Add(new gfx_range(0xc000, 0xffff, -0xc000));
                        break;
                    case "daimakai":
                        cpsb_addr = -1;
                        cpsb_value = 0x0000;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x2000, 0x3fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x03ff, 0x1000));
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0xc00));
                        lsRange2.Add(new gfx_range(0x0800, 0x0bff, 0x800));
                        lsRange2.Add(new gfx_range(0x0c00, 0x0fff, 0x400));
                        lsRange2.Add(new gfx_range(0x1000, 0x13ff, 0));
                        lsRange2.Add(new gfx_range(0x1400, 0x17ff, -0x400));
                        lsRange2.Add(new gfx_range(0x1800, 0x1bff, -0x800));
                        lsRange2.Add(new gfx_range(0x1c00, 0x1fff, -0xc00));
                        lsRange2.Add(new gfx_range(0x2000, 0x23ff, -0x1000));
                        lsRange2.Add(new gfx_range(0x2400, 0x27ff, -0x1400));
                        lsRange2.Add(new gfx_range(0x2800, 0x2bff, -0x1800));
                        lsRange2.Add(new gfx_range(0x2c00, 0x2fff, -0x1c00));
                        lsRange2.Add(new gfx_range(0x3000, 0x33ff, -0x2000));
                        lsRange2.Add(new gfx_range(0x3400, 0x37ff, -0x2400));
                        lsRange2.Add(new gfx_range(0x3800, 0x3bff, -0x2800));
                        lsRange2.Add(new gfx_range(0x3c00, 0x3fff, -0x2c00));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x0fff, 0));
                        lsRangeS.Add(new gfx_range(0x1000, 0x1fff, 0x4000));
                        break;
                    case "daimakair":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x2000, 0x2fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x0fff, 0x1000));
                        lsRange2.Add(new gfx_range(0x1000, 0x1fff, 0));
                        lsRange2.Add(new gfx_range(0x2000, 0x2fff, -0x1000));
                        lsRange2.Add(new gfx_range(0x3000, 0x3fff, -0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x0fff, 0));
                        lsRangeS.Add(new gfx_range(0x1000, 0x1fff, 0x4000));
                        break;
                    case "strider":
                    case "striderua":
                        cpsb_addr = -1;
                        cpsb_value = 0x0000;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xbf;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x7000, 0x7fff, 0x8000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x0fff, 0x1000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x27ff, 0));
                        break;
                    case "strideruc":
                        cpsb_addr = 0x08;
                        cpsb_value = 0x0407;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x14;
                        priority = new int[4] { 0x12, 0x10, 0x0e, 0x0c };
                        palette_control = 0x0a;
                        layer_enable_mask = new int[5] { 0x08, 0x10, 0x02, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xbf;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x7000, 0x7fff, 0x8000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x0fff, 0x1000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x27ff, 0));
                        break;
                    case "striderj":
                        cpsb_addr = -1;
                        cpsb_value = 0x0000;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xbf;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x7000, 0x7fff, 0x8000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x0fff, 0x1000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x27ff, 0));
                        break;
                    case "striderjr":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xbf;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x7000, 0x7fff, 0x8000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x0fff, 0x1000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x27ff, 0));
                        break;
                    case "dynwar":
                    case "dynwara":
                    case "dynwarj":
                        cpsb_addr = 0x20;
                        cpsb_value = 0x0002;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x2c;
                        priority = new int[4] { 0x2a, 0x28, 0x26, 0x24 };
                        palette_control = 0x22;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x6000, 0x7fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2000, 0x3fff, 0x4000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x07ff, 0x1000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x2fff, 0));
                        break;
                    case "dynwarjr":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x6000, 0x7fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2000, 0x3fff, 0x4000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x07ff, 0x1000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x2fff, 0));
                        break;
                    case "willow":
                    case "willowu":
                    case "willowuo":
                    case "willowj":
                        cpsb_addr = -1;
                        cpsb_value = 0x0000;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x30;
                        priority = new int[4] { 0x2e, 0x2c, 0x2a, 0x28 };
                        palette_control = 0x26;
                        layer_enable_mask = new int[5] { 0x20, 0x10, 0x08, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x7000, 0x7fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x1fff, 0x4000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0a00, 0x0dff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x27ff, 0));
                        break;
                    case "unsquad":
                    case "area88":
                        cpsb_addr = 0x32;
                        cpsb_value = 0x0401;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x08, 0x10, 0x20, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x3000, 0x3fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2000, 0x2fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0c00, 0x0fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x17ff, 0));
                        break;
                    case "area88r":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x3000, 0x3fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2000, 0x2fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0c00, 0x0fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x17ff, 0));
                        break;
                    case "ffight":
                    case "ffighta":
                    case "ffightu":
                    case "ffightu1":
                    case "ffightj":
                        cpsb_addr = 0x20;
                        cpsb_value = 0x0004;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x2e;
                        priority = new int[4] { 0x26, 0x30, 0x28, 0x32 };
                        palette_control = 0x2a;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4400, 0x4bff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x3000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0980, 0x0bff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x21ff, 0));
                        break;
                    case "ffightua":
                    case "ffightj1":
                    case "ffightjh":
                        cpsb_addr = -1;
                        cpsb_value = 0x0000;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4400, 0x4bff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x3000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0980, 0x0bff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x21ff, 0));
                        break;
                    case "ffightub":
                        cpsb_addr = -1;
                        cpsb_value = 0x0000;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x30;
                        priority = new int[4] { 0x2e, 0x2c, 0x2a, 0x28 };
                        palette_control = 0x26;
                        layer_enable_mask = new int[5] { 0x20, 0x10, 0x08, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4400, 0x4bff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x3000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0980, 0x0bff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x21ff, 0));
                        break;
                    case "ffightuc":
                    case "ffightj3":
                        cpsb_addr = 0x20;
                        cpsb_value = 0x0005;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x28;
                        priority = new int[4] { 0x2a, 0x2c, 0x2e, 0x30 };
                        palette_control = 0x32;
                        layer_enable_mask = new int[5] { 0x02, 0x08, 0x20, 0x14, 0x14 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4400, 0x4bff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x3000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0980, 0x0bff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x21ff, 0));
                        break;
                    case "ffightj2":
                        cpsb_addr = 0x20;
                        cpsb_value = 0x0002;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x2c;
                        priority = new int[4] { 0x2a, 0x28, 0x26, 0x24 };
                        palette_control = 0x22;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4400, 0x4bff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x3000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0980, 0x0bff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x21ff, 0));
                        break;
                    case "1941":
                    case "1941r1":
                    case "1941u":
                    case "1941j":
                        cpsb_addr = 0x20;
                        cpsb_value = 0x0005;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x28;
                        priority = new int[4] { 0x2a, 0x2c, 0x2e, 0x30 };
                        palette_control = 0x32;
                        layer_enable_mask = new int[5] { 0x02, 0x08, 0x20, 0x14, 0x14 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x47ff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2400, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x0fff, 0));
                        break;
                    case "mercs":
                    case "mercsu":
                    case "mercsur1":
                    case "mercsj":
                        cpsb_addr = 0x20;
                        cpsb_value = 0x0402;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x2c;
                        priority = new int[4] { 0x2a, 0x28, 0x26, 0x24 };
                        palette_control = 0x22;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x34;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x0bff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0600, 0x1dff, 0));
                        lsRange1.Add(new gfx_range(0x5400, 0x5bff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0780, 0x097f, 0));
                        lsRange2.Add(new gfx_range(0x1700, 0x17ff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x2600, 0x53ff, 0));
                        break;
                    case "mtwins":
                    case "chikij":
                        cpsb_addr = 0x1e;
                        cpsb_value = 0x0404;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x12;
                        priority = new int[4] { 0x14, 0x16, 0x18, 0x1a };
                        palette_control = 0x1c;
                        layer_enable_mask = new int[5] { 0x08, 0x20, 0x10, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xdc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x3000, 0x3fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2000, 0x37ff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0e00, 0x0fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x17ff, 0));
                        break;
                    case "msword":
                    case "mswordr1":
                    case "mswordu":
                    case "mswordj":
                        cpsb_addr = 0x2e;
                        cpsb_value = 0x0403;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x22;
                        priority = new int[4] { 0x24, 0x26, 0x28, 0x2a };
                        palette_control = 0x2c;
                        layer_enable_mask = new int[5] { 0x20, 0x02, 0x04, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xbc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x37ff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0e00, 0x0fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x1fff, 0));
                        break;
                    case "cawing":
                    case "cawingr1":
                    case "cawingu":
                    case "cawingj":
                        cpsb_addr = 0x00;
                        cpsb_value = 0x0406;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x0c;
                        priority = new int[4] { 0x0a, 0x08, 0x06, 0x04 };
                        palette_control = 0x02;
                        layer_enable_mask = new int[5] { 0x10, 0x0a, 0x0a, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x5000, 0x57ff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x17ff, 0));
                        lsRange1.Add(new gfx_range(0x2c00, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0600, 0x09ff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x17ff, 0));
                        lsRangeS.Add(new gfx_range(0x2c00, 0x3fff, 0));
                        break;
                    case "nemo":
                    case "nemor1":
                    case "nemoj":
                        cpsb_addr = 0x0e;
                        cpsb_value = 0x0405;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x02;
                        priority = new int[4] { 0x04, 0x06, 0x08, 0x0a };
                        palette_control = 0x0c;
                        layer_enable_mask = new int[5] { 0x04, 0x02, 0x20, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x47ff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x1fff, 0));
                        lsRange1.Add(new gfx_range(0x2400, 0x33ff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0d00, 0x0fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x1fff, 0));
                        lsRangeS.Add(new gfx_range(0x2400, 0x3dff, 0));
                        break;
                    case "sf2":
                    case "sf2ug":
                        cpsb_addr = 0x32;
                        cpsb_value = 0x0401;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x08, 0x10, 0x20, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2eb":
                    case "sf2ua":
                    case "sf2ub":
                    case "sf2uk":
                    case "sf2qp1":
                    case "sf2thndr":
                        cpsb_addr = 0x08;
                        cpsb_value = 0x0407;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x14;
                        priority = new int[4] { 0x12, 0x10, 0x0e, 0x0c };
                        palette_control = 0x0a;
                        layer_enable_mask = new int[5] { 0x08, 0x10, 0x02, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2ed":
                    case "sf2ud":
                        cpsb_addr = 0x20;
                        cpsb_value = 0x0005;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x28;
                        priority = new int[4] { 0x2a, 0x2c, 0x2e, 0x30 };
                        palette_control = 0x32;
                        layer_enable_mask = new int[5] { 0x02, 0x08, 0x20, 0x14, 0x14 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2ee":
                    case "sf2ue":
                        cpsb_addr = 0x10;
                        cpsb_value = 0x0408;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x1c;
                        priority = new int[4] { 0x1a, 0x18, 0x16, 0x14 };
                        palette_control = 0x12;
                        layer_enable_mask = new int[5] { 0x10, 0x08, 0x02, 0x00, 0x00 };
                        in2_addr = 0x3c;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2uc":
                        cpsb_addr = 0x20;
                        cpsb_value = 0x0402;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x2c;
                        priority = new int[4] { 0x2a, 0x28, 0x26, 0x24 };
                        palette_control = 0x22;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2uf":
                        cpsb_addr = 0x0e;
                        cpsb_value = 0x0405;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x02;
                        priority = new int[4] { 0x04, 0x06, 0x08, 0x0a };
                        palette_control = 0x0c;
                        layer_enable_mask = new int[5] { 0x04, 0x02, 0x20, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2ui":
                        cpsb_addr = 0x1e;
                        cpsb_value = 0x0404;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x12;
                        priority = new int[4] { 0x14, 0x16, 0x18, 0x1a };
                        palette_control = 0x1c;
                        layer_enable_mask = new int[5] { 0x08, 0x20, 0x10, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2j":
                    case "sf2jh":
                        cpsb_addr = 0x2e;
                        cpsb_value = 0x0403;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x22;
                        priority = new int[4] { 0x24, 0x26, 0x28, 0x2a };
                        palette_control = 0x2c;
                        layer_enable_mask = new int[5] { 0x20, 0x02, 0x04, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2ja":
                    case "sf2jl":
                        cpsb_addr = 0x08;
                        cpsb_value = 0x0407;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x14;
                        priority = new int[4] { 0x12, 0x10, 0x0e, 0x0c };
                        palette_control = 0x0a;
                        layer_enable_mask = new int[5] { 0x08, 0x10, 0x02, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2jc":
                        cpsb_addr = 0x20;
                        cpsb_value = 0x0402;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x2c;
                        priority = new int[4] { 0x2a, 0x28, 0x26, 0x24 };
                        palette_control = 0x22;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2jf":
                        cpsb_addr = 0x0e;
                        cpsb_value = 0x0405;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x02;
                        priority = new int[4] { 0x04, 0x06, 0x08, 0x0a };
                        palette_control = 0x0c;
                        layer_enable_mask = new int[5] { 0x04, 0x02, 0x20, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2ebbl":
                    case "sf2ebbl2":
                    case "sf2ebbl3":
                        cpsb_addr = 0x08;
                        cpsb_value = 0x0407;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x14;
                        priority = new int[4] { 0x12, 0x10, 0x0e, 0x0c };
                        palette_control = 0x0a;
                        layer_enable_mask = new int[5] { 0x08, 0x10, 0x02, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 1;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "3wonders":
                    case "3wondersr1":
                    case "3wondersu":
                    case "wonder3":
                        cpsb_addr = 0x32;
                        cpsb_value = 0x0800;
                        mult_factor1 = 0x0e;
                        mult_factor2 = 0x0c;
                        mult_result_lo = 0x0a;
                        mult_result_hi = 0x08;
                        layer_control = 0x28;
                        priority = new int[4] { 0x26, 0x24, 0x22, 0x20 };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x20, 0x04, 0x08, 0x12, 0x12 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0x9a;
                        dswc = 0x99;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x5400, 0x6fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x1400, 0x3fff, 0x4000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x07ff, 0x1000));
                        lsRange2.Add(new gfx_range(0x0e00, 0x0fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x29ff, 0));
                        lsRangeS.Add(new gfx_range(0x2a00, 0x3fff, 0x4000));
                        break;
                    case "3wondersb":
                        cpsb_addr = 0x32;
                        cpsb_value = 0x0800;
                        mult_factor1 = 0x0e;
                        mult_factor2 = 0x0c;
                        mult_result_lo = 0x0a;
                        mult_result_hi = 0x08;
                        layer_control = 0x28;
                        priority = new int[4] { 0x26, 0x24, 0x22, 0x20 };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x20, 0x04, 0x08, 0x12, 0x12 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0x88;
                        dswa = 0xff;
                        dswb = 0x9a;
                        dswc = 0x99;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x5400, 0x6fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x1400, 0x3fff, 0x4000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x07ff, 0x1000));
                        lsRange2.Add(new gfx_range(0x0e00, 0x0fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x29ff, 0));
                        lsRangeS.Add(new gfx_range(0x2a00, 0x3fff, 0x4000));
                        break;
                    case "3wondersh":
                        cpsb_addr = -1;
                        cpsb_value = -1;
                        mult_factor1 = 0x0e;
                        mult_factor2 = 0x0c;
                        mult_result_lo = 0x0a;
                        mult_result_hi = 0x08;
                        layer_control = 0x28;
                        priority = new int[4] { 0x26, 0x24, 0x22, 0x20 };
                        palette_control = 0x22;
                        layer_enable_mask = new int[5] { 0x20, 0x04, 0x08, 0x12, 0x12 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0x9a;
                        dswc = 0x99;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x5400, 0x6fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x1400, 0x3fff, 0x4000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x07ff, 0x1000));
                        lsRange2.Add(new gfx_range(0x0e00, 0x0fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x29ff, 0));
                        lsRangeS.Add(new gfx_range(0x2a00, 0x3fff, 0x4000));
                        break;
                    case "kod":
                    case "kodr1":
                    case "kodu":
                    case "kodj":
                    case "kodja":
                        cpsb_addr = -1;
                        cpsb_value = -1;
                        mult_factor1 = 0x1e;
                        mult_factor2 = 0x1c;
                        mult_result_lo = 0x1a;
                        mult_result_hi = 0x18;
                        layer_control = 0x20;
                        priority = new int[4] { 0x2e, 0x2c, 0x2a, 0x28 };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x30, 0x08, 0x30, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x34;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0xc000, 0xd7ff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x4800, 0x5fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x1b00, 0x1fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x47ff, 0));
                        break;
                    case "captcomm":
                    case "captcommr1":
                    case "captcommu":
                    case "captcommj":
                    case "captcommjr1":
                        cpsb_addr = -1;
                        cpsb_value = -1;
                        mult_factor1 = 0x06;
                        mult_factor2 = 0x04;
                        mult_result_lo = 0x02;
                        mult_result_hi = 0x00;
                        layer_control = 0x20;
                        priority = new int[4] { 0x2e, 0x2c, 0x2a, 0x28 };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x20, 0x12, 0x12, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x38;
                        out2_addr = 0x34;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x8000, 0xffff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x7fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x1000, 0x1fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x7fff, 0));
                        break;
                    case "captcommb":
                        cpsb_addr = -1;
                        cpsb_value = -1;
                        mult_factor1 = 0x06;
                        mult_factor2 = 0x04;
                        mult_result_lo = 0x02;
                        mult_result_hi = 0x00;
                        layer_control = 0x20;
                        priority = new int[4] { 0x2e, 0x2c, 0x2a, 0x28 };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x20, 0x12, 0x12, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x38;
                        out2_addr = 0x34;
                        bootleg_kludge = 3;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x8000, 0xffff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x7fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x1000, 0x1fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x7fff, 0));
                        break;
                    case "knights":
                    case "knightsu":
                    case "knightsj":
                    case "knightsja":
                        cpsb_addr = -1;
                        cpsb_value = -1;
                        mult_factor1 = 0x06;
                        mult_factor2 = 0x04;
                        mult_result_lo = 0x02;
                        mult_result_hi = 0x00;
                        layer_control = 0x28;
                        priority = new int[4] { 0x26, 0x24, 0x22, 0x20 };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x20, 0x10, 0x02, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x34;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x8000, 0x9fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x67ff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x1a00, 0x1fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x67ff, 0));
                        break;
                    case "sf2ce":
                    case "sf2ceea":
                    case "sf2ceua":
                    case "sf2ceub":
                    case "sf2ceuc":
                    case "sf2ceja":
                    case "sf2cejb":
                    case "sf2cejc":
                    case "sf2bhh":
                    case "sf2rb":
                    case "sf2rb2":
                    case "sf2rb3":
                    case "sf2red":
                    case "sf2v004":
                    case "sf2acc":
                    case "sf2acca":
                    case "sf2accp2":
                    case "sf2ceblp":
                    case "sf2cebltw":
                    case "sf2dkot2":
                    case "sf2dongb":
                    case "sf2hf":
                    case "sf2hfu":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2amf2":
                    case "sf2m5":
                    case "sf2m6":
                    case "sf2m7":
                    case "sf2yyc":
                    case "sf2koryu":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 1;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2m2":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 1;
                        dswa = 0xff;
                        dswb = 0xec;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2m3":
                    case "sf2m8":
                        cpsb_addr = -1;
                        cpsb_value = -1;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x14;
                        priority = new int[4] { 0x12, 0x10, 0x0e, 0x0c };
                        palette_control = 0x0a;
                        layer_enable_mask = new int[5] { 0x0e, 0x0e, 0x0e, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 2;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2m4":
                        cpsb_addr = -1;
                        cpsb_value = -1;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x14;
                        priority = new int[4] { 0x12, 0x10, 0x0e, 0x0c };
                        palette_control = 0x0a;
                        layer_enable_mask = new int[5] { 0x0e, 0x0e, 0x0e, 0x30, 0x30 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 1;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "sf2m10":
                        cpsb_addr = -1;
                        cpsb_value = -1;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x14;
                        priority = new int[4] { 0x12, 0x10, 0x0e, 0x0c };
                        palette_control = 0x0a;
                        layer_enable_mask = new int[5] { 0x0e, 0x0e, 0x0e, 0x30, 0x30 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 1;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "cworld2j":
                    case "cworld2jb":
                        cpsb_addr = -1;
                        cpsb_value = -1;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x20;
                        priority = new int[4] { 0x2e, 0x2c, 0x2a, 0x28 };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x20, 0x14, 0x14, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x34;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfe;
                        dswc = 0xdf;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x7800, 0x7fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x37ff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0e00, 0x0eff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x37ff, 0));
                        break;
                    case "cworld2ja":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfe;
                        dswc = 0xdf;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x7800, 0x7fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x37ff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0e00, 0x0eff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x37ff, 0));
                        break;
                    case "varth":
                    case "varthr1":
                    case "varthu":
                        cpsb_addr = 0x20;
                        cpsb_value = 0x0004;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x2e;
                        priority = new int[4] { 0x26, 0x30, 0x28, 0x32 };
                        palette_control = 0x2a;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x7fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x0fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x3fff, 0));
                        break;
                    case "varthj":
                    case "varthjr":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x0e;
                        mult_factor2 = 0x0c;
                        mult_result_lo = 0x0a;
                        mult_result_hi = 0x08;
                        layer_control = 0x20;
                        priority = new int[4] { 0x2e, 0x2c, 0x2a, 0x28 };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x20, 0x04, 0x02, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x7fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x0fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x3fff, 0));
                        break;
                    case "qad":
                        cpsb_addr = -1;
                        cpsb_value = -1;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x2c;
                        priority = new int[4] { -1, -1, -1, -1 };
                        palette_control = 0x12;
                        layer_enable_mask = new int[5] { 0x14, 0x02, 0x14, 0x00, 0x00 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x3fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x1fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x07ff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x1fff, 0));
                        break;
                    case "qadjr":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x36;
                        in3_addr = 0x38;
                        out2_addr = 0x34;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xdf;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x07ff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x1000, 0x3fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0100, 0x03ff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x1000, 0x3fff, 0));
                        break;
                    case "wof":
                    case "wofu":
                    case "wofj":
                        cpsb_addr = -1;
                        cpsb_value = -1;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x22;
                        priority = new int[4] { 0x24, 0x26, 0x28, 0x2a };
                        palette_control = 0x2c;
                        layer_enable_mask = new int[5] { 0x10, 0x08, 0x04, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0xffff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x7fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x1fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0xffff, 0));
                        break;
                    case "wofr1":
                    case "wofa":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0xffff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x7fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x1fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0xffff, 0));
                        break;
                    case "wofhfh":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xec;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0xffff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x7fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x1fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0xffff, 0));
                        break;
                    case "sf2hfj":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xf4;
                        dswc = 0x9f;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x4000, 0x4fff, 0x10000));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2800, 0x3fff, 0x8000));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0400, 0x07ff, 0x2000));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x8fff, 0));
                        break;
                    case "dino":
                    case "dinou":
                    case "dinoj":
                        cpsb_addr = -1;
                        cpsb_value = -1;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x0a;
                        priority = new int[4] { 0x0c, 0x0e, 0x00, 0x02 };
                        palette_control = 0x04;
                        layer_enable_mask = new int[5] { 0x16, 0x16, 0x16, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x0fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x4000, 0x6fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x1c00, 0x1fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0800, 0x6fff, 0));
                        break;
                    case "dinohunt":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xdc;
                        dswc = 0x9e;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x0fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x4000, 0x6fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x1c00, 0x1fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0800, 0x6fff, 0));
                        break;
                    case "punisher":
                    case "punisheru":
                    case "punisherh":
                    case "punisherj":
                        cpsb_addr = 0x0e;
                        cpsb_value = 0x0c00;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x12;
                        priority = new int[4] { 0x14, 0x16, 0x08, 0x0a };
                        palette_control = 0x0c;
                        layer_enable_mask = new int[5] { 0x04, 0x02, 0x20, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x0fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x4000, 0x6dff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x1b80, 0x1fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0800, 0x6dff, 0));
                        break;
                    case "punisherbz":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xef;
                        dswb = 0x94;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x0fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x4000, 0x6dff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x1b80, 0x1fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0800, 0x6dff, 0));
                        break;
                    case "slammast":
                    case "slammastu":
                    case "mbomberj":
                        cpsb_addr = 0x2e;
                        cpsb_value = 0x0c01;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x16;
                        priority = new int[4] { 0x00, 0x02, 0x28, 0x2a };
                        palette_control = 0x2c;
                        layer_enable_mask = new int[5] { 0x04, 0x08, 0x10, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x0fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0800, 0xb3ff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x2d00, 0x2fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0800, 0xb3ff, 0));
                        break;
                    case "mbombrd":
                    case "mbombrdj":
                        cpsb_addr = 0x1e;
                        cpsb_value = 0x0c02;
                        mult_factor1 = -1;
                        mult_factor2 = -1;
                        mult_result_lo = -1;
                        mult_result_hi = -1;
                        layer_control = 0x2a;
                        priority = new int[4] { 0x2c, 0x2e, 0x30, 0x32 };
                        palette_control = 0x1c;
                        layer_enable_mask = new int[5] { 0x04, 0x08, 0x10, 0x00, 0x00 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x0fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0800, 0xb3ff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x2d00, 0x2fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0800, 0xb3ff, 0));
                        break;
                    case "pnickj":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0xdf;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x0fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0800, 0x2fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0c00, 0x0fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0800, 0x2fff, 0));
                        break;
                    case "qtono2j":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x36;
                        in3_addr = 0x38;
                        out2_addr = 0x34;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfc;
                        dswc = 0xdf;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x0fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2000, 0x7fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0200, 0x07ff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0800, 0x2fff, 0));
                        break;
                    case "megaman":
                    case "megamana":
                    case "rockmanj":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xfe;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x1ffff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0xffff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x3fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0xffff, 0));
                        break;
                    case "pang3":
                    case "pang3r1":
                    case "pang3j":
                    case "pang3b":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0xa000, 0xbfff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x4fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x1800, 0x1fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x4fff, 0));
                        break;
                    case "pokonyan":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x36;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xbe;
                        dswb = 0xfb;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x7000, 0x7fff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x2000, 0x37ff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0600, 0x07ff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x17ff, 0));
                        break;
                    case "wofch":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0xffff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0x7fff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x1fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0x7fff, 0));
                        break;
                    case "sfzch":
                    case "sfach":
                    case "sfzbch":
                        cpsb_addr = 0x32;
                        cpsb_value = -1;
                        mult_factor1 = 0x00;
                        mult_factor2 = 0x02;
                        mult_result_lo = 0x04;
                        mult_result_hi = 0x06;
                        layer_control = 0x26;
                        priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                        palette_control = 0x30;
                        layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                        in2_addr = 0x00;
                        in3_addr = 0x00;
                        out2_addr = 0x00;
                        bootleg_kludge = 0;
                        dswa = 0xff;
                        dswb = 0xff;
                        dswc = 0xff;
                        lsRange0 = new List<gfx_range>();
                        lsRange0.Add(new gfx_range(0x0000, 0x1ffff, 0));
                        lsRange1 = new List<gfx_range>();
                        lsRange1.Add(new gfx_range(0x0000, 0xffff, 0));
                        lsRange2 = new List<gfx_range>();
                        lsRange2.Add(new gfx_range(0x0000, 0x3fff, 0));
                        lsRangeS = new List<gfx_range>();
                        lsRangeS.Add(new gfx_range(0x0000, 0xffff, 0));
                        break;
                }
                if (cps_version == 2)
                {
                    cpsb_addr = 0x32;
                    cpsb_value = -1;
                    mult_factor1 = 0x00;
                    mult_factor2 = 0x02;
                    mult_result_lo = 0x04;
                    mult_result_hi = 0x06;
                    layer_control = 0x26;
                    priority = new int[4] { 0x28, 0x2a, 0x2c, 0x2e };
                    palette_control = 0x30;
                    layer_enable_mask = new int[5] { 0x02, 0x04, 0x08, 0x30, 0x30 };
                    in2_addr = 0x00;
                    in3_addr = 0x00;
                    out2_addr = 0x00;
                    bootleg_kludge = 0;
                    lsRange0 = new List<gfx_range>();
                    lsRange0.Add(new gfx_range(0x0000, 0x1ffff, 0x20000));
                    lsRange1 = new List<gfx_range>();
                    lsRange1.Add(new gfx_range(0x0000, 0xffff, 0x10000));
                    lsRange2 = new List<gfx_range>();
                    lsRange2.Add(new gfx_range(0x0000, 0x3fff, 0x4000));
                    lsRangeS = new List<gfx_range>();
                    lsRangeS.Add(new gfx_range(0x0000, 0xffff, 0));
                }
            }
        }
        public static sbyte cps1_dsw_r(int offset)
        {
            string[] dswname = { "IN0", "DSWA", "DSWB", "DSWC" };
            int in0 = 0;
            if (offset == 0)
            {
                in0 = sbyte0;
            }
            else if (offset == 1)
            {
                in0 = dswa;
            }
            else if (offset == 2)
            {
                in0 = dswb;
            }
            else if (offset == 3)
            {
                in0 = dswc;
            }
            else
            {
                in0 = 0;
            }
            return (sbyte)in0;
        }
        public static void cps1_snd_bankswitch_w(byte data)
        {
            int bankaddr;
            bankaddr = ((data & 1) * 0x4000);
            basebanksnd = 0x10000 + bankaddr;
        }
        public static void cps1_oki_pin7_w(byte data)
        {
            OKI6295.okim6295_set_pin7(data & 1);
        }
        public static void cps1_coinctrl_w(ushort data)
        {
            Generic.coin_counter_w(0, data & 0x0100);
            Generic.coin_counter_w(1, data & 0x0200);
            Generic.coin_lockout_w(0, ~data & 0x0400);
            Generic.coin_lockout_w(1, ~data & 0x0800);
        }
        public static void qsound_banksw_w(byte data)
        {
            basebanksnd = 0x10000 + ((data & 0x0f) * 0x4000);
        }
        public static short qsound_rom_r(int offset)
        {
            if (user1rom != null)
            {
                return (short)(user1rom[offset] | 0xff00);
            }
            else
            {
                return 0;
            }
        }
        public static short qsound_sharedram1_r(int offset)
        {
            return (short)(qsound_sharedram1[offset] | 0xff00);
        }
        public static void qsound_sharedram1_w(int offset, byte data)
        {
            qsound_sharedram1[offset] = (byte)data;
        }
        public static short qsound_sharedram2_r(int offset)
        {
            return (short)(qsound_sharedram2[offset] | 0xff00);
        }
        public static void qsound_sharedram2_w(int offset, byte data)
        {
            qsound_sharedram2[offset] = (byte)(data);
        }
        public static void cps1_interrupt()
        {
            Cpuint.cpunum_set_input_line(0, 2, LineState.HOLD_LINE);
        }
        public static void cpsq_coinctrl2_w(ushort data)
        {
            Generic.coin_counter_w(2, data & 0x01);
            Generic.coin_lockout_w(2, ~data & 0x02);
            Generic.coin_counter_w(3, data & 0x04);
            Generic.coin_lockout_w(3, ~data & 0x08);
        }
        public static int cps1_eeprom_port_r()
        {
            return Eeprom.eeprom_read_bit();
        }
        public static void cps1_eeprom_port_w(int data)
        {
            /*
            bit 0 = data
            bit 6 = clock
            bit 7 = cs
            */
            Eeprom.eeprom_write_bit(data & 0x01);
            Eeprom.eeprom_set_cs_line(((data & 0x80) != 0) ? LineState.CLEAR_LINE : LineState.ASSERT_LINE);
            Eeprom.eeprom_set_clock_line(((data & 0x40) != 0) ? LineState.ASSERT_LINE : LineState.CLEAR_LINE);
        }
        public static void sf2m3_layer_w(ushort data)
        {
            cps1_cps_b_w(0x0a, data);
        }
        public static short cps2_objram2_r(int offset)
        {
            if ((cps2_objram_bank & 1) != 0)
            {
                return (short)cps2_objram1[offset];
            }
            else
            {
                return (short)cps2_objram2[offset];
            }
        }
        public static void cps2_objram1_w(int offset, ushort data)
        {
            if ((cps2_objram_bank & 1) != 0)
            {
                cps2_objram2[offset] = data;
            }
            else
            {
                cps2_objram1[offset] = data;
            }
        }
        public static void cps2_objram2_w(int offset, ushort data)
        {
            if ((cps2_objram_bank & 1) != 0)
            {
                cps2_objram1[offset] = data;
            }
            else
            {
                cps2_objram2[offset] = data;
            }
        }
        public static short cps2_qsound_volume_r()
        {
            if (cps2networkpresent != 0)
            {
                return (short)0x2021;
            }
            else
            {
                return unchecked((short)(0xe021));
            }
        }
        public static short kludge_r()
        {
            return -1;
        }
        public static void cps2_eeprom_port_bh(int data)
        {
            data = (data & 0xff) << 8;
            Eeprom.eeprom_write_bit(data & 0x1000);
            Eeprom.eeprom_set_clock_line(((data & 0x2000) != 0) ? LineState.ASSERT_LINE : LineState.CLEAR_LINE);
            Eeprom.eeprom_set_cs_line(((data & 0x4000) != 0) ? LineState.CLEAR_LINE : LineState.ASSERT_LINE);
        }
        public static void cps2_eeprom_port_bl(int data)
        {
            Cpuint.cpunum_set_input_line(1, (int)LineState.INPUT_LINE_RESET, ((data & 0x0008) != 0) ? LineState.CLEAR_LINE : LineState.ASSERT_LINE);
            Generic.coin_counter_w(0, data & 0x0001);
            Generic.coin_counter_w(1, data & 0x0002);
            Generic.coin_lockout_w(0, ~data & 0x0010);
            Generic.coin_lockout_w(1, ~data & 0x0020);
            Generic.coin_lockout_w(2, ~data & 0x0040);
            Generic.coin_lockout_w(3, ~data & 0x0080);
        }
        public static void cps2_eeprom_port_w(int data)
        {
            //high 8 bits
            {
                /* bit 0 - Unused */
                /* bit 1 - Unused */
                /* bit 2 - Unused */
                /* bit 3 - Unused? */
                /* bit 4 - Eeprom data  */
                /* bit 5 - Eeprom clock */
                /* bit 6 - */
                /* bit 7 - */

                /* EEPROM */
                Eeprom.eeprom_write_bit(data & 0x1000);
                Eeprom.eeprom_set_clock_line(((data & 0x2000)!=0) ? LineState.ASSERT_LINE : LineState.CLEAR_LINE);
                Eeprom.eeprom_set_cs_line(((data & 0x4000)!=0) ? LineState.CLEAR_LINE : LineState.ASSERT_LINE);
            }
            //low 8 bits
            {
                /* bit 0 - coin counter 1 */
                /* bit 0 - coin counter 2 */
                /* bit 2 - Unused */
                /* bit 3 - Allows access to Z80 address space (Z80 reset) */
                /* bit 4 - lock 1  */
                /* bit 5 - lock 2  */
                /* bit 6 - */
                /* bit 7 - */

                /* Z80 Reset */
                Cpuint.cpunum_set_input_line(1, (int)LineState.INPUT_LINE_RESET, ((data & 0x0008) != 0) ? LineState.CLEAR_LINE : LineState.ASSERT_LINE);

                Generic.coin_counter_w(0, data & 0x0001);                
                Generic.coin_counter_w(1, data & 0x0002);
                
                Generic.coin_lockout_w(0, ~data & 0x0010);
                Generic.coin_lockout_w(1, ~data & 0x0020);
                Generic.coin_lockout_w(2, ~data & 0x0040);
                Generic.coin_lockout_w(3, ~data & 0x0080);

                /*
                set_led_status(0,data & 0x01);
                set_led_status(1,data & 0x10);
                set_led_status(2,data & 0x20);
                */
            }
        }
        public static void cps2_objram_bank_w(int data)
        {
            cps2_objram_bank = data & 1;
        }
        public static void cps2_interrupt()
        {
            /* 2 is vblank, 4 is some sort of scanline interrupt, 6 is both at the same time. */
            if (scancount >= 261)
            {
                scancount = -1;
                cps1_scancalls = 0;
            }
            scancount++;
            if ((cps_b_regs[0x10 / 2] & 0x8000) != 0)
            {
                cps_b_regs[0x10 / 2] = (ushort)(cps_b_regs[0x10 / 2] & 0x1ff);
            }
            if ((cps_b_regs[0x12 / 2] & 0x8000) != 0)
            {
                cps_b_regs[0x12 / 2] = (ushort)(cps_b_regs[0x12 / 2] & 0x1ff);
            }
            /*if(cps1_scanline1 == scancount || (cps1_scanline1 < scancount && (cps1_scancalls!=0)))
            {
                CPS1.cps1_cps_b_regs[0x10 / 2] = 0;

                cpunum_set_input_line(machine, 0, 4, HOLD_LINE);
                cps2_set_sprite_priorities();
                video_screen_update_partial(machine->primary_screen, 16 - 10 + scancount);
                cps1_scancalls++;
            }
            if(cps1_scanline2 == scancount || (cps1_scanline2 < scancount && !cps1_scancalls))
            {
                cps1_cps_b_regs[0x12/2] = 0;
                cpunum_set_input_line(machine, 0, 4, HOLD_LINE);
                cps2_set_sprite_priorities();
                video_screen_update_partial(machine->primary_screen, 16 - 10 + scancount);
                cps1_scancalls++;
            }*/
            if (scancount == 256)  /* VBlank */
            {
                cps_b_regs[0x10 / 2] = (ushort)cps1_scanline1;
                cps_b_regs[0x12 / 2] = (ushort)cps1_scanline2;
                Cpuint.cpunum_set_input_line(0, 2, LineState.HOLD_LINE);
                if (cps1_scancalls != 0)
                {
                    cps2_set_sprite_priorities();
                    //video_screen_update_partial(machine->primary_screen, 256);
                }
                cps2_objram_latch();
            }
        }
        public static void machine_reset_cps()
        {
            basebanksnd = 0;
        }
    }
}

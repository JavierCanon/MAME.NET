using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cpu.z80;

namespace mame
{
    public partial class Tehkan
    {
        public static byte dsw1, dsw2;
        public static byte[] mainromop, gfx1rom, gfx2rom, gfx3rom, gfx32rom;
        public static void PbactionInit()
        {
            int i,n;
            Machine.bRom = true;
            switch (Machine.sName)
            {
                case "pbaction":
                case "pbaction2":
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    gfx1rom = Machine.GetRom("gfx1.rom");
                    gfx2rom = Machine.GetRom("gfx2.rom");
                    gfx3rom = Machine.GetRom("gfx3.rom");
                    gfx32rom = Machine.GetRom("gfx32.rom");
                    Memory.mainram = new byte[0x1000];
                    Memory.audioram = new byte[0x800];
                    Generic.videoram = new byte[0x400];
                    pbaction_videoram2 = new byte[0x400];
                    Generic.colorram = new byte[0x400];
                    pbaction_colorram2 = new byte[0x400];
                    Generic.spriteram = new byte[0x80];
                    Generic.paletteram = new byte[0x200];                  
                    if (Memory.mainrom == null || Memory.audiorom == null || gfx1rom == null || gfx2rom == null || gfx3rom == null || gfx32rom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "pbaction3":
                case "pbaction4":
                case "pbaction5":
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    mainromop = Machine.GetRom("maincpuop.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    gfx1rom = Machine.GetRom("gfx1.rom");
                    gfx2rom = Machine.GetRom("gfx2.rom");
                    gfx3rom = Machine.GetRom("gfx3.rom");
                    gfx32rom = Machine.GetRom("gfx32.rom");
                    Memory.mainram = new byte[0x1000];
                    Memory.audioram = new byte[0x800];
                    Generic.videoram = new byte[0x400];
                    pbaction_videoram2 = new byte[0x400];
                    Generic.colorram = new byte[0x400];
                    pbaction_colorram2 = new byte[0x400];
                    Generic.spriteram = new byte[0x80];
                    Generic.paletteram = new byte[0x200];
                    if (Memory.mainrom == null || mainromop == null || Memory.audiorom == null || gfx1rom == null || gfx2rom == null || gfx3rom == null || gfx32rom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
            }
            if (Machine.bRom)
            {
                switch (Machine.sName)
                {
                    case "pbaction":
                    case "pbaction2":
                    case "pbaction3":
                    case "pbaction4":
                    case "pbaction5":
                        dsw1 = 0x40;
                        dsw2 = 0x00;
                        break;
                }
            }
        }
        public static void pbaction_sh_command_w(byte data)
        {
            Sound.soundlatch_w(data);
            Cpuint.cpunum_set_input_line_and_vector2(1, 0, LineState.HOLD_LINE, 0);
        }
        public static void pbaction_interrupt()
        {
            Cpuint.cpunum_set_input_line_and_vector2(1, 0, LineState.HOLD_LINE, 0x02);
        }
        public static byte pbaction3_prot_kludge_r()
        {
            byte result;
            if (Z80A.zz1[0].PC == 0xab80)
            {
                result = 0;
            }
            else
            {
                result = Memory.mainram[0];
            }
            return result;
        }
        public static void machine_reset_tehkan()
        {
            
        }
    }
}

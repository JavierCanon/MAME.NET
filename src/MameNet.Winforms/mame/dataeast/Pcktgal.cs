using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mame
{
    public partial class Dataeast
    {
        public static byte[] audioromop, gfx1rom, gfx2rom, gfx12rom, gfx22rom, prom;
        public static byte dsw;
        public static int basebankmain1, basebankmain2, basebanksnd, msm5205next, toggle;
        public static void DataeastInit()
        {
            int i,n;
            Machine.bRom = true;
            Memory.mainram = new byte[0x800];
            Memory.audioram = new byte[0x800];
            Generic.spriteram = new byte[0x200];
            Generic.videoram = new byte[0x800];
            switch (Machine.sName)
            {
                case "pcktgal":
                case "pcktgalb":
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    audioromop = Machine.GetRom("audiocpuop.rom");
                    gfx1rom = Machine.GetRom("gfx1.rom");
                    gfx2rom = Machine.GetRom("gfx2.rom");
                    prom = Machine.GetRom("proms.rom");
                    if (Memory.mainrom == null || Memory.audiorom == null || audioromop == null || gfx1rom == null || gfx2rom == null || prom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
                case "pcktgal2":
                case "pcktgal2j":
                case "spool3":
                case "spool3i":
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    gfx1rom = Machine.GetRom("gfx1.rom");
                    gfx2rom = Machine.GetRom("gfx2.rom");
                    prom = Machine.GetRom("proms.rom");
                    if (Memory.mainrom == null || Memory.audiorom == null || gfx1rom == null || gfx2rom == null || prom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
            }            
            if (Machine.bRom)
            {
                dsw = 0xbf;
            }
        }
        public static void irqhandler(int irq)
        {

        }
        public static void pcktgal_bank_w(byte data)
        {
            if ((data & 1) != 0)
            {
                basebankmain1 = 0x4000;
            }
            else
            {
                basebankmain1 = 0x10000;
            }
            if ((data & 2) != 0)
            {
                basebankmain2 = 0x6000;
            }
            else
            {
                basebankmain2 = 0x12000;
            }
        }
        public static void pcktgal_sound_bank_w(byte data)
        {
            basebanksnd = 0x10000 + 0x4000 * ((data >> 2) & 1);
        }
        public static void pcktgal_sound_w(byte data)
        {
            Sound.soundlatch_w(data);
            Cpuint.cpunum_set_input_line(1, (int)LineState.INPUT_LINE_NMI, LineState.PULSE_LINE);
        }
        public static void pcktgal_adpcm_int(int data)
        {
            MSM5205.msm5205_data_w(0, msm5205next >> 4);
            msm5205next <<= 4;
            toggle = 1 - toggle;
            if (toggle != 0)
            {
                Cpuint.cpunum_set_input_line(1, 0, LineState.HOLD_LINE);
            }
        }
        public static void pcktgal_adpcm_data_w(byte data)
        {
            msm5205next = data;
        }
        public static byte pcktgal_adpcm_reset_r()
        {
            MSM5205.msm5205_reset_w(0, 0);
            return 0;
        }
        public static void machine_reset_dataeast()
        {
            basebankmain1 = 0;
            basebankmain2 = 0;
            basebanksnd = 0;
            msm5205next = 0;
            toggle = 0;
        }
    }
}

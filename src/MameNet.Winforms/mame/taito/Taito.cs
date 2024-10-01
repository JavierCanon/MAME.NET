using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cpu.m68000;

namespace mame
{
    public partial class Taito
    {
        public static int basebankmain, basebanksnd;
        public static byte[] bb1, bublbobl_mcu_sharedram, videoram, bublbobl_objectram, slaverom, mcurom, mcuram, mainram2, mainram3, subrom;
        public static void TaitoInit()
        {
            int i, n;
            Machine.bRom = true;
            switch (Machine.sName)
            {
                case "tokio":
                case "tokioo":
                case "tokiou":
                case "tokiob":
                    videoram = new byte[0x1d00];
                    bublbobl_objectram = new byte[0x300];
                    Memory.mainram = new byte[0x1800];
                    Memory.audioram = new byte[0x1000];
                    Generic.paletteram = new byte[0x200];
                    //bublbobl_mcu_sharedram = new byte[0x400];
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    slaverom = Machine.GetRom("slave.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    gfx12rom = Machine.GetRom("gfx1.rom");
                    n = gfx12rom.Length;
                    gfx1rom = new byte[n * 2];
                    for (i = 0; i < n; i++)
                    {
                        gfx1rom[i * 2] = (byte)(gfx12rom[i] >> 4);
                        gfx1rom[i * 2 + 1] = (byte)(gfx12rom[i] & 0x0f);
                    }
                    prom = Machine.GetRom("proms.rom");
                    bublbobl_video_enable = 1;
                    if (Memory.mainrom == null || slaverom == null || Memory.audiorom == null || gfx1rom == null || prom == null)
                    {
                        Machine.bRom = false;
                    }
                    if (Machine.bRom)
                    {
                        dsw0 = 0xfe;
                        dsw1 = 0x7e;
                    }
                    break;
                case "bublbobl":
                case "bublbobl1":
                case "bublboblr":
                case "bublboblr1":
                case "bub68705":
                case "bublcave":
                case "bublcave11":
                case "bublcave10":
                    videoram = new byte[0x1d00];
                    bublbobl_objectram = new byte[0x300];
                    Memory.mainram = new byte[0x1800];
                    Memory.audioram = new byte[0x1000];
                    mcuram = new byte[0xc0];
                    Generic.paletteram = new byte[0x200];
                    bublbobl_mcu_sharedram = new byte[0x400];
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    slaverom = Machine.GetRom("slave.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    mcurom = Machine.GetRom("mcu.rom");
                    gfx12rom = Machine.GetRom("gfx1.rom");
                    n = gfx12rom.Length;
                    gfx1rom = new byte[n * 2];
                    for (i = 0; i < n; i++)
                    {
                        gfx1rom[i * 2] = (byte)(gfx12rom[i] >> 4);
                        gfx1rom[i * 2 + 1] = (byte)(gfx12rom[i] & 0x0f);
                    }
                    prom = Machine.GetRom("proms.rom");
                    bublbobl_video_enable = 0;
                    if (Memory.mainrom == null || slaverom == null || Memory.audiorom == null || mcurom == null || gfx1rom == null || prom == null)
                    {
                        Machine.bRom = false;
                    }
                    if (Machine.bRom)
                    {
                        dsw0 = 0xfe;
                        dsw1 = 0xff;
                    }
                    break;
                case "boblbobl":
                case "sboblbobl":
                case "sboblbobla":
                case "sboblboblb":
                case "sboblbobld":
                case "sboblboblc":
                case "dland":
                case "bbredux":
                    mainram2 = new byte[0x100];
                    mainram3 = new byte[0x100];
                    videoram = new byte[0x1d00];
                    bublbobl_objectram = new byte[0x300];
                    Memory.mainram = new byte[0x1800];
                    Memory.audioram = new byte[0x1000];
                    Generic.paletteram = new byte[0x200];
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    slaverom = Machine.GetRom("slave.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    gfx12rom = Machine.GetRom("gfx1.rom");
                    n = gfx12rom.Length;
                    gfx1rom = new byte[n * 2];
                    for (i = 0; i < n; i++)
                    {
                        gfx1rom[i * 2] = (byte)(gfx12rom[i] >> 4);
                        gfx1rom[i * 2 + 1] = (byte)(gfx12rom[i] & 0x0f);
                    }
                    prom = Machine.GetRom("proms.rom");
                    bublbobl_video_enable = 0;
                    if (Memory.mainrom == null || slaverom == null || Memory.audiorom == null || gfx1rom == null || prom == null)
                    {
                        Machine.bRom = false;
                    }
                    if (Machine.bRom)
                    {
                        dsw0 = 0xfe;
                        dsw1 = 0x3f;
                    }
                    break;
                case "bublboblb":
                case "boblcave":
                    mainram2 = new byte[0x100];
                    mainram3 = new byte[0x100];
                    videoram = new byte[0x1d00];
                    bublbobl_objectram = new byte[0x300];
                    Memory.mainram = new byte[0x1800];
                    Memory.audioram = new byte[0x1000];
                    Generic.paletteram = new byte[0x200];
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    slaverom = Machine.GetRom("slave.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    gfx12rom = Machine.GetRom("gfx1.rom");
                    n = gfx12rom.Length;
                    gfx1rom = new byte[n * 2];
                    for (i = 0; i < n; i++)
                    {
                        gfx1rom[i * 2] = (byte)(gfx12rom[i] >> 4);
                        gfx1rom[i * 2 + 1] = (byte)(gfx12rom[i] & 0x0f);
                    }
                    prom = Machine.GetRom("proms.rom");
                    bublbobl_video_enable = 0;
                    if (Memory.mainrom == null || slaverom == null || Memory.audiorom == null || gfx1rom == null || prom == null)
                    {
                        Machine.bRom = false;
                    }
                    if (Machine.bRom)
                    {
                        dsw0 = 0xfe;
                        dsw1 = 0xc0;
                    }
                    break;
                case "opwolf":
                case "opwolfa":
                case "opwolfj":
                case "opwolfu":
                    mainram2 = new byte[0x10000];
                    cchip_ram = new byte[0x2000];
                    Generic.paletteram16 = new ushort[0x800];
                    Memory.mainram = new byte[0x8000];
                    Memory.audioram = new byte[0x1000];
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    bb1 = Machine.GetRom("audiocpu.rom");
                    Memory.audiorom = new byte[0x20000];
                    Array.Copy(bb1, 0, Memory.audiorom, 0, 0x10000);
                    gfx12rom = Machine.GetRom("gfx1.rom");
                    n = gfx12rom.Length;
                    gfx1rom = new byte[n * 2];
                    for (i = 0; i < n; i++)
                    {
                        gfx1rom[i * 2] = (byte)(gfx12rom[i] >> 4);
                        gfx1rom[i * 2 + 1] = (byte)(gfx12rom[i] & 0x0f);
                    }
                    gfx22rom = Machine.GetRom("gfx2.rom");
                    n = gfx22rom.Length;
                    gfx2rom = new byte[n * 2];
                    for (i = 0; i < n; i++)
                    {
                        gfx2rom[i * 2] = (byte)(gfx22rom[i] >> 4);
                        gfx2rom[i * 2 + 1] = (byte)(gfx22rom[i] & 0x0f);
                    }
                    adpcmrom = Machine.GetRom("adpcm.rom");
                    Taitosnd.taitosnd_start();                    
                    if (Memory.mainrom == null || Memory.audiorom == null || gfx1rom == null || gfx2rom == null || adpcmrom == null)
                    {
                        Machine.bRom = false;
                    }
                    if (Machine.bRom)
                    {
                        dswa = 0xff;
                        dswb = 0x7f;
                    }
                    break;
                case "opwolfb":
                    mainram2 = new byte[0x10000];
                    cchip_ram = new byte[0x2000];
                    Generic.paletteram16 = new ushort[0x800];
                    Memory.mainram = new byte[0x8000];
                    Memory.audioram = new byte[0x1000];
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    bb1 = Machine.GetRom("audiocpu.rom");
                    Memory.audiorom = new byte[0x20000];
                    Array.Copy(bb1, 0, Memory.audiorom, 0, 0x10000);
                    subrom = Machine.GetRom("sub.rom");
                    gfx12rom = Machine.GetRom("gfx1.rom");
                    n = gfx12rom.Length;
                    gfx1rom = new byte[n * 2];
                    for (i = 0; i < n; i++)
                    {
                        gfx1rom[i * 2] = (byte)(gfx12rom[i] >> 4);
                        gfx1rom[i * 2 + 1] = (byte)(gfx12rom[i] & 0x0f);
                    }
                    gfx22rom = Machine.GetRom("gfx2.rom");
                    n = gfx22rom.Length;
                    gfx2rom = new byte[n * 2];
                    for (i = 0; i < n; i++)
                    {
                        gfx2rom[i * 2] = (byte)(gfx22rom[i] >> 4);
                        gfx2rom[i * 2 + 1] = (byte)(gfx22rom[i] & 0x0f);
                    }
                    adpcmrom = Machine.GetRom("adpcm.rom");
                    Taitosnd.taitosnd_start();
                    if (Memory.mainrom == null || Memory.audiorom == null || subrom == null || gfx1rom == null || gfx2rom == null || adpcmrom == null)
                    {
                        Machine.bRom = false;
                    }
                    if (Machine.bRom)
                    {
                        dswa = 0xff;
                        dswb = 0xff;
                    }
                    break;
                case "opwolfp":
                    mainram2 = new byte[0x10000];
                    cchip_ram = new byte[0x2000];
                    Generic.paletteram16 = new ushort[0x800];
                    Memory.mainram = new byte[0x8000];
                    Memory.audioram = new byte[0x1000];
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    bb1 = Machine.GetRom("audiocpu.rom");
                    Memory.audiorom = new byte[0x20000];
                    Array.Copy(bb1, 0, Memory.audiorom, 0, 0x10000);
                    gfx12rom = Machine.GetRom("gfx1.rom");
                    n = gfx12rom.Length;
                    gfx1rom = new byte[n * 2];
                    for (i = 0; i < n; i++)
                    {
                        gfx1rom[i * 2] = (byte)(gfx12rom[i] >> 4);
                        gfx1rom[i * 2 + 1] = (byte)(gfx12rom[i] & 0x0f);
                    }
                    gfx22rom = Machine.GetRom("gfx2.rom");
                    n = gfx22rom.Length;
                    gfx2rom = new byte[n * 2];
                    for (i = 0; i < n; i++)
                    {
                        gfx2rom[i * 2] = (byte)(gfx22rom[i] >> 4);
                        gfx2rom[i * 2 + 1] = (byte)(gfx22rom[i] & 0x0f);
                    }
                    adpcmrom = Machine.GetRom("adpcm.rom");
                    Taitosnd.taitosnd_start();
                    if (Memory.mainrom == null || Memory.audiorom == null || gfx1rom == null || gfx2rom == null || adpcmrom == null)
                    {
                        Machine.bRom = false;
                    }
                    if (Machine.bRom)
                    {
                        dswa = 0xff;
                        dswb = 0xff;
                    }
                    break;
            }
        }
        public static void machine_reset_null()
        {

        }
        public static void irqhandler(int irq)
        {
            Cpuint.cpunum_set_input_line(2, 0, irq != 0 ? LineState.ASSERT_LINE : LineState.CLEAR_LINE);
        }
        public static void driver_init_opwolf()
        {
            opwolf_region = Memory.mainrom[0x03ffff];
            opwolf_cchip_init();
            opwolf_gun_xoffs = 0xec - Memory.mainrom[0x03ffb1];
            opwolf_gun_yoffs = 0x1c - Memory.mainrom[0x03ffaf];
            basebanksnd = 0x10000;
        }
        public static void driver_init_opwolfb()
        {
            opwolf_region = Memory.mainrom[0x03ffff];
            opwolf_gun_xoffs = -2;
            opwolf_gun_yoffs = 17;
            basebanksnd = 0x10000;
        }
        public static void driver_init_opwolfp()
        {
            opwolf_region = Memory.mainrom[0x03ffff];
            opwolf_gun_xoffs = 5;
            opwolf_gun_yoffs = 30;
            basebanksnd = 0x10000;
        }
    }
}

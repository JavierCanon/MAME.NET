using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using cpu.m68000;

namespace mame
{
    public partial class SunA8
    {
        public static byte m_rombank, m_spritebank, m_palettebank, spritebank_latch;
        public static byte suna8_unknown;
        public static byte m_gfxbank;
        public static int m_has_text;
        public static GFXBANK_TYPE m_gfxbank_type;
        public static byte dsw1, dsw2, dswcheat;
        public static byte m_rombank_latch, m_nmi_enable;
        public static byte[] mainromop, gfx1rom, gfx12rom, samplesrom;
        public static int basebankmain;
        public static short[] samplebuf, samplebuf2;
        public static int sample;
        public static int sample_offset;
        public static void SunA8Init()
        {
            int i, n;
            Machine.bRom = true;
            switch (Machine.sName)
            {
                case "starfigh":
                    Generic.spriteram = new byte[0x4000];
                    mainromop = Machine.GetRom("maincpuop.rom");
                    Memory.mainrom = Machine.GetRom("maincpu.rom");
                    Memory.audiorom = Machine.GetRom("audiocpu.rom");
                    samplesrom = Machine.GetRom("samples.rom");
                    gfx12rom = Machine.GetRom("gfx1.rom");
                    n = gfx12rom.Length;
                    gfx1rom = new byte[n * 2];
                    for (i = 0; i < n; i++)
                    {
                        gfx1rom[i * 2] = (byte)(gfx12rom[i] >> 4);
                        gfx1rom[i * 2 + 1] = (byte)(gfx12rom[i] & 0x0f);
                    }
                    Memory.mainram = new byte[0x1800];
                    Memory.audioram = new byte[0x800];
                    Generic.paletteram = new byte[0x200];
                    if (mainromop == null || Memory.mainrom == null || Memory.audiorom == null || samplesrom == null || gfx12rom == null)
                    {
                        Machine.bRom = false;
                    }
                    break;
            }
            if (Machine.bRom)
            {
                switch (Machine.sName)
                {
                    case "starfigh":
                        dsw1 = 0x5f;
                        dsw2 = 0xff;
                        dswcheat = 0xbf;
                        Sample.info.starthandler = suna8_sh_start;
                        break;
                }
            }
        }
        public static void hardhea2_flipscreen_w(byte data)
        {
            Generic.flip_screen_set(data & 0x01);
        }
        public static void starfigh_leds_w(byte data)
        {
            int bank;
            //set_led_status(0, data & 0x01);
            //set_led_status(1, data & 0x02);
            Generic.coin_counter_w(0, data & 0x04);
            m_gfxbank = (byte)((data & 0x08) != 0 ? 4 : 0);
            bank = m_rombank_latch & 0x0f;
            basebankmain = 0x10000 + bank * 0x4000;
            //memory_set_bank(1,bank);
            m_rombank = m_rombank_latch;
        }
        public static void starfigh_rombank_latch_w(byte data)
        {
            m_rombank_latch = data;
        }
        public static void starfigh_sound_latch_w(byte data)
        {
            if ((m_rombank_latch & 0x20) == 0)
            {
                Sound.soundlatch_w(data);
            }
        }
        public static byte starfigh_cheats_r()
        {
            byte b1 = dswcheat;
            if (Video.video_screen_get_vblank())
            {
                b1 = (byte)(dswcheat | 0x40);
            }
            return b1;
        }
        public static void hardhea2_interrupt()
        {
            switch (Cpuexec.iloops)
            {
                case 240:
                    Cpuint.cpunum_set_input_line(0, 0, LineState.HOLD_LINE);
                    break;
                case 112:
                    if (m_nmi_enable != 0)
                    {
                        Cpuint.cpunum_set_input_line(0, (int)LineState.INPUT_LINE_NMI, LineState.PULSE_LINE);
                    }
                    break;
            }
        }
        public static void starfigh_spritebank_latch_w(byte data)
        {
            spritebank_latch = (byte)((data >> 2) & 1);
            m_nmi_enable = (byte)((data >> 5) & 1);
        }
        public static void starfigh_spritebank_w()
        {
            m_spritebank = spritebank_latch;
        }
        public static void suna8_play_samples_w(int offset, byte data)
        {
            if (data != 0)
            {
                if ((~data & 0x10) != 0)
                {
                    sample_offset = 0x800 * sample;
                    if (sample_offset == 0x3000)
                    {
                        int i1 = 1;
                    }
                    Array.Copy(samplebuf, 0x800 * sample, samplebuf2, 0, 0x800);
                    Sample.sample_start_raw(0, samplebuf2, 0x0800, 4000, 0);
                }
                else if ((~data & 0x08) != 0)
                {
                    sample &= 3;
                    sample_offset = 0x800 * (sample + 7);
                    Array.Copy(samplebuf, 0x800 * (sample + 7), samplebuf2, 0, 0x800);
                    Sample.sample_start_raw(0, samplebuf2, 0x0800, 4000, 0);
                }
            }
        }
        public static void suna8_samples_number_w(int offset, byte data)
        {
            sample = data & 0xf;
        }
        public static void suna8_sh_start()
        {
            int i, len = samplesrom.Length;
            samplebuf = new short[len];
            samplebuf2 = new short[0x800];
            for (i = 0; i < len; i++)
            {
                samplebuf[i] = (short)((sbyte)(samplesrom[i] ^ 0x80) * 256);
            }
            /*BinaryWriter bw1 = new BinaryWriter(new FileStream(@"\VS2008\compare1\compare1\bin\Debug\sample.dat", FileMode.Append));
            for (i = 0; i < len; i++)
            {
                bw1.Write((byte)(samplebuf[i] >> 8));
            }
            bw1.Close();*/
        }
        public static void machine_reset_suna8()
        {

        }
    }
}

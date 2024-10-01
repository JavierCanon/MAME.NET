using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Taitob
    {
        public static byte[] gfxrom, gfx0rom, gfx1rom, mainram2, mainram3;
        public static ushort eep_latch;
        public static ushort coin_word;
        public static int basebanksnd;
        public static byte dswa, dswb, dswb_old;
        public static void TaitobInit()
        {
            int i, n;
            Generic.paletteram16 = new ushort[0x1000];
            TC0180VCU_ram = new ushort[0x8000];
            TC0180VCU_ctrl = new ushort[0x10];
            TC0220IOC_regs = new byte[8];
            TC0220IOC_port = 0;
            TC0640FIO_regs = new byte[8];
            taitob_scroll = new ushort[0x400];
            Memory.mainram = new byte[0x10000];
            mainram2 = new byte[0x1e80];
            mainram3 = new byte[0x2000];
            Memory.audioram = new byte[0x2000];
            bg_rambank = new ushort[2];
            fg_rambank = new ushort[2];
            pixel_scroll = new ushort[2];
            taitob_spriteram = new ushort[0xcc0];
            TC0640FIO_regs = new byte[8];
            Machine.bRom = true;
            Taitosnd.taitosnd_start();
            basebanksnd = 0x10000;
            eep_latch = 0;
            video_control = 0;
            coin_word = 0;
            for (i = 0; i < 0x10; i++)
            {
                TC0180VCU_ctrl[i] = 0;
            }
            Machine.bRom = true;
            Memory.mainrom = Machine.GetRom("maincpu.rom");
            Memory.audiorom = Machine.GetRom("audiocpu.rom");
            gfxrom = Machine.GetRom("gfx1.rom");
            n = gfxrom.Length;
            gfx0rom = new byte[n * 2];
            gfx1rom = new byte[n * 2];
            for (i = 0; i < n; i++)
            {
                gfx1rom[i * 2] = (byte)(gfxrom[i] >> 4);
                gfx1rom[i * 2 + 1] = (byte)(gfxrom[i] & 0x0f);
            }
            for (i = 0; i < n; i++)
            {
                gfx0rom[((i / 0x10) % 8 + (i / 0x80 * 0x10) + ((i / 8) % 2) * 8) * 8 + (i % 8)] = gfx1rom[i];
            }
            FM.ymsndrom = Machine.GetRom("ymsnd.rom");
            YMDeltat.ymsnddeltatrom = Machine.GetRom("ymsnddeltat.rom");
            if (Memory.mainrom == null || gfxrom == null || Memory.audiorom == null || FM.ymsndrom == null)
            {
                Machine.bRom = false;
            }
            if (Machine.bRom)
            {
                switch (Machine.sName)
                {
                    case "pbobble":
                        dswa = 0xff;
                        dswb = 0xff;
                        break;
                    case "silentd":
                    case "silentdj":
                    case "silentdu":
                        dswa = 0xff;
                        dswb = 0xbf;
                        break;
                }
            }
        }
        public static void irqhandler(int irq)
        {
            Cpuint.cpunum_set_input_line(1, 0, irq != 0 ? LineState.ASSERT_LINE : LineState.CLEAR_LINE);
        }
        public static void bankswitch_w(byte data)
        {
            basebanksnd = 0x10000 + 0x4000 * ((data - 1) & 3);
        }
        public static void rsaga2_interrupt2()
        {
            Cpuint.cpunum_set_input_line(0, 2, LineState.HOLD_LINE);
        }
        public static void rastansaga2_interrupt()
        {
            Timer.emu_timer timer = Timer.timer_alloc_common(rsaga2_interrupt2, "vblank_interrupt2", true);
            Timer.timer_adjust_periodic(timer, new Atime(0, (long)(5000 * Cpuexec.cpu[0].attoseconds_per_cycle)), Attotime.ATTOTIME_NEVER);
            Cpuint.cpunum_set_input_line(0, 4, LineState.HOLD_LINE);
        }
        public static void crimec_interrupt3()
        {
            Cpuint.cpunum_set_input_line(0, 3, LineState.HOLD_LINE);
        }
        public static void crimec_interrupt()
        {
            Timer.emu_timer timer = Timer.timer_alloc_common(crimec_interrupt3, "vblank_interrupt2", true);
            Timer.timer_adjust_periodic(timer, new Atime(0, (long)(5000 * Cpuexec.cpu[0].attoseconds_per_cycle)), Attotime.ATTOTIME_NEVER);
            Cpuint.cpunum_set_input_line(0, 5, LineState.HOLD_LINE);
        }
        public static void hitice_interrupt6()
        {
            Cpuint.cpunum_set_input_line(0, 6, LineState.HOLD_LINE);
        }
        public static void hitice_interrupt()
        {
            Timer.emu_timer timer = Timer.timer_alloc_common(hitice_interrupt6, "vblank_interrupt2", true);
            Timer.timer_adjust_periodic(timer, new Atime(0, (long)(5000 * Cpuexec.cpu[0].attoseconds_per_cycle)), Attotime.ATTOTIME_NEVER);
            Cpuint.cpunum_set_input_line(0, 4, LineState.HOLD_LINE);
        }
        public static void rambo3_interrupt1()
        {
            Cpuint.cpunum_set_input_line(0, 1, LineState.HOLD_LINE);
        }
        public static void rambo3_interrupt()
        {
            Timer.emu_timer timer = Timer.timer_alloc_common(rambo3_interrupt1, "vblank_interrupt2", true);
            Timer.timer_adjust_periodic(timer, new Atime(0, (long)(5000 * Cpuexec.cpu[0].attoseconds_per_cycle)), Attotime.ATTOTIME_NEVER);
            Cpuint.cpunum_set_input_line(0, 6, LineState.HOLD_LINE);
        }
        public static void pbobble_interrupt5()
        {
            Cpuint.cpunum_set_input_line(0, 5, LineState.HOLD_LINE);
        }
        public static void pbobble_interrupt()
        {
            Timer.emu_timer timer = Timer.timer_alloc_common(pbobble_interrupt5, "vblank_interrupt2", true);
            Timer.timer_adjust_periodic(timer, new Atime(0, (long)(5000 * Cpuexec.cpu[0].attoseconds_per_cycle)), Attotime.ATTOTIME_NEVER);
            Cpuint.cpunum_set_input_line(0, 3, LineState.HOLD_LINE);
        }
        public static void viofight_interrupt1()
        {
            Cpuint.cpunum_set_input_line(0, 1, LineState.HOLD_LINE);
        }
        public static void viofight_interrupt()
        {
            Timer.emu_timer timer = Timer.timer_alloc_common(viofight_interrupt1, "vblank_interrupt2", true);
            Timer.timer_adjust_periodic(timer, new Atime(0, (long)(5000 * Cpuexec.cpu[0].attoseconds_per_cycle)), Attotime.ATTOTIME_NEVER);
            Cpuint.cpunum_set_input_line(0, 4, LineState.HOLD_LINE);
        }
        public static void masterw_interrupt4()
        {
            Cpuint.cpunum_set_input_line(0, 4, LineState.HOLD_LINE);
        }
        public static void masterw_interrupt()
        {
            Timer.emu_timer timer = Timer.timer_alloc_common(masterw_interrupt4, "vblank_interrupt2", true);
            Timer.timer_adjust_periodic(timer, new Atime(0, (long)(5000 * Cpuexec.cpu[0].attoseconds_per_cycle)), Attotime.ATTOTIME_NEVER);
            Cpuint.cpunum_set_input_line(0, 5, LineState.HOLD_LINE);
        }
        public static void silentd_interrupt4()
        {
            Cpuint.cpunum_set_input_line(0, 4, LineState.HOLD_LINE);
        }
        public static void silentd_interrupt()
        {
            Timer.emu_timer timer = Timer.timer_alloc_common(silentd_interrupt4, "vblank_interrupt2", true);
            Timer.timer_adjust_periodic(timer, new Atime(0, (long)(5000 * Cpuexec.cpu[0].attoseconds_per_cycle)), Attotime.ATTOTIME_NEVER);
            Cpuint.cpunum_set_input_line(0, 6, LineState.HOLD_LINE);
        }
        public static void selfeena_interrupt4()
        {
            Cpuint.cpunum_set_input_line(0, 4, LineState.HOLD_LINE);
        }
        public static void selfeena_interrupt()
        {
            Timer.emu_timer timer = Timer.timer_alloc_common(selfeena_interrupt4, "vblank_interrupt2", true);
            Timer.timer_adjust_periodic(timer, new Atime(0, (long)(5000 * Cpuexec.cpu[0].attoseconds_per_cycle)), Attotime.ATTOTIME_NEVER);
            Cpuint.cpunum_set_input_line(0, 6, LineState.HOLD_LINE);
        }
        public static void sbm_interrupt5()
        {
            Cpuint.cpunum_set_input_line(0, 5, LineState.HOLD_LINE);
        }
        public static void sbm_interrupt()
        {
            Timer.emu_timer timer = Timer.timer_alloc_common(sbm_interrupt5, "vblank_interrupt2", true);
            Timer.timer_adjust_periodic(timer, new Atime(0, (long)(10000 * Cpuexec.cpu[0].attoseconds_per_cycle)), Attotime.ATTOTIME_NEVER);
            Cpuint.cpunum_set_input_line(0, 4, LineState.HOLD_LINE);
        }
        public static void mb87078_gain_changed(int channel, int percent)
        {
            if (channel == 1)
            {
                AY8910.AA8910[0].stream.gain = (int)(0x100 * (percent / 100.0));
                //sound_type type = Machine->config->sound[0].type;
                //sndti_set_output_gain(type, 0, 0, percent / 100.0);
                //sndti_set_output_gain(type, 1, 0, percent / 100.0);
                //sndti_set_output_gain(type, 2, 0, percent / 100.0);
            }
        }
        public static void machine_reset_mb87078()
        {
            MB87078_start(0);
        }
        public static void gain_control_w1(int offset, byte data)
        {
            if (offset == 0)
            {
                MB87078_data_w(0, data, 0);
            }
            else
            {
                MB87078_data_w(0, data, 1);
            }
        }
        public static void gain_control_w(int offset, ushort data)
        {
            if (offset == 0)
            {
                MB87078_data_w(0, data >> 8, 0);
            }
            else
            {
                MB87078_data_w(0, data >> 8, 1);
            }
        }
        public static void nvram_handler_load_taitob()
        {

        }
        public static void nvram_handler_save_taitob()
        {

        }
        public static ushort eeprom_r()
        {
            ushort res;
            res = (ushort)(Eeprom.eeprom_read_bit() & 0x01);
            res |= (ushort)(dswb & 0xfe);
            return res;
        }
        public static ushort eep_latch_r()
        {
            return eep_latch;
        }
        public static void eeprom_w1(byte data)
        {
            eep_latch = (ushort)((data << 8) | (eep_latch & 0xff));
            Eeprom.eeprom_write_bit(data & 0x04);
            Eeprom.eeprom_set_clock_line(((data & 0x08) != 0) ? LineState.ASSERT_LINE : LineState.CLEAR_LINE);
            Eeprom.eeprom_set_cs_line(((data & 0x10) != 0) ? LineState.CLEAR_LINE : LineState.ASSERT_LINE);
        }
        public static void eeprom_w2(byte data)
        {
            eep_latch = (ushort)((eep_latch & 0xff00) | data);
        }
        public static void eeprom_w(ushort data)
        {
            eep_latch = data;
            data >>= 8;
            Eeprom.eeprom_write_bit(data & 0x04);
            Eeprom.eeprom_set_clock_line(((data & 0x08) != 0) ? LineState.ASSERT_LINE : LineState.CLEAR_LINE);
            Eeprom.eeprom_set_cs_line(((data & 0x10) != 0) ? LineState.CLEAR_LINE : LineState.ASSERT_LINE);
        }
        public static void player_34_coin_ctrl_w(ushort data)
        {
            coin_word = data;
            //coin_lockout_w(2, ~data & 0x0100);
            //coin_lockout_w(3, ~data & 0x0200);
            //coin_counter_w(2, data & 0x0400);
            //coin_counter_w(3, data & 0x0800);
        }
        public static ushort pbobble_input_bypass_r(int offset)
        {
            ushort result = 0;
            switch (offset)
            {
                case 0x01:
                    result = (ushort)(eeprom_r() << 8);
                    break;
                default:
                    result = (ushort)(TC0640FIO_r(offset) << 8);
                    break;
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public class Generic
    {
        private static uint[] coin_count;
        private static uint[] coinlockedout;
        private static uint[] lastcoin;
        public static byte[] generic_nvram;
        public static ushort[] buffered_spriteram16;
        public static ushort[] spriteram16, spriteram16_2;
        public static ushort[] paletteram16, paletteram16_2;
        public static void generic_machine_init()
        {
            int counternum;
            coin_count = new uint[8];
            coinlockedout = new uint[8];
            lastcoin = new uint[8];
            for (counternum = 0; counternum < 8; counternum++)
            {
                lastcoin[counternum] = 0;
                coinlockedout[counternum] = 0;
            }
        }
        public static void coin_counter_w(int num, int on)
        {
            if (num >= 8)
            {
                return;
            }
            if (on != 0 && (lastcoin[num] == 0))
            {
                coin_count[num]++;
            }
            lastcoin[num] = (uint)on;
        }
        public static void coin_lockout_w(int num, int on)
        {
            if (num >= 8)
            {
                return;
            }
            coinlockedout[num] =(uint) on;
        }
        public static void coin_lockout_global_w(int on)
        {
            int i;
            for (i = 0; i < 8; i++)
            {
                coin_lockout_w(i, on);
            }
        }
        public static void nvram_load()
        {
            switch (Machine.sBoard)
            {
                case "Neo Geo":
                    Neogeo.nvram_handler_load_neogeo();
                    break;
                /*case "Namco System 1":
                    Namcos1.nvram_handler_load_namcos1();
                    break;*/
            }
        }
        public static void nvram_save()
        {
            switch (Machine.sBoard)
            {
                case "Neo Geo":
                    Neogeo.nvram_handler_save_neogeo();
                    break;
                /*case "Namco System 1":
                    Namcos1.nvram_handler_save_namcos1();
                    break;*/
            }
        }
        public static void nmi_line_pulse()
        {
            Cpuint.cpunum_set_input_line(1, (int)LineState.INPUT_LINE_NMI, LineState.PULSE_LINE);
        }
        public static void irq_1_0_line_hold()
        {
            Cpuint.cpunum_set_input_line(1, 0, LineState.HOLD_LINE);
        }
        public static void watchdog_reset_w()
        {
            Watchdog.watchdog_reset();
        }
        public static void buffer_spriteram16_w()
        {
            Array.Copy(spriteram16, buffered_spriteram16, spriteram16.Length);
        }
        public static void paletteram16_xRRRRRGGGGGBBBBB_word_w(int offset)
        {
            set_color_555(offset, 10, 5, 0, paletteram16[offset]);
        }
        public static void set_color_555(int color, int rshift, int gshift, int bshift, ushort data)
        {
            Palette.palette_entry_set_color2(color, Palette.make_rgb(Palette.pal5bit((byte)(data >> rshift)), Palette.pal5bit((byte)(data >> gshift)), (int)Palette.pal5bit((byte)(data >> bshift))));
        }
        public static void paletteram16_xBBBBBGGGGGRRRRR_word_w(int offset)
        {
            set_color_555(offset, 0, 5, 10, paletteram16[offset]);
        }
    }
}

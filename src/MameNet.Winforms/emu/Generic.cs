using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cpu.m68000;

namespace mame
{
    public class Generic
    {
        private static uint[] coin_count;
        private static uint[] coinlockedout;
        private static uint[] lastcoin;
        public static byte[] videoram, colorram;
        public static byte[] generic_nvram;
        public static byte[] buffered_spriteram;
        public static ushort[] buffered_spriteram16;
        public static byte[] spriteram;
        public static ushort[] spriteram16, spriteram16_2;
        public static byte[] paletteram, paletteram_2;
        public static ushort[] paletteram16, paletteram16_2;
        public static int[] interrupt_enable;
        public static int objcpunum;
        public static int flip_screen_x, flip_screen_y;
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
            interrupt_enable = new int[8];
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
        public static void watchdog_reset16_w()
        {
            Watchdog.watchdog_reset();
        }
        public static ushort watchdog_reset16_r()
        {
            Watchdog.watchdog_reset();
            return 0xffff;
        }
        public static void nmi_0_line_pulse()
        {
            irqn_line_set(0, (int)LineState.INPUT_LINE_NMI, (int)LineState.PULSE_LINE);
        }
        public static void nmi_1_line_pulse()
        {
            irqn_line_set(1, (int)LineState.INPUT_LINE_NMI, (int)LineState.PULSE_LINE);
        }
        public static void irq_0_0_line_hold()
        {
            Cpuint.cpunum_set_input_line(0, 0, LineState.HOLD_LINE);
        }
        public static void irq_0_1_line_hold()
        {
            Cpuint.cpunum_set_input_line(0, 1, LineState.HOLD_LINE);
        }
        public static void irq_0_6_line_hold()
        {
            Cpuint.cpunum_set_input_line(0, 6, LineState.HOLD_LINE);
        }
        public static void irq_1_0_line_hold()
        {
            Cpuint.cpunum_set_input_line(1, 0, LineState.HOLD_LINE);
        }
        public static void irq_2_0_line_hold()
        {
            Cpuint.cpunum_set_input_line(2, 0, LineState.HOLD_LINE);
        }        
        public static void watchdog_reset_w()
        {
            Watchdog.watchdog_reset();
        }        
        public static void interrupt_reset()
        {
            int cpunum;
            for (cpunum = 0; cpunum < Cpuexec.ncpu; cpunum++)
            {
                interrupt_enable[cpunum] = 1;
            }
        }
        public static void clear_all_lines()
        {
            int inputcount=0;
            int line;
            if (objcpunum == 0 && Cpuexec.cpu[0] == MC68000.m1)
            {
                inputcount = 8;
            }
            else
            {
                inputcount = 1;
            }
            Cpuint.cpunum_set_input_line(objcpunum, (int)LineState.INPUT_LINE_NMI, LineState.CLEAR_LINE);
            for (line = 0; line < inputcount; line++)
            {
                Cpuint.cpunum_set_input_line(objcpunum, line, LineState.CLEAR_LINE);
            }
        }
        public static void cpu_interrupt_enable(int cpunum, int enabled)
        {
            interrupt_enable[cpunum] = enabled;
            if (enabled == 0)
            {
                objcpunum = cpunum;
                Timer.timer_set_internal(clear_all_lines, "clear_all_lines");
            }
        }
        public static void interrupt_enable_w(byte data)
        {
            cpu_interrupt_enable(Cpuexec.activecpu, data);
        }
        public static void irqn_line_set(int cpunum, int line, int state)
        {
            if (interrupt_enable[cpunum] != 0)
            {
                Cpuint.cpunum_set_input_line(cpunum, line, (LineState)state);
            }
        }
        public static void nmi_line_pulse0()
        {
            nmi_line_pulse(0);
        }
        public static void nmi_line_pulse(int cpunum)
        {
            irqn_line_set(cpunum, (int)LineState.INPUT_LINE_NMI, (int)LineState.PULSE_LINE);
        }
        public static void irq0_line_hold1()
        {
            irq0_line_hold(1);
        }
        public static void irq0_line_hold(int cpunum)
        {
            irqn_line_set(cpunum, 0, (int)LineState.HOLD_LINE);
        }
        public static void irq4_line_hold(int cpunum)
        {
            irqn_line_set(cpunum, 4, (int)LineState.HOLD_LINE);
        }
        public static void irq5_line_hold0()
        {
            irq5_line_hold(0);
        }
        public static void irq5_line_hold(int cpunum)
        {
            irqn_line_set(cpunum, 5, (int)LineState.HOLD_LINE);
        }
        public static ushort paletteram16_split(int offset)
        {
            return (ushort)(paletteram[offset] | (paletteram_2[offset] << 8));
        }
        public static void buffer_spriteram_w()
        {
            Array.Copy(spriteram, buffered_spriteram, spriteram.Length);
        }
        public static void buffer_spriteram16_w()
        {
            Array.Copy(spriteram16, buffered_spriteram16, spriteram16.Length);
        }
        public static ushort paletteram16_le(int offset)
        {
            return (ushort)(paletteram[offset & ~1] | (paletteram[offset | 1] << 8));
        }
        public static ushort paletteram16_be(int offset)
        {
	        return (ushort)(paletteram[offset | 1] | (paletteram[offset & ~1] << 8));
        }
        public static void set_color_444(int color, int rshift, int gshift, int bshift, ushort data)
        {
            Palette.palette_set_callback(color, Palette.make_rgb(Palette.pal4bit((byte)(data >> rshift)), Palette.pal4bit((byte)(data >> gshift)), Palette.pal4bit((byte)(data >> bshift))));
        }
        public static void set_color_555(int color, int rshift, int gshift, int bshift, ushort data)
        {
            Palette.palette_set_callback(color, Palette.make_rgb(Palette.pal5bit((byte)(data >> rshift)), Palette.pal5bit((byte)(data >> gshift)), (int)Palette.pal5bit((byte)(data >> bshift))));
        }
        public static void updateflip()
        {
            int width = Video.screenstate.width;
            int height = Video.screenstate.height;
            long period = Video.screenstate.frame_period;
            RECT visarea = Video.screenstate.visarea;
            Tmap.tilemap_set_flip(null, (byte)((Tilemap.TILEMAP_FLIPX & flip_screen_x) | (Tilemap.TILEMAP_FLIPY & flip_screen_y)));
            if (flip_screen_x!=0)
            {
                int temp;
                temp = width - visarea.min_x - 1;
                visarea.min_x = width - visarea.max_x - 1;
                visarea.max_x = temp;
            }
            if (flip_screen_y!=0)
            {
                int temp;
                temp = height - visarea.min_y - 1;
                visarea.min_y = height - visarea.max_y - 1;
                visarea.max_y = temp;
            }
            Video.video_screen_configure(width, height, visarea, period);
        }
        public static void flip_screen_set(int on)
        {
            flip_screen_x_set(on);
            flip_screen_y_set(on);
        }
        public static void flip_screen_x_set(int on)
        {
            if (on != 0)
            {
                on = ~0;
            }
            if (flip_screen_x != on)
            {
                flip_screen_x = on;
                updateflip();
            }
        }
        public static void flip_screen_y_set(int on)
        {
            if (on != 0)
            {
                on = ~0;
            }
            if (flip_screen_y != on)
            {
                flip_screen_y = on;
                updateflip();
            }
        }
        public static int flip_screen_get()
        {
            return flip_screen_x;
        }
        public static void paletteram_xxxxBBBBGGGGRRRR_le_w(int offset, byte data)
        {
            paletteram[offset] = data;
            set_color_444(offset / 2, 0, 4, 8, paletteram16_le(offset));
        }
        public static void paletteram16_xxxxRRRRGGGGBBBB_word_w(int offset, ushort data)
        {
            paletteram16[offset] = data;
            set_color_444(offset, 8, 4, 0, paletteram16[offset]);
        }
        public static void paletteram16_xxxxRRRRGGGGBBBB_word_w1(int offset, byte data)
        {
            paletteram16[offset] = (ushort)((data << 8) | (paletteram16[offset] & 0xff));
            set_color_444(offset, 8, 4, 0, paletteram16[offset]);
        }
        public static void paletteram16_xxxxRRRRGGGGBBBB_word_w2(int offset, byte data)
        {
            paletteram16[offset] = (ushort)((paletteram16[offset] & 0xff00) | data);
            set_color_444(offset, 8, 4, 0, paletteram16[offset]);
        }
        public static void paletteram_RRRRGGGGBBBBxxxx_be_w(int offset, byte data)
        {
            paletteram[offset] = data;
            set_color_444(offset / 2, 12, 8, 4, paletteram16_be(offset));
        }
        public static void paletteram_RRRRGGGGBBBBxxxx_split1_w(int offset, byte data)
        {
            paletteram[offset] = data;
            set_color_444(offset, 12, 8, 4, paletteram16_split(offset));
        }
        public static void paletteram_RRRRGGGGBBBBxxxx_split2_w(int offset, byte data)
        {
            paletteram_2[offset] = data;
            set_color_444(offset, 12, 8, 4, paletteram16_split(offset));
        }

        public static void paletteram16_xBBBBBGGGGGRRRRR_word_w(int offset,ushort data)
        {
            paletteram16[offset] = data;
            set_color_555(offset, 0, 5, 10, paletteram16[offset]);
        }
        public static void paletteram16_xBBBBBGGGGGRRRRR_word_w1(int offset, byte data)
        {
            paletteram16[offset] = (ushort)((data << 8) | (paletteram16[offset] & 0xff));
            set_color_555(offset, 0, 5, 10, paletteram16[offset]);
        }
        public static void paletteram16_xBBBBBGGGGGRRRRR_word_w2(int offset, byte data)
        {
            paletteram16[offset] = (ushort)((paletteram16[offset] & 0xff00) | data);
            set_color_555(offset, 0, 5, 10, paletteram16[offset]);
        }
        public static void paletteram16_xRRRRRGGGGGBBBBB_word_w(int offset)
        {
            set_color_555(offset, 10, 5, 0, paletteram16[offset]);
        }
        public static void paletteram16_RRRRGGGGBBBBRGBx_word_w(int offset, ushort data)
        {
            paletteram16[offset] = data;
            ushort data1 = paletteram16[offset];
            Palette.palette_set_callback(offset, (uint)((Palette.pal5bit((byte)(((data1 >> 11) & 0x1e) | ((data1 >> 3) & 0x01))) << 16) | (Palette.pal5bit((byte)(((data >> 7) & 0x1e) | ((data >> 2) & 0x01))) << 8) | Palette.pal5bit((byte)(((data >> 3) & 0x1e) | ((data >> 1) & 0x01)))));
        }
        public static void paletteram16_RRRRGGGGBBBBRGBx_word_w1(int offset, byte data)
        {
            paletteram16[offset] = (ushort)((data << 8) | (paletteram16[offset] & 0xff));
            ushort data1 = paletteram16[offset];
            Palette.palette_set_callback(offset, (uint)((Palette.pal5bit((byte)(((data1 >> 11) & 0x1e) | ((data1 >> 3) & 0x01))) << 16) | (Palette.pal5bit((byte)(((data >> 7) & 0x1e) | ((data >> 2) & 0x01))) << 8) | Palette.pal5bit((byte)(((data >> 3) & 0x1e) | ((data >> 1) & 0x01)))));
        }
        public static void paletteram16_RRRRGGGGBBBBRGBx_word_w2(int offset, byte data)
        {
            paletteram16[offset] = (ushort)((paletteram16[offset] & 0xff00) | data);
            ushort data1 = paletteram16[offset];
            Palette.palette_set_callback(offset, (uint)((Palette.pal5bit((byte)(((data1 >> 11) & 0x1e) | ((data1 >> 3) & 0x01))) << 16) | (Palette.pal5bit((byte)(((data >> 7) & 0x1e) | ((data >> 2) & 0x01))) << 8) | Palette.pal5bit((byte)(((data >> 3) & 0x1e) | ((data >> 1) & 0x01)))));
        }        
    }
}

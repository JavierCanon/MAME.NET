using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.DirectX.DirectInput;
using ui;

namespace mame
{
    public class UI
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        private static uint UI_FILLCOLOR = Palette.make_argb(0xe0, 0x10, 0x10, 0x30);
        public delegate void ui_delegate();
        public static ui_delegate ui_handler_callback, ui_update_callback;
        public static bool single_step;
        public static mainForm mainform;
        public static void ui_init(mainForm form1)
        {
            mainform = form1;
        }
        public static void ui_update_and_render()
        {
            ui_update_callback();
            ui_handler_callback();
        }
        public static void ui_updateC()
        {
            int i;
            int red, green, blue;
            if (single_step || Mame.paused)
            {
                byte bright = 0xa7;
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    red = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff0000) >> 16) * bright / 0xff);
                    green = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff00) >> 8) * bright / 0xff);
                    blue = (int)((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff) * bright / 0xff);
                    Video.bitmapcolor[i] = (int)Palette.make_argb(0xff, red, green, blue);
                }
            }
            else
            {
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    Video.bitmapcolor[i] = (int)Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]];
                }
            }
        }
        public static void ui_updateTehkan()
        {
            int i;
            int red, green, blue;
            if (single_step || Mame.paused)
            {
                byte bright = 0xa7;
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    if (Video.bitmapbase[Video.curbitmap][i] < 0x100)
                    {
                        red = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff0000) >> 16) * bright / 0xff);
                        green = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff00) >> 8) * bright / 0xff);
                        blue = (int)((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff) * bright / 0xff);
                        Video.bitmapcolor[i] = (int)Palette.make_argb(0xff, red, green, blue);
                    }
                    else
                    {
                        int i1 = 1;
                    }
                }
            }
            else
            {
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    if (Video.bitmapbase[Video.curbitmap][i] < 0x100)
                    {
                        Video.bitmapcolor[i] = (int)Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]];
                    }
                    else
                    {
                        Video.bitmapcolor[i] = (int)Palette.entry_color[0];
                    }
                }
            }
        }
        public static void ui_updateN()
        {
            int i;
            int red, green, blue;
            if (single_step || Mame.paused)
            {
                byte bright = 0xa7;
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    red = ((Video.bitmapbaseN[Video.curbitmap][i] & 0xff0000) >> 16) * bright / 0xff;
                    green = ((Video.bitmapbaseN[Video.curbitmap][i] & 0xff00) >> 8) * bright / 0xff;
                    blue = (Video.bitmapbaseN[Video.curbitmap][i] & 0xff) * bright / 0xff;
                    Video.bitmapcolor[i] = (int)Palette.make_argb(0xff, red, green, blue);
                }
            }
            else
            {
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    Video.bitmapcolor[i] = (int)(0xff000000 | (uint)Video.bitmapbaseN[Video.curbitmap][i]);
                }
            }
        }
        public static void ui_updateNa()
        {
            int i;
            int red, green, blue;
            if (single_step || Mame.paused)
            {
                byte bright = 0xa7;
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    red = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff0000) >> 16) * bright / 0xff);
                    green = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff00) >> 8) * bright / 0xff);
                    blue = (int)((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff) * bright / 0xff);
                    Video.bitmapcolor[i] = (int)Palette.make_argb(0xff, red, green, blue);
                }
            }
            else
            {
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    Video.bitmapcolor[i] = (int)Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]];
                }
            }
        }
        public static void ui_updateIGS011()
        {
            int i;
            int red, green, blue;
            if (single_step || Mame.paused)
            {
                byte bright = 0xa7;
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    red = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff0000) >> 16) * bright / 0xff);
                    green = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff00) >> 8) * bright / 0xff);
                    blue = (int)((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff) * bright / 0xff);
                    Video.bitmapcolor[i] = (int)Palette.make_argb(0xff, red, green, blue);
                }
            }
            else
            {
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    Video.bitmapcolor[i] = (int)Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]];
                }
            }
        }
        public static void ui_updatePGM()
        {
            int i;
            int red, green, blue;
            if (single_step || Mame.paused)
            {
                byte bright = 0xa7;
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    red = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff0000) >> 16) * bright / 0xff);
                    green = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff00) >> 8) * bright / 0xff);
                    blue = (int)((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff) * bright / 0xff);
                    Video.bitmapcolor[i] = (int)Palette.make_argb(0xff, red, green, blue);
                }
            }
            else
            {
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    Video.bitmapcolor[i] = (int)Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]];
                }
            }
        }
        public static void handler_ingame()
        {
            Mame.handle2 = GetForegroundWindow();
            if (Mame.handle1 == Mame.handle2)
            {
                Mame.is_foreground = true;
            }
            else
            {
                Mame.is_foreground = false;
            }
            bool is_paused = Mame.mame_is_paused();
            if (single_step)
            {
                Mame.mame_pause(true);
                single_step = false;
            }
            if (Mame.is_foreground)
            {
                if(Keyboard.IsPressed(Key.F3))
                {
                    cpurun();
                    Mame.playState = Mame.PlayState.PLAY_RESET;
                }
                if (Keyboard.IsTriggered(Key.F7))
                {
                    cpurun();
                    if (Keyboard.IsPressed(Key.LeftShift) || Keyboard.IsPressed(Key.RightShift))
                    {
                        Mame.playState = Mame.PlayState.PLAY_SAVE;
                    }
                    else
                    {
                        Mame.playState = Mame.PlayState.PLAY_LOAD;                        
                    }
                    return;
                }
                if (Keyboard.IsTriggered(Key.F8))
                {
                    cpurun();
                    if (Keyboard.IsPressed(Key.LeftShift) || Keyboard.IsPressed(Key.RightShift))
                    {
                        if (Mame.playState == Mame.PlayState.PLAY_RECORDRUNNING)
                        {
                            Mame.playState = Mame.PlayState.PLAY_RECORDEND;
                        }
                        else
                        {
                            Mame.playState = Mame.PlayState.PLAY_RECORDSTART;
                        }
                    }
                    else
                    {
                        Mame.playState = Mame.PlayState.PLAY_REPLAYSTART;
                    }
                    return;
                }
                if (Keyboard.IsTriggered(Key.P))
                {
                    if (is_paused && (Keyboard.IsPressed(Key.LeftShift) || Keyboard.IsPressed(Key.RightShift)))
                    {
                        single_step = true;
                        Mame.mame_pause(false);                        
                    }
                    else
                    {
                        Mame.mame_pause(!Mame.mame_is_paused());
                    }                    
                }
                if (Keyboard.IsTriggered(Key.F10))
                {
                    Keyboard.bF10 = true;
                    bool b1 = Video.global_throttle;
                    Video.global_throttle = !b1;
                }
            }
        }
        public static void cpurun()
        {
            m68000Form.m68000State = m68000Form.M68000State.M68000_RUN;
            Machine.FORM.m68000form.tsslStatus.Text = "run";
            z80Form.z80State = z80Form.Z80AState.Z80A_RUN;
            Machine.FORM.z80form.tsslStatus.Text = "run";
        }
        private static double ui_get_line_height()
        {
            int raw_font_pixel_height = 0x0b;
            int target_pixel_height = 0xff;
            double one_to_one_line_height;
            double scale_factor;
            one_to_one_line_height = (double)raw_font_pixel_height / (double)target_pixel_height;
            scale_factor = 1.0;
            return scale_factor * one_to_one_line_height;
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace mame
{
    public struct screen_state
    {
        public int width;					/* current width (HTOTAL) */
        public int height;					/* current height (VTOTAL) */
        public RECT visarea;    			/* current visible area (HBLANK end/start, VBLANK end/start) */
        public int last_partial_scan;       /* scanline of last partial update */
        public long frame_period;			/* attoseconds per frame */
        public long vblank_period;			/* attoseconds per VBLANK period */
        public long scantime;				/* attoseconds per scanline */
        public long pixeltime;				/* attoseconds per pixel */
        public Atime vblank_start_time;
        public Atime vblank_end_time;
        public long frame_number;
    };
    partial class Video
    {
        public static bool flip_screen_x, flip_screen_y;
        public static long frame_number_obj;
        public static Atime frame_update_time;
        public static screen_state screenstate;
        private static int PAUSED_REFRESH_RATE = 30;
        public static Timer.emu_timer vblank_begin_timer;
        public static Timer.emu_timer scanline0_timer, scanline_timer;
        private static Atime throttle_emutime, throttle_realtime, speed_last_emutime, overall_emutime;
        //public static Atime partial_frame_period;        
        private static long throttle_last_ticks;
        private static long average_oversleep;        
        private static long speed_last_realtime, overall_real_ticks;
        private static double speed_percent;
        private static uint throttle_history, overall_valid_counter, overall_real_seconds;
        private static int[] popcount;
        public static ushort[][] bitmapbase;
        public static int[][] bitmapbaseN;
        public static int[] bitmapcolor;
        public static int fullwidth, fullheight;
        public static bool global_throttle;
        public static int scanline_param;
        private static Bitmap bitmapGDI;
        private static Bitmap[] bbmp;
        public static RECT new_clip;
        public static int curbitmap;
        public static string sDrawText;
        public static long popup_text_end;
        public static int iMode, nMode;
        private static BitmapData bitmapData;
        public static int offsetx, offsety, width, height;
        public delegate void video_delegate();
        public static video_delegate video_update_callback, video_eof_callback;
        private static int NEOGEO_HBEND = 0x01e;//30	/* this should really be 29.5 */
        private static int NEOGEO_HBSTART = 0x15e;//350 /* this should really be 349.5 */
        private static int NEOGEO_VTOTAL = 0x108;//264
        private static int NEOGEO_VBEND = 0x010;
        private static int NEOGEO_VBSTART = 0x0f0;//240
        private static int NEOGEO_VBLANK_RELOAD_HPOS = 0x11f;//287
        public static void video_init()
        {
            Wintime.wintime_init();
            global_throttle = true;
            UI.ui_handler_callback = UI.handler_ingame;
            sDrawText = "";
            popup_text_end = 0;
            popcount = new int[256]{
		        0,1,1,2,1,2,2,3, 1,2,2,3,2,3,3,4, 1,2,2,3,2,3,3,4, 2,3,3,4,3,4,4,5,
		        1,2,2,3,2,3,3,4, 2,3,3,4,3,4,4,5, 2,3,3,4,3,4,4,5, 3,4,4,5,4,5,5,6,
		        1,2,2,3,2,3,3,4, 2,3,3,4,3,4,4,5, 2,3,3,4,3,4,4,5, 3,4,4,5,4,5,5,6,
		        2,3,3,4,3,4,4,5, 3,4,4,5,4,5,5,6, 3,4,4,5,4,5,5,6, 4,5,5,6,5,6,6,7,
		        1,2,2,3,2,3,3,4, 2,3,3,4,3,4,4,5, 2,3,3,4,3,4,4,5, 3,4,4,5,4,5,5,6,
		        2,3,3,4,3,4,4,5, 3,4,4,5,4,5,5,6, 3,4,4,5,4,5,5,6, 4,5,5,6,5,6,6,7,
		        2,3,3,4,3,4,4,5, 3,4,4,5,4,5,5,6, 3,4,4,5,4,5,5,6, 4,5,5,6,5,6,6,7,
		        3,4,4,5,4,5,5,6, 4,5,5,6,5,6,6,7, 4,5,5,6,5,6,6,7, 5,6,6,7,6,7,7,8
            };
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                    screenstate.width = 0x200;
                    screenstate.height = 0x100;
                    screenstate.visarea.min_x = 0;
                    screenstate.visarea.max_x = 0x1ff;
                    screenstate.visarea.min_y = 0;
                    screenstate.visarea.max_y = 0x1ff;
                    fullwidth = 0x200;
                    fullheight = 0x200;
                    frame_update_time = new Atime(0, (long)(1e18 / 59.61));//59.61Hz
                    screenstate.vblank_period = 0;
                    bitmapGDI = new Bitmap(Video.fullwidth, Video.fullheight);
                    UI.ui_update_callback = UI.ui_updateC;
                    bitmapbase = new ushort[2][];
                    bitmapbase[0] = new ushort[0x200 * 0x200];
                    bitmapbase[1] = new ushort[0x200 * 0x200];
                    bbmp = new Bitmap[3];
                    bbmp[0] = new Bitmap(512, 512);
                    bbmp[1] = new Bitmap(512, 256);
                    bbmp[2] = new Bitmap(384, 224);
                    video_update_callback = CPS.video_update_cps1;
                    video_eof_callback = CPS.video_eof_cps1;
                    break;
                case "CPS2":
                    screenstate.width = 0x200;
                    screenstate.height = 0x100;
                    screenstate.visarea.min_x = 0;
                    screenstate.visarea.max_x = 0x1ff;
                    screenstate.visarea.min_y = 0;
                    screenstate.visarea.max_y = 0x1ff;
                    fullwidth = 0x200;
                    fullheight = 0x200;
                    frame_update_time = new Atime(0, (long)(1e18 / 8000000) * 512 * 262);//59.637404580152669Hz
                    screenstate.vblank_period = 0;
                    bitmapGDI = new Bitmap(Video.fullwidth, Video.fullheight);
                    UI.ui_update_callback = UI.ui_updateC;
                    bitmapbase = new ushort[2][];
                    bitmapbase[0] = new ushort[0x200 * 0x200];
                    bitmapbase[1] = new ushort[0x200 * 0x200];
                    bbmp = new Bitmap[3];
                    bbmp[0] = new Bitmap(512, 512);
                    bbmp[1] = new Bitmap(512, 256);
                    bbmp[2] = new Bitmap(384, 224);
                    video_update_callback = CPS.video_update_cps1;
                    video_eof_callback = CPS.video_eof_cps1;
                    break;
                case "Neo Geo":
                    screenstate.width = 384;
                    screenstate.height = 264;
                    screenstate.visarea.min_x = NEOGEO_HBEND;//30
                    screenstate.visarea.max_x = NEOGEO_HBSTART - 1;//349
                    screenstate.visarea.min_y = NEOGEO_VBEND;//16
                    screenstate.visarea.max_y = NEOGEO_VBSTART - 1;//239
                    fullwidth = 384;
                    fullheight = 264;
                    frame_update_time = new Atime(0, (long)(1e18 / 6000000) * screenstate.width * screenstate.height);//59.1856060608428Hz
                    screenstate.vblank_period = (long)(1e18 / 6000000) * 384 * (264 - 224);
                    UI.ui_update_callback = UI.ui_updateN;
                    bitmapbaseN = new int[2][];
                    bitmapbaseN[0] = new int[384 * 264];
                    bitmapbaseN[1] = new int[384 * 264];
                    bbmp = new Bitmap[1];
                    bbmp[0] = new Bitmap(320, 224);
                    video_update_callback = Neogeo.video_update_neogeo;
                    video_eof_callback = Neogeo.video_eof_neogeo;
                    break;
                case "Namco System 1":
                    screenstate.width = 0x200;
                    screenstate.height = 0x200;
                    screenstate.visarea.min_x = 0;
                    screenstate.visarea.max_x = 0x1ff;
                    screenstate.visarea.min_y = 0;
                    screenstate.visarea.max_y = 0x1ff;
                    fullwidth = 0x200;
                    fullheight = 0x200;
                    frame_update_time = new Atime(0, (long)(1e18 / 60.606060));
                    screenstate.vblank_period = 0;
                    UI.ui_update_callback = UI.ui_updateNa;
                    bitmapGDI = new Bitmap(Video.fullwidth, Video.fullheight);
                    bitmapbase = new ushort[2][];
                    bitmapbase[0] = new ushort[0x200 * 0x200];
                    bitmapbase[1] = new ushort[0x200 * 0x200];
                    bbmp = new Bitmap[2];
                    bbmp[0] = new Bitmap(512, 512);
                    bbmp[1] = new Bitmap(288, 224);
                    video_update_callback = Namcos1.video_update_namcos1;
                    video_eof_callback = Namcos1.video_eof_namcos1;
                    break;
                case "IGS011":
                    screenstate.width = 0x200;
                    screenstate.height = 0x100;
                    screenstate.visarea.min_x = 0;
                    screenstate.visarea.max_x = 0x1ff;
                    screenstate.visarea.min_y = 0;
                    screenstate.visarea.max_y = 0xff;
                    fullwidth = 0x200;
                    fullheight = 0x200;
                    frame_update_time = new Atime(0, (long)(1e18 / 60));
                    screenstate.vblank_period = 0;
                    UI.ui_update_callback = UI.ui_updateIGS011;
                    bitmapGDI = new Bitmap(Video.fullwidth, Video.fullheight);
                    bitmapbase = new ushort[2][];
                    bitmapbase[0] = new ushort[0x200 * 0x200];
                    bitmapbase[1] = new ushort[0x200 * 0x200];
                    bbmp = new Bitmap[1];
                    bbmp[0] = new Bitmap(512, 240);
                    video_update_callback = IGS011.video_update_igs011;
                    video_eof_callback = IGS011.video_eof_igs011;
                    break;
                case "PGM":
                    screenstate.width = 0x200;
                    screenstate.height = 0x200;
                    screenstate.visarea.min_x = 0;
                    screenstate.visarea.max_x = 0x1bf;
                    screenstate.visarea.min_y = 0;
                    screenstate.visarea.max_y = 0xdf;
                    fullwidth = 0x200;
                    fullheight = 0x200;
                    frame_update_time = new Atime(0, (long)(1e18 / 60));                    
                    screenstate.vblank_period = 0;
                    UI.ui_update_callback = UI.ui_updatePGM;
                    bitmapGDI = new Bitmap(Video.fullwidth, Video.fullheight);
                    bitmapbase = new ushort[2][];
                    bitmapbase[0] = new ushort[0x200 * 0x200];
                    bitmapbase[1] = new ushort[0x200 * 0x200];
                    bbmp = new Bitmap[1];
                    bbmp[0] = new Bitmap(448, 224);
                    video_update_callback = PGM.video_update_pgm;
                    video_eof_callback = PGM.video_eof_pgm;
                    break;
                case "M72":
                    screenstate.width = 0x200;
                    screenstate.height = 0x11c;
                    screenstate.visarea.min_x = 0x40;
                    screenstate.visarea.max_x = 0x1bf;
                    screenstate.visarea.min_y = 0;
                    screenstate.visarea.max_y = 0xff;
                    fullwidth = 0x200;
                    fullheight = 0x200;
                    frame_update_time = new Atime(0, (long)(1e18 / 8000000) * screenstate.width * screenstate.height);
                    screenstate.vblank_period = (long)(1e18 / 8000000) * 512 * (284 - 256);
                    UI.ui_update_callback = UI.ui_updatePGM;
                    bitmapbase = new ushort[2][];
                    bitmapbase[0] = new ushort[0x200 * 0x200];//0x11c
                    bitmapbase[1] = new ushort[0x200 * 0x200];//0x11c
                    bbmp = new Bitmap[1];
                    bbmp[0] = new Bitmap(512, 284);
                    video_update_callback = M72.video_update_m72;
                    video_eof_callback = M72.video_eof_m72;
                    break;
                case "M92":
                    screenstate.width = 0x200;
                    screenstate.height = 0x100;
                    screenstate.visarea.min_x = 0x50;
                    screenstate.visarea.max_x = 0x18f;
                    screenstate.visarea.min_y = 0x8;
                    screenstate.visarea.max_y = 0xf7;
                    fullwidth = 0x200;
                    fullheight = 0x200;
                    frame_update_time = new Atime(0, (long)(1e18 / 60));
                    screenstate.vblank_period = 0;
                    UI.ui_update_callback = UI.ui_updatePGM;
                    bitmapbase = new ushort[2][];
                    bitmapbase[0] = new ushort[0x200 * 0x200];
                    bitmapbase[1] = new ushort[0x200 * 0x200];
                    bbmp = new Bitmap[1];
                    bbmp[0] = new Bitmap(0x200, 0x100);
                    video_update_callback = M92.video_update_m92;
                    video_eof_callback = M92.video_eof_m92;
                    break;
            }
            screenstate.frame_period = frame_update_time.attoseconds;
            screenstate.scantime = screenstate.frame_period / screenstate.height;
            screenstate.pixeltime = screenstate.frame_period / (screenstate.height * screenstate.width);
            screenstate.frame_number = 0;
            bitmapGDI = new Bitmap(Video.fullwidth, Video.fullheight);
            bitmapcolor = new int[Video.fullwidth * Video.fullheight];
            vblank_begin_timer = Timer.timer_alloc_common(vblank_begin_callback, "vblank_begin_callback", false);
            Timer.timer_adjust_periodic(vblank_begin_timer, frame_update_time, Attotime.ATTOTIME_NEVER);
            scanline0_timer = Timer.timer_alloc_common(scanline0_callback, "scanline0_callback", false);
            Timer.timer_adjust_periodic(scanline0_timer, video_screen_get_time_until_pos(0, 0), Attotime.ATTOTIME_NEVER);
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "Namco System 1":
                    break;
                case "CPS2":
                    Cpuexec.cpu[0].partial_frame_period = Attotime.attotime_div(Video.frame_update_time, 262);
                    Cpuexec.cpu[0].partial_frame_timer = Timer.timer_alloc_common(Cpuexec.trigger_partial_frame_interrupt, "trigger_partial_frame_interrupt", false);
                    break;
                case "Neo Geo":
                    break;
                case "IGS011":
                    Cpuexec.cpu[0].partial_frame_period = Attotime.attotime_div(Video.frame_update_time, 5);
                    Cpuexec.cpu[0].partial_frame_timer = Timer.timer_alloc_common(Cpuexec.trigger_partial_frame_interrupt, "trigger_partial_frame_interrupt", false);
                    break;
                case "M72":
                    Cpuexec.cpu[1].partial_frame_period = Attotime.attotime_div(Video.frame_update_time, 128);
                    Cpuexec.cpu[1].partial_frame_timer = Timer.timer_alloc_common(Cpuexec.trigger_partial_frame_interrupt, "trigger_partial_frame_interrupt", false);
                    break;
            }
            screenstate.vblank_start_time = Attotime.ATTOTIME_ZERO;

        }
        public static bool video_screen_update_partial(int scanline)
        {
            new_clip = screenstate.visarea;
            bool result = false;	
            if (screenstate.last_partial_scan > new_clip.min_y)
            {
                new_clip.min_y = screenstate.last_partial_scan;
            }
            if (scanline < new_clip.max_y)
            {
                new_clip.max_y = scanline;
            }
            if (new_clip.min_y <= new_clip.max_y)
            {                
                video_update_callback();
                result = true;
            }
            screenstate.last_partial_scan = scanline + 1;
            return result;
        }
        public static int video_screen_get_vpos()
        {
            long delta = Attotime.attotime_to_attoseconds(Attotime.attotime_sub(Timer.get_current_time(), screenstate.vblank_start_time));
            int vpos;
            delta += screenstate.pixeltime / 2;
            vpos = (int)(delta / screenstate.scantime);
            return (screenstate.visarea.max_y + 1 + vpos) % screenstate.height;
        }
        public static Atime video_screen_get_time_until_pos(int vpos, int hpos)
        {
            long curdelta = Attotime.attotime_to_attoseconds(Attotime.attotime_sub(Timer.get_current_time(), screenstate.vblank_start_time));
            long targetdelta;
            vpos += screenstate.height - (screenstate.visarea.max_y + 1);
            vpos %= screenstate.height;
            targetdelta = vpos * screenstate.scantime + hpos * screenstate.pixeltime;
            if (targetdelta <= curdelta + screenstate.pixeltime / 2)
                targetdelta += screenstate.frame_period;
            while (targetdelta <= curdelta)
                targetdelta += screenstate.frame_period;
            return new Atime(0, targetdelta - curdelta);
        }
        private static bool effective_throttle()
        {
            //	if (mame_is_paused(machine) || ui_is_menu_active())
            //		return true;
            //	if (global.fastforward)
            //		return false;
            return global_throttle;
        }
        public static void vblank_begin_callback()
        {
            screenstate.vblank_start_time = Timer.global_basetime;// Timer.get_current_time();
            Cpuexec.on_vblank();
            video_frame_update();
            Timer.timer_adjust_periodic(vblank_begin_timer, frame_update_time, Attotime.ATTOTIME_NEVER);
        }
        public static void vblank_end_callback()
        {
            int i;
            //screenstate.frame_number++;
        }
        public static void scanline0_callback()
        {
            screenstate.last_partial_scan = 0;
            Timer.timer_adjust_periodic(scanline0_timer, video_screen_get_time_until_pos(0, 0), Attotime.ATTOTIME_NEVER);
        }
        public static void scanline_update_callback()
        {
            int scanline = scanline_param;
            video_screen_update_partial(scanline);
            scanline++;
            if (scanline > screenstate.visarea.max_y)
            {
                scanline = screenstate.visarea.min_y;
            }
            scanline_param=scanline;
            Timer.timer_adjust_periodic(scanline_timer, video_screen_get_time_until_pos(scanline, 0), Attotime.ATTOTIME_NEVER);
        }
        public static void video_frame_update()
        {
            Atime current_time = Timer.global_basetime;
            if (!Mame.paused)
            {
                finish_screen_updates();
            }
            Keyboard.Update();
            Inptport.frame_update_callback();
            UI.ui_update_and_render();
            if(Machine.FORM.cheatform.lockState == ui.cheatForm.LockState.LOCK_FRAME)
            {
                Machine.FORM.cheatform.ApplyCheat();
            }
            GDIDraw();
            if (effective_throttle())
            {
                update_throttle(current_time);
            }
            recompute_speed(current_time);
            if (Mame.paused)
            {
                //Thread.Sleep(5);
            }
            else
            {
                video_eof_callback();
            }
        }
        private static void finish_screen_updates()
        {
            video_screen_update_partial(screenstate.visarea.max_y);
            curbitmap = 1 - curbitmap;
        }
        private static void update_throttle(Atime emutime)
        {
            long real_delta_attoseconds;
            long emu_delta_attoseconds;
            long real_is_ahead_attoseconds;
            long attoseconds_per_tick;
            long ticks_per_second;
            long target_ticks;
            long diff_ticks;
            ticks_per_second = Wintime.ticks_per_second;
            attoseconds_per_tick = Attotime.ATTOSECONDS_PER_SECOND / ticks_per_second;
            if (Mame.mame_is_paused())
            {
                throttle_emutime = Attotime.attotime_sub_attoseconds(emutime, Attotime.ATTOSECONDS_PER_SECOND / PAUSED_REFRESH_RATE);
                throttle_realtime = throttle_emutime;
            }
            emu_delta_attoseconds = Attotime.attotime_to_attoseconds(Attotime.attotime_sub(emutime, throttle_emutime));
            if (emu_delta_attoseconds < 0 || emu_delta_attoseconds > Attotime.ATTOSECONDS_PER_SECOND / 10)
            {
                goto resync;
            }
            diff_ticks = Wintime.osd_ticks() - throttle_last_ticks;
            throttle_last_ticks += diff_ticks;
            if (diff_ticks >= ticks_per_second)
            {
                goto resync;
            }
            real_delta_attoseconds = diff_ticks * attoseconds_per_tick;
            throttle_emutime = emutime;
            throttle_realtime = Attotime.attotime_add_attoseconds(throttle_realtime, real_delta_attoseconds);
            throttle_history = (throttle_history << 1) | Convert.ToUInt32(emu_delta_attoseconds > real_delta_attoseconds);
            real_is_ahead_attoseconds = Attotime.attotime_to_attoseconds(Attotime.attotime_sub(throttle_emutime, throttle_realtime));
            if ((real_is_ahead_attoseconds < -Attotime.ATTOSECONDS_PER_SECOND / 10) || (real_is_ahead_attoseconds < 0 && popcount[throttle_history & 0xff] < 6))
            {
                goto resync;
            }
            if (real_is_ahead_attoseconds < 0)
            {
                return;
            }
            target_ticks = throttle_last_ticks + real_is_ahead_attoseconds / attoseconds_per_tick;
            diff_ticks = throttle_until_ticks(target_ticks) - throttle_last_ticks;
            throttle_last_ticks += diff_ticks;
            throttle_realtime = Attotime.attotime_add_attoseconds(throttle_realtime, diff_ticks * attoseconds_per_tick);
            return;
        resync:
            throttle_realtime = throttle_emutime = emutime;
        }
        private static long throttle_until_ticks(long target_ticks)
        {
            long minimum_sleep = Wintime.ticks_per_second / 1000;
            long current_ticks = Wintime.osd_ticks();
            long new_ticks;
            while (current_ticks < target_ticks)
            {
                long delta;
                bool slept = false;
                delta = (target_ticks - current_ticks) * 1000 / (1000 + average_oversleep);
                if (delta >= minimum_sleep)
                {
                    Wintime.osd_sleep(delta);
                    slept = true;
                }
                new_ticks = Wintime.osd_ticks();
                if (slept)
                {
                    long actual_ticks = new_ticks - current_ticks;
                    if (actual_ticks > delta)
                    {
                        long oversleep_milliticks = 1000 * (actual_ticks - delta) / delta;
                        average_oversleep = (average_oversleep * 99 + oversleep_milliticks) / 100;

                    }
                }
                current_ticks = new_ticks;
            }
            return current_ticks;
        }
        private static void recompute_speed(Atime emutime)
        {
            long delta_emutime;
            if (speed_last_realtime == 0 || Mame.mame_is_paused())
            {
                speed_last_realtime = Wintime.osd_ticks();
                speed_last_emutime = emutime;
            }
            delta_emutime = Attotime.attotime_to_attoseconds(Attotime.attotime_sub(emutime, speed_last_emutime));
            if (delta_emutime > Attotime.ATTOSECONDS_PER_SECOND / 4)
            {
                long realtime = Wintime.osd_ticks();
                long delta_realtime = realtime - speed_last_realtime;
                long tps = Wintime.ticks_per_second;
                speed_percent = (double)delta_emutime * (double)tps / ((double)delta_realtime * (double)Attotime.ATTOSECONDS_PER_SECOND);
                speed_last_realtime = realtime;
                speed_last_emutime = emutime;
                overall_valid_counter++;
                if (overall_valid_counter >= 4)
                {
                    overall_real_ticks += delta_realtime;
                    while (overall_real_ticks >= tps)
                    {
                        overall_real_ticks -= tps;
                        overall_real_seconds++;
                    }
                    overall_emutime = Attotime.attotime_add_attoseconds(overall_emutime, delta_emutime);
                }
            }
        }
        public static void flip_screen_set_no_update(bool on)
        {
            flip_screen_x = on;
        }
        public static bool flip_screen_get()
        {
            return flip_screen_x;
        }
        public static void SaveStateBinary(BinaryWriter writer)
        {
            writer.Write(scanline_param);
            writer.Write(screenstate.last_partial_scan);
            writer.Write(screenstate.vblank_start_time.seconds);
            writer.Write(screenstate.vblank_start_time.attoseconds);
            writer.Write(screenstate.frame_number);            
        }
        public static void LoadStateBinary(BinaryReader reader)
        {
            scanline_param = reader.ReadInt32();
            screenstate.last_partial_scan = reader.ReadInt32();
            screenstate.vblank_start_time.seconds = reader.ReadInt32();
            screenstate.vblank_start_time.attoseconds = reader.ReadInt64();
            screenstate.frame_number = reader.ReadInt64();            
        }
    }
}

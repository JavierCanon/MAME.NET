using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using cpu.m6800;

namespace mame
{
    public class Timer
    {
        public static List<emu_timer> lt;
        private static List<emu_timer2> lt2;
        public static Atime global_basetime;
        public static Atime global_basetime_obj;
        private static bool callback_timer_modified;
        private static emu_timer callback_timer;
        private static Atime callback_timer_expire_time;
        public delegate void timer_fired_func();
        public static Action setvector;
        public class emu_timer
        {
            public Action action;
            public string func;
            public bool enabled;
            public bool temporary;
            public Atime period;
            public Atime start;
            public Atime expire;
            public emu_timer()
            {

            }
        }
        public class emu_timer2
        {
            public int index;
            public Action action;
            public string func;
            public emu_timer2(int i1, Action ac1, string func1)
            {
                index = i1;
                action = ac1;
                func = func1;
            }
        }
        public static Action getactionbyindex(int index)
        {
            Action action = null;
            foreach (emu_timer2 timer in lt2)
            {
                if (timer.index == index)
                {
                    action = timer.action;
                    if (index == 4)
                    {
                        action = Sound.sound_update;
                    }
                    else if (index == 32)
                    {
                        action = M6800.action_rx;
                    }
                    else if (index == 33)
                    {
                        action = M6800.action_tx;
                    }
                    else if (index == 39)
                    {
                        action = setvector;
                    }
                    else if (index == 42)
                    {
                        action = Cpuexec.vblank_interrupt2;
                    }
                }
            }
            return action;
        }
        public static string getfuncbyindex(int index)
        {
            string func = "";
            foreach (emu_timer2 timer in lt2)
            {
                if (timer.index == index)
                {
                    func = timer.func;
                    break;
                }
            }
            return func;
        }
        public static int getindexbyfunc(string func)
        {
            int index = 0;
            foreach (emu_timer2 timer in lt2)
            {
                if (timer.func == func)
                {
                    index = timer.index;
                    break;
                }
            }
            return index;
        }
        public static void timer_init()
        {
            global_basetime = Attotime.ATTOTIME_ZERO;
            lt = new List<emu_timer>();
            lt2 = new List<emu_timer2>();
            lt2.Add(new emu_timer2(1, Video.vblank_begin_callback, "vblank_begin_callback"));
            lt2.Add(new emu_timer2(2, Mame.soft_reset, "soft_reset"));
            lt2.Add(new emu_timer2(3, Cpuint.cpunum_empty_event_queue, "cpunum_empty_event_queue"));
            lt2.Add(new emu_timer2(4, Sound.sound_update, "sound_update"));
            lt2.Add(new emu_timer2(5, Watchdog.watchdog_callback, "watchdog_callback"));
            lt2.Add(new emu_timer2(6, Generic.irq_1_0_line_hold, "irq_1_0_line_hold"));
            lt2.Add(new emu_timer2(7, Video.vblank_end_callback, "vblank_end_callback"));

            lt2.Add(new emu_timer2(10, YM2151.irqAon_callback, "irqAon_callback"));
            lt2.Add(new emu_timer2(11, YM2151.irqBon_callback, "irqBon_callback"));
            lt2.Add(new emu_timer2(12, YM2151.irqAoff_callback, "irqAoff_callback"));
            lt2.Add(new emu_timer2(13, YM2151.irqBoff_callback, "irqBoff_callback"));
            lt2.Add(new emu_timer2(14, YM2151.timer_callback_a, "timer_callback_a"));
            lt2.Add(new emu_timer2(15, YM2151.timer_callback_b, "timer_callback_b"));
            lt2.Add(new emu_timer2(16, Cpuexec.trigger_partial_frame_interrupt, "trigger_partial_frame_interrupt"));
            lt2.Add(new emu_timer2(17, Cpuexec.null_callback, "boost_callback"));
            lt2.Add(new emu_timer2(18, Cpuexec.end_interleave_boost, "end_interleave_boost"));
            lt2.Add(new emu_timer2(19, Video.scanline0_callback, "scanline0_callback"));
            lt2.Add(new emu_timer2(20, Sound.latch_callback, "latch_callback"));
            lt2.Add(new emu_timer2(21, Sound.latch_callback2, "latch_callback2"));
            lt2.Add(new emu_timer2(22, Sound.latch_callback3, "latch_callback3"));
            lt2.Add(new emu_timer2(23, Sound.latch_callback4, "latch_callback4"));
            lt2.Add(new emu_timer2(24, Neogeo.display_position_interrupt_callback, "display_position_interrupt_callback"));
            lt2.Add(new emu_timer2(25, Neogeo.display_position_vblank_callback, "display_position_vblank_callback"));
            lt2.Add(new emu_timer2(26, Neogeo.vblank_interrupt_callback, "vblank_interrupt_callback"));
            lt2.Add(new emu_timer2(27, Neogeo.auto_animation_timer_callback, "auto_animation_timer_callback"));
            lt2.Add(new emu_timer2(29, YM2610.F2610.timer_callback_0, "timer_callback_0"));
            lt2.Add(new emu_timer2(30, YM2610.F2610.timer_callback_1, "timer_callback_1"));
            lt2.Add(new emu_timer2(31, Neogeo.sprite_line_timer_callback, "sprite_line_timer_callback"));
            lt2.Add(new emu_timer2(32, M6800.action_rx, "m6800_rx_tick"));
            lt2.Add(new emu_timer2(33, M6800.action_tx, "m6800_tx_tick"));
            lt2.Add(new emu_timer2(34, YM3812.timer_callback_3812_0, "timer_callback_3812_0"));
            lt2.Add(new emu_timer2(35, YM3812.timer_callback_3812_1, "timer_callback_3812_1"));
            lt2.Add(new emu_timer2(36, ICS2115.timer_cb_0, "timer_cb_0"));
            lt2.Add(new emu_timer2(37, ICS2115.timer_cb_1, "timer_cb_1"));
            lt2.Add(new emu_timer2(38, M72.m72_scanline_interrupt, "m72_scanline_interrupt"));
            lt2.Add(new emu_timer2(39, setvector, "setvector_callback"));
            lt2.Add(new emu_timer2(40, M92.m92_scanline_interrupt, "m92_scanline_interrupt"));
            lt2.Add(new emu_timer2(41, Cpuexec.cpu_timeslicecallback, "cpu_timeslicecallback"));
            lt2.Add(new emu_timer2(42, Cpuexec.vblank_interrupt2, "vblank_interrupt2"));
            lt2.Add(new emu_timer2(43, Konami68000.nmi_callback, "nmi_callback"));
            lt2.Add(new emu_timer2(44, Upd7759.upd7759_slave_update, "upd7759_slave_update"));
            lt2.Add(new emu_timer2(45, Generic.irq_2_0_line_hold, "irq_2_0_line_hold"));
            lt2.Add(new emu_timer2(46, MSM5205.MSM5205_vclk_callback0, "msm5205_vclk_callback0"));
            lt2.Add(new emu_timer2(47, MSM5205.MSM5205_vclk_callback1, "msm5205_vclk_callback1"));
            lt2.Add(new emu_timer2(48, YM2203.timer_callback_2203_0_0, "timer_callback_2203_0_0"));
            lt2.Add(new emu_timer2(49, YM2203.timer_callback_2203_0_1, "timer_callback_2203_0_1"));
            lt2.Add(new emu_timer2(50, YM2203.timer_callback_2203_1_0, "timer_callback_2203_1_0"));
            lt2.Add(new emu_timer2(51, YM2203.timer_callback_2203_1_1, "timer_callback_2203_1_1"));
            lt2.Add(new emu_timer2(52, YM3812.timer_callback_3526_0, "timer_callback_3526_0"));
            lt2.Add(new emu_timer2(53, YM3812.timer_callback_3526_1, "timer_callback_3526_1"));
            lt2.Add(new emu_timer2(54, K054539.k054539_irq, "k054539_irq"));
            lt2.Add(new emu_timer2(55, Taito.cchip_timer, "cchip_timer"));
        }
        public static Atime get_current_time()
        {
            if (callback_timer != null)
            {
                return callback_timer_expire_time;
            }
            if (Cpuexec.activecpu >= 0 && Cpuexec.activecpu < Cpuexec.ncpu)
            {
                return Cpuexec.cpunum_get_localtime(Cpuexec.activecpu);
            }
            return global_basetime;
        }
        public static void timer_remove(emu_timer timer1)
        {
            if (timer1 == callback_timer)
            {
                callback_timer_modified = true;
            }
            timer_list_remove(timer1);
        }
        public static void timer_adjust_periodic(emu_timer which, Atime start_delay, Atime period)
        {
            Atime time = get_current_time();
            if (which == callback_timer)
            {
                callback_timer_modified = true;
            }
            which.enabled = true;
            if (start_delay.seconds < 0)
            {
                start_delay = Attotime.ATTOTIME_ZERO;
            }
            which.start = time;
            which.expire = Attotime.attotime_add(time, start_delay);
            which.period = period;
            timer_list_remove(which);
            timer_list_insert(which);
            if (lt.IndexOf(which) == 0)
            {
                if (Cpuexec.activecpu >= 0 && Cpuexec.activecpu < Cpuexec.ncpu)
                {
                    Cpuexec.activecpu_abort_timeslice(Cpuexec.activecpu);
                }
            }
        }
        public static void timer_pulse_internal(Atime period, Action action, string func)
        {
            emu_timer timer = timer_alloc_common(action, func, false);
            timer_adjust_periodic(timer, period, period);
        }
        public static void timer_set_internal(Action action, string func)
        {
            emu_timer timer = timer_alloc_common(action, func, true);
            timer_adjust_periodic(timer, Attotime.ATTOTIME_ZERO, Attotime.ATTOTIME_NEVER);
        }
        public static void timer_list_insert(emu_timer timer1)
        {
            int i;
            int i1 = -1;
            if (timer1.func == "cpunum_empty_event_queue" || timer1.func == "setvector_callback")
            {
                foreach (emu_timer et in lt)
                {
                    if (et.func == timer1.func && Attotime.attotime_compare(et.expire, global_basetime) <= 0)
                    {
                        i1 = lt.IndexOf(et);
                        break;
                    }
                }
            }
            if (i1 == -1)
            {
                Atime expire = timer1.enabled ? timer1.expire : Attotime.ATTOTIME_NEVER;
                for (i = 0; i < lt.Count; i++)
                {
                    if (Attotime.attotime_compare(lt[i].expire, expire) > 0)
                    {
                        break;
                    }
                }
                lt.Insert(i, timer1);
            }
        }
        public static void timer_list_remove(emu_timer timer1)
        {
            if (timer1.func == "cpunum_empty_event_queue" || timer1.func == "setvector_callback")
            {
                List<emu_timer> lt1 = new List<emu_timer>();
                foreach (emu_timer et in lt)
                {
                    if (et.func == timer1.func && Attotime.attotime_compare(et.expire, timer1.expire) == 0)
                    {
                        lt1.Add(et);
                        //lt.Remove(et);
                        //break;
                    }
                    else if (et.func == timer1.func && Attotime.attotime_compare(et.expire, timer1.expire) < 0)
                    {
                        int i1 = 1;
                    }
                    else if (et.func == timer1.func && Attotime.attotime_compare(et.expire, timer1.expire) > 0)
                    {
                        int i1 = 1;
                    }
                }
                foreach (emu_timer et1 in lt1)
                {
                    lt.Remove(et1);
                }
            }
            else
            {
                foreach (emu_timer et in lt)
                {
                    if (et.func == timer1.func)
                    {
                        lt.Remove(et);
                        break;
                    }
                }
            }
        }
        /*public static void sort()
        {
            int i1, i2, n1;
            Atime expire1, expire2;
            n1 = lt.Count;
            for (i2 = 1; i2 < n1; i2++)
            {
                for (i1 = 0; i1 < i2; i1++)
                {
                    if (lt[i1].enabled ==true)
                    {
                        expire1 = lt[i1].expire;
                    }
                    else
                    {
                        expire1 = Attotime.ATTOTIME_NEVER;
                    }
                    if (lt[i2].enabled == true)
                    {
                        expire2 = lt[i2].expire;
                    }
                    else
                    {
                        expire2 = Attotime.ATTOTIME_NEVER;
                    }
                    if (Attotime.attotime_compare(expire1, expire2) > 0)
                    {
                        var temp = lt[i1];
                        lt[i1] = lt[i2];
                        lt[i2] = temp;
                    }
                }
            }
        }*/
        public static void timer_set_global_time(Atime newbase)
        {
            emu_timer timer;
            global_basetime = newbase;
            while (Attotime.attotime_compare(lt[0].expire, global_basetime) <= 0)
            {
                bool was_enabled = lt[0].enabled;
                timer = lt[0];
                if (Attotime.attotime_compare(timer.period, Attotime.ATTOTIME_ZERO) == 0 || Attotime.attotime_compare(timer.period, Attotime.ATTOTIME_NEVER) == 0)
                {
                    timer.enabled = false;
                }
                callback_timer_modified = false;
                callback_timer = timer;
                callback_timer_expire_time = timer.expire;
                if (was_enabled && (timer.action != null && timer.action != Cpuexec.null_callback))
                {
                    timer.action();
                }
                callback_timer = null;
                if (callback_timer_modified == false)
                {
                    if (timer.temporary)
                    {
                        timer_list_remove(timer);
                    }
                    else
                    {
                        timer.start = timer.expire;
                        timer.expire = Attotime.attotime_add(timer.expire, timer.period);
                        timer_list_remove(timer);
                        timer_list_insert(timer);
                    }
                }
            }
        }
        public static emu_timer timer_alloc_common(Action action, string func, bool temp)
        {
            Atime time = get_current_time();
            emu_timer timer = new emu_timer();
            timer.action = action;
            timer.enabled = false;
            timer.temporary = temp;
            timer.period = Attotime.ATTOTIME_ZERO;
            timer.func = func;
            timer.start = time;
            timer.expire = Attotime.ATTOTIME_NEVER;
            timer_list_insert(timer);
            return timer;
        }
        public static bool timer_enable(emu_timer which, bool enable)
        {
            bool old;
            old = which.enabled;
            which.enabled = enable;
            timer_list_remove(which);
            timer_list_insert(which);
            return old;
        }
        public static bool timer_enabled(emu_timer which)
        {
            return which.enabled;
        }
        public static Atime timer_timeleft(emu_timer which)
        {
            return Attotime.attotime_sub(which.expire, get_current_time());
        }
        public static void SaveStateBinary(BinaryWriter writer)
        {
            int i, i1, n;
            n = lt.Count;
            writer.Write(n);
            for (i = 0; i < n; i++)
            {
                i1 = getindexbyfunc(lt[i].func);
                writer.Write(i1);
                writer.Write(lt[i].enabled);
                writer.Write(lt[i].temporary);
                writer.Write(lt[i].period.seconds);
                writer.Write(lt[i].period.attoseconds);
                writer.Write(lt[i].start.seconds);
                writer.Write(lt[i].start.attoseconds);
                writer.Write(lt[i].expire.seconds);
                writer.Write(lt[i].expire.attoseconds);
            }
            for (i = n; i < 32; i++)
            {
                writer.Write(0);
                writer.Write(false);
                writer.Write(false);
                writer.Write(0);
                writer.Write((long)0);
                writer.Write(0);
                writer.Write((long)0);
                writer.Write(0);
                writer.Write((long)0);
            }
        }
        public static void LoadStateBinary(BinaryReader reader)
        {
            int i, i1, n;
            n = reader.ReadInt32();
            lt = new List<emu_timer>();
            for (i = 0; i < n; i++)
            {
                lt.Add(new emu_timer());
                i1 = reader.ReadInt32();
                lt[i].action = getactionbyindex(i1);
                lt[i].func = getfuncbyindex(i1);
                lt[i].enabled = reader.ReadBoolean();
                lt[i].temporary = reader.ReadBoolean();
                lt[i].period.seconds = reader.ReadInt32();
                lt[i].period.attoseconds = reader.ReadInt64();
                lt[i].start.seconds = reader.ReadInt32();
                lt[i].start.attoseconds = reader.ReadInt64();
                lt[i].expire.seconds = reader.ReadInt32();
                lt[i].expire.attoseconds = reader.ReadInt64();
                if (lt[i].func == "vblank_begin_callback")
                {
                    Video.vblank_begin_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Video.vblank_begin_timer);
                }
                else if (lt[i].func == "vblank_end_callback")
                {
                    Video.vblank_end_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Video.vblank_end_timer);
                }
                else if (lt[i].func == "soft_reset")
                {
                    Mame.soft_reset_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Mame.soft_reset_timer);
                }
                else if (lt[i].func == "watchdog_callback")
                {
                    Watchdog.watchdog_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Watchdog.watchdog_timer);
                }
                else if (lt[i].func == "irq_1_0_line_hold")
                {
                    Cpuexec.timedint_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Cpuexec.timedint_timer);
                }
                else if (lt[i].func == "timer_callback_a")
                {
                    YM2151.PSG.timer_A = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(YM2151.PSG.timer_A);
                }
                else if (lt[i].func == "timer_callback_b")
                {
                    YM2151.PSG.timer_B = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(YM2151.PSG.timer_B);
                }
                else if (lt[i].func == "trigger_partial_frame_interrupt")
                {
                    switch (Machine.sBoard)
                    {
                        case "CPS2":
                        case "IGS011":
                        case "Konami68000":
                            Cpuexec.cpu[0].partial_frame_timer = lt[i];
                            lt.Remove(lt[i]);
                            lt.Add(Cpuexec.cpu[0].partial_frame_timer);
                            break;
                        case "M72":
                            Cpuexec.cpu[1].partial_frame_timer = lt[i];
                            lt.Remove(lt[i]);
                            lt.Add(Cpuexec.cpu[1].partial_frame_timer);
                            break;
                        case "Capcom":
                            switch (Machine.sName)
                            {
                                case "gng":
                                case "gnga":
                                case "gngbl":
                                case "gngprot":
                                case "gngblita":
                                case "gngc":
                                case "gngt":
                                case "makaimur":
                                case "makaimurc":
                                case "makaimurg":
                                case "diamond":
                                    Cpuexec.cpu[1].partial_frame_timer = lt[i];
                                    lt.Remove(lt[i]);
                                    lt.Add(Cpuexec.cpu[1].partial_frame_timer);
                                    break;
                            }
                            break;
                    }
                }
                else if (lt[i].func == "boost_callback")
                {
                    Cpuexec.interleave_boost_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Cpuexec.interleave_boost_timer);
                }
                else if (lt[i].func == "end_interleave_boost")
                {
                    Cpuexec.interleave_boost_timer_end = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Cpuexec.interleave_boost_timer_end);
                }
                else if (lt[i].func == "scanline0_callback")
                {
                    Video.scanline0_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Video.scanline0_timer);
                }
                else if (lt[i].func == "display_position_interrupt_callback")
                {
                    Neogeo.display_position_interrupt_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Neogeo.display_position_interrupt_timer);
                }
                else if (lt[i].func == "display_position_vblank_callback")
                {
                    Neogeo.display_position_vblank_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Neogeo.display_position_vblank_timer);
                }
                else if (lt[i].func == "vblank_interrupt_callback")
                {
                    Neogeo.vblank_interrupt_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Neogeo.vblank_interrupt_timer);
                }
                else if (lt[i].func == "auto_animation_timer_callback")
                {
                    Neogeo.auto_animation_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Neogeo.auto_animation_timer);
                }
                else if (lt[i].func == "sprite_line_timer_callback")
                {
                    Neogeo.sprite_line_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Neogeo.sprite_line_timer);
                }
                else if (lt[i].func == "timer_callback_0")
                {
                    YM2610.timer[0] = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(YM2610.timer[0]);
                }
                else if (lt[i].func == "timer_callback_1")
                {
                    YM2610.timer[1] = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(YM2610.timer[1]);
                }
                else if (lt[i].func == "m6800_rx_tick")
                {
                    M6800.m1.m6800_rx_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(M6800.m1.m6800_rx_timer);
                }
                else if (lt[i].func == "m6800_tx_tick")
                {
                    M6800.m1.m6800_tx_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(M6800.m1.m6800_tx_timer);
                }
                else if (lt[i].func == "timer_callback_3812_0")
                {
                    YM3812.timer[0] = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(YM3812.timer[0]);
                }
                else if (lt[i].func == "timer_callback_3812_1")
                {
                    YM3812.timer[1] = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(YM3812.timer[1]);
                }
                else if (lt[i].func == "timer_cb_0")
                {
                    ICS2115.timer[0].timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(ICS2115.timer[0].timer);
                }
                else if (lt[i].func == "timer_cb_1")
                {
                    ICS2115.timer[1].timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(ICS2115.timer[1].timer);
                }
                else if (lt[i].func == "m72_scanline_interrupt")
                {
                    M72.scanline_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(M72.scanline_timer);
                }
                else if (lt[i].func == "m92_scanline_interrupt")
                {
                    M92.scanline_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(M92.scanline_timer);
                }
                else if (lt[i].func == "cpu_timeslicecallback")
                {
                    Cpuexec.timeslice_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Cpuexec.timeslice_timer);
                }
                else if (lt[i].func == "upd7759_slave_update")
                {
                    Upd7759.chip.timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Upd7759.chip.timer);
                }
                else if (lt[i].func == "irq_2_0_line_hold")
                {
                    Cpuexec.timedint_timer = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(Cpuexec.timedint_timer);
                }
                else if (lt[i].func == "msm5205_vclk_callback0")
                {
                    MSM5205.timer[0] = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(MSM5205.timer[0]);
                }
                else if (lt[i].func == "msm5205_vclk_callback1")
                {
                    MSM5205.timer[1] = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(MSM5205.timer[1]);
                }
                else if (lt[i].func == "timer_callback_2203_0_0")
                {
                    YM2203.FF2203[0].timer[0] = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(YM2203.FF2203[0].timer[0]);
                }
                else if (lt[i].func == "timer_callback_2203_0_1")
                {
                    YM2203.FF2203[0].timer[1] = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(YM2203.FF2203[0].timer[1]);
                }
                else if (lt[i].func == "timer_callback_2203_1_0")
                {
                    YM2203.FF2203[1].timer[0] = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(YM2203.FF2203[1].timer[0]);
                }
                else if (lt[i].func == "timer_callback_2203_1_1")
                {
                    YM2203.FF2203[1].timer[1] = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(YM2203.FF2203[1].timer[1]);
                }
                else if (lt[i].func == "timer_callback_3526_0")
                {
                    YM3812.timer[0] = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(YM3812.timer[0]);
                }
                else if (lt[i].func == "timer_callback_3526_1")
                {
                    YM3812.timer[1] = lt[i];
                    lt.Remove(lt[i]);
                    lt.Add(YM3812.timer[1]);
                }
            }
            for (i = n; i < 32; i++)
            {
                reader.ReadInt32();
                reader.ReadBoolean();
                reader.ReadBoolean();
                reader.ReadInt32();
                reader.ReadInt64();
                reader.ReadInt32();
                reader.ReadInt64();
                reader.ReadInt32();
                reader.ReadInt64();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public class YM2610
    {
        public static Timer.emu_timer timer0, timer1;
        /* Timer overflow callback from timer.c */
        public static void timer_callback_0()
        {
            FM.ym2610_timer_over(0);
        }
        public static void timer_callback_1()
        {
            FM.ym2610_timer_over(1);
        }
        public static void timer_handler0(int count)
        {
            if (count == 0)
            {	/* Reset FM Timer */
                Timer.timer_enable(timer0, false);
            }
            else
            {	/* Start FM Timer */
                Atime period = Attotime.attotime_mul(new Atime(0, Attotime.ATTOSECONDS_PER_SECOND / 8000000), (uint)count);
                if (!Timer.timer_enable(timer0, true))
                {
                    Timer.timer_adjust_periodic(timer0, period, Attotime.ATTOTIME_NEVER);
                }
            }
        }
        public static void timer_handler1(int count)
        {
            if (count == 0)
            {	/* Reset FM Timer */
                Timer.timer_enable(timer1, false);
            }
            else
            {	/* Start FM Timer */
                Atime period = Attotime.attotime_mul(new Atime(0, Attotime.ATTOSECONDS_PER_SECOND / 8000000), (uint)count);
                if (!Timer.timer_enable(timer1, true))
                {
                    Timer.timer_adjust_periodic(timer1, period, Attotime.ATTOTIME_NEVER);
                }
            }
        }
        /* update request from fm.c */
        public static void ym2610_update_request()
        {
            Sound.ym2610stream.stream_update();
        }
        public static void ym2610_start()
        {
            AY8910.ay8910_start_ym();
            /* Timer Handler set */
            timer0 = Timer.timer_alloc_common(timer_callback_0, "timer_callback_0", false);
            timer1 = Timer.timer_alloc_common(timer_callback_1, "timer_callback_1", false);
            /**** initialize YM2610 ****/
            FM.ym2610_init();
        }
    }
}

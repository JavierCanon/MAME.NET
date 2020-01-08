using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace mame
{
    public class Wintime
    {
        [DllImport("kernel32.dll ")]
        public static extern bool QueryPerformanceCounter(ref long lpPerformanceCount);
        [DllImport("kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(ref long PerformanceFrequency);
        public static long ticks_per_second;
        public static void wintime_init()
        {
            long b = 0;
            QueryPerformanceFrequency(ref b);
            ticks_per_second = b;
        }
        public static long osd_ticks()
        {
            long a = 0;
            QueryPerformanceCounter(ref a);
            return a;
        }
        public static void osd_sleep(long duration)
        {
            int msec;
            msec = (int)(duration * 1000 / ticks_per_second);
            if (msec >= 2)
            {
                msec -= 2;
                Thread.Sleep(msec);
            }
        }
    }
}
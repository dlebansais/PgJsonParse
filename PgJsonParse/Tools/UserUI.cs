using System;
using System.Diagnostics;
using System.Threading;

namespace Tools
{
    public static class UserUI
    {
        public static void MinimalSleep(Stopwatch Watch)
        {
            MinimalSleep(Watch, TimeSpan.FromSeconds(4));
        }

        public static void MinimalSleep(Stopwatch Watch, TimeSpan MinimumTime)
        {
            TimeSpan Remaining = MinimumTime - Watch.Elapsed;
            if (Remaining > TimeSpan.Zero)
                Thread.Sleep(Remaining);
        }
    }
}

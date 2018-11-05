#if CSHTML5
using System;
using System.Diagnostics;
using System.Threading;

namespace Presentation
{
    public static class UserUI
    {
        public static void MinimalSleep(Stopwatch Watch)
        {
            MinimalSleep(Watch, TimeSpan.FromSeconds(4));
        }

        public static void MinimalSleep(Stopwatch Watch, TimeSpan MinimumTime)
        {
        }
    }
}
#else
using System;
using System.Diagnostics;
using System.Threading;

namespace Presentation
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
#endif
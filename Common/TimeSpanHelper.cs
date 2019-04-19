namespace System.Utilities.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public static class TimeSpanHelper
    {
        public static TimeSpan AddHours(this TimeSpan ts, int hours)
        {
            return ts.Add(new TimeSpan(hours, 0, 0));
        }

        public static TimeSpan AddMinutes(this TimeSpan ts, int minutes)
        {
            return ts.Add(new TimeSpan(0, minutes, 0));
        }

        public static TimeSpan AddSeconds(this TimeSpan ts, int seconds)
        {
            return ts.Add(new TimeSpan(0, 0, seconds));
        }
    }
}


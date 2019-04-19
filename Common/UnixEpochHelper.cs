namespace System.Utilities.Common
{
    using System;

    public class UnixEpochHelper
    {
        public static readonly DateTime UnixEpochUtcValue = new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime DateTimeFromUnixTimestampMillis(long millis)
        {
            return UnixEpochUtcValue.AddMilliseconds((double) millis);
        }

        public static long GetCurrentUnixTimestampMillis()
        {
            TimeSpan span = (TimeSpan) (DateTime.UtcNow - UnixEpochUtcValue);
            return (long) span.TotalMilliseconds;
        }
    }
}


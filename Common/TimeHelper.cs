namespace System.Utilities.Common
{
    using System;
    using System.Globalization;

    public static class TimeHelper
    {
        public static string DateDiff(DateTime startTime, DateTime endTime)
        {
            TimeSpan span = (TimeSpan) (endTime - startTime);
            if (span.Days >= 1)
            {
                return (startTime.Month.ToString() + "月" + startTime.Day.ToString() + "日");
            }
            if (span.Hours > 1)
            {
                return (span.Hours.ToString() + "小时前");
            }
            return (span.Minutes.ToString() + "分钟前");
        }

        public static string FormatDate(DateTime dateTime, string dateMode)
        {
            switch (dateMode)
            {
                case "0":
                    return dateTime.ToString("yyyy-MM-dd");

                case "1":
                    return dateTime.ToString("yyyy-MM-dd HH:mm:ss");

                case "2":
                    return dateTime.ToString("yyyy/MM/dd");

                case "3":
                    return dateTime.ToString("yyyy年MM月dd日");

                case "4":
                    return dateTime.ToString("MM-dd");

                case "5":
                    return dateTime.ToString("MM/dd");

                case "6":
                    return dateTime.ToString("MM月dd日");

                case "7":
                    return dateTime.ToString("yyyy-MM");

                case "8":
                    return dateTime.ToString("yyyy/MM");

                case "9":
                    return dateTime.ToString("yyyy年MM月");
            }
            return dateTime.ToString();
        }

        public static int GetMonthLastDate(int year, int month)
        {
            DateTime time = new DateTime(year, month, new GregorianCalendar().GetDaysInMonth(year, month));
            return time.Day;
        }

        public static DateTime GetRandomTime(DateTime timeStart, DateTime timeEnd)
        {
            Random random = new Random();
            DateTime time = new DateTime();
            TimeSpan span = new TimeSpan(timeStart.Ticks - timeEnd.Ticks);
            double totalSeconds = span.TotalSeconds;
            int num2 = 0;
            if (totalSeconds > 2147483647.0)
            {
                num2 = 0x7fffffff;
            }
            else if (totalSeconds < -2147483648.0)
            {
                num2 = -2147483648;
            }
            else
            {
                num2 = (int) totalSeconds;
            }
            if (num2 > 0)
            {
                time = timeEnd;
            }
            else if (num2 < 0)
            {
                time = timeStart;
            }
            else
            {
                return timeStart;
            }
            int num3 = num2;
            if (num2 <= -2147483648)
            {
                num3 = -2147483647;
            }
            int num4 = random.Next(Math.Abs(num3));
            return time.AddSeconds((double) num4);
        }

        public static int SecondToMinute(int second)
        {
            decimal d = second / 60M;
            return Convert.ToInt32(Math.Ceiling(d));
        }
    }
}


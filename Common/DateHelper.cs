namespace System.Utilities.Common
{
    using System.Utilities.Enums;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public static class DateHelper
    {
        public static int CountWeekdays(DateTime startTime, DateTime endTime)
        {
            TimeSpan span = (TimeSpan) (endTime - startTime);
            int num = 0;
            for (int i = 0; i < span.Days; i++)
            {
                if (startTime.AddDays((double) i).IsWeekDay())
                {
                    num++;
                }
            }
            return num;
        }

        public static int CountWeekends(DateTime startTime, DateTime endTime)
        {
            TimeSpan span = (TimeSpan) (endTime - startTime);
            int num = 0;
            for (int i = 0; i < span.Days; i++)
            {
                if (startTime.AddDays((double) i).IsWeekEnd())
                {
                    num++;
                }
            }
            return num;
        }

        public static int DateDiff(this DateTime startTime, DateTime endTime, DatePart part)
        {
            switch (part)
            {
                case DatePart.year:
                    return (endTime.Year - startTime.Year);

                case DatePart.month:
                    return (((endTime.Year - startTime.Year) * 12) + (endTime.Month - startTime.Month));

                case DatePart.day:
                {
                    TimeSpan span = (TimeSpan) (endTime - startTime);
                    return (int) span.TotalDays;
                }
                case DatePart.hour:
                {
                    TimeSpan span2 = (TimeSpan) (endTime - startTime);
                    return (int) span2.TotalHours;
                }
                case DatePart.minute:
                {
                    TimeSpan span3 = (TimeSpan) (endTime - startTime);
                    return (int) span3.TotalMinutes;
                }
                case DatePart.second:
                {
                    TimeSpan span4 = (TimeSpan) (endTime - startTime);
                    return (int) span4.TotalSeconds;
                }
            }
            return 0;
        }

        public static int GetDays(this DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }

        public static int GetWeekNumber(this DateTime date)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        public static bool IsWeekDay(this DateTime dt)
        {
            return (Convert.ToInt16(dt.DayOfWeek) < 6);
        }

        public static bool IsWeekEnd(this DateTime dt)
        {
            return (Convert.ToInt16(dt.DayOfWeek) > 5);
        }

        public static DateTime ParseExact(this string data, string format)
        {
            DateTime time = new DateTime();
            if (string.IsNullOrEmpty(data))
            {
                return time;
            }
            try
            {
                return DateTime.ParseExact(data, format, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }
    }
}


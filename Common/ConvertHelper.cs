namespace System.Utilities.Common
{
    using System.Utilities.Enums;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public static class ConvertHelper
    {
        public static bool ToBoolean(this object data, bool errorValue)
        {
            if (data != null)
            {
                bool result = false;
                if (bool.TryParse(data.ToString(), out result))
                {
                    return result;
                }
            }
            return errorValue;
        }

        public static byte ToByte(this object data, byte errorValue)
        {
            if (data != null)
            {
                byte result = 0;
                if (byte.TryParse(data.ToString(), out result))
                {
                    return result;
                }
            }
            return errorValue;
        }

        public static string ToChineseDate(this DateTime date)
        {
            ChineseLunisolarCalendar calendar = new ChineseLunisolarCalendar();
            string[] strArray = new string[] { "", "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "冬月", "腊月" };
            string[] strArray2 = new string[] { 
                "", "初一", "初二", "初三", "初四", "初五", "初六", "初七", "初八", "初九", "初十", "十一", "十二", "十三", "十四", "十五",
                "十六", "十七", "十八", "十九", "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十"
            };
            string[] strArray3 = new string[] { 
                "", "甲子", "乙丑", "丙寅", "丁卯", "戊辰", "己巳", "庚午", "辛未", "壬申", "癸酉", "甲戌", "乙亥", "丙子", "丁丑", "戊寅",
                "己卯", "庚辰", "辛己", "壬午", "癸未", "甲申", "乙酉", "丙戌", "丁亥", "戊子", "己丑", "庚寅", "辛卯", "壬辰", "癸巳", "甲午",
                "乙未", "丙申", "丁酉", "戊戌", "己亥", "庚子", "辛丑", "壬寅", "癸丑", "甲辰", "乙巳", "丙午", "丁未", "戊申", "己酉", "庚戌",
                "辛亥", "壬子", "癸丑", "甲寅", "乙卯", "丙辰", "丁巳", "戊午", "己未", "庚申", "辛酉", "壬戌", "癸亥"
            };
            int year = calendar.GetYear(date);
            string str = strArray3[calendar.GetSexagenaryYear(date)];
            int month = calendar.GetMonth(date);
            int dayOfMonth = calendar.GetDayOfMonth(date);
            int leapMonth = calendar.GetLeapMonth(year);
            string str2 = strArray[month];
            if (leapMonth > 0)
            {
                str2 = (month == leapMonth) ? string.Format("闰{0}", strArray[month - 1]) : str2;
                str2 = (month > leapMonth) ? strArray[month - 1] : str2;
            }
            return string.Format("{0}年{1}{2}", str, str2, strArray2[dayOfMonth]);
        }

        public static string ToChineseDay(int data)
        {
            if ((data == 0) || (data > 0x20))
            {
                return string.Empty;
            }
            string[] strArray = new string[] { 
                "〇", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二", "十三", "十四", "十五",
                "十六", "十七", "十八", "十九", "廿十", "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十", "三十一"
            };
            return strArray[data];
        }

        public static string ToChineseMonth(this int data)
        {
            if ((data != 0) && (data <= 12))
            {
                string[] strArray = new string[] { "〇", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二" };
                return strArray[data];
            }
            return string.Empty;
        }

        public static DateTime ToDate(this object data, DateTime errorValue)
        {
            DateTime time;
            if (data == null)
            {
                return errorValue;
            }
            if (!DateTime.TryParse(data.ToString(), out time))
            {
                return errorValue;
            }
            return time;
        }

        public static decimal ToDecimal(this object data, decimal errorValue)
        {
            if (data == null)
            {
                return errorValue;
            }
            decimal result = 0M;
            if (!decimal.TryParse(data.ToString(), out result))
            {
                return errorValue;
            }
            return result;
        }

        public static double ToDouble(this object data, double errorValue)
        {
            if (data == null)
            {
                return errorValue;
            }
            double result = 0.0;
            if (!double.TryParse(data.ToString(), out result))
            {
                return errorValue;
            }
            return result;
        }

        public static string ToHexBinDecOct(this string data, Conversion from, Conversion to)
        {
            try
            {
                string str = Convert.ToString(Convert.ToInt32(data, (int) from), (int) to);
                if (to == Conversion.Binary)
                {
                    str = str.ComplementLeftZero(8);
                }
                return str;
            }
            catch
            {
                return "0";
            }
        }

        public static int ToInt(this object data, int errorData)
        {
            if (data == null)
            {
                return errorData;
            }
            int result = 0;
            if (!int.TryParse(data.ToString(), out result))
            {
                return errorData;
            }
            return result;
        }

        public static short ToInt16(this object data, short errorData)
        {
            if (data == null)
            {
                return errorData;
            }
            short result = 0;
            if (!short.TryParse(data.ToString(), out result))
            {
                return errorData;
            }
            return result;
        }

        public static int ToInt32(this object data, int errorValue)
        {
            if (data == null)
            {
                return errorValue;
            }
            int result = 0;
            if (!int.TryParse(data.ToString(), out result))
            {
                return errorValue;
            }
            return result;
        }

        public static long ToInt64(this object data, long errorValue)
        {
            if (data == null)
            {
                return errorValue;
            }
            long result = 0L;
            if (!long.TryParse(data.ToString(), out result))
            {
                return errorValue;
            }
            return result;
        }

        public static string ToString(this object data, string errorValue)
        {
            if (data != null)
            {
                return data.ToString();
            }
            return errorValue;
        }

        public static string ToString<T>(this T[] array, string delimiter)
        {
            string[] strArray = Array.ConvertAll<T, string>(array, n => n.ToString());
            return string.Join(delimiter, strArray);
        }

        public static string ToString(this DateTime data, string formartString, string errorValue)
        {
            if (data != new DateTime())
            {
                return data.ToString(formartString);
            }
            return errorValue;
        }

        public static T ToStringBase<T>(this string data)
        {
            T local = default(T);
            if (!string.IsNullOrEmpty(data))
            {
                local = (T) TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(data);
            }
            return local;
        }
    }
}


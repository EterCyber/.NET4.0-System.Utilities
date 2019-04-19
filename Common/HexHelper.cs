namespace System.Utilities.Common
{
    using System;
    using System.Globalization;

    public class HexHelper
    {
        private static decimal dHexValue = 255M;

        public static decimal HexToPercent(string hex)
        {
            int result = -1;
            if (!int.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result))
            {
                throw new Exception("百分比转十六进制：十六进制值转换失败。");
            }
            return Math.Round((decimal) ((result / dHexValue) * 100M), 2);
        }

        public static string PercentToHex(decimal number, int capacity)
        {
            double num = ((double) number) / 100.0;
            double num2 = Math.Round((double) (num * 255.0), 0);
            return string.Format("{0:x}", (int) num2).PadLeft(capacity, '0');
        }

        public static decimal ToDecimal(string hexString)
        {
            return Convert.ToInt64(hexString, 0x10);
        }

        public static string ToHexString(int number)
        {
            string str = string.Format("{0:X}", number);
            if ((str.Length % 2) != 0)
            {
                str = string.Format("0{0}", str);
            }
            return str;
        }

        public static string ToHexString(int number, int capacity)
        {
            return ToHexString(number).PadLeft(capacity, '0');
        }

        public static int ToInt(string hexString)
        {
            return int.Parse(hexString, NumberStyles.AllowHexSpecifier);
        }
    }
}


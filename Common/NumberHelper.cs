namespace System.Utilities.Common
{
    using System;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;

    public static class NumberHelper
    {
        public static string AutoIncrementIndex(string Input)
        {
            int startIndex = 0;
            int num2 = 1;
            foreach (char ch in Input)
            {
                if (!char.IsNumber(ch))
                {
                    startIndex = num2;
                }
                num2++;
            }
            string str = (startIndex == Input.Length) ? "" : Input.Substring(startIndex, Input.Length - startIndex);
            if (str != "")
            {
                long num4 = Convert.ToInt64(str) + 1L;
                return string.Format("{0}{1}", Input.Remove(startIndex), num4.ToString().PadLeft(Input.Length - startIndex, '0'));
            }
            return string.Format("{0}1", Input);
        }

        public static byte GetByte(object src)
        {
            byte result = 0;
            if (src != null)
            {
                byte.TryParse(src.ToString(), out result);
            }
            return result;
        }

        public static byte GetByte(object src, byte defaultVal)
        {
            byte result = defaultVal;
            if ((src != null) && !byte.TryParse(src.ToString(), out result))
            {
                result = defaultVal;
            }
            return result;
        }

        public static decimal GetDecimal(object src)
        {
            decimal result = 0M;
            if (src != null)
            {
                decimal.TryParse(src.ToString(), out result);
            }
            return result;
        }

        public static double GetDouble(object src)
        {
            double result = 0.0;
            if (src != null)
            {
                double.TryParse(src.ToString(), out result);
            }
            return result;
        }

        public static int GetVal(object src)
        {
            int result = 0;
            if (src != null)
            {
                int.TryParse(src.ToString(), NumberStyles.AllowThousands | NumberStyles.AllowTrailingSign | NumberStyles.Integer, CultureInfo.CurrentUICulture, out result);
            }
            return result;
        }

        public static int GetVal(object src, int defaultVal)
        {
            int result = 0;
            if ((src != null) && !int.TryParse(src.ToString(), out result))
            {
                result = defaultVal;
            }
            return result;
        }

        public static bool IsEven(int value)
        {
            return ((value & 1) == 0);
        }

        public static bool IsInteger(string item)
        {
            Regex regex = new Regex("[^0-9-]");
            Regex regex2 = new Regex("^-[0-9]+$|^[0-9]+$");
            return (!regex.IsMatch(item) && regex2.IsMatch(item));
        }

        public static bool IsNaturalNumber(string item)
        {
            Regex regex = new Regex("[^0-9]");
            Regex regex2 = new Regex("0*[1-9][0-9]*");
            return (!regex.IsMatch(item) && regex2.IsMatch(item));
        }

        public static bool IsNumber(string item)
        {
            double num;
            return double.TryParse(item, NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out num);
        }

        public static bool IsOdd(int value)
        {
            return ((value & 1) == 1);
        }

        public static bool IsWholeNumber(string item)
        {
            Regex regex = new Regex("[^0-9]");
            return !regex.IsMatch(item);
        }

        public static double Random()
        {
            return new System.Random().NextDouble();
        }

        public static int Random(bool noZeros)
        {
            byte[] data = new byte[1];
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            if (noZeros)
            {
                provider.GetNonZeroBytes(data);
            }
            else
            {
                provider.GetBytes(data);
            }
            return data[0];
        }

        public static int Random(int high)
        {
            byte[] data = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Math.Abs((int) (BitConverter.ToInt32(data, 0) % high));
        }

        public static int Random(int low, int high)
        {
            return new System.Random().Next(low, high);
        }

        public static string ToPercent(int number, int dividendValue)
        {
            if (dividendValue > 0)
            {
                float num = (((float) number) / ((float) dividendValue)) * 100f;
                return (num.ToString("0.00 ") + "% ");
            }
            return "0.00 %";
        }
    }
}


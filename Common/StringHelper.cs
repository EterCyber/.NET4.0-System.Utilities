namespace System.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class StringHelper
    {
        public static string BuilderDelimiter(this string data, char delimiter)
        {
            if (string.IsNullOrEmpty(data))
            {
                return string.Empty;
            }
            char[] chArray = data.ToCharArray();
            StringBuilder builder = new StringBuilder();
            foreach (char ch in chArray)
            {
                builder.AppendFormat("{0}{1}", ch, delimiter);
            }
            string str = builder.ToString();
            int length = str.LastIndexOf(delimiter);
            if (length != -1)
            {
                str = str.Substring(0, length);
            }
            return str;
        }

        public static string ComplementLeftZero(this string data, int targetLength)
        {
            if (data.Length >= targetLength)
            {
                return data;
            }
            StringBuilder builder = new StringBuilder(targetLength);
            for (int i = 0; i < (targetLength - data.Length); i++)
            {
                builder.Append("0");
            }
            builder.Append(data);
            return builder.ToString();
        }

        public static string ComplementRigthZero(this string data, int targetLength)
        {
            if (data.Length >= targetLength)
            {
                return data;
            }
            StringBuilder builder = new StringBuilder(targetLength);
            builder.Append(data);
            for (int i = 0; i < (targetLength - data.Length); i++)
            {
                builder.Append("0");
            }
            return builder.ToString();
        }

        public static string DateTimeBCDFormart(DateTime dateTime)
        {
            string str = dateTime.Second.ToString().PadLeft(2, '0');
            string str2 = dateTime.Minute.ToString().PadLeft(2, '0');
            string str3 = dateTime.Hour.ToString().PadLeft(2, '0');
            string str4 = dateTime.Day.ToString().PadLeft(2, '0');
            string str5 = dateTime.Month.ToString().PadLeft(2, '0');
            string str6 = dateTime.Year.ToString();
            string str7 = string.Format("{0}{1}", str6.Substring(2, 2), str6.Substring(0, 2));
            return string.Format("{0}{1}{2}{3}{4}{5}", new object[] { str, str2, str3, str4, str5, str7 });
        }

        public static bool EqualLength(this string data, int length)
        {
            bool flag = false;
            if (data != null)
            {
                flag = data.Length == length;
            }
            return flag;
        }

        public static string ExceptBlanks(this string data)
        {
            int length = data.Length;
            if (length <= 0)
            {
                return data;
            }
            StringBuilder builder = new StringBuilder(length);
            for (int i = 0; i < data.Length; i++)
            {
                char c = data[i];
                if (!char.IsWhiteSpace(c))
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }

        public static string ExchangeHightToLow(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return data;
            }
            data = data.Trim();
            if (data.Length != 4)
            {
                throw new ArgumentException(string.Format("四位长度的字符串高低位互换，需要参数长度为四位，而传入参数：{0}，长度为：{1}。", data, data.Length));
            }
            return string.Format("{0}{1}", data.Substring(2, 2), data.Substring(0, 2));
        }

        public static string FormatDateTimeBCD(this string dateTime)
        {
            dateTime = dateTime.Trim();
            if (dateTime.Length != 14)
            {
                throw new ArgumentException("需要14个长度的时间类型字符串！");
            }
            string str = dateTime.Substring(0, 2);
            string str2 = dateTime.Substring(2, 2);
            string str3 = dateTime.Substring(4, 2);
            string str4 = dateTime.Substring(6, 2);
            string str5 = dateTime.Substring(8, 2);
            string str6 = dateTime.Substring(10, 2);
            string str7 = dateTime.Substring(12, 2);
            return string.Format("{0}{1}-{2}-{3} {4}：{5}：{6}", new object[] { str7, str6, str5, str4, str3, str2, str });
        }

        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, format, args);
        }

        public static string InsertCharAtDividedPosition(this string data, int count, string character)
        {
            int num = 0;
            while (((++num * count) + (num - 1)) < data.Length)
            {
                data = data.Insert((num * count) + (num - 1), character);
            }
            return data;
        }

        public static string LoopString(this string data, Func<char, char> loopFactory)
        {
            if (string.IsNullOrEmpty(data))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (char ch in data.ToCharArray())
            {
                builder.Append(loopFactory(ch));
            }
            return builder.ToString();
        }

        public static bool MaxThanDecimal(string decimalData, out decimal val, int decimalLen)
        {
            if (!decimal.TryParse(decimalData, out val))
            {
                return false;
            }
            int num = decimalData.LastIndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
            return (((num != -1) && (num <= decimalLen)) || ((num == -1) && (decimalData.Length <= decimalLen)));
        }

        public static int ParseThousandthString(this string data)
        {
            int num = -1;
            if (string.IsNullOrEmpty(data))
            {
                return num;
            }
            try
            {
                return int.Parse(data, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static string Remove(this string data, char delimiter)
        {
            if (!string.IsNullOrEmpty(data))
            {
                int index = data.IndexOf(delimiter);
                if (index != -1)
                {
                    data = data.Substring(0, index);
                }
            }
            return data;
        }

        public static string RemoveLast(this string data, char delimiter)
        {
            if (!string.IsNullOrEmpty(data))
            {
                int length = data.LastIndexOf(delimiter);
                if (length != -1)
                {
                    data = data.Substring(0, length);
                }
            }
            return data;
        }

        public static string ReplaceAt(this string data, int index, char replace)
        {
            StringBuilder builder = new StringBuilder(data);
            builder[index] = replace;
            return builder.ToString();
        }

        public static string ReplaceAt(this string data, int index, int length, string replace)
        {
            StringBuilder builder = new StringBuilder(data);
            builder.Remove(index, length);
            builder.Insert(index, replace);
            return builder.ToString();
        }

        public static string ReplaceFirstOccurrence(this string data, string find, string replace)
        {
            int index = data.IndexOf(find);
            return data.Remove(index, find.Length).Insert(index, replace);
        }

        public static string ReplaceLastOccurrence(this string data, string find, string replace)
        {
            int startIndex = data.LastIndexOf(find);
            return data.Remove(startIndex, find.Length).Insert(startIndex, replace);
        }

        public static string Reverse(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                char[] array = data.ToCharArray();
                Array.Reverse(array);
                return new string(array);
            }
            return data;
        }

        public static string ReverseUsingArrayClass(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                char[] array = data.ToCharArray();
                Array.Reverse(array);
                return new string(array);
            }
            return data;
        }

        public static string ReverseUsingCharacterBuffer(this string data)
        {
            if (data.Length <= 0)
            {
                return data;
            }
            char[] chArray = new char[data.Length];
            int num = data.Length - 1;
            for (int i = 0; i <= num; i++)
            {
                chArray[i] = data[num - i];
            }
            return new string(chArray);
        }

        public static string ReverseUsingStack(this string data)
        {
            if (data.Length <= 0)
            {
                return data;
            }
            Stack<char> stack = new Stack<char>();
            foreach (char ch in data)
            {
                stack.Push(ch);
            }
            StringBuilder builder = new StringBuilder();
            while (stack.Count > 0)
            {
                builder.Append(stack.Pop());
            }
            return builder.ToString();
        }

        public static string ReverseUsingStringBuilder(this string data)
        {
            if (data.Length <= 0)
            {
                return data;
            }
            StringBuilder builder = new StringBuilder(data.Length);
            for (int i = data.Length - 1; i >= 0; i--)
            {
                builder.Append(data[i]);
            }
            return builder.ToString();
        }

        public static string ReverseUsingXOR(this string data)
        {
            if (data.Length <= 0)
            {
                return data;
            }
            char[] chArray = data.ToCharArray();
            int index = data.Length - 1;
            int num2 = 0;
            while (num2 < index)
            {
                chArray[num2] = (char) (chArray[num2] ^ chArray[index]);
                chArray[index] = (char) (chArray[index] ^ chArray[num2]);
                chArray[num2] = (char) (chArray[num2] ^ chArray[index]);
                num2++;
                index--;
            }
            return new string(chArray);
        }

        public static string Substring(this string data, int maxLen)
        {
            if ((maxLen > 0) && (data.Length > maxLen))
            {
                return (data.Substring(0, maxLen) + "...");
            }
            return data;
        }

        public static string ToMD5(this string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return string.Empty;
            }
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] buffer2 = provider.ComputeHash(bytes);
            provider.Clear();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer2.Length; i++)
            {
                builder.Append(buffer2[i].ToString("X").PadLeft(2, '0'));
            }
            return builder.ToString().ToLower();
        }

        public static string ValidIndex(this string data, int offset, Predicate<char> validFactory)
        {
            if (string.IsNullOrEmpty(data))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            char[] chArray = data.ToCharArray();
            int num = 0;
            foreach (char ch in chArray)
            {
                if (validFactory(ch))
                {
                    builder.Append((int) (offset + num));
                }
                num++;
            }
            return builder.ToString();
        }

        public static string WrapText(this string data, int maxWidth)
        {
            int length = data.Length;
            if ((maxWidth <= 0) || (length <= maxWidth))
            {
                return data;
            }
            StringBuilder builder = new StringBuilder(data);
            int num2 = builder.Length / maxWidth;
            for (int i = 0; i < num2; i++)
            {
                int num4 = i * maxWidth;
                if (num4 != 0)
                {
                    int num5 = (i - 1) * 2;
                    builder.Insert(num4 + num5, Environment.NewLine);
                }
            }
            return builder.ToString();
        }
    }
}


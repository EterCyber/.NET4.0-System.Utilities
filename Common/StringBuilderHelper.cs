namespace System.Utilities.Common
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class StringBuilderHelper
    {
        public static void Clear(this StringBuilder builder)
        {
            if (builder != null)
            {
                builder.Length = 0;
                builder.Capacity = 0;
            }
        }

        public static StringBuilder NullOrCreate(this StringBuilder builder)
        {
            if (builder == null)
            {
                builder = new StringBuilder();
            }
            return builder;
        }

        public static string SubString(this StringBuilder sb, int start, int length)
        {
            if ((start + length) > sb.Length)
            {
                throw new IndexOutOfRangeException("超出字符串索引长度");
            }
            char[] chArray = new char[length];
            for (int i = 0; i < length; i++)
            {
                chArray[i] = sb[start + i];
            }
            return new string(chArray);
        }

        public static StringBuilder Trim(this StringBuilder sb)
        {
            if (sb.Length == 0)
            {
                return sb;
            }
            return sb.TrimEnd().TrimStart();
        }

        public static StringBuilder TrimEnd(this StringBuilder sb)
        {
            return sb.TrimEnd(' ');
        }

        public static StringBuilder TrimEnd(this StringBuilder sb, char c)
        {
            if (sb.Length != 0)
            {
                while (c.Equals(sb[sb.Length - 1]))
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                return sb;
            }
            return sb;
        }

        public static StringBuilder TrimEnd(this StringBuilder sb, char[] chars)
        {
            return sb.TrimEnd(new string(chars));
        }

        public static StringBuilder TrimEnd(this StringBuilder sb, string str)
        {
            if ((!string.IsNullOrEmpty(str) && (sb.Length != 0)) && (str.Length <= sb.Length))
            {
                while (sb.SubString((sb.Length - str.Length), str.Length).Equals(str))
                {
                    sb.Remove(sb.Length - str.Length, str.Length);
                    if (sb.Length < str.Length)
                    {
                        return sb;
                    }
                }
                return sb;
            }
            return sb;
        }

        public static StringBuilder TrimStart(this StringBuilder sb)
        {
            return sb.TrimStart(' ');
        }

        public static StringBuilder TrimStart(this StringBuilder sb, char c)
        {
            if (sb.Length != 0)
            {
                while (c.Equals(sb[0]))
                {
                    sb.Remove(0, 1);
                }
                return sb;
            }
            return sb;
        }

        public static StringBuilder TrimStart(this StringBuilder sb, char[] cs)
        {
            return sb.TrimStart(new string(cs));
        }

        public static StringBuilder TrimStart(this StringBuilder sb, string str)
        {
            if ((!string.IsNullOrEmpty(str) && (sb.Length != 0)) && (str.Length <= sb.Length))
            {
                while (sb.SubString(0, str.Length).Equals(str))
                {
                    sb.Remove(0, str.Length);
                    if (str.Length > sb.Length)
                    {
                        return sb;
                    }
                }
                return sb;
            }
            return sb;
        }
    }
}


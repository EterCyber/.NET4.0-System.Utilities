namespace System.Utilities.Common
{
    using System.Utilities.Enums;
    using System;
    using System.Text;

    public class ByteHelper
    {
        public static byte BinaryStringToByte(string binary)
        {
            return Convert.ToByte(binary, 2);
        }

        public static byte[] BinaryStringToBytes(string binaryString)
        {
            binaryString = binaryString.Replace(" ", "");
            int num = binaryString.Length / 8;
            byte[] buffer = new byte[num];
            for (int i = 0; i < num; i++)
            {
                buffer[i] = Convert.ToByte(binaryString.Substring(8 * i, 8), 2);
            }
            return buffer;
        }

        public static DateTime BytesInvertedToDT(byte[] bytes, int startIndex, int endIndex)
        {
            byte[] buffer = ArrayHelper.Copy<byte>(bytes, startIndex, endIndex);
            if ((buffer != null) && ((buffer.Length == 6) || (buffer.Length == 7)))
            {
                int second = Convert.ToInt16(buffer[0].ToString("X2"));
                int minute = Convert.ToInt16(buffer[1].ToString("X2"));
                int hour = Convert.ToInt16(buffer[2].ToString("X2"));
                int day = Convert.ToInt16(buffer[3].ToString("X2"));
                int month = Convert.ToInt16(buffer[4].ToString("X2"));
                return new DateTime(0x7d0 + Convert.ToInt16(buffer[5].ToString("X2")), month, day, hour, minute, second);
            }
            int length = buffer.Length;
            throw new Exception("时间格式应该6或7个字节！错误：" + length.ToString());
        }

        public static string BytesLen2ToTime(byte[] bytes, int startIndex, int endIndex)
        {
            string str = string.Empty;
            byte[] buffer = ArrayHelper.Copy<byte>(bytes, startIndex, endIndex);
            if ((buffer != null) && (buffer.Length == 2))
            {
                int num = Convert.ToInt16(buffer[0].ToString("X2"));
                int num2 = Convert.ToInt16(buffer[1].ToString("X2"));
                str = string.Format("{0}：{1}", num, num2);
            }
            return str;
        }

        public static DateTime BytesLen4ToDT(byte[] bytes, int startIndex, int endIndex)
        {
            DateTime time = new DateTime();
            byte[] buffer = ArrayHelper.Copy<byte>(bytes, startIndex, endIndex);
            if ((buffer != null) && (buffer.Length == 4))
            {
                int year = Convert.ToInt16(Convert.ToInt16(buffer[0].ToString("X2")).ToString() + Convert.ToInt16(buffer[1].ToString("X2")).ToString());
                int month = Convert.ToInt16(buffer[2].ToString("X2"));
                int day = Convert.ToInt16(buffer[3].ToString("X2"));
                time = new DateTime(year, month, day);
            }
            return time;
        }

        public static DateTime BytesLen6ToDT(byte[] bytes, int startIndex, int endIndex)
        {
            byte[] buffer = ArrayHelper.Copy<byte>(bytes, startIndex, endIndex);
            if ((buffer != null) && (buffer.Length == 6))
            {
                int minute = Convert.ToInt16(buffer[5].ToString("X2"));
                int hour = Convert.ToInt16(buffer[4].ToString("X2"));
                int day = Convert.ToInt16(buffer[3].ToString("X2"));
                return new DateTime(Convert.ToInt16(Convert.ToInt16(buffer[0].ToString("X2")).ToString() + Convert.ToInt16(buffer[1].ToString("X2")).ToString()), Convert.ToInt16(buffer[2].ToString("X2")), day, hour, minute, 0);
            }
            int length = bytes.Length;
            throw new Exception("时间格式应该6个字节！错误：" + length.ToString());
        }

        public static DateTime BytesLen7ToDT(byte[] bytes, int startIndex, int endIndex)
        {
            if ((endIndex - startIndex) != 7)
            {
                throw new ArgumentException(string.Format("{0}与{1}相差不等于7", startIndex, endIndex));
            }
            byte[] buffer = ArrayHelper.Copy<byte>(bytes, startIndex, endIndex);
            if ((buffer != null) && (buffer.Length == 7))
            {
                int second = Convert.ToInt16(buffer[6].ToString("X2"));
                int minute = Convert.ToInt16(buffer[5].ToString("X2"));
                int hour = Convert.ToInt16(buffer[4].ToString("X2"));
                int day = Convert.ToInt16(buffer[3].ToString("X2"));
                return new DateTime(Convert.ToInt16(Convert.ToInt16(buffer[0].ToString("X2")).ToString() + Convert.ToInt16(buffer[1].ToString("X2")).ToString()), Convert.ToInt16(buffer[2].ToString("X2")), day, hour, minute, second);
            }
            int length = bytes.Length;
            throw new Exception("时间格式应该7个字节！错误：" + length.ToString());
        }

        public static string BytesToBinaryString(byte[] bytes)
        {
            return Convert.ToString(HexHelper.ToInt(BytesToHexString(bytes, 0, bytes.Length)), 2);
        }

        public static string BytesToBinaryString(byte[] bytes, int startIndex, int endIndex)
        {
            return Convert.ToString(HexHelper.ToInt(BytesToHexString(bytes, startIndex, endIndex)), 2);
        }

        public static string BytesToHexExHL(byte[] bytes, int startIndex, int endIndex)
        {
            string str = string.Empty;
            byte[] array = ArrayHelper.Copy<byte>(bytes, startIndex, endIndex);
            if (array != null)
            {
                Array.Reverse(array, 0, array.Length);
                str = ToHexString(array, ToHexadecimal.Loop);
            }
            return str;
        }

        public static string BytesToHexString(byte[] bytes, int startIndex, int endIndex)
        {
            return ToHexString(ArrayHelper.Copy<byte>(bytes, startIndex, endIndex), ToHexadecimal.BitConverter);
        }

        public static int BytesToInt(byte[] bytes, int startIndex, int endIndex)
        {
            return HexHelper.ToInt(ToHexString(ArrayHelper.Copy<byte>(bytes, startIndex, endIndex), ToHexadecimal.BitConverter));
        }

        public static int BytesToIntExHL(byte[] bytes, int startIndex, int endIndex)
        {
            byte[] array = ArrayHelper.Copy<byte>(bytes, startIndex, endIndex);
            if (array != null)
            {
                Array.Reverse(array, 0, array.Length);
                return HexHelper.ToInt(ToHexString(array, ToHexadecimal.Loop));
            }
            return -1;
        }

        public static string BytesToIntStringExHL(byte[] bytes, int startIndex, int endIndex)
        {
            int num = BytesToIntExHL(bytes, startIndex, endIndex);
            if (num == -1)
            {
                return "--";
            }
            return num.ToString();
        }

        public static string ByteToBinaryString(byte data)
        {
            return Convert.ToString(data, 2).PadLeft(8, '0');
        }

        public static string ByteToHexWithBlank(byte[] bytes, int startIndex, int endIndex)
        {
            return ToHexStringWithBlank(ArrayHelper.Copy<byte>(bytes, startIndex, endIndex));
        }

        public static byte[] ConvertHexString(string hex)
        {
            hex = hex.Replace(" ", "");
            int length = hex.Length;
            byte[] buffer = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                buffer[i / 2] = Convert.ToByte(hex.Substring(i, 2), 0x10);
            }
            return buffer;
        }

        public static byte[] ConvertHexStringWithDelimiter(string hex, string delimiter)
        {
            hex = hex.Replace(delimiter, "");
            byte[] buffer = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                buffer[i / 2] = Convert.ToByte(hex.Substring(i, 2), 0x10);
            }
            return buffer;
        }

        public static byte[] DTToBytesLen4(DateTime date)
        {
            return new byte[] { Convert.ToByte(date.Year.ToString().Substring(0, 2).ToString(), 0x10), Convert.ToByte(date.Year.ToString().Substring(2, 2).ToString(), 0x10), Convert.ToByte(date.Month.ToString(), 0x10), Convert.ToByte(date.Day.ToString(), 0x10) };
        }

        public static byte[] DTToBytesLen6(DateTime date)
        {
            byte[] buffer = new byte[6];
            buffer[5] = Convert.ToByte(date.Minute.ToString(), 0x10);
            buffer[4] = Convert.ToByte(date.Hour.ToString(), 0x10);
            buffer[3] = Convert.ToByte(date.Day.ToString(), 0x10);
            buffer[2] = Convert.ToByte(date.Month.ToString(), 0x10);
            buffer[1] = Convert.ToByte(date.Year.ToString().Substring(2, 2).ToString(), 0x10);
            buffer[0] = Convert.ToByte(date.Year.ToString().Substring(0, 2).ToString(), 0x10);
            return buffer;
        }

        public static byte[] DTToBytesLen6Inverted(DateTime date)
        {
            return new byte[] { Convert.ToByte(date.Second.ToString(), 0x10), Convert.ToByte(date.Minute.ToString(), 0x10), Convert.ToByte(date.Hour.ToString(), 0x10), Convert.ToByte(date.Day.ToString(), 0x10), Convert.ToByte(date.Month.ToString(), 0x10), Convert.ToByte(date.ToString("yy").ToString(), 0x10) };
        }

        public static byte[] DTToBytesLen7(DateTime date)
        {
            byte[] buffer = new byte[7];
            buffer[6] = Convert.ToByte(date.Second.ToString(), 0x10);
            buffer[5] = Convert.ToByte(date.Minute.ToString(), 0x10);
            buffer[4] = Convert.ToByte(date.Hour.ToString(), 0x10);
            buffer[3] = Convert.ToByte(date.Day.ToString(), 0x10);
            buffer[2] = Convert.ToByte(date.Month.ToString(), 0x10);
            buffer[1] = Convert.ToByte(date.Year.ToString().Substring(2, 2).ToString(), 0x10);
            buffer[0] = Convert.ToByte(date.Year.ToString().Substring(0, 2).ToString(), 0x10);
            return buffer;
        }

        public static byte[] DTToBytesLen7Inverted(DateTime date)
        {
            return new byte[] { Convert.ToByte(date.Second.ToString(), 0x10), Convert.ToByte(date.Minute.ToString(), 0x10), Convert.ToByte(date.Hour.ToString(), 0x10), Convert.ToByte(date.Day.ToString(), 0x10), Convert.ToByte(date.Month.ToString(), 0x10), Convert.ToByte(date.Year.ToString().Substring(2, 2).ToString(), 0x10), Convert.ToByte(date.Year.ToString().Substring(0, 2).ToString(), 0x10) };
        }

        public static decimal MaxValueToPercent(decimal value)
        {
            decimal num = value / 255.00M;
            return Math.Round((decimal) (num * 100M), 2);
        }

        public static int OverPointValueUseReversed(byte[] source, byte[] compare)
        {
            int num2 = HexHelper.ToInt(ToHexString(source, ToHexadecimal.BitConverter));
            int num3 = HexHelper.ToInt(ToHexString(compare, ToHexadecimal.BitConverter));
            return ((num2 > num3) ? ~num2 : num2);
        }

        public static decimal OverPointValueUseReversed(byte[] source, byte[] compare, decimal divisorValue)
        {
            return Math.Abs((decimal) (OverPointValueUseReversed(source, compare) / divisorValue));
        }

        public static decimal OverPointValueUseReversed(byte[] source, byte[] compare, decimal divisorValue, int precisionValue)
        {
            return Math.Round(OverPointValueUseReversed(source, compare, divisorValue), precisionValue);
        }

        public static byte PercentToByte(int percent)
        {
            return Convert.ToByte(HexHelper.PercentToHex(percent, 2), 0x10);
        }

        public static byte ReverseBinaryToByte(string binaryStr)
        {
            return Convert.ToByte(binaryStr.Reverse(), 2);
        }

        public static byte[] TimeToBytesLen2(string time)
        {
            byte[] buffer = new byte[2];
            if (!string.IsNullOrEmpty(time) && (time.IndexOf(':') != -1))
            {
                string str = time.Substring(0, 2);
                string str2 = time.Substring(3, 2);
                buffer[0] = Convert.ToByte(str, 0x10);
                buffer[1] = Convert.ToByte(str2, 0x10);
            }
            return buffer;
        }

        public static byte ToByte(int value)
        {
            return Convert.ToByte(HexHelper.ToHexString(value), 0x10);
        }

        public static byte[] ToBytes(int value)
        {
            return ConvertHexString(HexHelper.ToHexString(value));
        }

        public static byte[] ToBytesExHL(int value)
        {
            byte[] array = ConvertHexString(HexHelper.ToHexString(value));
            Array.Reverse(array, 0, array.Length);
            return array;
        }

        public static byte[] ToBytesExHL(string hexString)
        {
            byte[] array = ConvertHexString(hexString);
            Array.Reverse(array, 0, array.Length);
            return array;
        }

        public static byte[] ToBytesLen2ExHL(int value)
        {
            string hex = HexHelper.ToHexString(value);
            byte[] array = new byte[2];
            byte[] buffer2 = ConvertHexString(hex);
            if (buffer2.Length < 2)
            {
                buffer2.CopyTo(array, 0);
                return array;
            }
            Array.Reverse(buffer2, 0, buffer2.Length);
            return buffer2;
        }

        public static string ToHexString(byte[] bytes, ToHexadecimal type)
        {
            string str = string.Empty;
            switch (type)
            {
                case ToHexadecimal.Loop:
                    return ToHexStringByLoop(bytes);

                case ToHexadecimal.BitConverter:
                    return ToHexStringByBitConverter(bytes);

                case ToHexadecimal.ConvertAll:
                    return ToHexStringByConvertAll(bytes);
            }
            return str;
        }

        private static string ToHexStringByBitConverter(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        private static string ToHexStringByConvertAll(byte[] bytes)
        {
            return string.Concat(Array.ConvertAll<byte, string>(bytes, x => x.ToString("X2")));
        }

        private static string ToHexStringByLoop(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder(bytes.Length * 2);
            foreach (byte num in bytes)
            {
                builder.AppendFormat("{0:X2}", num);
            }
            return builder.ToString();
        }

        public static string ToHexStringWithBlank(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", " ");
        }

        public static string ToHexStrWithDelimiter(byte[] bytes, string delimiter)
        {
            string str = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    str = str + bytes[i].ToString("X2");
                    if (i != (bytes.Length - 1))
                    {
                        str = str + delimiter;
                    }
                }
            }
            return str;
        }
    }
}


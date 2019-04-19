namespace System.Utilities.Common
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class BCDHelper
    {
        public static byte From8421BCDToByte(this int bcdNumber)
        {
            byte num = (byte) (bcdNumber % 10);
            bcdNumber /= 10;
            num = (byte) (num | ((byte) ((bcdNumber % 10) << 4)));
            bcdNumber /= 10;
            return num;
        }

        public static byte[] From8421BCDToBytes(this int bcdNumber, bool isLittleEndian)
        {
            string data = bcdNumber.ToString();
            if (!data.IsBinaryCodedDecimal())
            {
                data = data.PadLeft(data.Length + 1, '0');
            }
            return data.From8421BCDToBytes(isLittleEndian);
        }

        public static byte[] From8421BCDToBytes(this string bcdString, bool isLittleEndian)
        {
            byte[] buffer = null;
            char[] chArray = bcdString.ToCharArray();
            int num = chArray.Length / 2;
            buffer = new byte[num];
            if (isLittleEndian)
            {
                for (int j = 0; j < num; j++)
                {
                    byte num3 = byte.Parse(chArray[(2 * (num - 1)) - (2 * j)].ToString());
                    byte num4 = byte.Parse(chArray[((2 * (num - 1)) - (2 * j)) + 1].ToString());
                    buffer[j] = (byte) (((byte) (num3 << 4)) | num4);
                }
                return buffer;
            }
            for (int i = 0; i < num; i++)
            {
                byte num6 = byte.Parse(chArray[2 * i].ToString());
                byte num7 = byte.Parse(chArray[(2 * i) + 1].ToString());
                buffer[i] = (byte) (((byte) (num6 << 4)) | num7);
            }
            return buffer;
        }

        public static int GetBCDHigh(this byte data)
        {
            return (data >> 4);
        }

        public static int GetBCDLow(this byte data)
        {
            return (data & 15);
        }

        public static string To8421BCDString(this byte data)
        {
            byte num = data;
            int num2 = num >> 4;
            int num3 = num & 15;
            return string.Format("{0}{1}", num2, num3);
        }

        public static string To8421BCDString(this byte[] data, bool isLittleEndian)
        {
            StringBuilder builder = new StringBuilder(data.Length * 2);
            if (isLittleEndian)
            {
                for (int i = data.Length - 1; i >= 0; i--)
                {
                    byte num2 = data[i];
                    int num3 = num2 >> 4;
                    int num4 = num2 & 15;
                    builder.Append(string.Format("{0}{1}", num3, num4));
                }
            }
            else
            {
                for (int j = 0; j < data.Length; j++)
                {
                    byte num6 = data[j];
                    int num7 = num6 >> 4;
                    int num8 = num6 & 15;
                    builder.Append(string.Format("{0}{1}", num7, num8));
                }
            }
            return builder.ToString();
        }
    }
}


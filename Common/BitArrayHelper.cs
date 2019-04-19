namespace System.Utilities.Common
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class BitArrayHelper
    {
        public static BitArray Reverse(this BitArray bits)
        {
            int length = bits.Length;
            int num2 = length / 2;
            for (int i = 0; i < num2; i++)
            {
                bool flag = bits[i];
                bits[i] = bits[(length - i) - 1];
                bits[(length - i) - 1] = flag;
            }
            return bits;
        }

        public static string ToBinaryString(this BitArray bits)
        {
            return bits.ToBinaryString('1', '0');
        }

        public static string ToBinaryString(this BitArray bits, char trueValue, char falseValue)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i])
                {
                    builder.Append(trueValue);
                }
                else
                {
                    builder.Append(falseValue);
                }
            }
            return builder.ToString();
        }

        public static byte[] ToBytes(this BitArray bits)
        {
            int num = bits.Count / 8;
            if ((bits.Count % 8) != 0)
            {
                num++;
            }
            byte[] buffer = new byte[num];
            int index = 0;
            int num3 = 0;
            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                {
                    buffer[index] = (byte) (buffer[index] | ((byte) (((int) 1) << (7 - num3))));
                }
                num3++;
                if (num3 == 8)
                {
                    num3 = 0;
                    index++;
                }
            }
            return buffer;
        }
    }
}


namespace System.Utilities.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public static class BitHelper
    {
        public static byte ClearBit(this byte data, int index)
        {
            return (byte) (data & (0xff - (((int) 1) << index)));
        }

        public static int GetBit(this byte data, int index)
        {
            if ((data & (((int) 1) << index)) <= 0)
            {
                return 0;
            }
            return 1;
        }

        public static byte ReverseBit(this byte data, int index)
        {
            return (byte) (data ^ ((byte) (((int) 1) << index)));
        }

        public static byte SetBit(this byte data, int index)
        {
            return (byte) (data | (((int) 1) << index));
        }
    }
}


namespace System.Utilities.Common
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class ASCIIHelper
    {
        public static int ToASCII(this char data)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            char[] chars = new char[] { data };
            return encoding.GetBytes(chars)[0];
        }

        public static byte[] ToASCII(this string data)
        {
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(data))
            {
                bytes = Encoding.ASCII.GetBytes(data);
            }
            return bytes;
        }

        public static char ToChar(this byte asciiCode)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bytes = new byte[] { asciiCode };
            return encoding.GetString(bytes)[0];
        }
    }
}


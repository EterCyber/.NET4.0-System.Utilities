namespace System.Utilities.Common
{
    using System;
    using System.Drawing;
    using System.Text;

    public static class RandomHelper
    {
        public static string RandomString_09AZ = "0123456789ABCDEFGHIJKMLNOPQRSTUVWXYZ";

        public static string NetxtString(int size, bool lowerCase)
        {
            Random random = new Random((int) DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder(size);
            int num = lowerCase ? 0x61 : 0x41;
            for (int i = 0; i < size; i++)
            {
                builder.Append((char) ((ushort) ((26.0 * random.NextDouble()) + num)));
            }
            return builder.ToString();
        }

        public static string NetxtString(string randomString, int size, bool lowerCase)
        {
            string str = string.Empty;
            Random random = new Random((int) DateTime.Now.Ticks);
            if (!string.IsNullOrEmpty(randomString))
            {
                StringBuilder builder = new StringBuilder(size);
                int maxValue = randomString.Length - 1;
                for (int i = 0; i < size; i++)
                {
                    int num3 = random.Next(0, maxValue);
                    builder.Append(randomString[num3]);
                }
                str = builder.ToString();
            }
            if (!lowerCase)
            {
                return str.ToUpper();
            }
            return str.ToLower();
        }

        public static Color NextColor()
        {
            Random random = new Random((int) DateTime.Now.Ticks);
            return Color.FromArgb((byte) random.Next(0xff), (byte) random.Next(0xff), (byte) random.Next(0xff));
        }

        public static DateTime NextDateTime()
        {
            Random random = new Random((int) DateTime.Now.Ticks);
            int num = random.Next(2, 5);
            int num2 = random.Next(0, 60);
            int num3 = random.Next(0, 60);
            return Convert.ToDateTime(string.Format("{0} {1}：{2}：{3}", new object[] { DateTime.Now.ToString("yyyy-MM-dd"), num, num2, num3 }));
        }

        public static double NextDouble(double miniDouble, double maxiDouble)
        {
            Random random = new Random((int) DateTime.Now.Ticks);
            return ((random.NextDouble() * (maxiDouble - miniDouble)) + miniDouble);
        }

        public static string NextMacAddress()
        {
            int minValue = 0;
            int maxValue = 0x10;
            Random random = new Random((int) DateTime.Now.Ticks);
            return string.Format("{0}{1}：{2}{3}：{4}{5}：{6}{7}：{8}{9}：{10}{11}", new object[] { random.Next(minValue, maxValue).ToString("x"), random.Next(minValue, maxValue).ToString("x"), random.Next(minValue, maxValue).ToString("x"), random.Next(minValue, maxValue).ToString("x"), random.Next(minValue, maxValue).ToString("x"), random.Next(minValue, maxValue).ToString("x"), random.Next(minValue, maxValue).ToString("x"), random.Next(minValue, maxValue).ToString("x"), random.Next(minValue, maxValue).ToString("x"), random.Next(minValue, maxValue).ToString("x"), random.Next(minValue, maxValue).ToString("x"), random.Next(minValue, maxValue).ToString("x") }).ToUpper();
        }
    }
}


namespace System.Utilities.Common
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class GuidHelper
    {
        public static Guid ConvertToSqlServer(this Guid guid)
        {
            byte[] array = guid.ToByteArray();
            Array.Reverse(array, 0, 4);
            Array.Reverse(array, 4, 2);
            Array.Reverse(array, 6, 2);
            return new Guid(array);
        }

        public static Guid CreateSequential()
        {
            Guid guid;
            int num = UuidCreateSequential(out guid);
            if (num != 0)
            {
                throw new ApplicationException("UUID 失败： " + num);
            }
            return guid;
        }

        public static DateTime GetDateFromComb(Guid id)
        {
            DateTime time = new DateTime(0x76c, 1, 1);
            byte[] destinationArray = new byte[4];
            byte[] buffer2 = new byte[4];
            byte[] sourceArray = id.ToByteArray();
            Array.Copy(sourceArray, sourceArray.Length - 6, destinationArray, 2, 2);
            Array.Copy(sourceArray, sourceArray.Length - 4, buffer2, 0, 4);
            Array.Reverse(destinationArray);
            Array.Reverse(buffer2);
            int num = BitConverter.ToInt32(destinationArray, 0);
            int num2 = BitConverter.ToInt32(buffer2, 0);
            return time.AddDays((double) num).AddMilliseconds(num2 * 3.333333);
        }

        public static Guid NewDbGuid()
        {
            byte[] destinationArray = Guid.NewGuid().ToByteArray();
            DateTime time = new DateTime(0x76c, 1, 1);
            DateTime now = DateTime.Now;
            TimeSpan span = new TimeSpan(now.Ticks - time.Ticks);
            DateTime time3 = new DateTime(now.Year, now.Month, now.Day);
            TimeSpan span2 = new TimeSpan(now.Ticks - time3.Ticks);
            byte[] bytes = BitConverter.GetBytes(span.Days);
            byte[] array = BitConverter.GetBytes((long) (span2.TotalMilliseconds / 3.333333));
            Array.Reverse(bytes);
            Array.Reverse(array);
            Array.Copy(bytes, bytes.Length - 2, destinationArray, destinationArray.Length - 6, 2);
            Array.Copy(array, array.Length - 4, destinationArray, destinationArray.Length - 4, 4);
            return new Guid(destinationArray);
        }

        [DllImport("rpcrt4.dll", SetLastError=true)]
        private static extern int UuidCreateSequential(out Guid guid);
    }
}


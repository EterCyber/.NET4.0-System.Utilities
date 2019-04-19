namespace System.Utilities.Common
{
    using System;
    using System.Threading;

    public class ThreadPoolHelper
    {
        public static int ActiveThreadNumber
        {
            get
            {
                int num;
                int num2;
                int num3;
                ThreadPool.GetMaxThreads(out num, out num3);
                ThreadPool.GetAvailableThreads(out num2, out num3);
                return (num - num2);
            }
        }

        public static int AvailableThreadNumber
        {
            get
            {
                int num;
                int num2;
                ThreadPool.GetAvailableThreads(out num, out num2);
                return num;
            }
        }

        public static int MaxThreadNumber
        {
            get
            {
                int num;
                int num2;
                ThreadPool.GetMaxThreads(out num, out num2);
                return num;
            }
        }
    }
}


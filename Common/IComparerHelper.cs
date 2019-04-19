namespace System.Utilities.Common
{
    using System;

    public class IComparerHelper
    {
        public static int ComparePlus<T>(T x, T y, Func<T, T, int> comparedelete)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            if (x.Equals(y))
            {
                return 0;
            }
            return comparedelete(x, y);
        }
    }
}


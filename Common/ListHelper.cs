namespace System.Utilities.Common
{
    using System.Utilities.Base;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class ListHelper
    {
        public static ThreadSafeList<T> ToThreadSafeList<T>(this IEnumerable<T> self)
        {
            return new ThreadSafeList<T>(self);
        }
    }
}


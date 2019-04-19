namespace System.Utilities.Common
{
    using System.Utilities.Base;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class IEnumerableHelper
    {
        public static IEnumerable<T> AsLocked<T>(this IEnumerable<T> enumerable, object syncObject)
        {
            return new ThreadSafeEnumerableHelper<T>(enumerable, syncObject);
        }
    }
}


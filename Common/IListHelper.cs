namespace System.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class IListHelper
    {
        public static void AddRange<T>(this IList<T> self, IEnumerable<T> list)
        {
            ((List<T>) self).AddRange(list);
        }

        public static void AddUnique<T>(this IList<T> self, IEnumerable<T> items)
        {
            foreach (T local in items)
            {
                if (!self.Contains(local))
                {
                    self.Add(local);
                }
            }
        }

        public static void AddUnique<T>(this List<T> self, IEnumerable<T> items, IComparer<T> comparaer)
        {
            self.Sort(comparaer);
            foreach (T local in items)
            {
                if (self.BinarySearch(local, comparaer) < 0)
                {
                    self.Add(local);
                }
            }
        }

        public static List<T> ConvertToList<T>(this IEnumerable<T> self)
        {
            return new List<T>(self);
        }
    }
}


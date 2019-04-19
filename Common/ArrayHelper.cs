namespace System.Utilities.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ArrayHelper
    {
        public static T[] Add<T>(this T[] array, T item)
        {
            int length = array.Length;
            Array.Resize<T>(ref array, length + 1);
            array[length] = item;
            return array;
        }

        public static T[] AddRange<T>(this T[] sourceArray, T[] addArray)
        {
            int length = sourceArray.Length;
            int num2 = addArray.Length;
            Array.Resize<T>(ref sourceArray, length + num2);
            addArray.CopyTo(sourceArray, length);
            return sourceArray;
        }

        public static T[] Copy<T>(T[] source, int startIndex, int endIndex)
        {
            if (startIndex < endIndex)
            {
                int length = endIndex - startIndex;
                T[] destinationArray = new T[length];
                Array.Copy(source, startIndex, destinationArray, 0, length);
                return destinationArray;
            }
            return source;
        }

        public static T[] EnlargeArray<T>(this T[] array, int targetNumber)
        {
            return array.EnlargeArray<T>(targetNumber, false);
        }

        public static T[] EnlargeArray<T>(this T[] array, int targetNumber, bool formRight)
        {
            if (array == null)
            {
                return new T[targetNumber];
            }
            int length = array.Length;
            if (length >= targetNumber)
            {
                return array;
            }
            T[] localArray = new T[targetNumber];
            if (!formRight)
            {
                array.CopyTo(localArray, 0);
                return localArray;
            }
            array.CopyTo(localArray, length);
            return localArray;
        }

        public static bool Equals<T>(this T[] source, T[] compare)
        {
            if ((source == null) || (compare == null))
            {
                return false;
            }
            if (source.Length != compare.Length)
            {
                return false;
            }
            for (int i = 0; i < source.Length; i++)
            {
                if (!source[i].Equals(compare[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static T[] Insert<T>(this T[] source, T t, int index)
        {
            int length = source.Length;
            T local = source[index];
            Array.Resize<T>(ref source, length + 1);
            source[index] = t;
            source[length] = local;
            return source;
        }

        public static bool IsNullOrEmpty(this Array source)
        {
            if ((source != null) && (source.Length != 0))
            {
                return false;
            }
            return true;
        }

        public static int[] ToIntArray(this string[] array)
        {
            if (array.IsNullOrEmpty())
            {
                return null;
            }
            return Array.ConvertAll<string, int>(array, arg => int.Parse(arg));
        }
    }
}


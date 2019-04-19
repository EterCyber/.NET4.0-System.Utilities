namespace System.Utilities.Common
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public static class NameValueHelper
    {
        public static bool JudgedEqual(this NameValueCollection collection, string key, string toEqualValue)
        {
            bool flag = false;
            if ((collection != null) && !string.IsNullOrEmpty(key))
            {
                string str = collection[key];
                if (!string.IsNullOrEmpty(str))
                {
                    flag = str.Equals(toEqualValue);
                }
            }
            return flag;
        }

        public static T JudgedEqual<T>(this string value, string toEqualValue, Func<bool, T> judgeRule)
        {
            bool flag = value.Equals(toEqualValue);
            return judgeRule(flag);
        }

        public static T JudgedEqual<T>(this NameValueCollection collection, string key, string toEqualValue, Func<bool, T> judgeRule)
        {
            bool flag = collection.JudgedEqual(key, toEqualValue);
            return judgeRule(flag);
        }
    }
}


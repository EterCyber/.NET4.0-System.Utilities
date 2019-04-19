namespace System.Utilities.Common
{
    using System.Utilities.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class ReflectHelper
    {
        private static BindingFlags bindingFlags = (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        public static bool ContainProperty(this object instance, string propertyName)
        {
            return (((instance != null) && !string.IsNullOrEmpty(propertyName)) && (instance.GetType().GetProperty(propertyName) != null));
        }

        public static List<Variance> DetailedPublicInstanceProperties<T>(T self, string subKey, params string[] ignore) where T: class
        {
            List<Variance> list = new List<Variance>();
            if (self != null)
            {
                Type type = typeof(T);
                List<string> list2 = new List<string>(ignore);
                foreach (PropertyInfo info in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!list2.Contains(info.Name))
                    {
                        string name = info.Name;
                        object obj2 = type.GetProperty(name).GetValue(self, null);
                        Variance item = new Variance {
                            Key = name,
                            Value = obj2,
                            SubKey = subKey
                        };
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static List<Variance> DetailedPublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore) where T: class
        {
            List<Variance> list = new List<Variance>();
            if ((self != null) && (to != null))
            {
                Type type = typeof(T);
                List<string> list2 = new List<string>(ignore);
                foreach (PropertyInfo info in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!list2.Contains(info.Name))
                    {
                        string name = info.Name;
                        object obj2 = type.GetProperty(name).GetValue(self, null);
                        object obj3 = type.GetProperty(name).GetValue(to, null);
                        if ((obj2 != obj3) && ((obj2 == null) || !obj2.Equals(obj3)))
                        {
                            Variance item = new Variance {
                                Key = name,
                                Value = obj3
                            };
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public static List<Variance> DetailedPublicInstancePropertiesEqual<T>(T self, T to, string subKey, params string[] ignore) where T: class
        {
            List<Variance> list = DetailedPublicInstancePropertiesEqual<T>(self, to, ignore);
            if (!string.IsNullOrEmpty(subKey))
            {
                foreach (Variance variance in list)
                {
                    variance.SubKey = subKey;
                }
            }
            return list;
        }

        public static IDictionary<string, string> GetDisplayName<T>() where T: class
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                dictionary.Add(info.Name, (customAttributes.Length == 0) ? info.Name : ((DisplayNameAttribute) customAttributes[0]).DisplayName);
            }
            return dictionary;
        }

        public static object GetField(object obj, string name)
        {
            return obj.GetType().GetField(name, bindingFlags).GetValue(obj);
        }

        public static object InvokeMethod(object obj, string methodName, object[] args)
        {
            return obj.GetType().InvokeMember(methodName, bindingFlags | BindingFlags.InvokeMethod, null, obj, args);
        }

        public static bool PublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore) where T: class
        {
            if ((self == null) || (to == null))
            {
                return (self == to);
            }
            Type type = typeof(T);
            List<string> list = new List<string>(ignore);
            foreach (PropertyInfo info in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!list.Contains(info.Name))
                {
                    string name = info.Name;
                    object obj2 = type.GetProperty(name).GetValue(self, null);
                    object obj3 = type.GetProperty(name).GetValue(to, null);
                    if ((obj2 != obj3) && ((obj2 == null) || !obj2.Equals(obj3)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void SetField(object obj, string name, object value)
        {
            obj.GetType().GetField(name, bindingFlags).SetValue(obj, value);
        }
    }
}


namespace System.Utilities.Common
{
    using System.Utilities.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class EnumHelper
    {
        public static DescriptionAttribute[] GetDescriptAttr(this FieldInfo fieldInfo)
        {
            if (fieldInfo != null)
            {
                return (DescriptionAttribute[]) fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            }
            return null;
        }

        public static string GetDescription(this Enum enumName)
        {
            DescriptionAttribute[] descriptAttr = enumName.GetType().GetField(enumName.ToString()).GetDescriptAttr();
            if ((descriptAttr != null) && (descriptAttr.Length > 0))
            {
                return descriptAttr[0].Description;
            }
            return enumName.ToString();
        }

        public static T GetEnumName<T>(string description)
        {
            Type type = typeof(T);
            foreach (FieldInfo info in type.GetFields())
            {
                DescriptionAttribute[] descriptAttr = info.GetDescriptAttr();
                if ((descriptAttr != null) && (descriptAttr.Length > 0))
                {
                    if (descriptAttr[0].Description == description)
                    {
                        return (T) info.GetValue(null);
                    }
                }
                else if (info.Name == description)
                {
                    return (T) info.GetValue(null);
                }
            }
            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举。", description), "Description");
        }

        public static ArrayList ToArrayList(this Type type)
        {
            if (!type.IsEnum)
            {
                return null;
            }
            ArrayList list = new ArrayList();
            foreach (Enum enum2 in Enum.GetValues(type))
            {
                list.Add(new KeyValuePair<Enum, string>(enum2, enum2.GetDescription()));
            }
            return list;
        }

        public static List<EnumItem> ToList(Type type)
        {
            Array values = Enum.GetValues(type);
            List<EnumItem> list = new List<EnumItem>(values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                object obj2 = values.GetValue(i);
                EnumItem item = new EnumItem {
                    name = obj2.ToString(),
                    value = (int) obj2
                };
                list.Add(item);
            }
            return list;
        }
    }
}


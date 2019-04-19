namespace System.Utilities.WinForm
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public static class BindingSourceHelper
    {
        public static T Find<T>(this BindingSource dbSource, Predicate<T> match) where T: class
        {
            T local = default(T);
            if (dbSource != null)
            {
                foreach (T local2 in dbSource.List)
                {
                    if (match(local2))
                    {
                        return local2;
                    }
                }
            }
            return local;
        }

        public static IList<T> FindAll<T>(this BindingSource dbSource, Predicate<T> match) where T: class
        {
            IList<T> list = null;
            if (dbSource != null)
            {
                list = new List<T>();
                foreach (T local in dbSource.List)
                {
                    if (match(local))
                    {
                        list.Add(local);
                    }
                }
            }
            return list;
        }

        public static BindingSource GetBindingSource(this Control control)
        {
            if (control != null)
            {
                PropertyInfo property = control.GetType().GetProperty("DataSource");
                if (property != null)
                {
                    object obj2 = property.GetValue(control, null);
                    if ((obj2 != null) && (obj2 is BindingSource))
                    {
                        return (obj2 as BindingSource);
                    }
                }
            }
            return null;
        }

        public static int Remove<T>(this BindingSource dbSource, Predicate<T> match) where T: class
        {
            int num = 0;
            if (dbSource != null)
            {
                for (int i = 0; i < dbSource.List.Count; i++)
                {
                    object obj2 = dbSource.List[i];
                    if (match((T) obj2))
                    {
                        dbSource.List.Remove(obj2);
                        num++;
                        i--;
                    }
                }
            }
            return num;
        }
    }
}


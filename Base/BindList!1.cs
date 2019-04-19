namespace System.Utilities.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class BindList<T> : BindingList<T>, IBindingListView, IBindingList, IList, ICollection, IEnumerable
    {
        private readonly Dictionary<string, PropertyComparer<T>> comparerList;
        private object filterCompareValue;
        private string filterPropertyNameValue;
        private string filterValue;
        private ListSortDescriptionCollection listSortDescription;
        private List<PropertyComparer<T>> propertyComparer;
        private PropertyDescriptor propertyDesc;
        private ListSortDirection sortDirection;
        private List<T> unfilteredListValue;

        public BindList() : base(new List<T>())
        {
            this.comparerList = new Dictionary<string, PropertyComparer<T>>();
            this.unfilteredListValue = new List<T>();
        }

        public BindList(IEnumerable<T> Enumerable) : base(new List<T>(Enumerable))
        {
            this.comparerList = new Dictionary<string, PropertyComparer<T>>();
            this.unfilteredListValue = new List<T>();
        }

        public BindList(IList<T> List) : base(List)
        {
            this.comparerList = new Dictionary<string, PropertyComparer<T>>();
            this.unfilteredListValue = new List<T>();
        }

        private void ApplyFilter()
        {
            this.unfilteredListValue.Clear();
            this.unfilteredListValue.AddRange(base.Items);
            List<T> list = new List<T>();
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T))[this.FilterPropertyName];
            if (prop != null)
            {
                int num = -1;
                do
                {
                    num = this.FindCore(num + 1, prop, this.FilterCompare);
                    if (num != -1)
                    {
                        list.Add(base[num]);
                    }
                }
                while (num != -1);
            }
            this.ClearItems();
            if ((list != null) && (list.Count > 0))
            {
                foreach (T local in list)
                {
                    base.Add(local);
                }
            }
        }

        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            List<T> items = base.Items as List<T>;
            if (items != null)
            {
                this.listSortDescription = sorts;
                this.propertyComparer = new List<PropertyComparer<T>>();
                foreach (ListSortDescription description in (IEnumerable) sorts)
                {
                    this.propertyComparer.Add(new PropertyComparer<T>(description.PropertyDescriptor, description.SortDirection));
                }
                items.Sort(new Comparison<T>(this.CompareValuesByProperties));
            }
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection sortDirection)
        {
            PropertyComparer<T> comparer;
            List<T> items = (List<T>) base.Items;
            string name = property.Name;
            if (!this.comparerList.TryGetValue(name, out comparer))
            {
                comparer = new PropertyComparer<T>(property, sortDirection);
                this.comparerList.Add(name, comparer);
            }
            comparer.SetDirection(sortDirection);
            items.Sort(comparer);
            this.propertyDesc = property;
            this.sortDirection = sortDirection;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        private int CompareValuesByProperties(T x, T y)
        {
            if (x == null)
            {
                if (y != null)
                {
                    return -1;
                }
                return 0;
            }
            if (y == null)
            {
                return 1;
            }
            foreach (PropertyComparer<T> comparer in this.propertyComparer)
            {
                int num = comparer.Compare(x, y);
                if (num != 0)
                {
                    return num;
                }
            }
            return 0;
        }

        public T Find(Predicate<T> match)
        {
            T local = default(T);
            for (int i = 0; i < base.Count; i++)
            {
                T local2 = base.Items[i];
                if (match(local2))
                {
                    return local2;
                }
            }
            return local;
        }

        public int Find(int startIndex, string property, object key)
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T)).Find(property, true);
            if (prop == null)
            {
                return -1;
            }
            if (startIndex <= 0)
            {
                return this.FindCore(prop, key);
            }
            return this.FindCore(startIndex, prop, key);
        }

        public IList<T> FindAll(Predicate<T> match)
        {
            IList<T> list = null;
            if (base.Count > 0)
            {
                list = new List<T>();
            }
            for (int i = 0; i < base.Count; i++)
            {
                T local = base.Items[i];
                if (match(local))
                {
                    list.Add(local);
                }
            }
            return list;
        }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            PropertyInfo property = typeof(T).GetProperty(prop.Name);
            if (key != null)
            {
                for (int i = 0; i < base.Count; i++)
                {
                    T local = base.Items[i];
                    if (property.GetValue(local, null).Equals(key))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        protected int FindCore(int startIndex, PropertyDescriptor prop, object key)
        {
            PropertyInfo property = typeof(T).GetProperty(prop.Name);
            if (key != null)
            {
                for (int i = startIndex; i < base.Count; i++)
                {
                    T local = base.Items[i];
                    if (property.GetValue(local, null).Equals(key))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void GetFilterParts()
        {
            string[] strArray = this.Filter.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            this.filterPropertyNameValue = strArray[0].Replace("[", "").Replace("]", "").Trim();
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(T))[this.filterPropertyNameValue.ToString()];
            if (descriptor != null)
            {
                try
                {
                    this.filterCompareValue = TypeDescriptor.GetConverter(descriptor.PropertyType).ConvertFromString(strArray[1].Replace("'", "").Trim());
                    return;
                }
                catch (NotSupportedException)
                {
                    throw new ArgumentException(string.Concat(new object[] { "Specified filter value ", this.FilterCompare, " can not be converted from string...Implement a type converter for ", descriptor.PropertyType.ToString() }));
                }
            }
            throw new ArgumentException("Specified property '" + this.FilterPropertyName + "' is not found on type " + typeof(T).Name + ".");
        }

        public int Remove(Predicate<T> match)
        {
            int num = 0;
            for (int i = 0; i < base.Count; i++)
            {
                T local = base.Items[i];
                if (match(local))
                {
                    this.RemoveItem(i);
                    i--;
                    num++;
                }
            }
            return num;
        }

        public void RemoveFilter()
        {
            if (this.Filter != null)
            {
                this.Filter = null;
            }
        }

        public string Filter
        {
            get
            {
                return this.filterValue;
            }
            set
            {
                if (this.filterValue != value)
                {
                    base.RaiseListChangedEvents = false;
                    if (value == null)
                    {
                        this.ClearItems();
                        foreach (T local in this.unfilteredListValue)
                        {
                            base.Items.Add(local);
                        }
                        this.filterValue = value;
                    }
                    else if (value != "")
                    {
                        if (Regex.Matches(value, @"[?[\w ]+]? ?[=] ?'?[\w|/: ]+'?", RegexOptions.Singleline).Count != 1)
                        {
                            if (Regex.Matches(value, @"[?[\w ]+]? ?[=] ?'?[\w|/: ]+'?", RegexOptions.Singleline).Count > 1)
                            {
                                throw new ArgumentException("Multi-columnfiltering is not implemented.");
                            }
                            throw new ArgumentException("Filter is notin the format: propName = 'value'.");
                        }
                        this.unfilteredListValue.Clear();
                        this.unfilteredListValue.AddRange(base.Items);
                        this.filterValue = value;
                        this.GetFilterParts();
                        this.ApplyFilter();
                    }
                    base.RaiseListChangedEvents = true;
                    this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
                }
            }
        }

        public object FilterCompare
        {
            get
            {
                return this.filterCompareValue;
            }
        }

        public string FilterPropertyName
        {
            get
            {
                return this.filterPropertyNameValue;
            }
        }

        protected override bool IsSortedCore
        {
            get
            {
                return true;
            }
        }

        public ListSortDescriptionCollection SortDescriptions
        {
            get
            {
                return this.listSortDescription;
            }
        }

        public bool SupportsAdvancedSorting
        {
            get
            {
                return true;
            }
        }

        public bool SupportsFiltering
        {
            get
            {
                return true;
            }
        }

        public List<T> UnfilteredList
        {
            get
            {
                return this.unfilteredListValue;
            }
        }
    }
}


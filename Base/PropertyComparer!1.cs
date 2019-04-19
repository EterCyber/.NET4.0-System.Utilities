namespace System.Utilities.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    internal class PropertyComparer<T> : IComparer<T>
    {
        private ListSortDirection direction;
        private PropertyDescriptor property;

        public PropertyComparer(PropertyDescriptor pperty, ListSortDirection pdirection)
        {
            this.property = pperty;
            this.direction = pdirection;
        }

        public int Compare(T objA, T objB)
        {
            object propertyValue = this.GetPropertyValue(objA, this.property.Name);
            object obj3 = this.GetPropertyValue(objB, this.property.Name);
            if (this.direction == ListSortDirection.Ascending)
            {
                return this.CompareAscending(propertyValue, obj3);
            }
            return this.CompareDescending(propertyValue, obj3);
        }

        private int CompareAscending(object objA, object objB)
        {
            if (!(objA is IComparable) && objA.Equals(objB))
            {
                return 0;
            }
            return ((IComparable) objA).CompareTo(objB);
        }

        private int CompareDescending(object objA, object objB)
        {
            return -this.CompareAscending(objA, objB);
        }

        public bool Equals(T objA, T objB)
        {
            return objA.Equals(objB);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }

        private object GetPropertyValue(T value, string property)
        {
            return value.GetType().GetProperty(property).GetValue(value, null);
        }

        public void SetDirection(ListSortDirection sortDirection)
        {
            this.direction = sortDirection;
        }
    }
}


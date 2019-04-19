namespace System.Utilities.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class ListItem
    {
        public ListItem()
        {
        }

        public ListItem(string _key, string _value)
        {
            this.Key = _key;
            this.Value = _value;
        }

        public override string ToString()
        {
            return this.Value;
        }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}


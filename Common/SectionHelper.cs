namespace System.Utilities.Common
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    public class SectionHelper
    {
        private NameValueCollection ModulSettings;

        public SectionHelper(string sectionName)
        {
            this.ModulSettings = ConfigurationManager.GetSection(sectionName) as NameValueCollection;
        }

        public bool ContainKey(string key)
        {
            return (this.ContainSection() && (this.ModulSettings[key] != null));
        }

        public bool ContainSection()
        {
            return (this.ModulSettings != null);
        }

        public string GetValue(string Key)
        {
            string str = string.Empty;
            if (this.ContainKey(Key))
            {
                str = this.ModulSettings[Key];
            }
            return str;
        }
    }
}


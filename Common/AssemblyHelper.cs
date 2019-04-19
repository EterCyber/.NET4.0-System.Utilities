namespace System.Utilities.Common
{
    using System;
    using System.IO;
    using System.Reflection;

    public class AssemblyHelper
    {
        private Assembly assembly;

        public AssemblyHelper()
        {
            this.assembly = Assembly.GetExecutingAssembly();
        }

        public AssemblyHelper(string path)
        {
            if (File.Exists(path))
            {
                this.assembly = Assembly.LoadFile(path);
            }
        }

        public string GetAppFullName()
        {
            return this.assembly.FullName.ToString();
        }

        private void GetAssemblyCommon<T>(Action<T> assemblyFacotry) where T: Attribute
        {
            if (this.assembly != null)
            {
                object[] customAttributes = this.assembly.GetCustomAttributes(typeof(T), false);
                if (customAttributes.Length > 0)
                {
                    T local = (T) customAttributes[0];
                    assemblyFacotry(local);
                }
            }
        }

        public string GetCompany()
        {
            string _company = string.Empty;
            this.GetAssemblyCommon<AssemblyCompanyAttribute>(delegate (AssemblyCompanyAttribute _ass) {
                _company = _ass.Company;
            });
            return _company;
        }

        public string GetCopyright()
        {
            string _copyright = string.Empty;
            this.GetAssemblyCommon<AssemblyCopyrightAttribute>(delegate (AssemblyCopyrightAttribute _ass) {
                _copyright = _ass.Copyright;
            });
            return _copyright;
        }

        public string GetDescription()
        {
            string _description = string.Empty;
            this.GetAssemblyCommon<AssemblyDescriptionAttribute>(delegate (AssemblyDescriptionAttribute _ass) {
                _description = _ass.Description;
            });
            return _description;
        }

        public string GetProductName()
        {
            string _product = string.Empty;
            this.GetAssemblyCommon<AssemblyProductAttribute>(delegate (AssemblyProductAttribute _ass) {
                _product = _ass.Product;
            });
            return _product;
        }

        public string GetTitle()
        {
            string _title = string.Empty;
            this.GetAssemblyCommon<AssemblyTitleAttribute>(delegate (AssemblyTitleAttribute _ass) {
                _title = _ass.Title;
            });
            if (string.IsNullOrEmpty(_title))
            {
                _title = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
            return _title;
        }

        public string GetVersion()
        {
            return this.assembly.GetName().Version.ToString();
        }
    }
}


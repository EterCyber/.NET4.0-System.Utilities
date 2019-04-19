namespace System.Utilities.Models
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [Serializable]
    public class ClipboardCtrl
    {
        private static System.Windows.Forms.DataFormats.Format format = DataFormats.GetFormat(typeof(ClipboardCtrl).FullName);
        private Hashtable propertyList;

        public ClipboardCtrl()
        {
            this.propertyList = new Hashtable();
        }

        public ClipboardCtrl(Control ctrl)
        {
            this.propertyList = new Hashtable();
            this.CtrlName = ctrl.GetType().Name;
            this.CtrlNamespace = ctrl.GetType().Namespace;
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(ctrl))
            {
                if (descriptor.PropertyType.IsSerializable)
                {
                    this.propertyList.Add(descriptor.Name, descriptor.GetValue(ctrl));
                }
            }
        }

        public string CtrlName { get; set; }

        public string CtrlNamespace { get; set; }

        public Hashtable CtrlPropertyList
        {
            get
            {
                return this.propertyList;
            }
        }

        public static System.Windows.Forms.DataFormats.Format Format
        {
            get
            {
                return format;
            }
        }
    }
}


namespace System.Utilities.WinForm
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    public static class CheckBoxHelper
    {
        public static void SetCheckBoxTF(this CheckBox ck, bool check)
        {
            ck.UIThread(() => ck.Checked = check);
        }

        public static string TranCheckState(Func<CheckState, string> checkStateFactory, params CheckBox[] checks)
        {
            StringBuilder builder = new StringBuilder();
            if ((checks != null) && (checks.Length > 0))
            {
                foreach (CheckBox box in checks)
                {
                    builder.Append(checkStateFactory(box.CheckState));
                }
            }
            return builder.ToString();
        }
    }
}


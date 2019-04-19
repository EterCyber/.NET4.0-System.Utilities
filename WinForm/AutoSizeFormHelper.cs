namespace System.Utilities.WinForm
{
    using System.Utilities.Models;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class AutoSizeFormHelper
    {
        private int ctrlNo;
        public List<ControlRect> oldCtrl = new List<ControlRect>();

        private void AddControl(Control ctl)
        {
            foreach (Control control in ctl.Controls)
            {
                ControlRect rect;
                rect.Left = control.Left;
                rect.Top = control.Top;
                rect.Width = control.Width;
                rect.Height = control.Height;
                this.oldCtrl.Add(rect);
                if (control.Controls.Count > 0)
                {
                    this.AddControl(control);
                }
            }
        }

        private void AutoScaleControl(Control ctl, float wScale, float hScale)
        {
            foreach (Control control in ctl.Controls)
            {
                int left = this.oldCtrl[this.ctrlNo].Left;
                int top = this.oldCtrl[this.ctrlNo].Top;
                int width = this.oldCtrl[this.ctrlNo].Width;
                int height = this.oldCtrl[this.ctrlNo].Height;
                control.Left = (int) (left * wScale);
                control.Top = (int) (top * hScale);
                control.Width = (int) (width * wScale);
                control.Height = (int) (height * hScale);
                this.ctrlNo++;
                if (control.Controls.Count > 0)
                {
                    this.AutoScaleControl(control, wScale, hScale);
                }
            }
        }

        public void ControlAutoSize(Control form)
        {
            if (this.ctrlNo == 0)
            {
                ControlRect rect;
                rect.Left = form.Left;
                rect.Top = form.Top;
                rect.Width = form.Width;
                rect.Height = form.Height;
                this.oldCtrl.Add(rect);
                this.AddControl(form);
            }
            float wScale = ((float) form.Width) / ((float) this.oldCtrl[0].Width);
            float hScale = ((float) form.Height) / ((float) this.oldCtrl[0].Height);
            this.ctrlNo = 1;
            this.AutoScaleControl(form, wScale, hScale);
        }

        public void ControllInitializeSize(Control form)
        {
            ControlRect rect;
            rect.Left = form.Left;
            rect.Top = form.Top;
            rect.Width = form.Width;
            rect.Height = form.Height;
            this.oldCtrl.Add(rect);
            this.AddControl(form);
        }
    }
}


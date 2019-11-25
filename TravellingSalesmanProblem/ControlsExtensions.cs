using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TravellingSalesmanProblem
{
    public static class ControlsExtensions
    {
        public static void SetText(this Label label, string text)
        {
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            if (label.InvokeRequired)
            {
                label.Invoke(new MethodInvoker(UpdateText));
            }
            else
            {
                UpdateText();
            }

            void UpdateText()
            {
                label.Text = text;
            }
        }

        public static void SetMaximum(this TrackBar trackBar, int max)
        {
            if (trackBar == null)
                throw new ArgumentNullException(nameof(trackBar));

            if (trackBar.InvokeRequired)
            {
                trackBar.Invoke(new MethodInvoker(UpdateMaximum));
            }
            else
            {
                UpdateMaximum();
            }

            void UpdateMaximum()
            {
                trackBar.Maximum = max;
            }
        }

        public static void SetValue(this TrackBar trackBar, int value)
        {
            if (trackBar == null)
                throw new ArgumentNullException(nameof(trackBar));

            if (trackBar.InvokeRequired)
            {
                trackBar.Invoke(new MethodInvoker(UpdateValue));
            }
            else
            {
                UpdateValue();
            }

            void UpdateValue()
            {
                trackBar.Value = value;
            }
        }
    }
}

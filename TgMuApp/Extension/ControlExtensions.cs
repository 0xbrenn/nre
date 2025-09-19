using System;
using System.Windows.Forms;

namespace TgMuApp.Extension
{
    public static class ControlExtensions
    {
        
        public static void InvokeOnUiThreadIfRequired(this Control control, Action action)
        {

            if (control.Disposing || control.IsDisposed || !control.IsHandleCreated)
            {
                return;
            }

            if (control.InvokeRequired)
            {
                control.BeginInvoke((Action)(() =>
                {
                    if (control.Disposing || control.IsDisposed || !control.IsHandleCreated)
                    {
                        return;
                    }

                    action();
                }));
            }
            else
            {
                action.Invoke();
            }
        }
    }
}

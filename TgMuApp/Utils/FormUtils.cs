using System;
using System.Drawing;
using System.Windows.Forms;

namespace TgMuApp.Utils
{
    public class FormUtils
    {
        public static int GetAutoHeight(int width, int sourceheight)
        {
            var dpi = GetDpi();
            if (dpi >= 144)
            {
                return sourceheight;
            }
            var rHeight = (sourceheight * Screen.PrimaryScreen.Bounds.Width) / 1920 * (width / sourceheight);
            if (rHeight >= Screen.PrimaryScreen.Bounds.Height)
            {
                rHeight = Screen.PrimaryScreen.Bounds.Height - 80;
            }
            else if (rHeight < Screen.PrimaryScreen.Bounds.Height)
            {
                if (Screen.PrimaryScreen.Bounds.Height - rHeight < 80)
                {
                    rHeight = Screen.PrimaryScreen.Bounds.Height - 80;
                }
            }
            return rHeight;

        }

        public static int GetAutoWidth(int width)
        {
            var dpi = GetDpi();
            if (dpi >= 144)
            {
                return width;
            }
            return width * Screen.PrimaryScreen.Bounds.Width / 1920;

        }

        private static int GetDpi()
        {
            var dpiX = 96;
            using (var graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                dpiX = (int)graphics.DpiX;
            }
            return dpiX;
        }
    }
}

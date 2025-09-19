using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TgMuApp.Utils
{
    public class ImportUtils
    {
        public static string[] Import(bool isNumber = false)
        {
            var dialog = new OpenFileDialog
            {
                Filter = @"Text File|*.txt",
                Title = @"Select Text File"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var path = dialog.FileName;

                var pattern = isNumber ? @"[^0-9]+" : "";
                var list = File.ReadAllLines(path)
                          .Select(s => Regex.Replace(s, pattern, ""))
                          .Where(s => !string.IsNullOrEmpty(s)).ToArray();
                return list;
            }
            return new string[] { };
        }
    }
}

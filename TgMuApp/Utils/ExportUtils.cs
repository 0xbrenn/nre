using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TgMuApp.Utils
{
    public class ExportUtils
    {
        public static void ExprotFile(List<string> list, string fileName = null)
        {
            var save = new SaveFileDialog();
            save.Filter = "File Text|*.txt";
            save.FileName = string.IsNullOrEmpty(fileName) ? Environment.TickCount.ToString() : fileName;
            if (save.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(save.FileName, list);
            }
        }

        private static void ExportToCsv<T>(string path, List<T> list)
        {
            var resultList = new List<string>();
            var first = list.FirstOrDefault();
            var pList = first.GetType().GetProperties().ToList();
            var colName = pList.Select(p => p.Name).Aggregate((c, p) => c + "," + p);
            resultList.Add(colName);

            list.ForEach(item =>
            {
                var itemStr = pList.Select(p => p.GetValue(item) == null ? string.Empty
                             : p.GetValue(item).ToString())
                             .Aggregate((c, p) => c + "," + p);
                resultList.Add(itemStr);
            });

            File.WriteAllLines(path, resultList, Encoding.UTF8);

        }

        public static Tuple<bool, string> ExportToCsv<T>(List<T> list)
        {
            try
            {
                var saveFile = new SaveFileDialog
                {
                    FileName = $"{Environment.TickCount}.csv",
                    Filter = "CSV File|*.csv"
                };
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    var path = saveFile.FileName;
                    ExportToCsv(path, list);
                }
                return Tuple.Create(true, string.Empty);
            }
            catch (Exception ex)
            {

                return Tuple.Create(true, ex.Message);
            }

        }
    }
}

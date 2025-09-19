using Krypton.Toolkit;
using Newtonsoft.Json;
using Sqlite.Db;
using Sqlite.Db.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TgMuApp.Model;

namespace TgMuApp
{
    public partial class FrmTemplate : KryptonForm
    {
        private Repository<AccoutModel> service = new Repository<AccoutModel>();
        public FrmTemplate()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var open = new OpenFileDialog();
            open.Filter = "CSV File|*.csv";
            if (open.ShowDialog() == DialogResult.OK)
            {
                var path = open.FileName;
                try
                {
                    var list = this.ImportAccount(path);
                    foreach (var item in list)
                    {
                        this.service.Insert(item);
                    }
                    KryptonMessageBox.Show("Import Success!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                    this.DialogResult = DialogResult.OK;

                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show(ex.Message, "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);

                }
            }
        }

        private List<AccoutModel> ImportAccount(string path)
        {
            var resultList = new List<AccoutModel>();
            var strs = File.ReadAllLines(path, Encoding.UTF8).Skip(1).ToList();
            if (strs.Count() > 0)
            {
                foreach (var item in strs)
                {
                    var arr = item.Split(',');
                    var model = new AccoutModel
                    {
                        PhoneNumber = arr[0].TrimStart('+'),
                        ApiId = Convert.ToInt32(arr[1], CultureInfo.InvariantCulture),
                        ApiHash = arr[2],
                        Password = arr[3],
                    };
                    resultList.Add(model);
                }
            }
            return resultList;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var path = dialog.SelectedPath;
                var files = Directory.GetFiles(path, "*.json").ToList();
                if (files.Count == 0)
                {
                    KryptonMessageBox.Show("No Json File!", "Info",
                      KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                    return;
                }
                else
                {

                    foreach (var file in files)
                    {
                        var str = File.ReadAllText(file);
                        if (!string.IsNullOrEmpty(str))
                        {
                            var accItem = JsonConvert.DeserializeObject<AccItem>(str);
                            var accModel = new AccoutModel
                            {
                                PhoneNumber = accItem.Phone.TrimStart('+'),
                                ApiHash = accItem.AppHash,
                                ApiId = accItem.AppId,
                                Password = accItem.Password ?? accItem.TwoFa,
                            };
                            if (!this.service.IsAny(a => a.PhoneNumber == accItem.Phone))
                            {
                                this.service.Insert(accModel);
                            }
                        }
                    }


                    KryptonMessageBox.Show("Import Finish!", "Info",
                     KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var url = "https://medium.com/@alowoperon668/how-to-import-account-info-a8fccc76cb27";
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}

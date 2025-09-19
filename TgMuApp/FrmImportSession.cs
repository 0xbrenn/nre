using Krypton.Toolkit;
using System;
using System.IO;
using System.Windows.Forms;

namespace TgMuApp
{
    public partial class FrmImportSession : KryptonForm
    {
        public FrmImportSession()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var folder = dialog.SelectedPath;
                var fileList = Directory.GetFiles(folder, "*.session");
                if (fileList.Length == 0)
                {
                    KryptonMessageBox.Show("session file not found!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                    return;
                }
                else
                {
                    try
                    {
#if DEBUG
                        var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TgMuApp", "Phones");
#else
                var basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TgMuApp", "Phones");
#endif
                        if (!Directory.Exists(basePath))
                        {
                            Directory.CreateDirectory(basePath);
                        }
                        foreach (var f in fileList)
                        {
                            var fileName = Path.GetFileName(f);
                            var newPath = Path.Combine(basePath, fileName);
                            File.Copy(f, newPath);
                        }
                        KryptonMessageBox.Show("Import Success!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                    }
                    catch (Exception ex)
                    {
                        KryptonMessageBox.Show(ex.Message, "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
                    }

                }
            }


        }
    }
}

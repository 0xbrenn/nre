using Krypton.Toolkit;
using Sqlite.Db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TgMu.Api;
using SqlSugar;
using System.Text.RegularExpressions;
using System.IO;
using TgMu.Api.Model;
using TgMuApp.Extension;
using TgMuApp.Utils;

namespace TgMuApp.ChildForm
{
    public partial class FilterControl : UserControl
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private List<FilterResult> mList = new List<FilterResult>();
        public FilterControl()
        {
            InitializeComponent();
            FrmMain.OnLangChange += FrmMain_OnLangChange;
        }
        private void FrmMain_OnLangChange()
        {
            var model = LangUtils.GetLang();
            if (model != null)
            {
                kryptonGroupBox1.Values.Heading = model.ImportPhoneList;
                btnImport.Text = model.Import;
                btnClearName.Text = model.Clear;
                btnStart.Text = model.Start;
                btnStop.Text = model.Stop;

                this.dgvList.Columns["Column1"].HeaderText = model.Account;
                this.dgvList.Columns["Column2"].HeaderText = model.Phone;
                this.dgvList.Columns["Column3"].HeaderText = model.FirstName;
                this.dgvList.Columns["Column4"].HeaderText = model.LastName;
                this.dgvList.Columns["Column5"].HeaderText = model.UserName;
                this.dgvList.Columns["Column6"].HeaderText = model.Status;
                this.dgvList.Columns["Column7"].HeaderText = model.IsSuccess;





            }
        }
        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (this.kryptonListBox1.Items.Count == 0)
            {
                KryptonMessageBox.Show("Please import your phone list!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            var pList = this.kryptonListBox1.Items.Cast<string>().ToList();
            var isPhone=this.rbPhone.Checked;
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            toolStripProgressBar1.Visible = true;

            var token = this.cts.Token;
            Action<string> acMsg = s => AddLog(s);
            Action<FilterResult> action = m => this.AddRow(m);

            await MuClientApi.FilterAsync(pList, isPhone, acMsg, action, token).ContinueWith(t =>
            {
                this.cts = new CancellationTokenSource();
                this.InvokeOnUiThreadIfRequired(() =>
                {
                    this.btnStart.Enabled = true;
                    this.btnStop.Enabled = false;
                    toolStripProgressBar1.Visible = false;
                    KryptonMessageBox.Show("Finish!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                });
            });
        }

        private void AddRow(FilterResult m)
        {
            mList.Add(m);
            this.InvokeOnUiThreadIfRequired(() =>
            {
                this.dgvList.Rows.Insert(0, new object[] {
                    m.Account, m.Phone, m.FirstName, m.LastName, m.UserName, m.IsSuccess,m.Status
                });
                var color = m.IsSuccess == "Success" ? Color.Green : Color.Red;
                this.dgvList.Rows[0].Cells["Column7"].Style.ForeColor = color;
                this.dgvList.Rows[0].Cells["Column7"].Style.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
                this.dgvList.Rows[0].Cells["Column1"].Style.ForeColor = Color.Blue;

            });
        }

        private void AddLog(string msg)
        {
            this.InvokeOnUiThreadIfRequired(() => this.labLog.Text = msg);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.cts.Cancel();
        }

        private void btnClearName_Click(object sender, EventArgs e)
        {
            this.kryptonListBox1.Items.Clear();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                var isPhone = this.rbPhone.Checked;
                var dialog = new OpenFileDialog
                {
                    Filter = @"Text File|*.txt",
                    Title = @"Select Text File"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var path = dialog.FileName;
                    var list = new List<string>();
                    if (isPhone)
                    {
                        list = File.ReadAllLines(path)
                       .Select(s => Regex.Replace(s, @"[^0-9]+", ""))
                        .Where(s => !string.IsNullOrEmpty(s)).ToList();
                    }
                    else
                    {
                        list = File.ReadAllLines(path).Where(s => !string.IsNullOrEmpty(s)).ToList();
                    }


                    this.kryptonListBox1.Items.AddRange(list.ToArray());
                    this.kryptonGroupBox1.Values.Heading = $"Import Phone List:({list.Count})";

                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message, @"Error", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);

            }
        }

        private void tsMenuClear_Click(object sender, EventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                this.dgvList.Rows.Clear();
                mList.Clear();
            });
        }

        private void tsMenuExport_Click(object sender, EventArgs e)
        {
            if (this.dgvList.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info",
                                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                return;
            }

            ExportUtils.ExportToCsv(mList);
            KryptonMessageBox.Show("Export Success!", "Info",
                                  KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);

        }
    }
}

using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TgMu.Api;
using TgMu.Api.Model;
using TgMuApp.Extension;
using TgMuApp.Utils;

namespace TgMuApp
{
    public partial class FrmExtractMembers : KryptonForm
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private List<UserModel> rList = new List<UserModel>();
        public FrmExtractMembers()
        {
            InitializeComponent();
        }

        private async void btnGet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtGroup.Text.Trim()))
            {
                KryptonMessageBox.Show("Please enter goup link!", "Info",
                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
                return;
            }
            if (!ApiPool.IsLogin())
            {
                KryptonMessageBox.Show("Please login you account!", "Info",
                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
                return;
            }
            this.dgvList.Rows.Clear();
            this.rList.Clear();
            this.labTotal.Text = "0";
            var groupLink = this.txtGroup.Text.Trim();
            var groupName = groupLink.Split('/').LastOrDefault();
            var maxNum = (int)this.nudMax.Value;
            var isAll = this.cbAll.Checked;
            var isChat = this.cbChat.Checked;
            this.progressBar1.Visible = true;
            this.btnGet.Enabled = false;
            this.btnStop.Enabled = true;
            var token = this.cts.Token;
            Action<List<UserModel>> action = list => AddRow(list);
            Action<UserModel> action1 = m => AddRow(m);

            var task = isChat ? MuClientApi.GetUserFromMessageAsync(groupLink, action1, maxNum, isAll, token) : MuClientApi.GetMemberByLink(groupLink, action, maxNum, isAll, token);

            await task.ContinueWith(t =>
            {
                this.cts = new CancellationTokenSource();
                var msg = string.Empty;
                if (!isChat)
                {
                    if (rList.Count == 0)
                    {
                        msg = "No permission to read user list.\r\nYou can try to read chat history!";
                    }
                }
                else
                {
                    msg = t.Exception?.Message;
                }
                
                this.InvokeOnUiThreadIfRequired(() =>
                {
                    this.btnGet.Enabled = true;
                    this.btnStop.Enabled = false;
                    this.progressBar1.Visible = false;
                    if (!string.IsNullOrEmpty(msg))
                    {
                        KryptonMessageBox.Show(msg, "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                    }
                });
            });
        }

        private void AddRow(List<UserModel> list)
        {
            foreach (var item in list)
            {
                AddRow(item);
            }
        }
        private void AddRow(UserModel m)
        {
            if (!this.rList.Any(u => u.Username == m.Username))
            {
                this.rList.Add(m);
                this.InvokeOnUiThreadIfRequired(() =>
                {
                    this.labTotal.Text = rList.Count.ToString();
                    this.dgvList.Rows.Insert(0, new object[] { m.Id, m.FirstName, m.LastName, m.Username, m.Phone, m.Status });
                });
            }


        }
        private void FrmMembers_Load(object sender, EventArgs e)
        {
            ChangeLang();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.dgvList.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
                return;
            }
            var userList = rList.Where(s => !string.IsNullOrEmpty(s.Username)).Select(s => s.Username).ToList();
            var fileName = "Group-Members" + DateTime.Now.ToString("yyyyMMddHHmmss");
            ExportUtils.ExprotFile(userList, fileName);
            KryptonMessageBox.Show("Export Success!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
        }

        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            this.nudMax.Enabled = !cbAll.Checked;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new FrmExtractTip().ShowDialog();
        }

        private void ChangeLang()
        {
            var model = LangUtils.GetLang();
            if (model != null)
            {
                this.Text = model.GroupMembers;
                this.kryptonLabel1.Text = model.MaxNum;
                this.cbAll.Text = model.AllSlow;
                this.btnGet.Text = model.Get;
                this.btnExport.Text = model.Export;

                this.dgvList.Columns["Column1"].HeaderText = model.Id;
                this.dgvList.Columns["Column3"].HeaderText = model.FirstName;
                this.dgvList.Columns["Column4"].HeaderText = model.LastName;
                this.dgvList.Columns["Column5"].HeaderText = model.UserName;
                this.dgvList.Columns["Column6"].HeaderText = model.Phone;
                this.dgvList.Columns["Column7"].HeaderText = model.Status;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (this.dgvList.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
                return;
            }
            ExportUtils.ExportToCsv(rList);
            KryptonMessageBox.Show("Export Success!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.cts.Cancel();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.dgvList.Rows.Clear();
            this.rList.Clear();
            this.labTotal.Text = "0";
        }
    }
}

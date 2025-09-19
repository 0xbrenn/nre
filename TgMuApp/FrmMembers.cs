using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TgMu.Api;
using TgMu.Api.Model;
using TgMuApp.Extension;
using TgMuApp.Utils;

namespace TgMuApp
{
    public partial class FrmMembers : KryptonForm
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        public GroupModel Group { get; set; }
        public FrmMembers()
        {
            InitializeComponent();
        }

        private async void btnGet_Click(object sender, EventArgs e)
        {
            var maxNum = (int)this.nudMax.Value;
            var isAll = this.cbAll.Checked;
            this.panelWait.Visible = true;
            this.btnGet.Enabled = false;
            this.btnStop.Enabled = true;
            var isChat = this.cbChat.Checked;
            var token = this.cts.Token;
            var task = isChat ? MuClientApi.GetUserFromMessage(Group, maxNum, isAll, token) : MuClientApi.GetMemberList(Group, maxNum, isAll, token);
            await task.ContinueWith(t =>
            {
                var list = new List<UserModel>();
                var msg = string.Empty;
                if (t.Exception?.InnerExceptions.Count > 0)
                {
                    msg = t.Exception.InnerException.Message;
                    this.InvokeOnUiThreadIfRequired(() =>
                    {
                        KryptonMessageBox.Show(msg, "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
                    });
                }
                else
                {
                    list = t.Result;
                }

                this.cts = new CancellationTokenSource();
                this.InvokeOnUiThreadIfRequired(() =>
                {
                    this.panelWait.Visible = false;
                    this.btnGet.Enabled = true;
                    this.btnStop.Enabled = false;
                    this.dgvList.DataSource = list;
                    this.labTotal.Text = list.Count.ToString();
                });
            });
        }

        private void FrmMembers_Load(object sender, EventArgs e)
        {
            ChangeLang();
            this.labGroup.Text = this.Group.Title;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.dgvList.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            var list = this.dgvList.DataSource as List<UserModel>;
            var userList = list.Where(s => !string.IsNullOrEmpty(s.Username)).Select(s => s.Username).ToList();
            var fileName = this.labGroup.Text.Trim() + "-Members" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
            var list = this.dgvList.DataSource as List<UserModel>;
            ExportUtils.ExportToCsv(list);
            KryptonMessageBox.Show("Export Success!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.cts.Cancel();
        }
    }
}

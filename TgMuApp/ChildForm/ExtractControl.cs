using System;
using System.Windows.Forms;
using Krypton.Toolkit;
using TgMu.Api;
using TgMu.Api.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TgMuApp.Utils;
using System.IO;
using System.Text;

namespace TgMuApp.ChildForm
{
    public partial class ExtractControl : UserControl
    {

        public ExtractControl()
        {
            InitializeComponent();
            FrmMain.OnLangChange += FrmMain_OnLangChange;
        }

        private void FrmMain_OnLangChange()
        {
            var model = LangUtils.GetLang();
            if (model != null)
            {
                this.kryptonPage1.Text = model.Contact;
                this.kryptonPage2.Text = model.Group;
                this.kryptonPage3.Text = model.Channel;

                this.btnGetContact.Text = model.Get;
                this.btnExportContact.Text = model.Export;
                this.btnContactImport.Text = model.Import;
                this.btnClearContact.Text = model.Clear;
                this.kryptonLabel1.Text = model.Total;

                this.dgvContact.Columns["Column1"].HeaderText = model.Id;
                this.dgvContact.Columns["Column3"].HeaderText = model.FirstName;
                this.dgvContact.Columns["Column4"].HeaderText = model.LastName;
                this.dgvContact.Columns["Column5"].HeaderText = model.UserName;
                this.dgvContact.Columns["Column6"].HeaderText = model.Phone;
                this.dgvContact.Columns["Column7"].HeaderText = model.Status;


                this.btnGroupGet.Text = model.Get;
                this.btnGroupExport.Text = model.Export;
                this.btnGroupExtract.Text = model.ExtractMembers;
                this.btnClearGroup.Text = model.Clear;
                this.kryptonLabel3.Text = model.Total;

                this.dgvGroup.Columns["Column8"].HeaderText = model.Account;
                this.dgvGroup.Columns["Column9"].HeaderText = model.Id;
                this.dgvGroup.Columns["ColGroupUserName"].HeaderText = model.UserName;
                this.dgvGroup.Columns["Column11"].HeaderText = model.GroupName;
                this.dgvGroup.Columns["ColGroupCount"].HeaderText = model.Count;


                this.btnGetChannel.Text = model.Get;
                this.btnChannelExport.Text = model.Export;
                this.btnClearChannel.Text = model.Clear;
                this.btnExtractChannel.Text = model.ExtractMembers;
                this.kryptonLabel4.Text = model.Total;

                this.dgvChannel.Columns["Column14"].HeaderText = model.Account;
                this.dgvChannel.Columns["Column15"].HeaderText = model.Id;
                this.dgvChannel.Columns["ColChannelUserName"].HeaderText = model.UserName;
                this.dgvChannel.Columns["Column17"].HeaderText = model.GroupName;
                this.dgvChannel.Columns["ColChannelCount"].HeaderText = model.Count;
            }
        }

        private async void btnGetAll_Click(object sender, EventArgs e)
        {
            var gList = await MuClientApi.GetGroupList();

        }

        private void btnGetMembers_Click(object sender, EventArgs e)
        {

        }

        private void ExtractControl_Load(object sender, EventArgs e)
        {
            this.kryptonNavigator1.SelectedIndex = 0;
        }

        private async void btnGetContact_Click(object sender, EventArgs e)
        {
            this.panelContactWait.Visible = true;
            this.btnGetContact.Enabled = false;
            try
            {
                var list = await MuClientApi.GetContactList();
                this.dgvContact.DataSource = list;
                this.labTotalContact.Text = list.Count.ToString();
                this.panelContactWait.Visible = false;
                this.btnGetContact.Enabled = true;
            }
            catch (Exception ex)
            {

                var msg = $"{DateTime.Now}:{ex.Message}{Environment.NewLine}";
                File.AppendAllText("logs.txt", msg, Encoding.UTF8);
                this.panelContactWait.Visible = false;
                this.btnGetContact.Enabled = true;
            }


            KryptonMessageBox.Show("Sync complete!", "Info",
                        KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
        }

        private void btnClearContact_Click(object sender, EventArgs e)
        {
            this.dgvContact.DataSource = new List<UserModel>();
            this.labTotalContact.Text = "0";
        }

        private void dgvContact_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                e.CellStyle.ForeColor = Color.Green;
                e.CellStyle.Font = new Font(FontFamily.GenericSansSerif, 7.0F, FontStyle.Bold);
            }
            else if (e.ColumnIndex == 6)
            {
                var color = e.Value?.ToString() == "Online" ? Color.Green : Color.Red;
                e.CellStyle.ForeColor = color;
                e.CellStyle.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
            }
        }

        private async void btnGroupGet_Click(object sender, EventArgs e)
        {
            var isExtractCount = this.cbExtract.Checked;
            this.panelGroupWait.Visible = true;
            this.btnGroupGet.Enabled = false;
            var gList = await MuClientApi.GetGroupList(isExtractCount);
            gList = gList.Where(g => g.IsGroup).ToList();
            this.dgvGroup.DataSource = gList;
            this.labTotalGroup.Text = gList.Count.ToString();
            this.panelGroupWait.Visible = false;
            this.btnGroupGet.Enabled = true;
            KryptonMessageBox.Show("Sync complete!", "Info",
                      KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
        }

        private void btnClearGroup_Click(object sender, EventArgs e)
        {
            this.dgvGroup.DataSource = new List<GroupModel>();
            this.labTotalGroup.Text = "0";
        }

        private async void btnGetChannel_Click(object sender, EventArgs e)
        {
            var isExtractCount = this.cbExtractCount.Checked;
            this.panelChannelWait.Visible = true;
            var gList = await MuClientApi.GetGroupList(isExtractCount);
            gList = gList.Where(g => g.IsChannel).ToList();
            this.dgvChannel.DataSource = gList;
            this.labTotalChannel.Text = gList.Count.ToString();
            this.panelChannelWait.Visible = false;
            KryptonMessageBox.Show("Sync complete!", "Info",
                      KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
        }

        private void btnClearChannel_Click(object sender, EventArgs e)
        {
            this.dgvChannel.DataSource = new List<GroupModel>();
            this.labTotalChannel.Text = "0";
        }

        private void btnGroupExtract_Click(object sender, EventArgs e)
        {
            if (this.dgvGroup.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info",
                                   KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                return;
            }
            var group = this.dgvGroup.CurrentRow.DataBoundItem as GroupModel;
            var frm = new FrmMembers { Group = group };
            frm.ShowDialog();
        }

        private void btnExtractChannel_Click(object sender, EventArgs e)
        {
            if (this.dgvChannel.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info",
                                   KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                return;
            }
            var group = this.dgvChannel.CurrentRow.DataBoundItem as GroupModel;
            var frm = new FrmMembers { Group = group };
            frm.ShowDialog();
        }

        private void btnExportContact_Click(object sender, EventArgs e)
        {
            if (this.dgvContact.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info",
                                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                return;
            }
            var list = this.dgvContact.DataSource as List<UserModel>;
            var userNameList = list.Select(s => s.Username).Where(s => !string.IsNullOrEmpty(s)).ToList();
            ExportUtils.ExprotFile(userNameList);
            KryptonMessageBox.Show("Export Success!", "Info",
                                  KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
        }

        private void btnGroupExport_Click(object sender, EventArgs e)
        {
            if (this.dgvGroup.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info",
                                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                return;
            }
            var list = this.dgvGroup.DataSource as List<GroupModel>;
            var userNameList = list.Select(s => s.UserName).Where(s => !string.IsNullOrEmpty(s)).ToList();
            ExportUtils.ExprotFile(userNameList);
            KryptonMessageBox.Show("Export Success!", "Info",
                                  KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
        }

        private void btnChannelExport_Click(object sender, EventArgs e)
        {
            if (this.dgvChannel.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info",
                                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                return;
            }
            var list = this.dgvChannel.DataSource as List<GroupModel>;
            var userNameList = list.Select(s => s.UserName).Where(s => !string.IsNullOrEmpty(s)).ToList();
            ExportUtils.ExprotFile(userNameList);
            KryptonMessageBox.Show("Export Success!", "Info",
                                  KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
        }

        private void dgvGroup_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvGroup.Columns[e.ColumnIndex].Name == "ColGroupUserName")
            {
                e.CellStyle.ForeColor = Color.Blue;
                e.CellStyle.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
            }
            else if (this.dgvGroup.Columns[e.ColumnIndex].Name == "ColGroupCount")
            {
                e.CellStyle.ForeColor = Color.Green;
                e.CellStyle.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
            }
        }

        private void dgvChannel_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (this.dgvChannel.Columns[e.ColumnIndex].Name == "ColChannelUserName")
            {
                e.CellStyle.ForeColor = Color.Blue;
                e.CellStyle.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
            }
            else if (this.dgvChannel.Columns[e.ColumnIndex].Name == "ColChannelCount")
            {
                e.CellStyle.ForeColor = Color.Green;
                e.CellStyle.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
            }
        }

        private void btnContactImport_Click(object sender, EventArgs e)
        {
            new FrmImport().ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.dgvContact.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info",
                                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                return;
            }
            var list = this.dgvContact.DataSource as List<UserModel>;
            ExportUtils.ExportToCsv(list);
            KryptonMessageBox.Show("Export Success!", "Info",
                                  KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            new FrmExtractMembers().ShowDialog();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            new FrmExtractMembers().ShowDialog();
        }
    }
}

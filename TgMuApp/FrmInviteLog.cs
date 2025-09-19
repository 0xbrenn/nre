using Krypton.Toolkit;
using Sqlite.Db.Model;
using Sqlite.Db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TgMuApp.Utils;

namespace TgMuApp
{
    public partial class FrmInviteLog : KryptonForm
    {
        private Repository<InviteLogs> service = new Repository<InviteLogs>();
        public FrmInviteLog()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtGroupName.Text))
            {
                KryptonMessageBox.Show("Please import your group username!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            var groupName = this.txtGroupName.Text.Trim();
            var list = this.service.GetList(g => g.GroupUserName == groupName).OrderByDescending(s => s.Date).ToList();
            this.dgvList.DataSource = list;
        }

        private void ChangeLang()
        {
            var model = LangUtils.GetLang();
            if (model != null)
            {
                this.Text = model.Invite;
                this.kryptonLabel1.Text = model.GroupUserName;
                this.btnSearch.Text = model.Search;


                this.dgvList.Columns["Column1"].HeaderText = model.Account;
                this.dgvList.Columns["Column2"].HeaderText = model.GroupName;
                this.dgvList.Columns["Column3"].HeaderText = model.GroupUserName;
                this.dgvList.Columns["Column4"].HeaderText = model.UserName;
                this.dgvList.Columns["Column8"].HeaderText = model.FirstName;
                this.dgvList.Columns["Column9"].HeaderText = model.LastName;
                this.dgvList.Columns["Column5"].HeaderText = model.Date;
                this.dgvList.Columns["Column6"].HeaderText = model.IsSuccess;
                this.dgvList.Columns["Column7"].HeaderText = model.Reason;
            }
        }

        private void FrmInviteLog_Load(object sender, EventArgs e)
        {
            ChangeLang();
        }
    }
}

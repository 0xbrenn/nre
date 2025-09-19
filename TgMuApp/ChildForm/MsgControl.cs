using Sqlite.Db.Model;
using Sqlite.Db;
using System;
using System.Windows.Forms;
using Krypton.Toolkit;
using TgMuApp.Utils;

namespace TgMuApp.ChildForm
{
    public partial class MsgControl : UserControl
    {
        private Repository<MsgModel> service = new Repository<MsgModel>();
        public MsgControl()
        {
            InitializeComponent();
            FrmMain.OnLangChange += FrmMain_OnLangChange;
        }

        private void FrmMain_OnLangChange()
        {
            var model = LangUtils.GetLang();
            if (model != null)
            {

                this.btnAdd.Text = model.Add;
                this.btnDel.Text = model.Delete;
                kryptonButton1.Text = model.MarkdownV;
                this.btnEdit.Text = model.Edit;

                this.dgvList.Columns["Column2"].HeaderText = model.Title;
                this.dgvList.Columns["Column3"].HeaderText = model.Type;
                this.dgvList.Columns["Column4"].HeaderText = model.File;
                this.dgvList.Columns["Column5"].HeaderText = model.Content;


            }
        }

        private void Bind()
        {
            var list = this.service.GetList();
            this.dgvList.DataSource = list;
        }

        private void MsgControl_Load(object sender, EventArgs e)
        {
            Bind();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = new FrmEditMessage() { IsAdd = true };
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Bind();
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.dgvList.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info",
                         KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            if (this.dgvList.CurrentRow == null)
            {
                KryptonMessageBox.Show("Please choise data!", "Info",
                            KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            var msg = this.dgvList.CurrentRow.DataBoundItem as MsgModel;

            try
            {
                var frm = new FrmEditMessage() { IsAdd = false, Msg = msg };
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Bind();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.dgvList.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info",
                         KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            if (this.dgvList.CurrentRow == null)
            {
                KryptonMessageBox.Show("Please choise data!", "Info",
                            KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            if (KryptonMessageBox.Show("Are you sure del data?", "Ask",
                            KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Question, showCtrlCopy: false) == DialogResult.Yes)
            {
                var model = this.dgvList.CurrentRow.DataBoundItem as MsgModel;
                this.service.DeleteById(model.Id);
                this.Bind();
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            new FrmContentFormat().ShowDialog();
        }
    }
}

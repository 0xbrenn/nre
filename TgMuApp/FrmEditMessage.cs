using Krypton.Toolkit;
using Sqlite.Db.Model;
using Sqlite.Db;
using System;
using System.Windows.Forms;
using TgMuApp.Utils;

namespace TgMuApp
{
    public partial class FrmEditMessage : KryptonForm
    {
        private Repository<MsgModel> service = new Repository<MsgModel>();
        public bool IsAdd { get; set; }
        public MsgModel Msg { get; set; }
        public FrmEditMessage()
        {
             
            InitializeComponent();
            this.BindType();
            this.comCate.SelectedIndex = 0;
            var str = "Normal:Normal string message." + Environment.NewLine
              + "Markdown:Support the news of the thick body, " + Environment.NewLine+
              "oblique body, lowering line, etc., will filter off special characters" + Environment.NewLine
              + "Premium: Markdown+ Premium Pic";
            toolTip1.SetToolTip(this.pictureBox1, str);

        }
        private void BindType()
        {
            this.comType.Items.AddRange(new object[] { "Text", "Image", "Video" });
            this.comType.SelectedIndex = 0;
        }
        private void FrmEditMessage_Load(object sender, EventArgs e)
        {
            ChangeLang();
            if (!this.IsAdd)
            {
                this.txtTitle.Text = Msg?.Title;
                this.txtPath.Text = Msg?.File;
                this.txtContent.Text = Msg?.Content;
                this.comType.Text = Msg?.Type;
                this.comCate.Text = Msg?.Cate;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtTitle.Text))
            {
                KryptonMessageBox.Show("Please enter your title!", "Info",
                     KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            if (string.IsNullOrEmpty(this.txtContent.Text))
            {
                KryptonMessageBox.Show("Please enter your content!", "Info",
                     KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }

            var model = new MsgModel
            {
                Title = this.txtTitle.Text,
                Type = this.comType.Text,
                Cate = this.comCate.Text,
                Content = this.txtContent.Text,
                File = this.txtPath.Text,
            };
            if (!this.IsAdd)
            {
                model.Id = Msg.Id;
            }

            var r = this.IsAdd ? this.service.Insert(model) : this.service.Update(model);
            var msg = r ? "Save Success!" : "Save Fail!";
            KryptonMessageBox.Show(msg, "Info",
                   KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
            this.DialogResult = DialogResult.OK;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var open = new OpenFileDialog();
            open.Filter = "File|*.gif;*.jpg;*.png;*.mp4";
            if (open.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = open.FileName;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtPath.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ChangeLang()
        {
            var model = LangUtils.GetLang();
            if (model != null)
            {
                this.Text = model.EditMessage;
                this.kryptonWrapLabel1.Text = model.Title;
                this.kryptonWrapLabel2.Text = model.Type;
                this.kryptonWrapLabel3.Text = model.Path;
                this.kryptonWrapLabel4.Text = model.Content;

                this.btnSave.Text = model.Save;
                this.btnClear.Text = model.Clear;
                this.btnBrowse.Text = model.Browser;
                this.btnClose.Text = model.Close;
            }
        }
    }
}

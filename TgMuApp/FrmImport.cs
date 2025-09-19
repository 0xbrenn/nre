using Krypton.Toolkit;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TgMu.Api;
using TgMu.Api.Model;
using TgMuApp.Extension;
using TgMuApp.Utils;

namespace TgMuApp
{
    public partial class FrmImport : KryptonForm
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        public FrmImport()
        {
            InitializeComponent();
            var str = "We recommend that you import contacts using usernames." 
             + Environment.NewLine + "It is not recommended to use phone numbers to import contacts."
             + Environment.NewLine + "Because there are privacy restrictions on phone numbers.";
            toolTip1.SetToolTip(this.pictureBox1, str);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                var isPhone = this.rbPhone.Checked;
                var list = ImportUtils.Import(isPhone).ToList();
                this.txtPhone.Lines = list.ToArray();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message, @"Error", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
            }

        }

        private void btnClearContact_Click(object sender, EventArgs e)
        {
            this.txtPhone.Clear();
        }

        private void AddRow(ImContactModel item)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {

                this.dgvList.Rows.Insert(0, new object[]
                        {
                        item.Account,item.Phone,
                        item.FirstName, item.LastName, item.Username,
                        item.Status,item.Reason
                        });
                var color = string.IsNullOrEmpty(item.Reason) ? Color.Green : Color.Red;
                this.dgvList.Rows[0].Cells["Column1"].Style.ForeColor = Color.Blue;
                this.dgvList.Rows[0].Cells["Column7"].Style.ForeColor = color;
                this.dgvList.Rows[0].Cells["Column7"].Style.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
            });


        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPhone.Text.Trim()))
            {
                KryptonMessageBox.Show("please import you phone list!", "Info",
                            KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            var phoneList = this.txtPhone.Lines.ToList();
            var token = this.cts.Token;
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            var isPhone = this.rbPhone.Checked;
            Action<ImContactModel> action = m => this.AddRow(m);
            var task = isPhone ? MuClientApi.ImportContact(phoneList, action, token) : MuClientApi.ImportContactByUserName(phoneList, action, token);
            await task.ContinueWith(t =>
                {
                    this.InvokeOnUiThreadIfRequired(() =>
                    {
                        this.cts = new CancellationTokenSource();
                        this.btnStart.Enabled = true;
                        this.btnStop.Enabled = false;
                        KryptonMessageBox.Show("Finish!", "Info",
                           KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                    });
                });
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.cts.Cancel();
        }
    }
}

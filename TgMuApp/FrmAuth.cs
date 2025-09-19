using Krypton.Toolkit;
using Sqlite.Db;
using Sqlite.Db.Model;
using System;
using System.Windows.Forms;
using TgMuApp.Utils;

namespace TgMuApp
{
    public partial class FrmAuth : KryptonForm
    {
        private Repository<LicenseModel> service = new Repository<LicenseModel>();
        public string Msg { get; set; }
        public FrmAuth()
        {
            InitializeComponent();
        }


        private void btnAuth_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPurchaseCode.Text))
            {
                KryptonMessageBox.Show("Please enter your license key!", "Info",
                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
                return;
            }
            else
            {
                try
                {
                    this.btnAuth.Enabled = false;
                    var key = this.txtPurchaseCode.Text.Trim();
                    var serial = this.txtDeviceId.Text.Trim();
                    var err = string.Empty;
                    var result = License.TryValidateAndComputeExpiry(serial, key, out err);
                    if (result)
                    {
                        var model = new LicenseModel()
                        {
                            MachineCode = serial,
                            Key = key
                        };
                        var isExist = this.service.IsAny(a => a.Id != 0);
                        if (isExist)
                        {
                            var kModel = this.service.GetFirst(a => a.Id != 0);
                            model.Id = kModel.Id;
                        }
                        this.service.InsertOrUpdate(model);

                        KryptonMessageBox.Show("Authorized successfully!", "Info",
                                 KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                        this.Hide();
                        new FrmMain().ShowDialog();
                    }
                    else
                    {
                        KryptonMessageBox.Show(err, "Info",
                        KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
                    }

                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show(ex.Message, "Info",
                      KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
                }
                finally
                {
                    this.btnAuth.Enabled = true;
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmAuth_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
            else
            {

            }
        }

        private void FrmAuth_Load(object sender, EventArgs e)
        {
            this.labMsg.Text = this.Msg;
            this.txtDeviceId.Text = new DeviceIdGenerator().GenerateDeviceId();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.txtDeviceId.Text);
        }
    }
}

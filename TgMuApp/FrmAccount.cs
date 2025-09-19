using Krypton.Toolkit;
using Newtonsoft.Json;
using Sqlite.Db;
using Sqlite.Db.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TgMuApp.Model;
using TgMuApp.Utils;

namespace TgMuApp
{
    public partial class FrmAccount : KryptonForm
    {
        private Repository<AccoutModel> service = new Repository<AccoutModel>();
        public bool IsAdd { get; set; } = true;
        public AccoutModel Account { get; set; }
        public FrmAccount()
        {
            InitializeComponent();
        }

       
 
        private void Bind()
        {
            this.dgvList.DataSource = this.service.GetList();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPhone.Text))
            {
                KryptonMessageBox.Show("Please enter your phone number!", "Info",
                     KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            if (string.IsNullOrEmpty(this.txtApiId.Text))
            {
                KryptonMessageBox.Show("Please enter your apiId!", "Info",
                     KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            if (string.IsNullOrEmpty(this.txtApiHash.Text))
            {
                KryptonMessageBox.Show("Please enter your apihash!", "Info",
                     KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }

            var apiId = this.txtApiId.Text.Trim();
            var model = new AccoutModel
            {
                ApiId = Convert.ToInt32(apiId, CultureInfo.InvariantCulture),
                ApiHash = this.txtApiHash.Text.Trim(),
                Password = this.txtPwd.Text.Trim(),
                PhoneNumber = this.txtPhone.Text.TrimStart('+')
            };
            if (!this.IsAdd)
            {
                model.Id = Account.Id;
            }

            var r = this.IsAdd ? this.service.Insert(model) : this.service.Update(model);
            var msg = r ? "Save Success!" : "Save Fail!";
            this.IsAdd = true;
            this.Bind();
            KryptonMessageBox.Show(msg, "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtPwd.Clear();
            this.txtPhone.Clear();
            this.txtApiId.Clear();
            this.txtApiHash.Clear();
            this.IsAdd = true;
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
            this.Account = this.dgvList.CurrentRow.DataBoundItem as AccoutModel;
            if (this.Account != null)
            {
                this.IsAdd = false;
                this.txtPhone.Text = Account.PhoneNumber;
                this.txtApiId.Text = Account.ApiId.ToString();
                this.txtApiHash.Text = Account.ApiHash;
                this.txtPwd.Text = Account.Password;
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
                var model = this.dgvList.CurrentRow.DataBoundItem as AccoutModel;
                this.service.DeleteById(model.Id);
                this.Bind();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void FrmAccount_Load(object sender, EventArgs e)
        {
            this.ChangeLang();
            this.Bind();
        }

        private void FrmAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void dgvList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (var sb = new SolidBrush(this.dgvList.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(),
                    e.InheritedRowStyle.Font, sb, e.RowBounds.Location.X, e.RowBounds.Location.Y);

            }

        }


        private void ChangeLang()
        {
            var model = LangUtils.GetLang();
            if (model != null)
            {
                this.Text = model.ManageAccount;
                this.kryptonLabel1.Text = model.Phone;
                this.kryptonLabel2.Text = model.Password;
                this.kryptonLabel3.Text = model.ApiId;
                this.kryptonLabel4.Text = model.ApiHash;

                this.btnSave.Text = model.Save;
                this.btnClear.Text = model.Clear;
                //this.btnImport.Text = model.Import;
                //this.btnTemplate.Text = model.ImportSession;
                this.btnEdit.Text = model.Edit;
                this.btnDel.Text = model.Delete;
                this.btnClose.Text = model.Close;

                this.dgvList.Columns["Column1"].HeaderText = model.Phone;
                this.dgvList.Columns["Column2"].HeaderText = model.ApiId;
                this.dgvList.Columns["Column3"].HeaderText = model.ApiHash;
                this.dgvList.Columns["Column4"].HeaderText = model.Password;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            var basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TgMuApp", "Phones");
            if (Directory.Exists(basePath))
            {
                Process.Start(basePath);
            }
        }

        private void kryptonContextMenuItem1_Click(object sender, EventArgs e)
        {
             new FrmTemplate().ShowDialog();
        }

        private void kryptonContextMenuItem2_Click(object sender, EventArgs e)
        {
            var open = new OpenFileDialog();
            open.Filter = "CSV File|*.csv";
            if (open.ShowDialog() == DialogResult.OK)
            {
                var path = open.FileName;
                try
                {
                    var list = this.ImportAccount(path);
                    foreach (var item in list)
                    {
                        this.service.Insert(item);
                    }
                    KryptonMessageBox.Show("Import Success!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                    this.Bind();
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show(ex.Message, "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);

                }
            }
        }

        private List<AccoutModel> ImportAccount(string path)
        {
            var resultList = new List<AccoutModel>();
            var strs = File.ReadAllLines(path, Encoding.UTF8).Skip(1).ToList();
            if (strs.Count() > 0)
            {
                foreach (var item in strs)
                {
                    var arr = item.Split(',');
                    var model = new AccoutModel
                    {
                        PhoneNumber = arr[0].TrimStart('+'),
                        ApiId = Convert.ToInt32(arr[1], CultureInfo.InvariantCulture),
                        ApiHash = arr[2],
                        Password = arr[3],
                    };
                    resultList.Add(model);
                }
            }
            return resultList;
        }

        private void kryptonContextMenuItem3_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var path = dialog.SelectedPath;
                var files = Directory.GetFiles(path, "*.json").ToList();
                if (files.Count == 0)
                {
                    KryptonMessageBox.Show("No Json File!", "Info",
                      KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                    return;
                }
                else
                {

                    foreach (var file in files)
                    {
                        var str = File.ReadAllText(file);
                        if (!string.IsNullOrEmpty(str))
                        {
                            var accItem = JsonConvert.DeserializeObject<AccItem>(str);
                            var accModel = new AccoutModel
                            {
                                PhoneNumber = accItem.Phone.TrimStart('+'),
                                ApiHash = accItem.AppHash,
                                ApiId = accItem.AppId,
                                Password = accItem.Password ?? accItem.TwoFa,
                            };
                            if (!this.service.IsAny(a => a.PhoneNumber == accItem.Phone))
                            {
                                this.service.Insert(accModel);
                            }
                        }
                    }

                    this.Bind();
                    KryptonMessageBox.Show("Import Finish!", "Info",
                     KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                }
            }
        }

        private void kryptonContextMenuItem4_Click(object sender, EventArgs e)
        {
            new FrmImportSession().ShowDialog();
        }

        private void kryptonContextMenuItem5_Click(object sender, EventArgs e)
        {
            var url = "https://medium.com/@alowoperon668/how-to-import-account-info-a8fccc76cb27";
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void btnImport1_Click(object sender, EventArgs e)
        {
            this.kcMenu.Show(this.btnImport1, this.btnImport1.RectangleToScreen(this.btnImport1.ClientRectangle));
        }
    }
}

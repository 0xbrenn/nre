using Krypton.Toolkit;
using Sqlite.Db.Model;
using Sqlite.Db;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Krypton.Navigator;
using TgMuApp.ChildForm;
using TgMu.Api;
using TgMuApp.Model;
using System.IO;
using TgMuApp.Utils;

namespace TgMuApp
{
    public partial class FrmMain : KryptonForm
    {
        private Repository<AccoutModel> service = new Repository<AccoutModel>();
        private Repository<AccountLoginModel> lgService = new Repository<AccountLoginModel>();
        private Repository<LangItemModel> langService = new Repository<LangItemModel>();
        private TgClentApi api;
        public LoginStatus status = LoginStatus.Phone;
        public static event Action OnLangChange = delegate { };

        public FrmMain()
        {
            InitializeComponent();
            this.BindPhoneList();
            this.Height = FormUtils.GetAutoHeight(1449, 1120);
            this.Width = FormUtils.GetAutoWidth(1449);
        }

        private void CreatePage(KryptonPage page, Control control)
        {

            control.Dock = DockStyle.Fill;
            page.Controls.Add(control);
        }

        private void AddPageConent()
        {

            this.CreatePage(this.kryptonPage2, new MsgControl());
            this.CreatePage(this.kryptonPage6, new ExtractControl());
            this.CreatePage(this.kryptonPage4, new SendControl());
            this.CreatePage(this.kryptonPage5, new GroupControl());
            this.CreatePage(this.kryptonPage3, new WarmControl());
            this.CreatePage(this.kryptonPage7, new FilterControl());
            var setting = new SettingControl();
            setting.OnChange += Setting_OnChange;
            this.CreatePage(this.kryptonPage1, setting);
        }

        private void Setting_OnChange()
        {
            var model = LangUtils.GetLang();
            if (model != null)
            {

                this.Text = model.MainTitle;
                kryptonGroupBox1.Values.Heading = model.Login;
                kryptonGroupBox2.Values.Heading = model.AccountLoginList;
                this.kryptonLabel1.Text = model.Phone;
                labCode.Text = model.Code;
                this.btnAccount.Text = model.Account;
                this.btnLogin.Text = model.Login;
                this.btnLoginAll.Text = model.LoginAll;
                this.btnDel.Text = model.Delete;

                this.dgvAccountLogin.Columns["ColChoice"].HeaderText = model.Choice;
                this.dgvAccountLogin.Columns["ColPhone"].HeaderText = model.Phone;
                this.dgvAccountLogin.Columns["ColStatus"].HeaderText = model.Status;

                this.kryptonPage2.Text = model.Message;
                this.kryptonPage6.Text = model.Extract;
                this.kryptonPage4.Text = model.SendMessage;
                this.kryptonPage5.Text = model.Group;
                this.kryptonPage1.Text = model.Setting;
                this.kryptonPage3.Text = model.Warmer;
                this.kryptonPage7.Text = model.Filter;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.BindLoginHistory();
            BindLang();
            this.AddPageConent();
            this.kryptonNavigator1.SelectedIndex = 0;

        }

        private void BindLang()
        {
            var model = langService.GetFirst(a => a.Id != 0);
            if (model != null)
            {
                LangUtils.LangKey = model.Code;
                Setting_OnChange();
                OnLangChange();
            }

        }


        #region login
        private void BindPhoneList()
        {
            var list = this.service.GetList();
            this.comPhone.DataSource = list;
            this.comPhone.DisplayMember = "PhoneNumber";
            this.comPhone.ValueMember = "Id";
        }
        private void btnAccount_Click(object sender, EventArgs e)
        {
            var frm = new FrmAccount();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.BindPhoneList();
            }
        }


        private void BindLoginHistory()
        {
            var list = this.lgService.GetList();
            foreach (var item in list)
            {
                var status = string.IsNullOrEmpty(item.Reason) ? "NotLogin" : item.Reason;
                AddAccount(item.PhoneNumber, status);
            }

        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.comPhone.Items.Count == 0)
            {
                KryptonMessageBox.Show("Please add your account!", "Info",
                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }

            var accModel = this.comPhone.SelectedItem as AccoutModel;
            var phoneNumber = accModel.PhoneNumber;
            if (this.IsLogin(phoneNumber))
            {
                KryptonMessageBox.Show("Login Success!", "Info",
                                 KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                return;
            }
            if (api == null || api?.Phone != phoneNumber)
            {
                api = new TgClentApi(phoneNumber, accModel.ApiId.ToString(), accModel.ApiHash, accModel.Password);
            }

            try
            {
                if (status == LoginStatus.Phone)
                {
                    var result = await api.Login(phoneNumber);
                    if (result.Item1)
                    {
                        status = LoginStatus.Phone;
                        this.txtCode.Clear();
                        this.AddData(api);
                        KryptonMessageBox.Show("Login Success!", "Info",
                                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(result.Item2))
                        {
                            this.api = null;
                            KryptonMessageBox.Show("Please check the account parameters are correct!", "Info",
                                 KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                            return;
                        }
                        else if (result.Item2 == "code")
                        {
                            this.labCode.Text = "Code:";
                            status = LoginStatus.Code;
                            KryptonMessageBox.Show("Login code has been sent!", "Info",
                               KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);

                        }
                        else if (result.Item2 == "password")
                        {
                            this.labCode.Text = "Password:";
                            status = LoginStatus.Password;
                            KryptonMessageBox.Show("Please enter your password!", "Info",
                              KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                        }
                    }
                }
                else if (status == LoginStatus.Code)
                {
                    var code = this.txtCode.Text.Trim();
                    if (string.IsNullOrEmpty(code))
                    {
                        KryptonMessageBox.Show("Please enter your login code!", "Info",
                                  KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                        return;
                    }
                    var result = await api.Login(code);
                    if (result.Item1)
                    {
                        status = LoginStatus.Phone;
                        this.txtCode.Clear();
                        this.AddData(api);
                        KryptonMessageBox.Show("Login Success!", "Info",
                                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                        return;
                    }
                    else
                    {
                        if (result.Item2 == "password")
                        {
                            this.labCode.Text = "Password:";
                            status = LoginStatus.Password;
                            KryptonMessageBox.Show("Please enter your password!", "Info",
                           KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                        }
                    }
                }
                else if (status == LoginStatus.Password)
                {
                    var code = this.txtCode.Text.Trim();
                    if (string.IsNullOrEmpty(code))
                    {
                        KryptonMessageBox.Show("Please enter password!", "Info",
                                  KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                        return;
                    }
                    var result = await api.Login(code);
                    if (result.Item1)
                    {
                        this.labCode.Text = "Code:";
                        this.AddData(api);
                        status = LoginStatus.Phone;
                        this.txtCode.Clear();
                        KryptonMessageBox.Show("Login Success!", "Info",
                                 KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                    }
                    else
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                if (ex.Message == "API_ID_INVALID")
                {
                    KryptonMessageBox.Show("Your apiid is invalid, please check and modify!", "Info",
                                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
                    var path = this.SessionPath(phoneNumber);
                    var msg = $"Please manually delete the {Environment.NewLine}{path}{Environment.NewLine} file and try again!";
                    KryptonMessageBox.Show(msg, "Info",
                                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: true);
                }
                else if (ex.Message == "PHONE_CODE_INVALID")
                {
                    status = LoginStatus.Phone;
                    KryptonMessageBox.Show("PHONE CODE INVALID!", "Info",
                                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: true);
                }
                else
                {
                    var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TgMuApp", "Phones", "log.txt");
                    File.WriteAllText(filePath, ex.Message);
                    File.WriteAllText(filePath, ex.StackTrace);
                    KryptonMessageBox.Show(ex.Message, "Info",
                                   KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
                }

            }

        }

        private string SessionPath(string phone)
        {
            var fileName = $"{phone}.session";
#if DEBUG
            var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TgMuApp", "Phones", fileName);
#else
            var basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TgMuApp", "Phones", fileName);
#endif
            return basePath;
        }

        private void AddData(TgClentApi api)
        {
            ApiPool.Add(api);
            var phone = api.Phone;

            if (!this.IsExist(phone))
            {
                lgService.Insert(new AccountLoginModel { PhoneNumber = phone, Reason = string.Empty });
                this.AddAccount(phone, "Success");
            }
            else
            {
                this.EditAccount(phone, "Success");
            }
        }

        private bool IsLogin(string phone)
        {
            var dataRow = this.dgvAccountLogin.Rows.Cast<DataGridViewRow>()
                  .FirstOrDefault(row => (row.Cells["ColStatus"].Value.ToString() == "Success")
                  && (row.Cells["ColPhone"].Value.ToString() == phone));
            return dataRow != null;
        }
        private bool IsExist(string phone)
        {
            return lgService.IsAny(s => s.PhoneNumber == phone);

        }
        private void AddAccount(string phone, string statusStr)
        {
            var status = statusStr.Contains("Success");
            this.dgvAccountLogin.Rows.Insert(0, new object[] { true, phone, statusStr });
            this.dgvAccountLogin.Rows[0].Cells["ColStatus"].Style.ForeColor = status ? Color.Green : Color.Red;
            this.dgvAccountLogin.Rows[0].Cells["ColStatus"].Style.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
        }

        private void EditAccount(string phone, string statusStr)
        {
            var status = statusStr.Contains("Success");
            var index = this.dgvAccountLogin.Rows.Cast<DataGridViewRow>()
                  .FirstOrDefault(row => row.Cells["ColPhone"].Value.ToString() == phone).Index;
            this.dgvAccountLogin.Rows[index].Cells["ColStatus"].Value = statusStr;
            this.dgvAccountLogin.Rows[index].Cells["ColStatus"].Style.ForeColor = status ? Color.Green : Color.Red;
            this.dgvAccountLogin.Rows[index].Cells["ColStatus"].Style.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);

        }



        private async void btnLoginAll_Click(object sender, EventArgs e)
        {
            if (this.dgvAccountLogin.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No login record!", "Info",
                                   KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                return;
            }

            var phoneList = this.dgvAccountLogin.Rows.Cast<DataGridViewRow>()
                  .Where(row => (row.Cells["ColStatus"].Value.ToString() != "Success") && (row.Cells["ColStatus"].Value.ToString() != "PHONE_NUMBER_BANNED")
                  && row.Cells["ColChoice"].Value.ToString().ToUpper() == "TRUE")
                  .Select(m => m.Cells["ColPhone"]?.Value.ToString()).ToList();
            if (phoneList.Count == 0)
            {
                KryptonMessageBox.Show("Already logined!", "Info",
                                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                return;
            }
            this.btnLoginAll.Enabled = false;
            this.panelWait.Visible = true;
            var accList = service.GetList();
            foreach (var phone in phoneList)
            {
                var accModel = accList.FirstOrDefault(s => s.PhoneNumber == phone);
                if (accModel != null)
                {
                    var accApi = new TgClentApi(phone, accModel.ApiId.ToString(), accModel.ApiHash, accModel.Password);
                    try
                    {
                        var result = await accApi.Login(phone);
                        if (result.Item1)
                        {
                            this.EditAccount(phone, "Success");
                            ApiPool.Add(accApi);
                        }
                        else
                        {
                            this.EditAccount(phone, result.Item2);
                        }

                    }
                    catch (Exception ex)
                    {
                        this.EditAccount(phone, ex.Message);
                        if (ex.Message == "PHONE_NUMBER_BANNED")
                        {
                            var model = this.lgService.GetFirst(w => w.PhoneNumber == phone);
                            if (model != null)
                            {
                                model.Reason = ex.Message;
                                this.lgService.Update(model);
                            }
                        }

                    }

                }
            }
            this.panelWait.Visible = false;
            this.btnLoginAll.Enabled = true;
        }

        private void dgvAccountLogin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var row = this.dgvAccountLogin.Rows[e.RowIndex];
                var phone = row.Cells["ColPhone"].Value.ToString();
                var status = row.Cells["ColStatus"].Value.ToString();
                var isCheck = !Convert.ToBoolean(row.Cells["ColChoice"].EditedFormattedValue);
                if (status == "Success")
                {
                    ApiPool.UpdateStatus(phone, isCheck);
                }
            }
        }

        #endregion

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.dgvAccountLogin.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info",
                      KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }

            var rowList = this.dgvAccountLogin.Rows.Cast<DataGridViewRow>()
                 .Where(row => (row.Cells["ColStatus"].Value.ToString() != "Success") && row.Cells["ColChoice"].Value.ToString().ToUpper() == "TRUE")
                 .ToList();
            var phoneList = rowList.Select(m => m.Cells["ColPhone"]?.Value.ToString()).ToList();
            if (phoneList.Count == 0)
            {
                KryptonMessageBox.Show("Please select the login failed account!", "Info",
                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            if (DialogResult.OK == KryptonMessageBox.Show("Are you sure Del?", "Info",
                      KryptonMessageBoxButtons.OKCancel, KryptonMessageBoxIcon.Warning, showCtrlCopy: false))
            {
                foreach (var phone in phoneList)
                {
                    try
                    {
                        this.lgService.Delete(w => w.PhoneNumber == phone);
                    }
                    catch (Exception ex)
                    { }

                }

                foreach (var row in rowList)
                {
                    this.dgvAccountLogin.Rows.RemoveAt(row.Index);
                }
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new FrmLoginTip().ShowDialog();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (KryptonMessageBox.Show("Are you sure Exit?", "Info",
              KryptonMessageBoxButtons.OKCancel, KryptonMessageBoxIcon.Question) == DialogResult.OK)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }

            }
        }

        private void kryptonNavigator1_SelectedPageChanged(object sender, EventArgs e)
        {
            OnLangChange();
        }

        private void cbAccount_CheckedChanged(object sender, EventArgs e)
        {
            var state = this.cbAccount.Checked;
            if (this.dgvAccountLogin.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in this.dgvAccountLogin.Rows)
                {
                    row.Cells["ColChoice"].Value = state;
                    var phone = row.Cells["ColPhone"].Value.ToString();
                    var status = row.Cells["ColStatus"].Value.ToString();
                    if (status == "Success")
                    {
                        ApiPool.UpdateStatus(phone, state);
                    }
                }
            }
        }
    }
}

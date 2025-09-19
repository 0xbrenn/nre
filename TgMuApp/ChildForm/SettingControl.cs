using Sqlite.Db.Model;
using Sqlite.Db;
using System;
using System.Windows.Forms;
using System.Linq;
using Krypton.Toolkit;
using TgMuApp.Model;
using TgMuApp.Utils;

namespace TgMuApp.ChildForm
{
    public partial class SettingControl : UserControl
    {
        private Repository<SettingModel> service = new Repository<SettingModel>();
        private Repository<LangItemModel> langService = new Repository<LangItemModel>();
        public SettingModel SetModel { get; set; }
        public event Action OnChange = delegate { };
        public SettingControl()
        {
            InitializeComponent();
            BindLang();
            LoadLang();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.nudSendDelayMin.Value = 10;
            this.nudSendDelayMax.Value = 20;
            nudSendMin.Value = 10;
            nudSendMax.Value = 20;
            nudSendMsgNum.Value = 10;

            nudInviteDelayMin.Value = 10;
            nudInviteDelayMax.Value = 20;
            nudInviteMin.Value = 10;
            nudInviteMax.Value = 20;
            nudInviteUserNum.Value = 10;

            nudSendStrangerMax.Value = 50;
            nudInviteStrangerMax.Value = 50;
            nudPeerFloodNum.Value = 30;
            nudToManyRequstDelay.Value = 60;
            this.cbDisable.Checked = true;

            if (this.cbDisable.Checked)
            {
                nudSendStrangerMax.Enabled = false;
                nudInviteStrangerMax.Enabled = false;
                nudPeerFloodNum.Enabled = false;
                nudToManyRequstDelay.Enabled = false;
            }



        }

        private void SettingControl_Load(object sender, EventArgs e)
        {
            this.SetModel = this.service.GetList().FirstOrDefault();
            if (this.SetModel != null)
            {
                this.nudSendDelayMin.Value = SetModel.SendDelayMin;
                this.nudSendDelayMax.Value = SetModel.SendDelayMax;
                nudSendMin.Value = SetModel.SendMin;
                nudSendMax.Value = SetModel.SendMax;
                nudSendMsgNum.Value = SetModel.SendMsgNum;

                nudInviteDelayMin.Value = SetModel.InviteDelayMin;
                nudInviteDelayMax.Value = SetModel.InviteDelayMax;
                nudInviteMin.Value = SetModel.InviteMin;
                nudInviteMax.Value = SetModel.InviteMax;
                nudInviteUserNum.Value = SetModel.InviteUserNum;

                nudSendStrangerMax.Value = SetModel.MaxSendStranger;
                nudInviteStrangerMax.Value = SetModel.MaxInviteStranger;
                nudPeerFloodNum.Value = SetModel.MaxPeerFlood;
                nudToManyRequstDelay.Value = SetModel.MaxToManyReqestDelay;

                this.cbDisable.Checked = SetModel.ForceIgnore;
                var state = SetModel.ForceIgnore;
                nudSendStrangerMax.Enabled = !state;
                nudInviteStrangerMax.Enabled = !state;
                nudPeerFloodNum.Enabled = !state;
                nudToManyRequstDelay.Enabled = !state;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var model = new SettingModel()
            {
                Id = SetModel.Id,
                SendDelayMin = (int)this.nudSendDelayMin.Value,
                SendDelayMax = (int)nudSendDelayMax.Value,
                SendMin = (int)nudSendMin.Value,
                SendMax = (int)nudSendMax.Value,
                SendMsgNum = (int)nudInviteUserNum.Value,
                InviteDelayMin = (int)nudInviteDelayMin.Value,
                InviteDelayMax = (int)nudInviteDelayMax.Value,
                InviteMin = (int)nudInviteMin.Value,
                InviteMax = (int)nudInviteMax.Value,
                InviteUserNum = (int)nudInviteUserNum.Value,
                MaxSendStranger = (int)nudSendStrangerMax.Value,
                MaxInviteStranger = (int)nudInviteStrangerMax.Value,
                MaxPeerFlood = (int)nudPeerFloodNum.Value,
                MaxToManyReqestDelay = (int)nudToManyRequstDelay.Value,
                ScheduleDate = this.dtpSchedule.Value,
                ForceIgnore = this.cbDisable.Checked
            };
            this.service.Update(model);
            KryptonMessageBox.Show("Save Success!", "Info",
                      KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
        }

        private void picSend_Click(object sender, EventArgs e)
        {
            new FrmTip() { MsgType = 1 }.ShowDialog();
        }

        private void picInvite_Click(object sender, EventArgs e)
        {
            new FrmTip() { MsgType = 2 }.ShowDialog();
        }

        private void picAccountSend_Click(object sender, EventArgs e)
        {
            new FrmTip() { MsgType = 5 }.ShowDialog();
        }

        private void picAccountInvite_Click(object sender, EventArgs e)
        {
            new FrmTip() { MsgType = 3 }.ShowDialog();
        }

        private void picFlood_Click(object sender, EventArgs e)
        {
            new FrmTip() { MsgType = 4 }.ShowDialog();
        }

        private void BindLang()
        {
            this.comLang.SelectedIndexChanged -= this.comLang_SelectedIndexChanged;
            this.comLang.DataSource = LangUtils.GetAllLanguage();
            this.comLang.DisplayMember = "Name";
            this.comLang.ValueMember = "Code";
            this.comLang.SelectedIndex = 0;
            this.comLang.SelectedIndexChanged += this.comLang_SelectedIndexChanged;
        }

        private void comLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lang = this.comLang.SelectedItem as Language;

            LangUtils.LangKey = lang.Code;
            LoadLang();
            try
            {
                var langItem = this.langService.GetFirst(a => a.Id != 0);
                if (langItem != null)
                {
                    langItem.Code = lang.Code;
                    langItem.Name = lang.Name;
                    this.langService.Update(langItem);
                }
                else
                {
                    langItem = new LangItemModel { Name = lang.Name, Code = lang.Code };
                    this.langService.Insert(langItem);
                }


            }
            catch (Exception ex)
            {
            }
            OnChange();
        }

        private void LoadLang()
        {
            var model = LangUtils.GetLang();
            if (model != null)
            {

                kryptonGroupBox1.Values.Heading = model.SendSettings;
                kryptonGroupBox2.Values.Heading = model.InviteSettings;
                kryptonGroupBox3.Values.Heading = model.AccountSafeSettings;
                kryptonGroupBox4.Values.Heading = model.SystemSettings;
                this.kryptonLabel1.Text = model.Wait;
                kryptonWrapLabel1.Text = model.Wait;
                kryptonWrapLabel2.Text = model.Wait;
                kryptonLabel7.Text = model.Wait;

                kryptonLabel3.Text = model.secondsaftereverymessage;
                kryptonLabel5.Text = model.secondsafterevery;
                kryptonLabel6.Text = model.message;
                kryptonLabel8.Text = model.secondsaftereveryuser;
                kryptonLabel11.Text = model.secondsafterevery;
                kryptonLabel10.Text = model.users;

                kryptonLabel16.Text = model.EveryAccountMaxSend;
                kryptonLabel15.Text = model.Stranger;
                kryptonLabel13.Text = model.Stranger;
                kryptonWrapLabel3.Text = model.EveryAccountMaxInvite;
                kryptonLabel14.Text = model.messagesEveryDays;
                kryptonLabel17.Text = model.UsersEveryDays;
                kryptonLabel21.Text = model.Theaccountissuspended;
                kryptonLabel18.Text = model.EveryAccountPEERFLOODMaxNum;
                kryptonLabel19.Text = model.EveryAccountToomanyReqest;
                kryptonLabel20.Text = model.SecondsDelay;

                this.cbDisable.Text = model.DisableSafeSettings;
                kryptonLabel23.Text = model.Language;

                this.btnAbout.Text = model.About;
                this.btnReset.Text = model.Reset;
                this.btnSave.Text = model.Save;

            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            new FrmTip() { MsgType = 5 }.ShowDialog();

        }

        private void cbDisable_CheckedChanged(object sender, EventArgs e)
        {
            var state = this.cbDisable.Checked;
            nudSendStrangerMax.Enabled = !state;
            nudInviteStrangerMax.Enabled = !state;
            nudPeerFloodNum.Enabled = !state;
            nudToManyRequstDelay.Enabled = !state;
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            new FrmAbout().ShowDialog();
        }
    }
}

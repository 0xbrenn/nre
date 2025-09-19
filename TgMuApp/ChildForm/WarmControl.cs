using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TgMu.Api;
using TgMu.Api.Model;
using TgMuApp.Extension;
using TgMuApp.Utils;

namespace TgMuApp.ChildForm
{
    public partial class WarmControl : UserControl
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        public WarmControl()
        {
            InitializeComponent();
            this.kryptonNavigator1.SelectedIndex = 0;
            var str = "How to automatically add contact with each other?" + Environment.NewLine + "automatically Set user name for each account!"
                + Environment.NewLine + "If it is not a contact, it will send messages to strangers!";
            toolTip1.SetToolTip(this.picSend, str);
            FrmMain.OnLangChange += FrmMain_OnLangChange;
        }
        private void FrmMain_OnLangChange()
        {
            var model = LangUtils.GetLang();
            if (model != null)
            {
                this.kryptonPage1.Text = model.Message;
                this.kryptonPage2.Text = model.AutoWarm;
                btnGenrator.Text = model.Generator;
                btnMsgNext.Text = model.Next;


                kryptonWrapLabel1.Text = model.Wait;
                kryptonLabel1.Text = model.Wait;
                kryptonLabel7.Text = model.Count;
                kryptonLabel8.Text = model.WarmNumber;
                kryptonLabel3.Text = model.secondsaftereverymessage;
                kryptonLabel5.Text = model.secondsafterevery;
                kryptonLabel6.Text = model.message;
                cbContacts.Text = model.AddContact;

                btnStart.Text = model.Start;
                btnStop.Text = model.Stop;
                btnClear.Text = model.Clear;

                this.dgvList.Columns["Column6"].HeaderText = model.No;
                this.dgvList.Columns["Column1"].HeaderText = model.From;
                this.dgvList.Columns["Column2"].HeaderText = model.To;
                this.dgvList.Columns["Column3"].HeaderText = model.Time;
                this.dgvList.Columns["Column4"].HeaderText = model.Status;
                this.dgvList.Columns["Column5"].HeaderText = model.Message;







            }
        }

        private void btnGenrator_Click(object sender, EventArgs e)
        {
            var list = Generator.RandomMsgList();
            this.txtMessage.Lines = list.ToArray();
        }

        private void WarmControl_Load(object sender, EventArgs e)
        {
            var list = Generator.RandomMsgList();
            this.txtMessage.Lines = list.ToArray();
        }

        private void btnMsgNext_Click(object sender, EventArgs e)
        {
            this.kryptonNavigator1.SelectedIndex = 1;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtMessage.Text))
            {
                KryptonMessageBox.Show("Please set your message!", "Info",
                              KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            var accList = ApiPool.GetAll();
            if (accList.Count < 2)
            {
                KryptonMessageBox.Show("Please select at least 2 accounts!", "Info",
                           KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }

            var isAddContact = this.cbContacts.Checked;

            var msgList = this.txtMessage.Lines.Where(a => !string.IsNullOrEmpty(a)).ToList();
            var delayMin = (int)this.nudSendDelayMin.Value;
            var delayMax = (int)this.nudSendDelayMax.Value;
            var numDelayMin = (int)this.nudSendMin.Value;
            var numDelayMax = (int)this.nudSendMax.Value;
            var num = (int)this.nudSendMsgNum.Value;

            var countMin = (int)this.nudWarmMin.Value;
            var countMax = (int)this.nudWarmMax.Value;
            Action<WarmModel> acResult = model => this.AddRow(model);
            var count = new Random().Next(countMin, countMax);
            var token = this.cts.Token;
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            await AutoWarm(isAddContact, count, msgList, delayMin, delayMax, numDelayMin, numDelayMax, num, acResult, token)
                .ContinueWith(t =>
                {
                    this.cts = new CancellationTokenSource();
                    this.InvokeOnUiThreadIfRequired(() =>
                    {
                        this.btnStart.Enabled = true;
                        this.btnStop.Enabled = false;
                        KryptonMessageBox.Show("Finish!", "Info",
                              KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                    });


                });
        }

        public async static Task AutoWarm(bool isAddContact, int count, List<string> msgList, int delayMin = 10, int delayMax = 20,
            int numDelayMin = 10, int numDelayMax = 20, int num = 10, Action<WarmModel> acResult = default,
            CancellationToken cancel = default)
        {

            for (int i = 0; i < count; i++)
            {
                if (cancel.IsCancellationRequested)
                {
                    break;
                }
                isAddContact = i == 0 ? true : false;
                await MuClientApi.AutoWarm(isAddContact, msgList, delayMin, delayMax, numDelayMin, numDelayMax,
                    num, acResult, cancel);
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.cts.Cancel();
        }

        private void AddRow(WarmModel item)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                var count = this.dgvList.Rows.Count + 1;
                this.dgvList.Rows.Add(new object[] { count, item.From, item.To, item.Time, item.Status, item.Message });
                var index = count - 1;
                this.dgvList.Rows[index].Cells["Column4"].Style.ForeColor = item.Status == "Success" ? Color.Green : Color.Red;
                this.dgvList.Rows[index].Cells["Column4"].Style.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
            });
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                this.dgvList.Rows.Clear();
            });
        }


    }
}

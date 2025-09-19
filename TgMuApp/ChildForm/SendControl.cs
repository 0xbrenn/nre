using Sqlite.Db.Model;
using Sqlite.Db;
using System;
using System.Windows.Forms;
using Krypton.Toolkit;
using TgMu.Api;
using TgMu.Api.Model;
using System.Linq;
using System.Collections.Generic;
using TgMuApp.Extension;
using System.Drawing;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using TgMuApp.Model;
using TgMuApp.Utils;

namespace TgMuApp.ChildForm
{
    public partial class SendControl : UserControl
    {
        private Repository<SettingModel> service = new Repository<SettingModel>();
        private Repository<MsgModel> msgService = new Repository<MsgModel>();
        private CancellationTokenSource cancelTokenContact = new CancellationTokenSource();
        private int sendTotalContact = 0;

        private CancellationTokenSource cancelTokenName = new CancellationTokenSource();
        private int sendTotalName = 0;

        private CancellationTokenSource cancelTokenGroup = new CancellationTokenSource();
        private int sendTotalGroup = 0;
        public SendControl()
        {
            InitializeComponent();
            FrmMain.OnLangChange += FrmMain_OnLangChange;
        }

        private void FrmMain_OnLangChange()
        {
            var model = LangUtils.GetLang();
            if (model != null)
            {
                this.kryptonPage1.Text = model.Contacts;
                this.kryptonPage2.Text = model.UserName;
                this.kryptonPage4.Text = model.Group;

                this.btnContactStart.Text = model.Start;
                this.btnContactStop.Text = model.Stop;
                this.btnContactClear.Text = model.Clear;
                this.kryptonLabel1.Text = model.Total;
                this.cbContact.Text = model.All;
                this.btnGetContact.Text = model.Get;


                this.dgvContact.Columns["ColChoice"].HeaderText = model.Choice;
                this.dgvContact.Columns["Column5"].HeaderText = model.Account;
                this.dgvContact.Columns["Column4"].HeaderText = model.UserName;

                this.dgvContactResult.Columns["ColStatus"].HeaderText = model.Status;
                this.dgvContactResult.Columns["Column7"].HeaderText = model.Reason;
                this.dgvContactResult.Columns["Column14"].HeaderText = model.Account;
                this.dgvContactResult.Columns["Column8"].HeaderText = model.FirstName;
                this.dgvContactResult.Columns["Column9"].HeaderText = model.LastName;
                this.dgvContactResult.Columns["Column10"].HeaderText = model.UserName;
                this.dgvContactResult.Columns["Column36"].HeaderText = model.Phone;
                this.dgvContactResult.Columns["Column11"].HeaderText = model.Date;


                this.btnNameStart.Text = model.Start;
                this.btnNameStop.Text = model.Stop;
                this.btnNameClear.Text = model.Clear;
                this.kryptonLabel2.Text = model.Total;
                this.btnImport.Text = model.Import;
                this.btnClearName.Text = model.Clear;

                this.dgvUserName.Columns["ColStatusName"].HeaderText = model.Status;
                this.dgvUserName.Columns["Column15"].HeaderText = model.Reason;
                this.dgvUserName.Columns["Column19"].HeaderText = model.Account;
                this.dgvUserName.Columns["Column16"].HeaderText = model.FirstName;
                this.dgvUserName.Columns["Column17"].HeaderText = model.LastName;
                this.dgvUserName.Columns["Column18"].HeaderText = model.UserName;
                this.dgvUserName.Columns["Column20"].HeaderText = model.Date;


                this.btnGroupStart.Text = model.Start;
                this.btnGroupClear.Text = model.Clear;
                this.btnGroupStop.Text = model.Stop;
                this.cbRepeat.Text = model.Repeat;
                this.kryptonLabel4.Text = model.Total;
                this.cbGroupAll.Text = model.All;

                this.dgvGroup.Columns["ColGroupChoice"].HeaderText = model.Choice;
                this.dgvGroup.Columns["Column6"].HeaderText = model.Account;
                this.dgvGroup.Columns["dataGridViewTextBoxColumn1"].HeaderText = model.GroupName;


                this.dgvGroupResult.Columns["Column23"].HeaderText = model.Account;
                this.dgvGroupResult.Columns["Column24"].HeaderText = model.GroupName;
                this.dgvGroupResult.Columns["ColGroupStatus"].HeaderText = model.Status;
                this.dgvGroupResult.Columns["Column22"].HeaderText = model.Reason;
                this.dgvGroupResult.Columns["Column25"].HeaderText = model.Date;



            }
        }

        private async void btnGetContact_Click(object sender, EventArgs e)
        {
            var list = await MuClientApi.GetContacts();
            this.dgvContact.DataSource = list;
            KryptonMessageBox.Show("Sync complete!", "Info",
                       KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
        }

        private async void btnContactStart_Click(object sender, EventArgs e)
        {
            var msgList = this.msgService.GetList().Select(m => new MessageModel
            {
                Title = m.Title,
                Type = m.Type,
                Content = m.Content,
                File = m.File,
                Cate = m.Cate
            }).ToList();
            if (msgList.Count == 0)
            {
                KryptonMessageBox.Show("Please add message!", "Info",
                           KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            var setModel = this.service.GetList().FirstOrDefault();
            var sendModel = new SendSettingModel
            {
                MaxPeerFlood = setModel.MaxPeerFlood,
                MaxSendStranger = setModel.MaxSendStranger,
                MaxToManyReqestDelay = setModel.MaxToManyReqestDelay,
                ScheduleDate = setModel.ScheduleDate,
                SendDelayMax = setModel.SendDelayMax,
                SendDelayMin = setModel.SendDelayMin,
                SendMax = setModel.SendMax,
                SendMin = setModel.SendMin,
                SendMsgNum = setModel.SendMsgNum,
            };
            var safeModel = new SafeSetModel
            {
                ForceIgnore = setModel.ForceIgnore,
                MaxInviteStranger = setModel.MaxInviteStranger,
                MaxPeerFlood = setModel.MaxPeerFlood,
                MaxSendStranger = setModel.MaxSendStranger,
                MaxToManyReqestDelay = setModel.MaxToManyReqestDelay,
            };

            var list = this.dgvContact.DataSource as List<AccountContactModel>;
            list = list.Where(s => s.IsCheck).ToList();
            if (list.Count == 0)
            {
                KryptonMessageBox.Show("Please choice your contacts!", "Info",
                         KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            this.btnContactStart.Enabled = false;
            this.btnContactStop.Enabled = true;
            var token = cancelTokenContact.Token;
            Action<AccountContactModel, bool, string> action = (a, b, c) => AddContactResultRow(a, b, c);
            await MuClientApi.SendMessageToContact(list, sendModel, safeModel, action, msgList, token).ContinueWith(t =>
            {
                cancelTokenContact = new CancellationTokenSource();
                this.InvokeOnUiThreadIfRequired(() =>
                {
                    this.btnContactStart.Enabled = true;
                    this.btnContactStop.Enabled = false;
                    KryptonMessageBox.Show("Send complete!", "Info",
                          KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                });
            });

        }


        private void btnContactClear_Click(object sender, EventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                this.labContactTotal.Text = "0";
                this.sendTotalContact = 0;
                this.dgvContactResult.Rows.Clear();
            });

        }

        private void btnContactStop_Click(object sender, EventArgs e)
        {
            cancelTokenContact.Cancel();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                var list = ImportUtils.Import();
                this.txtUserName.Lines = list;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message, @"Error", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
            }
        }


        private void btnClearName_Click(object sender, EventArgs e)
        {
            this.txtUserName.Clear();
        }

        private async void btnNameStart_Click(object sender, EventArgs e)
        {
            var msgList = this.msgService.GetList().Select(m => new MessageModel
            {
                Title = m.Title,
                Type = m.Type,
                Content = m.Content,
                File = m.File,
                Cate = m.Cate
            }).ToList();

            if (msgList.Count == 0)
            {
                KryptonMessageBox.Show("Please add message!", "Info",
                           KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            var setModel = this.service.GetList().FirstOrDefault();
            var sendModel = new SendSettingModel
            {
                MaxPeerFlood = setModel.MaxPeerFlood,
                MaxSendStranger = setModel.MaxSendStranger,
                MaxToManyReqestDelay = setModel.MaxToManyReqestDelay,
                ScheduleDate = setModel.ScheduleDate,
                SendDelayMax = setModel.SendDelayMax,
                SendDelayMin = setModel.SendDelayMin,
                SendMax = setModel.SendMax,
                SendMin = setModel.SendMin,
                SendMsgNum = setModel.SendMsgNum,
            };
            var safeModel = new SafeSetModel
            {
                ForceIgnore = setModel.ForceIgnore,
                MaxInviteStranger = setModel.MaxInviteStranger,
                MaxPeerFlood = setModel.MaxPeerFlood,
                MaxSendStranger = setModel.MaxSendStranger,
                MaxToManyReqestDelay = setModel.MaxToManyReqestDelay,
            };
            var list = this.txtUserName.Lines.Where(s => !string.IsNullOrEmpty(s)).ToList();
            if (list.Count == 0)
            {
                KryptonMessageBox.Show("Please import your username!", "Info",
                         KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            this.btnNameStart.Enabled = false;
            this.btnNameStop.Enabled = true;
            var token = cancelTokenName.Token;
            Action<AccountContactModel, bool, string> action = (a, b, c) => AddResultRow(a, b, c);

            await MuClientApi.SendMessageByName(list, sendModel, safeModel, action, msgList, token).ContinueWith(t =>
            {
                cancelTokenName = new CancellationTokenSource();
                this.InvokeOnUiThreadIfRequired(() =>
                {
                    this.btnNameStart.Enabled = true;
                    this.btnNameStop.Enabled = false;
                    KryptonMessageBox.Show("Send Finish!", "Info",
                          KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                });
            });
        }


        private void btnNameStop_Click(object sender, EventArgs e)
        {
            cancelTokenName.Cancel();
        }

        private void btnNameClear_Click(object sender, EventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                this.labUserNameTotal.Text = "0";
                this.sendTotalName = 0;
                this.dgvUserName.Rows.Clear();
            });
        }

        private void AddContactResultRow(AccountContactModel model, bool status, string reason)
        {
            var statusStr = status ? "Success" : "Fail";
            this.InvokeOnUiThreadIfRequired(() =>
            {
                dgvContactResult.Rows.Insert(0, new object[]
                {
                    statusStr,reason,model.Account,model.FirstName,model.LastName,model.Username,DateTime.Now.ToString()
                 });
                dgvContactResult.Rows[0].Cells["ColStatus"].Style.ForeColor = status ? Color.Green : Color.Red;
                dgvContactResult.Rows[0].Cells["ColStatus"].Style.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
                this.sendTotalContact++;
                labContactTotal.Text = this.sendTotalContact.ToString();
            });
        }

        private void AddResultRow(AccountContactModel model, bool status, string reason)
        {
            var statusStr = status ? "Success" : "Fail";
            this.InvokeOnUiThreadIfRequired(() =>
            {
                dgvUserName.Rows.Insert(0, new object[]
                {
                    statusStr,reason,model.Account,model.FirstName,model.LastName,model.Username,DateTime.Now.ToString()
                 });
                dgvUserName.Rows[0].Cells["ColStatusName"].Style.ForeColor = status ? Color.Green : Color.Red;
                dgvUserName.Rows[0].Cells["ColStatusName"].Style.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
                this.sendTotalName++;
                labUserNameTotal.Text = sendTotalName.ToString();
            });
        }

        private async void btnGetGroup_Click(object sender, EventArgs e)
        {
            var gList = await MuClientApi.GetGroupList(false);
            var rList = gList.Select(g => new ChoiceGroupModel
            {
                Account = g.Account,
                GroupName = g.Title,
                Id = g.Id,
                IsChoice = true,
                AccessHash = g.AccessHash,
                IsChannel = g.IsChannel,
                IsGroup = g.IsGroup,
            }).ToList();
            this.dgvGroup.DataSource = rList;
        }

        private void cbGroupAll_CheckedChanged(object sender, EventArgs e)
        {
            var state = this.cbGroupAll.Checked;
            if (this.dgvGroup.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in this.dgvGroup.Rows)
                {
                    row.Cells["ColGroupChoice"].Value = state;
                }
            }
        }

        private void cbContact_CheckedChanged(object sender, EventArgs e)
        {
            var state = this.cbContact.Checked;
            if (this.dgvContact.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in this.dgvContact.Rows)
                {
                    row.Cells["ColChoice"].Value = state;
                }
            }
        }

        private void btnGroupClear_Click(object sender, EventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                this.labGroupTotal.Text = "0";
                this.sendTotalGroup = 0;
                this.dgvGroupResult.Rows.Clear();
            });
        }

        private void btnGroupStop_Click(object sender, EventArgs e)
        {
            this.cancelTokenGroup.Cancel();
        }

        private async void btnGroupStart_Click(object sender, EventArgs e)
        {
            var msgList = this.msgService.GetList().Select(m => new MessageModel
            {
                Title = m.Title,
                Type = m.Type,
                Content = m.Content,
                File = m.File,
                Cate = m.Cate
            }).ToList();

            if (msgList.Count == 0)
            {
                KryptonMessageBox.Show("Please add message!", "Info",
                           KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            var setModel = this.service.GetList().FirstOrDefault();
            var sendModel = new SendSettingModel
            {
                MaxPeerFlood = setModel.MaxPeerFlood,
                MaxSendStranger = setModel.MaxSendStranger,
                MaxToManyReqestDelay = setModel.MaxToManyReqestDelay,
                ScheduleDate = setModel.ScheduleDate,
                SendDelayMax = setModel.SendDelayMax,
                SendDelayMin = setModel.SendDelayMin,
                SendMax = setModel.SendMax,
                SendMin = setModel.SendMin,
                SendMsgNum = setModel.SendMsgNum,
            };
            var safeModel = new SafeSetModel
            {
                ForceIgnore = setModel.ForceIgnore,
                MaxInviteStranger = setModel.MaxInviteStranger,
                MaxPeerFlood = setModel.MaxPeerFlood,
                MaxSendStranger = setModel.MaxSendStranger,
                MaxToManyReqestDelay = setModel.MaxToManyReqestDelay,
            };

            var list = this.dgvGroup.DataSource as List<ChoiceGroupModel>;
            var gList = list.Where(s => s.IsChoice).Select(g => new GroupModel
            {
                AccessHash = g.AccessHash,
                Account = g.Account,
                Id = g.Id,
                Title = g.GroupName,
                IsChannel = g.IsChannel,
                IsGroup = g.IsGroup,
            }).ToList();
            if (list.Count == 0)
            {
                KryptonMessageBox.Show("Please choice your group!", "Info",
                         KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            var repeat = this.cbRepeat.Checked;
            this.btnGroupStart.Enabled = false;
            this.btnGroupStop.Enabled = true;
            var token = cancelTokenGroup.Token;
            Action<GroupModel, bool, string> action = (a, b, c) => AddGroupResultRow(a, b, c);
            await MuClientApi.SendMessageToGroup(gList, sendModel, safeModel, action, repeat, msgList, token).ContinueWith(t =>
            {
                cancelTokenGroup = new CancellationTokenSource();
                this.InvokeOnUiThreadIfRequired(() =>
                {
                    this.btnGroupStart.Enabled = true;
                    this.btnGroupStop.Enabled = false;
                    KryptonMessageBox.Show("Send complete!", "Info",
                          KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                });
            });
        }


        private void AddGroupResultRow(GroupModel model, bool status, string reason)
        {
            var statusStr = status ? "Success" : "Fail";
            this.InvokeOnUiThreadIfRequired(() =>
            {
                this.dgvGroupResult.Rows.Insert(0, new object[]
                {
                    model.Account,model.Title,statusStr,reason,DateTime.Now.ToString()
                 });
                this.dgvGroupResult.Rows[0].Cells["ColGroupStatus"].Style.ForeColor = status ? Color.Green : Color.Red;
                this.dgvGroupResult.Rows[0].Cells["ColGroupStatus"].Style.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
                this.sendTotalGroup++;
                this.labGroupTotal.Text = sendTotalGroup.ToString();
            });
        }

        private void SendControl_Load(object sender, EventArgs e)
        {
            this.kryptonNavigator1.SelectedIndex = 0;
        }
    }
}

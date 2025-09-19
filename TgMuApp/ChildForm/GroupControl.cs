using Sqlite.Db.Model;
using Sqlite.Db;
using System;
using System.Windows.Forms;
using Krypton.Toolkit;
using TgMu.Api;
using TgMu.Api.Model;
using TgMuApp.Utils;
using System.Linq;
using System.Threading;
using TgMuApp.Extension;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Diagnostics;
using log4net;

namespace TgMuApp.ChildForm
{
    public partial class GroupControl : UserControl
    {
        private readonly ILog log = LogManager.GetLogger(typeof(GroupControl));
        private Repository<InviteLogs> service = new Repository<InviteLogs>();
        private Repository<SettingModel> setService = new Repository<SettingModel>();
        private CancellationTokenSource inviteCts = new CancellationTokenSource();
        private CancellationTokenSource joinCancellation = new CancellationTokenSource();
        private int inviteSuccess = 0;
        private int inviteFailure = 0;
        public GroupControl()
        {
            InitializeComponent();
            FrmMain.OnLangChange += FrmMain_OnLangChange;
        }

        private void FrmMain_OnLangChange()
        {
            var model = LangUtils.GetLang();
            if (model != null)
            {
                this.kryptonPage1.Text = model.Invite;
                this.kryptonPage2.Text = model.Join;
                this.kryptonPage3.Text = model.Search;

                this.kryptonButton1.Text = model.History;
                this.btnStart.Text = model.Start;
                this.btnStop.Text = model.Stop;
                this.btnClear.Text = model.Clear;
                this.btnImport.Text = model.Import;
                btnClearImport.Text = model.Clear;
                this.kryptonWrapLabel1.Text = model.GroupName;

                kryptonLabel2.Text = model.Total;
                kryptonLabel3.Text = model.Success;
                kryptonLabel4.Text = model.Fail;
                kryptonLabel5.Text = model.Exclude;

                this.dgvInvite.Columns["Column1"].HeaderText = model.Account;
                this.dgvInvite.Columns["Column2"].HeaderText = model.GroupName;
                this.dgvInvite.Columns["Column3"].HeaderText = model.UserName;
                this.dgvInvite.Columns["Column4"].HeaderText = model.Date;
                this.dgvInvite.Columns["Column5"].HeaderText = model.Status;
                this.dgvInvite.Columns["Column6"].HeaderText = model.Reason;

                kryptonGroupBox1.Values.Heading = model.GroupUserName;
                this.btnJoinStart.Text = model.Start;
                //this.btnJoinStop.Text = model;
                this.btnJoinClear.Text = model.Clear;
                this.btnJoinImport.Text = model.Import;
                this.btnJoinImportClear.Text = model.Clear;

                this.dgvJoin.Columns["Column7"].HeaderText = model.Account;
                this.dgvJoin.Columns["Column8"].HeaderText = model.GroupName;
                this.dgvJoin.Columns["Column9"].HeaderText = model.IsSuccess;
                this.dgvJoin.Columns["Column11"].HeaderText = model.Reason;
                this.dgvJoin.Columns["Column10"].HeaderText = model.Date;


                this.kryptonLabel6.Text = model.GroupName;
                this.btnSearch.Text = model.Search;
                this.btnExport.Text = model.Export;
                this.dgvSearch.Columns["Column12"].HeaderText = model.Id;
                this.dgvSearch.Columns["Column13"].HeaderText = model.Title;
                this.dgvSearch.Columns["ColGUserName"].HeaderText = model.GroupUserName;
                this.dgvSearch.Columns["Column18"].HeaderText = model.IsGroup;
                this.dgvSearch.Columns["Column19"].HeaderText = model.IsChannel;
                this.dgvSearch.Columns["ColGSCount"].HeaderText = model.Count;


            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                var isNumber = this.rbPhone.Checked;
                var list = ImportUtils.Import(isNumber);
                this.txtUserName.Lines = list;
                this.labTotal.Text = list.Count().ToString();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message, @"Error", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
            }
        }
        private void btnClearImport_Click(object sender, EventArgs e)
        {
            this.txtUserName.Clear();

        }
        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUserName.Text))
            {
                KryptonMessageBox.Show("Please import your username!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            if (this.rbPhone.Checked)
            {
                var first = this.txtUserName.Lines.FirstOrDefault();
                var isNumber = Regex.IsMatch(first, @"^\d+$");
                if (!isNumber)
                {
                    KryptonMessageBox.Show("Please import phone number!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                    return;
                }

            }
            var isPhone = this.rbPhone.Checked;
            var groupLink = this.txtGroup.Text.Trim(Environment.NewLine.ToCharArray()).Trim();
            if (string.IsNullOrEmpty(groupLink))
            {
                KryptonMessageBox.Show("Please enter your groupname!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            var groupName = string.Empty;
            try
            {
                var gModel = await MuClientApi.GetGroupByLink(groupLink);
                groupName = gModel.UserName ?? gModel.Title;
            }
            catch (Exception ex)
            {
                var msg = ErrorList.GetErrMsg(ex.Message);
                KryptonMessageBox.Show(msg, "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
                return;
            }

            var setModel = this.setService.GetList().FirstOrDefault();
            var inviteModel = new InviteSettingModel
            {
                InviteDelayMax = setModel.InviteDelayMax,
                InviteDelayMin = setModel.InviteDelayMin,
                InviteMax = setModel.InviteMax,
                InviteMin = setModel.InviteMin,
                InviteUserNum = setModel.InviteUserNum,
            };
            var safeModel = new SafeSetModel
            {
                ForceIgnore = setModel.ForceIgnore,
                MaxInviteStranger = setModel.MaxInviteStranger,
                MaxPeerFlood = setModel.MaxPeerFlood,
                MaxSendStranger = setModel.MaxSendStranger,
                MaxToManyReqestDelay = setModel.MaxToManyReqestDelay,
            };

            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            var userNameList = this.txtUserName.Lines.ToList();
            var logList = this.service.GetList(w => w.GroupUserName == groupName)
                .Select(s => isPhone ? s.Phone : s.UserName);
            var sectList = userNameList.Intersect(logList);
            var inviteUser = userNameList.Except(logList).ToList();
            this.labHistory.Text = sectList.Count().ToString();
            this.labTotal.Text = inviteUser.Count.ToString();
            this.labSuccess.Text = "0";
            this.labFail.Text = "0";
            var token = this.inviteCts.Token;
            Action<InviteResult> action = m => this.AddRow(m, isPhone);
            var task = MuClientApi.JoinGroup(groupLink);
            await task.ContinueWith(async t =>
            {
                await MuClientApi.InviteUserAsync(isPhone, inviteUser, inviteModel, safeModel, action, token).ContinueWith(t1 =>
                {
                    if (t1.IsCanceled)
                    {
                        log.Error("exc--" + t1?.Exception);
                        log.Error("innerexc--" + t1?.Exception?.InnerException);
                    }
                    if (t1.IsFaulted)
                    {
                        log.Error("exc1--" + t1?.Exception);
                        log.Error("innerexc1--" + t1?.Exception?.InnerException);
                    }
                    this.inviteCts = new CancellationTokenSource();
                    this.InvokeOnUiThreadIfRequired(() =>
                    {
                        this.btnStart.Enabled = true;
                        this.btnStop.Enabled = false;
                        KryptonMessageBox.Show("Invite Finish!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                    });
                });
            });

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.inviteCts?.Cancel();
        }

        private void AddRow(InviteResult m, bool isPhone = false)
        {

            try
            {
                var isExist = isPhone ? this.service.IsAny(g => g.GroupUserName == m.GroupUserName && g.Phone == m.Phone) :
                    this.service.IsAny(g => g.GroupUserName == m.GroupUserName && g.UserName == m.UserName);

                if (!isExist)
                {
                    this.service.Insert(new InviteLogs
                    {
                        UserId = m.UserId,
                        GroupUserName = m.GroupUserName,
                        Account = m.Account,
                        Date = m.Date,
                        FirstName = m.FirstName,
                        GroupName = m.GroupName,
                        IsSuccess = m.IsSuccess,
                        LastName = m.LastName,
                        Phone = m.Phone,
                        Reason = m.Reason,
                        UserName = m.UserName
                    });
                }
                this.InvokeOnUiThreadIfRequired(() =>
                {
                    var status = m.IsSuccess ? "Success" : "Fail";
                    this.dgvInvite.Rows.Insert(0, new object[] { m.Account, m.GroupName, m.UserName, m.Date, m.Phone, status, m.Reason });
                    this.dgvInvite.Rows[0].Cells["Column5"].Style.ForeColor = m.IsSuccess ? Color.Green : Color.Red;
                    this.dgvInvite.Rows[0].Cells["Column5"].Style.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
                    this.dgvInvite.Rows[0].Cells["Column1"].Style.ForeColor = Color.Blue;
                    if (m.IsSuccess)
                    {
                        this.inviteSuccess++;
                    }
                    else
                    {
                        this.inviteFailure++;
                    }
                    this.labSuccess.Text = this.inviteSuccess.ToString();
                    this.labFail.Text = this.inviteFailure.ToString();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
        }

        private void btnJoinImport_Click(object sender, EventArgs e)
        {
            try
            {
                var list = ImportUtils.Import();
                this.txtGroupUserName.Lines = list;

            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message, @"Error", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);
            }
        }

        private void btnJoinImportClear_Click(object sender, EventArgs e)
        {
            this.txtGroupUserName.Clear();
        }

        private async void btnJoinStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtGroupUserName.Text.Trim()))
            {
                KryptonMessageBox.Show("Please import your group!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }

            this.btnJoinStart.Enabled = false;
            this.btnPuase.Enabled = true;
            var groupUserNameList = this.txtGroupUserName.Lines.ToList();

            var token = this.joinCancellation.Token;
            Action<JoinResult> action = m => this.AddJoinRow(m);

            MuClientApi.InitJoinQueue(groupUserNameList);
            await MuClientApi.BulkJoinGroupAsync(action, token).ContinueWith(t1 =>
            {
                this.joinCancellation = new CancellationTokenSource();
                this.InvokeOnUiThreadIfRequired(() =>
                {
                    this.btnJoinStart.Enabled = true;
                    this.btnPuase.Enabled = false;
                    KryptonMessageBox.Show("Join Finish!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);
                });
            });

        }

        private void AddJoinRow(JoinResult m)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                var status = m.IsSuccess == "Success" ? true : false;
                this.dgvJoin.Rows.Insert(0, new object[] { m.Account, m.GroupName, m.IsSuccess, m.Reason, m.Date });
                this.dgvJoin.Rows[0].Cells["Column9"].Style.ForeColor = status ? Color.Green : Color.Red;
                this.dgvJoin.Rows[0].Cells["Column9"].Style.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
            });
        }



        private void btnJoinClear_Click(object sender, EventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                this.dgvJoin.Rows.Clear();
            });
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtGroupName.Text))
            {
                KryptonMessageBox.Show("Please enter your group name!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            var groupName = this.txtGroupName.Text.Trim();
            groupName = groupName.Split('/').LastOrDefault();
            await MuClientApi.SearchGroupAsync(groupName).ContinueWith(t =>
            {
                var list = t.Result;
                this.InvokeOnUiThreadIfRequired(() => this.dgvSearch.DataSource = list);
                KryptonMessageBox.Show("Search Finish!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information, showCtrlCopy: false);

            });

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.dgvSearch.Rows.Count == 0)
            {
                KryptonMessageBox.Show("No Data!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning, showCtrlCopy: false);
                return;
            }
            var list = this.dgvSearch.DataSource as List<GroupModel>;
            var userNameList = list.Select(s => s.UserName).ToList();
            ExportUtils.ExprotFile(userNameList);
            KryptonMessageBox.Show("Export Success!", "Info", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information
                , showCtrlCopy: false);

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            new FrmInviteLog().ShowDialog();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                this.dgvInvite.Rows.Clear();
                this.inviteFailure = 0;
                this.inviteSuccess = 0;
                this.labSuccess.Text = "0";
                this.labFail.Text = "0";
            });
        }

        private void GroupControl_Load(object sender, EventArgs e)
        {
            this.kryptonNavigator1.SelectedIndex = 0;
        }

        private void dgvSearch_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvSearch.Columns[e.ColumnIndex].Name == "ColGUserName")
            {
                e.CellStyle.ForeColor = Color.Blue;
                e.CellStyle.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
            }
            else if (this.dgvSearch.Columns[e.ColumnIndex].Name == "ColGSCount")
            {
                e.CellStyle.ForeColor = Color.Green;
                e.CellStyle.Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Bold);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var url = "https://medium.com/@alowoperon668/how-to-invite-users-to-a-group-or-channel-in-telegram-08e10331cfdd";
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void btnPuase_Click(object sender, EventArgs e)
        {
            this.menuJoinPause.Show(this.btnPuase, this.btnPuase.RectangleToScreen(this.btnPuase.ClientRectangle));
        }

        private void kryptonContextMenuItem1_Click(object sender, EventArgs e)
        {
            this.joinCancellation.Cancel();
        }

        private void kryptonContextMenuItem2_Click(object sender, EventArgs e)
        {
            MuClientApi.JoinReset();
        }
    }
}

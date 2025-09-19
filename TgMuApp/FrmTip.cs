using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using TgMuApp.Utils;

namespace TgMuApp
{
    public partial class FrmTip : KryptonForm
    {
        public List<TipMsg> msgList = new List<TipMsg> {
          new TipMsg{  Type=1, Message="Why do you need a send delay?"+Environment.NewLine+
                                       "1. If you send too fast, you will receive telegram :Too Many Requests: retry after xxx Seconds” Or “FLOOD_WAIT”"+Environment.NewLine+
                                       "2. If you ignore telegram feedback many times, your account may be banned"+Environment.NewLine+
                                       "3. Don't send too many messages to strangers, strangers may report you"},
           new TipMsg{  Type=2, Message="Why set an invite delay?"+Environment.NewLine+
                                        "The invitation interval is set to prevent invite too quickly!"+Environment.NewLine+
                                        "Prevent telegram from mistaking it for spam, and return to force waiting!" },

             new TipMsg{  Type=3, Message="Why set the maximum number of invitations per account?"+Environment.NewLine+
                                        "1. In order to prevent spam messages, Telegram Api limits the cumulative maximum number of invitations for all accounts on each IP to 50 (the maximum number of requests is 50, not the maximum number of successful 50)"+Environment.NewLine+
                                        "2. Registration time < 1 month, the maximum number of invitations is 50"+Environment.NewLine+
                                        "3. Registration time > 1 month < 6 months, the maximum number of invitations is 60"+Environment.NewLine+
                                        "4. Registration time > 6 months, the maximum number of invitations is 60-100"+Environment.NewLine+
                                        "5. This quantity is reset every 24 hours"},

               new TipMsg{  Type=4, Message="Why set PEER_FLOOR maximum number of failures?"+Environment.NewLine+
                                        "1 When a PEER_FLOOR error occurs, the waiting time will be accumulated to the reset time (24 hours + wait time)!"+Environment.NewLine+
                                        "2 When many PEER_FLOOR errors occur, your account may be disabled!" +Environment.NewLine+
                                        "3 To protect account security, when the maximum set number is reached, stop inviting!"},

               new TipMsg{ Type=5,Message="Why set Every Account Max Send?"+Environment.NewLine+
                                          "1 Telegram limits each account to send messages to up to 50 strangers!"+Environment.NewLine+
                                          "2 This quantity is reset every 24 hours"+Environment.NewLine
                                          }

        };
        public int MsgType { get; set; }
        public FrmTip()
        {
            InitializeComponent();
            //this.Height = FormUtils.GetAutoHeight(902, 657);
            //this.Width = FormUtils.GetAutoWidth(902);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmTip_Load(object sender, EventArgs e)
        {
            var msg = this.msgList.FirstOrDefault(m => m.Type == this.MsgType);
            if (msg != null)
            {
                this.labMsg.Text = msg.Message;
            }
        }
    }
    public class TipMsg
    {
        public int Type { get; set; }
        public string Message { get; set; }
    }
}

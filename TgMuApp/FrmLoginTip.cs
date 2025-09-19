using Krypton.Toolkit;
using System;
using TgMuApp.Utils;

namespace TgMuApp
{
    public partial class FrmLoginTip : KryptonForm
    {
        public FrmLoginTip()
        {
           
            InitializeComponent();
            //this.Height = FormUtils.GetAutoHeight(1215, 945);
            //this.Width = FormUtils.GetAutoWidth(1215);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

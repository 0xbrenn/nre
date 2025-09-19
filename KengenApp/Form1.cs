using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KengenApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerator_Click(object sender, EventArgs e)
        {
            var deviceId = this.txtDeviceId.Text.Trim();
            if (string.IsNullOrEmpty(deviceId))
            {
                MessageBox.Show("Please enter Device ID!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var days = (int)this.nudays.Value;
            var key = License.Generate(deviceId, days);
            this.txtKey.Text = key;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.txtKey.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}

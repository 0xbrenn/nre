using Krypton.Toolkit;
using System;
using System.Diagnostics;
using System.Reflection;

namespace TgMuApp
{
    public partial class FrmAbout : KryptonForm
    {
        public FrmAbout()
        {
            InitializeComponent();
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            var url = "https://t.me/IonicSupport";
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            var url = "https://codecanyon.net/user/ionicstudio/portfolio";
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        private void FrmAbout1_Load(object sender, EventArgs e)
        {
 
            this.labName.Text = String.Format("About {0}", AssemblyTitle);
            this.labVersion.Text = String.Format("Version {0}", AssemblyVersion);
        
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
        
            var url = "https://bit.ly/rokettgmarket";
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

using Sqlite.Db;
using Sqlite.Db.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TgMuApp.Extension;
using TgMuApp.Utils;

namespace TgMuApp
{
    public partial class FrmWelcome : Form
    {
        private bool Check { get; set; } = false;
        private string Msg { get; set; } = "No authorize,Please authorize!";
        private Repository<LicenseModel> dbService = new Repository<LicenseModel>();
        public FrmWelcome()
        {
            InitializeComponent();
        }

        private void FrmWelcome_Load(object sender, EventArgs e)
        {
            this.progressBar1.Step = 5;
            Task.Factory.StartNew(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(100);
                    this.Invoke(new Action(() => this.progressBar1.PerformStep()));
                }
                var model = this.dbService.GetList().FirstOrDefault();
                if (model == null)
                {
                    this.Check = false;
                }
                else
                {
                    var serial = new DeviceIdGenerator().GenerateDeviceId();
                    var err = string.Empty;
                    this.Check = License.TryValidateAndComputeExpiry(model.Key, serial, out err);
                    Msg = err;
                }
                if (this.Check)
                {
                    this.InvokeOnUiThreadIfRequired(() =>
                    {
                        this.Hide();
                        new FrmMain().ShowDialog();
                    });
                }
                else
                {
                    this.InvokeOnUiThreadIfRequired(() =>
                    {
                        this.Hide();
                        new FrmAuth() { Msg = Msg }.ShowDialog();
                    });
                }
            });
        }
    }
}

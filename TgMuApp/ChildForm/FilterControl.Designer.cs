namespace TgMuApp.ChildForm
{
    partial class FilterControl
    {
        
        private System.ComponentModel.IContainer components = null;

       
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 

      
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterControl));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonListBox1 = new Krypton.Toolkit.KryptonListBox();
            this.kryptonPanel3 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel4 = new Krypton.Toolkit.KryptonPanel();
            this.btnClearName = new Krypton.Toolkit.KryptonButton();
            this.kryptonPanel6 = new Krypton.Toolkit.KryptonPanel();
            this.rbUser = new System.Windows.Forms.RadioButton();
            this.rbPhone = new System.Windows.Forms.RadioButton();
            this.btnImport = new Krypton.Toolkit.KryptonButton();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel5 = new Krypton.Toolkit.KryptonPanel();
            this.btnStop = new Krypton.Toolkit.KryptonButton();
            this.btnStart = new Krypton.Toolkit.KryptonButton();
            this.kryptonStatusStrip1 = new Krypton.Toolkit.KryptonStatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.labLog = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvList = new Krypton.Toolkit.KryptonDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.backMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMenuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuClear = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).BeginInit();
            this.kryptonPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel6)).BeginInit();
            this.kryptonPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel5)).BeginInit();
            this.kryptonPanel5.SuspendLayout();
            this.kryptonStatusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.backMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonPanel2);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonPanel1.Location = new System.Drawing.Point(800, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(325, 729);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonListBox1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonPanel3);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(325, 659);
            this.kryptonGroupBox1.TabIndex = 1;
            this.kryptonGroupBox1.Values.Heading = "Import Phone|Name List";
            // 
            // kryptonListBox1
            // 
            this.kryptonListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonListBox1.Location = new System.Drawing.Point(0, 90);
            this.kryptonListBox1.Name = "kryptonListBox1";
            this.kryptonListBox1.Size = new System.Drawing.Size(321, 536);
            this.kryptonListBox1.TabIndex = 1;
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.kryptonPanel4);
            this.kryptonPanel3.Controls.Add(this.kryptonPanel6);
            this.kryptonPanel3.Controls.Add(this.btnImport);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(321, 90);
            this.kryptonPanel3.TabIndex = 0;
            // 
            // kryptonPanel4
            // 
            this.kryptonPanel4.Controls.Add(this.btnClearName);
            this.kryptonPanel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonPanel4.Location = new System.Drawing.Point(169, 33);
            this.kryptonPanel4.Name = "kryptonPanel4";
            this.kryptonPanel4.Size = new System.Drawing.Size(152, 57);
            this.kryptonPanel4.TabIndex = 7;
            // 
            // btnClearName
            // 
            this.btnClearName.Location = new System.Drawing.Point(41, 6);
            this.btnClearName.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearName.Name = "btnClearName";
            this.btnClearName.Size = new System.Drawing.Size(109, 41);
            this.btnClearName.TabIndex = 4;
            this.btnClearName.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnClearName.Values.Image")));
            this.btnClearName.Values.Text = "Clear";
            this.btnClearName.Click += new System.EventHandler(this.btnClearName_Click);
            // 
            // kryptonPanel6
            // 
            this.kryptonPanel6.Controls.Add(this.rbUser);
            this.kryptonPanel6.Controls.Add(this.rbPhone);
            this.kryptonPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel6.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel6.Name = "kryptonPanel6";
            this.kryptonPanel6.Size = new System.Drawing.Size(321, 33);
            this.kryptonPanel6.TabIndex = 6;
            // 
            // rbUser
            // 
            this.rbUser.AutoSize = true;
            this.rbUser.BackColor = System.Drawing.Color.Transparent;
            this.rbUser.Checked = true;
            this.rbUser.Location = new System.Drawing.Point(177, 5);
            this.rbUser.Name = "rbUser";
            this.rbUser.Size = new System.Drawing.Size(105, 22);
            this.rbUser.TabIndex = 1;
            this.rbUser.TabStop = true;
            this.rbUser.Text = "UserName";
            this.rbUser.UseVisualStyleBackColor = false;
            // 
            // rbPhone
            // 
            this.rbPhone.AutoSize = true;
            this.rbPhone.BackColor = System.Drawing.Color.Transparent;
            this.rbPhone.Location = new System.Drawing.Point(14, 5);
            this.rbPhone.Name = "rbPhone";
            this.rbPhone.Size = new System.Drawing.Size(132, 22);
            this.rbPhone.TabIndex = 0;
            this.rbPhone.Text = "PhoneNumber";
            this.rbPhone.UseVisualStyleBackColor = false;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(14, 39);
            this.btnImport.Margin = new System.Windows.Forms.Padding(2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(116, 41);
            this.btnImport.TabIndex = 5;
            this.btnImport.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Values.Image")));
            this.btnImport.Values.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kryptonPanel5);
            this.kryptonPanel2.Controls.Add(this.btnStart);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 659);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(325, 70);
            this.kryptonPanel2.TabIndex = 0;
            // 
            // kryptonPanel5
            // 
            this.kryptonPanel5.Controls.Add(this.btnStop);
            this.kryptonPanel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonPanel5.Location = new System.Drawing.Point(166, 0);
            this.kryptonPanel5.Name = "kryptonPanel5";
            this.kryptonPanel5.Size = new System.Drawing.Size(159, 70);
            this.kryptonPanel5.TabIndex = 0;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(46, 15);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(109, 41);
            this.btnStop.TabIndex = 8;
            this.btnStop.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Values.Image")));
            this.btnStop.Values.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(16, 15);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(116, 41);
            this.btnStart.TabIndex = 7;
            this.btnStart.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Values.Image")));
            this.btnStart.Values.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // kryptonStatusStrip1
            // 
            this.kryptonStatusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonStatusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.kryptonStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.labLog});
            this.kryptonStatusStrip1.Location = new System.Drawing.Point(0, 707);
            this.kryptonStatusStrip1.Name = "kryptonStatusStrip1";
            this.kryptonStatusStrip1.ProgressBars = null;
            this.kryptonStatusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.kryptonStatusStrip1.Size = new System.Drawing.Size(800, 22);
            this.kryptonStatusStrip1.TabIndex = 1;
            this.kryptonStatusStrip1.Text = "kryptonStatusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 24);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBar1.Visible = false;
            // 
            // labLog
            // 
            this.labLog.BackColor = System.Drawing.Color.White;
            this.labLog.ForeColor = System.Drawing.Color.Red;
            this.labLog.Name = "labLog";
            this.labLog.Size = new System.Drawing.Size(785, 15);
            this.labLog.Spring = true;
            this.labLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column7,
            this.Column6});
            this.dgvList.ContextMenuStrip = this.backMenu;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidth = 62;
            this.dgvList.RowTemplate.Height = 30;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(800, 707);
            this.dgvList.StateNormal.Background.Color1 = System.Drawing.Color.White;
            this.dgvList.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Account";
            this.Column1.MinimumWidth = 80;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 114;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Phone";
            this.Column2.MinimumWidth = 80;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 114;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "FirstName";
            this.Column3.MinimumWidth = 80;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 114;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "LastName";
            this.Column4.MinimumWidth = 80;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 115;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "UserName";
            this.Column5.MinimumWidth = 80;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 114;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "IsSuccess";
            this.Column7.MinimumWidth = 100;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 114;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.HeaderText = "Status";
            this.Column6.MinimumWidth = 100;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // backMenu
            // 
            this.backMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.backMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.backMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuExport,
            this.tsMenuClear});
            this.backMenu.Name = "linkMenu";
            this.backMenu.Size = new System.Drawing.Size(152, 84);
            // 
            // tsMenuExport
            // 
            this.tsMenuExport.Image = ((System.Drawing.Image)(resources.GetObject("tsMenuExport.Image")));
            this.tsMenuExport.Name = "tsMenuExport";
            this.tsMenuExport.Size = new System.Drawing.Size(151, 40);
            this.tsMenuExport.Text = "Export";
            this.tsMenuExport.Click += new System.EventHandler(this.tsMenuExport_Click);
            // 
            // tsMenuClear
            // 
            this.tsMenuClear.Image = ((System.Drawing.Image)(resources.GetObject("tsMenuClear.Image")));
            this.tsMenuClear.Name = "tsMenuClear";
            this.tsMenuClear.Size = new System.Drawing.Size(151, 40);
            this.tsMenuClear.Text = "Clear";
            this.tsMenuClear.Click += new System.EventHandler(this.tsMenuClear_Click);
            // 
            // FilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.kryptonStatusStrip1);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "FilterControl";
            this.Size = new System.Drawing.Size(1125, 729);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).EndInit();
            this.kryptonPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel6)).EndInit();
            this.kryptonPanel6.ResumeLayout(false);
            this.kryptonPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel5)).EndInit();
            this.kryptonPanel5.ResumeLayout(false);
            this.kryptonStatusStrip1.ResumeLayout(false);
            this.kryptonStatusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.backMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private Krypton.Toolkit.KryptonButton btnImport;
        private Krypton.Toolkit.KryptonButton btnClearName;
        private Krypton.Toolkit.KryptonPanel kryptonPanel5;
        private Krypton.Toolkit.KryptonButton btnStop;
        private Krypton.Toolkit.KryptonButton btnStart;
        private Krypton.Toolkit.KryptonListBox kryptonListBox1;
        private Krypton.Toolkit.KryptonStatusStrip kryptonStatusStrip1;
        private Krypton.Toolkit.KryptonDataGridView dgvList;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel labLog;
        private System.Windows.Forms.ContextMenuStrip backMenu;
        private System.Windows.Forms.ToolStripMenuItem tsMenuExport;
        private System.Windows.Forms.ToolStripMenuItem tsMenuClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private Krypton.Toolkit.KryptonPanel kryptonPanel6;
        private Krypton.Toolkit.KryptonPanel kryptonPanel4;
        private System.Windows.Forms.RadioButton rbUser;
        private System.Windows.Forms.RadioButton rbPhone;
    }
}

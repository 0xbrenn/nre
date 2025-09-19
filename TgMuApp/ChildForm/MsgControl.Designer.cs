namespace TgMuApp.ChildForm
{
    partial class MsgControl
    {
        /// <summary> 
        ///  
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        ///  
        /// </summary>
        /// <param name="disposing"> </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region  

        /// <summary> 
         
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgControl));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel3 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.btnAdd = new Krypton.Toolkit.KryptonButton();
            this.btnDel = new Krypton.Toolkit.KryptonButton();
            this.btnEdit = new Krypton.Toolkit.KryptonButton();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.dgvList = new Krypton.Toolkit.KryptonDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonPanel3);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 652);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(802, 75);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.kryptonButton1);
            this.kryptonPanel3.Controls.Add(this.btnAdd);
            this.kryptonPanel3.Controls.Add(this.btnDel);
            this.kryptonPanel3.Controls.Add(this.btnEdit);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonPanel3.Location = new System.Drawing.Point(257, 0);
            this.kryptonPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(545, 75);
            this.kryptonPanel3.TabIndex = 0;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(31, 14);
            this.kryptonButton1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(174, 49);
            this.kryptonButton1.TabIndex = 9;
            this.kryptonButton1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonButton1.Values.Image")));
            this.kryptonButton1.Values.Text = "MarkdownV2";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(209, 14);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(106, 49);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Values.Image")));
            this.btnAdd.Values.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(430, 14);
            this.btnDel.Margin = new System.Windows.Forms.Padding(2);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(106, 49);
            this.btnDel.TabIndex = 7;
            this.btnDel.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Values.Image")));
            this.btnDel.Values.Text = "Del";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(320, 14);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(106, 49);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Values.Image")));
            this.btnEdit.Values.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.dgvList);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(802, 652);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column6,
            this.Column4,
            this.Column5});
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.Margin = new System.Windows.Forms.Padding(2);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidth = 82;
            this.dgvList.RowTemplate.Height = 37;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(802, 652);
            this.dgvList.StateNormal.Background.Color1 = System.Drawing.Color.White;
            this.dgvList.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Id";
            this.Column1.HeaderText = "Id";
            this.Column1.MinimumWidth = 10;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Title";
            this.Column2.FillWeight = 29.13546F;
            this.Column2.HeaderText = "Title";
            this.Column2.MinimumWidth = 100;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column3.DataPropertyName = "Type";
            this.Column3.FillWeight = 29.13546F;
            this.Column3.HeaderText = "Type";
            this.Column3.MinimumWidth = 100;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column6.DataPropertyName = "Cate";
            this.Column6.FillWeight = 29.13546F;
            this.Column6.HeaderText = "Cate";
            this.Column6.MinimumWidth = 100;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "File";
            this.Column4.FillWeight = 29.13546F;
            this.Column4.HeaderText = "File";
            this.Column4.MinimumWidth = 10;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Content";
            this.Column5.FillWeight = 29.13546F;
            this.Column5.HeaderText = "Content";
            this.Column5.MinimumWidth = 10;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // MsgControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MsgControl";
            this.Size = new System.Drawing.Size(802, 727);
            this.Load += new System.EventHandler(this.MsgControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private Krypton.Toolkit.KryptonDataGridView dgvList;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonButton btnDel;
        private Krypton.Toolkit.KryptonButton btnEdit;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}

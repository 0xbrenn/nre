namespace KengenApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnGenerator = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDeviceId = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.nudays = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudays)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnCopy);
            this.panel1.Controls.Add(this.btnGenerator);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 604);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(770, 99);
            this.panel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(604, 24);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(142, 53);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(189, 24);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(142, 53);
            this.btnCopy.TabIndex = 1;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnGenerator
            // 
            this.btnGenerator.Location = new System.Drawing.Point(25, 24);
            this.btnGenerator.Name = "btnGenerator";
            this.btnGenerator.Size = new System.Drawing.Size(142, 53);
            this.btnGenerator.TabIndex = 0;
            this.btnGenerator.Text = "Generator";
            this.btnGenerator.UseVisualStyleBackColor = true;
            this.btnGenerator.Click += new System.EventHandler(this.btnGenerator_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtDeviceId, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtKey, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.nudays, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(770, 604);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(139, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "DeviceId:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(175, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Days:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(184, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 540);
            this.label3.TabIndex = 2;
            this.label3.Text = "Key:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDeviceId
            // 
            this.txtDeviceId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDeviceId.Location = new System.Drawing.Point(234, 3);
            this.txtDeviceId.Name = "txtDeviceId";
            this.txtDeviceId.Size = new System.Drawing.Size(533, 28);
            this.txtDeviceId.TabIndex = 3;
            // 
            // txtKey
            // 
            this.txtKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKey.Location = new System.Drawing.Point(234, 67);
            this.txtKey.Multiline = true;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(533, 534);
            this.txtKey.TabIndex = 4;
            // 
            // nudays
            // 
            this.nudays.Location = new System.Drawing.Point(234, 35);
            this.nudays.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudays.Name = "nudays";
            this.nudays.Size = new System.Drawing.Size(120, 28);
            this.nudays.TabIndex = 5;
            this.nudays.Value = new decimal(new int[] {
            365,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 703);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudays)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDeviceId;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.NumericUpDown nudays;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnGenerator;
    }
}
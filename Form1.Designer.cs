using System;

namespace CsharpInterface
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
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btConnect = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.btRun = new System.Windows.Forms.Button();
            this.btPause = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btCheck = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(497, 2);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(5);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(1059, 750);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 25);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(120, 24);
            this.comboBox1.TabIndex = 1;
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(195, 2);
            this.btConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(120, 46);
            this.btConnect.TabIndex = 2;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // btExit
            // 
            this.btExit.Location = new System.Drawing.Point(371, 2);
            this.btExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(120, 46);
            this.btExit.TabIndex = 4;
            this.btExit.Text = "Exit";
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(195, 106);
            this.btSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(120, 46);
            this.btSave.TabIndex = 5;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btRun
            // 
            this.btRun.Location = new System.Drawing.Point(12, 65);
            this.btRun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btRun.Name = "btRun";
            this.btRun.Size = new System.Drawing.Size(120, 25);
            this.btRun.TabIndex = 6;
            this.btRun.Text = "Check";
            this.btRun.UseVisualStyleBackColor = true;
            this.btRun.Click += new System.EventHandler(this.btCheck_Click);
            // 
            // btPause
            // 
            this.btPause.Location = new System.Drawing.Point(371, 106);
            this.btPause.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btPause.Name = "btPause";
            this.btPause.Size = new System.Drawing.Size(120, 46);
            this.btPause.TabIndex = 7;
            this.btPause.Text = "Pause";
            this.btPause.UseVisualStyleBackColor = true;
            this.btPause.Click += new System.EventHandler(this.btPause_Click);
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(371, 54);
            this.btClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(120, 46);
            this.btClear.TabIndex = 8;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM5";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(10, 164);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(479, 573);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Potential (mV)";
            this.columnHeader1.Width = 163;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Current (mA)";
            this.columnHeader2.Width = 163;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 106);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(120, 25);
            this.progressBar1.TabIndex = 10;
            // 
            // btCheck
            // 
            this.btCheck.Location = new System.Drawing.Point(195, 53);
            this.btCheck.Name = "btCheck";
            this.btCheck.Size = new System.Drawing.Size(120, 47);
            this.btCheck.TabIndex = 11;
            this.btCheck.Text = "Start";
            this.btCheck.UseVisualStyleBackColor = true;
            this.btCheck.Click += new System.EventHandler(this.btRun_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1557, 748);
            this.Controls.Add(this.btCheck);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.btPause);
            this.Controls.Add(this.btRun);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.zedGraphControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Research and development of cyclic voltammetry measuring system for biomedical te" +
    "sting";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.Button btPause;
        private System.Windows.Forms.Button btClear;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btCheck;
    }
}


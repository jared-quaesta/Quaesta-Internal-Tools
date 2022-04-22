
using System.Windows.Forms;

namespace QIYRapidTest
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.npmsSelect = new System.Windows.Forms.CheckedListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.beginTestBtn = new System.Windows.Forms.Button();
            this.consolesCollect = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listenerWorker = new System.ComponentModel.BackgroundWorker();
            this.discoveryWorker = new System.ComponentModel.BackgroundWorker();
            this.testFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.consolesCollect.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdBox
            // 
            this.cmdBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmdBox.Location = new System.Drawing.Point(152, 26);
            this.cmdBox.Multiline = true;
            this.cmdBox.Name = "cmdBox";
            this.cmdBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.cmdBox.Size = new System.Drawing.Size(279, 368);
            this.cmdBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Commands to send on wake (separated by newline)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(152, 401);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(280, 69);
            this.button1.TabIndex = 2;
            this.button1.Text = "Sync Commands";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // npmsSelect
            // 
            this.npmsSelect.CheckOnClick = true;
            this.npmsSelect.FormattingEnabled = true;
            this.npmsSelect.Location = new System.Drawing.Point(12, 41);
            this.npmsSelect.Name = "npmsSelect";
            this.npmsSelect.Size = new System.Drawing.Size(120, 364);
            this.npmsSelect.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Search for NPMs";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SearchBtnClick);
            // 
            // connectButton
            // 
            this.connectButton.ForeColor = System.Drawing.Color.Black;
            this.connectButton.Location = new System.Drawing.Point(12, 412);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(120, 58);
            this.connectButton.TabIndex = 5;
            this.connectButton.Text = "Connect to selected";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_ClickAsync);
            // 
            // beginTestBtn
            // 
            this.beginTestBtn.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.beginTestBtn.ForeColor = System.Drawing.Color.Green;
            this.beginTestBtn.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.beginTestBtn.Location = new System.Drawing.Point(12, 476);
            this.beginTestBtn.Name = "beginTestBtn";
            this.beginTestBtn.Size = new System.Drawing.Size(420, 36);
            this.beginTestBtn.TabIndex = 6;
            this.beginTestBtn.Text = "Begin Test";
            this.beginTestBtn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.beginTestBtn.UseVisualStyleBackColor = true;
            this.beginTestBtn.Click += new System.EventHandler(this.BeginTest);
            // 
            // consolesCollect
            // 
            this.consolesCollect.Controls.Add(this.tabPage1);
            this.consolesCollect.Controls.Add(this.tabPage2);
            this.consolesCollect.Location = new System.Drawing.Point(439, 12);
            this.consolesCollect.Name = "consolesCollect";
            this.consolesCollect.SelectedIndex = 0;
            this.consolesCollect.Size = new System.Drawing.Size(1013, 500);
            this.consolesCollect.TabIndex = 7;
            this.consolesCollect.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.ChangedTabs);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1005, 472);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1005, 472);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listenerWorker
            // 
            this.listenerWorker.WorkerReportsProgress = true;
            this.listenerWorker.WorkerSupportsCancellation = true;
            this.listenerWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ListenDelay);
            this.listenerWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.CheckBuffer);
            // 
            // discoveryWorker
            // 
            this.discoveryWorker.WorkerReportsProgress = true;
            this.discoveryWorker.WorkerSupportsCancellation = true;
            this.discoveryWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.UDPBroadcast);
            this.discoveryWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.AddIP);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1464, 519);
            this.Controls.Add(this.consolesCollect);
            this.Controls.Add(this.beginTestBtn);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.npmsSelect);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdBox);
            this.Name = "MainForm";
            this.Text = "QIY Rapid Test";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.consolesCollect.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cmdBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox npmsSelect;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button beginTestBtn;
        private System.Windows.Forms.TabControl consolesCollect;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.ComponentModel.BackgroundWorker listenerWorker;
        private System.ComponentModel.BackgroundWorker discoveryWorker;
        private FolderBrowserDialog testFolderDialog;
    }
}


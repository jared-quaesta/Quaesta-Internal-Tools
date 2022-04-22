
namespace HardRebootQIY
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
            this.beginTest = new System.Windows.Forms.Button();
            this.pathText = new System.Windows.Forms.TextBox();
            this.pathFindBtn = new System.Windows.Forms.Button();
            this.seeFile = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.infoStringView = new System.Windows.Forms.RichTextBox();
            this.testLoop = new System.ComponentModel.BackgroundWorker();
            this.nrcs = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // beginTest
            // 
            this.beginTest.ForeColor = System.Drawing.Color.Green;
            this.beginTest.Location = new System.Drawing.Point(2, 591);
            this.beginTest.Name = "beginTest";
            this.beginTest.Size = new System.Drawing.Size(172, 59);
            this.beginTest.TabIndex = 1;
            this.beginTest.Text = "Begin Test";
            this.beginTest.UseVisualStyleBackColor = true;
            this.beginTest.Click += new System.EventHandler(this.beginTest_Click);
            // 
            // pathText
            // 
            this.pathText.Location = new System.Drawing.Point(0, 656);
            this.pathText.Name = "pathText";
            this.pathText.Size = new System.Drawing.Size(547, 20);
            this.pathText.TabIndex = 2;
            // 
            // pathFindBtn
            // 
            this.pathFindBtn.Location = new System.Drawing.Point(549, 653);
            this.pathFindBtn.Name = "pathFindBtn";
            this.pathFindBtn.Size = new System.Drawing.Size(29, 23);
            this.pathFindBtn.TabIndex = 3;
            this.pathFindBtn.Text = "...";
            this.pathFindBtn.UseVisualStyleBackColor = true;
            this.pathFindBtn.Click += new System.EventHandler(this.pathFindBtn_Click);
            // 
            // seeFile
            // 
            this.seeFile.FormattingEnabled = true;
            this.seeFile.Items.AddRange(new object[] {
            "192.168.15.32",
            "192.168.15.33"});
            this.seeFile.Location = new System.Drawing.Point(2, 22);
            this.seeFile.Name = "seeFile";
            this.seeFile.Size = new System.Drawing.Size(172, 563);
            this.seeFile.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Click IP To See Last Bad Info:";
            // 
            // infoStringView
            // 
            this.infoStringView.Location = new System.Drawing.Point(181, 22);
            this.infoStringView.Name = "infoStringView";
            this.infoStringView.Size = new System.Drawing.Size(397, 628);
            this.infoStringView.TabIndex = 6;
            this.infoStringView.Text = "";
            // 
            // testLoop
            // 
            this.testLoop.WorkerReportsProgress = true;
            this.testLoop.WorkerSupportsCancellation = true;
            this.testLoop.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TestSegment);
            this.testLoop.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ShowResults);
            // 
            // nrcs
            // 
            this.nrcs.AutoSize = true;
            this.nrcs.Location = new System.Drawing.Point(366, 6);
            this.nrcs.Name = "nrcs";
            this.nrcs.Size = new System.Drawing.Size(71, 13);
            this.nrcs.TabIndex = 7;
            this.nrcs.Text = "Reconnects: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 677);
            this.Controls.Add(this.nrcs);
            this.Controls.Add(this.infoStringView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.seeFile);
            this.Controls.Add(this.pathFindBtn);
            this.Controls.Add(this.pathText);
            this.Controls.Add(this.beginTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button beginTest;
        private System.Windows.Forms.TextBox pathText;
        private System.Windows.Forms.Button pathFindBtn;
        private System.Windows.Forms.ListBox seeFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RichTextBox infoStringView;
        private System.ComponentModel.BackgroundWorker testLoop;
        private System.Windows.Forms.Label nrcs;
    }
}


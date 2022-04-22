
namespace QIY_Interface__IAEA_
{
    partial class FWUpgradeTCP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FWUpgradeTCP));
            this.chooseFWBtn = new System.Windows.Forms.Button();
            this.sendFWBtn = new System.Windows.Forms.Button();
            this.transProgress = new System.Windows.Forms.ProgressBar();
            this.textProgress = new System.Windows.Forms.Label();
            this.findFirmwareDialog = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // chooseFWBtn
            // 
            this.chooseFWBtn.Location = new System.Drawing.Point(4, 2);
            this.chooseFWBtn.Name = "chooseFWBtn";
            this.chooseFWBtn.Size = new System.Drawing.Size(75, 23);
            this.chooseFWBtn.TabIndex = 0;
            this.chooseFWBtn.Text = "Choose FW";
            this.chooseFWBtn.UseVisualStyleBackColor = true;
            this.chooseFWBtn.Click += new System.EventHandler(this.ChooseFWAsync);
            // 
            // sendFWBtn
            // 
            this.sendFWBtn.Location = new System.Drawing.Point(4, 29);
            this.sendFWBtn.Name = "sendFWBtn";
            this.sendFWBtn.Size = new System.Drawing.Size(75, 23);
            this.sendFWBtn.TabIndex = 1;
            this.sendFWBtn.Text = "Send FW";
            this.sendFWBtn.UseVisualStyleBackColor = true;
            this.sendFWBtn.Click += new System.EventHandler(this.upgradeBtn_Click);
            // 
            // transProgress
            // 
            this.transProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transProgress.Location = new System.Drawing.Point(4, 58);
            this.transProgress.Name = "transProgress";
            this.transProgress.Size = new System.Drawing.Size(320, 23);
            this.transProgress.TabIndex = 2;
            // 
            // textProgress
            // 
            this.textProgress.AutoSize = true;
            this.textProgress.Location = new System.Drawing.Point(82, 39);
            this.textProgress.Name = "textProgress";
            this.textProgress.Size = new System.Drawing.Size(0, 13);
            this.textProgress.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FWUpgradeTCP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 87);
            this.Controls.Add(this.textProgress);
            this.Controls.Add(this.transProgress);
            this.Controls.Add(this.sendFWBtn);
            this.Controls.Add(this.chooseFWBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FWUpgradeTCP";
            this.Text = "FWUpgradeTCP";
            this.Load += new System.EventHandler(this.FWUpgradeTCP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button chooseFWBtn;
        private System.Windows.Forms.Button sendFWBtn;
        private System.Windows.Forms.ProgressBar transProgress;
        private System.Windows.Forms.Label textProgress;
        private System.Windows.Forms.OpenFileDialog findFirmwareDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
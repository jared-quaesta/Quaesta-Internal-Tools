
namespace NPM_General_App.Utilities
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
            this.chooseFWBtn = new System.Windows.Forms.Button();
            this.upgradeBtn = new System.Windows.Forms.Button();
            this.findFirmwareDialog = new System.Windows.Forms.OpenFileDialog();
            this.transProgress = new System.Windows.Forms.ProgressBar();
            this.textProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chooseFWBtn
            // 
            this.chooseFWBtn.Location = new System.Drawing.Point(12, 12);
            this.chooseFWBtn.Name = "chooseFWBtn";
            this.chooseFWBtn.Size = new System.Drawing.Size(75, 23);
            this.chooseFWBtn.TabIndex = 0;
            this.chooseFWBtn.Text = "Choose fw";
            this.chooseFWBtn.UseVisualStyleBackColor = true;
            this.chooseFWBtn.Click += new System.EventHandler(this.ChooseFWAsync);
            // 
            // upgradeBtn
            // 
            this.upgradeBtn.Enabled = false;
            this.upgradeBtn.Location = new System.Drawing.Point(12, 41);
            this.upgradeBtn.Name = "upgradeBtn";
            this.upgradeBtn.Size = new System.Drawing.Size(75, 23);
            this.upgradeBtn.TabIndex = 1;
            this.upgradeBtn.Text = "Send FW";
            this.upgradeBtn.UseVisualStyleBackColor = true;
            this.upgradeBtn.Click += new System.EventHandler(this.upgradeBtn_Click);
            // 
            // findFirmwareDialog
            // 
            this.findFirmwareDialog.FileName = "openFileDialog1";
            // 
            // transProgress
            // 
            this.transProgress.Location = new System.Drawing.Point(12, 70);
            this.transProgress.Name = "transProgress";
            this.transProgress.Size = new System.Drawing.Size(269, 23);
            this.transProgress.TabIndex = 2;
            // 
            // textProgress
            // 
            this.textProgress.AutoSize = true;
            this.textProgress.Location = new System.Drawing.Point(112, 48);
            this.textProgress.Name = "textProgress";
            this.textProgress.Size = new System.Drawing.Size(0, 15);
            this.textProgress.TabIndex = 3;
            // 
            // FWUpgradeTCP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 101);
            this.Controls.Add(this.textProgress);
            this.Controls.Add(this.transProgress);
            this.Controls.Add(this.upgradeBtn);
            this.Controls.Add(this.chooseFWBtn);
            this.Name = "FWUpgradeTCP";
            this.Text = "FWUpgradeTCP";
            this.Load += new System.EventHandler(this.FWUpgradeTCP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button chooseFWBtn;
        private System.Windows.Forms.Button upgradeBtn;
        private System.Windows.Forms.OpenFileDialog findFirmwareDialog;
        private System.Windows.Forms.ProgressBar transProgress;
        private System.Windows.Forms.Label textProgress;
    }
}
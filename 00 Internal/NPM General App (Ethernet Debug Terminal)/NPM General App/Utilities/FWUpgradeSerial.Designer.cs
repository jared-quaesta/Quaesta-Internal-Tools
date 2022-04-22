
namespace NPM_General_App.Utilities
{
    partial class FWUpgradeSerial
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
            this.upgradeBtn = new System.Windows.Forms.Button();
            this.chooseFWBtn = new System.Windows.Forms.Button();
            this.findFirmwareDialog = new System.Windows.Forms.OpenFileDialog();
            this.textProgress = new System.Windows.Forms.Label();
            this.transProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // upgradeBtn
            // 
            this.upgradeBtn.Enabled = false;
            this.upgradeBtn.Location = new System.Drawing.Point(12, 41);
            this.upgradeBtn.Name = "upgradeBtn";
            this.upgradeBtn.Size = new System.Drawing.Size(75, 23);
            this.upgradeBtn.TabIndex = 5;
            this.upgradeBtn.Text = "Send FW";
            this.upgradeBtn.UseVisualStyleBackColor = true;
            this.upgradeBtn.Click += new System.EventHandler(this.upgradeBtn_Click);
            // 
            // chooseFWBtn
            // 
            this.chooseFWBtn.Location = new System.Drawing.Point(12, 12);
            this.chooseFWBtn.Name = "chooseFWBtn";
            this.chooseFWBtn.Size = new System.Drawing.Size(75, 23);
            this.chooseFWBtn.TabIndex = 4;
            this.chooseFWBtn.Text = "Choose fw";
            this.chooseFWBtn.UseVisualStyleBackColor = true;
            this.chooseFWBtn.Click += new System.EventHandler(this.chooseFWBtn_Click);
            // 
            // findFirmwareDialog
            // 
            this.findFirmwareDialog.FileName = "openFileDialog1";
            // 
            // textProgress
            // 
            this.textProgress.AutoSize = true;
            this.textProgress.Location = new System.Drawing.Point(112, 48);
            this.textProgress.Name = "textProgress";
            this.textProgress.Size = new System.Drawing.Size(0, 15);
            this.textProgress.TabIndex = 7;
            // 
            // transProgress
            // 
            this.transProgress.Location = new System.Drawing.Point(12, 70);
            this.transProgress.Name = "transProgress";
            this.transProgress.Size = new System.Drawing.Size(327, 23);
            this.transProgress.TabIndex = 6;
            // 
            // FWUpgradeSerial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 103);
            this.Controls.Add(this.upgradeBtn);
            this.Controls.Add(this.chooseFWBtn);
            this.Controls.Add(this.textProgress);
            this.Controls.Add(this.transProgress);
            this.Name = "FWUpgradeSerial";
            this.Text = "FWUpgradeSerial";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button upgradeBtn;
        private System.Windows.Forms.Button chooseFWBtn;
        private System.Windows.Forms.OpenFileDialog findFirmwareDialog;
        private System.Windows.Forms.Label textProgress;
        private System.Windows.Forms.ProgressBar transProgress;
    }
}
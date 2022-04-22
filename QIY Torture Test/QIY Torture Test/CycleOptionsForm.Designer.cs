
namespace QIY_Torture_Test
{
    partial class CycleOptionsForm
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
            this.testsCheckListBox = new System.Windows.Forms.CheckedListBox();
            this.newTestBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // testsCheckListBox
            // 
            this.testsCheckListBox.CheckOnClick = true;
            this.testsCheckListBox.FormattingEnabled = true;
            this.testsCheckListBox.Items.AddRange(new object[] {
            "powerOff; Connect; time 1; uptime 1; voltage 2; info 1; saveHGM=1; saveBin=1; sav" +
                "eDat=1; 1h",
            "powerOff; Connect; time 1; uptime 1; voltage 2; info 1; saveHGM=0; saveBin=0; sav" +
                "eDat=0; 1h",
            "ethOff; Connect; time 1; uptime 1; voltage 2; info 1; saveHGM=1; saveBin=1; saveD" +
                "at=1; 1h",
            "ethOff; Connect; time 1; uptime 1; voltage 2; info 1; saveHGM=0; saveBin=0; saveD" +
                "at=0; 1h",
            "noCycle; ping 1; saveHGM=1; saveBin=1; saveDat=1; 1h",
            "noCycle; ping 1; saveHGM=0; saveBin=0; saveDat=0; 1h"});
            this.testsCheckListBox.Location = new System.Drawing.Point(12, 13);
            this.testsCheckListBox.Name = "testsCheckListBox";
            this.testsCheckListBox.Size = new System.Drawing.Size(443, 214);
            this.testsCheckListBox.TabIndex = 10;
            // 
            // newTestBtn
            // 
            this.newTestBtn.Location = new System.Drawing.Point(12, 233);
            this.newTestBtn.Name = "newTestBtn";
            this.newTestBtn.Size = new System.Drawing.Size(443, 23);
            this.newTestBtn.TabIndex = 11;
            this.newTestBtn.Text = "New";
            this.newTestBtn.UseVisualStyleBackColor = true;
            this.newTestBtn.Click += new System.EventHandler(this.newTestBtn_Click);
            // 
            // CycleOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 264);
            this.Controls.Add(this.newTestBtn);
            this.Controls.Add(this.testsCheckListBox);
            this.Name = "CycleOptionsForm";
            this.Text = "Cycles";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HideForm);
            this.Load += new System.EventHandler(this.CycleOptionsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button newTestBtn;
        internal System.Windows.Forms.CheckedListBox testsCheckListBox;
    }
}
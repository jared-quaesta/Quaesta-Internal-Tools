
namespace UniversalUpdate
{
    partial class STLinkForm
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
            this.flashBtn = new System.Windows.Forms.Button();
            this.fwBox = new System.Windows.Forms.ComboBox();
            this.progressTxt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flashBtn
            // 
            this.flashBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flashBtn.ForeColor = System.Drawing.Color.Green;
            this.flashBtn.Location = new System.Drawing.Point(13, 40);
            this.flashBtn.Name = "flashBtn";
            this.flashBtn.Size = new System.Drawing.Size(351, 34);
            this.flashBtn.TabIndex = 0;
            this.flashBtn.Text = "Flash";
            this.flashBtn.UseVisualStyleBackColor = true;
            this.flashBtn.Click += new System.EventHandler(this.flashBtn_Click);
            // 
            // fwBox
            // 
            this.fwBox.FormattingEnabled = true;
            this.fwBox.Items.AddRange(new object[] {
            "Rev C",
            "Rev D",
            "QIX Suri LP"});
            this.fwBox.Location = new System.Drawing.Point(13, 13);
            this.fwBox.Name = "fwBox";
            this.fwBox.Size = new System.Drawing.Size(351, 21);
            this.fwBox.TabIndex = 1;
            // 
            // progressTxt
            // 
            this.progressTxt.AutoSize = true;
            this.progressTxt.Location = new System.Drawing.Point(13, 81);
            this.progressTxt.Name = "progressTxt";
            this.progressTxt.Size = new System.Drawing.Size(52, 13);
            this.progressTxt.TabIndex = 2;
            this.progressTxt.Text = "Waiting...";
            // 
            // STLinkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 98);
            this.Controls.Add(this.progressTxt);
            this.Controls.Add(this.fwBox);
            this.Controls.Add(this.flashBtn);
            this.Name = "STLinkForm";
            this.Text = "ST-Link Utility";
            this.Load += new System.EventHandler(this.STLinkForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button flashBtn;
        private System.Windows.Forms.ComboBox fwBox;
        private System.Windows.Forms.Label progressTxt;
    }
}

namespace UniversalUpdate
{
    partial class Finished
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
            this.infoTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comLbl = new System.Windows.Forms.Label();
            this.fwLbl = new System.Windows.Forms.Label();
            this.fwdBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // infoTxt
            // 
            this.infoTxt.Location = new System.Drawing.Point(12, 78);
            this.infoTxt.Multiline = true;
            this.infoTxt.Name = "infoTxt";
            this.infoTxt.ReadOnly = true;
            this.infoTxt.Size = new System.Drawing.Size(470, 473);
            this.infoTxt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Info:";
            // 
            // comLbl
            // 
            this.comLbl.AutoSize = true;
            this.comLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comLbl.Location = new System.Drawing.Point(12, 13);
            this.comLbl.Name = "comLbl";
            this.comLbl.Size = new System.Drawing.Size(70, 18);
            this.comLbl.TabIndex = 2;
            this.comLbl.Text = "COM 12";
            // 
            // fwLbl
            // 
            this.fwLbl.AutoSize = true;
            this.fwLbl.Location = new System.Drawing.Point(16, 35);
            this.fwLbl.Name = "fwLbl";
            this.fwLbl.Size = new System.Drawing.Size(52, 13);
            this.fwLbl.TabIndex = 3;
            this.fwLbl.Text = "Firmware:";
            // 
            // fwdBtn
            // 
            this.fwdBtn.Location = new System.Drawing.Point(287, 557);
            this.fwdBtn.Name = "fwdBtn";
            this.fwdBtn.Size = new System.Drawing.Size(195, 23);
            this.fwdBtn.TabIndex = 4;
            this.fwdBtn.Text = ">";
            this.fwdBtn.UseVisualStyleBackColor = true;
            this.fwdBtn.Click += new System.EventHandler(this.fwdBtn_Click);
            // 
            // backBtn
            // 
            this.backBtn.Location = new System.Drawing.Point(12, 557);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(195, 23);
            this.backBtn.TabIndex = 5;
            this.backBtn.Text = "<";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // Finished
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 594);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.fwdBtn);
            this.Controls.Add(this.fwLbl);
            this.Controls.Add(this.comLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.infoTxt);
            this.Name = "Finished";
            this.Text = "End";
            this.Load += new System.EventHandler(this.Finished_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox infoTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label comLbl;
        private System.Windows.Forms.Label fwLbl;
        private System.Windows.Forms.Button fwdBtn;
        private System.Windows.Forms.Button backBtn;
    }
}
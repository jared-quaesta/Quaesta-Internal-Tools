
namespace GeneralFirstPhase
{
    partial class SelectDL
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
            this.comSel = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chooseBtn = new System.Windows.Forms.Button();
            this.abortBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comSel
            // 
            this.comSel.FormattingEnabled = true;
            this.comSel.Location = new System.Drawing.Point(12, 52);
            this.comSel.Name = "comSel";
            this.comSel.Size = new System.Drawing.Size(202, 160);
            this.comSel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "This tool has detected multiple dataloggers. Select the one connected to the NPMs" +
    ".";
            // 
            // chooseBtn
            // 
            this.chooseBtn.Location = new System.Drawing.Point(114, 218);
            this.chooseBtn.Name = "chooseBtn";
            this.chooseBtn.Size = new System.Drawing.Size(100, 23);
            this.chooseBtn.TabIndex = 2;
            this.chooseBtn.Text = "Choose Selected";
            this.chooseBtn.UseVisualStyleBackColor = true;
            this.chooseBtn.Click += new System.EventHandler(this.chooseBtn_Click);
            // 
            // abortBtn
            // 
            this.abortBtn.ForeColor = System.Drawing.Color.Red;
            this.abortBtn.Location = new System.Drawing.Point(12, 218);
            this.abortBtn.Name = "abortBtn";
            this.abortBtn.Size = new System.Drawing.Size(100, 23);
            this.abortBtn.TabIndex = 3;
            this.abortBtn.Text = "Abort Test";
            this.abortBtn.UseVisualStyleBackColor = true;
            this.abortBtn.Click += new System.EventHandler(this.abortBtn_Click);
            // 
            // SelectDL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 251);
            this.Controls.Add(this.abortBtn);
            this.Controls.Add(this.chooseBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comSel);
            this.Name = "SelectDL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HideForm);
            this.Load += new System.EventHandler(this.SelectDL_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox comSel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button chooseBtn;
        private System.Windows.Forms.Button abortBtn;
    }
}
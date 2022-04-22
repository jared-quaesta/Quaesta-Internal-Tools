
namespace QIXSerialize
{
    partial class Form1
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
            this.Status = new System.Windows.Forms.Label();
            this.comName = new System.Windows.Forms.Label();
            this.devSearch = new System.ComponentModel.BackgroundWorker();
            this.conSn = new System.Windows.Forms.Label();
            this.output = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Font = new System.Drawing.Font("Segoe UI", 45F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Status.ForeColor = System.Drawing.Color.Red;
            this.Status.Location = new System.Drawing.Point(12, 9);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(436, 81);
            this.Status.TabIndex = 0;
            this.Status.Text = "Not Connected";
            // 
            // comName
            // 
            this.comName.AutoSize = true;
            this.comName.Location = new System.Drawing.Point(13, 94);
            this.comName.Name = "comName";
            this.comName.Size = new System.Drawing.Size(63, 15);
            this.comName.TabIndex = 1;
            this.comName.Text = "comName";
            // 
            // devSearch
            // 
            this.devSearch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.waitTimer);
            this.devSearch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CheckNewDev);
            // 
            // conSn
            // 
            this.conSn.AutoSize = true;
            this.conSn.Location = new System.Drawing.Point(13, 122);
            this.conSn.Name = "conSn";
            this.conSn.Size = new System.Drawing.Size(42, 15);
            this.conSn.TabIndex = 2;
            this.conSn.Text = "ConSn";
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(13, 141);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(539, 240);
            this.output.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 393);
            this.Controls.Add(this.output);
            this.Controls.Add(this.conSn);
            this.Controls.Add(this.comName);
            this.Controls.Add(this.Status);
            this.Name = "Form1";
            this.Text = "Reserialize";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DisposeForm);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Label comName;
        private System.ComponentModel.BackgroundWorker devSearch;
        private System.Windows.Forms.Label conSn;
        private System.Windows.Forms.TextBox output;
    }
}


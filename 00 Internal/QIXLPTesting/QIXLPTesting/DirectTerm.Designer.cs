
namespace QIXLPTesting
{
    partial class DirectTerm
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
            this.termOut = new System.Windows.Forms.RichTextBox();
            this.termIn = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // termOut
            // 
            this.termOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.termOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(47)))), ((int)(((byte)(59)))));
            this.termOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.termOut.ForeColor = System.Drawing.Color.White;
            this.termOut.Location = new System.Drawing.Point(12, 12);
            this.termOut.Name = "termOut";
            this.termOut.Size = new System.Drawing.Size(617, 569);
            this.termOut.TabIndex = 5;
            this.termOut.Text = "";
            // 
            // termIn
            // 
            this.termIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(47)))), ((int)(((byte)(59)))));
            this.termIn.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.termIn.ForeColor = System.Drawing.Color.White;
            this.termIn.Location = new System.Drawing.Point(12, 583);
            this.termIn.Name = "termIn";
            this.termIn.Size = new System.Drawing.Size(617, 22);
            this.termIn.TabIndex = 6;
            this.termIn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendCmd);
            // 
            // DirectTerm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 615);
            this.Controls.Add(this.termIn);
            this.Controls.Add(this.termOut);
            this.Name = "DirectTerm";
            this.Text = "DirectTerm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DisconnectDereference);
            this.Load += new System.EventHandler(this.DirectTerm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox termOut;
        private System.Windows.Forms.TextBox termIn;
    }
}

namespace Pulsewave
{
    partial class Terminal
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
            this.termIn = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // termOut
            // 
            this.termOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.termOut.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.termOut.Location = new System.Drawing.Point(13, 13);
            this.termOut.Name = "termOut";
            this.termOut.ReadOnly = true;
            this.termOut.Size = new System.Drawing.Size(823, 667);
            this.termOut.TabIndex = 0;
            this.termOut.Text = "";
            // 
            // termIn
            // 
            this.termIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.termIn.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.termIn.Location = new System.Drawing.Point(13, 687);
            this.termIn.Name = "termIn";
            this.termIn.Size = new System.Drawing.Size(823, 23);
            this.termIn.TabIndex = 1;
            this.termIn.Text = "";
            this.termIn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendCommand);
            // 
            // Terminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 722);
            this.Controls.Add(this.termIn);
            this.Controls.Add(this.termOut);
            this.Name = "Terminal";
            this.Text = "Terminal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HideForm);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox termOut;
        private System.Windows.Forms.RichTextBox termIn;
    }
}
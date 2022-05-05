
namespace Universal_NPM_Interface.Controls
{
    partial class DirectTerminal
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.termIn = new System.Windows.Forms.TextBox();
            this.termOut = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // termIn
            // 
            this.termIn.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Bold);
            this.termIn.Location = new System.Drawing.Point(0, 552);
            this.termIn.Name = "termIn";
            this.termIn.Size = new System.Drawing.Size(667, 24);
            this.termIn.TabIndex = 5;
            this.termIn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendCommand);
            // 
            // termOut
            // 
            this.termOut.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.termOut.Location = new System.Drawing.Point(0, 0);
            this.termOut.Multiline = true;
            this.termOut.Name = "termOut";
            this.termOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.termOut.Size = new System.Drawing.Size(667, 546);
            this.termOut.TabIndex = 4;
            // 
            // DirectTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.termIn);
            this.Controls.Add(this.termOut);
            this.Name = "DirectTerminal";
            this.Size = new System.Drawing.Size(668, 577);
            this.Load += new System.EventHandler(this.DirectTerminal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox termIn;
        private System.Windows.Forms.TextBox termOut;
    }
}

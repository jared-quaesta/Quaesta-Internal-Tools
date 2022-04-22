
namespace SDI12AddressTool
{
    partial class AlertForm
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
            this.no = new System.Windows.Forms.Button();
            this.yes = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // no
            // 
            this.no.Location = new System.Drawing.Point(12, 50);
            this.no.Name = "no";
            this.no.Size = new System.Drawing.Size(145, 54);
            this.no.TabIndex = 0;
            this.no.Text = "Cancel";
            this.no.UseVisualStyleBackColor = true;
            this.no.Click += new System.EventHandler(this.no_Click);
            // 
            // yes
            // 
            this.yes.Location = new System.Drawing.Point(190, 50);
            this.yes.Name = "yes";
            this.yes.Size = new System.Drawing.Size(145, 54);
            this.yes.TabIndex = 1;
            this.yes.Text = "Thats okay";
            this.yes.UseVisualStyleBackColor = true;
            this.yes.Click += new System.EventHandler(this.yes_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "Warning! There is another NPM with this sdi address. Do you want to continue?";
            // 
            // AlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 116);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.yes);
            this.Controls.Add(this.no);
            this.Name = "AlertForm";
            this.Text = "Alert!";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button no;
        private System.Windows.Forms.Button yes;
        private System.Windows.Forms.Label label1;
    }
}
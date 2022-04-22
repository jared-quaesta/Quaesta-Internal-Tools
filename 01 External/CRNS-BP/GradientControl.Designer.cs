/// GradientControl Labels


namespace CRNS_BP
{
    partial class GradientControl
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
            this.bt1 = new System.Windows.Forms.Button();
            this.bt2 = new System.Windows.Forms.Button();
            this.bt3 = new System.Windows.Forms.Button();
            this.gradientPicture = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.bt4 = new System.Windows.Forms.Button();
            this.lb1 = new System.Windows.Forms.Label();
            this.lb3 = new System.Windows.Forms.Label();
            this.lb2 = new System.Windows.Forms.Label();
            this.lb4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // bt1
            // 
            this.bt1.Location = new System.Drawing.Point(0, 130);
            this.bt1.Name = "bt1";
            this.bt1.Size = new System.Drawing.Size(32, 12);
            this.bt1.TabIndex = 40;
            this.bt1.UseVisualStyleBackColor = true;
            this.bt1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectColor);
            this.bt1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveBtn);
            // 
            // bt2
            // 
            this.bt2.Location = new System.Drawing.Point(0, 204);
            this.bt2.Name = "bt2";
            this.bt2.Size = new System.Drawing.Size(32, 12);
            this.bt2.TabIndex = 41;
            this.bt2.UseVisualStyleBackColor = true;
            this.bt2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectColor);
            this.bt2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveBtn);
            // 
            // bt3
            // 
            this.bt3.Location = new System.Drawing.Point(0, 294);
            this.bt3.Name = "bt3";
            this.bt3.Size = new System.Drawing.Size(32, 12);
            this.bt3.TabIndex = 42;
            this.bt3.UseVisualStyleBackColor = true;
            this.bt3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectColor);
            this.bt3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveBtn);
            // 
            // gradientPicture
            // 
            this.gradientPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientPicture.Dock = System.Windows.Forms.DockStyle.Left;
            this.gradientPicture.Location = new System.Drawing.Point(0, 0);
            this.gradientPicture.Name = "gradientPicture";
            this.gradientPicture.Size = new System.Drawing.Size(43, 463);
            this.gradientPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.gradientPicture.TabIndex = 37;
            this.gradientPicture.TabStop = false;
            // 
            // bt4
            // 
            this.bt4.Location = new System.Drawing.Point(0, 334);
            this.bt4.Name = "bt4";
            this.bt4.Size = new System.Drawing.Size(32, 12);
            this.bt4.TabIndex = 43;
            this.bt4.UseVisualStyleBackColor = true;
            this.bt4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectColor);
            this.bt4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveBtn);
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Location = new System.Drawing.Point(45, 11);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(35, 13);
            this.lb1.TabIndex = 44;
            this.lb1.Text = "label1";
            // 
            // lb3
            // 
            this.lb3.AutoSize = true;
            this.lb3.Location = new System.Drawing.Point(45, 58);
            this.lb3.Name = "lb3";
            this.lb3.Size = new System.Drawing.Size(35, 13);
            this.lb3.TabIndex = 45;
            this.lb3.Text = "label1";
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.Location = new System.Drawing.Point(45, 34);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(35, 13);
            this.lb2.TabIndex = 46;
            this.lb2.Text = "label1";
            // 
            // lb4
            // 
            this.lb4.AutoSize = true;
            this.lb4.Location = new System.Drawing.Point(45, 81);
            this.lb4.Name = "lb4";
            this.lb4.Size = new System.Drawing.Size(35, 13);
            this.lb4.TabIndex = 47;
            this.lb4.Text = "label1";
            // 
            // GradientControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lb4);
            this.Controls.Add(this.lb2);
            this.Controls.Add(this.lb3);
            this.Controls.Add(this.lb1);
            this.Controls.Add(this.bt3);
            this.Controls.Add(this.bt2);
            this.Controls.Add(this.bt1);
            this.Controls.Add(this.bt4);
            this.Controls.Add(this.gradientPicture);
            this.Name = "GradientControl";
            this.Size = new System.Drawing.Size(83, 463);
            this.Load += new System.EventHandler(this.GradientControl_Load);
            this.Resize += new System.EventHandler(this.ResizeFixButtonPositions);
            ((System.ComponentModel.ISupportInitialize)(this.gradientPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox gradientPicture;
        private System.Windows.Forms.Button bt1;
        private System.Windows.Forms.Button bt2;
        private System.Windows.Forms.Button bt3;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button bt4;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Label lb3;
        private System.Windows.Forms.Label lb2;
        private System.Windows.Forms.Label lb4;
    }
}

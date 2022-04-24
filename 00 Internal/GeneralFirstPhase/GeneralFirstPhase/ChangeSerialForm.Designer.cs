
namespace QIXLPTesting
{
    partial class ChangeSerialForm
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
            this.changeBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.oldBox = new System.Windows.Forms.TextBox();
            this.newBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // changeBtn
            // 
            this.changeBtn.Location = new System.Drawing.Point(220, 38);
            this.changeBtn.Name = "changeBtn";
            this.changeBtn.Size = new System.Drawing.Size(130, 23);
            this.changeBtn.TabIndex = 0;
            this.changeBtn.Text = "Change Serial Number";
            this.changeBtn.UseVisualStyleBackColor = true;
            this.changeBtn.Click += new System.EventHandler(this.changeBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(12, 38);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(202, 23);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Change";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "to";
            // 
            // oldBox
            // 
            this.oldBox.Enabled = false;
            this.oldBox.Location = new System.Drawing.Point(62, 10);
            this.oldBox.Name = "oldBox";
            this.oldBox.Size = new System.Drawing.Size(130, 20);
            this.oldBox.TabIndex = 5;
            // 
            // newBox
            // 
            this.newBox.Location = new System.Drawing.Point(220, 10);
            this.newBox.Name = "newBox";
            this.newBox.Size = new System.Drawing.Size(130, 20);
            this.newBox.TabIndex = 6;
            // 
            // ChangeSerialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 67);
            this.Controls.Add(this.newBox);
            this.Controls.Add(this.oldBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.changeBtn);
            this.Name = "ChangeSerialForm";
            this.Text = "ChangeSerialForm";
            this.Load += new System.EventHandler(this.ChangeSerialForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button changeBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox oldBox;
        private System.Windows.Forms.TextBox newBox;
    }
}
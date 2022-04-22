
namespace QIY_Torture_Test
{
    partial class NewIPForm
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
            this.addBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.ipOne = new System.Windows.Forms.TextBox();
            this.ipFour = new System.Windows.Forms.TextBox();
            this.ipThree = new System.Windows.Forms.TextBox();
            this.ipTwo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // addBtn
            // 
            this.addBtn.ForeColor = System.Drawing.Color.Green;
            this.addBtn.Location = new System.Drawing.Point(120, 61);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(105, 35);
            this.addBtn.TabIndex = 0;
            this.addBtn.Text = "Add New IP";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(0, 61);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(114, 35);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // ipOne
            // 
            this.ipOne.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipOne.Location = new System.Drawing.Point(12, 35);
            this.ipOne.Name = "ipOne";
            this.ipOne.Size = new System.Drawing.Size(46, 20);
            this.ipOne.TabIndex = 3;
            this.ipOne.Text = "192";
            // 
            // ipFour
            // 
            this.ipFour.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.ipFour.Location = new System.Drawing.Point(168, 35);
            this.ipFour.Name = "ipFour";
            this.ipFour.Size = new System.Drawing.Size(46, 20);
            this.ipFour.TabIndex = 4;
            // 
            // ipThree
            // 
            this.ipThree.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.ipThree.Location = new System.Drawing.Point(116, 35);
            this.ipThree.Name = "ipThree";
            this.ipThree.Size = new System.Drawing.Size(46, 20);
            this.ipThree.TabIndex = 5;
            this.ipThree.Text = "15";
            // 
            // ipTwo
            // 
            this.ipTwo.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.ipTwo.Location = new System.Drawing.Point(64, 35);
            this.ipTwo.Name = "ipTwo";
            this.ipTwo.Size = new System.Drawing.Size(46, 20);
            this.ipTwo.TabIndex = 6;
            this.ipTwo.Text = "168";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Try to connect to non-discovered IP:";
            // 
            // NewIPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 100);
            this.Controls.Add(this.ipOne);
            this.Controls.Add(this.ipTwo);
            this.Controls.Add(this.ipThree);
            this.Controls.Add(this.ipFour);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.addBtn);
            this.Name = "NewIPForm";
            this.Text = "New IP";
            this.Load += new System.EventHandler(this.NewIPForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.TextBox ipOne;
        private System.Windows.Forms.TextBox ipFour;
        private System.Windows.Forms.TextBox ipThree;
        private System.Windows.Forms.TextBox ipTwo;
        private System.Windows.Forms.Label label1;
    }
}
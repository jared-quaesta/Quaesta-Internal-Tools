
namespace QIY_Interface__IAEA_
{
    partial class IpListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IpListForm));
            this.ip0 = new System.Windows.Forms.TextBox();
            this.ip1 = new System.Windows.Forms.TextBox();
            this.ip2 = new System.Windows.Forms.TextBox();
            this.ip3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.addbtn = new System.Windows.Forms.Button();
            this.moveUp = new System.Windows.Forms.Button();
            this.moveDown = new System.Windows.Forms.Button();
            this.rembtn = new System.Windows.Forms.Button();
            this.discover = new System.Windows.Forms.Button();
            this.apply = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.readConfig = new System.Windows.Forms.Button();
            this.saveConfig = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ip0
            // 
            this.ip0.Font = new System.Drawing.Font("Courier New", 9F);
            this.ip0.Location = new System.Drawing.Point(15, 23);
            this.ip0.Name = "ip0";
            this.ip0.Size = new System.Drawing.Size(35, 21);
            this.ip0.TabIndex = 0;
            this.ip0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ip0.TextChanged += new System.EventHandler(this.NextBox);
            // 
            // ip1
            // 
            this.ip1.Font = new System.Drawing.Font("Courier New", 9F);
            this.ip1.Location = new System.Drawing.Point(54, 23);
            this.ip1.Name = "ip1";
            this.ip1.Size = new System.Drawing.Size(35, 21);
            this.ip1.TabIndex = 1;
            this.ip1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ip1.TextChanged += new System.EventHandler(this.NextBox);
            this.ip1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrevBox);
            // 
            // ip2
            // 
            this.ip2.Font = new System.Drawing.Font("Courier New", 9F);
            this.ip2.Location = new System.Drawing.Point(93, 23);
            this.ip2.Name = "ip2";
            this.ip2.Size = new System.Drawing.Size(35, 21);
            this.ip2.TabIndex = 2;
            this.ip2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ip2.TextChanged += new System.EventHandler(this.NextBox);
            this.ip2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrevBox);
            // 
            // ip3
            // 
            this.ip3.Font = new System.Drawing.Font("Courier New", 9F);
            this.ip3.Location = new System.Drawing.Point(133, 23);
            this.ip3.Name = "ip3";
            this.ip3.Size = new System.Drawing.Size(35, 21);
            this.ip3.TabIndex = 3;
            this.ip3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ip3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrevBox);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = ".";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = ".";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = ".";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "IP Address:";
            // 
            // addbtn
            // 
            this.addbtn.Location = new System.Drawing.Point(172, 22);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(83, 20);
            this.addbtn.TabIndex = 9;
            this.addbtn.Text = "Add to List";
            this.addbtn.UseVisualStyleBackColor = true;
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // moveUp
            // 
            this.moveUp.Location = new System.Drawing.Point(172, 47);
            this.moveUp.Name = "moveUp";
            this.moveUp.Size = new System.Drawing.Size(83, 20);
            this.moveUp.TabIndex = 10;
            this.moveUp.Text = "Move Up";
            this.moveUp.UseVisualStyleBackColor = true;
            this.moveUp.Click += new System.EventHandler(this.moveUp_Click);
            // 
            // moveDown
            // 
            this.moveDown.Location = new System.Drawing.Point(172, 72);
            this.moveDown.Name = "moveDown";
            this.moveDown.Size = new System.Drawing.Size(83, 20);
            this.moveDown.TabIndex = 11;
            this.moveDown.Text = "Move Down";
            this.moveDown.UseVisualStyleBackColor = true;
            this.moveDown.Click += new System.EventHandler(this.moveDown_Click);
            // 
            // rembtn
            // 
            this.rembtn.Location = new System.Drawing.Point(172, 97);
            this.rembtn.Name = "rembtn";
            this.rembtn.Size = new System.Drawing.Size(83, 20);
            this.rembtn.TabIndex = 12;
            this.rembtn.Text = "Delete";
            this.rembtn.UseVisualStyleBackColor = true;
            this.rembtn.Click += new System.EventHandler(this.rembtn_Click);
            // 
            // discover
            // 
            this.discover.Location = new System.Drawing.Point(172, 122);
            this.discover.Name = "discover";
            this.discover.Size = new System.Drawing.Size(83, 20);
            this.discover.TabIndex = 13;
            this.discover.Text = "Discover";
            this.discover.UseVisualStyleBackColor = true;
            this.discover.Click += new System.EventHandler(this.discover_Click);
            // 
            // apply
            // 
            this.apply.Location = new System.Drawing.Point(172, 302);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(83, 49);
            this.apply.TabIndex = 14;
            this.apply.Text = "Apply and Save";
            this.apply.UseVisualStyleBackColor = true;
            this.apply.Click += new System.EventHandler(this.apply_Click);
            // 
            // listBox
            // 
            this.listBox.Font = new System.Drawing.Font("Courier New", 9F);
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 15;
            this.listBox.Location = new System.Drawing.Point(15, 47);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(153, 304);
            this.listBox.TabIndex = 15;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = global::QIY_Interface__IAEA_.Properties.Resources.QI_justQ;
            this.pictureBox1.Location = new System.Drawing.Point(172, 217);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(84, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // readConfig
            // 
            this.readConfig.Location = new System.Drawing.Point(172, 159);
            this.readConfig.Name = "readConfig";
            this.readConfig.Size = new System.Drawing.Size(83, 24);
            this.readConfig.TabIndex = 17;
            this.readConfig.Text = "Read Config";
            this.readConfig.UseVisualStyleBackColor = true;
            this.readConfig.Click += new System.EventHandler(this.readConfig_Click);
            // 
            // saveConfig
            // 
            this.saveConfig.Location = new System.Drawing.Point(172, 189);
            this.saveConfig.Name = "saveConfig";
            this.saveConfig.Size = new System.Drawing.Size(83, 24);
            this.saveConfig.TabIndex = 18;
            this.saveConfig.Text = "Save Config";
            this.saveConfig.UseVisualStyleBackColor = true;
            this.saveConfig.Click += new System.EventHandler(this.saveConfig_Click);
            // 
            // IpListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 356);
            this.Controls.Add(this.saveConfig);
            this.Controls.Add(this.readConfig);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ip0);
            this.Controls.Add(this.ip1);
            this.Controls.Add(this.ip2);
            this.Controls.Add(this.ip3);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.apply);
            this.Controls.Add(this.discover);
            this.Controls.Add(this.rembtn);
            this.Controls.Add(this.moveDown);
            this.Controls.Add(this.moveUp);
            this.Controls.Add(this.addbtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IpListForm";
            this.Text = "Ip Address List";
            this.Load += new System.EventHandler(this.IpListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ip0;
        private System.Windows.Forms.TextBox ip1;
        private System.Windows.Forms.TextBox ip2;
        private System.Windows.Forms.TextBox ip3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        internal System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button addbtn;
        private System.Windows.Forms.Button moveUp;
        private System.Windows.Forms.Button moveDown;
        private System.Windows.Forms.Button rembtn;
        private System.Windows.Forms.Button discover;
        private System.Windows.Forms.Button apply;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button readConfig;
        private System.Windows.Forms.Button saveConfig;
    }
}
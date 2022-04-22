
namespace QIY_Interface__IAEA_
{
    partial class TransferFileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferFileForm));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.destPathBox = new System.Windows.Forms.TextBox();
            this.fileProgressLbl = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.npmFilenameBox = new System.Windows.Forms.TextBox();
            this.pcFilenameBox = new System.Windows.Forms.TextBox();
            this.destSelectBtn = new System.Windows.Forms.Button();
            this.bytesRecievedLbl = new System.Windows.Forms.Label();
            this.bytesTransferredLbl = new System.Windows.Forms.Label();
            this.packetsTransferredLbl = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.completeLbl = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "NPM Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP Address";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(70, 30);
            this.nameBox.Name = "nameBox";
            this.nameBox.ReadOnly = true;
            this.nameBox.Size = new System.Drawing.Size(145, 20);
            this.nameBox.TabIndex = 5;
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(70, 10);
            this.ipBox.Name = "ipBox";
            this.ipBox.ReadOnly = true;
            this.ipBox.Size = new System.Drawing.Size(145, 20);
            this.ipBox.TabIndex = 4;
            // 
            // startBtn
            // 
            this.startBtn.ForeColor = System.Drawing.Color.Green;
            this.startBtn.Location = new System.Drawing.Point(419, 10);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(94, 40);
            this.startBtn.TabIndex = 9;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_ClickAsync);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Enabled = false;
            this.cancelBtn.ForeColor = System.Drawing.Color.Red;
            this.cancelBtn.Location = new System.Drawing.Point(519, 10);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(85, 40);
            this.cancelBtn.TabIndex = 10;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // destPathBox
            // 
            this.destPathBox.Location = new System.Drawing.Point(85, 64);
            this.destPathBox.Name = "destPathBox";
            this.destPathBox.Size = new System.Drawing.Size(489, 20);
            this.destPathBox.TabIndex = 11;
            // 
            // fileProgressLbl
            // 
            this.fileProgressLbl.AutoSize = true;
            this.fileProgressLbl.Location = new System.Drawing.Point(6, 154);
            this.fileProgressLbl.Name = "fileProgressLbl";
            this.fileProgressLbl.Size = new System.Drawing.Size(96, 13);
            this.fileProgressLbl.TabIndex = 13;
            this.fileProgressLbl.Text = "Ready To Transfer";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(6, 55);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(598, 2);
            this.textBox2.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Destination";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "NPM Filename";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "PC Filename";
            // 
            // npmFilenameBox
            // 
            this.npmFilenameBox.Location = new System.Drawing.Point(85, 95);
            this.npmFilenameBox.Name = "npmFilenameBox";
            this.npmFilenameBox.Size = new System.Drawing.Size(520, 20);
            this.npmFilenameBox.TabIndex = 18;
            // 
            // pcFilenameBox
            // 
            this.pcFilenameBox.Location = new System.Drawing.Point(85, 123);
            this.pcFilenameBox.Name = "pcFilenameBox";
            this.pcFilenameBox.Size = new System.Drawing.Size(519, 20);
            this.pcFilenameBox.TabIndex = 19;
            // 
            // destSelectBtn
            // 
            this.destSelectBtn.Location = new System.Drawing.Point(580, 64);
            this.destSelectBtn.Name = "destSelectBtn";
            this.destSelectBtn.Size = new System.Drawing.Size(24, 21);
            this.destSelectBtn.TabIndex = 20;
            this.destSelectBtn.Text = "...";
            this.destSelectBtn.UseVisualStyleBackColor = true;
            this.destSelectBtn.Click += new System.EventHandler(this.destSelectBtn_Click);
            // 
            // bytesRecievedLbl
            // 
            this.bytesRecievedLbl.AutoSize = true;
            this.bytesRecievedLbl.Location = new System.Drawing.Point(6, 180);
            this.bytesRecievedLbl.Name = "bytesRecievedLbl";
            this.bytesRecievedLbl.Size = new System.Drawing.Size(85, 13);
            this.bytesRecievedLbl.TabIndex = 23;
            this.bytesRecievedLbl.Text = "Bytes Received:";
            // 
            // bytesTransferredLbl
            // 
            this.bytesTransferredLbl.AutoSize = true;
            this.bytesTransferredLbl.Location = new System.Drawing.Point(6, 199);
            this.bytesTransferredLbl.Name = "bytesTransferredLbl";
            this.bytesTransferredLbl.Size = new System.Drawing.Size(93, 13);
            this.bytesTransferredLbl.TabIndex = 24;
            this.bytesTransferredLbl.Text = "Bytes Transferred:";
            // 
            // packetsTransferredLbl
            // 
            this.packetsTransferredLbl.AutoSize = true;
            this.packetsTransferredLbl.Location = new System.Drawing.Point(6, 218);
            this.packetsTransferredLbl.Name = "packetsTransferredLbl";
            this.packetsTransferredLbl.Size = new System.Drawing.Size(106, 13);
            this.packetsTransferredLbl.TabIndex = 25;
            this.packetsTransferredLbl.Text = "Packets Transferred:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(6, 237);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(598, 20);
            this.progressBar.TabIndex = 26;
            // 
            // completeLbl
            // 
            this.completeLbl.AutoSize = true;
            this.completeLbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.completeLbl.ForeColor = System.Drawing.Color.Green;
            this.completeLbl.Location = new System.Drawing.Point(244, 10);
            this.completeLbl.Name = "completeLbl";
            this.completeLbl.Size = new System.Drawing.Size(149, 21);
            this.completeLbl.TabIndex = 27;
            this.completeLbl.Text = "Transfer Complete";
            this.completeLbl.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = global::QIY_Interface__IAEA_.Properties.Resources.QuaestaLogo;
            this.pictureBox1.Location = new System.Drawing.Point(454, 177);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // TransferFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 269);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.completeLbl);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.packetsTransferredLbl);
            this.Controls.Add(this.bytesTransferredLbl);
            this.Controls.Add(this.bytesRecievedLbl);
            this.Controls.Add(this.destSelectBtn);
            this.Controls.Add(this.pcFilenameBox);
            this.Controls.Add(this.npmFilenameBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.fileProgressLbl);
            this.Controls.Add(this.destPathBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.ipBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TransferFileForm";
            this.Text = "File Transfer";
            this.Load += new System.EventHandler(this.TransferFileForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.TextBox destPathBox;
        private System.Windows.Forms.Label fileProgressLbl;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox npmFilenameBox;
        private System.Windows.Forms.TextBox pcFilenameBox;
        private System.Windows.Forms.Button destSelectBtn;
        private System.Windows.Forms.Label bytesRecievedLbl;
        private System.Windows.Forms.Label bytesTransferredLbl;
        private System.Windows.Forms.Label packetsTransferredLbl;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label completeLbl;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

namespace SDI12AddressTool
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.npmSnBox = new System.Windows.Forms.TextBox();
            this.noteBox = new System.Windows.Forms.TextBox();
            this.tubeOwnerBox = new System.Windows.Forms.TextBox();
            this.tubeSnBox = new System.Windows.Forms.TextBox();
            this.sdiAddressBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.listPanel = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.queryNPMBtn = new System.Windows.Forms.Button();
            this.applySettings = new System.Windows.Forms.Button();
            this.applyAllButton = new System.Windows.Forms.Button();
            this.tubeTypeBox = new System.Windows.Forms.ComboBox();
            this.listenWorker = new System.ComponentModel.BackgroundWorker();
            this.fwBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "SN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tube Owner";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tube SN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(129, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "SDI Address";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(129, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Tube Type";
            // 
            // npmSnBox
            // 
            this.npmSnBox.Location = new System.Drawing.Point(213, 12);
            this.npmSnBox.Name = "npmSnBox";
            this.npmSnBox.Size = new System.Drawing.Size(115, 23);
            this.npmSnBox.TabIndex = 6;
            // 
            // noteBox
            // 
            this.noteBox.Location = new System.Drawing.Point(334, 25);
            this.noteBox.Multiline = true;
            this.noteBox.Name = "noteBox";
            this.noteBox.Size = new System.Drawing.Size(185, 70);
            this.noteBox.TabIndex = 7;
            // 
            // tubeOwnerBox
            // 
            this.tubeOwnerBox.Location = new System.Drawing.Point(213, 101);
            this.tubeOwnerBox.Name = "tubeOwnerBox";
            this.tubeOwnerBox.Size = new System.Drawing.Size(115, 23);
            this.tubeOwnerBox.TabIndex = 8;
            // 
            // tubeSnBox
            // 
            this.tubeSnBox.Location = new System.Drawing.Point(213, 72);
            this.tubeSnBox.Name = "tubeSnBox";
            this.tubeSnBox.Size = new System.Drawing.Size(115, 23);
            this.tubeSnBox.TabIndex = 9;
            // 
            // sdiAddressBox
            // 
            this.sdiAddressBox.Location = new System.Drawing.Point(213, 43);
            this.sdiAddressBox.Name = "sdiAddressBox";
            this.sdiAddressBox.Size = new System.Drawing.Size(115, 23);
            this.sdiAddressBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(334, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "Note (Optional)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(228, 170);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 15);
            this.label10.TabIndex = 5;
            this.label10.Text = "TUBE TYPE";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(147, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "TUBE SN";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(67, 170);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "NPM SN";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "SDI";
            // 
            // listPanel
            // 
            this.listPanel.AutoScroll = true;
            this.listPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listPanel.Location = new System.Drawing.Point(13, 188);
            this.listPanel.Name = "listPanel";
            this.listPanel.Size = new System.Drawing.Size(656, 527);
            this.listPanel.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(424, 170);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 15);
            this.label12.TabIndex = 7;
            this.label12.Text = "FW VERSION";
            // 
            // queryNPMBtn
            // 
            this.queryNPMBtn.Location = new System.Drawing.Point(12, 12);
            this.queryNPMBtn.Name = "queryNPMBtn";
            this.queryNPMBtn.Size = new System.Drawing.Size(107, 141);
            this.queryNPMBtn.TabIndex = 15;
            this.queryNPMBtn.Text = "Query";
            this.queryNPMBtn.UseVisualStyleBackColor = true;
            this.queryNPMBtn.Click += new System.EventHandler(this.queryNPMBtn_Click);
            // 
            // applySettings
            // 
            this.applySettings.ForeColor = System.Drawing.Color.Green;
            this.applySettings.Location = new System.Drawing.Point(529, 12);
            this.applySettings.Name = "applySettings";
            this.applySettings.Size = new System.Drawing.Size(140, 141);
            this.applySettings.TabIndex = 16;
            this.applySettings.Text = "Apply new NPM settings";
            this.applySettings.UseVisualStyleBackColor = true;
            this.applySettings.Click += new System.EventHandler(this.applySettings_Click);
            // 
            // applyAllButton
            // 
            this.applyAllButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.applyAllButton.ForeColor = System.Drawing.Color.Green;
            this.applyAllButton.Location = new System.Drawing.Point(12, 721);
            this.applyAllButton.Name = "applyAllButton";
            this.applyAllButton.Size = new System.Drawing.Size(658, 29);
            this.applyAllButton.TabIndex = 17;
            this.applyAllButton.Text = "Apply Session Data to Server";
            this.applyAllButton.UseVisualStyleBackColor = true;
            this.applyAllButton.Click += new System.EventHandler(this.applyAllButton_Click);
            // 
            // tubeTypeBox
            // 
            this.tubeTypeBox.FormattingEnabled = true;
            this.tubeTypeBox.Items.AddRange(new object[] {
            "GN:3\" ODx33.5\" BF3 ; GN82EB45-76HS",
            "GN:2\" ODx24\" He3 (1.5atm)",
            "LND:2\" ODx33.5\" BF3 ; LND20361",
            "95He3-114-76HS",
            "None"});
            this.tubeTypeBox.Location = new System.Drawing.Point(213, 130);
            this.tubeTypeBox.Name = "tubeTypeBox";
            this.tubeTypeBox.Size = new System.Drawing.Size(306, 23);
            this.tubeTypeBox.TabIndex = 18;
            // 
            // listenWorker
            // 
            this.listenWorker.WorkerReportsProgress = true;
            this.listenWorker.WorkerSupportsCancellation = true;
            this.listenWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ListenForChanges);
            this.listenWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.UpdateView);
            // 
            // fwBox
            // 
            this.fwBox.Location = new System.Drawing.Point(404, 101);
            this.fwBox.Name = "fwBox";
            this.fwBox.Size = new System.Drawing.Size(115, 23);
            this.fwBox.TabIndex = 20;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(342, 104);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 15);
            this.label19.TabIndex = 19;
            this.label19.Text = "Firmware";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 753);
            this.Controls.Add(this.fwBox);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.tubeTypeBox);
            this.Controls.Add(this.applyAllButton);
            this.Controls.Add(this.applySettings);
            this.Controls.Add(this.queryNPMBtn);
            this.Controls.Add(this.listPanel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.sdiAddressBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tubeSnBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tubeOwnerBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.noteBox);
            this.Controls.Add(this.npmSnBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "SDI Addressing Tool ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label conStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox npmSnBox;
        private System.Windows.Forms.TextBox noteBox;
        private System.Windows.Forms.TextBox tubeOwnerBox;
        private System.Windows.Forms.TextBox tubeSnBox;
        private System.Windows.Forms.TextBox sdiAddressBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel listPanel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button queryNPMBtn;
        private System.Windows.Forms.Button applySettings;
        private System.Windows.Forms.Button applyAllButton;
        private System.Windows.Forms.ComboBox tubeTypeBox;
        private System.ComponentModel.BackgroundWorker listenWorker;
        private System.Windows.Forms.TextBox fwBox;
        private System.Windows.Forms.Label label19;
    }
}



namespace QIY_Interface__IAEA_
{
    partial class DataRetrievalForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataRetrievalForm));
            this.ipBox = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DAT = new System.Windows.Forms.RadioButton();
            this.BIN = new System.Windows.Forms.RadioButton();
            this.HGM = new System.Windows.Forms.RadioButton();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.internalInfoBox = new System.Windows.Forms.RichTextBox();
            this.externalInfoBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.internalDirBox = new System.Windows.Forms.CheckedListBox();
            this.externalDirBox = new System.Windows.Forms.CheckedListBox();
            this.internalSelectedFilesBox = new System.Windows.Forms.TextBox();
            this.internalTotalFilesBox = new System.Windows.Forms.TextBox();
            this.internalBytesSelectedBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.externalBytesSelectedBox = new System.Windows.Forms.TextBox();
            this.externalTotalFilesBox = new System.Windows.Forms.TextBox();
            this.externalSelectedFilesBox = new System.Windows.Forms.TextBox();
            this.internalGB = new System.Windows.Forms.GroupBox();
            this.clearSel = new System.Windows.Forms.Button();
            this.retFiles = new System.Windows.Forms.Button();
            this.clearInternalArchive = new System.Windows.Forms.CheckBox();
            this.selInt = new System.Windows.Forms.RadioButton();
            this.allInt = new System.Windows.Forms.RadioButton();
            this.nonArchiveBtnIn = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.clearExternalArchive = new System.Windows.Forms.CheckBox();
            this.selExt = new System.Windows.Forms.RadioButton();
            this.allExt = new System.Windows.Forms.RadioButton();
            this.nonArchiveBtnEx = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.internalGB.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(69, 10);
            this.ipBox.Name = "ipBox";
            this.ipBox.ReadOnly = true;
            this.ipBox.Size = new System.Drawing.Size(121, 20);
            this.ipBox.TabIndex = 0;
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(274, 10);
            this.nameBox.Name = "nameBox";
            this.nameBox.ReadOnly = true;
            this.nameBox.Size = new System.Drawing.Size(128, 20);
            this.nameBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "NPM Name";
            // 
            // DAT
            // 
            this.DAT.AutoSize = true;
            this.DAT.Location = new System.Drawing.Point(10, 36);
            this.DAT.Name = "DAT";
            this.DAT.Size = new System.Drawing.Size(122, 17);
            this.DAT.TabIndex = 4;
            this.DAT.TabStop = true;
            this.DAT.Text = "Show DAT Directory";
            this.DAT.UseVisualStyleBackColor = true;
            this.DAT.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // BIN
            // 
            this.BIN.AutoSize = true;
            this.BIN.Location = new System.Drawing.Point(147, 36);
            this.BIN.Name = "BIN";
            this.BIN.Size = new System.Drawing.Size(118, 17);
            this.BIN.TabIndex = 5;
            this.BIN.TabStop = true;
            this.BIN.Text = "Show BIN Directory";
            this.BIN.UseVisualStyleBackColor = true;
            this.BIN.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // HGM
            // 
            this.HGM.AutoSize = true;
            this.HGM.Location = new System.Drawing.Point(274, 36);
            this.HGM.Name = "HGM";
            this.HGM.Size = new System.Drawing.Size(125, 17);
            this.HGM.TabIndex = 6;
            this.HGM.TabStop = true;
            this.HGM.Text = "Show HGM Directory";
            this.HGM.UseVisualStyleBackColor = true;
            this.HGM.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // refreshBtn
            // 
            this.refreshBtn.Location = new System.Drawing.Point(423, 10);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(195, 42);
            this.refreshBtn.TabIndex = 7;
            this.refreshBtn.Text = "Refresh";
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(11, 54);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(607, 2);
            this.textBox3.TabIndex = 8;
            // 
            // internalInfoBox
            // 
            this.internalInfoBox.Location = new System.Drawing.Point(10, 84);
            this.internalInfoBox.Name = "internalInfoBox";
            this.internalInfoBox.Size = new System.Drawing.Size(388, 64);
            this.internalInfoBox.TabIndex = 9;
            this.internalInfoBox.Text = "";
            // 
            // externalInfoBox
            // 
            this.externalInfoBox.Location = new System.Drawing.Point(423, 84);
            this.externalInfoBox.Name = "externalInfoBox";
            this.externalInfoBox.Size = new System.Drawing.Size(388, 64);
            this.externalInfoBox.TabIndex = 10;
            this.externalInfoBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "SD Card 0 (Internal SD Card)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(423, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "SD Card 1 (External SD Card)";
            // 
            // internalDirBox
            // 
            this.internalDirBox.CheckOnClick = true;
            this.internalDirBox.Font = new System.Drawing.Font("Lucida Console", 9F);
            this.internalDirBox.FormattingEnabled = true;
            this.internalDirBox.HorizontalScrollbar = true;
            this.internalDirBox.Location = new System.Drawing.Point(10, 153);
            this.internalDirBox.Name = "internalDirBox";
            this.internalDirBox.Size = new System.Drawing.Size(388, 284);
            this.internalDirBox.TabIndex = 13;
            this.internalDirBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ValidateSelection);
            this.internalDirBox.SelectedIndexChanged += new System.EventHandler(this.SelectNone);
            this.internalDirBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragSelect);
            this.internalDirBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UpdateInternalDetails);
            // 
            // externalDirBox
            // 
            this.externalDirBox.CheckOnClick = true;
            this.externalDirBox.Font = new System.Drawing.Font("Lucida Console", 9F);
            this.externalDirBox.FormattingEnabled = true;
            this.externalDirBox.HorizontalScrollbar = true;
            this.externalDirBox.Location = new System.Drawing.Point(423, 153);
            this.externalDirBox.Name = "externalDirBox";
            this.externalDirBox.Size = new System.Drawing.Size(388, 284);
            this.externalDirBox.TabIndex = 14;
            this.externalDirBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ValidateSelection);
            this.externalDirBox.SelectedIndexChanged += new System.EventHandler(this.SelectNone);
            this.externalDirBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragSelect);
            this.externalDirBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UpdateExternalDetails);
            // 
            // internalSelectedFilesBox
            // 
            this.internalSelectedFilesBox.Location = new System.Drawing.Point(10, 458);
            this.internalSelectedFilesBox.Name = "internalSelectedFilesBox";
            this.internalSelectedFilesBox.ReadOnly = true;
            this.internalSelectedFilesBox.Size = new System.Drawing.Size(48, 20);
            this.internalSelectedFilesBox.TabIndex = 15;
            // 
            // internalTotalFilesBox
            // 
            this.internalTotalFilesBox.Location = new System.Drawing.Point(76, 458);
            this.internalTotalFilesBox.Name = "internalTotalFilesBox";
            this.internalTotalFilesBox.ReadOnly = true;
            this.internalTotalFilesBox.Size = new System.Drawing.Size(48, 20);
            this.internalTotalFilesBox.TabIndex = 16;
            // 
            // internalBytesSelectedBox
            // 
            this.internalBytesSelectedBox.Location = new System.Drawing.Point(167, 458);
            this.internalBytesSelectedBox.Name = "internalBytesSelectedBox";
            this.internalBytesSelectedBox.ReadOnly = true;
            this.internalBytesSelectedBox.Size = new System.Drawing.Size(85, 20);
            this.internalBytesSelectedBox.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 465);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "of";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(127, 465);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Files";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(252, 465);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "B Selected";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(664, 465);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "B Selected";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(539, 465);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Files";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(471, 465);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "of";
            // 
            // externalBytesSelectedBox
            // 
            this.externalBytesSelectedBox.Location = new System.Drawing.Point(579, 458);
            this.externalBytesSelectedBox.Name = "externalBytesSelectedBox";
            this.externalBytesSelectedBox.ReadOnly = true;
            this.externalBytesSelectedBox.Size = new System.Drawing.Size(85, 20);
            this.externalBytesSelectedBox.TabIndex = 23;
            // 
            // externalTotalFilesBox
            // 
            this.externalTotalFilesBox.Location = new System.Drawing.Point(489, 458);
            this.externalTotalFilesBox.Name = "externalTotalFilesBox";
            this.externalTotalFilesBox.ReadOnly = true;
            this.externalTotalFilesBox.Size = new System.Drawing.Size(48, 20);
            this.externalTotalFilesBox.TabIndex = 22;
            // 
            // externalSelectedFilesBox
            // 
            this.externalSelectedFilesBox.Location = new System.Drawing.Point(423, 458);
            this.externalSelectedFilesBox.Name = "externalSelectedFilesBox";
            this.externalSelectedFilesBox.ReadOnly = true;
            this.externalSelectedFilesBox.Size = new System.Drawing.Size(48, 20);
            this.externalSelectedFilesBox.TabIndex = 21;
            // 
            // internalGB
            // 
            this.internalGB.Controls.Add(this.clearSel);
            this.internalGB.Controls.Add(this.retFiles);
            this.internalGB.Controls.Add(this.clearInternalArchive);
            this.internalGB.Controls.Add(this.selInt);
            this.internalGB.Controls.Add(this.allInt);
            this.internalGB.Controls.Add(this.nonArchiveBtnIn);
            this.internalGB.Location = new System.Drawing.Point(10, 484);
            this.internalGB.Name = "internalGB";
            this.internalGB.Size = new System.Drawing.Size(387, 81);
            this.internalGB.TabIndex = 27;
            this.internalGB.TabStop = false;
            this.internalGB.Text = "Transfer Options";
            // 
            // clearSel
            // 
            this.clearSel.Location = new System.Drawing.Point(5, 16);
            this.clearSel.Name = "clearSel";
            this.clearSel.Size = new System.Drawing.Size(115, 38);
            this.clearSel.TabIndex = 5;
            this.clearSel.Text = "Clear Selection";
            this.clearSel.UseVisualStyleBackColor = true;
            this.clearSel.Click += new System.EventHandler(this.clearSel_Click);
            // 
            // retFiles
            // 
            this.retFiles.Location = new System.Drawing.Point(125, 16);
            this.retFiles.Name = "retFiles";
            this.retFiles.Size = new System.Drawing.Size(141, 38);
            this.retFiles.TabIndex = 4;
            this.retFiles.Text = "Retrieve Internal SD Files";
            this.retFiles.UseVisualStyleBackColor = true;
            this.retFiles.Click += new System.EventHandler(this.retFiles_Click);
            // 
            // clearInternalArchive
            // 
            this.clearInternalArchive.AutoSize = true;
            this.clearInternalArchive.Location = new System.Drawing.Point(5, 60);
            this.clearInternalArchive.Name = "clearInternalArchive";
            this.clearInternalArchive.Size = new System.Drawing.Size(171, 17);
            this.clearInternalArchive.TabIndex = 3;
            this.clearInternalArchive.Text = "Clear Archive Bit After Transfer";
            this.clearInternalArchive.UseVisualStyleBackColor = true;
            // 
            // selInt
            // 
            this.selInt.AutoSize = true;
            this.selInt.Checked = true;
            this.selInt.Location = new System.Drawing.Point(272, 16);
            this.selInt.Name = "selInt";
            this.selInt.Size = new System.Drawing.Size(91, 17);
            this.selInt.TabIndex = 2;
            this.selInt.TabStop = true;
            this.selInt.Text = "Selected Files";
            this.selInt.UseVisualStyleBackColor = true;
            // 
            // allInt
            // 
            this.allInt.AutoSize = true;
            this.allInt.Location = new System.Drawing.Point(272, 38);
            this.allInt.Name = "allInt";
            this.allInt.Size = new System.Drawing.Size(60, 17);
            this.allInt.TabIndex = 1;
            this.allInt.Text = "All Files";
            this.allInt.UseVisualStyleBackColor = true;
            this.allInt.CheckedChanged += new System.EventHandler(this.allInt_CheckedChanged);
            // 
            // nonArchiveBtnIn
            // 
            this.nonArchiveBtnIn.AutoSize = true;
            this.nonArchiveBtnIn.Location = new System.Drawing.Point(272, 60);
            this.nonArchiveBtnIn.Name = "nonArchiveBtnIn";
            this.nonArchiveBtnIn.Size = new System.Drawing.Size(114, 17);
            this.nonArchiveBtnIn.TabIndex = 0;
            this.nonArchiveBtnIn.Text = "Non-Archived Files";
            this.nonArchiveBtnIn.UseVisualStyleBackColor = true;
            this.nonArchiveBtnIn.CheckedChanged += new System.EventHandler(this.nonArchiveBtnIn_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.clearExternalArchive);
            this.groupBox2.Controls.Add(this.selExt);
            this.groupBox2.Controls.Add(this.allExt);
            this.groupBox2.Controls.Add(this.nonArchiveBtnEx);
            this.groupBox2.Location = new System.Drawing.Point(423, 484);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(387, 81);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transfer Options";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(5, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 38);
            this.button2.TabIndex = 7;
            this.button2.Text = "Clear Selection";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.clearSel_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(122, 16);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(143, 38);
            this.button4.TabIndex = 6;
            this.button4.Text = "Retrieve External SD Files";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.retFiles_Click);
            // 
            // clearExternalArchive
            // 
            this.clearExternalArchive.AutoSize = true;
            this.clearExternalArchive.Location = new System.Drawing.Point(5, 60);
            this.clearExternalArchive.Name = "clearExternalArchive";
            this.clearExternalArchive.Size = new System.Drawing.Size(171, 17);
            this.clearExternalArchive.TabIndex = 3;
            this.clearExternalArchive.Text = "Clear Archive Bit After Transfer";
            this.clearExternalArchive.UseVisualStyleBackColor = true;
            // 
            // selExt
            // 
            this.selExt.AutoSize = true;
            this.selExt.Checked = true;
            this.selExt.Location = new System.Drawing.Point(270, 16);
            this.selExt.Name = "selExt";
            this.selExt.Size = new System.Drawing.Size(91, 17);
            this.selExt.TabIndex = 2;
            this.selExt.TabStop = true;
            this.selExt.Text = "Selected Files";
            this.selExt.UseVisualStyleBackColor = true;
            // 
            // allExt
            // 
            this.allExt.AutoSize = true;
            this.allExt.Location = new System.Drawing.Point(270, 38);
            this.allExt.Name = "allExt";
            this.allExt.Size = new System.Drawing.Size(60, 17);
            this.allExt.TabIndex = 1;
            this.allExt.Text = "All Files";
            this.allExt.UseVisualStyleBackColor = true;
            this.allExt.CheckedChanged += new System.EventHandler(this.allInt_CheckedChanged);
            // 
            // nonArchiveBtnEx
            // 
            this.nonArchiveBtnEx.AutoSize = true;
            this.nonArchiveBtnEx.Location = new System.Drawing.Point(270, 60);
            this.nonArchiveBtnEx.Name = "nonArchiveBtnEx";
            this.nonArchiveBtnEx.Size = new System.Drawing.Size(114, 17);
            this.nonArchiveBtnEx.TabIndex = 0;
            this.nonArchiveBtnEx.Text = "Non-Archived Files";
            this.nonArchiveBtnEx.UseVisualStyleBackColor = true;
            this.nonArchiveBtnEx.CheckedChanged += new System.EventHandler(this.nonArchiveBtnIn_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = global::QIY_Interface__IAEA_.Properties.Resources.QuaestaLogo;
            this.pictureBox1.Location = new System.Drawing.Point(623, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(187, 66);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // DataRetrievalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 568);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.internalGB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.externalBytesSelectedBox);
            this.Controls.Add(this.externalTotalFilesBox);
            this.Controls.Add(this.externalSelectedFilesBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.internalBytesSelectedBox);
            this.Controls.Add(this.internalTotalFilesBox);
            this.Controls.Add(this.internalSelectedFilesBox);
            this.Controls.Add(this.externalDirBox);
            this.Controls.Add(this.internalDirBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.externalInfoBox);
            this.Controls.Add(this.internalInfoBox);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.refreshBtn);
            this.Controls.Add(this.HGM);
            this.Controls.Add(this.BIN);
            this.Controls.Add(this.DAT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.ipBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataRetrievalForm";
            this.Text = "DataRetrievalForm";
            this.Load += new System.EventHandler(this.DataRetrievalForm_Load);
            this.internalGB.ResumeLayout(false);
            this.internalGB.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton DAT;
        private System.Windows.Forms.RadioButton BIN;
        private System.Windows.Forms.RadioButton HGM;
        private System.Windows.Forms.Button refreshBtn;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.CheckedListBox internalDirBox;
        internal System.Windows.Forms.CheckedListBox externalDirBox;
        internal System.Windows.Forms.RichTextBox internalInfoBox;
        internal System.Windows.Forms.RichTextBox externalInfoBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.TextBox internalSelectedFilesBox;
        internal System.Windows.Forms.TextBox internalTotalFilesBox;
        internal System.Windows.Forms.TextBox internalBytesSelectedBox;
        internal System.Windows.Forms.TextBox externalBytesSelectedBox;
        internal System.Windows.Forms.TextBox externalTotalFilesBox;
        internal System.Windows.Forms.TextBox externalSelectedFilesBox;
        private System.Windows.Forms.GroupBox internalGB;
        private System.Windows.Forms.Button retFiles;
        private System.Windows.Forms.CheckBox clearInternalArchive;
        private System.Windows.Forms.RadioButton selInt;
        private System.Windows.Forms.RadioButton allInt;
        private System.Windows.Forms.RadioButton nonArchiveBtnIn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox clearExternalArchive;
        private System.Windows.Forms.RadioButton selExt;
        private System.Windows.Forms.RadioButton allExt;
        private System.Windows.Forms.RadioButton nonArchiveBtnEx;
        private System.Windows.Forms.Button clearSel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
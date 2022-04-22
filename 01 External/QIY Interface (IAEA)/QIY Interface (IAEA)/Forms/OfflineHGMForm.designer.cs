
namespace QIY_Interface__IAEA_
{
    partial class OfflineHGMForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OfflineHGMForm));
            this.plotView = new OxyPlot.WindowsForms.PlotView();
            this.hgmDate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.elapsedTimeBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.stopTimeBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.startTimeBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.fileNameBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.infoGB = new System.Windows.Forms.GroupBox();
            this.modelBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.snBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.nbinsBox = new System.Windows.Forms.TextBox();
            this.hrsIntBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.secIntBox = new System.Windows.Forms.TextBox();
            this.gainBox = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.hvBox = new System.Windows.Forms.TextBox();
            this.discHighBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.discLowBox = new System.Windows.Forms.TextBox();
            this.rphgmBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.rpbox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.trackbar = new System.Windows.Forms.TrackBar();
            this.advSegBtn = new System.Windows.Forms.Button();
            this.backSegBtn = new System.Windows.Forms.Button();
            this.totalHGMBox = new System.Windows.Forms.TextBox();
            this.curHGMBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.infoGB.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // plotView
            // 
            this.plotView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotView.BackColor = System.Drawing.SystemColors.Control;
            this.plotView.Location = new System.Drawing.Point(10, 92);
            this.plotView.Name = "plotView";
            this.plotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView.Size = new System.Drawing.Size(493, 273);
            this.plotView.TabIndex = 0;
            this.plotView.Text = "plotView1";
            this.plotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // hgmDate
            // 
            this.hgmDate.Location = new System.Drawing.Point(380, 47);
            this.hgmDate.Name = "hgmDate";
            this.hgmDate.ReadOnly = true;
            this.hgmDate.Size = new System.Drawing.Size(124, 20);
            this.hgmDate.TabIndex = 75;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(321, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 74;
            this.label5.Text = "Date/Time";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // elapsedTimeBox
            // 
            this.elapsedTimeBox.Location = new System.Drawing.Point(416, 68);
            this.elapsedTimeBox.Name = "elapsedTimeBox";
            this.elapsedTimeBox.ReadOnly = true;
            this.elapsedTimeBox.Size = new System.Drawing.Size(88, 20);
            this.elapsedTimeBox.TabIndex = 83;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(321, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 82;
            this.label6.Text = "Elapsed Time (sec)";
            // 
            // stopTimeBox
            // 
            this.stopTimeBox.Location = new System.Drawing.Point(67, 68);
            this.stopTimeBox.Name = "stopTimeBox";
            this.stopTimeBox.ReadOnly = true;
            this.stopTimeBox.Size = new System.Drawing.Size(112, 20);
            this.stopTimeBox.TabIndex = 81;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 80;
            this.label7.Text = "Last Rec:";
            // 
            // startTimeBox
            // 
            this.startTimeBox.Location = new System.Drawing.Point(67, 47);
            this.startTimeBox.Name = "startTimeBox";
            this.startTimeBox.ReadOnly = true;
            this.startTimeBox.Size = new System.Drawing.Size(112, 20);
            this.startTimeBox.TabIndex = 79;
            this.startTimeBox.TextChanged += new System.EventHandler(this.startTimeBox_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 78;
            this.label8.Text = "First Rec:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // fileNameBox
            // 
            this.fileNameBox.Location = new System.Drawing.Point(67, 26);
            this.fileNameBox.Name = "fileNameBox";
            this.fileNameBox.ReadOnly = true;
            this.fileNameBox.Size = new System.Drawing.Size(629, 20);
            this.fileNameBox.TabIndex = 77;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 76;
            this.label9.Text = "File Name:";
            // 
            // infoGB
            // 
            this.infoGB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoGB.Controls.Add(this.modelBox);
            this.infoGB.Controls.Add(this.label11);
            this.infoGB.Controls.Add(this.snBox);
            this.infoGB.Controls.Add(this.label10);
            this.infoGB.Controls.Add(this.nbinsBox);
            this.infoGB.Controls.Add(this.hrsIntBox);
            this.infoGB.Controls.Add(this.label1);
            this.infoGB.Controls.Add(this.label2);
            this.infoGB.Controls.Add(this.secIntBox);
            this.infoGB.Controls.Add(this.gainBox);
            this.infoGB.Controls.Add(this.label21);
            this.infoGB.Controls.Add(this.label22);
            this.infoGB.Controls.Add(this.hvBox);
            this.infoGB.Controls.Add(this.discHighBox);
            this.infoGB.Controls.Add(this.label15);
            this.infoGB.Controls.Add(this.label16);
            this.infoGB.Controls.Add(this.discLowBox);
            this.infoGB.Controls.Add(this.rphgmBox);
            this.infoGB.Controls.Add(this.label17);
            this.infoGB.Controls.Add(this.label18);
            this.infoGB.Controls.Add(this.rpbox);
            this.infoGB.Controls.Add(this.label13);
            this.infoGB.Controls.Add(this.nameBox);
            this.infoGB.Controls.Add(this.label12);
            this.infoGB.Location = new System.Drawing.Point(509, 46);
            this.infoGB.Name = "infoGB";
            this.infoGB.Size = new System.Drawing.Size(189, 319);
            this.infoGB.TabIndex = 84;
            this.infoGB.TabStop = false;
            this.infoGB.Text = "Info";
            // 
            // modelBox
            // 
            this.modelBox.Location = new System.Drawing.Point(77, 62);
            this.modelBox.Name = "modelBox";
            this.modelBox.ReadOnly = true;
            this.modelBox.Size = new System.Drawing.Size(103, 20);
            this.modelBox.TabIndex = 95;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(11, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 94;
            this.label11.Text = "Model Vers.";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // snBox
            // 
            this.snBox.Location = new System.Drawing.Point(77, 36);
            this.snBox.Name = "snBox";
            this.snBox.ReadOnly = true;
            this.snBox.Size = new System.Drawing.Size(103, 20);
            this.snBox.TabIndex = 93;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(11, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 92;
            this.label10.Text = "Serial #";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nbinsBox
            // 
            this.nbinsBox.Location = new System.Drawing.Point(121, 288);
            this.nbinsBox.Name = "nbinsBox";
            this.nbinsBox.ReadOnly = true;
            this.nbinsBox.Size = new System.Drawing.Size(58, 20);
            this.nbinsBox.TabIndex = 91;
            // 
            // hrsIntBox
            // 
            this.hrsIntBox.Location = new System.Drawing.Point(121, 263);
            this.hrsIntBox.Name = "hrsIntBox";
            this.hrsIntBox.ReadOnly = true;
            this.hrsIntBox.Size = new System.Drawing.Size(58, 20);
            this.hrsIntBox.TabIndex = 89;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 90;
            this.label1.Text = "Number of Bins";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 88;
            this.label2.Text = "HGM Interval (hrs)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // secIntBox
            // 
            this.secIntBox.Location = new System.Drawing.Point(121, 237);
            this.secIntBox.Name = "secIntBox";
            this.secIntBox.ReadOnly = true;
            this.secIntBox.Size = new System.Drawing.Size(60, 20);
            this.secIntBox.TabIndex = 87;
            // 
            // gainBox
            // 
            this.gainBox.Location = new System.Drawing.Point(121, 212);
            this.gainBox.Name = "gainBox";
            this.gainBox.ReadOnly = true;
            this.gainBox.Size = new System.Drawing.Size(60, 20);
            this.gainBox.TabIndex = 85;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(11, 240);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(105, 13);
            this.label21.TabIndex = 86;
            this.label21.Text = "HGM Interval (sec)";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(11, 215);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(105, 13);
            this.label22.TabIndex = 84;
            this.label22.Text = "Gain";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hvBox
            // 
            this.hvBox.Location = new System.Drawing.Point(121, 187);
            this.hvBox.Name = "hvBox";
            this.hvBox.ReadOnly = true;
            this.hvBox.Size = new System.Drawing.Size(60, 20);
            this.hvBox.TabIndex = 83;
            // 
            // discHighBox
            // 
            this.discHighBox.Location = new System.Drawing.Point(121, 162);
            this.discHighBox.Name = "discHighBox";
            this.discHighBox.ReadOnly = true;
            this.discHighBox.Size = new System.Drawing.Size(60, 20);
            this.discHighBox.TabIndex = 81;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(11, 190);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(105, 13);
            this.label15.TabIndex = 82;
            this.label15.Text = "High Voltage";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(11, 165);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(105, 13);
            this.label16.TabIndex = 80;
            this.label16.Text = "Upper Discriminator";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // discLowBox
            // 
            this.discLowBox.Location = new System.Drawing.Point(121, 137);
            this.discLowBox.Name = "discLowBox";
            this.discLowBox.ReadOnly = true;
            this.discLowBox.Size = new System.Drawing.Size(60, 20);
            this.discLowBox.TabIndex = 79;
            // 
            // rphgmBox
            // 
            this.rphgmBox.Location = new System.Drawing.Point(121, 112);
            this.rphgmBox.Name = "rphgmBox";
            this.rphgmBox.ReadOnly = true;
            this.rphgmBox.Size = new System.Drawing.Size(60, 20);
            this.rphgmBox.TabIndex = 77;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(11, 140);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(105, 13);
            this.label17.TabIndex = 78;
            this.label17.Text = "Lower Discriminator";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(11, 114);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(105, 13);
            this.label18.TabIndex = 76;
            this.label18.Text = "Records Per HGM";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rpbox
            // 
            this.rpbox.Location = new System.Drawing.Point(121, 87);
            this.rpbox.Name = "rpbox";
            this.rpbox.ReadOnly = true;
            this.rpbox.Size = new System.Drawing.Size(60, 20);
            this.rpbox.TabIndex = 75;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(11, 89);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 13);
            this.label13.TabIndex = 74;
            this.label13.Text = "Record Period";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(77, 11);
            this.nameBox.Name = "nameBox";
            this.nameBox.ReadOnly = true;
            this.nameBox.Size = new System.Drawing.Size(103, 20);
            this.nameBox.TabIndex = 69;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(11, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 68;
            this.label12.Text = "Name";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(707, 24);
            this.menuStrip1.TabIndex = 85;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // trackbar
            // 
            this.trackbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackbar.AutoSize = false;
            this.trackbar.Enabled = false;
            this.trackbar.Location = new System.Drawing.Point(10, 370);
            this.trackbar.Name = "trackbar";
            this.trackbar.Size = new System.Drawing.Size(687, 29);
            this.trackbar.TabIndex = 86;
            this.trackbar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackbar.Scroll += new System.EventHandler(this.trackbar_Scroll);
            // 
            // advSegBtn
            // 
            this.advSegBtn.Location = new System.Drawing.Point(238, 67);
            this.advSegBtn.Name = "advSegBtn";
            this.advSegBtn.Size = new System.Drawing.Size(19, 20);
            this.advSegBtn.TabIndex = 92;
            this.advSegBtn.Text = ">";
            this.advSegBtn.UseVisualStyleBackColor = true;
            this.advSegBtn.Click += new System.EventHandler(this.advSegBtn_Click);
            // 
            // backSegBtn
            // 
            this.backSegBtn.Location = new System.Drawing.Point(216, 67);
            this.backSegBtn.Name = "backSegBtn";
            this.backSegBtn.Size = new System.Drawing.Size(19, 20);
            this.backSegBtn.TabIndex = 91;
            this.backSegBtn.Text = "<";
            this.backSegBtn.UseVisualStyleBackColor = true;
            this.backSegBtn.Click += new System.EventHandler(this.backSegBtn_Click);
            // 
            // totalHGMBox
            // 
            this.totalHGMBox.Location = new System.Drawing.Point(279, 68);
            this.totalHGMBox.Name = "totalHGMBox";
            this.totalHGMBox.ReadOnly = true;
            this.totalHGMBox.Size = new System.Drawing.Size(37, 20);
            this.totalHGMBox.TabIndex = 90;
            // 
            // curHGMBox
            // 
            this.curHGMBox.Location = new System.Drawing.Point(279, 47);
            this.curHGMBox.Name = "curHGMBox";
            this.curHGMBox.Size = new System.Drawing.Size(37, 20);
            this.curHGMBox.TabIndex = 88;
            this.curHGMBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChooseIndex);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(204, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 87;
            this.label3.Text = "Histogram #";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(255, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 89;
            this.label4.Text = "of";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OfflineHGMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 402);
            this.Controls.Add(this.advSegBtn);
            this.Controls.Add(this.backSegBtn);
            this.Controls.Add(this.totalHGMBox);
            this.Controls.Add(this.curHGMBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trackbar);
            this.Controls.Add(this.infoGB);
            this.Controls.Add(this.elapsedTimeBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.stopTimeBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.startTimeBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.fileNameBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.hgmDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.plotView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "OfflineHGMForm";
            this.Text = "NPM Histogram Charts - Offline";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HideForm);
            this.Load += new System.EventHandler(this.OfflineHGMForm_Load);
            this.infoGB.ResumeLayout(false);
            this.infoGB.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView plotView;
        private System.Windows.Forms.TextBox hgmDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox elapsedTimeBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox stopTimeBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox startTimeBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox fileNameBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox infoGB;
        private System.Windows.Forms.TextBox nbinsBox;
        private System.Windows.Forms.TextBox hrsIntBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox secIntBox;
        private System.Windows.Forms.TextBox gainBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox hvBox;
        private System.Windows.Forms.TextBox discHighBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox discLowBox;
        private System.Windows.Forms.TextBox rphgmBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox rpbox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox modelBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox snBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar trackbar;
        private System.Windows.Forms.Button advSegBtn;
        private System.Windows.Forms.Button backSegBtn;
        private System.Windows.Forms.TextBox totalHGMBox;
        private System.Windows.Forms.TextBox curHGMBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
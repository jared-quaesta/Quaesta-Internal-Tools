
namespace QIY_Interface__IAEA_
{
    partial class OfflinePlotForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OfflinePlotForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBINFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDATFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.fileNameBox = new System.Windows.Forms.TextBox();
            this.startTimeBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.stopTimeBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.elapsedTimeBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numRecordsBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.curSegBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.segGB = new System.Windows.Forms.GroupBox();
            this.segLogSigBox = new System.Windows.Forms.TextBox();
            this.segLogBattBox = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.segLogRhBox = new System.Windows.Forms.TextBox();
            this.segLogTBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.segLogExtBox = new System.Windows.Forms.TextBox();
            this.segPulseThresh = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.segLogHgmsBox = new System.Windows.Forms.TextBox();
            this.segDiscHighBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.segDiscLowBox = new System.Windows.Forms.TextBox();
            this.segGainBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.segHvBox = new System.Windows.Forms.TextBox();
            this.segNfBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.segRecPerHGMBox = new System.Windows.Forms.TextBox();
            this.segRpBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.segSnBox = new System.Windows.Forms.TextBox();
            this.segNameBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.segStopBox = new System.Windows.Forms.TextBox();
            this.segStartBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.genPlot = new OxyPlot.WindowsForms.PlotView();
            this.countsPlot = new OxyPlot.WindowsForms.PlotView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateRadio = new System.Windows.Forms.RadioButton();
            this.recordRadio = new System.Windows.Forms.RadioButton();
            this.genPanel = new System.Windows.Forms.Panel();
            this.signalRadio = new System.Windows.Forms.RadioButton();
            this.voltRadio = new System.Windows.Forms.RadioButton();
            this.rhRadio = new System.Windows.Forms.RadioButton();
            this.tempRadio = new System.Windows.Forms.RadioButton();
            this.label25 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.genRPRadio = new System.Windows.Forms.RadioButton();
            this.label26 = new System.Windows.Forms.Label();
            this.crcErrBox = new System.Windows.Forms.TextBox();
            this.allSegsBox = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.backSegBtn = new System.Windows.Forms.Button();
            this.advSegBtn = new System.Windows.Forms.Button();
            this.countsPanel = new System.Windows.Forms.Panel();
            this.extCountsCheck = new System.Windows.Forms.CheckBox();
            this.npmCountsCheck = new System.Windows.Forms.CheckBox();
            this.refMain = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.newEpochRadio = new System.Windows.Forms.RadioButton();
            this.oldEpochRadio = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.segGB.SuspendLayout();
            this.panel1.SuspendLayout();
            this.genPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.countsPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(706, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openBINFileToolStripMenuItem,
            this.openDATFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openBINFileToolStripMenuItem
            // 
            this.openBINFileToolStripMenuItem.Name = "openBINFileToolStripMenuItem";
            this.openBINFileToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.openBINFileToolStripMenuItem.Text = "Open BIN File";
            this.openBINFileToolStripMenuItem.Click += new System.EventHandler(this.openBINFileToolStripMenuItem_Click);
            // 
            // openDATFileToolStripMenuItem
            // 
            this.openDATFileToolStripMenuItem.Name = "openDATFileToolStripMenuItem";
            this.openDATFileToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.openDATFileToolStripMenuItem.Text = "Open DAT File";
            this.openDATFileToolStripMenuItem.Click += new System.EventHandler(this.openDATFileToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "File Name:";
            // 
            // fileNameBox
            // 
            this.fileNameBox.Location = new System.Drawing.Point(69, 22);
            this.fileNameBox.Name = "fileNameBox";
            this.fileNameBox.ReadOnly = true;
            this.fileNameBox.Size = new System.Drawing.Size(629, 20);
            this.fileNameBox.TabIndex = 4;
            // 
            // startTimeBox
            // 
            this.startTimeBox.Location = new System.Drawing.Point(69, 46);
            this.startTimeBox.Name = "startTimeBox";
            this.startTimeBox.ReadOnly = true;
            this.startTimeBox.Size = new System.Drawing.Size(136, 20);
            this.startTimeBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Start Time";
            // 
            // stopTimeBox
            // 
            this.stopTimeBox.Location = new System.Drawing.Point(263, 46);
            this.stopTimeBox.Name = "stopTimeBox";
            this.stopTimeBox.ReadOnly = true;
            this.stopTimeBox.Size = new System.Drawing.Size(136, 20);
            this.stopTimeBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(208, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "End Time";
            // 
            // elapsedTimeBox
            // 
            this.elapsedTimeBox.Location = new System.Drawing.Point(488, 46);
            this.elapsedTimeBox.Name = "elapsedTimeBox";
            this.elapsedTimeBox.ReadOnly = true;
            this.elapsedTimeBox.Size = new System.Drawing.Size(29, 20);
            this.elapsedTimeBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(402, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Elapsed Time (h)";
            // 
            // numRecordsBox
            // 
            this.numRecordsBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.numRecordsBox.Location = new System.Drawing.Point(570, 74);
            this.numRecordsBox.Name = "numRecordsBox";
            this.numRecordsBox.ReadOnly = true;
            this.numRecordsBox.Size = new System.Drawing.Size(43, 20);
            this.numRecordsBox.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(483, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "# Data Records";
            // 
            // curSegBox
            // 
            this.curSegBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.curSegBox.Location = new System.Drawing.Point(570, 99);
            this.curSegBox.Name = "curSegBox";
            this.curSegBox.ReadOnly = true;
            this.curSegBox.Size = new System.Drawing.Size(36, 20);
            this.curSegBox.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(487, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Data Segment";
            // 
            // segGB
            // 
            this.segGB.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.segGB.Controls.Add(this.segLogSigBox);
            this.segGB.Controls.Add(this.segLogBattBox);
            this.segGB.Controls.Add(this.label23);
            this.segGB.Controls.Add(this.label24);
            this.segGB.Controls.Add(this.segLogRhBox);
            this.segGB.Controls.Add(this.segLogTBox);
            this.segGB.Controls.Add(this.label19);
            this.segGB.Controls.Add(this.label20);
            this.segGB.Controls.Add(this.segLogExtBox);
            this.segGB.Controls.Add(this.segPulseThresh);
            this.segGB.Controls.Add(this.label21);
            this.segGB.Controls.Add(this.label22);
            this.segGB.Controls.Add(this.segLogHgmsBox);
            this.segGB.Controls.Add(this.segDiscHighBox);
            this.segGB.Controls.Add(this.label15);
            this.segGB.Controls.Add(this.label16);
            this.segGB.Controls.Add(this.segDiscLowBox);
            this.segGB.Controls.Add(this.segGainBox);
            this.segGB.Controls.Add(this.label17);
            this.segGB.Controls.Add(this.label18);
            this.segGB.Controls.Add(this.segHvBox);
            this.segGB.Controls.Add(this.segNfBox);
            this.segGB.Controls.Add(this.label13);
            this.segGB.Controls.Add(this.label14);
            this.segGB.Controls.Add(this.segRecPerHGMBox);
            this.segGB.Controls.Add(this.segRpBox);
            this.segGB.Controls.Add(this.label11);
            this.segGB.Controls.Add(this.label12);
            this.segGB.Controls.Add(this.segSnBox);
            this.segGB.Controls.Add(this.segNameBox);
            this.segGB.Controls.Add(this.label9);
            this.segGB.Controls.Add(this.label10);
            this.segGB.Controls.Add(this.segStopBox);
            this.segGB.Controls.Add(this.segStartBox);
            this.segGB.Controls.Add(this.label7);
            this.segGB.Controls.Add(this.label8);
            this.segGB.Location = new System.Drawing.Point(483, 124);
            this.segGB.Name = "segGB";
            this.segGB.Size = new System.Drawing.Size(219, 486);
            this.segGB.TabIndex = 15;
            this.segGB.TabStop = false;
            this.segGB.Text = "Segment Params";
            // 
            // segLogSigBox
            // 
            this.segLogSigBox.Location = new System.Drawing.Point(144, 446);
            this.segLogSigBox.Name = "segLogSigBox";
            this.segLogSigBox.ReadOnly = true;
            this.segLogSigBox.Size = new System.Drawing.Size(71, 20);
            this.segLogSigBox.TabIndex = 51;
            // 
            // segLogBattBox
            // 
            this.segLogBattBox.Location = new System.Drawing.Point(144, 421);
            this.segLogBattBox.Name = "segLogBattBox";
            this.segLogBattBox.ReadOnly = true;
            this.segLogBattBox.Size = new System.Drawing.Size(71, 20);
            this.segLogBattBox.TabIndex = 49;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(5, 449);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(134, 13);
            this.label23.TabIndex = 50;
            this.label23.Text = "Log Signal";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(5, 424);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(134, 13);
            this.label24.TabIndex = 48;
            this.label24.Text = "Log Battery Voltage";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segLogRhBox
            // 
            this.segLogRhBox.Location = new System.Drawing.Point(144, 396);
            this.segLogRhBox.Name = "segLogRhBox";
            this.segLogRhBox.ReadOnly = true;
            this.segLogRhBox.Size = new System.Drawing.Size(71, 20);
            this.segLogRhBox.TabIndex = 47;
            // 
            // segLogTBox
            // 
            this.segLogTBox.Location = new System.Drawing.Point(144, 371);
            this.segLogTBox.Name = "segLogTBox";
            this.segLogTBox.ReadOnly = true;
            this.segLogTBox.Size = new System.Drawing.Size(71, 20);
            this.segLogTBox.TabIndex = 45;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(5, 399);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(134, 13);
            this.label19.TabIndex = 46;
            this.label19.Text = "Log RH%";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(5, 374);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(134, 13);
            this.label20.TabIndex = 44;
            this.label20.Text = "LogT_degC";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segLogExtBox
            // 
            this.segLogExtBox.Location = new System.Drawing.Point(144, 346);
            this.segLogExtBox.Name = "segLogExtBox";
            this.segLogExtBox.ReadOnly = true;
            this.segLogExtBox.Size = new System.Drawing.Size(71, 20);
            this.segLogExtBox.TabIndex = 43;
            // 
            // segPulseThresh
            // 
            this.segPulseThresh.Location = new System.Drawing.Point(144, 321);
            this.segPulseThresh.Name = "segPulseThresh";
            this.segPulseThresh.ReadOnly = true;
            this.segPulseThresh.Size = new System.Drawing.Size(71, 20);
            this.segPulseThresh.TabIndex = 41;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(5, 348);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(134, 13);
            this.label21.TabIndex = 42;
            this.label21.Text = "Log Ext Pulses";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(5, 323);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(134, 13);
            this.label22.TabIndex = 40;
            this.label22.Text = "Pulse Thresh";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segLogHgmsBox
            // 
            this.segLogHgmsBox.Location = new System.Drawing.Point(144, 296);
            this.segLogHgmsBox.Name = "segLogHgmsBox";
            this.segLogHgmsBox.ReadOnly = true;
            this.segLogHgmsBox.Size = new System.Drawing.Size(71, 20);
            this.segLogHgmsBox.TabIndex = 39;
            // 
            // segDiscHighBox
            // 
            this.segDiscHighBox.Location = new System.Drawing.Point(144, 270);
            this.segDiscHighBox.Name = "segDiscHighBox";
            this.segDiscHighBox.ReadOnly = true;
            this.segDiscHighBox.Size = new System.Drawing.Size(71, 20);
            this.segDiscHighBox.TabIndex = 37;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(5, 298);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(134, 13);
            this.label15.TabIndex = 38;
            this.label15.Text = "Log Histograms";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(5, 273);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(134, 13);
            this.label16.TabIndex = 36;
            this.label16.Text = "Upper Discriminator";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segDiscLowBox
            // 
            this.segDiscLowBox.Location = new System.Drawing.Point(144, 245);
            this.segDiscLowBox.Name = "segDiscLowBox";
            this.segDiscLowBox.ReadOnly = true;
            this.segDiscLowBox.Size = new System.Drawing.Size(71, 20);
            this.segDiscLowBox.TabIndex = 35;
            // 
            // segGainBox
            // 
            this.segGainBox.Location = new System.Drawing.Point(144, 220);
            this.segGainBox.Name = "segGainBox";
            this.segGainBox.ReadOnly = true;
            this.segGainBox.Size = new System.Drawing.Size(71, 20);
            this.segGainBox.TabIndex = 33;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(5, 248);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(134, 13);
            this.label17.TabIndex = 34;
            this.label17.Text = "Lower Discriminator";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(5, 223);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(134, 13);
            this.label18.TabIndex = 32;
            this.label18.Text = "Gain";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segHvBox
            // 
            this.segHvBox.Location = new System.Drawing.Point(144, 195);
            this.segHvBox.Name = "segHvBox";
            this.segHvBox.ReadOnly = true;
            this.segHvBox.Size = new System.Drawing.Size(71, 20);
            this.segHvBox.TabIndex = 31;
            // 
            // segNfBox
            // 
            this.segNfBox.Location = new System.Drawing.Point(144, 170);
            this.segNfBox.Name = "segNfBox";
            this.segNfBox.ReadOnly = true;
            this.segNfBox.Size = new System.Drawing.Size(71, 20);
            this.segNfBox.TabIndex = 29;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(5, 198);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(134, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "High Voltage";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(5, 172);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(134, 13);
            this.label14.TabIndex = 28;
            this.label14.Text = "New File Period";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segRecPerHGMBox
            // 
            this.segRecPerHGMBox.Location = new System.Drawing.Point(144, 145);
            this.segRecPerHGMBox.Name = "segRecPerHGMBox";
            this.segRecPerHGMBox.ReadOnly = true;
            this.segRecPerHGMBox.Size = new System.Drawing.Size(71, 20);
            this.segRecPerHGMBox.TabIndex = 27;
            // 
            // segRpBox
            // 
            this.segRpBox.Location = new System.Drawing.Point(144, 120);
            this.segRpBox.Name = "segRpBox";
            this.segRpBox.ReadOnly = true;
            this.segRpBox.Size = new System.Drawing.Size(71, 20);
            this.segRpBox.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(5, 147);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Records Per HGM";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(5, 122);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(134, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Record Period (sec)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segSnBox
            // 
            this.segSnBox.Location = new System.Drawing.Point(87, 94);
            this.segSnBox.Name = "segSnBox";
            this.segSnBox.ReadOnly = true;
            this.segSnBox.Size = new System.Drawing.Size(127, 20);
            this.segSnBox.TabIndex = 23;
            // 
            // segNameBox
            // 
            this.segNameBox.Location = new System.Drawing.Point(87, 69);
            this.segNameBox.Name = "segNameBox";
            this.segNameBox.ReadOnly = true;
            this.segNameBox.Size = new System.Drawing.Size(127, 20);
            this.segNameBox.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(5, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Serial Number";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(5, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Name";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segStopBox
            // 
            this.segStopBox.Location = new System.Drawing.Point(87, 44);
            this.segStopBox.Name = "segStopBox";
            this.segStopBox.ReadOnly = true;
            this.segStopBox.Size = new System.Drawing.Size(127, 20);
            this.segStopBox.TabIndex = 19;
            // 
            // segStartBox
            // 
            this.segStartBox.Location = new System.Drawing.Point(87, 19);
            this.segStartBox.Name = "segStartBox";
            this.segStartBox.ReadOnly = true;
            this.segStartBox.Size = new System.Drawing.Size(127, 20);
            this.segStartBox.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(5, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Stop";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(5, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Start";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // genPlot
            // 
            this.genPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genPlot.BackColor = System.Drawing.SystemColors.Control;
            this.genPlot.Location = new System.Drawing.Point(5, 29);
            this.genPlot.Name = "genPlot";
            this.genPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.genPlot.Size = new System.Drawing.Size(448, 224);
            this.genPlot.TabIndex = 17;
            this.genPlot.Text = "plotView2";
            this.genPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.genPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.genPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // countsPlot
            // 
            this.countsPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.countsPlot.BackColor = System.Drawing.SystemColors.Control;
            this.countsPlot.Location = new System.Drawing.Point(5, 29);
            this.countsPlot.Name = "countsPlot";
            this.countsPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.countsPlot.Size = new System.Drawing.Size(448, 222);
            this.countsPlot.TabIndex = 18;
            this.countsPlot.Text = "plotView1";
            this.countsPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.countsPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.countsPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dateRadio);
            this.panel1.Controls.Add(this.recordRadio);
            this.panel1.Location = new System.Drawing.Point(310, 232);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 20);
            this.panel1.TabIndex = 19;
            // 
            // dateRadio
            // 
            this.dateRadio.AutoSize = true;
            this.dateRadio.Location = new System.Drawing.Point(70, 1);
            this.dateRadio.Name = "dateRadio";
            this.dateRadio.Size = new System.Drawing.Size(74, 17);
            this.dateRadio.TabIndex = 1;
            this.dateRadio.Text = "Date Time";
            this.dateRadio.UseVisualStyleBackColor = true;
            this.dateRadio.CheckedChanged += new System.EventHandler(this.dateRadio_CheckedChanged);
            // 
            // recordRadio
            // 
            this.recordRadio.AutoSize = true;
            this.recordRadio.Checked = true;
            this.recordRadio.Location = new System.Drawing.Point(3, 1);
            this.recordRadio.Name = "recordRadio";
            this.recordRadio.Size = new System.Drawing.Size(70, 17);
            this.recordRadio.TabIndex = 0;
            this.recordRadio.TabStop = true;
            this.recordRadio.Text = "Record #";
            this.recordRadio.UseVisualStyleBackColor = true;
            this.recordRadio.CheckedChanged += new System.EventHandler(this.CountsXAxis);
            // 
            // genPanel
            // 
            this.genPanel.BackColor = System.Drawing.Color.Transparent;
            this.genPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.genPanel.Controls.Add(this.signalRadio);
            this.genPanel.Controls.Add(this.voltRadio);
            this.genPanel.Controls.Add(this.rhRadio);
            this.genPanel.Controls.Add(this.tempRadio);
            this.genPanel.Location = new System.Drawing.Point(43, 7);
            this.genPanel.Name = "genPanel";
            this.genPanel.Size = new System.Drawing.Size(362, 20);
            this.genPanel.TabIndex = 20;
            // 
            // signalRadio
            // 
            this.signalRadio.AutoSize = true;
            this.signalRadio.Location = new System.Drawing.Point(271, 1);
            this.signalRadio.Name = "signalRadio";
            this.signalRadio.Size = new System.Drawing.Size(81, 17);
            this.signalRadio.TabIndex = 3;
            this.signalRadio.Text = "Signal Input";
            this.signalRadio.UseVisualStyleBackColor = true;
            this.signalRadio.CheckedChanged += new System.EventHandler(this.ChangegenPlot);
            // 
            // voltRadio
            // 
            this.voltRadio.AutoSize = true;
            this.voltRadio.Location = new System.Drawing.Point(177, 1);
            this.voltRadio.Name = "voltRadio";
            this.voltRadio.Size = new System.Drawing.Size(96, 17);
            this.voltRadio.TabIndex = 2;
            this.voltRadio.Text = "Supply Voltage";
            this.voltRadio.UseVisualStyleBackColor = true;
            this.voltRadio.CheckedChanged += new System.EventHandler(this.ChangegenPlot);
            // 
            // rhRadio
            // 
            this.rhRadio.AutoSize = true;
            this.rhRadio.Location = new System.Drawing.Point(89, 1);
            this.rhRadio.Name = "rhRadio";
            this.rhRadio.Size = new System.Drawing.Size(87, 17);
            this.rhRadio.TabIndex = 1;
            this.rhRadio.Text = "Rel. Humidity";
            this.rhRadio.UseVisualStyleBackColor = true;
            this.rhRadio.CheckedChanged += new System.EventHandler(this.ChangegenPlot);
            // 
            // tempRadio
            // 
            this.tempRadio.AutoSize = true;
            this.tempRadio.Checked = true;
            this.tempRadio.Location = new System.Drawing.Point(3, 1);
            this.tempRadio.Name = "tempRadio";
            this.tempRadio.Size = new System.Drawing.Size(85, 17);
            this.tempRadio.TabIndex = 0;
            this.tempRadio.TabStop = true;
            this.tempRadio.Text = "Temperature";
            this.tempRadio.UseVisualStyleBackColor = true;
            this.tempRadio.CheckedChanged += new System.EventHandler(this.ChangegenPlot);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(4, 11);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(37, 13);
            this.label25.TabIndex = 21;
            this.label25.Text = "Show:";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.radioButton1);
            this.panel3.Controls.Add(this.genRPRadio);
            this.panel3.Location = new System.Drawing.Point(310, 233);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(143, 20);
            this.panel3.TabIndex = 20;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(70, 1);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(74, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.Text = "Date Time";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // genRPRadio
            // 
            this.genRPRadio.AutoSize = true;
            this.genRPRadio.Checked = true;
            this.genRPRadio.Location = new System.Drawing.Point(3, 1);
            this.genRPRadio.Name = "genRPRadio";
            this.genRPRadio.Size = new System.Drawing.Size(70, 17);
            this.genRPRadio.TabIndex = 0;
            this.genRPRadio.TabStop = true;
            this.genRPRadio.Text = "Record #";
            this.genRPRadio.UseVisualStyleBackColor = true;
            this.genRPRadio.CheckedChanged += new System.EventHandler(this.genRPRadio_CheckedChanged);
            // 
            // label26
            // 
            this.label26.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(618, 76);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(50, 13);
            this.label26.TabIndex = 22;
            this.label26.Text = "CRC Errs";
            // 
            // crcErrBox
            // 
            this.crcErrBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.crcErrBox.Location = new System.Drawing.Point(666, 74);
            this.crcErrBox.Name = "crcErrBox";
            this.crcErrBox.ReadOnly = true;
            this.crcErrBox.Size = new System.Drawing.Size(35, 20);
            this.crcErrBox.TabIndex = 23;
            // 
            // allSegsBox
            // 
            this.allSegsBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.allSegsBox.Location = new System.Drawing.Point(622, 99);
            this.allSegsBox.Name = "allSegsBox";
            this.allSegsBox.ReadOnly = true;
            this.allSegsBox.Size = new System.Drawing.Size(36, 20);
            this.allSegsBox.TabIndex = 24;
            // 
            // label27
            // 
            this.label27.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(606, 101);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(16, 13);
            this.label27.TabIndex = 25;
            this.label27.Text = "of";
            // 
            // backSegBtn
            // 
            this.backSegBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.backSegBtn.Location = new System.Drawing.Point(662, 99);
            this.backSegBtn.Name = "backSegBtn";
            this.backSegBtn.Size = new System.Drawing.Size(19, 20);
            this.backSegBtn.TabIndex = 26;
            this.backSegBtn.Text = "<";
            this.backSegBtn.UseVisualStyleBackColor = true;
            this.backSegBtn.Click += new System.EventHandler(this.backSegBtn_Click);
            // 
            // advSegBtn
            // 
            this.advSegBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.advSegBtn.Location = new System.Drawing.Point(681, 99);
            this.advSegBtn.Name = "advSegBtn";
            this.advSegBtn.Size = new System.Drawing.Size(19, 20);
            this.advSegBtn.TabIndex = 27;
            this.advSegBtn.Text = ">";
            this.advSegBtn.UseVisualStyleBackColor = true;
            this.advSegBtn.Click += new System.EventHandler(this.advSegBtn_Click);
            // 
            // countsPanel
            // 
            this.countsPanel.BackColor = System.Drawing.Color.Transparent;
            this.countsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.countsPanel.Controls.Add(this.extCountsCheck);
            this.countsPanel.Controls.Add(this.npmCountsCheck);
            this.countsPanel.Location = new System.Drawing.Point(7, 6);
            this.countsPanel.Name = "countsPanel";
            this.countsPanel.Size = new System.Drawing.Size(197, 20);
            this.countsPanel.TabIndex = 20;
            // 
            // extCountsCheck
            // 
            this.extCountsCheck.AutoSize = true;
            this.extCountsCheck.Location = new System.Drawing.Point(87, 2);
            this.extCountsCheck.Name = "extCountsCheck";
            this.extCountsCheck.Size = new System.Drawing.Size(109, 17);
            this.extCountsCheck.TabIndex = 1;
            this.extCountsCheck.Text = "Ext. Pulse Counts";
            this.extCountsCheck.UseVisualStyleBackColor = true;
            this.extCountsCheck.CheckedChanged += new System.EventHandler(this.npmCountsCheck_CheckedChanged);
            // 
            // npmCountsCheck
            // 
            this.npmCountsCheck.AutoSize = true;
            this.npmCountsCheck.Checked = true;
            this.npmCountsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.npmCountsCheck.Location = new System.Drawing.Point(1, 2);
            this.npmCountsCheck.Name = "npmCountsCheck";
            this.npmCountsCheck.Size = new System.Drawing.Size(86, 17);
            this.npmCountsCheck.TabIndex = 0;
            this.npmCountsCheck.Text = "NPM Counts";
            this.npmCountsCheck.UseVisualStyleBackColor = true;
            this.npmCountsCheck.CheckedChanged += new System.EventHandler(this.npmCountsCheck_CheckedChanged);
            // 
            // refMain
            // 
            this.refMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.refMain.Location = new System.Drawing.Point(9, 232);
            this.refMain.Name = "refMain";
            this.refMain.Size = new System.Drawing.Size(86, 20);
            this.refMain.TabIndex = 28;
            this.refMain.Text = "Reset Axes";
            this.refMain.UseVisualStyleBackColor = true;
            this.refMain.Click += new System.EventHandler(this.refMain_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(9, 233);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 20);
            this.button1.TabIndex = 29;
            this.button1.Text = "Reset Axes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.refGen_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(521, 49);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(38, 13);
            this.label28.TabIndex = 30;
            this.label28.Text = "Epoch";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.newEpochRadio);
            this.panel2.Controls.Add(this.oldEpochRadio);
            this.panel2.Location = new System.Drawing.Point(558, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(174, 20);
            this.panel2.TabIndex = 31;
            // 
            // newEpochRadio
            // 
            this.newEpochRadio.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.newEpochRadio.AutoSize = true;
            this.newEpochRadio.Checked = true;
            this.newEpochRadio.Location = new System.Drawing.Point(73, 1);
            this.newEpochRadio.Name = "newEpochRadio";
            this.newEpochRadio.Size = new System.Drawing.Size(71, 17);
            this.newEpochRadio.TabIndex = 1;
            this.newEpochRadio.TabStop = true;
            this.newEpochRadio.Text = "1/1/2000";
            this.newEpochRadio.UseVisualStyleBackColor = true;
            this.newEpochRadio.CheckedChanged += new System.EventHandler(this.newEpochRadio_CheckedChanged);
            // 
            // oldEpochRadio
            // 
            this.oldEpochRadio.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.oldEpochRadio.AutoSize = true;
            this.oldEpochRadio.Location = new System.Drawing.Point(3, 1);
            this.oldEpochRadio.Name = "oldEpochRadio";
            this.oldEpochRadio.Size = new System.Drawing.Size(71, 17);
            this.oldEpochRadio.TabIndex = 0;
            this.oldEpochRadio.Text = "1/1/1970";
            this.oldEpochRadio.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.refMain);
            this.panel4.Controls.Add(this.countsPanel);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.countsPlot);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(458, 262);
            this.panel4.TabIndex = 32;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.panel3);
            this.panel5.Controls.Add(this.label25);
            this.panel5.Controls.Add(this.genPanel);
            this.panel5.Controls.Add(this.genPlot);
            this.panel5.Location = new System.Drawing.Point(3, 271);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(458, 262);
            this.panel5.TabIndex = 33;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 74);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(464, 536);
            this.tableLayoutPanel1.TabIndex = 34;
            // 
            // OfflinePlotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 616);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.advSegBtn);
            this.Controls.Add(this.backSegBtn);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.allSegsBox);
            this.Controls.Add(this.crcErrBox);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.curSegBox);
            this.Controls.Add(this.segGB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numRecordsBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.elapsedTimeBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.stopTimeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.startTimeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fileNameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(722, 655);
            this.Name = "OfflinePlotForm";
            this.Text = "NPM Data Charts - Offline";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Hide);
            this.Load += new System.EventHandler(this.OfflinePlotForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.segGB.ResumeLayout(false);
            this.segGB.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.genPanel.ResumeLayout(false);
            this.genPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.countsPanel.ResumeLayout(false);
            this.countsPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openBINFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDATFileToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fileNameBox;
        private System.Windows.Forms.TextBox startTimeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox stopTimeBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox elapsedTimeBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox numRecordsBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox curSegBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox segGB;
        private System.Windows.Forms.TextBox segStopBox;
        private System.Windows.Forms.TextBox segStartBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox segLogSigBox;
        private System.Windows.Forms.TextBox segLogBattBox;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox segLogRhBox;
        private System.Windows.Forms.TextBox segLogTBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox segLogExtBox;
        private System.Windows.Forms.TextBox segPulseThresh;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox segLogHgmsBox;
        private System.Windows.Forms.TextBox segDiscHighBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox segDiscLowBox;
        private System.Windows.Forms.TextBox segGainBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox segHvBox;
        private System.Windows.Forms.TextBox segNfBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox segRecPerHGMBox;
        private System.Windows.Forms.TextBox segRpBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox segSnBox;
        private System.Windows.Forms.TextBox segNameBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private OxyPlot.WindowsForms.PlotView genPlot;
        private OxyPlot.WindowsForms.PlotView countsPlot;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton dateRadio;
        private System.Windows.Forms.RadioButton recordRadio;
        private System.Windows.Forms.Panel genPanel;
        private System.Windows.Forms.RadioButton signalRadio;
        private System.Windows.Forms.RadioButton voltRadio;
        private System.Windows.Forms.RadioButton rhRadio;
        private System.Windows.Forms.RadioButton tempRadio;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton genRPRadio;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox crcErrBox;
        private System.Windows.Forms.TextBox allSegsBox;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button backSegBtn;
        private System.Windows.Forms.Button advSegBtn;
        private System.Windows.Forms.Panel countsPanel;
        private System.Windows.Forms.CheckBox extCountsCheck;
        private System.Windows.Forms.CheckBox npmCountsCheck;
        private System.Windows.Forms.Button refMain;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton newEpochRadio;
        private System.Windows.Forms.RadioButton oldEpochRadio;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
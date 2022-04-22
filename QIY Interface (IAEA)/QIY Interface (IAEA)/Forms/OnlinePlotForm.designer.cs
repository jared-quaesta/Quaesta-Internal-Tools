
namespace QIY_Interface__IAEA_
{
    partial class OnlinePlotForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OnlinePlotForm));
            this.countsPanel = new System.Windows.Forms.Panel();
            this.extCountsCheck = new System.Windows.Forms.CheckBox();
            this.npmCountsCheck = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.genRPRadio = new System.Windows.Forms.RadioButton();
            this.label25 = new System.Windows.Forms.Label();
            this.genPanel = new System.Windows.Forms.Panel();
            this.signalRadio = new System.Windows.Forms.RadioButton();
            this.voltRadio = new System.Windows.Forms.RadioButton();
            this.rhRadio = new System.Windows.Forms.RadioButton();
            this.tempRadio = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateRadio = new System.Windows.Forms.RadioButton();
            this.recordRadio = new System.Windows.Forms.RadioButton();
            this.countsPlot = new OxyPlot.WindowsForms.PlotView();
            this.genPlot = new OxyPlot.WindowsForms.PlotView();
            this.segGB = new System.Windows.Forms.GroupBox();
            this.queryParamsBtn = new System.Windows.Forms.Button();
            this.saveBinStatus = new System.Windows.Forms.Label();
            this.saveHGMStatus = new System.Windows.Forms.Label();
            this.saveDatStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.countsPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.genPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.segGB.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // countsPanel
            // 
            this.countsPanel.BackColor = System.Drawing.Color.Transparent;
            this.countsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.countsPanel.Controls.Add(this.extCountsCheck);
            this.countsPanel.Controls.Add(this.npmCountsCheck);
            this.countsPanel.Location = new System.Drawing.Point(42, 2);
            this.countsPanel.Name = "countsPanel";
            this.countsPanel.Size = new System.Drawing.Size(187, 20);
            this.countsPanel.TabIndex = 46;
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
            this.extCountsCheck.CheckedChanged += new System.EventHandler(this.extCountsCheck_CheckedChanged);
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
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.radioButton1);
            this.panel3.Controls.Add(this.genRPRadio);
            this.panel3.Location = new System.Drawing.Point(325, 232);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(143, 20);
            this.panel3.TabIndex = 45;
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
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(5, 4);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(37, 13);
            this.label25.TabIndex = 47;
            this.label25.Text = "Show:";
            // 
            // genPanel
            // 
            this.genPanel.BackColor = System.Drawing.Color.Transparent;
            this.genPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.genPanel.Controls.Add(this.signalRadio);
            this.genPanel.Controls.Add(this.voltRadio);
            this.genPanel.Controls.Add(this.rhRadio);
            this.genPanel.Controls.Add(this.tempRadio);
            this.genPanel.Location = new System.Drawing.Point(44, 0);
            this.genPanel.Name = "genPanel";
            this.genPanel.Size = new System.Drawing.Size(353, 20);
            this.genPanel.TabIndex = 44;
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
            this.signalRadio.CheckedChanged += new System.EventHandler(this.genRadioChange);
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
            this.voltRadio.CheckedChanged += new System.EventHandler(this.genRadioChange);
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
            this.rhRadio.CheckedChanged += new System.EventHandler(this.genRadioChange);
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
            this.tempRadio.CheckedChanged += new System.EventHandler(this.genRadioChange);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dateRadio);
            this.panel1.Controls.Add(this.recordRadio);
            this.panel1.Location = new System.Drawing.Point(325, 218);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 20);
            this.panel1.TabIndex = 43;
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
            this.recordRadio.CheckedChanged += new System.EventHandler(this.recordRadio_CheckedChanged);
            // 
            // countsPlot
            // 
            this.countsPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.countsPlot.BackColor = System.Drawing.SystemColors.Control;
            this.countsPlot.Location = new System.Drawing.Point(3, 27);
            this.countsPlot.Name = "countsPlot";
            this.countsPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.countsPlot.Size = new System.Drawing.Size(463, 221);
            this.countsPlot.TabIndex = 42;
            this.countsPlot.Text = "plotView1";
            this.countsPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.countsPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.countsPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // genPlot
            // 
            this.genPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genPlot.BackColor = System.Drawing.SystemColors.Control;
            this.genPlot.Location = new System.Drawing.Point(5, 23);
            this.genPlot.Name = "genPlot";
            this.genPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.genPlot.Size = new System.Drawing.Size(463, 229);
            this.genPlot.TabIndex = 41;
            this.genPlot.Text = "plotView2";
            this.genPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.genPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.genPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // segGB
            // 
            this.segGB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.segGB.Controls.Add(this.queryParamsBtn);
            this.segGB.Controls.Add(this.saveBinStatus);
            this.segGB.Controls.Add(this.saveHGMStatus);
            this.segGB.Controls.Add(this.saveDatStatus);
            this.segGB.Controls.Add(this.label4);
            this.segGB.Controls.Add(this.label3);
            this.segGB.Controls.Add(this.label2);
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
            this.segGB.Location = new System.Drawing.Point(486, 11);
            this.segGB.Name = "segGB";
            this.segGB.Size = new System.Drawing.Size(214, 504);
            this.segGB.TabIndex = 40;
            this.segGB.TabStop = false;
            this.segGB.Text = "Current Params";
            // 
            // queryParamsBtn
            // 
            this.queryParamsBtn.Location = new System.Drawing.Point(5, 479);
            this.queryParamsBtn.Name = "queryParamsBtn";
            this.queryParamsBtn.Size = new System.Drawing.Size(204, 20);
            this.queryParamsBtn.TabIndex = 58;
            this.queryParamsBtn.Text = "Query Params";
            this.queryParamsBtn.UseVisualStyleBackColor = true;
            this.queryParamsBtn.Click += new System.EventHandler(this.queryParamsBtn_Click);
            // 
            // saveBinStatus
            // 
            this.saveBinStatus.Location = new System.Drawing.Point(70, 459);
            this.saveBinStatus.Name = "saveBinStatus";
            this.saveBinStatus.Size = new System.Drawing.Size(139, 13);
            this.saveBinStatus.TabIndex = 57;
            this.saveBinStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // saveHGMStatus
            // 
            this.saveHGMStatus.Location = new System.Drawing.Point(70, 441);
            this.saveHGMStatus.Name = "saveHGMStatus";
            this.saveHGMStatus.Size = new System.Drawing.Size(139, 13);
            this.saveHGMStatus.TabIndex = 56;
            this.saveHGMStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // saveDatStatus
            // 
            this.saveDatStatus.Location = new System.Drawing.Point(70, 423);
            this.saveDatStatus.Name = "saveDatStatus";
            this.saveDatStatus.Size = new System.Drawing.Size(139, 13);
            this.saveDatStatus.TabIndex = 55;
            this.saveDatStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(5, 459);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "SaveBIN:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 441);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "SaveHGM:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 423);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "SaveDAT:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segLogSigBox
            // 
            this.segLogSigBox.Location = new System.Drawing.Point(144, 396);
            this.segLogSigBox.Name = "segLogSigBox";
            this.segLogSigBox.ReadOnly = true;
            this.segLogSigBox.Size = new System.Drawing.Size(66, 20);
            this.segLogSigBox.TabIndex = 51;
            // 
            // segLogBattBox
            // 
            this.segLogBattBox.Location = new System.Drawing.Point(144, 371);
            this.segLogBattBox.Name = "segLogBattBox";
            this.segLogBattBox.ReadOnly = true;
            this.segLogBattBox.Size = new System.Drawing.Size(66, 20);
            this.segLogBattBox.TabIndex = 49;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(5, 399);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(134, 13);
            this.label23.TabIndex = 50;
            this.label23.Text = "Log Signal";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(5, 374);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(134, 13);
            this.label24.TabIndex = 48;
            this.label24.Text = "Log Battery Voltage";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segLogRhBox
            // 
            this.segLogRhBox.Location = new System.Drawing.Point(144, 346);
            this.segLogRhBox.Name = "segLogRhBox";
            this.segLogRhBox.ReadOnly = true;
            this.segLogRhBox.Size = new System.Drawing.Size(66, 20);
            this.segLogRhBox.TabIndex = 47;
            // 
            // segLogTBox
            // 
            this.segLogTBox.Location = new System.Drawing.Point(144, 321);
            this.segLogTBox.Name = "segLogTBox";
            this.segLogTBox.ReadOnly = true;
            this.segLogTBox.Size = new System.Drawing.Size(66, 20);
            this.segLogTBox.TabIndex = 45;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(5, 348);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(134, 13);
            this.label19.TabIndex = 46;
            this.label19.Text = "Log RH%";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(5, 323);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(134, 13);
            this.label20.TabIndex = 44;
            this.label20.Text = "LogT_degC";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segLogExtBox
            // 
            this.segLogExtBox.Location = new System.Drawing.Point(144, 296);
            this.segLogExtBox.Name = "segLogExtBox";
            this.segLogExtBox.ReadOnly = true;
            this.segLogExtBox.Size = new System.Drawing.Size(66, 20);
            this.segLogExtBox.TabIndex = 43;
            // 
            // segPulseThresh
            // 
            this.segPulseThresh.Location = new System.Drawing.Point(144, 270);
            this.segPulseThresh.Name = "segPulseThresh";
            this.segPulseThresh.ReadOnly = true;
            this.segPulseThresh.Size = new System.Drawing.Size(66, 20);
            this.segPulseThresh.TabIndex = 41;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(5, 298);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(134, 13);
            this.label21.TabIndex = 42;
            this.label21.Text = "Log Ext Pulses";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(5, 273);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(134, 13);
            this.label22.TabIndex = 40;
            this.label22.Text = "Pulse Thresh";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segLogHgmsBox
            // 
            this.segLogHgmsBox.Location = new System.Drawing.Point(144, 245);
            this.segLogHgmsBox.Name = "segLogHgmsBox";
            this.segLogHgmsBox.ReadOnly = true;
            this.segLogHgmsBox.Size = new System.Drawing.Size(66, 20);
            this.segLogHgmsBox.TabIndex = 39;
            // 
            // segDiscHighBox
            // 
            this.segDiscHighBox.Location = new System.Drawing.Point(144, 220);
            this.segDiscHighBox.Name = "segDiscHighBox";
            this.segDiscHighBox.ReadOnly = true;
            this.segDiscHighBox.Size = new System.Drawing.Size(66, 20);
            this.segDiscHighBox.TabIndex = 37;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(5, 248);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(134, 13);
            this.label15.TabIndex = 38;
            this.label15.Text = "Log Histograms";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(5, 223);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(134, 13);
            this.label16.TabIndex = 36;
            this.label16.Text = "Upper Discriminator";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segDiscLowBox
            // 
            this.segDiscLowBox.Location = new System.Drawing.Point(144, 195);
            this.segDiscLowBox.Name = "segDiscLowBox";
            this.segDiscLowBox.ReadOnly = true;
            this.segDiscLowBox.Size = new System.Drawing.Size(66, 20);
            this.segDiscLowBox.TabIndex = 35;
            // 
            // segGainBox
            // 
            this.segGainBox.Location = new System.Drawing.Point(144, 170);
            this.segGainBox.Name = "segGainBox";
            this.segGainBox.ReadOnly = true;
            this.segGainBox.Size = new System.Drawing.Size(66, 20);
            this.segGainBox.TabIndex = 33;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(5, 198);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(134, 13);
            this.label17.TabIndex = 34;
            this.label17.Text = "Lower Discriminator";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(5, 172);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(134, 13);
            this.label18.TabIndex = 32;
            this.label18.Text = "Gain";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segHvBox
            // 
            this.segHvBox.Location = new System.Drawing.Point(144, 145);
            this.segHvBox.Name = "segHvBox";
            this.segHvBox.ReadOnly = true;
            this.segHvBox.Size = new System.Drawing.Size(66, 20);
            this.segHvBox.TabIndex = 31;
            // 
            // segNfBox
            // 
            this.segNfBox.Location = new System.Drawing.Point(144, 120);
            this.segNfBox.Name = "segNfBox";
            this.segNfBox.ReadOnly = true;
            this.segNfBox.Size = new System.Drawing.Size(66, 20);
            this.segNfBox.TabIndex = 29;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(5, 147);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(134, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "High Voltage";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(5, 122);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(134, 13);
            this.label14.TabIndex = 28;
            this.label14.Text = "New File Period";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segRecPerHGMBox
            // 
            this.segRecPerHGMBox.Location = new System.Drawing.Point(144, 94);
            this.segRecPerHGMBox.Name = "segRecPerHGMBox";
            this.segRecPerHGMBox.ReadOnly = true;
            this.segRecPerHGMBox.Size = new System.Drawing.Size(66, 20);
            this.segRecPerHGMBox.TabIndex = 27;
            // 
            // segRpBox
            // 
            this.segRpBox.Location = new System.Drawing.Point(144, 69);
            this.segRpBox.Name = "segRpBox";
            this.segRpBox.ReadOnly = true;
            this.segRpBox.Size = new System.Drawing.Size(66, 20);
            this.segRpBox.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(5, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Records Per HGM";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(5, 72);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(134, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Record Period (sec)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // segSnBox
            // 
            this.segSnBox.Location = new System.Drawing.Point(87, 44);
            this.segSnBox.Name = "segSnBox";
            this.segSnBox.ReadOnly = true;
            this.segSnBox.Size = new System.Drawing.Size(122, 20);
            this.segSnBox.TabIndex = 23;
            // 
            // segNameBox
            // 
            this.segNameBox.Location = new System.Drawing.Point(87, 19);
            this.segNameBox.Name = "segNameBox";
            this.segNameBox.ReadOnly = true;
            this.segNameBox.Size = new System.Drawing.Size(122, 20);
            this.segNameBox.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(5, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Serial Number";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(5, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Name";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Show:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.countsPanel);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.countsPlot);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(479, 251);
            this.panel2.TabIndex = 49;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.label25);
            this.panel4.Controls.Add(this.genPanel);
            this.panel4.Controls.Add(this.genPlot);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 260);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(479, 252);
            this.panel4.TabIndex = 50;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(485, 515);
            this.tableLayoutPanel1.TabIndex = 51;
            // 
            // OnlinePlotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 520);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.segGB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(720, 559);
            this.Name = "OnlinePlotForm";
            this.Text = "NPM Data Charts -- Online";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HideForm);
            this.Load += new System.EventHandler(this.OnlinePlotForm_Load);
            this.countsPanel.ResumeLayout(false);
            this.countsPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.genPanel.ResumeLayout(false);
            this.genPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.segGB.ResumeLayout(false);
            this.segGB.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel countsPanel;
        private System.Windows.Forms.CheckBox extCountsCheck;
        private System.Windows.Forms.CheckBox npmCountsCheck;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton genRPRadio;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel genPanel;
        private System.Windows.Forms.RadioButton signalRadio;
        private System.Windows.Forms.RadioButton voltRadio;
        private System.Windows.Forms.RadioButton rhRadio;
        private System.Windows.Forms.RadioButton tempRadio;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton dateRadio;
        private System.Windows.Forms.RadioButton recordRadio;
        private OxyPlot.WindowsForms.PlotView countsPlot;
        private OxyPlot.WindowsForms.PlotView genPlot;
        private System.Windows.Forms.GroupBox segGB;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label saveBinStatus;
        private System.Windows.Forms.Label saveHGMStatus;
        private System.Windows.Forms.Label saveDatStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button queryParamsBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
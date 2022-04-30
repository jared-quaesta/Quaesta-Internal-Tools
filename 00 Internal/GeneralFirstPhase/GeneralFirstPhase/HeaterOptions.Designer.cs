
namespace GeneralFirstPhase
{
    partial class HeaterOptions
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.minBinBox = new System.Windows.Forms.ComboBox();
            this.noiseFloor = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.center = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.centerSpread = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.psValid = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.gainBox = new System.Windows.Forms.ComboBox();
            this.voltOptionsGB = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.validVoltRange = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.voltLevel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timeBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.voltOptionsGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.minBinBox);
            this.groupBox2.Controls.Add(this.noiseFloor);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label34);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(343, 78);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SDEV Noise Test Options";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(116, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Counts/Second";
            // 
            // minBinBox
            // 
            this.minBinBox.FormattingEnabled = true;
            this.minBinBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.minBinBox.Location = new System.Drawing.Point(102, 22);
            this.minBinBox.Name = "minBinBox";
            this.minBinBox.Size = new System.Drawing.Size(49, 21);
            this.minBinBox.TabIndex = 11;
            this.minBinBox.Text = "3";
            // 
            // noiseFloor
            // 
            this.noiseFloor.FormattingEnabled = true;
            this.noiseFloor.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "30"});
            this.noiseFloor.Location = new System.Drawing.Point(67, 49);
            this.noiseFloor.Name = "noiseFloor";
            this.noiseFloor.Size = new System.Drawing.Size(49, 21);
            this.noiseFloor.TabIndex = 27;
            this.noiseFloor.Text = "100";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Noise Floor";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 25);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(95, 13);
            this.label34.TabIndex = 10;
            this.label34.Text = "Maximum Valid Bin";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label52);
            this.groupBox1.Controls.Add(this.label57);
            this.groupBox1.Controls.Add(this.label58);
            this.groupBox1.Controls.Add(this.center);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.centerSpread);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.psValid);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.gainBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 84);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PulseSim Test Options";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(285, 24);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(47, 13);
            this.label52.TabIndex = 31;
            this.label52.Text = "Bins /64";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(131, 24);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(98, 13);
            this.label57.TabIndex = 29;
            this.label57.Text = "Acceptable Spread";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(305, 51);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(24, 13);
            this.label58.TabIndex = 28;
            this.label58.Text = "/64";
            // 
            // center
            // 
            this.center.FormattingEnabled = true;
            this.center.Location = new System.Drawing.Point(254, 48);
            this.center.Name = "center";
            this.center.Size = new System.Drawing.Size(49, 21);
            this.center.TabIndex = 27;
            this.center.Text = "36";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(117, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Bins From Expected Center";
            // 
            // centerSpread
            // 
            this.centerSpread.FormattingEnabled = true;
            this.centerSpread.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.centerSpread.Location = new System.Drawing.Point(67, 48);
            this.centerSpread.Name = "centerSpread";
            this.centerSpread.Size = new System.Drawing.Size(49, 21);
            this.centerSpread.TabIndex = 25;
            this.centerSpread.Text = "3";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Valid Within";
            // 
            // psValid
            // 
            this.psValid.FormattingEnabled = true;
            this.psValid.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.psValid.Location = new System.Drawing.Point(230, 21);
            this.psValid.Name = "psValid";
            this.psValid.Size = new System.Drawing.Size(49, 21);
            this.psValid.TabIndex = 11;
            this.psValid.Text = "3";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 24);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(60, 13);
            this.label26.TabIndex = 1;
            this.label26.Text = "Set Gain to";
            // 
            // gainBox
            // 
            this.gainBox.FormattingEnabled = true;
            this.gainBox.Items.AddRange(new object[] {
            "7",
            "10",
            "25"});
            this.gainBox.Location = new System.Drawing.Point(67, 21);
            this.gainBox.Name = "gainBox";
            this.gainBox.Size = new System.Drawing.Size(49, 21);
            this.gainBox.TabIndex = 0;
            this.gainBox.Text = "7";
            // 
            // voltOptionsGB
            // 
            this.voltOptionsGB.Controls.Add(this.label6);
            this.voltOptionsGB.Controls.Add(this.validVoltRange);
            this.voltOptionsGB.Controls.Add(this.label5);
            this.voltOptionsGB.Location = new System.Drawing.Point(12, 39);
            this.voltOptionsGB.Name = "voltOptionsGB";
            this.voltOptionsGB.Size = new System.Drawing.Size(343, 55);
            this.voltOptionsGB.TabIndex = 20;
            this.voltOptionsGB.TabStop = false;
            this.voltOptionsGB.Text = "Voltage Test Options";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(120, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Volts";
            // 
            // validVoltRange
            // 
            this.validVoltRange.FormattingEnabled = true;
            this.validVoltRange.Items.AddRange(new object[] {
            "10",
            "15",
            "30",
            "100",
            "200"});
            this.validVoltRange.Location = new System.Drawing.Point(70, 22);
            this.validVoltRange.Name = "validVoltRange";
            this.validVoltRange.Size = new System.Drawing.Size(49, 21);
            this.validVoltRange.TabIndex = 4;
            this.validVoltRange.Text = "15";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Valid Within";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(105, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Volts";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Test at";
            // 
            // voltLevel
            // 
            this.voltLevel.FormattingEnabled = true;
            this.voltLevel.Items.AddRange(new object[] {
            "500",
            "750",
            "1000",
            "1500",
            "1750",
            "2000"});
            this.voltLevel.Location = new System.Drawing.Point(55, 12);
            this.voltLevel.Name = "voltLevel";
            this.voltLevel.Size = new System.Drawing.Size(49, 21);
            this.voltLevel.TabIndex = 0;
            this.voltLevel.Text = "500";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(277, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "min";
            // 
            // timeBox
            // 
            this.timeBox.FormattingEnabled = true;
            this.timeBox.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "30",
            "60"});
            this.timeBox.Location = new System.Drawing.Point(227, 12);
            this.timeBox.Name = "timeBox";
            this.timeBox.Size = new System.Drawing.Size(49, 21);
            this.timeBox.TabIndex = 23;
            this.timeBox.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Query Every";
            // 
            // HeaterOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 278);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.voltOptionsGB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.voltLevel);
            this.Controls.Add(this.label3);
            this.Name = "HeaterOptions";
            this.Text = "HeaterOptions";
            this.Load += new System.EventHandler(this.HeaterOptions_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.voltOptionsGB.ResumeLayout(false);
            this.voltOptionsGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox minBinBox;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox psValid;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox gainBox;
        private System.Windows.Forms.GroupBox voltOptionsGB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox validVoltRange;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox voltLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox timeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox noiseFloor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.ComboBox center;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox centerSpread;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label57;
    }
}
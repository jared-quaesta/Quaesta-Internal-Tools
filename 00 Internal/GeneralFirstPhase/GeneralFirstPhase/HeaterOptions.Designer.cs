
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
            this.minBinBox = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.psValid = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
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
            this.groupBox2.Controls.Add(this.minBinBox);
            this.groupBox2.Controls.Add(this.label34);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 57);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SDEV Noise Test Options";
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
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.psValid);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.gainBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 62);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PulseSim Test Options";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(254, 24);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(27, 13);
            this.label18.TabIndex = 12;
            this.label18.Text = "Bins";
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
            this.psValid.Location = new System.Drawing.Point(204, 21);
            this.psValid.Name = "psValid";
            this.psValid.Size = new System.Drawing.Size(49, 21);
            this.psValid.TabIndex = 11;
            this.psValid.Text = "3";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(140, 24);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(63, 13);
            this.label19.TabIndex = 10;
            this.label19.Text = "Valid Within";
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
            this.voltOptionsGB.Size = new System.Drawing.Size(291, 55);
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
            this.ClientSize = new System.Drawing.Size(320, 239);
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
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox psValid;
        private System.Windows.Forms.Label label19;
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
    }
}
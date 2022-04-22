
namespace QIY_Torture_Test
{
    partial class CreateTest
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.noCycleBtn = new System.Windows.Forms.RadioButton();
            this.ethOffBtn = new System.Windows.Forms.RadioButton();
            this.powerOffBtn = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.uptimeCount = new System.Windows.Forms.TextBox();
            this.subUptime = new System.Windows.Forms.Button();
            this.addUptime = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.addVoltage = new System.Windows.Forms.Button();
            this.subVoltage = new System.Windows.Forms.Button();
            this.voltageCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.addInfo = new System.Windows.Forms.Button();
            this.subInfo = new System.Windows.Forms.Button();
            this.infoCount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.addTime = new System.Windows.Forms.Button();
            this.subTime = new System.Windows.Forms.Button();
            this.timeCount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.addPing = new System.Windows.Forms.Button();
            this.subPing = new System.Windows.Forms.Button();
            this.pingCount = new System.Windows.Forms.TextBox();
            this.saveDatCheck = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.saveHgmCheck = new System.Windows.Forms.CheckBox();
            this.saveBinCheck = new System.Windows.Forms.CheckBox();
            this.finishButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.intervalSetting = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.connectCheck = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.noCycleBtn);
            this.panel1.Controls.Add(this.ethOffBtn);
            this.panel1.Controls.Add(this.powerOffBtn);
            this.panel1.Location = new System.Drawing.Point(27, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(101, 76);
            this.panel1.TabIndex = 0;
            // 
            // noCycleBtn
            // 
            this.noCycleBtn.AutoSize = true;
            this.noCycleBtn.Location = new System.Drawing.Point(4, 50);
            this.noCycleBtn.Name = "noCycleBtn";
            this.noCycleBtn.Size = new System.Drawing.Size(88, 17);
            this.noCycleBtn.TabIndex = 2;
            this.noCycleBtn.Text = "Do Not Cycle";
            this.noCycleBtn.UseVisualStyleBackColor = true;
            // 
            // ethOffBtn
            // 
            this.ethOffBtn.AutoSize = true;
            this.ethOffBtn.Location = new System.Drawing.Point(4, 27);
            this.ethOffBtn.Name = "ethOffBtn";
            this.ethOffBtn.Size = new System.Drawing.Size(82, 17);
            this.ethOffBtn.TabIndex = 1;
            this.ethOffBtn.Text = "Ethernet Off";
            this.ethOffBtn.UseVisualStyleBackColor = true;
            // 
            // powerOffBtn
            // 
            this.powerOffBtn.AutoSize = true;
            this.powerOffBtn.Checked = true;
            this.powerOffBtn.Location = new System.Drawing.Point(4, 4);
            this.powerOffBtn.Name = "powerOffBtn";
            this.powerOffBtn.Size = new System.Drawing.Size(91, 17);
            this.powerOffBtn.TabIndex = 0;
            this.powerOffBtn.TabStop = true;
            this.powerOffBtn.Text = "Full Power Off";
            this.powerOffBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Power Cycle:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Actions:";
            // 
            // uptimeCount
            // 
            this.uptimeCount.Location = new System.Drawing.Point(48, 149);
            this.uptimeCount.Name = "uptimeCount";
            this.uptimeCount.Size = new System.Drawing.Size(30, 20);
            this.uptimeCount.TabIndex = 4;
            this.uptimeCount.Text = "0";
            // 
            // subUptime
            // 
            this.subUptime.Location = new System.Drawing.Point(28, 149);
            this.subUptime.Name = "subUptime";
            this.subUptime.Size = new System.Drawing.Size(20, 20);
            this.subUptime.TabIndex = 5;
            this.subUptime.Text = "-";
            this.subUptime.UseVisualStyleBackColor = true;
            this.subUptime.Click += new System.EventHandler(this.subUptime_Click);
            // 
            // addUptime
            // 
            this.addUptime.Location = new System.Drawing.Point(78, 149);
            this.addUptime.Name = "addUptime";
            this.addUptime.Size = new System.Drawing.Size(20, 20);
            this.addUptime.TabIndex = 6;
            this.addUptime.Text = "+";
            this.addUptime.UseVisualStyleBackColor = true;
            this.addUptime.Click += new System.EventHandler(this.addUptime_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Uptime";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Voltage";
            // 
            // addVoltage
            // 
            this.addVoltage.Location = new System.Drawing.Point(78, 175);
            this.addVoltage.Name = "addVoltage";
            this.addVoltage.Size = new System.Drawing.Size(20, 20);
            this.addVoltage.TabIndex = 10;
            this.addVoltage.Text = "+";
            this.addVoltage.UseVisualStyleBackColor = true;
            this.addVoltage.Click += new System.EventHandler(this.addVoltage_Click);
            // 
            // subVoltage
            // 
            this.subVoltage.Location = new System.Drawing.Point(28, 175);
            this.subVoltage.Name = "subVoltage";
            this.subVoltage.Size = new System.Drawing.Size(20, 20);
            this.subVoltage.TabIndex = 9;
            this.subVoltage.Text = "-";
            this.subVoltage.UseVisualStyleBackColor = true;
            this.subVoltage.Click += new System.EventHandler(this.subVoltage_Click);
            // 
            // voltageCount
            // 
            this.voltageCount.Location = new System.Drawing.Point(48, 175);
            this.voltageCount.Name = "voltageCount";
            this.voltageCount.Size = new System.Drawing.Size(30, 20);
            this.voltageCount.TabIndex = 8;
            this.voltageCount.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(104, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Info";
            // 
            // addInfo
            // 
            this.addInfo.Location = new System.Drawing.Point(78, 201);
            this.addInfo.Name = "addInfo";
            this.addInfo.Size = new System.Drawing.Size(20, 20);
            this.addInfo.TabIndex = 14;
            this.addInfo.Text = "+";
            this.addInfo.UseVisualStyleBackColor = true;
            this.addInfo.Click += new System.EventHandler(this.addInfo_Click);
            // 
            // subInfo
            // 
            this.subInfo.Location = new System.Drawing.Point(28, 201);
            this.subInfo.Name = "subInfo";
            this.subInfo.Size = new System.Drawing.Size(20, 20);
            this.subInfo.TabIndex = 13;
            this.subInfo.Text = "-";
            this.subInfo.UseVisualStyleBackColor = true;
            this.subInfo.Click += new System.EventHandler(this.subInfo_Click);
            // 
            // infoCount
            // 
            this.infoCount.Location = new System.Drawing.Point(48, 201);
            this.infoCount.Name = "infoCount";
            this.infoCount.Size = new System.Drawing.Size(30, 20);
            this.infoCount.TabIndex = 12;
            this.infoCount.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(104, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Time";
            // 
            // addTime
            // 
            this.addTime.Location = new System.Drawing.Point(78, 227);
            this.addTime.Name = "addTime";
            this.addTime.Size = new System.Drawing.Size(20, 20);
            this.addTime.TabIndex = 18;
            this.addTime.Text = "+";
            this.addTime.UseVisualStyleBackColor = true;
            this.addTime.Click += new System.EventHandler(this.addTime_Click);
            // 
            // subTime
            // 
            this.subTime.Location = new System.Drawing.Point(28, 227);
            this.subTime.Name = "subTime";
            this.subTime.Size = new System.Drawing.Size(20, 20);
            this.subTime.TabIndex = 17;
            this.subTime.Text = "-";
            this.subTime.UseVisualStyleBackColor = true;
            this.subTime.Click += new System.EventHandler(this.subTime_Click);
            // 
            // timeCount
            // 
            this.timeCount.Location = new System.Drawing.Point(48, 227);
            this.timeCount.Name = "timeCount";
            this.timeCount.Size = new System.Drawing.Size(30, 20);
            this.timeCount.TabIndex = 16;
            this.timeCount.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(104, 256);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Ping";
            // 
            // addPing
            // 
            this.addPing.Location = new System.Drawing.Point(78, 253);
            this.addPing.Name = "addPing";
            this.addPing.Size = new System.Drawing.Size(20, 20);
            this.addPing.TabIndex = 22;
            this.addPing.Text = "+";
            this.addPing.UseVisualStyleBackColor = true;
            this.addPing.Click += new System.EventHandler(this.addPing_Click);
            // 
            // subPing
            // 
            this.subPing.Location = new System.Drawing.Point(28, 253);
            this.subPing.Name = "subPing";
            this.subPing.Size = new System.Drawing.Size(20, 20);
            this.subPing.TabIndex = 21;
            this.subPing.Text = "-";
            this.subPing.UseVisualStyleBackColor = true;
            this.subPing.Click += new System.EventHandler(this.subPing_Click);
            // 
            // pingCount
            // 
            this.pingCount.Location = new System.Drawing.Point(48, 253);
            this.pingCount.Name = "pingCount";
            this.pingCount.Size = new System.Drawing.Size(30, 20);
            this.pingCount.TabIndex = 20;
            this.pingCount.Text = "0";
            // 
            // saveDatCheck
            // 
            this.saveDatCheck.AutoSize = true;
            this.saveDatCheck.Location = new System.Drawing.Point(175, 33);
            this.saveDatCheck.Name = "saveDatCheck";
            this.saveDatCheck.Size = new System.Drawing.Size(68, 17);
            this.saveDatCheck.TabIndex = 24;
            this.saveDatCheck.Text = "SaveDat";
            this.saveDatCheck.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(153, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "NPM Parameters:";
            // 
            // saveHgmCheck
            // 
            this.saveHgmCheck.AutoSize = true;
            this.saveHgmCheck.Location = new System.Drawing.Point(175, 56);
            this.saveHgmCheck.Name = "saveHgmCheck";
            this.saveHgmCheck.Size = new System.Drawing.Size(73, 17);
            this.saveHgmCheck.TabIndex = 26;
            this.saveHgmCheck.Text = "SaveHgm";
            this.saveHgmCheck.UseVisualStyleBackColor = true;
            // 
            // saveBinCheck
            // 
            this.saveBinCheck.AutoSize = true;
            this.saveBinCheck.Location = new System.Drawing.Point(175, 79);
            this.saveBinCheck.Name = "saveBinCheck";
            this.saveBinCheck.Size = new System.Drawing.Size(66, 17);
            this.saveBinCheck.TabIndex = 29;
            this.saveBinCheck.Text = "SaveBin";
            this.saveBinCheck.UseVisualStyleBackColor = true;
            // 
            // finishButton
            // 
            this.finishButton.ForeColor = System.Drawing.Color.Green;
            this.finishButton.Location = new System.Drawing.Point(27, 279);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(227, 38);
            this.finishButton.TabIndex = 30;
            this.finishButton.Text = "Finish";
            this.finishButton.UseVisualStyleBackColor = true;
            this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(200, 155);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "h";
            // 
            // intervalSetting
            // 
            this.intervalSetting.Location = new System.Drawing.Point(176, 152);
            this.intervalSetting.Name = "intervalSetting";
            this.intervalSetting.Size = new System.Drawing.Size(24, 20);
            this.intervalSetting.TabIndex = 32;
            this.intervalSetting.Text = "12";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(154, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "Stage Time:";
            // 
            // connectCheck
            // 
            this.connectCheck.AutoSize = true;
            this.connectCheck.Checked = true;
            this.connectCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.connectCheck.Location = new System.Drawing.Point(30, 102);
            this.connectCheck.Name = "connectCheck";
            this.connectCheck.Size = new System.Drawing.Size(66, 17);
            this.connectCheck.TabIndex = 34;
            this.connectCheck.Text = "Connect";
            this.connectCheck.UseVisualStyleBackColor = true;
            // 
            // CreateTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 326);
            this.Controls.Add(this.connectCheck);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.intervalSetting);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.finishButton);
            this.Controls.Add(this.saveBinCheck);
            this.Controls.Add(this.saveHgmCheck);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.saveDatCheck);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.addPing);
            this.Controls.Add(this.subPing);
            this.Controls.Add(this.pingCount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.addTime);
            this.Controls.Add(this.subTime);
            this.Controls.Add(this.timeCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.addInfo);
            this.Controls.Add(this.subInfo);
            this.Controls.Add(this.infoCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.addVoltage);
            this.Controls.Add(this.subVoltage);
            this.Controls.Add(this.voltageCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.addUptime);
            this.Controls.Add(this.subUptime);
            this.Controls.Add(this.uptimeCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "CreateTest";
            this.Text = "New Test";
            this.Load += new System.EventHandler(this.CreateTest_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton noCycleBtn;
        private System.Windows.Forms.RadioButton ethOffBtn;
        private System.Windows.Forms.RadioButton powerOffBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox uptimeCount;
        private System.Windows.Forms.Button subUptime;
        private System.Windows.Forms.Button addUptime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button addVoltage;
        private System.Windows.Forms.Button subVoltage;
        private System.Windows.Forms.TextBox voltageCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button addInfo;
        private System.Windows.Forms.Button subInfo;
        private System.Windows.Forms.TextBox infoCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button addTime;
        private System.Windows.Forms.Button subTime;
        private System.Windows.Forms.TextBox timeCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button addPing;
        private System.Windows.Forms.Button subPing;
        private System.Windows.Forms.TextBox pingCount;
        private System.Windows.Forms.CheckBox saveDatCheck;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox saveHgmCheck;
        private System.Windows.Forms.CheckBox saveBinCheck;
        private System.Windows.Forms.Button finishButton;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.TextBox intervalSetting;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox connectCheck;
    }
}
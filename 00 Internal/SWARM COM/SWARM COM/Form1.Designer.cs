
using System;
using System.ComponentModel;

namespace SWARM_COM
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
            this.outBox = new System.Windows.Forms.TextBox();
            this.sendCommandBox = new System.Windows.Forms.TextBox();
            this.tcpButton = new System.Windows.Forms.Button();
            this.rssiButton = new System.Windows.Forms.Button();
            this.ipConfig = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkOutWorker = new System.ComponentModel.BackgroundWorker();
            this.rrsiLabel = new System.Windows.Forms.Label();
            this.runTestButton = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.lastMsg = new System.Windows.Forms.Label();
            this.runTestWorker = new System.ComponentModel.BackgroundWorker();
            this.nextTimeLbl = new System.Windows.Forms.Label();
            this.mtCheck = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // outBox
            // 
            this.outBox.Location = new System.Drawing.Point(12, 12);
            this.outBox.Multiline = true;
            this.outBox.Name = "outBox";
            this.outBox.ReadOnly = true;
            this.outBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outBox.Size = new System.Drawing.Size(415, 594);
            this.outBox.TabIndex = 0;
            // 
            // sendCommandBox
            // 
            this.sendCommandBox.Location = new System.Drawing.Point(12, 613);
            this.sendCommandBox.Name = "sendCommandBox";
            this.sendCommandBox.Size = new System.Drawing.Size(415, 23);
            this.sendCommandBox.TabIndex = 1;
            this.sendCommandBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendMessage);
            // 
            // tcpButton
            // 
            this.tcpButton.Location = new System.Drawing.Point(434, 13);
            this.tcpButton.Name = "tcpButton";
            this.tcpButton.Size = new System.Drawing.Size(159, 23);
            this.tcpButton.TabIndex = 2;
            this.tcpButton.Text = "Query TCP Connection";
            this.tcpButton.UseVisualStyleBackColor = true;
            this.tcpButton.Click += new System.EventHandler(this.tcpButton_Click);
            // 
            // rssiButton
            // 
            this.rssiButton.Location = new System.Drawing.Point(434, 43);
            this.rssiButton.Name = "rssiButton";
            this.rssiButton.Size = new System.Drawing.Size(159, 23);
            this.rssiButton.TabIndex = 3;
            this.rssiButton.Text = "Query RSSI";
            this.rssiButton.UseVisualStyleBackColor = true;
            this.rssiButton.Click += new System.EventHandler(this.rssiButton_Click);
            // 
            // ipConfig
            // 
            this.ipConfig.Location = new System.Drawing.Point(456, 72);
            this.ipConfig.Name = "ipConfig";
            this.ipConfig.Size = new System.Drawing.Size(137, 23);
            this.ipConfig.TabIndex = 4;
            this.ipConfig.Text = "192.168.15.131";
            this.ipConfig.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NewIp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(434, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP";
            // 
            // checkOutWorker
            // 
            this.checkOutWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CheckOutput);
            this.checkOutWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.UpdateUI);
            // 
            // rrsiLabel
            // 
            this.rrsiLabel.AutoSize = true;
            this.rrsiLabel.Location = new System.Drawing.Point(434, 111);
            this.rrsiLabel.Name = "rrsiLabel";
            this.rrsiLabel.Size = new System.Drawing.Size(36, 15);
            this.rrsiLabel.TabIndex = 6;
            this.rrsiLabel.Text = "RRSI: ";
            // 
            // runTestButton
            // 
            this.runTestButton.Location = new System.Drawing.Point(12, 642);
            this.runTestButton.Name = "runTestButton";
            this.runTestButton.Size = new System.Drawing.Size(75, 23);
            this.runTestButton.TabIndex = 7;
            this.runTestButton.Text = "Run Test";
            this.runTestButton.UseVisualStyleBackColor = true;
            this.runTestButton.Click += new System.EventHandler(this.runTestButton_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(12, 668);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(80, 15);
            this.lbl.TabIndex = 8;
            this.lbl.Text = "Last Message:";
            // 
            // lastMsg
            // 
            this.lastMsg.Location = new System.Drawing.Point(93, 642);
            this.lastMsg.Name = "lastMsg";
            this.lastMsg.Size = new System.Drawing.Size(334, 65);
            this.lastMsg.TabIndex = 9;
            // 
            // runTestWorker
            // 
            this.runTestWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WaitBetween);
            this.runTestWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ExcecuteTest);
            // 
            // nextTimeLbl
            // 
            this.nextTimeLbl.AutoSize = true;
            this.nextTimeLbl.Location = new System.Drawing.Point(12, 683);
            this.nextTimeLbl.Name = "nextTimeLbl";
            this.nextTimeLbl.Size = new System.Drawing.Size(0, 15);
            this.nextTimeLbl.TabIndex = 10;
            this.nextTimeLbl.Click += new System.EventHandler(this.label2_Click);
            // 
            // mtCheck
            // 
            this.mtCheck.Location = new System.Drawing.Point(434, 141);
            this.mtCheck.Name = "mtCheck";
            this.mtCheck.Size = new System.Drawing.Size(159, 23);
            this.mtCheck.TabIndex = 11;
            this.mtCheck.Text = "Query MT Amount";
            this.mtCheck.UseVisualStyleBackColor = true;
            this.mtCheck.Click += new System.EventHandler(this.mtCheck_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 716);
            this.Controls.Add(this.mtCheck);
            this.Controls.Add(this.nextTimeLbl);
            this.Controls.Add(this.lastMsg);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.runTestButton);
            this.Controls.Add(this.rrsiLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ipConfig);
            this.Controls.Add(this.rssiButton);
            this.Controls.Add(this.tcpButton);
            this.Controls.Add(this.sendCommandBox);
            this.Controls.Add(this.outBox);
            this.Name = "MainForm";
            this.Text = "SWARM TelNet Com";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.TextBox outBox;
        private System.Windows.Forms.TextBox sendCommandBox;
        private System.Windows.Forms.Button tcpButton;
        private System.Windows.Forms.Button rssiButton;
        private System.Windows.Forms.TextBox ipConfig;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker checkOutWorker;
        private System.Windows.Forms.Label rrsiLabel;
        private System.Windows.Forms.Button runTestButton;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lastMsg;
        private BackgroundWorker runTestWorker;
        private System.Windows.Forms.Label nextTimeLbl;
        private System.Windows.Forms.Button mtCheck;
    }
}


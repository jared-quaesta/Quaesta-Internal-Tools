
namespace NPM_General_App
{
    partial class AdvancedTerminalTCP
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
            this.advTermOut = new System.Windows.Forms.RichTextBox();
            this.advTermIn = new System.Windows.Forms.RichTextBox();
            this.crBttn = new System.Windows.Forms.Button();
            this.lfBttn = new System.Windows.Forms.Button();
            this.flushOut = new System.Windows.Forms.Button();
            this.flushIn = new System.Windows.Forms.Button();
            this.connectionBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.commandsList = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.runNTimes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.byteDelayms = new System.Windows.Forms.TextBox();
            this.lineDelayms = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.runCmds = new System.Windows.Forms.Button();
            this.crRadio = new System.Windows.Forms.RadioButton();
            this.crlfRadio = new System.Windows.Forms.RadioButton();
            this.lfRadio = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.npmIP = new System.Windows.Forms.Label();
            this.debugWorker = new System.ComponentModel.BackgroundWorker();
            this.displayCRLFCheck = new System.Windows.Forms.CheckBox();
            this.responseTimeoutBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.timeoutPlot = new OxyPlot.WindowsForms.PlotView();
            this.expandChartBtn = new System.Windows.Forms.Button();
            this.byteAtATimeCheck = new System.Windows.Forms.CheckBox();
            this.rcCheck = new System.Windows.Forms.CheckBox();
            this.rcWait = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.retriesAllowed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // advTermOut
            // 
            this.advTermOut.Location = new System.Drawing.Point(8, 8);
            this.advTermOut.Name = "advTermOut";
            this.advTermOut.Size = new System.Drawing.Size(621, 528);
            this.advTermOut.TabIndex = 0;
            this.advTermOut.Text = "";
            // 
            // advTermIn
            // 
            this.advTermIn.Location = new System.Drawing.Point(8, 538);
            this.advTermIn.Name = "advTermIn";
            this.advTermIn.Size = new System.Drawing.Size(621, 24);
            this.advTermIn.TabIndex = 1;
            this.advTermIn.Text = "";
            this.advTermIn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendCommand);
            // 
            // crBttn
            // 
            this.crBttn.Location = new System.Drawing.Point(635, 523);
            this.crBttn.Name = "crBttn";
            this.crBttn.Size = new System.Drawing.Size(53, 39);
            this.crBttn.TabIndex = 2;
            this.crBttn.Text = "CR";
            this.crBttn.UseVisualStyleBackColor = true;
            this.crBttn.Click += new System.EventHandler(this.crBttn_Click);
            // 
            // lfBttn
            // 
            this.lfBttn.Location = new System.Drawing.Point(691, 523);
            this.lfBttn.Name = "lfBttn";
            this.lfBttn.Size = new System.Drawing.Size(53, 39);
            this.lfBttn.TabIndex = 3;
            this.lfBttn.Text = "LF";
            this.lfBttn.UseVisualStyleBackColor = true;
            this.lfBttn.Click += new System.EventHandler(this.lfBttn_Click);
            // 
            // flushOut
            // 
            this.flushOut.Location = new System.Drawing.Point(750, 523);
            this.flushOut.Name = "flushOut";
            this.flushOut.Size = new System.Drawing.Size(112, 39);
            this.flushOut.TabIndex = 4;
            this.flushOut.Text = "Flush Out Buffer";
            this.flushOut.UseVisualStyleBackColor = true;
            // 
            // flushIn
            // 
            this.flushIn.Location = new System.Drawing.Point(750, 478);
            this.flushIn.Name = "flushIn";
            this.flushIn.Size = new System.Drawing.Size(112, 39);
            this.flushIn.TabIndex = 5;
            this.flushIn.Text = "Flush In Buffer";
            this.flushIn.UseVisualStyleBackColor = true;
            this.flushIn.Click += new System.EventHandler(this.flushIn_Click);
            // 
            // connectionBtn
            // 
            this.connectionBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.connectionBtn.ForeColor = System.Drawing.Color.Red;
            this.connectionBtn.Location = new System.Drawing.Point(868, 478);
            this.connectionBtn.Name = "connectionBtn";
            this.connectionBtn.Size = new System.Drawing.Size(128, 84);
            this.connectionBtn.TabIndex = 7;
            this.connectionBtn.Text = "Disconnected";
            this.connectionBtn.UseVisualStyleBackColor = true;
            this.connectionBtn.Click += new System.EventHandler(this.connectionBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(635, 478);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(109, 39);
            this.clearBtn.TabIndex = 8;
            this.clearBtn.Text = "Clear Output";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // commandsList
            // 
            this.commandsList.Location = new System.Drawing.Point(635, 56);
            this.commandsList.Multiline = true;
            this.commandsList.Name = "commandsList";
            this.commandsList.Size = new System.Drawing.Size(109, 190);
            this.commandsList.TabIndex = 9;
            this.commandsList.TextChanged += new System.EventHandler(this.UpdateCommands);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(635, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Commands:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(751, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Run N Times:";
            // 
            // runNTimes
            // 
            this.runNTimes.Location = new System.Drawing.Point(780, 55);
            this.runNTimes.Name = "runNTimes";
            this.runNTimes.Size = new System.Drawing.Size(48, 23);
            this.runNTimes.TabIndex = 12;
            this.runNTimes.Text = "100";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(750, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "N =";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(749, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Byte Delay:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(800, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "ms";
            // 
            // byteDelayms
            // 
            this.byteDelayms.Location = new System.Drawing.Point(749, 223);
            this.byteDelayms.Name = "byteDelayms";
            this.byteDelayms.Size = new System.Drawing.Size(50, 23);
            this.byteDelayms.TabIndex = 16;
            this.byteDelayms.Text = "0";
            // 
            // lineDelayms
            // 
            this.lineDelayms.Location = new System.Drawing.Point(749, 175);
            this.lineDelayms.Name = "lineDelayms";
            this.lineDelayms.Size = new System.Drawing.Size(50, 23);
            this.lineDelayms.TabIndex = 19;
            this.lineDelayms.Text = "20";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(800, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 15);
            this.label7.TabIndex = 18;
            this.label7.Text = "ms";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(749, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "Line Delay:";
            // 
            // runCmds
            // 
            this.runCmds.ForeColor = System.Drawing.Color.DarkBlue;
            this.runCmds.Location = new System.Drawing.Point(840, 192);
            this.runCmds.Name = "runCmds";
            this.runCmds.Size = new System.Drawing.Size(156, 54);
            this.runCmds.TabIndex = 20;
            this.runCmds.Text = "Run Debug Commands";
            this.runCmds.UseVisualStyleBackColor = true;
            this.runCmds.Click += new System.EventHandler(this.runCmds_Click);
            // 
            // crRadio
            // 
            this.crRadio.AutoSize = true;
            this.crRadio.Location = new System.Drawing.Point(872, 53);
            this.crRadio.Name = "crRadio";
            this.crRadio.Size = new System.Drawing.Size(40, 19);
            this.crRadio.TabIndex = 21;
            this.crRadio.Text = "CR";
            this.crRadio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.crRadio.UseVisualStyleBackColor = true;
            // 
            // crlfRadio
            // 
            this.crlfRadio.AutoSize = true;
            this.crlfRadio.Checked = true;
            this.crlfRadio.Location = new System.Drawing.Point(872, 86);
            this.crlfRadio.Name = "crlfRadio";
            this.crlfRadio.Size = new System.Drawing.Size(66, 19);
            this.crlfRadio.TabIndex = 22;
            this.crlfRadio.TabStop = true;
            this.crlfRadio.Text = "CR + LF";
            this.crlfRadio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.crlfRadio.UseVisualStyleBackColor = true;
            // 
            // lfRadio
            // 
            this.lfRadio.AutoSize = true;
            this.lfRadio.Location = new System.Drawing.Point(872, 69);
            this.lfRadio.Name = "lfRadio";
            this.lfRadio.Size = new System.Drawing.Size(37, 19);
            this.lfRadio.TabIndex = 23;
            this.lfRadio.Text = "LF";
            this.lfRadio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lfRadio.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(865, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 15);
            this.label9.TabIndex = 24;
            this.label9.Text = "Send With:";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(751, 89);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 47);
            this.label10.TabIndex = 26;
            this.label10.Text = "Wait For Response:";
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Location = new System.Drawing.Point(635, 249);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(361, 2);
            this.label11.TabIndex = 27;
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Location = new System.Drawing.Point(635, 32);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(361, 2);
            this.label12.TabIndex = 28;
            // 
            // npmIP
            // 
            this.npmIP.AutoSize = true;
            this.npmIP.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.npmIP.Location = new System.Drawing.Point(635, 6);
            this.npmIP.Name = "npmIP";
            this.npmIP.Size = new System.Drawing.Size(112, 21);
            this.npmIP.TabIndex = 29;
            this.npmIP.Text = "192.168.15.33";
            // 
            // debugWorker
            // 
            this.debugWorker.WorkerSupportsCancellation = true;
            this.debugWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.RunDebugCommandsAsync);
            // 
            // displayCRLFCheck
            // 
            this.displayCRLFCheck.AutoSize = true;
            this.displayCRLFCheck.Checked = true;
            this.displayCRLFCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayCRLFCheck.Location = new System.Drawing.Point(635, 453);
            this.displayCRLFCheck.Name = "displayCRLFCheck";
            this.displayCRLFCheck.Size = new System.Drawing.Size(187, 19);
            this.displayCRLFCheck.TabIndex = 30;
            this.displayCRLFCheck.Text = "Display CR+LF Bytes in Output";
            this.displayCRLFCheck.UseVisualStyleBackColor = true;
            // 
            // responseTimeoutBox
            // 
            this.responseTimeoutBox.Location = new System.Drawing.Point(751, 120);
            this.responseTimeoutBox.Name = "responseTimeoutBox";
            this.responseTimeoutBox.Size = new System.Drawing.Size(50, 23);
            this.responseTimeoutBox.TabIndex = 32;
            this.responseTimeoutBox.Text = "300";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(801, 127);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 15);
            this.label13.TabIndex = 31;
            this.label13.Text = "ms";
            // 
            // timeoutPlot
            // 
            this.timeoutPlot.BackColor = System.Drawing.SystemColors.Control;
            this.timeoutPlot.Location = new System.Drawing.Point(635, 273);
            this.timeoutPlot.Name = "timeoutPlot";
            this.timeoutPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.timeoutPlot.Size = new System.Drawing.Size(356, 189);
            this.timeoutPlot.TabIndex = 33;
            this.timeoutPlot.Text = "plotView1";
            this.timeoutPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.timeoutPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.timeoutPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // expandChartBtn
            // 
            this.expandChartBtn.Location = new System.Drawing.Point(635, 254);
            this.expandChartBtn.Name = "expandChartBtn";
            this.expandChartBtn.Size = new System.Drawing.Size(361, 23);
            this.expandChartBtn.TabIndex = 34;
            this.expandChartBtn.Text = "Expand Chart";
            this.expandChartBtn.UseVisualStyleBackColor = true;
            // 
            // byteAtATimeCheck
            // 
            this.byteAtATimeCheck.AutoSize = true;
            this.byteAtATimeCheck.Location = new System.Drawing.Point(828, 453);
            this.byteAtATimeCheck.Name = "byteAtATimeCheck";
            this.byteAtATimeCheck.Size = new System.Drawing.Size(138, 19);
            this.byteAtATimeCheck.TabIndex = 36;
            this.byteAtATimeCheck.Text = "Byte At A Time Mode";
            this.byteAtATimeCheck.UseVisualStyleBackColor = true;
            this.byteAtATimeCheck.CheckedChanged += new System.EventHandler(this.byteAtATimeCheck_CheckedChanged);
            // 
            // rcCheck
            // 
            this.rcCheck.AutoSize = true;
            this.rcCheck.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.rcCheck.Location = new System.Drawing.Point(847, 111);
            this.rcCheck.Name = "rcCheck";
            this.rcCheck.Size = new System.Drawing.Size(123, 19);
            this.rcCheck.TabIndex = 37;
            this.rcCheck.Text = "Rapid Reconnect:";
            this.rcCheck.UseVisualStyleBackColor = true;
            // 
            // rcWait
            // 
            this.rcWait.Location = new System.Drawing.Point(847, 130);
            this.rcWait.Name = "rcWait";
            this.rcWait.Size = new System.Drawing.Size(50, 23);
            this.rcWait.TabIndex = 39;
            this.rcWait.Text = "20";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(898, 138);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 15);
            this.label14.TabIndex = 38;
            this.label14.Text = "ms";
            // 
            // retriesAllowed
            // 
            this.retriesAllowed.Location = new System.Drawing.Point(847, 161);
            this.retriesAllowed.Name = "retriesAllowed";
            this.retriesAllowed.Size = new System.Drawing.Size(50, 23);
            this.retriesAllowed.TabIndex = 41;
            this.retriesAllowed.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(898, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 40;
            this.label1.Text = "Retries Allowed";
            // 
            // AdvancedTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 565);
            this.Controls.Add(this.retriesAllowed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.byteAtATimeCheck);
            this.Controls.Add(this.displayCRLFCheck);
            this.Controls.Add(this.expandChartBtn);
            this.Controls.Add(this.rcWait);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.rcCheck);
            this.Controls.Add(this.timeoutPlot);
            this.Controls.Add(this.responseTimeoutBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.npmIP);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lfRadio);
            this.Controls.Add(this.crlfRadio);
            this.Controls.Add(this.crRadio);
            this.Controls.Add(this.runCmds);
            this.Controls.Add(this.lineDelayms);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.byteDelayms);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.runNTimes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.commandsList);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.connectionBtn);
            this.Controls.Add(this.flushIn);
            this.Controls.Add(this.flushOut);
            this.Controls.Add(this.lfBttn);
            this.Controls.Add(this.crBttn);
            this.Controls.Add(this.advTermIn);
            this.Controls.Add(this.advTermOut);
            this.Name = "AdvancedTerminal";
            this.Text = "AdvancedTerminal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HideForm);
            this.Load += new System.EventHandler(this.AdvancedTerminal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox advTermOut;
        private System.Windows.Forms.RichTextBox advTermIn;
        private System.Windows.Forms.Button crBttn;
        private System.Windows.Forms.Button lfBttn;
        private System.Windows.Forms.Button flushOut;
        private System.Windows.Forms.Button flushIn;
        private System.Windows.Forms.Button connectionBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.TextBox commandsList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox runNTimes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox byteDelayms;
        private System.Windows.Forms.TextBox lineDelayms;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button runCmds;
        private System.Windows.Forms.RadioButton crRadio;
        private System.Windows.Forms.RadioButton crlfRadio;
        private System.Windows.Forms.RadioButton lfRadio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label npmIP;
        private System.ComponentModel.BackgroundWorker debugWorker;
        private System.Windows.Forms.CheckBox displayCRLFCheck;
        private System.Windows.Forms.TextBox responseTimeoutBox;
        private System.Windows.Forms.Label label13;
        private OxyPlot.WindowsForms.PlotView timeoutPlot;
        private System.Windows.Forms.Button expandChartBtn;
        private System.Windows.Forms.CheckBox byteAtATimeCheck;
        private System.Windows.Forms.CheckBox rcCheck;
        private System.Windows.Forms.TextBox rcWait;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox retriesAllowed;
        private System.Windows.Forms.Label label1;
    }
}
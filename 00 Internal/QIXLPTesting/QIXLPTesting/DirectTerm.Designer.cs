
namespace QIXLPTesting
{
    partial class DirectTerm
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
            this.termOut = new System.Windows.Forms.RichTextBox();
            this.termIn = new System.Windows.Forms.TextBox();
            this.sendCrBtn = new System.Windows.Forms.Button();
            this.sendLfBtn = new System.Windows.Forms.Button();
            this.sendImmediatelyCheck = new System.Windows.Forms.CheckBox();
            this.flushOutBuffer = new System.Windows.Forms.Button();
            this.showCrLfCheck = new System.Windows.Forms.CheckBox();
            this.commandsBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.descLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.runDebugBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lineDelayBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.indefiniteCheck = new System.Windows.Forms.CheckBox();
            this.byteDelayBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.crRadio = new System.Windows.Forms.RadioButton();
            this.lfRadio = new System.Windows.Forms.RadioButton();
            this.crlfRadio = new System.Windows.Forms.RadioButton();
            this.connectedLbl = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.connectBtn = new System.Windows.Forms.Button();
            this.debugWorker = new System.ComponentModel.BackgroundWorker();
            this.clrOut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // termOut
            // 
            this.termOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.termOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(47)))), ((int)(((byte)(59)))));
            this.termOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.termOut.ForeColor = System.Drawing.Color.White;
            this.termOut.Location = new System.Drawing.Point(12, 12);
            this.termOut.Name = "termOut";
            this.termOut.Size = new System.Drawing.Size(679, 400);
            this.termOut.TabIndex = 5;
            this.termOut.Text = "";
            // 
            // termIn
            // 
            this.termIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.termIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(47)))), ((int)(((byte)(59)))));
            this.termIn.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.termIn.ForeColor = System.Drawing.Color.White;
            this.termIn.Location = new System.Drawing.Point(12, 416);
            this.termIn.Name = "termIn";
            this.termIn.Size = new System.Drawing.Size(679, 22);
            this.termIn.TabIndex = 6;
            this.termIn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendCmd);
            // 
            // sendCrBtn
            // 
            this.sendCrBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendCrBtn.Location = new System.Drawing.Point(697, 386);
            this.sendCrBtn.Name = "sendCrBtn";
            this.sendCrBtn.Size = new System.Drawing.Size(42, 23);
            this.sendCrBtn.TabIndex = 7;
            this.sendCrBtn.Text = "CR";
            this.sendCrBtn.UseVisualStyleBackColor = true;
            this.sendCrBtn.Click += new System.EventHandler(this.sendCrBtn_Click);
            // 
            // sendLfBtn
            // 
            this.sendLfBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendLfBtn.Location = new System.Drawing.Point(697, 415);
            this.sendLfBtn.Name = "sendLfBtn";
            this.sendLfBtn.Size = new System.Drawing.Size(42, 23);
            this.sendLfBtn.TabIndex = 8;
            this.sendLfBtn.Text = "LF";
            this.sendLfBtn.UseVisualStyleBackColor = true;
            this.sendLfBtn.Click += new System.EventHandler(this.sendLfBtn_Click);
            // 
            // sendImmediatelyCheck
            // 
            this.sendImmediatelyCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendImmediatelyCheck.AutoSize = true;
            this.sendImmediatelyCheck.Location = new System.Drawing.Point(697, 363);
            this.sendImmediatelyCheck.Name = "sendImmediatelyCheck";
            this.sendImmediatelyCheck.Size = new System.Drawing.Size(109, 17);
            this.sendImmediatelyCheck.TabIndex = 9;
            this.sendImmediatelyCheck.Text = "Send Immediately";
            this.sendImmediatelyCheck.UseVisualStyleBackColor = true;
            // 
            // flushOutBuffer
            // 
            this.flushOutBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flushOutBuffer.Location = new System.Drawing.Point(746, 386);
            this.flushOutBuffer.Name = "flushOutBuffer";
            this.flushOutBuffer.Size = new System.Drawing.Size(75, 52);
            this.flushOutBuffer.TabIndex = 10;
            this.flushOutBuffer.Text = "Flush Buffer";
            this.flushOutBuffer.UseVisualStyleBackColor = true;
            this.flushOutBuffer.Click += new System.EventHandler(this.flushOutBuffer_Click);
            // 
            // showCrLfCheck
            // 
            this.showCrLfCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.showCrLfCheck.AutoSize = true;
            this.showCrLfCheck.Checked = true;
            this.showCrLfCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showCrLfCheck.Location = new System.Drawing.Point(814, 363);
            this.showCrLfCheck.Name = "showCrLfCheck";
            this.showCrLfCheck.Size = new System.Drawing.Size(88, 17);
            this.showCrLfCheck.TabIndex = 12;
            this.showCrLfCheck.Text = "Show CR/LF";
            this.showCrLfCheck.UseVisualStyleBackColor = true;
            // 
            // commandsBox
            // 
            this.commandsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.commandsBox.Location = new System.Drawing.Point(697, 130);
            this.commandsBox.Multiline = true;
            this.commandsBox.Name = "commandsBox";
            this.commandsBox.Size = new System.Drawing.Size(100, 158);
            this.commandsBox.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(694, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 2);
            this.label1.TabIndex = 14;
            this.label1.Text = "label1";
            // 
            // descLbl
            // 
            this.descLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.descLbl.AutoSize = true;
            this.descLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descLbl.Location = new System.Drawing.Point(697, 16);
            this.descLbl.Name = "descLbl";
            this.descLbl.Size = new System.Drawing.Size(208, 18);
            this.descLbl.TabIndex = 15;
            this.descLbl.Text = "COM ___ || Serial Number";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(697, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "Commands:";
            // 
            // runDebugBtn
            // 
            this.runDebugBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.runDebugBtn.Location = new System.Drawing.Point(697, 316);
            this.runDebugBtn.Name = "runDebugBtn";
            this.runDebugBtn.Size = new System.Drawing.Size(208, 23);
            this.runDebugBtn.TabIndex = 17;
            this.runDebugBtn.Text = "Run Debug Commands";
            this.runDebugBtn.UseVisualStyleBackColor = true;
            this.runDebugBtn.Click += new System.EventHandler(this.runDebugBtn_ClickAsync);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(702, 297);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Run";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(803, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Line:";
            // 
            // lineDelayBox
            // 
            this.lineDelayBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lineDelayBox.Location = new System.Drawing.Point(833, 132);
            this.lineDelayBox.Name = "lineDelayBox";
            this.lineDelayBox.Size = new System.Drawing.Size(44, 20);
            this.lineDelayBox.TabIndex = 21;
            this.lineDelayBox.Text = "20";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(876, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "ms";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(803, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 15);
            this.label8.TabIndex = 24;
            this.label8.Text = "Delays:";
            // 
            // indefiniteCheck
            // 
            this.indefiniteCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.indefiniteCheck.AutoSize = true;
            this.indefiniteCheck.Location = new System.Drawing.Point(730, 296);
            this.indefiniteCheck.Name = "indefiniteCheck";
            this.indefiniteCheck.Size = new System.Drawing.Size(76, 17);
            this.indefiniteCheck.TabIndex = 25;
            this.indefiniteCheck.Text = "Indefinitely";
            this.indefiniteCheck.UseVisualStyleBackColor = true;
            // 
            // byteDelayBox
            // 
            this.byteDelayBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.byteDelayBox.Location = new System.Drawing.Point(833, 158);
            this.byteDelayBox.Name = "byteDelayBox";
            this.byteDelayBox.Size = new System.Drawing.Size(44, 20);
            this.byteDelayBox.TabIndex = 26;
            this.byteDelayBox.Text = "0";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(876, 161);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "ms";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(803, 161);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Byte:";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(803, 191);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 15);
            this.label11.TabIndex = 29;
            this.label11.Text = "Send With:";
            // 
            // crRadio
            // 
            this.crRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crRadio.AutoSize = true;
            this.crRadio.Location = new System.Drawing.Point(818, 209);
            this.crRadio.Name = "crRadio";
            this.crRadio.Size = new System.Drawing.Size(40, 17);
            this.crRadio.TabIndex = 30;
            this.crRadio.Text = "CR";
            this.crRadio.UseVisualStyleBackColor = true;
            // 
            // lfRadio
            // 
            this.lfRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lfRadio.AutoSize = true;
            this.lfRadio.Location = new System.Drawing.Point(818, 232);
            this.lfRadio.Name = "lfRadio";
            this.lfRadio.Size = new System.Drawing.Size(37, 17);
            this.lfRadio.TabIndex = 31;
            this.lfRadio.Text = "LF";
            this.lfRadio.UseVisualStyleBackColor = true;
            // 
            // crlfRadio
            // 
            this.crlfRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crlfRadio.AutoSize = true;
            this.crlfRadio.Checked = true;
            this.crlfRadio.Location = new System.Drawing.Point(818, 255);
            this.crlfRadio.Name = "crlfRadio";
            this.crlfRadio.Size = new System.Drawing.Size(64, 17);
            this.crlfRadio.TabIndex = 32;
            this.crlfRadio.TabStop = true;
            this.crlfRadio.Text = "CR + LF";
            this.crlfRadio.UseVisualStyleBackColor = true;
            // 
            // connectedLbl
            // 
            this.connectedLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectedLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectedLbl.ForeColor = System.Drawing.Color.Red;
            this.connectedLbl.Location = new System.Drawing.Point(746, 40);
            this.connectedLbl.Name = "connectedLbl";
            this.connectedLbl.Size = new System.Drawing.Size(161, 32);
            this.connectedLbl.TabIndex = 33;
            this.connectedLbl.Text = "NOT CONNECTED";
            this.connectedLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Location = new System.Drawing.Point(694, 349);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(208, 2);
            this.label13.TabIndex = 34;
            this.label13.Text = "label13";
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(837, 75);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(71, 29);
            this.connectBtn.TabIndex = 35;
            this.connectBtn.Text = "Disconnect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // debugWorker
            // 
            this.debugWorker.WorkerReportsProgress = true;
            this.debugWorker.WorkerSupportsCancellation = true;
            this.debugWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.debugWorker_DoWork);
            this.debugWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.debugWorker_RunWorkerCompleted);
            // 
            // clrOut
            // 
            this.clrOut.Location = new System.Drawing.Point(827, 386);
            this.clrOut.Name = "clrOut";
            this.clrOut.Size = new System.Drawing.Size(78, 52);
            this.clrOut.TabIndex = 36;
            this.clrOut.Text = "Clear";
            this.clrOut.UseVisualStyleBackColor = true;
            this.clrOut.Click += new System.EventHandler(this.clrOut_Click);
            // 
            // DirectTerm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 446);
            this.Controls.Add(this.clrOut);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.connectedLbl);
            this.Controls.Add(this.crlfRadio);
            this.Controls.Add(this.lfRadio);
            this.Controls.Add(this.crRadio);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.byteDelayBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.indefiniteCheck);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lineDelayBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.runDebugBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.descLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.commandsBox);
            this.Controls.Add(this.showCrLfCheck);
            this.Controls.Add(this.flushOutBuffer);
            this.Controls.Add(this.sendImmediatelyCheck);
            this.Controls.Add(this.sendLfBtn);
            this.Controls.Add(this.sendCrBtn);
            this.Controls.Add(this.termIn);
            this.Controls.Add(this.termOut);
            this.Controls.Add(this.label6);
            this.MinimumSize = new System.Drawing.Size(600, 412);
            this.Name = "DirectTerm";
            this.Text = "DirectTerm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DisconnectDereference);
            this.Load += new System.EventHandler(this.DirectTerm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox termOut;
        private System.Windows.Forms.TextBox termIn;
        private System.Windows.Forms.Button sendCrBtn;
        private System.Windows.Forms.Button sendLfBtn;
        private System.Windows.Forms.CheckBox sendImmediatelyCheck;
        private System.Windows.Forms.Button flushOutBuffer;
        private System.Windows.Forms.CheckBox showCrLfCheck;
        private System.Windows.Forms.TextBox commandsBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label descLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button runDebugBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox lineDelayBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox indefiniteCheck;
        private System.Windows.Forms.TextBox byteDelayBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton crRadio;
        private System.Windows.Forms.RadioButton lfRadio;
        private System.Windows.Forms.RadioButton crlfRadio;
        private System.Windows.Forms.Label connectedLbl;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button connectBtn;
        private System.ComponentModel.BackgroundWorker debugWorker;
        private System.Windows.Forms.Button clrOut;
    }
}
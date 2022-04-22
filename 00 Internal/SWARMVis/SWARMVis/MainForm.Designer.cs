
namespace SWARMVis
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
            this.rawOutBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.allMsgButton = new System.Windows.Forms.Button();
            this.allDevsButton = new System.Windows.Forms.Button();
            this.authButton = new System.Windows.Forms.Button();
            this.authLbl = new System.Windows.Forms.Label();
            this.availDevsLbl = new System.Windows.Forms.Label();
            this.msgAvailLbl = new System.Windows.Forms.Label();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.refServerBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rawOutBox
            // 
            this.rawOutBox.Location = new System.Drawing.Point(165, 24);
            this.rawOutBox.Multiline = true;
            this.rawOutBox.Name = "rawOutBox";
            this.rawOutBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rawOutBox.Size = new System.Drawing.Size(375, 551);
            this.rawOutBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Raw Output (JSON)";
            // 
            // allMsgButton
            // 
            this.allMsgButton.Location = new System.Drawing.Point(13, 552);
            this.allMsgButton.Name = "allMsgButton";
            this.allMsgButton.Size = new System.Drawing.Size(134, 23);
            this.allMsgButton.TabIndex = 2;
            this.allMsgButton.Text = "Find Messages";
            this.allMsgButton.UseVisualStyleBackColor = true;
            this.allMsgButton.Click += new System.EventHandler(this.allMsgButton_Click);
            // 
            // allDevsButton
            // 
            this.allDevsButton.Location = new System.Drawing.Point(13, 505);
            this.allDevsButton.Name = "allDevsButton";
            this.allDevsButton.Size = new System.Drawing.Size(134, 23);
            this.allDevsButton.TabIndex = 3;
            this.allDevsButton.Text = "Find Devices";
            this.allDevsButton.UseVisualStyleBackColor = true;
            this.allDevsButton.Click += new System.EventHandler(this.allDevsButton_Click);
            // 
            // authButton
            // 
            this.authButton.Location = new System.Drawing.Point(13, 24);
            this.authButton.Name = "authButton";
            this.authButton.Size = new System.Drawing.Size(134, 23);
            this.authButton.TabIndex = 4;
            this.authButton.Text = "Log Out";
            this.authButton.UseVisualStyleBackColor = true;
            this.authButton.Click += new System.EventHandler(this.logOutButton_Click);
            // 
            // authLbl
            // 
            this.authLbl.AutoSize = true;
            this.authLbl.ForeColor = System.Drawing.Color.Green;
            this.authLbl.Location = new System.Drawing.Point(13, 6);
            this.authLbl.Name = "authLbl";
            this.authLbl.Size = new System.Drawing.Size(0, 15);
            this.authLbl.TabIndex = 5;
            // 
            // availDevsLbl
            // 
            this.availDevsLbl.AutoSize = true;
            this.availDevsLbl.Location = new System.Drawing.Point(12, 487);
            this.availDevsLbl.Name = "availDevsLbl";
            this.availDevsLbl.Size = new System.Drawing.Size(104, 15);
            this.availDevsLbl.TabIndex = 6;
            this.availDevsLbl.Text = "Devices Available: ";
            // 
            // msgAvailLbl
            // 
            this.msgAvailLbl.AutoSize = true;
            this.msgAvailLbl.Location = new System.Drawing.Point(12, 534);
            this.msgAvailLbl.Name = "msgAvailLbl";
            this.msgAvailLbl.Size = new System.Drawing.Size(115, 15);
            this.msgAvailLbl.TabIndex = 7;
            this.msgAvailLbl.Text = "Messages Available: ";
            // 
            // refreshBtn
            // 
            this.refreshBtn.Location = new System.Drawing.Point(13, 53);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(134, 23);
            this.refreshBtn.TabIndex = 8;
            this.refreshBtn.Text = "Refresh";
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // refServerBtn
            // 
            this.refServerBtn.Location = new System.Drawing.Point(13, 82);
            this.refServerBtn.Name = "refServerBtn";
            this.refServerBtn.Size = new System.Drawing.Size(134, 23);
            this.refServerBtn.TabIndex = 9;
            this.refServerBtn.Text = "Update Local Server";
            this.refServerBtn.UseVisualStyleBackColor = true;
            this.refServerBtn.Click += new System.EventHandler(this.refServerBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 587);
            this.Controls.Add(this.refServerBtn);
            this.Controls.Add(this.refreshBtn);
            this.Controls.Add(this.msgAvailLbl);
            this.Controls.Add(this.availDevsLbl);
            this.Controls.Add(this.authLbl);
            this.Controls.Add(this.authButton);
            this.Controls.Add(this.allDevsButton);
            this.Controls.Add(this.allMsgButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rawOutBox);
            this.Name = "MainForm";
            this.Text = "SWARM Data Retireval Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox rawOutBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button allMsgButton;
        private System.Windows.Forms.Button allDevsButton;
        private System.Windows.Forms.Button authButton;
        private System.Windows.Forms.Label authLbl;
        private System.Windows.Forms.Label availDevsLbl;
        private System.Windows.Forms.Label msgAvailLbl;
        private System.Windows.Forms.Button refreshBtn;
        private System.Windows.Forms.Button refServerBtn;
    }
}


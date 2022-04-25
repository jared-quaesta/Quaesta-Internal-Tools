
namespace UniversalUpdate
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.availComs = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.avail = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tCPDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showWarningsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnOffAllLEDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sTLinkFlashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crpDrag = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.manSelectBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.updateBtn = new System.Windows.Forms.Button();
            this.addCommandsBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.secretCheck = new System.Windows.Forms.CheckBox();
            this.clrFWBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.prevSn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.desSn = new System.Windows.Forms.TextBox();
            this.desEx = new System.Windows.Forms.Label();
            this.prevEx = new System.Windows.Forms.Label();
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.refServerCheck = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.serverPanel = new System.Windows.Forms.Panel();
            this.progTxt = new System.Windows.Forms.Label();
            this.blinkBtn = new System.Windows.Forms.Button();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.serverPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // availComs
            // 
            this.availComs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.availComs.CheckOnClick = true;
            this.availComs.FormattingEnabled = true;
            this.availComs.Location = new System.Drawing.Point(12, 45);
            this.availComs.Name = "availComs";
            this.availComs.Size = new System.Drawing.Size(183, 529);
            this.availComs.TabIndex = 0;
            this.availComs.SelectedIndexChanged += new System.EventHandler(this.BlinkSelectedLED);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 577);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SelAll);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(62, 577);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(51, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "None";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SelNone);
            // 
            // avail
            // 
            this.avail.AutoSize = true;
            this.avail.Location = new System.Drawing.Point(13, 26);
            this.avail.Name = "avail";
            this.avail.Size = new System.Drawing.Size(152, 13);
            this.avail.TabIndex = 3;
            this.avail.Text = "Available NPMs (0 Connected)";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.sTLinkFlashToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(460, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serialDevicesToolStripMenuItem,
            this.tCPDevicesToolStripMenuItem});
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // serialDevicesToolStripMenuItem
            // 
            this.serialDevicesToolStripMenuItem.Name = "serialDevicesToolStripMenuItem";
            this.serialDevicesToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.serialDevicesToolStripMenuItem.Text = "Serial Devices";
            this.serialDevicesToolStripMenuItem.Click += new System.EventHandler(this.serialDevicesToolStripMenuItem_ClickAsync);
            // 
            // tCPDevicesToolStripMenuItem
            // 
            this.tCPDevicesToolStripMenuItem.Name = "tCPDevicesToolStripMenuItem";
            this.tCPDevicesToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.tCPDevicesToolStripMenuItem.Text = "TCP Devices";
            this.tCPDevicesToolStripMenuItem.Click += new System.EventHandler(this.tCPDevicesToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showWarningsToolStripMenuItem,
            this.showMenuToolStripMenuItem,
            this.turnOffAllLEDsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // showWarningsToolStripMenuItem
            // 
            this.showWarningsToolStripMenuItem.Name = "showWarningsToolStripMenuItem";
            this.showWarningsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.showWarningsToolStripMenuItem.Text = "Show Warnings";
            this.showWarningsToolStripMenuItem.Click += new System.EventHandler(this.showWarningsToolStripMenuItem_Click);
            // 
            // showMenuToolStripMenuItem
            // 
            this.showMenuToolStripMenuItem.Name = "showMenuToolStripMenuItem";
            this.showMenuToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.showMenuToolStripMenuItem.Text = "Show Menu";
            this.showMenuToolStripMenuItem.Click += new System.EventHandler(this.showMenuToolStripMenuItem_Click);
            // 
            // turnOffAllLEDsToolStripMenuItem
            // 
            this.turnOffAllLEDsToolStripMenuItem.Name = "turnOffAllLEDsToolStripMenuItem";
            this.turnOffAllLEDsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.turnOffAllLEDsToolStripMenuItem.Text = "Turn off all LEDs";
            this.turnOffAllLEDsToolStripMenuItem.Click += new System.EventHandler(this.turnOffAllLEDsToolStripMenuItem_Click);
            // 
            // sTLinkFlashToolStripMenuItem
            // 
            this.sTLinkFlashToolStripMenuItem.Name = "sTLinkFlashToolStripMenuItem";
            this.sTLinkFlashToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.sTLinkFlashToolStripMenuItem.Text = "ST-Link Flash";
            this.sTLinkFlashToolStripMenuItem.Click += new System.EventHandler(this.sTLinkFlashToolStripMenuItem_Click);
            // 
            // crpDrag
            // 
            this.crpDrag.AllowDrop = true;
            this.crpDrag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.crpDrag.Location = new System.Drawing.Point(201, 62);
            this.crpDrag.Multiline = true;
            this.crpDrag.Name = "crpDrag";
            this.crpDrag.ReadOnly = true;
            this.crpDrag.Size = new System.Drawing.Size(247, 71);
            this.crpDrag.TabIndex = 5;
            this.crpDrag.DragDrop += new System.Windows.Forms.DragEventHandler(this.crpDrag_DragDrop);
            this.crpDrag.DragOver += new System.Windows.Forms.DragEventHandler(this.crpDrag_DragOver);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Drag .crp here";
            // 
            // manSelectBtn
            // 
            this.manSelectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.manSelectBtn.Location = new System.Drawing.Point(267, 139);
            this.manSelectBtn.Name = "manSelectBtn";
            this.manSelectBtn.Size = new System.Drawing.Size(75, 23);
            this.manSelectBtn.TabIndex = 7;
            this.manSelectBtn.Text = "Select";
            this.manSelectBtn.UseVisualStyleBackColor = true;
            this.manSelectBtn.Click += new System.EventHandler(this.manSelectBtn_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(249, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "or";
            // 
            // updateBtn
            // 
            this.updateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateBtn.ForeColor = System.Drawing.Color.Green;
            this.updateBtn.Location = new System.Drawing.Point(201, 532);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(247, 68);
            this.updateBtn.TabIndex = 9;
            this.updateBtn.Text = "Go";
            this.updateBtn.UseVisualStyleBackColor = true;
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_ClickAsync);
            // 
            // addCommandsBox
            // 
            this.addCommandsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addCommandsBox.Location = new System.Drawing.Point(201, 182);
            this.addCommandsBox.Multiline = true;
            this.addCommandsBox.Name = "addCommandsBox";
            this.addCommandsBox.Size = new System.Drawing.Size(247, 152);
            this.addCommandsBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(201, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Additional Commands (one per line)";
            // 
            // secretCheck
            // 
            this.secretCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.secretCheck.AutoSize = true;
            this.secretCheck.Location = new System.Drawing.Point(330, 340);
            this.secretCheck.Name = "secretCheck";
            this.secretCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.secretCheck.Size = new System.Drawing.Size(118, 17);
            this.secretCheck.TabIndex = 13;
            this.secretCheck.Text = "?Secret Commands";
            this.secretCheck.UseVisualStyleBackColor = true;
            this.secretCheck.CheckedChanged += new System.EventHandler(this.secretCheck_CheckedChanged);
            // 
            // clrFWBtn
            // 
            this.clrFWBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clrFWBtn.Location = new System.Drawing.Point(371, 139);
            this.clrFWBtn.Name = "clrFWBtn";
            this.clrFWBtn.Size = new System.Drawing.Size(75, 23);
            this.clrFWBtn.TabIndex = 14;
            this.clrFWBtn.Text = "Clear";
            this.clrFWBtn.UseVisualStyleBackColor = true;
            this.clrFWBtn.Click += new System.EventHandler(this.clrFWBtn_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(348, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "or";
            // 
            // prevSn
            // 
            this.prevSn.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prevSn.Location = new System.Drawing.Point(4, 25);
            this.prevSn.Name = "prevSn";
            this.prevSn.Size = new System.Drawing.Size(244, 20);
            this.prevSn.TabIndex = 16;
            this.prevSn.TextChanged += new System.EventHandler(this.prevSn_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(200, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Previous Serial Number Format (Optional)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(199, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Desired Serial Number Format (Required)";
            // 
            // desSn
            // 
            this.desSn.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desSn.Location = new System.Drawing.Point(4, 81);
            this.desSn.Name = "desSn";
            this.desSn.Size = new System.Drawing.Size(244, 20);
            this.desSn.TabIndex = 18;
            this.desSn.TextChanged += new System.EventHandler(this.desSn_TextChanged);
            // 
            // desEx
            // 
            this.desEx.AutoSize = true;
            this.desEx.Location = new System.Drawing.Point(1, 104);
            this.desEx.Name = "desEx";
            this.desEx.Size = new System.Drawing.Size(21, 13);
            this.desEx.TabIndex = 20;
            this.desEx.Text = "ex:";
            // 
            // prevEx
            // 
            this.prevEx.AutoSize = true;
            this.prevEx.Location = new System.Drawing.Point(1, 48);
            this.prevEx.Name = "prevEx";
            this.prevEx.Size = new System.Drawing.Size(21, 13);
            this.prevEx.TabIndex = 21;
            this.prevEx.Text = "ex:";
            // 
            // typeBox
            // 
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Items.AddRange(new object[] {
            "QIY",
            "QIX LP",
            "QIX HP",
            "ETHERNET",
            "SPAWAR",
            "PENTA PULSE",
            "GAMMA SPECTROMETER",
            "DL2200"});
            this.typeBox.Location = new System.Drawing.Point(71, 125);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(177, 21);
            this.typeBox.TabIndex = 22;
            this.typeBox.Text = "Not Selected";
            // 
            // refServerCheck
            // 
            this.refServerCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refServerCheck.AutoSize = true;
            this.refServerCheck.Location = new System.Drawing.Point(377, 357);
            this.refServerCheck.Name = "refServerCheck";
            this.refServerCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.refServerCheck.Size = new System.Drawing.Size(71, 17);
            this.refServerCheck.TabIndex = 23;
            this.refServerCheck.Text = "?Serialize";
            this.refServerCheck.UseVisualStyleBackColor = true;
            this.refServerCheck.CheckedChanged += new System.EventHandler(this.refServerCheck_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 128);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Type";
            // 
            // serverPanel
            // 
            this.serverPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.serverPanel.Controls.Add(this.typeBox);
            this.serverPanel.Controls.Add(this.prevEx);
            this.serverPanel.Controls.Add(this.desEx);
            this.serverPanel.Controls.Add(this.label7);
            this.serverPanel.Controls.Add(this.desSn);
            this.serverPanel.Controls.Add(this.label6);
            this.serverPanel.Controls.Add(this.prevSn);
            this.serverPanel.Controls.Add(this.label10);
            this.serverPanel.Location = new System.Drawing.Point(200, 380);
            this.serverPanel.Name = "serverPanel";
            this.serverPanel.Size = new System.Drawing.Size(259, 152);
            this.serverPanel.TabIndex = 25;
            this.serverPanel.Visible = false;
            // 
            // progTxt
            // 
            this.progTxt.AutoSize = true;
            this.progTxt.Location = new System.Drawing.Point(12, 609);
            this.progTxt.Name = "progTxt";
            this.progTxt.Size = new System.Drawing.Size(55, 13);
            this.progTxt.TabIndex = 26;
            this.progTxt.Text = "Waiting....";
            // 
            // blinkBtn
            // 
            this.blinkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.blinkBtn.Location = new System.Drawing.Point(119, 577);
            this.blinkBtn.Name = "blinkBtn";
            this.blinkBtn.Size = new System.Drawing.Size(76, 23);
            this.blinkBtn.TabIndex = 27;
            this.blinkBtn.Text = "Blink";
            this.blinkBtn.UseVisualStyleBackColor = true;
            this.blinkBtn.Click += new System.EventHandler(this.blinkBtn_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.manualToolStripMenuItem.Text = "Manual";
            this.manualToolStripMenuItem.Click += new System.EventHandler(this.manualToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 631);
            this.Controls.Add(this.blinkBtn);
            this.Controls.Add(this.progTxt);
            this.Controls.Add(this.serverPanel);
            this.Controls.Add(this.refServerCheck);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.clrFWBtn);
            this.Controls.Add(this.secretCheck);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.addCommandsBox);
            this.Controls.Add(this.updateBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.manSelectBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.crpDrag);
            this.Controls.Add(this.avail);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.availComs);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(476, 670);
            this.MinimumSize = new System.Drawing.Size(476, 670);
            this.Name = "MainForm";
            this.Text = "Super Fast Universal Update and Settings Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.serverPanel.ResumeLayout(false);
            this.serverPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox availComs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label avail;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.TextBox crpDrag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button manSelectBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button updateBtn;
        private System.Windows.Forms.TextBox addCommandsBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.CheckBox secretCheck;
        private System.Windows.Forms.Button clrFWBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showWarningsToolStripMenuItem;
        private System.Windows.Forms.TextBox prevSn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox desSn;
        private System.Windows.Forms.Label desEx;
        private System.Windows.Forms.Label prevEx;
        private System.Windows.Forms.ComboBox typeBox;
        private System.Windows.Forms.CheckBox refServerCheck;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel serverPanel;
        private System.Windows.Forms.ToolStripMenuItem sTLinkFlashToolStripMenuItem;
        private System.Windows.Forms.Label progTxt;
        private System.Windows.Forms.ToolStripMenuItem showMenuToolStripMenuItem;
        private System.Windows.Forms.Button blinkBtn;
        private System.Windows.Forms.ToolStripMenuItem turnOffAllLEDsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serialDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tCPDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
    }
}


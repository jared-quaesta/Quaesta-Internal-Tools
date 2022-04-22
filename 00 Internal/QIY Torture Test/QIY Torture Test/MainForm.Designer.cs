
namespace QIY_Torture_Test
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
            this.stagesTabControl = new System.Windows.Forms.TabControl();
            this.stageOneTab = new System.Windows.Forms.TabPage();
            this.relayOnBtn = new System.Windows.Forms.Button();
            this.relayOffBtn = new System.Windows.Forms.Button();
            this.selNoneBtn = new System.Windows.Forms.Button();
            this.refreshTCP = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dlsbox = new System.Windows.Forms.ComboBox();
            this.searchPath = new System.Windows.Forms.Button();
            this.s1PathBox = new System.Windows.Forms.TextBox();
            this.dlConnStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.selAllBtn = new System.Windows.Forms.Button();
            this.beginStageOne = new System.Windows.Forms.Button();
            this.npmSelect = new System.Windows.Forms.CheckedListBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cycleOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.stagesTabControl.SuspendLayout();
            this.stageOneTab.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stagesTabControl
            // 
            this.stagesTabControl.Controls.Add(this.stageOneTab);
            this.stagesTabControl.Controls.Add(this.tabPage2);
            this.stagesTabControl.Location = new System.Drawing.Point(0, 24);
            this.stagesTabControl.Name = "stagesTabControl";
            this.stagesTabControl.SelectedIndex = 0;
            this.stagesTabControl.Size = new System.Drawing.Size(429, 314);
            this.stagesTabControl.TabIndex = 0;
            // 
            // stageOneTab
            // 
            this.stageOneTab.Controls.Add(this.relayOnBtn);
            this.stageOneTab.Controls.Add(this.relayOffBtn);
            this.stageOneTab.Controls.Add(this.selNoneBtn);
            this.stageOneTab.Controls.Add(this.refreshTCP);
            this.stageOneTab.Controls.Add(this.label3);
            this.stageOneTab.Controls.Add(this.dlsbox);
            this.stageOneTab.Controls.Add(this.searchPath);
            this.stageOneTab.Controls.Add(this.s1PathBox);
            this.stageOneTab.Controls.Add(this.dlConnStatus);
            this.stageOneTab.Controls.Add(this.label1);
            this.stageOneTab.Controls.Add(this.selAllBtn);
            this.stageOneTab.Controls.Add(this.beginStageOne);
            this.stageOneTab.Controls.Add(this.npmSelect);
            this.stageOneTab.Controls.Add(this.richTextBox1);
            this.stageOneTab.Location = new System.Drawing.Point(4, 22);
            this.stageOneTab.Name = "stageOneTab";
            this.stageOneTab.Padding = new System.Windows.Forms.Padding(3);
            this.stageOneTab.Size = new System.Drawing.Size(421, 288);
            this.stageOneTab.TabIndex = 0;
            this.stageOneTab.Text = "Stage One";
            this.stageOneTab.UseVisualStyleBackColor = true;
            // 
            // relayOnBtn
            // 
            this.relayOnBtn.Enabled = false;
            this.relayOnBtn.ForeColor = System.Drawing.Color.Black;
            this.relayOnBtn.Location = new System.Drawing.Point(203, 194);
            this.relayOnBtn.Name = "relayOnBtn";
            this.relayOnBtn.Size = new System.Drawing.Size(60, 23);
            this.relayOnBtn.TabIndex = 15;
            this.relayOnBtn.Text = "Relay On";
            this.relayOnBtn.UseVisualStyleBackColor = true;
            this.relayOnBtn.Click += new System.EventHandler(this.relayOnBtn_Click);
            // 
            // relayOffBtn
            // 
            this.relayOffBtn.Enabled = false;
            this.relayOffBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.relayOffBtn.Location = new System.Drawing.Point(137, 194);
            this.relayOffBtn.Name = "relayOffBtn";
            this.relayOffBtn.Size = new System.Drawing.Size(60, 23);
            this.relayOffBtn.TabIndex = 14;
            this.relayOffBtn.Text = "Relay Off";
            this.relayOffBtn.UseVisualStyleBackColor = true;
            this.relayOffBtn.Click += new System.EventHandler(this.relayOffBtn_Click);
            // 
            // selNoneBtn
            // 
            this.selNoneBtn.Location = new System.Drawing.Point(68, 260);
            this.selNoneBtn.Name = "selNoneBtn";
            this.selNoneBtn.Size = new System.Drawing.Size(60, 23);
            this.selNoneBtn.TabIndex = 12;
            this.selNoneBtn.Text = "None";
            this.selNoneBtn.UseVisualStyleBackColor = true;
            this.selNoneBtn.Click += new System.EventHandler(this.selNoneBtn_Click);
            // 
            // refreshTCP
            // 
            this.refreshTCP.Location = new System.Drawing.Point(8, 237);
            this.refreshTCP.Name = "refreshTCP";
            this.refreshTCP.Size = new System.Drawing.Size(120, 23);
            this.refreshTCP.TabIndex = 11;
            this.refreshTCP.Text = "Refresh";
            this.refreshTCP.UseVisualStyleBackColor = true;
            this.refreshTCP.Click += new System.EventHandler(this.refreshTCP_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(134, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Write Data to This Directory:";
            // 
            // dlsbox
            // 
            this.dlsbox.FormattingEnabled = true;
            this.dlsbox.Location = new System.Drawing.Point(280, 212);
            this.dlsbox.Name = "dlsbox";
            this.dlsbox.Size = new System.Drawing.Size(134, 21);
            this.dlsbox.TabIndex = 9;
            this.dlsbox.SelectedIndexChanged += new System.EventHandler(this.TryConnectDL);
            // 
            // searchPath
            // 
            this.searchPath.Location = new System.Drawing.Point(392, 235);
            this.searchPath.Name = "searchPath";
            this.searchPath.Size = new System.Drawing.Size(24, 23);
            this.searchPath.TabIndex = 8;
            this.searchPath.Text = "...";
            this.searchPath.UseVisualStyleBackColor = true;
            this.searchPath.Click += new System.EventHandler(this.searchPath_Click);
            // 
            // s1PathBox
            // 
            this.s1PathBox.Location = new System.Drawing.Point(137, 237);
            this.s1PathBox.Name = "s1PathBox";
            this.s1PathBox.Size = new System.Drawing.Size(253, 20);
            this.s1PathBox.TabIndex = 7;
            // 
            // dlConnStatus
            // 
            this.dlConnStatus.AutoSize = true;
            this.dlConnStatus.ForeColor = System.Drawing.Color.Red;
            this.dlConnStatus.Location = new System.Drawing.Point(335, 196);
            this.dlConnStatus.Name = "dlConnStatus";
            this.dlConnStatus.Size = new System.Drawing.Size(79, 13);
            this.dlConnStatus.TabIndex = 6;
            this.dlConnStatus.Text = "Not Connected";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(277, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Datalogger:";
            // 
            // selAllBtn
            // 
            this.selAllBtn.Location = new System.Drawing.Point(8, 260);
            this.selAllBtn.Name = "selAllBtn";
            this.selAllBtn.Size = new System.Drawing.Size(60, 23);
            this.selAllBtn.TabIndex = 3;
            this.selAllBtn.Text = "All";
            this.selAllBtn.UseVisualStyleBackColor = true;
            this.selAllBtn.Click += new System.EventHandler(this.selAllBtn_Click);
            // 
            // beginStageOne
            // 
            this.beginStageOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.beginStageOne.ForeColor = System.Drawing.Color.Green;
            this.beginStageOne.Location = new System.Drawing.Point(135, 260);
            this.beginStageOne.Name = "beginStageOne";
            this.beginStageOne.Size = new System.Drawing.Size(281, 23);
            this.beginStageOne.TabIndex = 2;
            this.beginStageOne.Text = "Begin";
            this.beginStageOne.UseVisualStyleBackColor = true;
            this.beginStageOne.Click += new System.EventHandler(this.beginStageOne_Click);
            // 
            // npmSelect
            // 
            this.npmSelect.CheckOnClick = true;
            this.npmSelect.FormattingEnabled = true;
            this.npmSelect.Location = new System.Drawing.Point(8, 8);
            this.npmSelect.Name = "npmSelect";
            this.npmSelect.Size = new System.Drawing.Size(120, 229);
            this.npmSelect.TabIndex = 1;
            this.npmSelect.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddNewIP);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(134, 8);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(282, 180);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(421, 288);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Stage Two";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cycleOptionsToolStripMenuItem,
            this.terminalToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(430, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cycleOptionsToolStripMenuItem
            // 
            this.cycleOptionsToolStripMenuItem.Name = "cycleOptionsToolStripMenuItem";
            this.cycleOptionsToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.cycleOptionsToolStripMenuItem.Text = "Cycle Options";
            this.cycleOptionsToolStripMenuItem.Click += new System.EventHandler(this.cycleOptionsToolStripMenuItem_Click);
            // 
            // terminalToolStripMenuItem
            // 
            this.terminalToolStripMenuItem.Name = "terminalToolStripMenuItem";
            this.terminalToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.terminalToolStripMenuItem.Text = "Terminal";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 339);
            this.Controls.Add(this.stagesTabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "QIY Stress Test Suite";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.stagesTabControl.ResumeLayout(false);
            this.stageOneTab.ResumeLayout(false);
            this.stageOneTab.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl stagesTabControl;
        private System.Windows.Forms.TabPage stageOneTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button selNoneBtn;
        private System.Windows.Forms.Button refreshTCP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox dlsbox;
        private System.Windows.Forms.Button searchPath;
        private System.Windows.Forms.TextBox s1PathBox;
        private System.Windows.Forms.Label dlConnStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button selAllBtn;
        private System.Windows.Forms.Button beginStageOne;
        private System.Windows.Forms.CheckedListBox npmSelect;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cycleOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terminalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button relayOnBtn;
        private System.Windows.Forms.Button relayOffBtn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
    }
}


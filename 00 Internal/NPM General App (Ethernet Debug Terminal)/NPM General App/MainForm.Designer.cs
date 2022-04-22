
namespace NPM_General_App
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.connectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshSerialConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshSDI12ConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataLoggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workingDirectoriesAndFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedModesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xPortSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataRetrievalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainClockWorker = new System.ComponentModel.BackgroundWorker();
            this.npmTabControl = new System.Windows.Forms.TabControl();
            this.findFirmwareDialog = new System.Windows.Forms.OpenFileDialog();
            this.enterIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowDrop = true;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.dataRetrievalToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(977, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // connectionsToolStripMenuItem
            // 
            this.connectionsToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.connectionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshDevicesToolStripMenuItem,
            this.refreshSerialConnectionsToolStripMenuItem,
            this.refreshSDI12ConnectionsToolStripMenuItem,
            this.dataLoggerToolStripMenuItem,
            this.enterIPToolStripMenuItem});
            this.connectionsToolStripMenuItem.Name = "connectionsToolStripMenuItem";
            this.connectionsToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.connectionsToolStripMenuItem.Text = "Connections";
            // 
            // refreshDevicesToolStripMenuItem
            // 
            this.refreshDevicesToolStripMenuItem.Name = "refreshDevicesToolStripMenuItem";
            this.refreshDevicesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refreshDevicesToolStripMenuItem.Text = "TCP";
            this.refreshDevicesToolStripMenuItem.Click += new System.EventHandler(this.refreshDevicesToolStripMenuItem_Click);
            // 
            // refreshSerialConnectionsToolStripMenuItem
            // 
            this.refreshSerialConnectionsToolStripMenuItem.Name = "refreshSerialConnectionsToolStripMenuItem";
            this.refreshSerialConnectionsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refreshSerialConnectionsToolStripMenuItem.Text = "Serial (USB)";
            this.refreshSerialConnectionsToolStripMenuItem.Click += new System.EventHandler(this.refreshSerialConnectionsToolStripMenuItem_Click);
            // 
            // refreshSDI12ConnectionsToolStripMenuItem
            // 
            this.refreshSDI12ConnectionsToolStripMenuItem.Name = "refreshSDI12ConnectionsToolStripMenuItem";
            this.refreshSDI12ConnectionsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refreshSDI12ConnectionsToolStripMenuItem.Text = "RS232";
            // 
            // dataLoggerToolStripMenuItem
            // 
            this.dataLoggerToolStripMenuItem.Name = "dataLoggerToolStripMenuItem";
            this.dataLoggerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dataLoggerToolStripMenuItem.Text = "DataLogger";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.workingDirectoriesAndFilesToolStripMenuItem,
            this.advancedModesToolStripMenuItem,
            this.xPortSettingsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // workingDirectoriesAndFilesToolStripMenuItem
            // 
            this.workingDirectoriesAndFilesToolStripMenuItem.Name = "workingDirectoriesAndFilesToolStripMenuItem";
            this.workingDirectoriesAndFilesToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.workingDirectoriesAndFilesToolStripMenuItem.Text = "Working Directories and Files";
            // 
            // advancedModesToolStripMenuItem
            // 
            this.advancedModesToolStripMenuItem.Name = "advancedModesToolStripMenuItem";
            this.advancedModesToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.advancedModesToolStripMenuItem.Text = "Advanced Modes";
            // 
            // xPortSettingsToolStripMenuItem
            // 
            this.xPortSettingsToolStripMenuItem.Name = "xPortSettingsToolStripMenuItem";
            this.xPortSettingsToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.xPortSettingsToolStripMenuItem.Text = "XPort Settings";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.terminalToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // terminalToolStripMenuItem
            // 
            this.terminalToolStripMenuItem.Name = "terminalToolStripMenuItem";
            this.terminalToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.terminalToolStripMenuItem.Text = "Update Firmware";
            this.terminalToolStripMenuItem.Click += new System.EventHandler(this.openFWUpgrader);
            // 
            // dataRetrievalToolStripMenuItem
            // 
            this.dataRetrievalToolStripMenuItem.Name = "dataRetrievalToolStripMenuItem";
            this.dataRetrievalToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.dataRetrievalToolStripMenuItem.Text = "Data Retrieval";
            // 
            // mainClockWorker
            // 
            this.mainClockWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ClockSleep);
            this.mainClockWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.UpdateData);
            // 
            // npmTabControl
            // 
            this.npmTabControl.Location = new System.Drawing.Point(12, 27);
            this.npmTabControl.Name = "npmTabControl";
            this.npmTabControl.SelectedIndex = 0;
            this.npmTabControl.Size = new System.Drawing.Size(958, 563);
            this.npmTabControl.TabIndex = 1;
            // 
            // findFirmwareDialog
            // 
            this.findFirmwareDialog.FileName = "openFileDialog1";
            // 
            // enterIPToolStripMenuItem
            // 
            this.enterIPToolStripMenuItem.Name = "enterIPToolStripMenuItem";
            this.enterIPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.enterIPToolStripMenuItem.Text = "Enter IP";
            this.enterIPToolStripMenuItem.Click += new System.EventHandler(this.enterIPToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 595);
            this.Controls.Add(this.npmTabControl);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem connectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem workingDirectoriesAndFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedModesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xPortSettingsToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker mainClockWorker;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terminalToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl npmTabControl;
        private System.Windows.Forms.ToolStripMenuItem refreshSerialConnectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshSDI12ConnectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataLoggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataRetrievalToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog findFirmwareDialog;
        private System.Windows.Forms.ToolStripMenuItem enterIPToolStripMenuItem;
    }
}


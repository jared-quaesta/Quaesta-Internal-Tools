using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPM_General_App.Utilities
{
    public partial class FWUpgradeTCP : Form
    {
        byte[] fwBin;
        TCPNPMLink link;
        internal FWUpgradeTCP(TCPNPMLink link)
        {
            this.link = link;
            InitializeComponent();
        }

        private void ChooseFWAsync(object sender, EventArgs e)
        {
            findFirmwareDialog.ShowDialog();
            if (File.Exists(findFirmwareDialog.FileName)) upgradeBtn.Enabled = true;
            string[] name = findFirmwareDialog.FileName.Split('\\');
            textProgress.Text = $"Selected: {name[name.Length - 1]}";
        }

        internal ProgressBar getPB()
        {
            return transProgress;
        }
        internal Label getPt()
        {
            return textProgress;
        }

        private void upgradeBtn_Click(object sender, EventArgs e)
        {
            link.tcpMan.UpdateFirmware(findFirmwareDialog.FileName, this);
            upgradeBtn.Enabled = false;
        }

        private void FWUpgradeTCP_Load(object sender, EventArgs e)
        {

        }
    }
}

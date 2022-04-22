using NPM_General_App.SerialNPM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NPM_General_App.Utilities
{
    public partial class FWUpgradeSerial : Form
    {
        private SerialNPMLink link;

        internal FWUpgradeSerial(SerialNPMLink link)
        {
            this.link = link;
            InitializeComponent();
        }

        private void chooseFWBtn_Click(object sender, EventArgs e)
        {
            findFirmwareDialog.ShowDialog();
            if (File.Exists(findFirmwareDialog.FileName)) upgradeBtn.Enabled = true;
            string[] name = findFirmwareDialog.FileName.Split('\\');
            textProgress.Text = $"Selected: {name[name.Length - 1]}";
        }

        private void upgradeBtn_Click(object sender, EventArgs e)
        {
            link.UpdateNPM(findFirmwareDialog.FileName, this);
        }

        internal ProgressBar getPB()
        {
            return transProgress;
        }
        internal Label getPt()
        {
            return textProgress;
        }
    }
}

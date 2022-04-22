using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIY_Interface__IAEA_
{
    public partial class FWUpgradeTCP : Form
    {
        byte[] fwBin;
        TCPManager tcpMan;
        internal FWUpgradeTCP(TCPManager tcpMan)
        {
            this.tcpMan = tcpMan;
            InitializeComponent();
        }
        private void ChooseFWAsync(object sender, EventArgs e)
        {
            findFirmwareDialog.ShowDialog();
            if (File.Exists(findFirmwareDialog.FileName) && findFirmwareDialog.FileName.ToLower().Contains(".crp")) sendFWBtn.Enabled = true;
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
            if (!File.Exists(findFirmwareDialog.FileName)) return;
            tcpMan.UpdateFirmware(findFirmwareDialog.FileName, this);
            sendFWBtn.Enabled = false;
            chooseFWBtn.Enabled = false;
            
        }
        private void FWUpgradeTCP_Load(object sender, EventArgs e)
        {

        }
    }
}

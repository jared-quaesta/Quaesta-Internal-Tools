using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HardRebootQIY
{
    public partial class Form1 : Form
    {
        List<TCPNPMManager> tcpMans = new List<TCPNPMManager>();
        DLManager dlMan = new DLManager();
        string path = "";
        int nRCs = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void pathFindBtn_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            path = folderBrowserDialog1.SelectedPath;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // establish connection to DL and QIYs
            foreach (string ip in seeFile.Items)
            {
                TCPNPMManager man = new TCPNPMManager(ip);
                tcpMans.Add(man);
                man.AddListener(new StatsManager(this));
            }
            dlMan.Connect(dlMan.GetCom());
            
        }

        private void beginTest_Click(object sender, EventArgs e)
        {
            if (testLoop.IsBusy) return;
            testLoop.RunWorkerAsync();
        }

        private async void TestSegment(object sender, DoWorkEventArgs e)
        {
            dlMan.SendCommand("Relays=1\r\n");

            foreach (TCPNPMManager man in tcpMans)
            {
                man.rebooting = true;
                await man.GotInfo();
                Debug.WriteLine("Got info");
            }

            dlMan.SendCommand("Relays=0\r\n");
            
            Thread.Sleep(2000);
        }

        private void ShowResults(object sender, RunWorkerCompletedEventArgs e)
        {
            nRCs++;
            string selIP = "p";
            if (seeFile.SelectedItems.Count > 0)
            {
                selIP = seeFile.SelectedItem.ToString().Split(' ')[0];
            }

            seeFile.Items.Clear();
            string lastInfo = "";
            foreach (TCPNPMManager man in tcpMans)
            {
                List<string> errs = man.GetErrs();
                lastInfo = man.getLast();
                int i = seeFile.Items.Add(man.GetIP() + $" ({errs.Count} Errs)");
                if (selIP.Equals(man.GetIP()))
                {
                    if (errs.Count > 0)
                    infoStringView.Text = errs[0];
                    seeFile.SelectedIndex = i;
                }        
            }
            if (seeFile.SelectedItems.Count == 0)
                infoStringView.Text = lastInfo;
            nrcs.Text = $"Reconnects: {nRCs}";

            Refresh();
            testLoop.RunWorkerAsync();
        }
    }
}

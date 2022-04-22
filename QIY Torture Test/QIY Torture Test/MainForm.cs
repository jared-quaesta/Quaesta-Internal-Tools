using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIY_Torture_Test
{
    public partial class MainForm : Form
    {
        UDPManager udpMan;
        DLManager dlMan;
        List<TCPNPMManager> inTesting = new List<TCPNPMManager>();
        CycleOptionsForm cycleForm = new CycleOptionsForm();
        public MainForm()
        {
            InitializeComponent();
        }

        private void refreshTCP_Click(object sender, EventArgs e)
        {
            // refresh both tcp connections and dataloggers
            dlsbox.Items.Clear();
            npmSelect.Items.Clear();

            //begin udp message
            udpMan.SearchNeuchQIY();
            //get dl coms
            foreach (string com in DLManager.GetComs("Serial Port"))
            {
                dlsbox.Items.Add(com);
            }
            if (dlsbox.Items.Count != 0)
            {
                dlsbox.SelectedIndex = 0;
            }
        }

        internal async void ParseUDPResponse(string data)
        {
            string[] incomingData = data.Split('\n');
            if (data.Length == 0) return;

            npmSelect.Invoke((MethodInvoker)async delegate {
                foreach (string incoming in incomingData)
                {
                    if (incoming.Split(',').Length == 1) continue;
                    // 70B3D588F0D5,C0A80FD5,FFFFFF00,C0A80F01,10001,QIY_A_100,NE0D5
                    Debug.WriteLine(incoming);
                    string ip, port, mac, serial;
                    /////////////////// IP ///////////////////
                    ip = "";
                    if (incoming.Split(',').Length > 1)
                    {
                        IPAddress parsedIP = new IPAddress(long.Parse(incoming.Split(',')[1], NumberStyles.AllowHexSpecifier));

                        

                        string[] arr = parsedIP.ToString().Split('.');
                        foreach (string sect in arr)
                        {
                            ip = sect + "." + ip;
                        }

                        ip = ip.Trim('.');

                        TCPNPMManager man = new TCPNPMManager(ip);
                        if (!await man.TryConnectionAsync()) continue;
                        man.Disconnect();
                        //if (TCPLinkTracking.ContainsKey(ip)) continue;
                    }

                    ///////////////// FW /////////////////////
                    string fw = incoming.Split(',')[5];

                    ///////////////// SERIAL //////////////////
                    serial = incoming.Split(',')[5];

                    ///////////////// MAC ////////////////////
                    string macIndex = incoming.Split(',')[0];
                    mac = "";
                    for (int i = 0; i < macIndex.Length; i += 2)
                    {
                        mac += macIndex.Substring(i, 2) + " : ";
                    }
                    mac = mac.Trim(':', ' ');

                    if (!npmSelect.Items.Contains(ip))
                    npmSelect.Items.Add($"{ip}");

                }
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            udpMan = new UDPManager(this);
        }


        private void TryConnectDL(object sender, EventArgs e)
        {
            if (dlMan == null)
            {
                dlMan = new DLManager(dlsbox.Text);
            }
            else
            {
                dlMan.Disconnect();
            }

            if (dlMan.Connect(dlsbox.Text))
            {
                Debug.WriteLine("Connected to DL");
                dlConnStatus.Text = "Connected";
                dlConnStatus.ForeColor = Color.Green;
                relayOffBtn.Enabled = true;
                relayOnBtn.Enabled = true;
            }
            else
            {
                Debug.WriteLine("Failed to connect to DL");
                dlConnStatus.Text = "Not Connected";
                dlConnStatus.ForeColor = Color.Red;
                relayOffBtn.Enabled = false;
                relayOnBtn.Enabled = false;
            }
            

        }

        private void relayOnBtn_Click(object sender, EventArgs e)
        {
            dlMan.SendCommand("relays=9\r\n");
        }

        private void relayOffBtn_Click(object sender, EventArgs e)
        {
            dlMan.SendCommand("relays=0\r\n");
        }

        private void searchPath_Click(object sender, EventArgs e)
        {
            folderBrowser.ShowDialog();
            s1PathBox.Text = folderBrowser.SelectedPath;
        }

        private void beginStageOne_Click(object sender, EventArgs e)
        {
            if (dlMan == null) return;
            if (!dlMan.IsConnected()) return;
            if (npmSelect.CheckedItems.Count == 0) return;
            if (!Directory.Exists(s1PathBox.Text)) return;

            Debug.WriteLine("All set!");
            
            foreach (string ip in npmSelect.CheckedItems)
            {
                inTesting.Add(new TCPNPMManager(ip));
            }

            TestForm s1Test = new TestForm(s1PathBox.Text, inTesting, dlMan, cycleForm);

            s1Test.ShowDialog();
            inTesting.Clear();
            


        }

        private void AddNewIP(object sender, MouseEventArgs e)
        {
            Debug.WriteLine(e.Button);
            if (e.Button == MouseButtons.Right)
            {
                NewIPForm newIp = new NewIPForm();
                newIp.ShowDialog();
                if (!newIp.ip.Equals(""))
                {
                    npmSelect.Items.Add(newIp.ip);
                }
            }
            
        }

        private void selAllBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < npmSelect.Items.Count; i++)
                npmSelect.SetItemCheckState(i, CheckState.Checked);
        }

        private void selNoneBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < npmSelect.Items.Count; i++)
                npmSelect.SetItemCheckState(i, CheckState.Unchecked);
        }

        private void cycleOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cycleForm.ShowDialog();
        }
    }
}

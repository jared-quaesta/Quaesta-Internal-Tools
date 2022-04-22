using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIYRapidTest
{
    public partial class MainForm : Form
    {
        private Dictionary<string, TextBox> consolePair = new Dictionary<string, TextBox>();
        private Dictionary<string, TCPManager> connected = new Dictionary<string, TCPManager>();

        private Dictionary<string, Tuple<int, double, int[], double, int[]>> testDevData = new Dictionary<string, Tuple<int, double, int[], double, int[]>>();

        private readonly Size consoleOutSize = new Size(990, 425);
        private readonly Size consoleInSize = new Size(990, 23);

        private UdpClient udpClient = new UdpClient();

        private int DISCPORT = 15334;


        private TestSuite testSuite;

        RichTextBox testResultBox = new RichTextBox() 
        {
            Size = new Size(1013, 500),
            Location = new Point(439, 12),
            ReadOnly = true

        };

        private string curTab = "";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            consolesCollect.Controls.Clear();
            // For rapid test, remove in actual test
            npmsSelect.Items.Add("192.168.15.232");
            //npmsSelect.Items.Add("192.168.15.241");
            //npmsSelect.Items.Add("192.168.15.231");
            //npmsSelect.Items.Add("192.168.15.205");
            //npmsSelect.Items.Add("192.168.15.213");
            //npmsSelect.Items.Add("192.168.15.232");


            //udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, DISCPORT));


            cmdBox.Text = $"voltage{Environment.NewLine}uptime{Environment.NewLine}battery";

            discoveryWorker.RunWorkerAsync();
            listenerWorker.RunWorkerAsync();
        }

        private async void connectButton_ClickAsync(object sender, EventArgs e)
        {
            consolesCollect.Controls.Clear();
            consolePair.Clear();
            connected.Clear();
            curTab = "";
            // generate dict, connect indidually
            foreach (string checkedNpm in npmsSelect.CheckedItems)
            {
                TabPage consoleTab = new TabPage(checkedNpm);
                consoleTab.Name = checkedNpm;
                if (curTab.Length == 0) curTab = checkedNpm;
                TCPManager man = new TCPManager(checkedNpm);
                connected.Add(checkedNpm, man);

                TextBox consoleOut = new TextBox();
                consoleOut.Multiline = true;
                consoleOut.Size = consoleOutSize;
                consoleOut.Location = new Point(5, 5);
                consoleTab.Controls.Add(consoleOut);
                consoleOut.ScrollBars = ScrollBars.Vertical;
                consoleOut.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point);
                consolePair.Add(checkedNpm, consoleOut);

                consoleOut.Text = "Trying to connect..\n\n";
                bool success = await man.TryConnection();
                if (success)
                {
                    consoleOut.Text = "Connected.\n\n";
                }
                else
                {
                    consoleOut.Text = "Failed to Connect.\n\n";
                }


                TextBox consoleIn = new TextBox();
                consoleIn.Multiline = true;
                consoleIn.Size = consoleInSize;
                consoleIn.Location = new Point(5, 440);
                consoleIn.KeyDown += (sender, e) => SendCommand(sender, e, checkedNpm, man);
                consoleIn.TextChanged += (sender, e) =>
                {
                    man.SendCommand(consoleIn.Text);
                    consoleIn.Text = "";
                };
                consoleTab.Controls.Add(consoleIn);

                consolesCollect.Controls.Add(consoleTab);
            }
            if (npmsSelect.CheckedItems.Count != 0)
                connectButton.Text = $"Connected to {npmsSelect.CheckedItems.Count} NPMs.";


        }

        private async void SendCommand(object sender, KeyEventArgs e, string key, TCPManager man)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox consoleIn = (TextBox)sender;

                if (consoleIn.Text == "clear")
                {
                    man.ClearData();
                    consoleIn.Text = "";
                    consolePair[key].Text = "";
                    return;
                }

                bool success = await man.SendCommand(consoleIn.Text + "\r\n");

                if (!success)
                {
                    consolePair[key].AppendText("Failed to send command.\n\n");
                }
                consoleIn.Text = "";
            }

        }

        private void ListenDelay(object sender, DoWorkEventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (!listenerWorker.CancellationPending)
            {
                if (watch.ElapsedMilliseconds > 200)
                {
                    listenerWorker.ReportProgress(0);
                    watch.Restart();
                }
                Thread.Sleep(100);
            }
        }

        private void CheckBuffer(object sender, ProgressChangedEventArgs e)
        {
            if (curTab.Length == 0) return;

            TextBox consoleOut = consolePair[curTab];
            consoleOut.AppendText(connected[curTab].GetData());

        }

        private void BeginTest(object sender, EventArgs e)
        {
            if (connected.Keys.Count == 0) return;
         
            if (testSuite != null)
            {
                testSuite.StopTest();
                testSuite = null;
                beginTestBtn.Enabled = false;
                return;
            }

            testFolderDialog.ShowDialog();
            

            beginTestBtn.ForeColor = Color.Red;
            beginTestBtn.Text = "Stop Test";
            listenerWorker.CancelAsync();
            consolesCollect.Visible = false;
            //beginTestBtn.Enabled = false;

            
            testResultBox.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Controls.Add(testResultBox);

            // start test 

            ArrayList devs = new ArrayList();
            foreach (string ip in connected.Keys)
            {
                devs.Add(connected[ip]);
                testDevData.Add(ip, new Tuple<int, double, int[], double, int[]>(0, 0, new int[] {0, 0}, 0, new int[] { 0, 0 }));
            }

            ArrayList cmds = new ArrayList() 
            {
                "uptime",
            };

            testSuite = new TestSuite(devs, cmds, testFolderDialog.SelectedPath);
            testSuite.newData += UpdateTestResults;
            testSuite.RunTest();

        }

        // individual mode
        public void UpdateTestResults(object sender, TestArgs args)
        {
            testResultBox.Invoke((MethodInvoker)delegate
            {
                int[] minMax = args.minMax;
                int connectionAttempts = args.connectAttempts;
                string ip = args.ip;
                double uptime = args.UpTime;

                // update average connect time
                int currentT = testDevData[ip].Item1;
                double newAveReconnectAttempts = ((double)(testDevData[ip].Item2 * currentT) + connectionAttempts) / (currentT + 1);

                int connectfail = testDevData[ip].Item5[0];
                int sendfail = testDevData[ip].Item5[1];

                if (connectionAttempts == 3)
                    connectfail++;
                if (minMax[0] > 250)
                    sendfail++;

                testDevData[ip] = new Tuple<int, double, int[], double, int[]>(currentT+1, newAveReconnectAttempts, minMax, uptime, new int[] {connectfail, sendfail });

                testResultBox.Clear();
                foreach (string ipKey in testDevData.Keys)
                {
                    Tuple<int, double, int[], double, int[]> data = testDevData[ipKey];

                    testResultBox.SelectionColor = Color.Green;
                    testResultBox.AppendText($"IP: {ipKey}{Environment.NewLine}");
                    testResultBox.SelectionColor = testResultBox.ForeColor;
                    testResultBox.AppendText($"Average Attempts to reconnect:    {data.Item2:0.00}         CONNECTION Failures: {data.Item5[0]}{Environment.NewLine}");
                    testResultBox.AppendText($"Min time to recieve command:      {data.Item3[0]}ms       SEND CMD   Failures: {data.Item5[1]}{Environment.NewLine}");
                    testResultBox.AppendText($"Max time to recieve command:      {data.Item3[1]}ms{Environment.NewLine}");
                    testResultBox.AppendText($"Total Reconnects: {data.Item1}{Environment.NewLine}{Environment.NewLine}");
                    
                }
                testResultBox.AppendText($"Total Uptime: {TimeSpan.FromMilliseconds(args.UpTime).ToString(@"hh\:mm\:ss")}");
            });
        }


        private void ChangedTabs(object sender, TabControlCancelEventArgs e)
        {
            if (consolesCollect.SelectedTab == null) return;
            curTab = consolesCollect.SelectedTab.Text;
            Debug.WriteLine(curTab);
        }

        private void SearchBtnClick(object sender, EventArgs e)
        {
            if (!discoveryWorker.IsBusy)
            {
                discoveryWorker.RunWorkerAsync();
            }
            npmsSelect.Items.Clear();
            var data = Encoding.UTF8.GetBytes("neuchrometer report\r\n");
            udpClient.Send(data, data.Length, "255.255.255.255", DISCPORT);
        }

        private void UDPBroadcast(object sender, DoWorkEventArgs e)
        {
            try
            {
                udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, DISCPORT));
            } catch
            {
                MessageBox.Show("Already Searching....");
            }
            udpClient.Client.ReceiveTimeout = 100;
            
            var from = new IPEndPoint(0, 0);
            Stopwatch sw = Stopwatch.StartNew();

            while (!discoveryWorker.CancellationPending && sw.ElapsedMilliseconds < 5000)
            {
                try
                {
                    var recvBuffer = udpClient.Receive(ref from);
                    discoveryWorker.ReportProgress(1, Encoding.UTF8.GetString(recvBuffer));
                }
                catch { }
                
                
                
                Debug.WriteLine(sw.ElapsedMilliseconds);
            }
            Debug.WriteLine("Stopped listening.");
            udpClient.Close();
            udpClient = new UdpClient();
        }

        private void AddIP(object sender, ProgressChangedEventArgs e)
        {
            string res = (string)e.UserState;
            if (res.Split(',').Length > 1)
            {
                
                IPAddress parsedIP = new IPAddress(long.Parse(res.Split(',')[1], NumberStyles.AllowHexSpecifier));
                string[] arr = parsedIP.ToString().Split('.');
                string newIP = "";
                foreach (string sect in arr)
                {
                    newIP = sect + "." + newIP;
                }
                newIP = newIP.Trim('.');

                npmsSelect.Items.Add(newIP);
            }
        }
    }
}

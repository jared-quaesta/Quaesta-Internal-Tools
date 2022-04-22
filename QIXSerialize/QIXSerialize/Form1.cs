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

namespace QIXSerialize
{
    public partial class Form1 : Form
    {
        DevManager devMan = new DevManager();
        string port;

        Dictionary<string, string> monthlookup = new Dictionary<string, string>
        {
            { "JAN", "01" },
            { "FEB", "02" },
            { "MAR", "03" },
            { "APR", "04" },
            { "MAY", "05" },
            { "JUN", "06" },
            { "JUL", "07" },
            { "AUG", "08" },
            { "SEP", "09" },
            { "OCT", "10" },
            { "NOV", "11" },
            { "DEC", "12" },
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            port = devMan.GetCom();
            devMan.Connect(port);
            comName.Text = devMan.comName;

            devSearch.WorkerReportsProgress = true;
            devSearch.WorkerSupportsCancellation = true;
            devSearch.RunWorkerAsync();
        }

        private void waitTimer(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
        }


        private void CheckNewDev(object sender, RunWorkerCompletedEventArgs e)
        {
            devMan.SendCommand("iinnffoo\r\n");

            string read = devMan.Read();
            string[] output = read.Split('\n');
            //this.output.AppendText(read);
            Debug.WriteLine(read);
            if (output.Length < 2)
            {
                Status.ForeColor = Color.Red;
                Status.Text = "NOT CONNECTED";
                conSn.Text = $"";
                Update();
                devSearch.RunWorkerAsync();
                return;
            }
            Status.ForeColor = Color.Green;
            Status.Text = "CONNECTED";
            
            Update();
            foreach (string line in output)
            {
                if (line.Contains("Serial Number"))
                {
                    string[] split = line.Split(' ');
                    string sn = split[split.Length - 1].Trim();
                    conSn.Text = $"Connected: {sn}";

                    if (sn.Contains('-'))
                        ChangeSn(sn);
                    else
                    {
                        RunBasicParams();
                        Console.Beep();
                    }
                }
            }

            devSearch.RunWorkerAsync();

        }

        private void ChangeSn(string sn)
        {
            bool secret = false;
            int timeout = 0;
            while (!secret)
            {
                if (timeout == 10)
                {
                    return;
                }
                timeout++;
                devMan.AllowSecret();
                string readout = devMan.Read(100);
                if (readout.Contains("Secret Commands are now accessible")) secret = true;

                output.AppendText(readout);
                Update();
                Thread.Sleep(100);
            }

            //AUG2021 - 030

            string givMonth = sn.Substring(0, 3);
            string month = monthlookup[givMonth];
            string year = sn.Substring(4, 3).Substring(1);
            string devNum = $"{sn.Split('-')[1].Trim():0000}";

            string newsn = MakeReadable($"serialnumber={year}{month}0{devNum}");
            devMan.SendCommand($"{newsn}\r\r");
            
            RunBasicParams(1);
            

        }

        private void RunBasicParams(int i=0)
        {
            if (i == 0)
            {

            bool secret = false;
            int timeout = 0;
            while (!secret)
            {
                if (timeout == 10)
                {
                    return;
                }
                timeout++;
                devMan.AllowSecret();
                string readout = devMan.Read(100);
                if (readout.Contains("Secret Commands are now accessible")) secret = true;

                
                Update();
                Thread.Sleep(100);
            }

            }

            devMan.SendCommand($"{MakeReadable($"hgmmode=3")}\r\r"); Thread.Sleep(50);
            devMan.SendCommand($"{MakeReadable($"sensorson=1")}\r\r"); Thread.Sleep(50);
            //devMan.SendCommand($"SENSORSON=1\r\r"); Thread.Sleep(50);
            devMan.SendCommand($"{MakeReadable($"maxvoltage=1700")}\r\r"); Thread.Sleep(50);
            //devMan.SendCommand($"maxvoltage=1700\r\r"); Thread.Sleep(50);
            devMan.SendCommand($"{MakeReadable($"nbins=64")}\r\r"); Thread.Sleep(50);
            //devMan.SendCommand($"nbins=64\r\r"); Thread.Sleep(50);
            //devMan.SendCommand($"hgmmode=3\r\r"); Thread.Sleep(50);

            output.AppendText(devMan.Read());
            Update();
        }

        private string MakeReadable(string sn)
        {
            string ret = "";

            foreach (char i in sn)
            {
                ret += $"{i}{i}";
            }

            return ret;

        }

        private void DisposeForm(object sender, FormClosedEventArgs e)
        {
            devMan.Disconnect();
            Dispose();
        }
    }
}

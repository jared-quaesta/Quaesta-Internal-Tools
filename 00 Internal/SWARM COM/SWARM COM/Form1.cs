using KD.Telnet.TcpTelnetClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWARM_COM
{
    public partial class MainForm : Form
    {
        public static string latestOut = "";
        public IPAddress ipa = IPAddress.Parse("192.168.15.110");
        private int port = 23;
        private TimeSpan timeout = TimeSpan.FromSeconds(2.0);
        ITcpTelnetClient telnetClient = new TcpTelnetClient();
        private bool pingOn = false;
        private bool runtest = false;

        
        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                telnetClient.ConnectAsync(ipa, port);
            });
            Thread.Sleep(300);
            //var data = telnetClient.ReceiveData(timeout).ConfigureAwait(false);
            outBox.AppendText($"Connected: {telnetClient.IsConnected()}{Environment.NewLine}");
            checkOutWorker.RunWorkerAsync();
        }

        private async void tcpButton_Click(object sender, EventArgs e)
        {
            if (!telnetClient.IsConnected()) return;
            outBox.AppendText($"$FV*10{Environment.NewLine}");
            await Task.Run(() =>
            {
                telnetClient.SendData("$FV*10\r\n");
            });
        }

        private void CheckOutput(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(100);
        }

        private async void UpdateUI(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!telnetClient.IsConnected()) return;
            try
            {
                var data = await Task.Run(() =>
                {
                    return telnetClient.ReceiveData(timeout);
                });
                outBox.AppendText($"{data}{Environment.NewLine}");
                if (data.Contains('-') && !data.Contains(','))
                {
                    string rrsi = "SSRI: ";
                    string total = "";
                    for (int i = data.IndexOf('-'); i < data.IndexOf('*'); i++)
                    {
                        rrsi += data[i];
                        total += data[i];
                    }

                    if (Int32.Parse(total) > -97)
                    {
                        rrsiLabel.ForeColor = Color.Red;
                    }
                    else
                    {
                        rrsiLabel.ForeColor = Color.Green;
                    }
                    rrsiLabel.Text = rrsi;
                }
            } 
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine("No new data");
            }
            checkOutWorker.RunWorkerAsync();
        }

        private async void rssiButton_Click(object sender, EventArgs e)
        {
            if (!telnetClient.IsConnected()) return;
            pingOn = !pingOn;
            if (pingOn)
            {
                outBox.AppendText($"$RT 0*16{ Environment.NewLine}");
                await Task.Run(() =>
                {
                    telnetClient.SendData("$RT 0*16\r\n");
                });
            }
            else
            {
                outBox.AppendText($"$RT 1*17{ Environment.NewLine}");

                await Task.Run(() =>
                {
                    telnetClient.SendData("$RT 5*13\r\n");
                });
            }
        }

        private async void SendMessage(object sender, KeyEventArgs e)
        {
            if (!telnetClient.IsConnected()) return;
            if (sendCommandBox.Text.Length == 0) return;
            if (e.KeyCode == Keys.Enter)
            {
                string text = "TD " + sendCommandBox.Text + '*';
                string com = CalculateNMEA(text);
                sendCommandBox.Text = "";
                outBox.AppendText($"{com}{Environment.NewLine}");
                await Task.Run(() =>
                {
                    telnetClient.SendData($"{com}\r\n");
                });
            }
        }

        private string CalculateNMEA(string sentence)
        {
            //Start with first Item
            int checksum = Convert.ToByte(sentence[sentence.IndexOf('$') + 1]);
            // Loop through all chars to get a checksum
            for (int i = sentence.IndexOf('$') + 2; i < sentence.IndexOf('*'); i++)
            {
                // No. XOR the checksum with this character's value
                checksum ^= Convert.ToByte(sentence[i]);
            }
            // Return the checksum formatted as a two-character hexadecimal
            return '$' + sentence + checksum.ToString("X2");
        }

        private async void NewIp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ipa = IPAddress.Parse(ipConfig.Text);
                telnetClient = new TcpTelnetClient();
                await Task.Run(() =>
                {
                    telnetClient.ConnectAsync(ipa, port);
                });

            }
        }

        private void WaitBetween(object sender, DoWorkEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int i = 0;
            runTestWorker.ReportProgress(i);
            i++;
            long lastMs = 0;
            while (!runTestWorker.CancellationPending)
            {

                //if (lastMs != sw.ElapsedMilliseconds)
                //{
                    //runTestWorker.ReportProgress(-1, (int)(sw.ElapsedMilliseconds / 60000));
                    //lastMs = sw.ElapsedMilliseconds;
                //}
                if (i == 64) break;
                if (sw.ElapsedMilliseconds > 500)
                {
                    runTestWorker.ReportProgress(i);
                    sw.Restart();
                    i++;
                }
            }
        }

        private async void ExcecuteTest(object sender, ProgressChangedEventArgs e)
        {

            if (e.ProgressPercentage == -1)
            {
                nextTimeLbl.Text = $"Next: {30 - (int)e.UserState}m";
                return;
            }

            string num = e.ProgressPercentage.ToString("X");
            string text = $"TD {num.PadLeft(4, '0')}123456789abcdef101112131415161718191a1b1c1d1e1f202122232425262728292a2b2c2d2e2f303132333435363738393a3b3c3d3e3f404142434445464748494a4b4c4d4e4f505152535455565758595a5b5c5d5e5f606162636465666768696a6b6c6d6e6f707172737475767778797a7b7c7d7e7f808182838485868788898a8b8c8d8e8f909192939495969798999a9b9c9d9e9afa0a1a2a3a4a5a6a7a8a9aaabacadaeafb0b1b2b3b4b5b6b7b8b9babbbcbdbebfc0c1c2c3c4c5*";
            string com = CalculateNMEA(text);
            sendCommandBox.Text = "";
            outBox.AppendText($"{com}{Environment.NewLine}");
            lastMsg.Text = $"{num.PadLeft(4, '0')}123456789abcdef101112131415161718191a1b1c1d1e1f202122232425262728292a2b2c2d2e2f303132333435363738393a3b3c3d3e3f404142434445464748494a4b4c4d4e4f505152535455565758595a5b5c5d5e5f606162636465666768696a6b6c6d6e6f707172737475767778797a7b7c7d7e7f808182838485868788898a8b8c8d8e8f909192939495969798999a9b9c9d9e9afa0a1a2a3a4a5a6a7a8a9aaabacadaeafb0b1b2b3b4b5b6b7b8b9babbbcbdbebfc0c1c2c3c4c5";
            await Task.Run(() =>
            {
                telnetClient.SendData($"{com}\r\n");
            });

        }

        private void runTestButton_Click(object sender, EventArgs e)
        {
            runtest = !runtest;
            if (runtest)
            {
                runTestWorker.WorkerReportsProgress = true;
                runTestWorker.WorkerSupportsCancellation = true;
                runTestWorker.RunWorkerAsync();

            }
            else
            {
                runTestWorker.CancelAsync();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void mtCheck_Click(object sender, EventArgs e)
        {
            string text = "MT C=U*";
            string com = CalculateNMEA(text);
            sendCommandBox.Text = "";
            outBox.AppendText($"{com}{Environment.NewLine}");
            await Task.Run(() =>
            {
                telnetClient.SendData($"{com}\r\n");
            });
        }
    }

}




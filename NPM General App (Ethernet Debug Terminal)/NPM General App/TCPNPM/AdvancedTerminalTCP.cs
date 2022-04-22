using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPM_General_App
{
    public partial class AdvancedTerminalTCP : Form
    {
        TCPNPMLink link;
        string lastInputCommand = "NOLASTINPUTCOMMAND";
        List<string> cmdsList = new List<string>();
        bool gotResponse = false;

        // PLOT
        LineSeries timeoutSeries = new LineSeries() { Color = OxyColors.Green };
        LineSeries maxSeries = new LineSeries() {Color = OxyColors.Red };
        LineSeries rcSeries = new LineSeries() {Color = OxyColors.Blue };
        PlotModel model = new PlotModel();
        Axis xAxis = new LinearAxis()
        {
            Title = "Runtime (s)",
            Position = AxisPosition.Bottom,
            FontSize = 10

        };

        Axis yAxis = new LinearAxis()
        {
            Title = "Response Time (ms)",
            Position = AxisPosition.Left,
            FontSize = 10,
            Minimum = 0,
            Key = "default"

        };

        Axis yAxisRC = new LinearAxis()
        {
            Title = "Reconnect Attempts",
            Position = AxisPosition.Right,
            FontSize = 10,
            Minimum = 0,
            Maximum = 4,
            Key = "RC"


        };

        bool stopWorker = false;
        bool debugRunning = false;


        internal AdvancedTerminalTCP(TCPNPMLink link)
        {
            this.link = link;
            InitializeComponent();
        }

        private void AdvancedTerminal_Load(object sender, EventArgs e)
        {
            npmIP.Text = link.tcpMan.GetIP();

            if (link.tcpMan.IsConnectedShallow())
            {
                connectionBtn.ForeColor = Color.Green;
                connectionBtn.Text = "Connected";
            } else
            {
                connectionBtn.ForeColor = Color.Red;
                connectionBtn.Text = "Disconnected";
            }

            
            model.Axes.Clear();
            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);
            model.Axes.Add(yAxisRC);
            timeoutSeries.YAxisKey = "default";
            maxSeries.YAxisKey = "default";
            rcSeries.YAxisKey = "RC";
            model.Series.Add(timeoutSeries);
            model.Series.Add(rcSeries);
            model.Series.Add(maxSeries);
            timeoutPlot.Model = model;
            

            // AutoScroll
            advTermOut.TextChanged += (sender, e) =>
            {
                advTermOut.SelectionStart = advTermOut.Text.Length;
                // scroll it automatically
                advTermOut.ScrollToCaret();
            };
        }

        internal void NewData(string data)
        {
            /// Split up data by newline index
            List<string> parsed = SplitData(data);


            foreach (string dataSplit in parsed)
            {
                if (dataSplit.Length == 0) continue;
                
                string asActualOutput = dataSplit.Replace("\n", "\\n").Replace("\r", "\\r");
                string checkInput = dataSplit.ToLower().TrimEnd('\r', '\n');
                if (checkInput.Equals(lastInputCommand.ToLower()))
                {
                    AppendAsColor(Color.Blue, asActualOutput, true);
                }
                else if (cmdsList.Contains(checkInput))
                {
                    AppendAsColor(Color.Green, asActualOutput, true);
                }
                else
                {
                    AppendAsColor(Color.Black, asActualOutput);
                    if (asActualOutput.Contains("="))
                        gotResponse = true;
                }
            }
            
        }

        private List<string> SplitData(string data)
        {
            //Debug.WriteLine(data.IndexOf("\n"));
            List<string> ret = new List<string>();
            while (data.IndexOf("\n") != -1)
            {
                ret.Add(data.Substring(0, data.IndexOf('\n')+1));
                data = data.Substring(data.IndexOf('\n')+1);
            }
            ret.Add(data);
            return ret;
        }

        private void AppendAsColor(Color color, string data, bool bold = false)
        {
            advTermOut.SelectionStart = advTermOut.TextLength;
            advTermOut.SelectionLength = 0;
            advTermOut.SelectionColor = color;
            if (bold)
            {
                advTermOut.SelectionFont = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point);
            } else
            {
                advTermOut.SelectionFont = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
            }
            advTermOut.AppendText(data);
            if (data.Contains("\\n"))
            {
                advTermOut.AppendText(Environment.NewLine);
            }
        }


        private void HideForm(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            advTermOut.Clear();
            gotResponse = false;
        }

        private void SendCommand(object sender, KeyEventArgs e)
        {
            if (byteAtATimeCheck.Checked)
            {
                link.tcpMan.SendCommandAsync(e.KeyData.ToString());
                e.SuppressKeyPress = true;
            }


            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                link.tcpMan.SendCommandAsync(advTermIn.Text.Trim() + "\r\n") ;
                lastInputCommand = advTermIn.Text.ToLower();
                advTermIn.Clear();

            }
        }

        private void UpdateCommands(object sender, EventArgs e)
        {
            cmdsList.Clear();
            string input = commandsList.Text.Trim();
            foreach (string cmd in input.Split('\n'))
            {
                cmdsList.Add(cmd.Trim('\r', '\n').ToLower());
            }

        }

        private void crBttn_Click(object sender, EventArgs e)
        {
            link.tcpMan.SendCommandAsync("\r");
        }

        private void lfBttn_Click(object sender, EventArgs e)
        {
            link.tcpMan.SendCommandAsync("\n");
        }

        private void runCmds_Click(object sender, EventArgs e)
        {
            if (runCmds.Text.Trim().Length == 0) return;
            
            if (debugRunning)
            {
                stopWorker = true;

                runCmds.Text = "Run Debug Commands";
                runCmds.ForeColor = Color.DarkBlue;

            } else
            {
                stopWorker = false;
                debugWorker.RunWorkerAsync();

                runCmds.Text = "Stop Debug Commands";
                runCmds.ForeColor = Color.Red;
            }
            debugRunning = !debugRunning;

        }

        private async void RunDebugCommandsAsync(object sender, DoWorkEventArgs e)
        {
            // get debug params
            timeoutSeries.Points.Clear();
            maxSeries.Points.Clear();
            rcSeries.Points.Clear();
            gotResponse = false;
            int rcDelay = Int32.Parse(rcWait.Text);
            int lineDelay = Int32.Parse(lineDelayms.Text);
            int byteDelay = Int32.Parse(byteDelayms.Text);
            int responseTimeout = Int32.Parse(responseTimeoutBox.Text);
            int N = Int32.Parse(runNTimes.Text);
            string endCmd = "\r\n";
            if (crRadio.Checked)
                endCmd = "\r";
            else if (lfRadio.Checked)
                endCmd = "\n";
            List<string> commands = new List<string>(cmdsList);

            yAxis.Maximum = responseTimeout + 10;
            int dcrcRetries = Int32.Parse(retriesAllowed.Text);
            yAxisRC.Maximum = dcrcRetries + 1;

            Stopwatch runtime = Stopwatch.StartNew();

            for (int i = 0; i < N; i++)
            {
                if (stopWorker)
                    return;

                foreach (string cmd in commands)
                {
                    if (byteDelay == 0)
                    {
                        await link.tcpMan.SendCommandAsync(cmd + endCmd);
                    } else
                    {
                        foreach (char b in cmd + endCmd)
                        {
                            await link.tcpMan.SendCommandAsync(b.ToString());
                            Thread.Sleep(byteDelay);
                        }
                    }

                    long response = await Task.Run(() => 
                    {
                        Stopwatch sw = Stopwatch.StartNew();
                        while (sw.ElapsedMilliseconds <= responseTimeout)
                        {
                            if (gotResponse)
                            {
                                return sw.ElapsedMilliseconds;
                            }
                            Thread.Sleep(1);
                        }
                        return responseTimeout;
                    });
                    gotResponse = false;
                    UpdatePlot(response, responseTimeout, (double)runtime.ElapsedMilliseconds/1000);
                    Thread.Sleep(lineDelay);
                }
                
                if (rcCheck.Checked)
                {
                    link.tcpMan.Disconnect();
                    Thread.Sleep(rcDelay);

                    bool rc = await link.tcpMan.TryConnectionAsync();
                    for (int retries = 0; retries < dcrcRetries; retries++)
                    {
                        if (rc)
                        {
                            UpdateRCPlot(retries, (double)runtime.ElapsedMilliseconds / 1000);
                            break;
                        }
                        Thread.Sleep(rcDelay);
                        rc = await link.tcpMan.TryConnectionAsync();
                    }
                    Thread.Sleep(rcDelay);
                    // Three retries, give up, end test, see what happened
                    if (!link.tcpMan.IsConnectedShallow())
                    {
                        UpdateRCPlot(dcrcRetries, (double)runtime.ElapsedMilliseconds / 1000);
                        debugRunning = false;
                        link.main.Invoke((MethodInvoker)delegate
                        {
                            runCmds.Text = "Run Debug Commands";
                            runCmds.ForeColor = Color.DarkBlue;
                            MessageBox.Show("Maximum Reconnection Retries Reached: " +dcrcRetries);
                        });
                        return;
                    }


                }

            }
            debugRunning = false;
            link.main.Invoke((MethodInvoker)delegate
            {
                runCmds.Text = "Run Debug Commands";
                runCmds.ForeColor = Color.DarkBlue;
            });

        }

        private void UpdateRCPlot(int retries, double now)
        {
            rcSeries.Points.Add(new DataPoint(now, retries));
            model.InvalidatePlot(true);
        }

        private void UpdatePlot(long response, int timeout, double now)
        {
            double time = response;
            timeoutSeries.Points.Add(new DataPoint(now, time));
            maxSeries.Points.Add(new DataPoint(now, timeout));
            model.InvalidatePlot(true);
        }

        private async void connectionBtn_Click(object sender, EventArgs e)
        {
            if (!link.tcpMan.IsConnectedShallow())
            {
                connectionBtn.Enabled = false;
                connectionBtn.ForeColor = Color.Green;
                await link.tcpMan.TryConnectionAsync();
                connectionBtn.Enabled = true;
                connectionBtn.Text = "Connected";
            }
            else
            {
                connectionBtn.ForeColor = Color.Red;
                link.tcpMan.Disconnect();
                connectionBtn.Text = "Disconnected";
            }

        }

        private void flushIn_Click(object sender, EventArgs e)
        {
            link.tcpMan.SendCommandAsync("\r\n");
        }

        private void byteAtATimeCheck_CheckedChanged(object sender, EventArgs e)
        {
            advTermIn.Clear();
        }
    }
}

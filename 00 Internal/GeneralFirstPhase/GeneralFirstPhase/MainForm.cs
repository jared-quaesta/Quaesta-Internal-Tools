using OxyPlot;
using QIXLPTesting.SerialTools;
using QIXLPTesting.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIXLPTesting
{
    public partial class MainForm : Form
    {
        private const string voltDesc =
            "Given a desired voltage and range, query each npm as fast as possible. " +
            "Optionally ramp down to 0v at the end of the test.\n\n" +
            "Pass/fail is determined by whether the average voltage reported by the NPM is within the range " +
            "set by the operator over the time set by the operator.";


        private const string pulseSimDesc =
            "User sets Gain,. DiscLow, DiscHigh, and NBins. Test sets PulseSim=1 and LedMode=1. " +
            "This test repeatedly queries and plots each NPMs histogram." +
            "\n\n" +
            "Pass/fail is determined by whether the histogram spreads bins over a range greater than " +
            "the user determines necessary.";

        private const string sdevDesc =
            "Enclose NPMs in a shielded environment. To determine noise, voltage is set by the user, and gain is set to 25.5 (Max). " +
            "The histograms from each NPM are queried and plotted.\n\n" +
            "Pass/fail is determined by whether the maximum bin with counts is less than the user set maximum.";

        private const string tempDesc =
            "Repeatedly query temperature for each device and plot it.\n\n" +
            "Pass/Fail is determined by whether the average temperature falls between the set minimum and maximum.";

        List<string> curServerResults = new List<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private async void refreshConnectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (testTabControl.SelectedTab.Name.Equals("serverTab"))
            {
                searchBar.Clear();
                serverDetailsPanel.Visible = false;
                List<string> sns = SQLManager.GetAllSerials();
                curServerResults.Clear();
                curServerResults.AddRange(sns);
                sns.Sort();
                inServer.Items.Clear();

                foreach (string sn in sns)
                {
                    inServer.Items.Add(sn);
                }
            }
            else if (testTabControl.SelectedTab.Name.Equals("dlTab"))
            {
                availDataloggers.Items.Clear();
                foreach (string dl in SerialDataloggerManager.GetComs())
                {
                    availDataloggers.Items.Add(dl, false);
                }
                if (availDataloggers.Items.Count > 0) availDataloggers.SetItemChecked(0, true);

            }
            else
            {
                refreshConnectedToolStripMenuItem.Enabled = false;
                availNpms.Items.Clear();
                outputBox.Clear();

                List<SerialNPMManager> serialMans = new List<SerialNPMManager>();
                foreach (Tuple<string, string> com in SerialNPMManager.GetComs("STMicroelectronics"))
                {
                    // get sn, app to com number
                    serialMans.Add(new SerialNPMManager("unk", com.Item1));
                    //
                }
                List<Thread> threads = new List<Thread>();
                BlockingCollection<string> coms = new BlockingCollection<string>();
                BlockingCollection<string> errs = new BlockingCollection<string>();

                foreach (SerialNPMManager serialMan in serialMans)
                {
                    ThreadStart threadDelegate = new ThreadStart(() =>
                    {
                        // connect
                        int att = 0;

                        serialMan.Connect(serialMan.com);
                        while (!serialMan.IsConnected())
                        {
                            if (att == 10)
                            {
                                break;
                            }
                            att++;
                            Thread.Sleep(500);
                            serialMan.Connect(serialMan.com);
                        }
                        if (!serialMan.IsConnected())
                        {
                            errs.Add(serialMan.com);
                            return;
                        }
                        // get info
                        serialMan.ClearInput();
                        serialMan.SendCommand("info\r");
                        Thread.Sleep(100);
                        att = 0;
                        while (serialMan.listener.GetSerial().Length == 0)
                        {
                            if (att == 10)
                            {
                                Debug.WriteLine("Unable to get " + serialMan.com);
                                break;
                            };
                            att++;
                            serialMan.listener.Clearinfo();
                            Thread.Sleep(30);
                            serialMan.SendCommand("info\r");
                            Thread.Sleep(30);
                            serialMan.SendCommand("info\r");
                            Thread.Sleep(30);
                            serialMan.listener.ParseInfo();
                        }

                        string serial = serialMan.listener.GetSerial();
                        att = 0;
                        while (!coms.TryAdd(serial + " : " + serialMan.com))
                        {
                            if (att++ == 10) break;
                            Thread.Sleep(30);
                        }
                        serialMan.Disconnect();
                    });
                    Thread thread = new Thread(threadDelegate);
                    thread.Start();
                    threads.Add(thread);
                }

                await Task.Run(() =>
                {
                    foreach (var thread in threads)
                    {
                        thread.Join();
                    }
                });

                List<string> sortMe = new List<string>();
                foreach (string npm in coms)
                {
                    sortMe.Add(npm);
                }
                sortMe.Sort();
                foreach (string i in sortMe)
                {
                    availNpms.Items.Add(i);
                }

                foreach (string com in errs.ToArray())
                {
                    AddOutput("Unable to connect: ", Color.FromArgb(251, 55, 40));
                    AddOutput(com + Environment.NewLine, Color.White);
                }

                refreshConnectedToolStripMenuItem.Enabled = true;
                avail.Text = $"Available NPMs ({availNpms.Items.Count} Connected)";


            }
        }

        private void AddOutput(string text, Color color)
        {
            Invoke((MethodInvoker)delegate
            {
                outputBox.SelectionStart = outputBox.TextLength;
                outputBox.SelectionLength = 0;

                outputBox.SelectionColor = color;
                outputBox.AppendText(text);
                outputBox.SelectionColor = outputBox.ForeColor;

                outputBox.SelectionStart = outputBox.TextLength;
                outputBox.ScrollToCaret();
                outputBox.Refresh();

            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            refreshConnectedToolStripMenuItem_Click(null, null);
            TestChange(null, null);
            testTabControl.TabPages.Remove(dlTab);
        }

        private void SelAll(object sender, EventArgs e)
        {
            for (int i = 0; i < availNpms.Items.Count; i++)
            {
                availNpms.SetItemChecked(i, true);
            }
        }

        private void SelNone(object sender, EventArgs e)
        {
            for (int i = 0; i < availNpms.Items.Count; i++)
            {
                availNpms.SetItemChecked(i, false);
            }
        }

        private void clearOut_Click(object sender, EventArgs e)
        {
            outputBox.Clear();
        }

        private async void runBtn_Click(object sender, EventArgs e)
        {

            if (availNpms.CheckedItems.Count == 0)
            {
                MessageBox.Show("No NPMs Selected.", "Error");
                return;
            }

            // Connect

            outputBox.Clear();
            AddOutput("--Connecting--\n", Color.FromArgb(255, 131, 89));
            List<SerialNPMManager> serialMans = new List<SerialNPMManager>();
            foreach (string checkedCom in availNpms.CheckedItems)
            {
                serialMans.Add(new SerialNPMManager(checkedCom.Split(':')[0].Trim(), checkedCom.Split(':')[1].Trim()));
            }
            int finished = 0;
            int numUpdating = serialMans.Count;
            List<string> failedConnect = new List<string>();
            List<Thread> threads = new List<Thread>();
            BlockingCollection<string> coms = new BlockingCollection<string>();
            foreach (SerialNPMManager serialMan in serialMans)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    // connect
                    int att = 0;

                    serialMan.Connect(serialMan.com);
                    while (!serialMan.IsConnected())
                    {
                        if (att == 10)
                        {
                            break;
                        }
                        att++;
                        Thread.Sleep(500);
                        serialMan.Connect(serialMan.com);
                    }
                    if (!serialMan.IsConnected())
                    {
                        MessageBox.Show("Unable to connect to ");
                    }
                });
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);
            }

            await Task.Run(() =>
            {
                foreach (var thread in threads)
                {
                    thread.Join();
                }
            });

            // validate all NPMs connected
            if (failedConnect.Count != 0)
            {
                foreach (string npm in failedConnect)
                {
                    AddOutput("Connection Failed: ", Color.FromArgb(251, 55, 40));
                    AddOutput(npm + Environment.NewLine, Color.White);
                }

                AddOutput("Terminating Test\n", Color.FromArgb(251, 55, 40));
                foreach (SerialNPMManager serialMan in serialMans) serialMan.Disconnect();
                return;
            }
            else
            {
                AddOutput("Connection Success\n", Color.FromArgb(0, 200, 156));
            }


            if (voltageRadio.Checked)
            {
                bool voltErr = await RunVoltageTest(serialMans);
            }
            else if (ledRadio.Checked)
            {
                bool ledErr = await RunLEDTest(serialMans);
            }
            else if (psRadio.Checked)
            {
                bool psErr = await RunPulseSimTest(serialMans);
            }
            else if (sdevRadio.Checked)
            {
                bool sdevErr = await RunSdevTest(serialMans);
            }
            else if (tempRadio.Checked)
            {
                bool tempErr = await RunTempTest(serialMans);
            }


            foreach (SerialNPMManager serialMan in serialMans) serialMan.Disconnect();

        }

        private async Task<bool> RunTempTest(List<SerialNPMManager> serialMans)
        {

            if (!int.TryParse(tempWait.Text, out int waitSec) ||
                !double.TryParse(tempMinBox.Text, out double minT) ||
                !double.TryParse(tempMaxBox.Text, out double maxT))
            {
                AddOutput("Invalid Inputs\n", Color.FromArgb(251, 55, 40));
                return true;
            }


            AddOutput("--Begin Temp Test--\n", Color.FromArgb(255, 131, 89));
            AddOutput("Acceptable Range: ", Color.FromArgb(71, 134, 255));
            AddOutput(tempMinBox.Text + " - " + tempMaxBox.Text + "C" + Environment.NewLine, Color.White);
            AddOutput("Waiting: ", Color.FromArgb(71, 134, 255));
            AddOutput(tempWait.Text + " Seconds" + Environment.NewLine, Color.White);

            List<Thread> threads = new List<Thread>();


            ConcurrentDictionary<string, Tuple<bool, double>> tempDict = new ConcurrentDictionary<string, Tuple<bool, double>>();
            progressBar.Value = 0;
            progressBar.Maximum = 1000;
            LinePlotForm pf = new LinePlotForm(1, 1, "Temperature Reported", "Temp (C)");
            pf.Show();
            Stopwatch sw = Stopwatch.StartNew();
            foreach (SerialNPMManager serialMan in serialMans)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    Tests voltTestClass = new Tests();
                    foreach (DataPoint point in voltTestClass.TemperatureTest(serialMan, minT, maxT, waitSec))
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            pf.AddData(serialMan.GetSerial(), point);
                        });
                    }
                    // NOTE: This will run forever if there are two of the same serial #s in the test,
                    //       though, this should NEVER happen. Catch this early on and use this  
                    //       loop as a watchpoint in the future should it even happen.
                    //
                    //       Would much prefer an infinite loop than a duplicate serial number reaching 
                    //       my server somehow.
                    while (!tempDict.TryAdd(
                        serialMan.GetSerial(),
                        new Tuple<bool, double>(voltTestClass.errOccurred, voltTestClass.averageT)))
                    {
                        Thread.Sleep(100);
                    }

                });
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);
            }

            Task.Run(() =>
            {
                Stopwatch watch = Stopwatch.StartNew();
                int time = waitSec;
                while (watch.ElapsedMilliseconds / 1000 < time)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        int val = (int)(watch.ElapsedMilliseconds / time);
                        if (val > 1000) val = 1000;
                        progressBar.Value = val;
                        progressBar.Refresh();
                    });
                    Thread.Sleep(100);
                }

            });

            await Task.Run(() =>
            {
                foreach (var thread in threads)
                {
                    thread.Join();
                }
            });
            bool errFound = false;
            foreach (string key in tempDict.Keys)
            {
                if (tempDict[key].Item2 <= maxT && tempDict[key].Item2 >= minT)
                {
                    AddOutput(key + ": ", Color.FromArgb(71, 134, 255));
                    AddOutput($"{tempDict[key].Item2:0.00}" + "C (Ave)" + Environment.NewLine, Color.FromArgb(0, 200, 156));
                    SQLManager.UpdateTempTest(key, true);
                }
                else
                {
                    AddOutput(key + ": ", Color.FromArgb(71, 134, 255));
                    AddOutput($"{tempDict[key].Item2:0.00}" + "C (Ave)" + Environment.NewLine, Color.FromArgb(251, 55, 40));

                    SQLManager.UpdateTempTest(key, false);
                    errFound = true;
                }

            }
            pf.canClose = true;
            progressBar.Value = 0;
            return errFound;


        }

        private async Task<bool> RunSdevTest(List<SerialNPMManager> serialMans)
        {

            if (!int.TryParse(sdevWait.Text, out int waitSec) ||
                !int.TryParse(sdevVolt.Text, out int voltage) ||
                !int.TryParse(minBinBox.Text, out int minBin))
            {
                AddOutput("Invalid Inputs\n", Color.FromArgb(251, 55, 40));
                return true;
            }
            if (voltage > 500)
            {
                if (MessageBox.Show($"Is the NPM protected for {voltage} Volts?", "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    AddOutput("ABORT TEST\n", Color.FromArgb(251, 55, 40));
                    return true;
                }
            }
            AddOutput("--Begin SDEV Test--\n", Color.FromArgb(255, 131, 89));
            AddOutput("Wait: ", Color.FromArgb(71, 134, 255));
            AddOutput(sdevWait.Text + " Seconds" + Environment.NewLine, Color.White);
            AddOutput("Valid Below: ", Color.FromArgb(71, 134, 255));
            AddOutput("Bin " + minBinBox.Text + Environment.NewLine, Color.White);
            AddOutput("Voltage: ", Color.FromArgb(71, 134, 255));
            AddOutput(sdevVolt.Text + "V" + Environment.NewLine, Color.White);

            List<Thread> threads = new List<Thread>();

            ConcurrentDictionary<string, Tuple<bool, int>> psDict = new ConcurrentDictionary<string, Tuple<bool, int>>();
            HGMPlotForm pf = new HGMPlotForm();
            pf.Show();
            progressBar.Value = 0;
            progressBar.Maximum = 1000;


            Stopwatch sw = Stopwatch.StartNew();
            foreach (SerialNPMManager serialMan in serialMans)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    Tests psTestClass = new Tests();
                    foreach (int[] series in psTestClass.SdevTest(serialMan, waitSec, voltage, minBin))
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            pf.UpdateSeries(series, serialMan.GetSerial());
                        });
                    }
                    // NOTE: This will run forever if there are two of the same serial #s in the test,
                    //       though, this should NEVER happen. Catch this early on and use this  
                    //       loop as a watchpoint in the future should it even happen.
                    //
                    //       Would much prefer an infinite loop than a duplicate serial number reaching 
                    //       my server somehow.
                    while (!psDict.TryAdd(
                        serialMan.GetSerial(),
                        new Tuple<bool, int>(psTestClass.errOccurred, psTestClass.maxBin)))
                    {
                        Thread.Sleep(100);
                    }

                });
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);
            }

            Task.Run(() =>
            {
                Thread.Sleep(1000);
                Stopwatch watch = Stopwatch.StartNew();
                int time = waitSec;
                while (watch.ElapsedMilliseconds / 1000 < time)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        int val = (int)(watch.ElapsedMilliseconds / time);
                        if (val > 1000) val = 1000;
                        progressBar.Value = val;
                        progressBar.Refresh();
                    });
                    Thread.Sleep(100);
                }

            });

            await Task.Run(() =>
            {
                foreach (var thread in threads)
                {
                    thread.Join();
                }
            });

            bool errFound = false;
            foreach (string key in psDict.Keys)
            {
                if (psDict[key].Item2 <= minBin)
                {
                    AddOutput(key + ": ", Color.FromArgb(71, 134, 255));
                    AddOutput("Max bin " + psDict[key].Item2 + Environment.NewLine, Color.FromArgb(0, 200, 156));
                }
                else
                {
                    AddOutput(key + ": ", Color.FromArgb(71, 134, 255));
                    AddOutput("Max bin " + psDict[key].Item2 + Environment.NewLine, Color.FromArgb(251, 55, 40));
                }
                errFound = errFound || psDict[key].Item1;
                SQLManager.UpdateSdevTest(key, !psDict[key].Item1);

            }
            pf.canClose = true;
            progressBar.Value = 0;


            return errFound;
        }

        private async Task<bool> RunPulseSimTest(List<SerialNPMManager> serialMans)
        {
            if (!int.TryParse(waitPs.Text, out int waitSec) ||
                !int.TryParse(psNbins.Text, out int nbins) ||
                !int.TryParse(psValid.Text, out int range) ||
                !double.TryParse(gainBox.Text, out double gain) ||
                !int.TryParse(psDiscHigh.Text, out int discHigh) ||
                !int.TryParse(psDiscLow.Text, out int discLow))
            {
                AddOutput("Invalid Inputs\n", Color.FromArgb(251, 55, 40));
                return true;
            }


            AddOutput("--Begin PulseSim Test--\n", Color.FromArgb(255, 131, 89));
            AddOutput("NBins: ", Color.FromArgb(71, 134, 255));
            AddOutput(psNbins.Text + Environment.NewLine, Color.White);
            AddOutput("DiscLow: ", Color.FromArgb(71, 134, 255));
            AddOutput(psDiscLow.Text + Environment.NewLine, Color.White);
            AddOutput("DiscHigh: ", Color.FromArgb(71, 134, 255));
            AddOutput(psDiscHigh.Text + Environment.NewLine, Color.White);
            AddOutput("Gain: ", Color.FromArgb(71, 134, 255));
            AddOutput(gainBox.Text + Environment.NewLine, Color.White);
            AddOutput("Wait: ", Color.FromArgb(71, 134, 255));
            AddOutput(waitPs.Text + " Seconds" + Environment.NewLine, Color.White);
            AddOutput("Valid Within: ", Color.FromArgb(71, 134, 255));
            AddOutput(psValid.Text + " Bins" + Environment.NewLine, Color.White);

            List<Thread> threads = new List<Thread>();

            ConcurrentDictionary<string, Tuple<bool, int>> psDict = new ConcurrentDictionary<string, Tuple<bool, int>>();
            HGMPlotForm pf = new HGMPlotForm();
            pf.Show();
            progressBar.Value = 0;
            progressBar.Maximum = 1000;


            Stopwatch sw = Stopwatch.StartNew();
            foreach (SerialNPMManager serialMan in serialMans)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    Tests psTestClass = new Tests();
                    foreach (int[] series in psTestClass.PulseSimTest(serialMan, gain, waitSec, range, discLow, discHigh, nbins))
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            pf.UpdateSeries(series, serialMan.GetSerial());
                        });
                    }
                    // NOTE: This will run forever if there are two of the same serial #s in the test,
                    //       though, this should NEVER happen. Catch this early on and use this  
                    //       loop as a watchpoint in the future should it even happen.
                    //
                    //       Would much prefer an infinite loop than a duplicate serial number reaching 
                    //       my server somehow.
                    while (!psDict.TryAdd(
                        serialMan.GetSerial(),
                        new Tuple<bool, int>(psTestClass.errOccurred, psTestClass.maxSpread)))
                    {
                        Thread.Sleep(100);
                    }

                });
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);
            }

            Task.Run(() =>
            {
                Stopwatch watch = Stopwatch.StartNew();
                int time = waitSec;
                while (watch.ElapsedMilliseconds / 1000 < time)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        int val = (int)(watch.ElapsedMilliseconds / time);
                        if (val > 1000) val = 1000;
                        progressBar.Value = val;
                        progressBar.Refresh();
                    });
                    Thread.Sleep(100);
                }

            });

            await Task.Run(() =>
            {
                foreach (var thread in threads)
                {
                    thread.Join();
                }
            });

            bool errFound = false;
            foreach (string key in psDict.Keys)
            {
                if (psDict[key].Item2 < range)
                {
                    AddOutput(key + ": ", Color.FromArgb(71, 134, 255));
                    AddOutput(psDict[key].Item2 + " Bin Max Spread" + Environment.NewLine, Color.FromArgb(0, 200, 156));
                }
                else
                {
                    AddOutput(key + ": ", Color.FromArgb(71, 134, 255));
                    AddOutput(psDict[key].Item2 + " Bin Max Spread" + Environment.NewLine, Color.FromArgb(251, 55, 40));
                }
                errFound = errFound || psDict[key].Item1;
                SQLManager.UpdatePSTest(key, !psDict[key].Item1);

            }
            pf.canClose = true;
            progressBar.Value = 0;


            return errFound;

        }

        private async Task<bool> RunLEDTest(List<SerialNPMManager> serialMans)
        {
            AddOutput("--Begin LED Test--\n", Color.FromArgb(255, 131, 89));
            List<Thread> threads = new List<Thread>();

            foreach (SerialNPMManager serialMan in serialMans)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    Tests.SetupLEDTest(serialMan);
                });
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);
            }

            await Task.Run(() =>
            {
                foreach (var thread in threads)
                {
                    thread.Join();
                }
            });

            bool errFound = false;
            progressBar.Value = 0;
            progressBar.Maximum = serialMans.Count;
            foreach (SerialNPMManager serialMan in serialMans)
            {
                bool? res = Tests.BlinkTest(serialMan);
                if (!res.HasValue)
                {
                    AddOutput("ABORT TEST" + Environment.NewLine, Color.FromArgb(251, 55, 40));
                    errFound = true;
                    break;
                }

                SQLManager.UpdateLEDTest(serialMan.GetSerial(), res.Value);
                errFound = errFound || res.Value;
                if (res.Value)
                {
                    AddOutput(serialMan.GetSerial() + ": ", Color.FromArgb(71, 134, 255));
                    AddOutput("Pass" + Environment.NewLine, Color.FromArgb(0, 200, 156));
                }
                else
                {
                    AddOutput(serialMan.GetSerial() + ": ", Color.FromArgb(71, 134, 255));
                    AddOutput("Fail" + Environment.NewLine, Color.FromArgb(251, 55, 40));
                }
                progressBar.Value++;
                progressBar.Refresh();
            }
            progressBar.Value = 0;
            return errFound;
        }

        private async Task<bool> RunVoltageTest(List<SerialNPMManager> serialMans)
        {
            if (!int.TryParse(waitVolt.Text, out int waitSec) ||
                !int.TryParse(voltLevel.Text, out int testVoltage) ||
                !int.TryParse(validVoltRange.Text, out int range))
            {
                AddOutput("Invalid Inputs\n", Color.FromArgb(251, 55, 40));
                return true;
            }

            if (testVoltage > 500)
            {
                if (MessageBox.Show($"Is the NPM protected for {testVoltage} Volts?", "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    AddOutput("ABORT TEST\n", Color.FromArgb(251, 55, 40));
                    return true;
                }
            }

            AddOutput("--Begin Voltage Test--\n", Color.FromArgb(255, 131, 89));
            AddOutput("Set Voltage: ", Color.FromArgb(71, 134, 255));
            AddOutput(voltLevel.Text + "V" + Environment.NewLine, Color.White);
            AddOutput("Within: ", Color.FromArgb(71, 134, 255));
            AddOutput(validVoltRange.Text + "V" + Environment.NewLine, Color.White);
            AddOutput("Wait: ", Color.FromArgb(71, 134, 255));
            AddOutput(waitVolt.Text + " Seconds" + Environment.NewLine, Color.White);

            List<Thread> threads = new List<Thread>();

            bool rampDown = rampDownVoltCheck.Checked;
            ConcurrentDictionary<string, Tuple<bool, int>> voltDict = new ConcurrentDictionary<string, Tuple<bool, int>>();
            progressBar.Value = 0;
            progressBar.Maximum = 1000;
            LinePlotForm pf = new LinePlotForm(testVoltage, range, "Voltage Reported", "Voltage");
            pf.Show();
            Stopwatch sw = Stopwatch.StartNew();
            foreach (SerialNPMManager serialMan in serialMans)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    Tests voltTestClass = new Tests();
                    foreach (DataPoint point in voltTestClass.VoltageTest(serialMan, testVoltage, range, waitSec, rampDown))
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            pf.AddData(serialMan.GetSerial(), point);
                        });
                    }
                    // NOTE: This will run forever if there are two of the same serial #s in the test,
                    //       though, this should NEVER happen. Catch this early on and use this  
                    //       loop as a watchpoint in the future should it even happen.
                    //
                    //       Would much prefer an infinite loop than a duplicate serial number reaching 
                    //       my server somehow.
                    while (!voltDict.TryAdd(
                        serialMan.GetSerial(),
                        new Tuple<bool, int>(voltTestClass.errOccurred, (int)voltTestClass.averageVoltage)))
                    {
                        Thread.Sleep(100);
                    }

                });
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);
            }

            Task.Run(() =>
            {
                Stopwatch watch = Stopwatch.StartNew();
                int time = waitSec;
                if (rampDownVoltCheck.Checked)
                    time *= 2;
                while (watch.ElapsedMilliseconds / 1000 < time)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        int val = (int)(watch.ElapsedMilliseconds / time);
                        if (val > 1000) val = 1000;
                        progressBar.Value = val;
                        progressBar.Refresh();
                    });
                    Thread.Sleep(100);
                }

            });

            await Task.Run(() =>
            {
                foreach (var thread in threads)
                {
                    thread.Join();
                }
            });
            bool errFound = false;
            foreach (string key in voltDict.Keys)
            {
                if (voltDict[key].Item2 < testVoltage + range && voltDict[key].Item2 > testVoltage - range)
                {
                    AddOutput(key + ": ", Color.FromArgb(71, 134, 255));
                    AddOutput(voltDict[key].Item2 + "V (Ave)" + Environment.NewLine, Color.FromArgb(0, 200, 156));
                }
                else
                {
                    AddOutput(key + ": ", Color.FromArgb(71, 134, 255));
                    AddOutput(voltDict[key].Item2 + "V (Ave)" + Environment.NewLine, Color.FromArgb(251, 55, 40));
                }
                errFound = errFound || voltDict[key].Item1;
                SQLManager.UpdateVoltTest(key, !voltDict[key].Item1);

            }
            pf.canClose = true;
            progressBar.Value = 0;
            return errFound;

        }



        private void openTerm_Click(object sender, EventArgs e)
        {
            if (availNpms.SelectedIndex == -1) return;

            string com = availNpms.SelectedItem.ToString().Split(':')[1].Trim();
            string serial = availNpms.SelectedItem.ToString().Split(':')[0].Trim();
            SerialNPMManager man = new SerialNPMManager(serial, com);
            man.ShowTerm();

        }

        private void TestChange(object sender, EventArgs e)
        {
            if (voltageRadio.Checked) testDesc.Text = voltDesc;
            else if (sdevRadio.Checked) testDesc.Text = sdevDesc;
            else if (psRadio.Checked) testDesc.Text = pulseSimDesc;
            else if (tempRadio.Checked) testDesc.Text = tempDesc;
            else testDesc.Text = "";
        }

        private void RefreshAvailable(object sender, EventArgs e)
        {
            if (refreshConnectedToolStripMenuItem.Enabled)
            {
                refreshConnectedToolStripMenuItem_Click(null, null);
            }
        }

        private void DisplayInfo(object sender, EventArgs e)
        {
            if (inServer.SelectedIndex == -1)
            {
                serverDetailsPanel.Visible = false;
                return;
            }

            cancelTestBtn.Enabled = false;
            saveTestBtn.Enabled = false;
            saveBtn.Enabled = false;
            resetBtn.Enabled = false;

            string serial = inServer.SelectedItem.ToString();

            NPMData data = SQLManager.GetInfo(serial);
            dateLbl.Text = data.Edited.ToString("yyyy-MM-dd");
            fwLbl.Text = data.Firmware;
            modelLbl.Text = data.Model;
            familyLbl.Text = data.Family;
            noteBox.Text = data.Note;
            tubeLbl.Text = data.Tube;
            snLbl.Text = serial;
            serverDetailsPanel.Visible = true;

            modelLbl.BackColor = SystemColors.ScrollBar;
            familyLbl.BackColor = SystemColors.ScrollBar;
            fwLbl.BackColor = SystemColors.ScrollBar;
            noteBox.BackColor = SystemColors.ScrollBar;
            tubeLbl.BackColor = SystemColors.ScrollBar;
            resetBtn.Enabled = false;
            saveBtn.Enabled = false;
            SQLManager.GetTestInfo(ref data);

            if (data.Volt == null)
            {
                ndV.Checked = true;
            }
            else if (data.Volt == true)
            {
                passV.Checked = true;
            }
            else if (data.Volt == false)
            {
                failV.Checked = true;
            }

            if (data.Led == null)
            {
                ndL.Checked = true;
            }
            else if (data.Led == true)
            {
                passL.Checked = true;
            }
            else if (data.Led == false)
            {
                failL.Checked = true;
            }

            if (data.Sdev == null)
            {
                ndS.Checked = true;
            }
            else if (data.Sdev == true)
            {
                passS.Checked = true;
            }
            else if (data.Sdev == false)
            {
                failS.Checked = true;
            }

            if (data.Pulsesim == null)
            {
                ndP.Checked = true;
            }
            else if (data.Pulsesim == true)
            {
                passP.Checked = true;
            }
            else if (data.Pulsesim == false)
            {
                failP.Checked = true;
            }

            if (data.Temp == null)
            {
                ndT.Checked = true;
            }
            else if (data.Temp == true)
            {
                passT.Checked = true;
            }
            else if (data.Temp == false)
            {
                failT.Checked = true;
            }


            Refresh();
        }

        private void EditText(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = SystemColors.Window;
            saveBtn.Enabled = true;
            resetBtn.Enabled = true;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (modelLbl.BackColor == SystemColors.Window)
            {
                SQLManager.UpdateModel(modelLbl.Text, snLbl.Text);
            }
            if (familyLbl.BackColor == SystemColors.Window)
            {
                SQLManager.UpdateFamily(familyLbl.Text, snLbl.Text);
            }
            if (fwLbl.BackColor == SystemColors.Window)
            {
                SQLManager.UpdateFirmware(fwLbl.Text, snLbl.Text);
            }
            if (noteBox.BackColor == SystemColors.Window)
            {
                SQLManager.UpdateNote(noteBox.Text, snLbl.Text);
            }
            if (tubeLbl.BackColor == SystemColors.Window)
            {
                SQLManager.UpdateTube(tubeLbl.Text, snLbl.Text);
            }

            DisplayInfo(null, null);

        }

        private async void editSnBtn_ClickAsync(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure? Do not change this serial number unless you know what you're doing.", "Warning", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {

                SerialNPMManager com = await FindComWithSerialNumber(snLbl.Text);

                if (com != null)
                {
                    if (!ChangeSerialForm.ChangeSerialDialog(com, snLbl.Text))
                    {
                        MessageBox.Show("An error occurred while changing the serial number. Try again.");
                    }
                    else
                    {
                        MessageBox.Show("Success.");
                    }
                }
                else
                {
                    MessageBox.Show("Could not find this serial number connected. Please plug it in.");
                }

            }
            refreshConnectedToolStripMenuItem_Click(null, null);
        }

        private async Task<SerialNPMManager> FindComWithSerialNumber(string sn)
        {
            // connect, find info, add to dictionary {serial, serialman}
            ConcurrentDictionary<string, SerialNPMManager> comDict = new System.Collections.Concurrent.ConcurrentDictionary<string, SerialNPMManager>();
            List<SerialNPMManager> serialMans = new List<SerialNPMManager>();
            foreach (Tuple<string, string> com in SerialNPMManager.GetComs("STMicroelectronics"))
            {
                serialMans.Add(new SerialNPMManager("unk", com.Item1));

            }

            List<Thread> threads = new List<Thread>();

            foreach (SerialNPMManager serialMan in serialMans)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    // connect
                    int att = 0;

                    serialMan.Connect(serialMan.com);
                    while (!serialMan.IsConnected())
                    {
                        if (att == 10)
                        {
                            break;
                        }
                        att++;
                        Thread.Sleep(500);
                        serialMan.Connect(serialMan.com);
                    }
                    if (!serialMan.IsConnected())
                    {
                        return;
                    }
                    // get info
                    serialMan.ClearInput();
                    serialMan.SendCommand("info\r");
                    Thread.Sleep(100);
                    att = 0;
                    while (serialMan.listener.GetInfo().Contains("Unknown") || serialMan.listener.GetInfo().Trim().Length == 0)
                    {
                        if (att == 10) break;
                        att++;
                        serialMan.listener.Clearinfo();
                        Thread.Sleep(30);
                        serialMan.SendCommand("info\r");
                        Thread.Sleep(30);
                    }

                    serialMan.listener.ParseInfo();
                    string serial = serialMan.listener.GetSerial();
                    att = 0;
                    while (!comDict.TryAdd(serial, serialMan))
                    {
                        if (att++ == 10) break;
                        Thread.Sleep(30);
                    }
                    if (!serial.Equals(sn)) serialMan.Disconnect();
                });
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);
            }

            await Task.Run(() =>
            {
                foreach (var thread in threads)
                {
                    thread.Join();
                }
            });
            if (comDict.ContainsKey(sn)) return comDict[sn];
            else return null;
        }

        private void checkVoltRadio(object sender, EventArgs e)
        {
            if (passV.Checked)
            {
                voltLbl.Text = "Passed";
                voltLbl.ForeColor = Color.Green;
            }
            else if (failV.Checked)
            {
                voltLbl.Text = "Failed";
                voltLbl.ForeColor = Color.Red;
            }
            else
            {
                voltLbl.Text = "Not Done";
                voltLbl.ForeColor = Color.Orange;
            }
        }

        private void CheckLEDRadio(object sender, EventArgs e)
        {
            if (passL.Checked)
            {
                ledLbl.Text = "Passed";
                ledLbl.ForeColor = Color.Green;
            }
            else if (failL.Checked)
            {
                ledLbl.Text = "Failed";
                ledLbl.ForeColor = Color.Red;
            }
            else
            {
                ledLbl.Text = "Not Done";
                ledLbl.ForeColor = Color.Orange;
            }
        }

        private void CheckSDEVRadio(object sender, EventArgs e)
        {
            if (passS.Checked)
            {
                sdevLbl.Text = "Passed";
                sdevLbl.ForeColor = Color.Green;
            }
            else if (failS.Checked)
            {
                sdevLbl.Text = "Failed";
                sdevLbl.ForeColor = Color.Red;
            }
            else
            {
                sdevLbl.Text = "Not Done";
                sdevLbl.ForeColor = Color.Orange;
            }
        }

        private void CheckPSRadio(object sender, EventArgs e)
        {
            if (passP.Checked)
            {
                pulseSimLbl.Text = "Passed";
                pulseSimLbl.ForeColor = Color.Green;
            }
            else if (failP.Checked)
            {
                pulseSimLbl.Text = "Failed";
                pulseSimLbl.ForeColor = Color.Red;
            }
            else
            {
                pulseSimLbl.Text = "Not Done";
                pulseSimLbl.ForeColor = Color.Orange;
            }
        }

        private void CheckTempRadio(object sender, EventArgs e)
        {
            if (passT.Checked)
            {
                tempLbl.Text = "Passed";
                tempLbl.ForeColor = Color.Green;
            }
            else if (failT.Checked)
            {
                tempLbl.Text = "Failed";
                tempLbl.ForeColor = Color.Red;
            }
            else
            {
                tempLbl.Text = "Not Done";
                tempLbl.ForeColor = Color.Orange;
            }
        }

        private void blinkBtn_Click(object sender, EventArgs e)
        {
            if (availNpms.SelectedIndex == -1)
            {
                MessageBox.Show("No NPM Selected.", "ERROR");
                return;
            }
            string selCom = availNpms.SelectedItem.ToString().Split(':')[1].Trim();
            string serial = availNpms.SelectedItem.ToString().Split(':')[0].Trim();
            List<SerialNPMManager> serialManagers = new List<SerialNPMManager>();
            foreach (string com in availNpms.Items)
            {
                string comport = com.Split(':')[1].Trim();
                SerialNPMManager serialMan = new SerialNPMManager(serial, comport);
                serialManagers.Add(serialMan);
            }
            List<Thread> threads = new List<Thread>();

            // start threads
            foreach (SerialNPMManager serialMan in serialManagers)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    // connect
                    int att = 0;
                    serialMan.Connect(serialMan.com);
                    while (!serialMan.IsConnected())
                    {
                        if (att == 10)
                        {
                            break;
                        }
                        att++;
                        Thread.Sleep(100);
                        serialMan.Connect(serialMan.com);
                    }
                    if (!serialMan.IsConnected())
                    {
                        Debug.WriteLine("Could not connect: " + serialMan.com);
                        return;
                    }

                    serialMan.ClearInput();
                    Thread.Sleep(30);
                    if (!serialMan.com.Equals(selCom))
                    {
                        serialMan.SendCommand("pulsesim=0\r\n");
                        Thread.Sleep(30);
                        serialMan.SendCommand("ledmode=0\r\n");
                        serialMan.Disconnect();
                    }
                    else
                    {
                        serialMan.SendCommand("pulsesim=100\r\n");
                        Thread.Sleep(30);
                        serialMan.SendCommand("ledmode=1\r\n");
                        Debug.WriteLine("Turned on: " + serialMan.com + " : " + serialMan.IsConnected());
                    }

                });
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);

            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            if (MessageBox.Show("LED Blinking?", serial, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SQLManager.UpdateLEDTest(serial, true);
                AddOutput(serial + ": ", Color.FromArgb(71, 134, 255));
                AddOutput("Blinks" + Environment.NewLine, Color.FromArgb(0, 200, 156));
            }
            else
            {
                SQLManager.UpdateLEDTest(serial, false);
                AddOutput(serial + ": ", Color.FromArgb(71, 134, 255));
                AddOutput("No Blink" + Environment.NewLine, Color.FromArgb(251, 55, 40));
            }

            // turn off led now
            // 
            foreach (SerialNPMManager serialMan in serialManagers)
            {
                if (serialMan.com.Equals(selCom))
                {
                    serialMan.SendCommand("pulsesim=0\r\n");
                    Thread.Sleep(30);
                    serialMan.SendCommand("ledmode=0\r\n");
                    serialMan.Disconnect();
                }
            }


        }

        private void remBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure? Do not delete this serial number unless you know what you're doing.", "Warning", MessageBoxButtons.YesNo)
                == DialogResult.Yes &&
                MessageBox.Show("Are you ABSOLUTELY sure? This will rmeove all data stored with this serial number.", "Warning", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {

                SQLManager.RemoveNPM(snLbl.Text);
            }



            refreshConnectedToolStripMenuItem_Click(null, null);



        }
        private void ValidateMaxVoltage(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            string txt = box.Text;
            if (int.TryParse(txt, out int i))
            {
                if (i > 500)
                {
                    box.ForeColor = Color.Red;
                }
                else
                {
                    box.ForeColor = Color.Black;
                }
            }
            else
            {
                box.ForeColor = Color.Red;
            }
        }
        private void ValidateIntegerInput(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            string txt = box.Text;
            if (int.TryParse(txt, out int i))
            {
                box.ForeColor = Color.Black;
            }
            else
            {
                box.ForeColor = Color.Red;
            }

        }

        private void ValidateDecimalInput(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            string txt = box.Text;
            if (double.TryParse(txt, out double i))
            {
                box.ForeColor = Color.Black;
            }
            else
            {
                box.ForeColor = Color.Red;
            }
        }

        private void EnableTestSaveCancelBtns(object sender, EventArgs e)
        {
            cancelTestBtn.Enabled = true;
            saveTestBtn.Enabled = true;
        }

        private void saveTestBtn_Click(object sender, EventArgs e)
        {
            string sn = snLbl.Text;
            bool? volt;
            bool? led;
            bool? sdev;
            bool? pulse;
            bool? temp;
            // voltage
            if (ndV.Checked)
            {
                volt = null;
            } else
            {
                volt = passV.Checked;
            }
            if (ndL.Checked)
            {
                led = null;
            }
            else
            {
                led = passL.Checked;
            }
            if (ndS.Checked)
            {
                sdev = null;
            }
            else
            {
                sdev = passS.Checked;
            }
            if (ndT.Checked)
            {
                temp = null;
            }
            else
            {
                temp = passT.Checked;
            }
            if (ndP.Checked)
            {
                pulse = null;
            }
            else
            {
                pulse = passP.Checked;
            }

            SQLManager.UpdateAllTests(sn, volt, sdev, temp, led, pulse);

            DisplayInfo(null, null);
        }

        private void searchBar_TextChanged(object sender, EventArgs e)
        {
            inServer.BeginUpdate();
            inServer.Items.Clear(); 
            foreach (string item in curServerResults)
            {
                if (item.ToLower().Contains(searchBar.Text.ToLower()))
                {
                    inServer.Items.Add(item);
                }
            }
            if (inServer.Items.Count == 0) 
            {
                serverDetailsPanel.Visible = false;
                searchBar.ForeColor = Color.Red;
            } 
            else searchBar.ForeColor = Color.Black;
            
            if (inServer.SelectedIndex == -1) 
                serverDetailsPanel.Visible = false;

            inServer.EndUpdate();
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string loc = Path.Combine(Environment.CurrentDirectory, @"Manual\Manual.pdf");
            try
            {
                Process.Start("chrome.exe", string.Format("\"{0}\"", loc));
            }
            catch
            {
                Process.Start(loc);
            }
        }
    }
}

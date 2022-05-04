using GeneralFirstPhase;
using GeneralFirstPhase.Charting;
using GeneralFirstPhase.Data;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using GeneralFirstPhase.SerialTools;
using GeneralFirstPhase.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralFirstPhase
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
        SerialDataloggerManager dlMan = new SerialDataloggerManager();
        
        // Heater stuff
        HeaterOptions heaterOptionsForm = new HeaterOptions();

        bool manCollect = false;
        bool pause = false;
        
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

                refreshConnectedToolStripMenuItem.Enabled = false;
                availNpmsHeater.Items.Clear();

                List<SerialNPMManager> serialMans = new List<SerialNPMManager>();
                foreach (Tuple<string, string> com in SerialNPMManager.GetComs("STMicroelectronics"))
                {
                    // get sn, app to com number
                    serialMans.Add(new SerialNPMManager("unk", com.Item1));
                    //
                }
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
                        while (!coms.TryAdd(serial + " : " + serialMan.com + " : " + serialMan.listener.GetAddress()))
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
                    availNpmsHeater.Items.Add(i);
                }

                refreshConnectedToolStripMenuItem.Enabled = true;
                avail.Text = $"Available NPMs ({availNpms.Items.Count} Connected)";


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
                        while (!coms.TryAdd(serial + " : " + serialMan.com + " : " + serialMan.listener.GetAddress()))
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
            //refreshConnectedToolStripMenuItem_Click(null, null);
            TestChange(null, null);
            // force load heater options without showing it.
            IntPtr p = heaterOptionsForm.Handle;
            
            
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
            runBtn.Enabled = false;
            // Connect

            outputBox.Clear();
            AddOutput("--Connecting--\n", Color.FromArgb(255, 131, 89));
            List<SerialNPMManager> serialMans = new List<SerialNPMManager>();
            foreach (string checkedCom in availNpms.CheckedItems)
            {
                serialMans.Add(new SerialNPMManager(checkedCom.Split(':')[0].Trim(), checkedCom.Split(':')[1].Trim(), checkedCom.Split(':')[2].Trim()[0]));
            }
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
                runBtn.Enabled = true;
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
            else if (sdiRadio.Checked)
            {
                bool sdiErr = await RunSDITest(serialMans);
            }


            foreach (SerialNPMManager serialMan in serialMans) serialMan.Disconnect();
            runBtn.Enabled = true;

        }

        private async Task<bool> RunSDITest(List<SerialNPMManager> serialMans)
        {
            // first, assign ALL addresses connected to the board.
            if (MessageBox.Show("This test requires that all NPMS on the SDI12 path are able to connect. " +
                "Validate all NPMS are reachable by closing teraterm and validating the expected amount vs the " +
                "displayed amount in the app.\n" +
                "Abort this test by clicking the Cancel button, otherwise it will begin immediately.", 
                "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                AddOutput("ABORT TEST\n", Color.FromArgb(251, 55, 40));
                return true;
            }

            // add non-checked npms to list
            List<SerialNPMManager> nonCheckedNPMS = new List<SerialNPMManager>();
            foreach (string com in availNpms.Items)
            {
                if (!availNpms.CheckedItems.Contains(com))
                {
                    SerialNPMManager newMan = new SerialNPMManager(com.Split(':')[0].Trim(), com.Split(':')[1].Trim());
                    serialMans.Add(newMan);
                    nonCheckedNPMS.Add(newMan);
                }
            }
            if (await Tests.SetupSDI(serialMans, false))
            {
                AddOutput("ABORT TEST\n", Color.FromArgb(251, 55, 40));
                return true;
            }

            // remove non checked npms from test
            foreach (SerialNPMManager uncheckedMan in nonCheckedNPMS)
            {
                uncheckedMan.Disconnect();
                serialMans.Remove(uncheckedMan);
            }

            // Now connect to datalogger. Ask user if there are multiple available.
            List<string> dls = SerialDataloggerManager.GetComs();
            string dlCom = "";
            if (dls.Count > 1)
            {
                SelectDL selDl = new SelectDL(dls.ToArray());
                selDl.ShowDialog();

                dlCom = selDl.selectedCom;
                selDl.Dispose();
                if (dlCom.Equals(""))
                {
                    AddOutput("ABORT TEST\n", Color.FromArgb(251, 55, 40));
                    return true;
                }

            } 
            else if (dls.Count == 0)
            {
                AddOutput("No Datalogger detected\n", Color.FromArgb(251, 55, 40));
                AddOutput("ABORT TEST\n", Color.FromArgb(251, 55, 40));
                return true;
            } else
            {
                dlCom = dls[0];
            }
            AddOutput($"Connecting to Datalogger: {dlCom}\n", Color.FromArgb(71, 134, 255));

            await Task.Run(() =>
            {
                int att = 0;
                while (!dlMan.Connect(dlCom))
                {
                    if (att++ == 10) break;
                    Thread.Sleep(30);
                }
            });
            if (!dlMan.IsConnected())
            {
                AddOutput("Unable to connect to Datalogger\n", Color.FromArgb(251, 55, 40));
                AddOutput("ABORT TEST\n", Color.FromArgb(251, 55, 40));
                return true;
            }

            progressBar.Value = 0;
            progressBar.Maximum = serialMans.Count;

            bool err = false;
            await Task.Run(async ()=>
            {
                foreach (SerialNPMManager serialMan in serialMans)
                {
                    char addr = serialMan.GetAddress();
                    string r8 = await dlMan.GetR8(addr);

                    Invoke((MethodInvoker)delegate
                    {
                        progressBar.Value++;
                        if (r8.Length == 0)
                        {
                            AddOutput(serialMan.GetSerial() + ": ", Color.FromArgb(71, 134, 255));
                            AddOutput($"NO RESPONSE" + Environment.NewLine, Color.FromArgb(251, 55, 40));
                            err = true;
                            SQLManager.UpdateSDITest(serialMan.GetSerial(), false);
                        } else
                        {
                            AddOutput(serialMan.GetSerial() + ": ", Color.FromArgb(71, 134, 255));
                            AddOutput(r8 + Environment.NewLine, Color.FromArgb(0, 200, 156));
                            SQLManager.UpdateSDITest(serialMan.GetSerial(), true);
                        }

                        Refresh();
                    });


                }
            });
            progressBar.Value = 0;
            return err;




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
                !int.TryParse(noiseFloorBox.Text, out int floor) ||
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
            AddOutput("--Begin Null Hist Test--\n", Color.FromArgb(255, 131, 89));
            AddOutput("Wait: ", Color.FromArgb(71, 134, 255));
            AddOutput(sdevWait.Text + " Seconds" + Environment.NewLine, Color.White);
            AddOutput("Valid Below: ", Color.FromArgb(71, 134, 255));
            AddOutput("Bin " + minBinBox.Text + Environment.NewLine, Color.White);
            AddOutput("Voltage: ", Color.FromArgb(71, 134, 255));
            AddOutput(sdevVolt.Text + "V" + Environment.NewLine, Color.White);
            AddOutput("Floor: ", Color.FromArgb(71, 134, 255));
            AddOutput(floor + " Counts/Second" + Environment.NewLine, Color.White);

            List<Thread> threads = new List<Thread>();

            ConcurrentDictionary<string, Tuple<bool, int>> psDict = new ConcurrentDictionary<string, Tuple<bool, int>>();
            HGMPlotForm pf = new HGMPlotForm("Counts/Second", true);
            pf.Show();
            progressBar.Value = 0;
            progressBar.Maximum = 1000;


            Stopwatch sw = Stopwatch.StartNew();
            foreach (SerialNPMManager serialMan in serialMans)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    Tests psTestClass = new Tests();
                    foreach (int[] series in psTestClass.NullHist(serialMan, waitSec, voltage, minBin, floor))
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
                        new Tuple<bool, int>(psTestClass.errOccurred, psTestClass.maxBin/4)))
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
                    AddOutput(key + " Noise Floor: ", Color.FromArgb(71, 134, 255));
                    if (psDict[key].Item2 < int.Parse(tagNoise.Text))
                        AddOutput(psDict[key].Item2 + "/64 (TAG!)" + Environment.NewLine, Color.FromArgb(0, 200, 156));
                    else
                        AddOutput(psDict[key].Item2 + "/64" + Environment.NewLine, Color.FromArgb(0, 200, 156));
                }
                
                else
                {
                    AddOutput(key + " Noise Floor: ", Color.FromArgb(71, 134, 255));
                    AddOutput(psDict[key].Item2 + "/64" + Environment.NewLine, Color.FromArgb(251, 55, 40));
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
                !int.TryParse(psCenterSpread.Text, out int range) ||
                !int.TryParse(psCenter.Text, out int center) ||
                !int.TryParse(psCenterSpread.Text, out int centerSpread) ||
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
            AddOutput("Maximum Spread: ", Color.FromArgb(71, 134, 255));
            AddOutput(psCenterSpread.Text + " Bins" + Environment.NewLine, Color.White);
            AddOutput("Valid Within: ", Color.FromArgb(71, 134, 255));
            AddOutput(psCenterSpread.Text + " Bins of Center " + psCenter.Text + Environment.NewLine, Color.White);

            List<Thread> threads = new List<Thread>();

            ConcurrentDictionary<string, Tuple<bool, int, double>> psDict = new ConcurrentDictionary<string, Tuple<bool, int, double>>();
            HGMPlotForm pf = new HGMPlotForm("Counts");
            pf.Show();
            progressBar.Value = 0;
            progressBar.Maximum = 1000;


            Stopwatch sw = Stopwatch.StartNew();
            foreach (SerialNPMManager serialMan in serialMans)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    Tests psTestClass = new Tests();
                    foreach (int[] series in psTestClass.PulseSimTest(serialMan, gain, waitSec, range, discLow, discHigh, nbins, center, centerSpread))
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
                        new Tuple<bool, int, double>(psTestClass.errOccurred, psTestClass.maxSpread, psTestClass.center)))
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
                if (!psDict[key].Item1)
                {
                    AddOutput(key + ": ", Color.FromArgb(71, 134, 255));
                    AddOutput(psDict[key].Item2 + " Bin Max Spread, Center: " + $"{psDict[key].Item3:0.00}" + Environment.NewLine, Color.FromArgb(0, 200, 156));
                }
                else
                {
                    AddOutput(key + ": ", Color.FromArgb(71, 134, 255));
                    AddOutput(psDict[key].Item2 + " Bin Max Spread, Center: " + $"{psDict[key].Item3:0.00}" + Environment.NewLine, Color.FromArgb(251, 55, 40));
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

            if (data.Sdi == null)
            {
                ndSd.Checked = true;
            }
            else if (data.Sdi == true)
            {
                passSd.Checked = true;
            }
            else if (data.Sdi == false)
            {
                failSd.Checked = true;
            }

            if (data.HeatTemp == null)
            {
                ndHt.Checked = true;
            }
            else if (data.HeatTemp == true)
            {
                passHt.Checked = true;
            }
            else if (data.HeatTemp == false)
            {
                failHt.Checked = true;
            }
            
            if (data.HeatVolt == null)
            {
                ndHv.Checked = true;
            }
            else if (data.HeatVolt == true)
            {
                passHv.Checked = true;
            }
            else if (data.HeatVolt == false)
            {
                failHv.Checked = true;
            }
            
            if (data.HeatPulsesim == null)
            {
                ndHp.Checked = true;
            }
            else if (data.HeatPulsesim == true)
            {
                passHp.Checked = true;
            }
            else if (data.HeatPulsesim == false)
            {
                failHp.Checked = true;
            }
            
            if (data.HeatNoise == null)
            {
                ndHn.Checked = true;
            }
            else if (data.HeatNoise == true)
            {
                passHn.Checked = true;
            }
            else if (data.HeatNoise == false)
            {
                failHn.Checked = true;
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

        private void CheckHvRadio(object sender, EventArgs e)
        {
            if (passHv.Checked)
            {
                hvLbl.Text = "Passed";
                hvLbl.ForeColor = Color.Green;
            }
            else if (failHv.Checked)
            {
                hvLbl.Text = "Failed";
                hvLbl.ForeColor = Color.Red;
            }
            else
            {
                hvLbl.Text = "Not Done";
                hvLbl.ForeColor = Color.Orange;
            }
        }

        private void CheckHnRadio(object sender, EventArgs e)
        {
            if (passHn.Checked)
            {
                hnLbl.Text = "Passed";
                hnLbl.ForeColor = Color.Green;
            }
            else if (failHn.Checked)
            {
                hnLbl.Text = "Failed";
                hnLbl.ForeColor = Color.Red;
            }
            else
            {
                hnLbl.Text = "Not Done";
                hnLbl.ForeColor = Color.Orange;
            }
        }

        private void CheckHpRadio(object sender, EventArgs e)
        {
            if (passHp.Checked)
            {
                hpLbl.Text = "Passed";
                hpLbl.ForeColor = Color.Green;
            }
            else if (failHp.Checked)
            {
                hpLbl.Text = "Failed";
                hpLbl.ForeColor = Color.Red;
            }
            else
            {
                hpLbl.Text = "Not Done";
                hpLbl.ForeColor = Color.Orange;
            }
        }

        private void CheckHtRadio(object sender, EventArgs e)
        {
            if (passHt.Checked)
            {
                htLbl.Text = "Passed";
                htLbl.ForeColor = Color.Green;
            }
            else if (failHt.Checked)
            {
                htLbl.Text = "Failed";
                htLbl.ForeColor = Color.Red;
            }
            else
            {
                htLbl.Text = "Not Done";
                htLbl.ForeColor = Color.Orange;
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

        private void CheckSDIRadio(object sender, EventArgs e)
        {
            if (passSd.Checked)
            {
                sdiLbl.Text = "Passed";
                sdiLbl.ForeColor = Color.Green;
            }
            else if (failSd.Checked)
            {
                sdiLbl.Text = "Failed";
                sdiLbl.ForeColor = Color.Red;
            }
            else
            {
                sdiLbl.Text = "Not Done";
                sdiLbl.ForeColor = Color.Orange;
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
            bool? sdi;
            bool? heatVolt;
            bool? heatNoise;
            bool? heatPulsesim;
            bool? heatTemp;
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
            if (ndSd.Checked)
            {
                sdi = null;
            }
            else
            {
                sdi = passSd.Checked;
            }
            if (ndHv.Checked)
            {
                heatVolt = null;
            }
            else
            {
                heatVolt = passHv.Checked;
            }
            if (ndHn.Checked)
            {
                heatNoise = null;
            }
            else
            {
                heatNoise = passHn.Checked;
            }
            if (ndHp.Checked)
            {
                heatPulsesim = null;
            }
            else
            {
                heatPulsesim = passHp.Checked;
            }
            if (ndHt.Checked)
            {
                heatTemp = null;
            }
            else
            {
                heatTemp = passHt.Checked;
            }

            SQLManager.UpdateAllTests(sn, volt, sdev, temp, led, pulse, sdi, heatVolt, heatNoise, heatPulsesim, heatTemp);

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

        private void availDataloggers_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (e.NewValue == CheckState.Unchecked) return;
            dlMan.Disconnect();
            dlConnectedLabel.Text = "Not Connected";
            dlConnectedLabel.ForeColor = Color.Red;
            bool tryConnect = false;
            tryConnect = dlMan.Connect(availDataloggers.Items[e.Index].ToString());

            foreach (int avail in availDataloggers.CheckedIndices)
            {
                availDataloggers.SetItemCheckState(avail, CheckState.Unchecked);
            }

            if (tryConnect)
            {
                dlConnectedLabel.Text = "Connected";
                dlConnectedLabel.ForeColor = Color.Green;
            }

        }


        private void minTrack_Scroll(object sender, EventArgs e)
        {
            minTempBox.Text = minTrack.Value.ToString();
        }

        private void maxTrack_Scroll(object sender, EventArgs e)
        {
            maxTempBox.Text = maxTrack.Value.ToString();
        }



        private void ParseMax(object sender, KeyEventArgs e)
        {
            if (int.TryParse(maxTempBox.Text, out int val))
            {
                if (val >= 0 && val <= maxTrack.Maximum) maxTrack.Value = val;
            }
            Refresh();
        }

        private void ParseMin(object sender, KeyEventArgs e)
        {
            if (int.TryParse(minTempBox.Text, out int val))
            {
                if (val >= 0 && val <= minTrack.Maximum) minTrack.Value = val;
            }
            Refresh();
        }

        private async void queryDlBtn_Click(object sender, EventArgs e)
        {
            if (!dlMan.IsConnected()) return;
            cs215Btn.Enabled = false;
            dlPanel.Enabled = false;
            await dlMan.QueryCycle();
            Tuple<bool, bool, bool, bool, int, int, int> cycle = dlMan.listener.GetCycle();

            bool flgThermalCyclingEnabled = cycle.Item1;
            bool flgHotCycle = cycle.Item2;
            bool flgColdCycle = cycle.Item3;
            bool flgUseCS215 = cycle.Item4;
            int maxCycleTemp = cycle.Item5;
            int minCycleTemp = cycle.Item6;
            int relays = cycle.Item7;

            dlPanel.Enabled = true;
            cs215Btn.Enabled = true;

            if (!flgThermalCyclingEnabled) dlMan.SendCommand("flgThermalCyclingEnabled=1\r\n");
            if (maxCycleTemp >= 0 && maxCycleTemp <= maxTrack.Maximum)
            {
                maxTrack.Value = maxCycleTemp;
                maxTempBox.Text = maxCycleTemp.ToString();
            }
            if (minCycleTemp >= 0 && minCycleTemp <= minTrack.Maximum)
            {
                minTrack.Value = minCycleTemp;
                minTempBox.Text = minCycleTemp.ToString();
            }
            coolCycleCheck.Checked = flgColdCycle;
            heatCycleCheck.Checked = flgHotCycle;
            if (relays == 1) coolerOnRadio.Checked = true;
            else if (relays == 8) heaterOnRadio.Checked = true;
            else if (relays == 0) relayOffRadio.Checked = true;
            Refresh();
        }


        private async void relayChange(object sender, EventArgs e)
        {
            if (!dlMan.IsConnected()) return;
            await dlMan.INICommand($"relays={((RadioButton)sender).Tag}\r\n");
            await dlMan.QueryCycle();
            queryDlBtn_Click(null, null);
        }


        private async void UpdateHotCycle(object sender, EventArgs e)
        {
            int val = 0;
            if (heatCycleCheck.Checked) val = 1;
            await dlMan.INICommand($"flgHotCycle={val}\r\n\r\n");
            await dlMan.QueryCycle();
            queryDlBtn_Click(null, null);
        }

        private async void UpdateCoolCycle(object sender, EventArgs e)
        {
            int val = 0;
            if (coolCycleCheck.Checked) val = 1;
            await dlMan.INICommand($"flgColdCycle={val}\r\n\r\n");
            await dlMan.QueryCycle();
            queryDlBtn_Click(null, null);
        }

        private async void runHeatTest_ClickAsync(object sender, EventArgs e)
        {
            if (heatTestWorker.IsBusy)
            {
                heatTestWorker.CancelAsync();
                await Task.Run(() =>
                {
                    while (heatTestWorker.IsBusy) Thread.Sleep(30);
                });
                runHeatTest.Text = "Run Test";
                runHeatTest.ForeColor = Color.Green;
            }
            else
            {
                //if (MessageBox.Show("Are you sure you want to begin this test? " +
                //    "All previous data for the selected NPMs will be overwritten.",
                //    "Warning",
                //    MessageBoxButtons.YesNo) != DialogResult.Yes) 
                //    return;



                if (availNpmsHeater.CheckedItems.Count == 0)
                {
                    MessageBox.Show("No NPMs Selected.", "Error");
                    return;
                }
                if (!dlMan.IsConnected())
                {
                    MessageBox.Show("Datalogger not connected.", "Error");
                    return;
                }

                List<SerialNPMManager> serialMans = new List<SerialNPMManager>();
                foreach (string checkedCom in availNpmsHeater.CheckedItems)
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
                            return;
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
                    MessageBox.Show("One or more NPMs refused to connect.\nTerminating Test", "Error");
                    foreach (SerialNPMManager serialMan in serialMans) serialMan.Disconnect();
                    return;
                }

                runHeatTest.Text = "Stop Test";
                runHeatTest.ForeColor = Color.Red;

                await Tests.SetupSDI(serialMans, false);
                nextRecLbl.Visible = true;
                pauseBtn.Visible = true;
                manColBtn.Visible = true;
                heatTestWorker.RunWorkerAsync(serialMans);
            }

        }


        private async void cs215Btn_Click(object sender, EventArgs e)
        {
            cs215Lbl.Text = "";
            if (!dlMan.IsConnected())
            {
                MessageBox.Show("Datalogger not connected.", "Error");
                return;
            }
            cs215Btn.Enabled = false;
            dlPanel.Enabled = false;
            string cs215 = await dlMan.GetCS215();
            cs215Lbl.Text = cs215;
            cs215Btn.Enabled = true;
            dlPanel.Enabled = true;
        }

        private void selAllHeat_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < availNpmsHeater.Items.Count; i++)
            {
                availNpmsHeater.SetItemChecked(i, true);
            }
        }

        private void selNoneHeat_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < availNpmsHeater.Items.Count; i++)
            {
                availNpmsHeater.SetItemChecked(i, false);
            }
        }

        private void showTermHeat_Click(object sender, EventArgs e)
        {
            if (availNpmsHeater.SelectedIndex == -1) return;

            string com = availNpmsHeater.SelectedItem.ToString().Split(':')[1].Trim();
            string serial = availNpmsHeater.SelectedItem.ToString().Split(':')[0].Trim();
            SerialNPMManager man = new SerialNPMManager(serial, com);
            man.ShowTerm();
        }

        private void showHeaterOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            heaterOptionsForm.ShowDialog();
        }

        private void HeatTestWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            List<SerialNPMManager> serialMans = (List<SerialNPMManager>)e.Argument;

            // get params from heateroptions
            double psGain = heaterOptionsForm.GetHeatGain();
            int queryTime = heaterOptionsForm.GetQueryTime();
            int voltRange = heaterOptionsForm.GetHeatVoltRange();
            int noiseFloor = heaterOptionsForm.GetNoiseFloor();
            int voltage = heaterOptionsForm.GetHeatVolts();
            int nullMax = heaterOptionsForm.GetMaximumBin();
            int psBinRange = heaterOptionsForm.GetPSBinRange();
            int psCenter = heaterOptionsForm.GetPSCenter();
            int psCenterSpread = heaterOptionsForm.GetPSCenterSpread();
            TimeSpan span = TimeSpan.FromMinutes(queryTime);
            Tests.SetupHeaterTest(serialMans, voltage);
            
            Stopwatch timer = Stopwatch.StartNew();
            while (!heatTestWorker.CancellationPending)
            {
                if (timer.ElapsedMilliseconds > queryTime*60000 || manCollect)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        nextRecLbl.Text = $"Next Record: Now";
                        nextRecLbl.Refresh();
                        manColBtn.Enabled = false;
                        pauseBtn.Enabled = false;

                        heatProgress.Value = 0;
                        heatProgress.Maximum = 25;
                        heatProgress.Visible = true;
                        heatProgress.Refresh();

                        pauseBtn.Visible = true;
                        pauseBtn.Refresh();

                    });
                    manCollect = false;
                    DateTime now = DateTime.Now;
                    timer.Restart();
                    span = TimeSpan.FromMinutes(queryTime);

                    string cs215str = dlMan.GetCS215Sync();
                    string temp = "-1";
                    try
                    {
                        temp = cs215str.Split('=')[1].Split(',')[0].Trim();
                    }
                    catch 
                    {
                        cs215str = dlMan.GetCS215Sync();
                        temp = "-1";
                        try
                        {
                            temp = cs215str.Split('=')[1].Split(',')[0].Trim();
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    List<Thread> threads = new List<Thread>();
                    ConcurrentDictionary<string, HeaterDataResults> data = new ConcurrentDictionary<string, HeaterDataResults>();
                    // progress
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    Task.Run(() =>
                    {

                        Stopwatch w = Stopwatch.StartNew();
                        while (heatProgress.Value < heatProgress.Maximum)
                        {
                            if (w.ElapsedMilliseconds > 1000)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    heatProgress.Value++;
                                    Refresh();
                                });
                                w.Restart();
                            }
                            Thread.Sleep(200);
                        }
                        Invoke((MethodInvoker)delegate
                        {
                            heatProgress.Visible = false;
                        });
                    });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                    foreach (SerialNPMManager serialMan in serialMans)
                    {
                        ThreadStart threadDelegate = new ThreadStart(async () =>
                        {
                            
                            HeaterDataResults res = Tests.GetHeaterData(serialMan, psGain, voltRange, voltage, nullMax, psBinRange, noiseFloor);
                            
                            res.Cs215Temp = (int)double.Parse(temp);
                            res.Time = now;
                            data.TryAdd(serialMan.GetSerial(), res);

                            SQLManager.AddHeaterData(res.Serial, now, res.Voltage, res.NpmTemp, res.Cs215Temp, res.PsHGM, res.SdevHGM);
                            DetermineHeaterResults(res, noiseFloor, nullMax, psGain, voltage, voltRange, psBinRange, psCenter, psCenterSpread);
                            Debug.WriteLine("Added entry");
                        });
                        Thread thread = new Thread(threadDelegate);
                        thread.Start();
                        threads.Add(thread);
                    }

                    // wait for collection to finish
                    foreach (var thread in threads)
                    {
                        thread.Join();
                    }
                    heatTestWorker.ReportProgress(0, data);
                }

                span -= TimeSpan.FromMilliseconds(300);
                Invoke((MethodInvoker)delegate
                {
                    nextRecLbl.Text = $"Next Record: {span.ToString(@"hh\:mm\:ss")}";
                    nextRecLbl.Refresh();
                    manColBtn.Enabled = true;
                    pauseBtn.Enabled = true;
                });

                if (pause) 
                { 
                    Invoke((MethodInvoker)delegate
                    {
                        nextRecLbl.Text = $"Next Record: PAUSED";
                        nextRecLbl.Refresh();

                        dlConnectedLabel.Text = "Paused";
                        dlConnectedLabel.Refresh();
                    });
                    foreach (SerialNPMManager serialMan in serialMans) serialMan.Disconnect();
                    dlMan.Disconnect();
                    while (pause)
                    {
                        Thread.Sleep(100);
                    }

                    // reconnect
                    string selCom = "";
                    Invoke((MethodInvoker)delegate
                    {
                        nextRecLbl.Text = $"Reconnecting...";
                        nextRecLbl.Refresh();
                        selCom = availDataloggers.CheckedItems[0].ToString();
                    });
                    bool npmErr = false;
                    foreach (SerialNPMManager serialMan in serialMans) 
                    {
                        int att = 0;
                        serialMan.Connect(serialMan.com);
                        while (!serialMan.IsConnected())
                        {
                            if (att++ > 10) break;
                            serialMan.Connect(serialMan.com);
                            Thread.Sleep(100);
                        }
                        if (!serialMan.IsConnected())
                        {
                            pause = true;
                            MessageBox.Show($"Unable to connect to {serialMan.com} (NPM)", "Error");
                            npmErr = true;
                            break;
                        }
                    }

                    int attdl = 0;
                    dlMan.Connect(selCom);
                    while (!dlMan.IsConnected())
                    {
                        if (attdl++ == 10) break;
                        Thread.Sleep(100);
                        dlMan.Connect(selCom);
                    }
                    if (!dlMan.IsConnected())
                    {
                        pause = true;
                        MessageBox.Show($"Unable to connect to {selCom} (Datalogger)", "Error");
                    }
                    if (dlMan.IsConnected() && !npmErr)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            nextRecLbl.Text = $"Reconnecting...";
                            nextRecLbl.Refresh();
                            dlConnectedLabel.Text = "Connected";
                        });
                    }
                    else
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            pauseBtn.Text = "Resume";
                        });
                    }
                    

                    timer.Restart();
                }
                Thread.Sleep(300);

            }
            foreach (SerialNPMManager serialMan in serialMans) serialMan.Disconnect();
        }

        private void DetermineHeaterResults
            (HeaterDataResults res, int noiseFloor, int nullMax, double psGain, int voltage, int voltRange, int psRange, int psCenter, int psCenterSpread)
        {
            // voltage
            if (res.Voltage >= voltage + voltRange || res.Voltage <= voltage - voltRange)
            {
                if (res.Voltage != -1)
                    SQLManager.UpdateHeatVoltTest(res.Serial, false);
            }

            // noise
            int[] noiseHGM = Array.ConvertAll(res.SdevHGM.Split(','), int.Parse);
            int maxBin = 0;
            for (int i = 0; i < noiseHGM.Length; i++)
            {
                if (noiseHGM[i] - noiseFloor < 0)
                {
                    maxBin = i;
                    break;
                }
            }
            if (maxBin > nullMax)
            {
                SQLManager.UpdateHeatNoiseTest(res.Serial, false);
            }

            // pulsesim
            int[] psHGM = Array.ConvertAll(res.SdevHGM.Split(','), int.Parse);
            int startBin = 0;
            int endBin = 0;
            for (int i = 0; i < noiseHGM.Length; i++)
            {
                if (noiseHGM[i] > 0)
                {
                    if (startBin != 0) startBin = i;
                    endBin = i;
                }
            }
            if (endBin - startBin > psRange)
            {
                SQLManager.UpdateHeatPulsesimTest(res.Serial, false);
            }

            // determine senter
            int c = 0;
            int sumWeights = 0;
            for (int i = 0; i < psHGM.Length; i++)
            {
                if (psHGM[i] > 0)
                {
                    sumWeights += i * psHGM[i];
                    c += psHGM[i];
                }
            }
            if (c != 0)
            {
                int rCenter = sumWeights / c; 
                if (rCenter > psCenter + psCenterSpread || rCenter < psCenter - psCenterSpread)
                {
                    SQLManager.UpdateHeatPulsesimTest(res.Serial, false);

                }
            }
                

            // temp
            if (res.NpmTemp > res.Cs215Temp + 10 || res.NpmTemp < res.Cs215Temp - 10)
            {
                if (res.NpmTemp != -1)
                    SQLManager.UpdateHeatTempTest(res.Serial, false);
            }


        }

        private void UpdateHeatCharts(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            // unpack and display data
            ConcurrentDictionary<string, HeaterDataResults> data = (ConcurrentDictionary<string, HeaterDataResults>)e.UserState;
            heatPlots1.UpdateCharts(data, heaterOptionsForm.GetHeatVolts());

        }

        private void heatTestWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("End");
            nextRecLbl.Visible = false;
            manColBtn.Visible = false;
            pauseBtn.Visible = false;

        }

        private async void setMinMaxTemp_Click(object sender, EventArgs e)
        {
            if (!dlMan.IsConnected())
            {
                if (!dlMan.IsConnected())
                {
                    MessageBox.Show("Datalogger not connected.", "Error");
                    return;
                }
            }
            cs215Btn.Enabled = false;
            dlPanel.Enabled = false;
            //MaxCycleTemp = 40
            //MinCycleTemp = 20
            await dlMan.INICommand($"MaxCycleTemp = {maxTempBox.Text}\r\n");
            Thread.Sleep(30);
            await dlMan.INICommand($"MinCycleTemp = {minTempBox.Text}\r\n");
            Thread.Sleep(30);
            cs215Btn.Enabled = true;
            dlPanel.Enabled = true;
            await dlMan.QueryCycle();
            queryDlBtn_Click(null, null);
        }

        private void manColBtn_Click(object sender, EventArgs e)
        {
            manCollect = true;
        }

        private void copyOutput_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(outputBox.Text);
            }
            catch { }
        }

        private void ShowHeatVoltagePlots(object sender, EventArgs e)
        {
            
        }

        private void seeChartsBtn_Click(object sender, EventArgs e)
        {
            HeatTestPlotView newPlots = new HeatTestPlotView(snLbl.Text);
            if (newPlots.HasData())
                newPlots.ShowPlots();
            else
                MessageBox.Show("No data for this device.", "Error");
        }

        private void pauseBtn_Click(object sender, EventArgs e)
        {
            pause = !pause;
            if (pause)
            {
                pauseBtn.Text = "Resume";
            } 
            else
            {
                pauseBtn.Text = "Pause";
            }
        }
    }
}

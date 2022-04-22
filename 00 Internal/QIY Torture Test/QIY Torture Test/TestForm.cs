using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIY_Torture_Test
{
    public partial class TestForm : Form
    {
        string path;
        List<TCPNPMManager> inTesting;
        CycleOptionsForm cycleForm;
        DLManager dlMan;
        Stopwatch testTime = Stopwatch.StartNew();
        Stopwatch stageTime = Stopwatch.StartNew();
        int cycles = 0;
        bool testRunning = true;
        List<string> stagesToRun;
        int curStage = 0;
        Dictionary<string, double> stageInterval = new Dictionary<string, double>();

        // {ip: Tuple {Connection Label, Error Label} }
        Dictionary<string, Tuple<Label, Label>> statusDict = new Dictionary<string, Tuple<Label, Label>>();
        private bool signalStageChange = false;
        readonly Point NPMTITLEPOS = new Point(15, 52);
        readonly Size NPMTITLESIZE = new Size(130, 13);

        readonly Point NPMSTATUSPOS = new Point(145, 52);

        readonly Point NPMERRORPOS = new Point(230, 52);
        readonly Size NPMERRORSIZE = new Size(58, 13);

        readonly int NPMYOFFSET = 15;
        Dictionary<string, List<Delegate>> stages;


        private delegate string InfoDel(TCPNPMManager man);
        private delegate string TimeDel(TCPNPMManager man);
        private delegate string UptimeDel(TCPNPMManager man);
        private delegate string VoltageDel(TCPNPMManager man);
        private delegate void PowerOffDel();
        private delegate void EthOffDel();
        private delegate void RemainOnDel();
        private delegate bool ConnectDel(TCPNPMManager man);
        private delegate string PingDel(TCPNPMManager man);
        private delegate double AwaitBoolDel(TCPNPMManager man);

        private Dictionary<string, string[]> initParams = new Dictionary<string, string[]>();

        internal TestForm(string path, List<TCPNPMManager> inTesting, DLManager dlMan, CycleOptionsForm cycleForm)
        {
            this.cycleForm = cycleForm; 
            this.path = path;
            this.inTesting = inTesting;
            this.dlMan = dlMan; 
            InitializeComponent();
        }


        private void StageOneTestForm_Load(object sender, EventArgs e)
        {
            InfoDel infoDel = Info;
            TimeDel timeDel = Time;
            PingDel pingDel = Ping;
            UptimeDel uptimeDel = Uptime;
            VoltageDel voltageDel = Voltage;
            PowerOffDel powerOffDel = RelayOFF;
            EthOffDel ethOffDel = EthOFF;
            RemainOnDel remOnDel = RemainOn;
            ConnectDel connectDel = Connect;
            ConnectDel awaitBootDel = AwaitBoot;

            
            stages = new Dictionary<string, List<Delegate>>()
            {
                //// Presets
                //{"one", new List<Delegate>()
                //    {
                //        powerOffDel,
                //        timeDel,
                //        uptimeDel,
                //        voltageDel,
                //        voltageDel,
                //        infoDel,
                //    }
                //},
                //{"two", new List<Delegate>()
                //    {
                //        ethOffDel,
                //        timeDel,
                //        uptimeDel,
                //        voltageDel,
                //        voltageDel,
                //        infoDel,
                //    }
                //},
                //{"three", new List<Delegate>()
                //   {
                //        powerOffDel,
                //        timeDel,
                //        uptimeDel,
                //        voltageDel,
                //        voltageDel,
                //        infoDel
                //    }
                //},
                //{"four", new List<Delegate>()
                //    {
                //        ethOffDel,
                //        timeDel,
                //        uptimeDel,
                //        voltageDel,
                //        voltageDel,
                //        infoDel,
                //    }
                //},
                //{"five", new List<Delegate>()
                //    {
                //        powerOffDel,
                //        timeDel,
                //        uptimeDel,
                //        voltageDel,
                //        voltageDel,
                //        infoDel,
                //    }
                //},
                //{"six", new List<Delegate>()
                //    {
                //        ethOffDel,
                //        timeDel,
                //        uptimeDel,
                //        voltageDel,
                //        voltageDel,
                //        infoDel,
                //    }
                //},
                //{"ping", new List<Delegate>()
                //    {
                //        ethOffDel,
                //        timeDel,
                //        uptimeDel,
                //        voltageDel,
                //        voltageDel,
                //        infoDel,
                //    }
                //},
            };
            

            stagesToRun = new List<string>();
            string npmParams = "";
            // create stages to run and assign a name
            foreach (string stage in cycleForm.testsCheckListBox.CheckedItems)
            {
                // name
                string name = "";
                List<Delegate> actions = new List<Delegate>();

                double time = 1;
                string[] stageSplit = stage.Split(';');
                foreach (string action in stageSplit)
                {
                    if (action.Contains("powerOff"))
                    {
                        name += "PWR-";
                        actions.Add(powerOffDel);
                        actions.Add(awaitBootDel);
                    }
                    else if (action.Contains("ethOff"))
                    {
                        name += "ETH-";
                        actions.Add(ethOffDel);
                        actions.Add(awaitBootDel);
                    }
                    else if (action.Contains("noCycle"))
                    {
                        name += "NOC-";
                        actions.Add(remOnDel);
                    }
                    else if (action.Contains("Connect"))
                    {
                        name += "NOC-";
                        actions.Add(connectDel);
                    }
                    else if (action.Contains("uptime"))
                    {
                        name += "U-";
                        int dup = Int32.Parse(action.Trim().Split(' ')[1]);
                        for (int n=0; n < dup; n++)
                            actions.Add(uptimeDel);
                    }
                    else if (action.Contains("ping"))
                    {
                        name += "P-";
                        int dup = Int32.Parse(action.Trim().Split(' ')[1]);
                        for (int n=0; n < dup; n++)
                            actions.Add(pingDel);
                    }
                    else if (action.Contains("time"))
                    {
                        name += "T-";
                        int dup = Int32.Parse(action.Trim().Split(' ')[1]);
                        for (int n=0; n < dup; n++)
                            actions.Add(timeDel);
                    }
                    else if (action.Contains("voltage"))
                    {
                        name += "V-";
                        int dup = Int32.Parse(action.Trim().Split(' ')[1]);
                        for (int n=0; n < dup; n++)
                            actions.Add(voltageDel);
                    }
                    else if (action.Contains("info"))
                    {
                        name += "I-";
                        int dup = Int32.Parse(action.Trim().Split(' ')[1]);
                        for (int n=0; n < dup; n++)
                            actions.Add(voltageDel);
                    }
                    else if (action.Contains("="))
                    {
                        npmParams += action.Trim() + ",";
                        if (action.Contains("1") && !name.Contains("SAVE"))
                        {
                            name += "SAVE";
                        }
                    }
                    else if (action.Contains('h'))
                    {
                        time = double.Parse(action.Trim(' ', 'h'));
                    }
                }

                if (stageInterval.ContainsKey(name)) continue;

                stageInterval.Add(name, time);
                initParams.Add(name, npmParams.Trim(',').Split(','));
                stages.Add(name, actions);
                stagesToRun.Add(name);
                
            }

            // make gui
            int i = 0;
            foreach (TCPNPMManager man in inTesting)
            {
                man.Disconnect();

                Label title = new Label();
                title.Text = $"NPM{i:00}: {man}:";
                title.Location = new Point(NPMTITLEPOS.X, NPMTITLEPOS.Y + (i * NPMYOFFSET));
                title.Size = NPMTITLESIZE;
                Controls.Add(title);

                Label connection = new Label();
                connection.Text = $"Not Connected";
                connection.ForeColor = Color.Red;
                connection.Location = new Point(NPMSTATUSPOS.X, NPMSTATUSPOS.Y + (i * NPMYOFFSET));
                connection.AutoSize = true;
                Controls.Add(connection);

                Label errs = new Label();
                errs.Text = $"Errors: 0";
                errs.Location = new Point(NPMERRORPOS.X, NPMERRORPOS.Y + (i * NPMYOFFSET));
                errs.Size = NPMERRORSIZE;
                Controls.Add(errs);

                statusDict.Add(man.ToString(), new Tuple<Label, Label>(connection, errs));

                i++;
            }
            Label pathLbl = new Label();
            pathLbl.Text = path;
            pathLbl.Location = new Point(NPMTITLEPOS.X, NPMTITLEPOS.Y + (i * NPMYOFFSET) + NPMYOFFSET);
            pathLbl.AutoSize = true;
            pathLbl.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Underline, GraphicsUnit.Point, 0);
            pathLbl.MouseClick += (s, ev) =>
            {
                Process.Start("explorer.exe", path);
            };
            Controls.Add(pathLbl);

            testLoopWorker.RunWorkerAsync();
            stageTimerWorker.RunWorkerAsync();
            
        }

        private bool AwaitBoot(TCPNPMManager man)
        {
            for (int i = 0; i < 5; i++)
            {
                if (man.AwaitReboot()) return true;
            }
            return false;
        }

        private void RemainOn()
        {
            return;
        }

        private bool Connect(TCPNPMManager man)
        {
            bool errFound = false;
            int timeout = 0;
            while (!man.TryConnectionSync())
            {
                if (timeout == 4)
                {
                    Debug.WriteLine("Error in CONNECT");
                    errFound = true;
                    break;
                }
                Thread.Sleep(10);
                timeout++;
            }

            return !errFound;

        }
        private string Ping(TCPNPMManager man)
        {
            Stopwatch sw = Stopwatch.StartNew();
            bool res = man.Ping(30);
            return sw.ElapsedMilliseconds + "," + res;        }

        private string Time(TCPNPMManager man)
        {
            Invoke((MethodInvoker)delegate
            {
                statusDict[man.ToString()].Item1.Text = "CMD:TIME";
                statusDict[man.ToString()].Item1.ForeColor = Color.Green;
            });
            int timeout = 0;
            bool errFound = false;
            while (!man.GotTime())
            {
                if (timeout == 3)
                {
                    Debug.WriteLine("Error in TIME");
                    errFound = true;
                    break;
                }
                Thread.Sleep(10);
                timeout++;
            }
            return man.GetTime() + "," + man.GetTimeAttempts();
        }

        private string Uptime(TCPNPMManager man)
        {
            Invoke((MethodInvoker)delegate
            {
                statusDict[man.ToString()].Item1.Text = "CMD:UPTIME";
                statusDict[man.ToString()].Item1.ForeColor = Color.Green;
            });
            int timeout = 0;
            bool errFound = false;
            while (!man.GotUptime())
            {
                if (timeout == 3)
                {
                    Debug.WriteLine("Error in UPTIME");
                    errFound = true;
                    break;
                }
                Thread.Sleep(10);
                timeout++;
            }
            return man.GetUptime() + "," + man.GetUptimeAttempts();
        }

        private string Voltage(TCPNPMManager man)
        {
            Invoke((MethodInvoker)delegate
            {
                statusDict[man.ToString()].Item1.Text = "CMD:VOLTAGE";
                statusDict[man.ToString()].Item1.ForeColor = Color.Green;
            });

            int timeout = 0;
            bool errFound = false;
            while (!man.GotVoltage())
            {
                if (timeout == 3)
                {
                    Debug.WriteLine("Error in VOLT");
                    errFound = true;
                    break;
                }
                Thread.Sleep(10);
                timeout++;
            }
            return man.GetVoltage() + "," + man.GetVoltAttempts();
        }

        private string Info(TCPNPMManager man)
        {
            Invoke((MethodInvoker)delegate
            {
                statusDict[man.ToString()].Item1.Text = "CMD:VOLTAGE";
                statusDict[man.ToString()].Item1.ForeColor = Color.Green;
            });

            bool errFound = false;
            int timeout = 0;
            while (!man.GotVoltage())
            {
                if (timeout == 3)
                {
                    Debug.WriteLine("Error in VOLT 1");
                    errFound = true;
                    break;
                }
                Thread.Sleep(10);
                timeout++;
            }

            return man.GetDiff();
        }

        private void RunTestLoop(object sender, DoWorkEventArgs e)
        {
            while (testRunning)
            {
                string stage = stagesToRun[curStage];

                // run init
                dlMan.SendCommand("relays=9\r\n");
                foreach (TCPNPMManager man in inTesting)
                {
                    man.Ping();
                    man.Initialize(initParams[stage]);
                }

                while (!signalStageChange)
                {
                    TimeSpan ts = testTime.Elapsed;
                    Invoke((MethodInvoker)delegate
                    {
                        runtimeLbl.Text = ts.ToString("dd\\:hh\\:mm\\:ss");
                    });

                    dlMan.SendCommand("relays=9\r\n");

                    Invoke((MethodInvoker)delegate
                    {
                        relayLbl.Text = "ON";
                        relayLbl.ForeColor = Color.Green;
                        foreach (Tuple<Label, Label> lt in statusDict.Values)
                        {
                            lt.Item1.Text = "PING";
                            lt.Item1.ForeColor = Color.Blue;
                        }
                    });


                    foreach (TCPNPMManager man in inTesting)
                    {


                        /////////////// STAGE NAME NEEDS ID
                        man.ClearBuffer();
                        string file = path + $"\\Stage {stage}--{man.ToString().Replace('.', '-')}.csv";
                        if (!File.Exists(file))
                        {
                            using (StreamWriter writer = new StreamWriter(file))
                            {
                                writer.WriteLine("Need to Gen Header");
                            }
                        }



                        string dataToWrite = DateTime.Now.ToString("HH:mm:ss") + ",";


                        //if (!Connect(man))
                        //{
                        //    dataToWrite += man.GetConnectionAttempts() + "," + 0;
                        //    Invoke((MethodInvoker)delegate
                        //    {
                        //        statusDict[man.ToString()].Item1.Text = "FAILED";
                        //        statusDict[man.ToString()].Item1.ForeColor = Color.Red;
                        //    });
                        //    using (StreamWriter writer = File.AppendText(file))
                        //    {
                        //        writer.WriteLine(dataToWrite);
                        //    }
                        //    continue;
                        //}
                        //else
                        //{
                        //    dataToWrite += man.GetConnectionAttempts() + "," + 1 + ",";
                        //    Invoke((MethodInvoker)delegate
                        //    {
                        //        statusDict[man.ToString()].Item1.Text = "CONNECTED";
                        //        statusDict[man.ToString()].Item1.ForeColor = Color.Green;
                        //    });
                        //}


                        //////////////////////CYCLE DEPENDENT///////////////////////////////
                        ////////////////////////////////////////////////////////////////////
                        for (int j = 1; j < stages[stage].Count; j++)
                        {
                            Delegate d = stages[stage][j];
                            var res = d.DynamicInvoke(new object[] { man });
                            if (res.GetType().Equals(typeof(Boolean)))
                            {
                                if (!(bool)res) continue;
                            }
                            dataToWrite += res + ",";
                        }


                        ///////// WRAP UP CYCLE ////////////
                        Invoke((MethodInvoker)delegate
                        {
                            statusDict[man.ToString()].Item1.Text = "DONE CYCLE";
                            statusDict[man.ToString()].Item1.ForeColor = Color.Green;
                        });

                        using (StreamWriter writer = File.AppendText(file))
                        {
                            Debug.WriteLine($"Writing to {man}:\n'{dataToWrite}'");
                            writer.WriteLine(dataToWrite);
                        }

                    }
                    stages[stage][0].DynamicInvoke();
                    
                }
                Debug.WriteLine("Next Stage");
                curStage++;
                if (curStage == stagesToRun.Count) curStage = 0;
                signalStageChange = false;
                stageTime.Restart();
                stageTimerWorker.RunWorkerAsync();
            }
        }

        private void RelayOFF()
        {
            dlMan.SendCommand($"relays=0\r\n");
            cycles++;
            Invoke((MethodInvoker)delegate
            {
                relayLbl.Text = "OFF";
                relayLbl.ForeColor = Color.Red;

                foreach (TCPNPMManager man in inTesting)
                {
                    Tuple<Label, Label> lt = statusDict[man.ToString()];

                    lt.Item1.Text = "Not Connected";
                    lt.Item1.ForeColor = Color.Red;

                    lt.Item2.Text = $"Errors: {man.errCount}";
                }
                cyclesLbl.Text = cycles.ToString();

            });
            Thread.Sleep(2000);
        }
         private void EthOFF()
         {
            dlMan.SendCommand($"relays=1\r\n");
            cycles++;
            Invoke((MethodInvoker)delegate
            {
                relayLbl.Text = "ETH";
                relayLbl.ForeColor = Color.Red;

                foreach (TCPNPMManager man in inTesting)
                {
                    Tuple<Label, Label> lt = statusDict[man.ToString()];

                    lt.Item1.Text = "Not Connected";
                    lt.Item1.ForeColor = Color.Red;

                    lt.Item2.Text = $"Errors: {man.errCount}";
                }
                cyclesLbl.Text = cycles.ToString();

            });
            Thread.Sleep(2000);
        }

        private string MakeString(TCPNPMManager man)
        {
            return "";
        }

        private void stageTimerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            TimeSpan time = TimeSpan.FromHours(stageInterval[stagesToRun[curStage]]);
            string dateString = "hh\\:mm\\:ss";
            TimeSpan diff = time;
            while (diff > TimeSpan.Zero && testRunning)
            {
                TimeSpan ts = stageTime.Elapsed;
                diff = time - ts;
                Invoke((MethodInvoker)delegate
                {
                    Text = $"Stage {stagesToRun[curStage]}: {(time - ts).ToString(dateString)}";
                    Refresh();
                });
                Thread.Sleep(1000);
            }
            signalStageChange = true;
        }

        private void CloseDown(object sender, FormClosingEventArgs e)
        {
            testRunning = false;

            MessageBox.Show("Closing down the loop, this may take a second.");
            while (stageTimerWorker.IsBusy && testLoopWorker.IsBusy)
            {
                Thread.Sleep(10);
            }

        }
    }
}

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPM_General_App
{
    class TCPNPMLink
    {
        // Status
        Button connectionStatus;

        // NPM PARAMS
        TextBox npmModel;
        TextBox npmModelVersion; 
        TextBox npmSerial; 
        TextBox npmName; 
        TextBox npmMaxVoltage; 
        TextBox npmSetVoltage; 
        TextBox npmMeasuredVoltage;
        TextBox npmGain; 
        TextBox npmLowerDisc; 
        TextBox npmUpperDisc; 
        TextBox npmNBins; 
        TextBox npmDeadTime; 
        TextBox npmMaxCountRate;
        TextBox npmLEDMode; 
        TextBox npmPulseLevel; 
        TextBox npmPeakMode; 
        TextBox npmHGMMode; 
        TextBox npmQSim; 
        TextBox npmPulseSim; 
        TextBox npmDTRes; 
        TextBox npmMcsLen; 
        TextBox npmMcsDwell; 
        TextBox npmMcsPasses;
        TextBox npmFwVersion;

        // TODO find location for these
        TextBox npmTTLMode;
        TextBox npmTTLWidth;
        TextBox npmTTLCounter;


        // DataLogging Params
        TextBox dlPrintDAT;
        TextBox dlSaveDat;
        TextBox dlSaveBin;
        TextBox dlPrintHGM;
        TextBox dlSaveHGM;
        TextBox dlSaveTemp;
        TextBox dlSaveHum;
        TextBox dlPulseCounterOn;
        TextBox dlTemperatureOn;
        TextBox dlHumidityOn;


        TextBox dlBatteryOn;
        TextBox dlSignalOn;
        TextBox dlRecordPeriodOn;
        TextBox dlCTSSamp;
        TextBox dlRecordsPerHGM;
        TextBox dlHGMPeriod;
        TextBox dlNewFilePeriod;

        // Terminal
        RichTextBox termOut;
        RichTextBox termIn;
        Button openAdv;

        // Status
        TextBox statusBox;
        Stopwatch curTime = Stopwatch.StartNew();

        // Live Charts
        PlotView THPlot;

        PlotModel thmodel = new PlotModel();

        Axis xAxisTH = new LinearAxis()
        {
            Title = "Runtime (s)",
            Position = AxisPosition.Bottom,
            FontSize = 10

        };

        Axis yAxisT = new LinearAxis()
        {
            Title = "Temp",
            Position = AxisPosition.Left,
            FontSize = 10,
            Minimum = -10,
            Maximum = 100,
            MajorStep = 20,
            Key = "Temp"
        };

        Axis yAxisH = new LinearAxis()
        {
            Title = "Hum",
            Position = AxisPosition.Right,
            FontSize = 10,
            Minimum = 0,
            Maximum = 100,
            Key = "Hum"

        };

        LineSeries hSeries = new LineSeries() { Color = OxyColors.Blue, YAxisKey = "Hum" };
        LineSeries tSeries = new LineSeries() { Color = OxyColors.Red, YAxisKey = "Temp" };


        // LINK OBJECTS
        internal TCPNPMManager tcpMan;
        internal TCPListener listener;
        internal MainForm main;
        internal AdvancedTerminalTCP advTerm;

        // model
        internal Dictionary<string, string> npmModelSet;
        internal Dictionary<string, string> dlModelSet;

        // radiobuttons link
        private Dictionary<string, Dictionary<string, RadioButton>> radioLink = 
            new Dictionary<string, Dictionary<string, RadioButton>>();

        internal string statusString = "";

        public TCPNPMLink(TCPNPMManager tcpMan, MainForm main)
        {
            this.main = main;
            this.tcpMan = tcpMan;
            // create and add listener to tcp manager
            listener = new TCPListener(this);
            tcpMan.AddListener(listener);
        }

        internal void SetupTHPlot(PlotView plot)
        {
            thmodel.Series.Add(tSeries);
            thmodel.Series.Add(hSeries);

            thmodel.Axes.Clear();

            thmodel.Axes.Add(xAxisTH);

            thmodel.Axes.Add(yAxisT);
            thmodel.Axes.Add(yAxisH);

            plot.Model = thmodel;

        }

        internal void UpdateHumPlot(double tryMe)
        {
            int time = (int)(curTime.ElapsedMilliseconds / 1000);
            hSeries.Points.Add(new DataPoint(time, tryMe));
            thmodel.InvalidatePlot(true);
        }

        internal void UpdateTempPlot(double tryMe)
        {
            int time = (int)(curTime.ElapsedMilliseconds / 1000);
            tSeries.Points.Add(new DataPoint(time, tryMe));
            //thmodel.InvalidatePlot(true);

        }

        internal void EditStatus(string line)
        {
            //Debug.WriteLine("Adding Line: " + line);
            main.Invoke((MethodInvoker)delegate
            {
                statusBox.AppendText(line.Trim(' ', '\r', '\n') + Environment.NewLine);
            });

        }

        internal async Task<bool> GotAck()
        {
            while (!listener.GotAck())
            {
                Thread.Sleep(10);
            }
            return true;
        }
        internal void RefreshTerminal(string data)
        {
            main.Invoke((MethodInvoker)delegate
            {
                termOut.AppendText(data);
                advTerm.NewData(data);
            });

        }

        internal void AddRadioLink(string name, string value, RadioButton button)
        {
            if (radioLink.ContainsKey(name))
            {
                radioLink[name].Add(value, button);
            }
            else
            {
                Dictionary<string, RadioButton> dict = new Dictionary<string, RadioButton>();
                dict.Add(value, button);
                radioLink.Add(name, dict);
            }
        }

        internal void UpdateRadio(string name, string value)
        {
            string onOffVal = "-1";
            if (value.ToLower().Trim().Equals("1"))
            {
                onOffVal = "On";
            } else if (value.ToLower().Trim().Equals("0"))
            {
                onOffVal = "Off";
            }

            if (radioLink.ContainsKey(name))
            {
                if (radioLink[name].ContainsKey(value))
                {
                    radioLink[name][value].Checked = true;
                    return;
                }
                if (radioLink[name].ContainsKey(onOffVal))
                {
                    radioLink[name][onOffVal].Checked = true;
                    return;
                }
            }
        }

        internal void UpdateConnectionStatus(bool status)
        {
            main.Invoke((MethodInvoker)delegate
            {
                if (status)
                {
                    // connected
                    this.connectionStatus.ForeColor = Color.Green;
                    this.connectionStatus.Text = "Connected";

                } else
                {
                    // not connected
                    TermOut.Clear();
                    tcpMan.Disconnect();
                    this.connectionStatus.ForeColor = Color.Red;
                    this.connectionStatus.Text = "Connect";
                }
            });
        }

        internal void ShowAdvancedTerminal()
        {
            advTerm.Show();
        }

        internal void SetName(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmName.Text = val;
                UpdateRadio(NpmName.Name, val);
                NpmName.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }
        internal void SetPeakMode(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmPeakMode.Text = val;
                UpdateRadio(NpmPeakMode.Name, val);
                NpmPeakMode.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }
        internal void SetHGMMode(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmHGMMode.Text = val;
                UpdateRadio(NpmHGMMode.Name, val);
                NpmHGMMode.BackColor = SystemColors.ScrollBar; main.toChange.Clear();
            });
        }
        internal void SetFWVersion(string val)
        {
            main.Invoke((MethodInvoker)delegate
            { 
                NpmFwVersion.Text = val;
                main.toChange.Clear();
            });
        }
        internal void SetQsim(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmQSim.Text = val;
                UpdateRadio(NpmQSim.Name, val);
                NpmQSim.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetModelVersion(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                npmModelVersion.Text = val;
                UpdateRadio(npmModelVersion.Name, val);
                npmModelVersion.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetSerial(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                npmSerial.Text = val;
                UpdateRadio(npmSerial.Name, val);
                npmSerial.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetVoltage(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmSetVoltage.Text = val;
                UpdateRadio(NpmSetVoltage.Name, val);
                NpmSetVoltage.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetMaxVoltage(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmMaxVoltage.Text = val;
                UpdateRadio(NpmMaxVoltage.Name, val);
                NpmMaxVoltage.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }
        
        internal void SetPulseSim(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmPulseSim.Text = val;
                UpdateRadio(NpmPulseSim.Name, val);
                NpmPulseSim.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }
        
        internal void SetDTRes(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                npmDTRes.Text = val;
                UpdateRadio(npmDTRes.Name, val);
                npmDTRes.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }
        
        
        internal void SetMcsLen(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmMcsLen.Text = val;
                UpdateRadio(NpmMcsLen.Name, val);
                NpmMcsLen.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }
        
        internal void SetMcsDwell(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmMcsDwell.Text = val;
                UpdateRadio(NpmMcsDwell.Name, val);
                NpmMcsDwell.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }
        
        internal void SetMcsPasses(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmMcsPasses.Text = val;
                UpdateRadio(NpmMcsPasses.Name, val);
                NpmMcsPasses.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }
        
        
        internal void SetGain(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                npmGain.Text = val;
                UpdateRadio(npmGain.Name, val);
                npmGain.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetLowerDisc(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmLowerDisc.Text = val;
                UpdateRadio(NpmLowerDisc.Name, val);
                NpmLowerDisc.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetUpperDisc(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmUpperDisc.Text = val;
                UpdateRadio(NpmUpperDisc.Name, val);
                NpmUpperDisc.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetDeadTime(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmDeadTime.Text = val;
                UpdateRadio(NpmDeadTime.Name, val);
                NpmDeadTime.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetNBins(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmNBins.Text = val;
                UpdateRadio(NpmNBins.Name, val);
                NpmNBins.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetLEDMode(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmLEDMode.Text = val;
                UpdateRadio(NpmLEDMode.Name, val);
                NpmLEDMode.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetPulseLevel(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                NpmPulseLevel.Text = val;
                UpdateRadio(NpmPulseLevel.Name, val);
                NpmPulseLevel.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetPrintDat(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                DlPrintDAT.Text = val;
                UpdateRadio(DlPrintDAT.Name, val);
                DlPrintDAT.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetSaveDat(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                DlSaveDat.Text = val;
                UpdateRadio(DlSaveDat.Name, val);
                DlSaveDat.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }
        
        internal void SetSaveTemp(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                DlSaveTemp.Text = val;
                UpdateRadio(DlSaveTemp.Name, val);
                DlSaveTemp.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }
        
        internal void SetSaveHum(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                DlSaveHum.Text = val;
                UpdateRadio(DlSaveHum.Name, val);
                DlSaveHum.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetSaveBin(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                dlSaveBin.Text = val;
                UpdateRadio(dlSaveBin.Name, val);
                dlSaveBin.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetPrintHGM(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                DlPrintHGM.Text = val;
                UpdateRadio(DlPrintHGM.Name, val);
                DlPrintHGM.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetSaveHGM(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                DlSaveHGM.Text = val;
                UpdateRadio(DlSaveHGM.Name, val);
                DlSaveHGM.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetPulseCounterOn(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                DlPulseOn.Text = val;
                UpdateRadio(DlPulseOn.Name, val);
                DlPulseOn.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetTemperatureOn(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                dlTemperatureOn.Text = val;
                UpdateRadio(dlTemperatureOn.Name, val);
                dlTemperatureOn.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetHumidityOn(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                DlHumidityOn.Text = val;
                UpdateRadio(DlHumidityOn.Name, val);
                DlHumidityOn.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetBatteryOn(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                dlBatteryOn.Text = val;
                UpdateRadio(dlBatteryOn.Name, val);
                dlBatteryOn.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetSignalOn(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                DlSignalOn.Text = val;
                UpdateRadio(DlSignalOn.Name, val);
                DlSignalOn.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetRecordPeriod(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                dlRecordPeriodOn.Text = val;
                UpdateRadio(dlRecordPeriodOn.Name, val);
                dlRecordPeriodOn.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetCTSSamp(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                dlCTSSamp.Text = val;
                UpdateRadio(dlCTSSamp.Name, val);
                dlCTSSamp.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetRecordsPerHGM(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                DlRecordsPerHGM.Text = val;
                UpdateRadio(DlRecordsPerHGM.Name, val);
                DlRecordsPerHGM.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetHGMPeriod(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                dlHGMPeriod.Text = val;
                UpdateRadio(dlHGMPeriod.Name, val);
                dlHGMPeriod.BackColor = SystemColors.ScrollBar;  main.toChange.Clear();
            });
        }

        internal void SetNewFilePeriod(string val)
        {
            main.Invoke((MethodInvoker)delegate
            {
                DlNewFilePeriod.Text = val;
                UpdateRadio(DlNewFilePeriod.Name, val);
                DlNewFilePeriod.BackColor = SystemColors.ScrollBar;
                main.toChange.Clear();
            });
        }

        public TextBox NpmModel { get => npmModel; set => npmModel = value; }
        public TextBox NpmVersion { get => npmModelVersion; set => npmModelVersion = value; }
        public TextBox NpmSerial { get => npmSerial; set => npmSerial = value; }
        public TextBox NpmName { get => npmName; set => npmName = value; }
        public TextBox NpmMaxVoltage { get => npmMaxVoltage; set => npmMaxVoltage = value; }
        public TextBox NpmSetVoltage { get => npmSetVoltage; set => npmSetVoltage = value; }
        public TextBox NpmMeasuredVoltage { get => npmMeasuredVoltage; set => npmMeasuredVoltage = value; }
        public TextBox NpmGain { get => npmGain; set => npmGain = value; }
        public TextBox NpmLowerDisc { get => npmLowerDisc; set => npmLowerDisc = value; }
        public TextBox NpmUpperDisc { get => npmUpperDisc; set => npmUpperDisc = value; }
        public TextBox NpmNBins { get => npmNBins; set => npmNBins = value; }
        public TextBox NpmDeadTime { get => npmDeadTime; set => npmDeadTime = value; }
        public TextBox NpmMaxCountRate { get => npmMaxCountRate; set => npmMaxCountRate = value; }
        public TextBox NpmLEDMode { get => npmLEDMode; set => npmLEDMode = value; }
        public TextBox NpmPulseLevel { get => npmPulseLevel; set => npmPulseLevel = value; }
        public TextBox NpmTTLMode { get => npmTTLMode; set => npmTTLMode = value; }
        public TextBox NpmTTLWidth { get => npmTTLWidth; set => npmTTLWidth = value; }
        public TextBox NpmTTLCounter { get => npmTTLCounter; set => npmTTLCounter = value; }
        public TextBox DlPrintDAT { get => dlPrintDAT; set => dlPrintDAT = value; }
        public TextBox DlSaveDat { get => dlSaveDat; set => dlSaveDat = value; }
        public TextBox DlSaveBin { get => dlSaveBin; set => dlSaveBin = value; }
        public TextBox DlPrintHGM { get => dlPrintHGM; set => dlPrintHGM = value; }
        public TextBox DlSaveHGM { get => dlSaveHGM; set => dlSaveHGM = value; }
        public TextBox DlPulseOn { get => dlPulseCounterOn; set => dlPulseCounterOn = value; }
        public TextBox DlTemperatureOn { get => dlTemperatureOn; set => dlTemperatureOn = value; }
        public TextBox DlHumidityOn { get => dlHumidityOn; set => dlHumidityOn = value; }
        public TextBox DlBatteryOn { get => dlBatteryOn; set => dlBatteryOn = value; }
        public TextBox DlSignalOn { get => dlSignalOn; set => dlSignalOn = value; }
        public TextBox DlRecordPeriodOn { get => dlRecordPeriodOn; set => dlRecordPeriodOn = value; }
        public TextBox DlCTSSamp { get => dlCTSSamp; set => dlCTSSamp = value; }
        public TextBox DlRecordsPerHGM { get => dlRecordsPerHGM; set => dlRecordsPerHGM = value; }
        public TextBox DlHGMPeriod { get => dlHGMPeriod; set => dlHGMPeriod = value; }
        public TextBox DlNewFilePeriod { get => dlNewFilePeriod; set => dlNewFilePeriod = value; }
        public RichTextBox TermOut { get => termOut; set => termOut = value; }
        public RichTextBox TermIn { get => termIn; set => termIn = value; }
        public Button Status { get => connectionStatus; set => connectionStatus = value; }
        public Button OpenAdv { get => openAdv; set => openAdv = value; }
        public TextBox NpmPeakMode { get => npmPeakMode; set => npmPeakMode = value; }
        public TextBox NpmHGMMode { get => npmHGMMode; set => npmHGMMode = value; }
        public TextBox NpmQSim { get => npmQSim; set => npmQSim = value; }
        public TextBox NpmPulseSim { get => npmPulseSim; set => npmPulseSim = value; }
        public TextBox NpmDTRes { get => npmDTRes; set => npmDTRes = value; }
        public TextBox NpmMcsLen { get => npmMcsLen; set => npmMcsLen = value; }
        public TextBox NpmMcsDwell { get => npmMcsDwell; set => npmMcsDwell = value; }
        public TextBox NpmMcsPasses { get => npmMcsPasses; set => npmMcsPasses = value; }
        public TextBox DlSaveTemp { get => dlSaveTemp; set => dlSaveTemp = value; }
        public TextBox DlSaveHum { get => dlSaveHum; set => dlSaveHum = value; }
        public TextBox StatusBox { get => statusBox; set => statusBox = value; }
        public PlotView THPlot1 { get => THPlot; set => THPlot = value; }
        public TextBox NpmFwVersion { get => npmFwVersion; set => npmFwVersion = value; }
    }
}

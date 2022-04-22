using NPM_General_App.SerialNPM;
using NPM_General_App.Utilities;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPM_General_App
{
    public partial class MainForm : Form
    {
        private UDPManager udpMan;
        private readonly int REFRESHRATE = 100;
        internal Dictionary<string, string> toChange = new Dictionary<string, string>();

        ///// GENERAL ///////
        private readonly Point TOPLEFT = new Point(1, 1);

        ////// CONNECTION GB //////////
        private readonly Size TCPCONGBSIZE = new Size(599, 94);
        private readonly Point TCPCONGBLOCATION = new Point(7, 7);

        private readonly Size SERIALCONGBSIZE = new Size(340, 94);
        private readonly Point SERIALCONGBLOCATION = new Point(7, 7);

        ////// CONN CONTROL SIZES ///////
        private readonly Size CONNECTBUTTONSIZE = new Size(95, 75);
        private readonly Point CONNECTBUTTONLOCATION = new Point(498, 13);

        private readonly Size PINGBUTTONSIZE = new Size(51, 75);
        private readonly Point PINGBUTTONLOCATION = new Point(238, 13);

        private readonly Size PINGBOXSIZE = new Size(202, 75);
        private readonly Point PINGBOXLOCATION = new Point(290, 13);

        private readonly Size IPBOXSIZE = new Size(173, 23);
        private readonly Point IPBOXLOCATION = new Point(50, 17);

        private readonly Size MACBOXSIZE = new Size(173, 23);
        private readonly Point MACBOXLOCATION = new Point(50, 39);
        
        private readonly Size FWBOXSIZE = new Size(173, 23);
        private readonly Point FWBOXLOCATION = new Point(50, 61);
        
        private readonly Size COMBOXSIZE = new Size(173, 23);
        private readonly Point COMBOXLOCATION = new Point(58, 29);

        private readonly Size SNBOXSIZE = new Size(173, 23);
        private readonly Point SNBOXLOCATION = new Point(58, 51);

        private readonly Size IPLABELSIZE = new Size(20, 17);
        private readonly Point IPLABELLOCATION = new Point(7, 17);

        private readonly Size MACLABELSIZE = new Size(37, 15);
        private readonly Point MACLABELLOCATION = new Point(7, 41);
        
        private readonly Size FWLABELSIZE = new Size(37, 15);
        private readonly Point FWLABELLOCATION = new Point(7, 63);
        
        private readonly Size COMLABELSIZE = new Size(45, 15);
        private readonly Point COMLABELLOCATION = new Point(7, 31);
        
        private readonly Size SNLABELSIZE = new Size(50, 15);
        private readonly Point SNLABELLOCATION = new Point(7, 53);


        ////// Refresh Apply Cancel Buttons ////////

        private readonly Size REFRESHBUTTONSIZE = new Size(75, 23);
        private readonly Point REFRESHBUTTONLOCATION = new Point(207, 100);

        private readonly Size APPLYBUTTONSIZE = new Size(75, 23);
        private readonly Point APPLYBUTTONLOCATION = new Point(307, 100);

        private readonly Size CANCELBUTTONSIZE = new Size(75, 23);
        private readonly Point CANCELBUTTONLOCATION = new Point(407, 100);


        ////// NPM PARAMS ///////
        private readonly Size NPMPARAMSSIZE = new Size(299, 410);
        private readonly Point NPMPARAMSLOCATION = new Point(7, 123);

        private readonly Size PARAMTITLESIZE = new Size(87, 21);
        private readonly Point PARAMTITLELOCATION = new Point(6, 62 + 45);

        private readonly Size PARAMBOXSIZE = new Size(60, 62);
        private readonly Point PARAMBOXLOCATION = new Point(99, 62 + 45);

        private readonly Size PARAMDESCSIZE = new Size(124, 21);
        private readonly Point PARAMDESCLOCATION = new Point(160, 62 + 45);

        private readonly int NPMYOFFSET = 20;

        // serial

        private readonly Size SERIALTOPPARAMSGBSIZE = new Size(592, 94);
        private readonly Point SERIALTOPPARAMSGBLOCATION = new Point(352, 7);

        private readonly Size SERIALPARAMSGBSIZE = new Size(340, 423);
        private readonly Point SERIALPARAMSGBLOCATION = new Point(7, 106);

        private readonly Size SERIALTABPANELSIZE = new Size(592, 423);
        private readonly Point SERIALTABPANELLOCATION = new Point(352, 106);

        //////// DL PARAMS /////////
        private readonly Size DLPARAMSSIZE = new Size(294, 410);
        private readonly Point DLPARAMSLOCATION = new Point(312, 123);

        private readonly Size DLPARAMTITLESIZE = new Size(94, 21);
        private readonly Point DLPARAMTITLELOCATION = new Point(6, 25);

        private readonly Size DLPARAMBOXSIZE = new Size(60, 62);
        private readonly Point DLPARAMBOXLOCATION = new Point(105, 25);

        private readonly Size DLRADIOPANELSIZE = new Size(100, 21);
        private readonly Point DLRADIOPANELLOCATION = new Point(170, 25);

        private readonly Size DLRADIOBUTTONSIZE = new Size(45, 19);
        private readonly Size DLRADIOBUTTONNUMERICSIZE = new Size(30, 19);
        private readonly Point DLRADIOBUTTONLOCATION = new Point(4, 4);

        private readonly int RADIOXOFFSET = 45;
        private readonly int RADIOXOFFSETNUMERIC = 30;

        //////// SMALL TERM + STATUS /////////
        private readonly Size TERMTCSIZE = new Size(409, 525);
        private readonly Point TERMTCLOCATION = new Point(612, 7);

        private readonly Size TERMOUTSIZE = new Size(330, 415);

        private readonly Size TERMINSIZE = new Size(330, 23);
        private readonly Point TERMINLOCATION = new Point(1, 415);

        private readonly Size STATUSSIZE = new Size(330, 435);

        private readonly Size EXPANDBUTTONSIZE = new Size(330, 60);
        private readonly Point EXPANDBUTTONLOCATION = new Point(1, 438);


        // serial
        private readonly Size SERIALTERMOUTSIZE = new Size(578, 316);

        private readonly Size SERIALTERMINSIZE = new Size(578, 23);
        private readonly Point SERIALTERMINLOCATION = new Point(1, 321);

        private readonly Size SERIALEXPANDBUTTONSIZE = new Size(578, 38);
        private readonly Point SERIALEXPANDBUTTONLOCATION = new Point(1, 351);




        /////// LIVECHARTS ///////
        private readonly Size QUERYFREQLABELSIZE = new Size(100, 23);
        private readonly Point QUERYFREQLABELLOCATION = new Point(7, 9);

        private readonly Size QUERYFREQBOXSIZE = new Size(50, 23);
        private readonly Point QUERYFREQBOXLOCATION = new Point(107, 7);

        private readonly Size HGMPLOTSIZE = new Size(330, 200);
        private readonly Point HGMPLOTLOCATION = new Point(1, 50);
        
        private readonly Size THPLOTSIZE = new Size(330, 200);
        private readonly Point THPLOTLOCATION = new Point(1, 300);



        // {fw: [npmparams,dlparams]}
        private readonly Dictionary<string, Tuple<Dictionary<string, string>, Dictionary<string, string>>> models =
            new Dictionary<string, Tuple<Dictionary<string, string>, Dictionary<string, string>>>() 
            {
                {
                    "TEST_QIY_A_100",
                    new Tuple<Dictionary<string, string>, Dictionary<string, string>>
                    (
                        new Dictionary<string, string>
                        {
                            {"Voltage", "100V - MaxVoltage"},
                            {"MaxVoltage", "500 - 2000V" },
                            {"Gain", "1.0 - 20.0" },
                            {"LowerDisc", "nBins/32 - UpperDisc" },
                            {"UpperDisc", "LowerDisc - nBins-1" },
                            {"nBins", "64, 128, 256, 512, 1024" },
                            {"PeakMode", "" },
                            {"HGMMode", "" },
                            {"DeadTime", "160 - 16000usec" },
                            {"LEDmode", "" },
                            {"PulseLevel", "0.01 - 4.00V" },
                            {"QSim", "" }
                        },

                        new Dictionary<string, string>
                        {
                            {"PrintDAT", "On,Off"},
                            {"PrintHGM", "On,Off" },
                            {"SaveDAT", "On,Off" },
                            {"SaveHGM", "On,Off" },
                            {"SaveBin", "On,Off" },
                            {"PulseOn", "On,Off" },
                            {"TemperatureOn", "On,Off" },
                            {"HumidityOn", "On,Off" },
                            {"BatteryOn", "On,Off" },
                            {"SignalOn", "On,Off" },
                            {"RecordPeriod", "" },
                            {"RecordsPerHGM", "" },
                            {"NewFilePeriod", "" }
                        }
                    )
                },
                {
                    "QIY_A_102",
                    new Tuple<Dictionary<string, string>, Dictionary<string, string>>
                    (
                        new Dictionary<string, string>
                        {
                            {"Voltage", "100V - MaxVoltage"},
                            {"MaxVoltage", "500 - 2000V" },
                            {"Gain", "1.0 - 20.0" },
                            {"LowerDisc", "nBins/32 - UpperDisc" },
                            {"UpperDisc", "LowerDisc - nBins-1" },
                            {"nBins", "64, 128, 256, 512, 1024" },
                            {"PeakMode", "" },
                            {"HGMMode", "" },
                            {"DeadTime", "160 - 16000usec" },
                            {"LEDmode", "" },
                            {"PulseLevel", "0.01 - 4.00V" },
                            {"QSim", "" }
                        },

                        new Dictionary<string, string>
                        {
                            {"PrintDAT", "On,Off"},
                            {"PrintHGM", "On,Off" },
                            {"SaveDAT", "On,Off" },
                            {"SaveHGM", "On,Off" },
                            {"SaveBin", "On,Off" },
                            {"PulseOn", "On,Off" },
                            {"TemperatureOn", "On,Off" },
                            {"HumidityOn", "On,Off" },
                            {"BatteryOn", "On,Off" },
                            {"SignalOn", "On,Off" },
                            {"RecordPeriod", "" },
                            {"RecordsPerHGM", "" },
                            {"NewFilePeriod", "" }
                        }
                    )
                },

                {
                    "QIY_A_101",
                    new Tuple<Dictionary<string, string>, Dictionary<string, string>>
                    (
                        new Dictionary<string, string>
                        {
                            {"Voltage", "100V - MaxVoltage"},
                            {"MaxVoltage", "500 - 2000V" },
                            {"Gain", "1.0 - 20.0" },
                            {"LowerDisc", "nBins/32 - UpperDisc" },
                            {"UpperDisc", "LowerDisc - nBins-1" },
                            {"nBins", "64, 128, 256, 512, 1024" },
                            {"PeakMode", "" },
                            {"HGMMode", "" },
                            {"DeadTime", "160 - 16000usec" },
                            {"LEDmode", "" },
                            {"PulseLevel", "0.01 - 4.00V" },
                            {"QSim", "" }
                        },

                        new Dictionary<string, string>
                        {
                            {"PrintDAT", "On,Off"},
                            {"PrintHGM", "On,Off" },
                            {"SaveDAT", "On,Off" },
                            {"SaveHGM", "On,Off" },
                            {"SaveBin", "On,Off" },
                            {"PulseOn", "On,Off" },
                            {"TemperatureOn", "On,Off" },
                            {"HumidityOn", "On,Off" },
                            {"BatteryOn", "On,Off" },
                            {"SignalOn", "On,Off" },
                            {"RecordPeriod", "" },
                            {"RecordsPerHGM", "" },
                            {"NewFilePeriod", "" }
                        }
                    )
                },

                {
                "QIY_A_100",
                    new Tuple<Dictionary<string, string>, Dictionary<string, string>>
                    (
                        new Dictionary<string, string>
                        {
                            {"Voltage", "100V - MaxVoltage"},
                            {"MaxVoltage", "500 - 2000V" },
                            {"Gain", "1.0 - 20.0" },
                            {"LowerDisc", "nBins/32 - UpperDisc" },
                            {"UpperDisc", "LowerDisc - nBins-1" },
                            {"nBins", "64, 128, 256, 512, 1024" },
                            {"PeakMode", "" },
                            {"HGMMode", "" },
                            {"DeadTime", "160 - 16000usec" },
                            {"LEDmode", "" },
                            {"PulseLevel", "0.01 - 4.00V" },
                            {"QSim", "" }
                        },

                        new Dictionary<string, string>
                        {
                            {"PrintDAT", "On,Off"},
                            {"PrintHGM", "On,Off" },
                            {"SaveDAT", "On,Off" },
                            {"SaveHGM", "On,Off" },
                            {"SaveBin", "On,Off" },
                            {"PulseOn", "On,Off" },
                            {"TemperatureOn", "On,Off" },
                            {"HumidityOn", "On,Off" },
                            {"BatteryOn", "On,Off" },
                            {"SignalOn", "On,Off" },
                            {"RecordPeriod", "" },
                            {"RecordsPerHGM", "" },
                            {"NewFilePeriod", "" }
                        }
                    )
                },

                {
                    "RevD2_2.0.4",
                    new Tuple<Dictionary<string, string>, Dictionary<string, string>>
                    (
                        new Dictionary<string, string>
                        {
                            {"Voltage", "250V - MaxVoltage"},
                            {"MaxVoltage", "500 - 2000V" },
                            {"Gain", "1.0 - 20.0" },
                            {"LowerDisc", "nBins/32 - UpperDisc" },
                            {"UpperDisc", "LowerDisc - nBins-1" },
                            {"DeadTime", "160 - 16000usec" },
                            {"PeakMode", "" },
                            {"LEDmode", "" },
                            {"PulseSim", "" },
                            {"nBins", "64, 128, 256, 512, 1024" },
                            {"HGMMode", "" },
                            {"DTResolution", "0.01 - 4.00V" },
                            {"McsLength", "" },
                            {"McsDwell", "" },
                            {"McsPasses", "" },
                        },

                        new Dictionary<string, string>
                        {
                            {"RecordPeriod", "" },
                            {"NewFilePeriod", "" },
                            {"RecordsPerHGM", "" },
                            {"PrintDAT", "On,Off"},
                            {"PrintHGM", "On,Off" },
                            {"LogDAT", "On,Off" },
                            {"LogHGM", "On,Off" },
                            {"LogTemp", "On,Off" },
                            {"LogHum", "On,Off" },
                        }
                    )

                },

                {
                    "COM",
                    new Tuple<Dictionary<string, string>, Dictionary<string, string>>
                    (
                        // top info
                        new Dictionary<string, string>
                        {
                            {"Firmware Version", ""},
                            {"Model", ""},
                            {"Model Version", ""},
                            {"Time Constant", ""},
                            {"Name", ""},
                            {"ID", ""},
                            {"Local Address", ""},
                            
                        },

                        // info
                        new Dictionary<string, string>
                        {
                            {"VoltageSet", "" },
                            {"VoltageMeasured", "" },
                            {"MaxVoltage", "" },
                            {"Gain", ""},
                            {"DiscLow", "" },
                            {"DiscHigh", "" },
                            {"DeadTime", "" },
                            {"PeakMode", "" },
                            {"LEDMode", "" },
                            {"TTLMode", "" },
                            {"TTLWidth", "" },
                            {"TTLCounter", "" },
                            {"PulseSim", "" },
                            {"nBins", "" },
                            {"HGMMode", "" },
                            {"DtResolution", "" },
                            {"ListStream", "" },
                            {"ListRamMode", "" },
                            {"ListRamMax", "" },
                            {"ListRamStatus", "" },
                        }
                    )

                },

            };

        /// Link Tracking Dicts
        
        // {ip : tcpman}
        private Dictionary<string, TCPNPMLink> TCPLinkTracking = new Dictionary<string, TCPNPMLink>();
        // {com : serialman}
        private Dictionary<string, SerialNPMLink> SerialLinkTracking = new Dictionary<string, SerialNPMLink>();

        

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mainClockWorker.RunWorkerAsync();
            udpMan = new UDPManager(this);
            //udpMan.SearchNeuchQIY();

        }


        private void ClearLinks()
        {
            DisconnectAll();
            TCPLinkTracking.Clear();
            SerialLinkTracking.Clear();
            npmTabControl.Controls.Clear();
        }

        private void refreshSerialConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearLinks();
            foreach (Tuple<string, string> com in SerialNPMManager.GetComs("STMicroelectronics"))
            {
                CreateSerialTab(com.Item1, com.Item2);
            }
        }
        private void refreshDevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisconnectAll();
            ClearLinks();
            udpMan.SearchNeuchQIY();
        }

        private void ClockSleep(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(REFRESHRATE);
        }

        private void UpdateData(object sender, RunWorkerCompletedEventArgs e)
        {
            mainClockWorker.RunWorkerAsync();
        }

        internal async void ParseUDP(string data)
        {
            //string data = udpMan.GetData();
            
            string[] incomingData = data.Split('\n');
            // no new data
            if (data.Length == 0) return;

            // this generally only does anything if the user decides to 
            // refresh the connections, so we should disconnect all and
            // regernerate tracked dictionary
            DisconnectAll();
            

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

                    if (TCPLinkTracking.ContainsKey(ip)) continue;
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
                    mac += macIndex.Substring(i,2) + " : ";
                }
                mac = mac.Trim(':', ' ');


                //////////////// CREATE TAB ///////////////
                CreateTCPTab(ip, mac, fw);
            }
        }

        private void CreateTCPTab(string ip, string mac, string fw)
        {

            if (!models.ContainsKey(fw)) 
            {
                Debug.WriteLine($"could not find FW: {fw}");
                return;
            }
            // Create a TCP Manager and link it to a tracked object
            TCPNPMManager tcpman = new TCPNPMManager(ip);
            TCPNPMLink link = new TCPNPMLink(tcpman, this);
            link.advTerm = new AdvancedTerminalTCP(link);


            link.npmModelSet = models[fw].Item1;
            link.dlModelSet = models[fw].Item2;

            // store the link in a dictionary {ip : link}
            TCPLinkTracking.Add(ip, link);

            TabPage newTab = new TabPage(tcpman.GetIP());
            tcpman = link.tcpMan;
            // Connection Params
            GroupBox conGB = new GroupBox();
            conGB.Text = "TCP/IP Connection Data";
            conGB.Size = TCPCONGBSIZE;
            conGB.Location = TCPCONGBLOCATION;
            newTab.Controls.Add(conGB);

            //MAC label
            Label macLabel = new Label();
            macLabel.Text = "MAC:";
            macLabel.Location = MACLABELLOCATION;
            macLabel.Size = MACLABELSIZE;
            conGB.Controls.Add(macLabel);

            //IP label
            Label ipLabel = new Label();
            ipLabel.Text = "IP:";
            ipLabel.Location = IPLABELLOCATION;
            ipLabel.Size = IPLABELSIZE;
            conGB.Controls.Add(ipLabel);

            //FW label
            Label fwLabel = new Label();
            fwLabel.Text = "FW:";
            fwLabel.Location = FWLABELLOCATION;
            fwLabel.Size = FWLABELSIZE;
            conGB.Controls.Add(fwLabel);

            // MAC text
            TextBox macBox = new TextBox();
            macBox.Text = mac;
            macBox.Location = MACBOXLOCATION;
            macBox.Size = MACBOXSIZE;
            conGB.Controls.Add(macBox);

            // ip text
            TextBox ipBox = new TextBox();
            ipBox.Text = tcpman.GetIP();
            ipBox.Location = IPBOXLOCATION;
            ipBox.Size = IPBOXSIZE;
            conGB.Controls.Add(ipBox);
            
            // FW text
            TextBox fwBox = new TextBox();
            link.NpmFwVersion = fwBox;
            fwBox.Text = fw;
            fwBox.Location = FWBOXLOCATION;
            fwBox.Size = FWBOXSIZE;
            conGB.Controls.Add(fwBox);

            // Ping text
            RichTextBox pingBox = new RichTextBox();
            pingBox.Location = PINGBOXLOCATION;
            pingBox.Size = PINGBOXSIZE;
            pingBox.Multiline = true;
            pingBox.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
            pingBox.ReadOnly = true;
            conGB.Controls.Add(pingBox);

            // Ping button
            Button pingBtn = new Button();
            pingBtn.Text = "Ping";
            pingBtn.Location = PINGBUTTONLOCATION;
            pingBtn.Size = PINGBUTTONSIZE;
            pingBtn.Click += (sender, e) =>
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                PingReply reply = pingSender.Send(tcpman.GetIP(), timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    pingBox.Clear();
                    //Debug.WriteLine("Address: {0}", reply.Address.ToString());
                    pingBox.AppendText($"Pinging {tcpman.GetIP()}...{Environment.NewLine}");
                    pingBox.AppendText($"Roundtrip Time: {reply.RoundtripTime}ms{Environment.NewLine}");
                    pingBox.AppendText($"Sent: { buffer.Length}b{Environment.NewLine}");
                    pingBox.AppendText($"Got: { reply.Buffer.Length}b");
                } else
                {
                    pingBox.Text = "No response";
                }
            };
            conGB.Controls.Add(pingBtn);


            // connect button
            Button connectButton = new Button();
            link.Status = connectButton;
            connectButton.Text = "Connect";
            connectButton.ForeColor = Color.Red;
            connectButton.Location = CONNECTBUTTONLOCATION;
            connectButton.Size = CONNECTBUTTONSIZE;
            connectButton.Name = "connect_" + tcpman.GetIP();

            // create all other info on connection, MAY need to redraw on update as well...
            connectButton.Click += (sender, e) =>
            {
                ConnectTCP(tcpman.GetIP(), (Button)sender);
            };

            conGB.Controls.Add(connectButton);

            // Refresh cancel apply
            Button refreshBtn = new Button();
            refreshBtn.Text = "Refresh";
            refreshBtn.Location = REFRESHBUTTONLOCATION;
            refreshBtn.Size = REFRESHBUTTONSIZE;
            refreshBtn.Click += (sender, e) =>
            {
                toChange.Clear();
                tcpman.NewCmd("info\r\n");
            };
            newTab.Controls.Add(refreshBtn);

            Button cancelBtn = new Button();
            cancelBtn.Text = "Cancel";
            cancelBtn.Location = CANCELBUTTONLOCATION;
            cancelBtn.Size = CANCELBUTTONSIZE;
            cancelBtn.Click += (sender, e) => 
            {
                toChange.Clear();
                tcpman.NewCmd("info\r\n");
            };
            newTab.Controls.Add(cancelBtn);

            Button applyBtn = new Button();
            applyBtn.Text = "Apply";
            applyBtn.Location = APPLYBUTTONLOCATION;
            applyBtn.Size = APPLYBUTTONSIZE;
            applyBtn.Click += (sender, e) =>
            {
                Dictionary<string, string> toChangeCopy = new Dictionary<string, string>(toChange);
                toChange.Clear();
                Task.Run(async () =>
                {
                    foreach (string cmd in toChangeCopy.Keys)
                    {
                        link.tcpMan.NewCmd($"{cmd}={toChangeCopy[cmd]}\r\n");
                        Thread.Sleep(100);
                    }
                    toChangeCopy.Clear();
                    tcpman.NewCmd("info\r\n");

                }); 
            };
            newTab.Controls.Add(applyBtn);


            // create controls but dont fill yet
            newTab.Controls.Add(CreateTCPNPMParamsGB(link));
            newTab.Controls.Add(CreateTCPDLParamsGB(link));
            newTab.Controls.Add(CreateTerminalGB(link));

            npmTabControl.Controls.Add(newTab);
        }

        private Control CreateTerminalGB(TCPNPMLink link)
        {
            TCPNPMManager tcpman = link.tcpMan;


            Panel containerPanel = new Panel();
            containerPanel.Size = TERMTCSIZE;
            containerPanel.Location = TERMTCLOCATION;

            TabControl paramsTC = new TabControl();
            paramsTC.Size = TERMTCSIZE;
            paramsTC.Location = TOPLEFT;
            containerPanel.Controls.Add(paramsTC);
            //paramsGB.Text = "Terminal";



            ///////TERMINAL TAB////////
            TabPage termTab = new TabPage("Terminal");
            paramsTC.Controls.Add(termTab);

            RichTextBox termin = new RichTextBox();
            termin.Size = TERMINSIZE;
            termin.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
            termin.Location = TERMINLOCATION;
            termin.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    link.tcpMan.NewCmd(((RichTextBox)sender).Text + "\r\n");
                    e.SuppressKeyPress = true;
                    ((RichTextBox)sender).Clear();
                }
            };
            termTab.Controls.Add(termin);

            RichTextBox termout = new RichTextBox();
            link.TermOut = termout;
            termout.Size = TERMOUTSIZE;
            termout.Location = TOPLEFT;
            termout.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
            //termout.ReadOnly = true;
            termout.TextChanged += (sender, e) => 
            {
                termout.SelectionStart = termout.Text.Length;
                // scroll it automatically
                termout.ScrollToCaret();
            };
            termout.KeyDown += (sender, e) => { e.SuppressKeyPress = true; termin.Select(); };
            termTab.Controls.Add(termout);

            Button expand = new Button();
            link.OpenAdv = expand;
            expand.Text = "Debug Terminal in Separate Window";
            expand.Size = EXPANDBUTTONSIZE;
            expand.Location = EXPANDBUTTONLOCATION;
            expand.Click += (sender, e) => link.ShowAdvancedTerminal();
            termTab.Controls.Add(expand);

            ///////STATUS TAB////////
            TabPage statusTP = new TabPage("Status");
            paramsTC.Controls.Add(statusTP);

            TextBox status = new TextBox();
            link.StatusBox = status;
            status.Location = TOPLEFT;
            status.Size = STATUSSIZE;
            status.Multiline = true;
            status.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
            statusTP.Controls.Add(status);

            Button getStatus = new Button();
            getStatus.Text = "Query Status";
            getStatus.Size = EXPANDBUTTONSIZE;
            getStatus.Location = EXPANDBUTTONLOCATION;
            getStatus.Click += async (sender, e) => {
                status.Clear();
                link.tcpMan.NewCmd("status\r\n");
                //status.Text = statusString;
            };
            statusTP.Controls.Add(getStatus);

            ///////CHARTS TAB////////
            TabPage liveChartTP = new TabPage("Live Charts");
            paramsTC.Controls.Add(liveChartTP);

            Label queryFreqLabel = new Label();
            queryFreqLabel.Text = "Query Frequency:";
            queryFreqLabel.Size = QUERYFREQLABELSIZE;
            queryFreqLabel.Location = QUERYFREQLABELLOCATION;
            liveChartTP.Controls.Add(queryFreqLabel);
            
            TextBox queryFreqBox = new TextBox();
            queryFreqBox.Size = QUERYFREQBOXSIZE;
            queryFreqBox.Location = QUERYFREQBOXLOCATION;
            liveChartTP.Controls.Add(queryFreqBox);

            PlotView hgmplot = new PlotView();
            hgmplot.Size = HGMPLOTSIZE;
            hgmplot.Location = HGMPLOTLOCATION;
            liveChartTP.Controls.Add(hgmplot);
            
            PlotView thplot = new PlotView();
            link.SetupTHPlot(thplot);
            thplot.Size = THPLOTSIZE;
            thplot.Location = THPLOTLOCATION;
            liveChartTP.Controls.Add(thplot);


            return containerPanel;
        }

        private GroupBox CreateTCPDLParamsGB(TCPNPMLink link)
        {
            TCPNPMManager tcpman = link.tcpMan;
            GroupBox paramsGB = new GroupBox();
            paramsGB.Size = DLPARAMSSIZE;
            paramsGB.Location = DLPARAMSLOCATION;
            paramsGB.Text = "Datalogger Parameters";
            paramsGB.Name = link.tcpMan.GetIP();

            int offset = 0;
            foreach (string key in link.dlModelSet.Keys)
            {
                // title
                Point offsetTitleLoc = new Point(DLPARAMTITLELOCATION.X, DLPARAMTITLELOCATION.Y + offset);
                Label title = new Label();
                title.Text = key;
                title.Location = offsetTitleLoc;
                title.Size = DLPARAMTITLESIZE;
                title.TextAlign = ContentAlignment.TopRight;
                paramsGB.Controls.Add(title);

                // box
                Point offsetBoxLoc = new Point(DLPARAMBOXLOCATION.X, DLPARAMBOXLOCATION.Y + offset);
                TextBox parambox = new TextBox();
                
                parambox.Location = offsetBoxLoc;
                parambox.Size = DLPARAMBOXSIZE;
                parambox.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
                parambox.BackColor = SystemColors.ScrollBar;
                parambox.KeyDown += (sender, e) => EditTCPParamAsync(key, (TextBox)sender);
                parambox.Name = key;
                parambox.KeyDown += async (sender, e) =>
                {
                    if (!TCPLinkTracking[tcpman.GetIP()].tcpMan.IsConnectedShallow())
                    {
                        ((TextBox)sender).Text = "";
                        return;
                    }
                };
                parambox.KeyUp += (sender, e) => 
                {
                    if (toChange.ContainsKey(key))
                    {
                        toChange[key] = ((TextBox)sender).Text;
                    }
                    else
                    {
                        toChange.Add(key, ((TextBox)sender).Text);
                    }
                    foreach (string key in toChange.Keys)
                    {
                        Debug.WriteLine($"{key} : {toChange[key]}");
                    }
                };
                paramsGB.Controls.Add(parambox);


                Point offsetRadioLoc = new Point(DLRADIOPANELLOCATION.X, DLRADIOPANELLOCATION.Y + offset-2);
                Panel radioPanel = new Panel();
                radioPanel.Location = offsetRadioLoc;
                radioPanel.Size = DLRADIOPANELSIZE;

                int radioOffset = 0;
                foreach (string param in link.dlModelSet[key].Split(','))
                {
                    if (param.Length == 0) continue;
                    if (Int32.TryParse(param, out int a))
                    {
                        RadioButton rb = new RadioButton();
                        rb.Size = DLRADIOBUTTONNUMERICSIZE;
                        
                        rb.Location = new Point(DLRADIOBUTTONLOCATION.X + radioOffset, DLRADIOBUTTONLOCATION.Y);
                        rb.Text = param;
                        rb.CheckedChanged += (sender, e) =>
                        {
                            if (!parambox.Text.Equals(param))
                            {
                                parambox.Text = param;
                                parambox.BackColor = Color.White;
                                
                                if (toChange.ContainsKey(key))
                                {
                                    toChange[key] = param;
                                }
                                else
                                {
                                    toChange.Add(key, param);
                                }
                                foreach (string key in toChange.Keys)
                                {
                                    Debug.WriteLine($"{key} : {toChange[key]}");
                                }

                            }
                        };
                        radioPanel.Controls.Add(rb);
                        radioOffset += RADIOXOFFSETNUMERIC;


                        link.AddRadioLink(key, param, rb);

                    }
                    else
                    {
                        RadioButton rb = new RadioButton();
                        rb.Size = DLRADIOBUTTONSIZE;
                        rb.Location = new Point(DLRADIOBUTTONLOCATION.X + radioOffset, DLRADIOBUTTONLOCATION.Y);
                        rb.Text = param;
                        rb.CheckedChanged += (sender, e) =>
                        {
                            if (param.Equals("On"))
                            {
                                if (!parambox.Text.Equals("1"))
                                {
                                    parambox.Text = "1";
                                    parambox.BackColor = Color.White;
                                }
                            } else
                            {
                                if (!parambox.Text.Equals("0"))
                                {
                                    parambox.Text = "0";
                                    parambox.BackColor = Color.White;
                                }
                            }
                            if (toChange.ContainsKey(key))
                            {
                                toChange[key] = parambox.Text;
                            }
                            else
                            {
                                toChange.Add(key, parambox.Text);
                            }
                            foreach (string key in toChange.Keys)
                            {
                                Debug.WriteLine($"{key} : {toChange[key]}");
                            }

                        };
                        radioPanel.Controls.Add(rb);
                        radioOffset += RADIOXOFFSET;


                        link.AddRadioLink(key, param, rb);

                    }
                }
                if (key.Equals("PrintDAT"))
                    link.DlPrintDAT = parambox;
                if (key.Equals("SaveDAT") || key.Equals("LogDAT"))
                    link.DlSaveDat = parambox;
                if (key.Equals("SaveBin"))
                    link.DlSaveBin = parambox;
                if (key.Equals("LogTemp"))
                    link.DlSaveTemp = parambox;
                if (key.Equals("LogHum"))
                    link.DlSaveHum = parambox;
                if (key.Equals("PrintHGM"))
                    link.DlPrintHGM = parambox;
                if (key.Equals("SaveHGM") || key.Equals("LogHGM"))
                    link.DlSaveHGM = parambox;
                if (key.Equals("PulseOn"))
                    link.DlPulseOn = parambox;
                if (key.Equals("TemperatureOn"))
                    link.DlTemperatureOn = parambox;
                if (key.Equals("HumidityOn"))
                    link.DlHumidityOn = parambox;
                if (key.Equals("BatteryOn"))
                    link.DlBatteryOn = parambox;
                if (key.Equals("SignalOn"))
                    link.DlSignalOn = parambox;
                if (key.Equals("RecordPeriod"))
                    link.DlRecordPeriodOn = parambox;
                if (key.Equals("CTS Samp Period"))
                    link.DlCTSSamp = parambox;
                if (key.Equals("RecordsPerHGM"))
                    link.DlRecordsPerHGM = parambox;
                if (key.Equals("HGM Period"))
                    link.DlHGMPeriod = parambox;
                if (key.Equals("NewFilePeriod"))
                    link.DlNewFilePeriod = parambox;

                offset += NPMYOFFSET;
                paramsGB.Controls.Add(radioPanel);
            }
            return paramsGB;
        }

        private GroupBox CreateTCPNPMParamsGB(TCPNPMLink link)
        {
            TCPNPMManager tcpman = link.tcpMan;
            // NPM params

            GroupBox paramsGB = new GroupBox();
            paramsGB.Text = "Nutron Pulse Module Parameters";
            paramsGB.Size = NPMPARAMSSIZE;
            paramsGB.Location = NPMPARAMSLOCATION;
            paramsGB.Name = tcpman.GetIP();
            Debug.WriteLine(tcpman.GetIP());

            // name is longer textbox, add it here
            // TODO make these dynamic

            Label name = new Label();
            name.Text = "Name";
            name.Size = new Size(40, 15);
            name.Location = new Point(7, 29);
            paramsGB.Controls.Add(name);

            TextBox namebox = new TextBox();
            link.NpmName = namebox;
            namebox.Size = new Size(151, 21);
            namebox.Location = new Point(55, 29);
            namebox.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point);
            namebox.BackColor = SystemColors.ScrollBar;
            namebox.KeyDown += (sender, e) => EditTCPParamAsync("name", (TextBox)sender);
            namebox.KeyDown += (sender, e) =>
            {
                if (!TCPLinkTracking[tcpman.GetIP()].tcpMan.IsConnectedShallow())
                    ((TextBox)sender).Text = "";
            };
            paramsGB.Controls.Add(namebox);

            Label namedesc = new Label();
            namedesc.Text = "10 - 15 chars";
            namedesc.Size = new Size(73, 15);
            namedesc.Location = new Point(207, 29);
            paramsGB.Controls.Add(namedesc);


            Label serial = new Label();
            serial.Text = "Serial";
            serial.Size = new Size(40, 15);
            serial.Location = new Point(7, 29 + 25);
            paramsGB.Controls.Add(serial);

            TextBox serialbox = new TextBox();
            link.NpmSerial = serialbox;
            serialbox.Size = new Size(151, 21);
            serialbox.Location = new Point(55, 29 + 25);
            serialbox.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point);
            serialbox.BackColor = SystemColors.ScrollBar;
            serialbox.KeyDown += (sender, e) => EditTCPParamAsync("name", (TextBox)sender);
            serialbox.KeyDown += (sender, e) => { if (!TCPLinkTracking[tcpman.GetIP()].tcpMan.IsConnectedShallow()) ((TextBox)sender).Text = ""; };
            paramsGB.Controls.Add(serialbox);
            

            Label version = new Label();
            version.Text = "Version";
            version.Size = new Size(45, 15);
            version.Location = new Point(7, 29 + 25 + 25);
            paramsGB.Controls.Add(version);

            TextBox versionbox = new TextBox();
            link.NpmVersion = versionbox;
            versionbox.Size = new Size(151, 21);
            versionbox.Location = new Point(55, 29 + 25 + 25);
            versionbox.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point);
            versionbox.BackColor = SystemColors.ScrollBar;
            versionbox.KeyDown += (sender, e) => EditTCPParamAsync("name", (TextBox)sender);
            versionbox.ReadOnly = true;
            paramsGB.Controls.Add(versionbox);



            int offset = 0;
            foreach (string key in link.npmModelSet.Keys)
            {
                // Need a title, textbox, and description for each param

                // title
                Point offsetTitleLoc = new Point(PARAMTITLELOCATION.X, PARAMTITLELOCATION.Y + offset);
                Label title = new Label();
                title.Text = key;
                title.Location = offsetTitleLoc;
                title.Size = PARAMTITLESIZE;
                title.TextAlign = ContentAlignment.TopRight;
                paramsGB.Controls.Add(title);

                // box
                Point offsetBoxLoc = new Point(PARAMBOXLOCATION.X, PARAMBOXLOCATION.Y + offset);
                TextBox parambox = new TextBox();
                parambox.Location = offsetBoxLoc;
                parambox.Size = PARAMBOXSIZE;
                parambox.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
                parambox.BackColor = SystemColors.ScrollBar;
                parambox.KeyDown += (sender, e) => EditTCPParamAsync(key, (TextBox)sender);
                parambox.KeyDown += (sender, e) => { if (!TCPLinkTracking[tcpman.GetIP()].tcpMan.IsConnectedShallow()) ((TextBox)sender).Text = ""; };
                parambox.KeyUp += (sender, e) =>
                {
                    if (toChange.ContainsKey(key))
                    {
                        toChange[key] = ((TextBox)sender).Text;
                    }
                    else
                    {
                        toChange.Add(key, ((TextBox)sender).Text);
                    }
                    foreach (string key in toChange.Keys)
                    {
                        Debug.WriteLine($"{key} : {toChange[key]}");
                    }
                };
                paramsGB.Controls.Add(parambox);


                if (key.Equals("Voltage")) link.NpmSetVoltage = parambox;
                if (key.Equals("MaxVoltage")) link.NpmMaxVoltage = parambox;
                if (key.Equals("Gain")) link.NpmGain = parambox;
                if (key.Equals("LowerDisc")) link.NpmLowerDisc = parambox;
                if (key.Equals("UpperDisc")) link.NpmUpperDisc = parambox;
                if (key.Equals("nBins")) link.NpmNBins = parambox;
                if (key.Equals("DeadTime")) link.NpmDeadTime = parambox;
                if (key.Equals("LEDmode")) link.NpmLEDMode = parambox;
                if (key.Equals("PulseLevel")) link.NpmPulseLevel = parambox;
                if (key.Equals("PeakMode")) link.NpmPeakMode = parambox;
                if (key.Equals("HGMMode")) link.NpmHGMMode = parambox;
                if (key.Equals("QSim")) link.NpmQSim = parambox;
                if (key.Equals("PulseSim")) link.NpmPulseSim = parambox;
                if (key.Equals("DTResolution")) link.NpmDTRes = parambox;
                if (key.Equals("McsLength")) link.NpmMcsLen = parambox;
                if (key.Equals("McsDwell")) link.NpmMcsDwell = parambox;
                if (key.Equals("McsPasses")) link.NpmMcsPasses = parambox;

                // title
                Point offsetDescLoc = new Point(PARAMDESCLOCATION.X, PARAMDESCLOCATION.Y + offset);
                Label desc = new Label();
                desc.Text = link.npmModelSet[key];
                desc.Location = offsetDescLoc;
                desc.Size = PARAMDESCSIZE;
                paramsGB.Controls.Add(desc);

                offset += NPMYOFFSET;
            }

            return paramsGB;
        }

        private async void ConnectTCP(string ip, Button sender)
        {
            TCPNPMManager tcpman = TCPLinkTracking[ip].tcpMan;
            if (tcpman.IsConnectedShallow())
            {
                tcpman.Disconnect();
                toChange.Clear();
                return;
            }

            bool connected = await tcpman.TryConnectionAsync();
            tcpman.NewCmd("info\r\n");
            if (!connected)
            {
                toChange.Clear();
                MessageBox.Show("Could not connect to this device. Is something else connected?");
            }
        }
        private void DisconnectAll()
        {
            foreach (string ip in TCPLinkTracking.Keys)
            {
                TCPLinkTracking[ip].tcpMan.Disconnect();
            }
            foreach (string com in SerialLinkTracking.Keys)
            {
                SerialLinkTracking[com].Disconnect();
            }
        }


        private void EditTCPParamAsync(string param, TextBox box)
        {
            if (!TCPLinkTracking[((GroupBox)box.Parent).Name].tcpMan.IsConnectedShallow())
                return;
            box.BackColor = Color.White;
        }


        private void CreateSerialTab(string com, string sn)
        {
            SerialNPMLink link = new SerialNPMLink(this, com);
            SerialLinkTracking.Add(com, link);

            TabPage newTab = new TabPage(com);
            // Connection Params
            GroupBox conGB = new GroupBox();
            conGB.Text = "USB Connection Data";
            conGB.Size = SERIALCONGBSIZE;
            conGB.Location = SERIALCONGBLOCATION;
            newTab.Controls.Add(conGB);

            //COM label
            Label comLabel = new Label();
            comLabel.Text = "COM:";
            comLabel.Location = COMLABELLOCATION;
            comLabel.Size = COMLABELSIZE;
            conGB.Controls.Add(comLabel);

            //SN label
            Label snLabel = new Label();
            snLabel.Text = "SERIAL:";
            snLabel.Location = SNLABELLOCATION;
            snLabel.Size = SNLABELSIZE;
            conGB.Controls.Add(snLabel);

            //COM text
            TextBox comBox = new TextBox();
            comBox.Text = com;
            comBox.Location = COMBOXLOCATION;
            comBox.Size = COMBOXSIZE;
            conGB.Controls.Add(comBox);

            //SN text
            TextBox snBox = new TextBox();
            link.NpmSerial = snBox;
            snBox.Text = sn;
            snBox.Location = SNBOXLOCATION;
            snBox.Size = SNBOXSIZE;
            conGB.Controls.Add(snBox);

            // Connect button
            Button connectButton = new Button();
            link.ConnectionStatus = connectButton;
            connectButton.Text = "Connect";
            connectButton.Location = PINGBUTTONLOCATION;
            connectButton.Size = CONNECTBUTTONSIZE;
            connectButton.ForeColor = Color.Red;
            connectButton.Click += (sender, e) => 
            {

                if (link.IsConnected())
                {
                    link.Disconnect();
                    connectButton.Text = "Connect";
                    connectButton.ForeColor = Color.Red;
                } 
                else
                {
                    if (!link.TryConnect())
                    {
                        MessageBox.Show("Could not connect. Is something else connected already?");
                        return;
                    }
                    connectButton.Text = "Connected";
                    connectButton.ForeColor = Color.Green;
                    link.NewCmd("\rinfo\r");
                }
                
            
            };
            conGB.Controls.Add(connectButton);

            newTab.Controls.Add(CreateSerialNPMTopParamsGB(link));
            newTab.Controls.Add(CreateSerialNPMParamsGB(link));
            newTab.Controls.Add(CreateSerialNPMTerminalTab(link));

            npmTabControl.Controls.Add(newTab);

            // terminal 

        }

        private Control CreateSerialNPMTerminalTab(SerialNPMLink link)
        {
            Panel ret = new Panel();
            ret.Size = SERIALTABPANELSIZE;
            ret.Location = SERIALTABPANELLOCATION;

            TabControl tc = new TabControl();
            tc.Size = SERIALTABPANELSIZE;
            tc.Location = TOPLEFT;
            ret.Controls.Add(tc);

            TabPage term = new TabPage("Terminal");
            tc.Controls.Add(term);

            RichTextBox termout = new RichTextBox();
            link.TermOut = termout;
            termout.ReadOnly = true;
            termout.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
            termout.Location = TOPLEFT;
            termout.Size = SERIALTERMOUTSIZE;
            termout.TextChanged += (sender, e) =>
            {
                termout.SelectionStart = termout.Text.Length;
                // scroll it automatically
                termout.ScrollToCaret();
            };
            term.Controls.Add(termout);

            RichTextBox termin = new RichTextBox();
            link.TermIn = termin;
            termin.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
            termin.Location = SERIALTERMINLOCATION;
            termin.Size = SERIALTERMINSIZE;
            termin.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    link.NewCmd(((RichTextBox)sender).Text + "\r\n");
                    e.SuppressKeyPress = true;
                    ((RichTextBox)sender).Clear();
                }
            };
            term.Controls.Add(termin);

            Button expand = new Button();

            expand.Text = "Debug Terminal in Separate Window";
            expand.Location = SERIALEXPANDBUTTONLOCATION;
            expand.Size = SERIALEXPANDBUTTONSIZE;

            term.Controls.Add(expand);


            return ret;
        }

        private Control CreateSerialNPMParamsGB(SerialNPMLink link)
        {
            GroupBox ret = new GroupBox();
            ret.Text = "NPM Params";
            ret.Size = SERIALPARAMSGBSIZE;
            ret.Location = SERIALPARAMSGBLOCATION;

            return ret;
        }

        private Control CreateSerialNPMTopParamsGB(SerialNPMLink link)
        {
            GroupBox ret = new GroupBox();
            ret.Text = "Top NPM Params";
            ret.Size = SERIALTOPPARAMSGBSIZE;
            ret.Location = SERIALTOPPARAMSGBLOCATION;


            return ret;

        }

        private async void openFWUpgrader(object sender, EventArgs e)
        {
            if (npmTabControl.Controls.Count == 0)
            {
                MessageBox.Show("Please select a device to update.");
                return;
            }

            if (TCPLinkTracking.Count != 0)
            {
                TCPNPMLink link = TCPLinkTracking[npmTabControl.SelectedTab.Text];
                if (!await link.tcpMan.IsConnectedDeep())
                {
                    MessageBox.Show("Please connect to the device before updating.");
                    return;
                }
                new FWUpgradeTCP(link).Show();

            } 
            else
            {
                SerialNPMLink link = SerialLinkTracking[npmTabControl.SelectedTab.Text];
                if (!link.IsConnected())
                {
                    MessageBox.Show("Please connect to the device before updating.");
                    return;
                }

                new FWUpgradeSerial(link).Show();

            }


        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void enterIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTCPTab("192.168.15.205", "", "QIY_A_103");
        }
    }
}

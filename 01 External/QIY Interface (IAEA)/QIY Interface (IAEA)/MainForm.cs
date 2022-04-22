using QIY_Interface__IAEA_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIY_Interface__IAEA_
{
    public partial class MainForm : Form
    {
        internal UDPManager udpMan;
        TCPManager tcpMan;
        DirectoriesForm directoriesForm;
        DataRetrievalForm dataRetrievalForm;
        IpListForm ipListForm;
        TerminalForm term;
        OfflinePlotForm offlinePlot= new OfflinePlotForm();
        OfflineHGMForm offlineHGM= new OfflineHGMForm();
        OnlineHGMForm onlineHGM;
        OnlinePlotForm onlinePlot;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ResizeWindow(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            udpMan = new UDPManager(this);
            term = new TerminalForm(this);
            ipListForm = new IpListForm(this);
            directoriesForm = new DirectoriesForm();
            localIP.Text = $"Local PC IP Addr: {GetLocalIPAddress()}";
            udpMan.SearchNeuchQIY();

        }

        internal void NewTermData(string strDat)
        {
            if (term == null) return;
            term.NewData(strDat);
        }

        internal void NewTermCmd(string text)
        {

            tcpMan.NewCmd(text);
            if (text.Contains('='))
            {
                tcpMan.NewCmd("info\r\n");
            }
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "Not Found";
        }

        internal void UpdateGenSN(string data)
        {
            genSN.Text = data;
        }
        internal void UpdateGenFW(string data)
        {
            genFW.Text = data;
        }
        internal void UpdateGenName(string data)
        {
            genName.Text = data;
            paramName.Text = data;
            paramName.BackColor = SystemColors.ScrollBar;
        }

        internal void NewHGM(string hgm)
        {
            if (onlineHGM != null)
            {
                onlineHGM.IncomingHGM(hgm);
            }
        }


        internal void IncomingPrintDat(string line)
        {
            if (onlinePlot != null)
            {
                onlinePlot.IncomingDat(line);
            }

        }

        internal void UpdateGenMV(string data)
        {
            genMV.Text = data;
        }
        internal void UpdateGenMAC(string data)
        {
            genMAC.Text = data;
        }


        internal void UpdateGenVoltage(string data)
        {
            paramVoltage.Text = data;
            paramVoltage.BackColor = SystemColors.ScrollBar;
        }
        
        internal void UpdateGenMaxVoltage(string data)
        {
            paramMaxVoltage.Text = data;
            paramMaxVoltage.BackColor = SystemColors.ScrollBar;
        }

        internal void UpdateInternalDisk(string v)
        {
            statSD0.Text = v;
        }

        internal void UpdateExternalDisk(string v)
        {
            statSD1.Text = v;
        }

        internal void UpdateDate(string v)
        {
            statTime.Text = v;
            pcTime.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
        }

        internal void UpdateGenGain(string data)
        {
            paramGain.Text = data;
            paramGain.BackColor = SystemColors.ScrollBar;
        }
        internal void UpdateGenDiscLow(string data)
        {
            paramDiscLow.Text = data;
            paramDiscLow.BackColor = SystemColors.ScrollBar;
        }
        internal void UpdateGenDiscHigh(string data)
        {
            paramDiscHigh.Text = data;
            paramDiscHigh.BackColor = SystemColors.ScrollBar;
        }
        internal void UpdateGenDeadTime(string data)
        {
            paramDeadTime.Text = data;
            paramDeadTime.BackColor = SystemColors.ScrollBar;

            maxCtnRate.Text = ((int)(1 / (int.Parse(data) * Math.Pow(10, -6)))).ToString();

        }
        internal void UpdateGenNBins(string data)
        {
            paramNBins.Text = data;
            paramNBins.BackColor = SystemColors.ScrollBar;
        }
        internal void UpdateGenLEDMode(string data)
        {
            paramLEDMode.Text = data;
            paramLEDMode.BackColor = SystemColors.ScrollBar;
            if (data.Equals("0")) ledOff.Checked = true;
            else ledOn.Checked = true;
        }
        internal void UpdateGenPulseLevel(string data)
        {
            paramPulseLevel.Text = data;
            paramPulseLevel.BackColor = SystemColors.ScrollBar;
        }
        internal void UpdateGenPrintDAT(string data)
        {
            paramPrintDAT.Text = data;
            paramPrintDAT.BackColor = SystemColors.ScrollBar;
            if (data.Equals("0")) printDATOff.Checked = true;
            else printDATOn.Checked = true;
        }
        internal void UpdateGenSaveDAT(string data)
        {
            paramSaveDAT.Text = data;
            paramSaveDAT.BackColor = SystemColors.ScrollBar;
            if (data.Equals("0")) saveDATOff.Checked = true;
            else saveDATOn.Checked = true;
        }
        internal void UpdateGenSaveBin(string data)
        {
            paramSaveBin.Text = data;
            paramSaveBin.BackColor = SystemColors.ScrollBar;
            if (data.Equals("0")) saveBinOff.Checked = true;
            else saveBinOn.Checked = true;
        }
        internal void UpdateGenPrintHGM(string data)
        {
            paramPrintHGM.Text = data;
            paramPrintHGM.BackColor = SystemColors.ScrollBar;
            if (data.Equals("0")) printHGMOff.Checked = true;
            else printHGMOn.Checked = true;
        }
        internal void UpdateGenSaveHGM(string data)
        {
            paramSaveHGM.Text = data;
            paramSaveHGM.BackColor = SystemColors.ScrollBar;
            if (data.Equals("0")) saveHGMOff.Checked = true;
            else saveHGMOn.Checked = true;
        }
        internal void UpdatePulseCounterOn(string data)
        {
            paramPulseCounterOn.Text = data;
            paramPulseCounterOn.BackColor = SystemColors.ScrollBar;
            if (data.Equals("0"))
            {
                pulseCounterOff.Checked = true;
                tcpMan.extPulseOn = false;
            }
            else
            {
                pulseCounterOn.Checked = true;
                tcpMan.extPulseOn = true;
            }
        }
        internal void UpdateTemperatureOn(string data)
        {
            paramTemperatureOn.Text = data;
            paramTemperatureOn.BackColor = SystemColors.ScrollBar;
            if (data.Equals("0"))
            {
                temperatureOff.Checked = true;
                tcpMan.tempOn = false;
            }
            else
            {
                temperatureOn.Checked = true;
                tcpMan.tempOn = true;
            }
        }
        internal void UpdateHumidityOn(string data)
        {
            paramHumidityOn.Text = data;
            paramHumidityOn.BackColor = SystemColors.ScrollBar;
            if (data.Equals("0"))
            {
                humidityOff.Checked = true;
                tcpMan.humOn = false;
            }
            else
            {
                humidityOn.Checked = true;
                tcpMan.humOn = true;
            }
        }

        internal void UpdateHGMMode(string data)
        {
            if (int.TryParse(data, out int hgmmode))
                tcpMan.hgmMode = hgmmode;
        }

        internal void UpdateBatteryOn(string data)
        {
            paramBatteryOn.Text = data;
            paramBatteryOn.BackColor = SystemColors.ScrollBar;
            if (data.Equals("0"))
            {
                batteryOff.Checked = true;
                tcpMan.battOn = false;
            }
            else
            {
                batteryOn.Checked = true;
                tcpMan.battOn = true;
            }
        }
        
        internal void UpdateSignalOn(string data)
        {
            paramSignalOn.Text = data;
            paramSignalOn.BackColor = SystemColors.ScrollBar;
            if (data.Equals("0"))
            {
                signalOff.Checked = true;
                tcpMan.sigOn = false;
            }
            else
            {
                signalOn.Checked = true;
                tcpMan.sigOn = true;
            }
        }
        internal void UpdateRecordPeriod(string data)
        {
            paramRecordPeriod.Text = data;
            paramRecordPeriod.BackColor = SystemColors.ScrollBar;
            TryFillHGMPeriod();
        }


        internal void UpdateRecordsPerHGM(string data)
        {
            paramRecordsPerHGM.Text = data;
            paramRecordsPerHGM.BackColor = SystemColors.ScrollBar;
            TryFillHGMPeriod();
        }
        internal void UpdateNewFilePeriod(string data)
        {
            paramNewFilePeriod.Text = data;
            paramNewFilePeriod.BackColor = SystemColors.ScrollBar;
            if (data.Equals("Day")) nfDay.Checked = true;
            else if (data.Equals("Month")) nfMonth.Checked = true;
            else nfYear.Checked = true;
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        internal void ParseUDP(string data)
        {
            string[] incomingData = data.Split('\n');
            // no new data
            if (data.Length == 0) return;

            /////////////////// IP ///////////////////
            foreach (string incoming in incomingData)
            {
                if (incoming.Split(',').Length == 1) continue;
                // 70B3D588F0D5,C0A80FD5,FFFFFF00,C0A80F01,10001,QIY_A_100,NE0D5
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
                    mac += macIndex.Substring(i, 2) + ":";
                }
                mac = mac.Trim(':', ' ');

                string add = $"{ip}";
                if (IPList.Items.Contains(add)) continue;
                IPList.SelectedIndex = IPList.Items.Add(add);
                ipListForm.listBox.Items.Add(ip);
            }

        }

        internal void EditIpList(List<string> ips)
        {
            IPList.Items.Clear();

            foreach (string ip in ips)
            {
                IPList.Items.Add(ip);
            }
        }

        private void findDevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            udpMan.SearchNeuchQIY();
        }

        private void pingBtn_Click(object sender, EventArgs e)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;


            string ip = IPList.Text;
            if (!IPAddress.TryParse(ip, out IPAddress a))
            {
                MessageBox.Show($"'{IPList.Text}' is not a valid IP", "ERROR");
                return;
            };
            
            PingReply reply = pingSender.Send(ip, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                pingBox.Clear();
                //Debug.WriteLine("Address: {0}", reply.Address.ToString());
                pingBox.AppendText($"Pinging {ip}...{Environment.NewLine}");
                pingBox.AppendText($"Roundtrip Time: {reply.RoundtripTime}ms{Environment.NewLine}");
                pingBox.AppendText($"Sent: { buffer.Length}b{Environment.NewLine}");
                pingBox.AppendText($"Got: { reply.Buffer.Length}b");
            }
            else
            {
                pingBox.Text = "No response";
            }
        }

        private async void ConnectBtn_Click(object sender, EventArgs e)
        {
            string ip = IPList.Text;
            if (!IPAddress.TryParse(ip, out IPAddress a))
            {
                MessageBox.Show($"'{IPList.Text}' is not a valid IP", "ERROR");
                return;
            };

            // change tcpman 
            
            tcpMan = new TCPManager(ip, this);

            if (!await tcpMan.TryConnectionAsync())
            {
                MessageBox.Show("Could not connect to NPM. Is something else already connected?", "ERROR");
                return;

            }

            tcpMan.AddListener(new TCPListener(this));

            disconnectBtn.Enabled = true;
            ConnectBtn.Enabled = false;
            sockState.Text = "TCP Socket State: Open";
            sockState.ForeColor = Color.Green;
            genIP.Text = ip;
            dlParamGb.Enabled = true;
            npmParamGB.Enabled = true;
            genPanel.Enabled = true;
            statusGb.Enabled = true;
            queryNpmBtn.Enabled = true;
            RunQueryCommands();
        }

        internal void UpdateStatFwVersion(string data)
        {
            statFw.Text = data;
        }
        internal void UpdateStatName(string data)
        {
            statName.Text = data;
            mainTab.Text = $"Connected: {data} ({tcpMan.GetIP()})";
            tcpMan.SetName(data); 
        }
        
        internal void UpdateStatSetHV(string data)
        {
            statSetHV.Text = data;
        }
        internal void UpdateStatMeasuredHV(string data)
        {
            statMeasHV.Text = data;
        }
        internal void UpdateStatTemp(string data)
        {
            statTemp.Text = data;
        }
        internal void UpdateStatRH(string data)
        {
            statRH.Text = data;
        }
        internal void UpdateStatBatt(string data)
        {
            statBatt.Text = data;
        }
        internal void UpdateStatSignal(string data)
        {
            statSignal.Text = data;
        }
        internal void UpdateStatPulselevel(string data)
        {
            statPulse.Text = data;
        }
        internal void UpdateStatRecordPeriod(string data)
        {
            statRecordPeriod.Text = data;
        }
        internal void UpdateStatRecordsPerHGM(string data)
        {
            statRecordsPerHGM.Text = data;
        }
        internal void UpdateStatNewFilePeriod(string data)
        {
            statNewFilePeriod.Text = data;
        }
        internal void UpdateStatCurrRec(string data)
        {
            StatCurRec.Text = data;
        }
        internal void UpdateStatSaveDat(string data)
        {
            statSaveDat.Text = data;
        }
        internal void UpdateStatSaveBin(string data)
        {
            statSaveBin.Text = data;
        }
        internal void UpdateStatSaveHGM(string data)
        {
            statSaveHGM.Text = data;
        }
        internal void UpdateStatDatFile(string data)
        {
            statDatFile.Text = data;
        }
        internal void UpdateStatBinFile(string data)
        {
            statBinFile.Text = data;
        }
        internal void UpdateStatHGMFile(string data)
        {
            statHgmFile.Text = data;
        }
        internal void UpdateUptime(string data)
        {
            double days = double.Parse(data) / 86400;
            statUptimeDays.Text = $"{days:0.0}";
            statUptimeSecs.Text = data;
        }

        private async void RunQueryCommands()
        {
            if (tcpMan == null) return;
            tcpMan.NewCmd("info\r\n");
            tcpMan.NewCmd("status\r\n");
            tcpMan.NewCmd("time\r\n");
            tcpMan.NewCmd("diskinfo\r\n");
            //Debug.WriteLine(await tcpMan.SendCommandAsync("info\r\n"));
        }

        private void queryNpmBtn_Click(object sender, EventArgs e)
        {
            RunQueryCommands();
        }

        internal void disconnectBtn_Click(object sender, EventArgs e)
        {
            disconnectBtn.Enabled = false;
            ConnectBtn.Enabled = true;
            tcpMan.Disconnect();
            clearInfo();
            dlParamGb.Enabled = false;
            npmParamGB.Enabled = false;
            genPanel.Enabled = false;
            statusGb.Enabled = false;
            sockState.Text = "TCP Socket State: Closed";
            sockState.ForeColor = Color.Red;
            mainTab.Text = "Not Connected";
            if (onlineHGM != null)
                onlineHGM.Hide();
            term.Hide();
            if (dataRetrievalForm != null)
                dataRetrievalForm.Hide();
            if (onlinePlot != null)
                onlinePlot.Hide();

        }

        private void clearInfo()
        {
            //foreach (TextBox tb in npmParamGB.Controls)
            //{

            //}
            //foreach (Control c in dlParamGb.Controls)
            //{
            //    if (c.Name.Contains("param")) c.Text = "";
            //    if (c is Panel)
            //    {
            //        foreach (Control r in c.Controls)
            //        {
            //            if (r is RadioButton)
            //                ((RadioButton)r).Checked = false;
            //        }
            //    }
            //    if (c is RadioButton)
            //        ((RadioButton)c).Checked = false;

            //}
            //foreach (Control c in statusGb.Controls)
            //{
            //    if (c.Name.Contains("param")) c.Text = "";
            //}
            //foreach (Control c in genPanel.Controls)
            //{
            //    if (c.Name.Contains("gen")) c.Text = "";
            //}

            //queryNpmBtn.Enabled = false;
        }


        internal void ImmEscapeChar()
        {
            if (tcpMan == null || !tcpMan.IsConnectedShallow()) return;
            tcpMan.SendByte(0x1B);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (tcpMan == null || !tcpMan.IsConnectedShallow())
            {
                MessageBox.Show("Not Connected", "ERROR");
                return;
            }
            if (dataRetrievalForm != null) dataRetrievalForm.Dispose();

            if (paramPrintDAT.Text == "1" || paramPrintHGM.Text == "1")
            {
                for (int i = 0; i < 2; i++)
                {
                    tcpMan.NewCmd("printdat=0\r\n");
                    Application.DoEvents();
                    tcpMan.NewCmd("printhgm=0\r\n");
                    RunQueryCommands();
                    Refresh();
                    Application.DoEvents();
                    Thread.Sleep(100);
                }
            }
            dataRetrievalForm = new DataRetrievalForm(tcpMan, genName.Text);
            dataRetrievalForm.Text = $"Data Retrieval: ({tcpMan.GetIP()})";
            dataRetrievalForm.Show();
        }

        internal void UpdateInternalDRBox(string strDat, bool clear = false)
        {
            if (dataRetrievalForm == null || dataRetrievalForm.IsDisposed) return;
            if (clear)
            {
                dataRetrievalForm.internalDirBox.Items.Clear();
                return;
            }
            else
            {
                dataRetrievalForm.internalDirBox.Items.Add(strDat.Trim());
            }
        }
        
        internal void UpdateExternalDRBox(string strDat, bool clear = false)
        {
            if (dataRetrievalForm == null || dataRetrievalForm.IsDisposed) return;
            if (clear)
            {
                dataRetrievalForm.externalDirBox.Items.Clear();
                return;
            }
            else
            {
                dataRetrievalForm.externalDirBox.Items.Add(strDat.Trim());
            }
        }
        
        internal void UpdateInternalInfoDRBox(string strDat, bool clear = false)
        {
            if (dataRetrievalForm == null || dataRetrievalForm.IsDisposed) return;
            if (clear)
            {
                dataRetrievalForm.internalInfoBox.Clear();
                return;
            }
            else
            {
                dataRetrievalForm.internalInfoBox.AppendText(strDat.Trim() + Environment.NewLine);
            }
        }
        internal void UpdateExternalInfoDRBox(string strDat, bool clear = false)
        {
            if (dataRetrievalForm == null || dataRetrievalForm.IsDisposed) return;
            if (clear)
            {
                dataRetrievalForm.externalInfoBox.Clear();
                return;
            }
            else
            {
                dataRetrievalForm.externalInfoBox.AppendText(strDat.Trim() + Environment.NewLine);
            }
        }

        internal void UpdateExternalDRDetails(string strDat, bool select = false, bool clear = false)
        {
            if (dataRetrievalForm == null || dataRetrievalForm.IsDisposed) return;
            if (clear)
            {
                dataRetrievalForm.externalBytesSelectedBox.Clear();
                dataRetrievalForm.externalSelectedFilesBox.Clear();
                dataRetrievalForm.externalTotalFilesBox.Clear();
                return;
            }
            else
            {
                if (select)
                {

                }
                else
                    dataRetrievalForm.externalTotalFilesBox.Text = strDat;
            }
        }
        internal void UpdateInternalDRDetails(string strDat, bool select = false, bool clear = false)
        {
            if (dataRetrievalForm == null || dataRetrievalForm.IsDisposed) return;
            if (clear)
            {
                dataRetrievalForm.internalBytesSelectedBox.Clear();
                dataRetrievalForm.internalSelectedFilesBox.Clear();
                dataRetrievalForm.internalTotalFilesBox.Clear();
                return;
            }
            else
            {
                if (select)
                {

                }
                else
                    dataRetrievalForm.internalTotalFilesBox.Text = strDat;
            }
        }

        private void EditParam(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }

        private void EditByRadio(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            Panel parent = (Panel)(btn.Parent);
            foreach (TextBox disp in parent.Controls.OfType<TextBox>())
            {
                if (btn.Name.Contains("On"))
                    disp.Text = "1";
                else disp.Text = "0";
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (tcpMan == null || !tcpMan.IsConnectedShallow())
            {
                MessageBox.Show("Not Connected", "ERROR");
                return;
            }
            term.Text = $"{tcpMan.GetIP()} -- Terminal";
            term.Show();
        }

        private void TryFillHGMPeriod()
        {
            if (paramRecordPeriod.Text.Length != 0 && paramRecordsPerHGM.Text.Length != 0)
            {
                if (int.TryParse(paramRecordPeriod.Text, out int rp) && int.TryParse(paramRecordsPerHGM.Text, out int rphgm))
                {
                    TimeSpan hgmPeriod = TimeSpan.FromSeconds(rp*rphgm);
                    TimeSpan ctsPeriod = TimeSpan.FromSeconds(rp);

                    ctsSampleMin.Text = $"{ctsPeriod.TotalMinutes:0.1}";
                    ctsSampleHrs.Text = $"{ctsPeriod.TotalHours:0.01}";

                    hgmPeriodMin.Text = $"{hgmPeriod.TotalMinutes:0.1}";
                    hgmPeriodHrs.Text = $"{hgmPeriod.TotalHours:0.01}";

                }
            }
        }
        private void Apply_Click(object sender, EventArgs e)
        {
            if (paramName.BackColor == Color.White)
            {
                tcpMan.NewCmd($"name={paramName.Text}\r\n");
                tcpMan.NewCmd($"name={paramName.Text}\r\n");
            }
            if (paramMaxVoltage.BackColor == Color.White)
            {
                tcpMan.NewCmd($"maxvoltage={paramMaxVoltage.Text}\r\n");
                tcpMan.NewCmd($"maxvoltage={paramMaxVoltage.Text}\r\n");
            }
            if (paramGain.BackColor == Color.White)
            {
                tcpMan.NewCmd($"gain={paramGain.Text}\r\n");
                tcpMan.NewCmd($"gain={paramGain.Text}\r\n");
            }
            if (paramVoltage.BackColor == Color.White)
            {
                tcpMan.NewCmd($"voltage={paramVoltage.Text}\r\n");
                tcpMan.NewCmd($"voltage={paramVoltage.Text}\r\n");
            }
            if (paramNBins.BackColor == Color.White)
            {
                tcpMan.NewCmd($"nbins={paramNBins.Text}\r\n");
                tcpMan.NewCmd($"nbins={paramNBins.Text}\r\n");
            }
            if (paramDiscLow.BackColor == Color.White)
            {
                tcpMan.NewCmd($"disclow={paramDiscLow.Text}\r\n");
                tcpMan.NewCmd($"disclow={paramDiscLow.Text}\r\n");
            }
            if (paramDiscHigh.BackColor == Color.White)
            {
                tcpMan.NewCmd($"dischigh={paramDiscHigh.Text}\r\n");
                tcpMan.NewCmd($"dischigh={paramDiscHigh.Text}\r\n");
            }
            if (paramDeadTime.BackColor == Color.White)
            {
                tcpMan.NewCmd($"deadtime={paramDeadTime.Text}\r\n");
                tcpMan.NewCmd($"deadtime={paramDeadTime.Text}\r\n");
            }
            if (paramLEDMode.BackColor == Color.White)
            {
                tcpMan.NewCmd($"ledmode={paramLEDMode.Text}\r\n");
                tcpMan.NewCmd($"ledmode={paramLEDMode.Text}\r\n");
            }
            if (paramPulseLevel.BackColor == Color.White)
            {
                tcpMan.NewCmd($"pulselevel={paramPulseLevel.Text}\r\n");
                tcpMan.NewCmd($"pulselevel={paramPulseLevel.Text}\r\n");
            }
            if (paramPrintDAT.BackColor == Color.White)
            {
                tcpMan.NewCmd($"printdat={paramPrintDAT.Text}\r\n");
                tcpMan.NewCmd($"printdat={paramPrintDAT.Text}\r\n");
            }
            if (paramSaveDAT.BackColor == Color.White)
            {
                tcpMan.NewCmd($"savedat={paramSaveDAT.Text}\r\n");
                tcpMan.NewCmd($"savedat={paramSaveDAT.Text}\r\n");
            }
            if (paramSaveBin.BackColor == Color.White)
            {
                tcpMan.NewCmd($"savebin={paramSaveBin.Text}\r\n");
                tcpMan.NewCmd($"savebin={paramSaveBin.Text}\r\n");
            }
            if (paramPrintHGM.BackColor == Color.White)
            {
                tcpMan.NewCmd($"printhgm={paramPrintHGM.Text}\r\n");
                tcpMan.NewCmd($"printhgm={paramPrintHGM.Text}\r\n");
            }
            if (paramSaveHGM.BackColor == Color.White)
            {
                tcpMan.NewCmd($"savehgm={paramSaveHGM.Text}\r\n");
                tcpMan.NewCmd($"savehgm={paramSaveHGM.Text}\r\n");
            }
            if (paramPulseCounterOn.BackColor == Color.White)
            {
                tcpMan.NewCmd($"pulseon={paramPulseCounterOn.Text}\r\n");
                tcpMan.NewCmd($"pulseon={paramPulseCounterOn.Text}\r\n");
            }
            if (paramTemperatureOn.BackColor == Color.White)
            {
                tcpMan.NewCmd($"temperatureon={paramTemperatureOn.Text}\r\n");
                tcpMan.NewCmd($"temperatureon={paramTemperatureOn.Text}\r\n");
            }
            if (paramHumidityOn.BackColor == Color.White)
            {
                tcpMan.NewCmd($"humidityon={paramHumidityOn.Text}\r\n");
                tcpMan.NewCmd($"humidityon={paramHumidityOn.Text}\r\n");
            }
            if (paramBatteryOn.BackColor == Color.White)
            {
                tcpMan.NewCmd($"batteryon={paramBatteryOn.Text}\r\n");
                tcpMan.NewCmd($"batteryon={paramBatteryOn.Text}\r\n");
            }
            if (paramSignalOn.BackColor == Color.White)
            {
                tcpMan.NewCmd($"SignalOn={paramSignalOn.Text}\r\n");
                tcpMan.NewCmd($"SignalOn={paramSignalOn.Text}\r\n");
            }
            if (paramRecordsPerHGM.BackColor == Color.White)
            {
                tcpMan.NewCmd($"recordsperhgm={paramRecordsPerHGM.Text}\r\n");
                tcpMan.NewCmd($"recordsperhgm={paramRecordsPerHGM.Text}\r\n");
            }
            if (paramNewFilePeriod.BackColor == Color.White)
            {
                tcpMan.NewCmd($"NewFilePeriod={paramNewFilePeriod.Text}\r\n");
                tcpMan.NewCmd($"NewFilePeriod={paramNewFilePeriod.Text}\r\n");
            }
            if (paramRecordPeriod.BackColor == Color.White)
            {
                tcpMan.NewCmd($"recordperiod={paramRecordPeriod.Text}\r\n");
                tcpMan.NewCmd($"recordperiod={paramRecordPeriod.Text}\r\n");
            }
            RunQueryCommands();
            RunQueryCommands();
        }

        private void firmwareUpgradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tcpMan == null || !tcpMan.IsConnectedShallow())
            {
                MessageBox.Show("Not Connected", "ERROR");
                return;
            }
            string time = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            if (utc.Checked) time = DateTime.UtcNow.ToString("yyyy/MM/dd hh:mm:ss");
            tcpMan.NewCmd($"Time={time}");
            RunQueryCommands();
        }

        private void iPAddressListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ipListForm.Show();
        }

        private void workingDirectoriesAndFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            directoriesForm.Show();
        }

        private void supportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Quaesta Instruments LLC\n" +
                $"Tucson, AZ\n\n" +
                $"Software Version: QIY Interface, 1.0.0\n" +
                $"Build Date: February 25, 2022\n\n" +
                $"If help is needed:\n" +
                $"Web: https://www.quaestainstruments.com/ \n" +
                $"Email: support@quaestainstruments.com \n" +
                $"Phone: (520) 882 3706", "Support");
        }

        private void offlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            offlinePlot.Show();
            offlinePlot.Select();
        }

        private void offlineHistogramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            offlineHGM.Show();
            offlineHGM.Select();
        }

        private void realTimeDataSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tcpMan == null || !tcpMan.IsConnectedShallow())
            {
                MessageBox.Show("Not Connected", "ERROR");
                return;
            }
            if (onlinePlot != null)
            {
                onlinePlot.Show();
                onlinePlot.Select();
            } else
            {
                onlinePlot = new OnlinePlotForm(tcpMan, this);
                onlinePlot.Text = $"{tcpMan.GetIP()} -- Online Data Plotting";
                onlinePlot.Show();
            }
        }

        private void realTimeHistogramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tcpMan == null || !tcpMan.IsConnectedShallow())
            {
                MessageBox.Show("Not Connected", "ERROR");
                return;
            }
            if (onlineHGM != null)
            {
                onlineHGM.Show();
                onlineHGM.Show();
            } else
            {
                onlineHGM = new OnlineHGMForm(tcpMan);
                onlineHGM.Text = $"{tcpMan.GetIP()} -- Online Histogram Plotting";
                onlineHGM.Show();
            }
        }

        private void SpecialRadioChange(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (!rb.Checked) return;

            paramNewFilePeriod.Text = rb.Name.Trim('n', 'f');

        }


        internal string GetCurParams()
        {
            return $"{paramName.Text}|" +
                $"{genSN.Text}|" +
                $"{paramRecordPeriod.Text}|" +
                $"{paramRecordsPerHGM.Text}|" +
                $"{paramNewFilePeriod.Text}|" +
                $"{paramVoltage.Text}|" +
                $"{paramGain.Text}|" +
                $"{paramDiscLow.Text}|" +
                $"{paramDiscHigh.Text}|" +
                $"{paramSaveHGM.Text}|" +
                $"{paramPulseLevel.Text}|" +
                $"{paramPulseCounterOn.Text}|" +
                $"{paramTemperatureOn.Text}|" +
                $"{paramHumidityOn.Text}|" +
                $"{paramBatteryOn.Text}|" +
                $"{paramSignalOn.Text}|" +
                $"{paramSaveDAT.Text}|" +
                $"{paramSaveHGM.Text}|" +
                $"{paramSaveBin.Text}|";
        }

        private void firmwareUpgradeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tcpMan == null || !tcpMan.IsConnectedShallow())
            {
                MessageBox.Show("Not Connected", "ERROR");
                return;
            }
            new FWUpgradeTCP(tcpMan).ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

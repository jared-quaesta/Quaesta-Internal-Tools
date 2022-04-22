using NPM_General_App.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPM_General_App.SerialNPM
{
    class SerialNPMLink
    {
        // Status
        Button connectionStatus;

        // NPM PARAMS
        TextBox npmModel; //
        TextBox npmModelVersion; //
        TextBox npmSerial; //
        TextBox npmTimeConstant; //


        TextBox npmName; //
        TextBox npmIdentifier; //
        TextBox npmLocalAddress; //
        TextBox npmMaxVoltage; //
        TextBox npmSetVoltage; //
        TextBox npmMeasuredVoltage; //
        TextBox npmGain; //
        TextBox npmLowerDisc; //
        TextBox npmUpperDisc; //
        TextBox npmNBins;  //
        TextBox npmDeadTime; //
        TextBox npmMaxCountRate;
        TextBox npmLEDMode; //
        TextBox npmPulseLevel;
        TextBox npmPeakMode; //
        TextBox npmHGMMode; //
        TextBox npmPulseSim; //
        TextBox npmDTRes; //                    


        TextBox npmFwVersion; //
        TextBox npmTTLMode; //
        TextBox npmTTLWidth; //
        TextBox npmTTLCounter; //


        // List Params
        TextBox npmListStream; //
        TextBox npmListRamMode; //
        TextBox npmListRamMax; //
        TextBox npmListRamStatus; //


        // Terminal
        RichTextBox termOut;
        RichTextBox termIn;
        Button openAdv;

        // views and controls
        private MainForm main;
        private SerialNPMManager serialMan;
        private SerialListener listener;
        private string com;

        // states
        internal bool updating = false;
        private bool gotC = false;
        private bool gotACK = false;
        private bool gotNAK = false;
        private bool gotY = false;

        // constants
        private readonly string ACK = Encoding.UTF8.GetString(new byte[] { 0x06 });
        private readonly string NAK = Encoding.UTF8.GetString(new byte[] { 0x21 });

        public SerialNPMLink(MainForm main, string com)
        {
            this.com = com;
            this.main = main;
            
            // make listener and manager
            listener = new SerialListener(this);
            serialMan = new SerialNPMManager(listener, com);        
        }


        internal void NewDataUpdating(string data)
        {

        }

        internal async void UpdateNPM(string filename, FWUpgradeSerial fWUpgradeSerial)
        {
            if (updating) return;
            updating = true; 
            main.Invoke((MethodInvoker)delegate
            {
                termIn.Enabled = false;
            });
            fWUpgradeSerial.Invoke((MethodInvoker)delegate
            {
                fWUpgradeSerial.getPt().Text = $"Awaiting Update Signal...";
            });
            fWUpgradeSerial.getPB().Maximum = ((int)new FileInfo(filename).Length / 128) + 2;

            await Task.Run(() =>
            {
                serialMan.SendCommand("updatefirmware\r");
                Debug.WriteLine("Waiting for C...");
                while (!gotC)
                {
                    Thread.Sleep(10);
                }
                Debug.WriteLine("Got C...");
                gotC = false;

                using (Stream source = File.OpenRead(filename))
                {
                    byte[] buffer = new byte[128];
                    int bytesRead;
                    Debug.WriteLine("Reading bytes from file:");
                    int blocknum = 0;
                    while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        byte[] block = new byte[133];

                        // make padding
                        block[0] = 0x01;
                        block[1] = 0x00;
                        block[2] = 0x00;
                        block[131] = 0x00;
                        block[132] = 0x00;

                        // fill in data
                        for (int i = 0; i < 128; i++)
                        {
                            block[i + 3] = buffer[i];
                        }

                        //NewCmd("", block);
                        serialMan.SendBytes(block, 0);
                        while (!gotACK)
                        {
                            Thread.Sleep(1);
                        }
                        gotACK = false;

                        fWUpgradeSerial.Invoke((MethodInvoker)delegate
                        {
                            fWUpgradeSerial.getPB().Value = blocknum++;
                            fWUpgradeSerial.getPt().Text = $"Sending Block {fWUpgradeSerial.getPB().Value} of {fWUpgradeSerial.getPB().Maximum}";
                            fWUpgradeSerial.Refresh();
                        });

                    }

                    // finish transfer


                    serialMan.SendBytes(new byte[] { 0x04 });
                    Debug.WriteLine("Sending finish");
                    try
                    {
                        while (true)
                            serialMan.SendCommand("Y");
                        Thread.Sleep(100);

                    }
                    catch
                    {
                        Debug.WriteLine("Done");
                    }

                    while (!TryConnect())
                    {
                        fWUpgradeSerial.Invoke((MethodInvoker)delegate
                        {
                            fWUpgradeSerial.getPB().Value = fWUpgradeSerial.getPB().Maximum;
                            fWUpgradeSerial.getPt().Text = $"Reconnecting...";
                            fWUpgradeSerial.Refresh();
                        });
                        Thread.Sleep(10);
                    }
                    updating = false;
                    main.Invoke((MethodInvoker)delegate
                    {
                        termIn.Enabled = true;
                    });


                }
                fWUpgradeSerial.Invoke((MethodInvoker)delegate
                {
                    fWUpgradeSerial.Close();
                });
            });
        }
        internal bool TryConnect()
        {
            if (serialMan.Connect(com))
            {
                return true;
            } return false;
        }

        internal bool IsConnected()
        {
            return serialMan.IsConnected();
        }

        internal void Disconnect()
        {
            serialMan.Disconnect();
        }

        internal void NewCmd(string cmd)
        {
            serialMan.SendCommand(cmd);
        }

        internal void NewData(string data)
        {
            if (data.Equals("C")) gotC = true;
            if (data.Equals(NAK)) gotNAK = true;
            if (data.Equals(ACK)) gotACK = true;
            if (data.Equals("'Y'")) gotY = true;
            if (updating) return;
            main.Invoke((MethodInvoker)delegate
            {
                termOut.AppendText(data);
            });
        }


        public Button ConnectionStatus { get => connectionStatus; set => connectionStatus = value; }
        public TextBox NpmModel { get => npmModel; set => npmModel = value; }
        public TextBox NpmModelVersion { get => npmModelVersion; set => npmModelVersion = value; }
        public TextBox NpmSerial { get => npmSerial; set => npmSerial = value; }
        public TextBox NpmTimeConstant { get => npmTimeConstant; set => npmTimeConstant = value; }
        public TextBox NpmName { get => npmName; set => npmName = value; }
        public TextBox NpmIdentifier { get => npmIdentifier; set => npmIdentifier = value; }
        public TextBox NpmLocalAddress { get => npmLocalAddress; set => npmLocalAddress = value; }
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
        public TextBox NpmPeakMode { get => npmPeakMode; set => npmPeakMode = value; }
        public TextBox NpmHGMMode { get => npmHGMMode; set => npmHGMMode = value; }
        public TextBox NpmPulseSim { get => npmPulseSim; set => npmPulseSim = value; }
        public TextBox NpmDTRes { get => npmDTRes; set => npmDTRes = value; }
        public TextBox NpmFwVersion { get => npmFwVersion; set => npmFwVersion = value; }
        public TextBox NpmTTLMode { get => npmTTLMode; set => npmTTLMode = value; }
        public TextBox NpmTTLWidth { get => npmTTLWidth; set => npmTTLWidth = value; }
        public TextBox NpmTTLCounter { get => npmTTLCounter; set => npmTTLCounter = value; }
        public TextBox NpmListStream { get => npmListStream; set => npmListStream = value; }
        public TextBox NpmListRamMode { get => npmListRamMode; set => npmListRamMode = value; }
        public TextBox NpmListRamMax { get => npmListRamMax; set => npmListRamMax = value; }
        public TextBox NpmListRamStatus { get => npmListRamStatus; set => npmListRamStatus = value; }
        public RichTextBox TermOut { get => termOut; set => termOut = value; }
        public RichTextBox TermIn { get => termIn; set => termIn = value; }
        public Button OpenAdv { get => openAdv; set => openAdv = value; }

    }
}

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NPM_General_App
{
    internal class TCPListener
    {
        TCPNPMLink link;
        string incompleteInput = "";
        bool isConnected = false;
        string lastCommand = "";
        bool gotStatusString = false;
        bool gotAck = false;

        public TCPListener(TCPNPMLink link)
        {
            this.link = link;
        }

        internal void IncomingData(string newData, string lastCommand)
        {
            // no real data
            if (newData.Trim().Length == 0) return;
            this.lastCommand = lastCommand;
            Debug.WriteLine("NEW DATA");

            link.RefreshTerminal(newData);

            ParseMain(newData);
        }

        internal void UpdateConnected(bool status)
        {
            if (isConnected != status)
            {
                isConnected = status;
                link.UpdateConnectionStatus(status);
            }
        }

        internal void ParseMain(string raw)
        {
            // combine last incomplete data, then clear it
            string[] data = (incompleteInput + raw).Split("\n");
            incompleteInput = "";

            Debug.WriteLine(lastCommand);
            for (int i = 0; i < data.Length; i++)
            {
                string line = data[i];
                if (line.Length == 0) continue;
                if (!line.Contains('\r'))
                {
                    incompleteInput += line;
                }

                string[] splitLine = line.Split(' ');
                string tryMe = splitLine[splitLine.Length - 1].Trim('\r', '\n');

                if (lastCommand.ToLower().Trim('\r', '\n', ' ').Equals("info"))
                {
                    ParseInfo(line, tryMe);
                }

                if (lastCommand.ToLower().Trim('\r', '\n', ' ').Equals("status"))
                {
                    if (line.Contains("status"))
                    {
                        Debug.WriteLine("Start Status");
                        link.statusString = "";
                    }
                    link.EditStatus(line.Trim());

                    if (line.Contains("Current Time"))
                    {
                        Debug.WriteLine("End Status");
                        gotStatusString = true;
                    }

                    if (line.Contains("Temperature"))
                    {
                        link.UpdateTempPlot(double.Parse(tryMe));
                    }
                    if (line.Contains("Humidity"))
                    {
                        link.UpdateHumPlot(double.Parse(tryMe));
                    }
                    
                }

            }


        }

        internal string GetStatusString()
        {
            gotStatusString = false;
            return link.statusString;
        }

        internal bool GotStatus()
        {
            return gotStatusString;
        }

        private void ParseInfo(string line, string tryMe)
        {
            if (line.Contains("Serial Number"))
            {
                link.SetSerial(tryMe);
            }

            if (line.Contains("RecordPeriod(Sec)"))
            {
                link.SetRecordPeriod(tryMe);
            }

            if (line.Contains("RecordsPerHGM"))
            {
                link.SetRecordsPerHGM(tryMe);
            }

            if (line.Contains("NewFilePeriod"))
            {
                link.SetNewFilePeriod(tryMe);
            }

            if (line.Contains("PrintDAT"))
            {
                link.SetPrintDat(tryMe);
            }

            if (line.Contains("TemperatureON"))
            {
                link.SetTemperatureOn(tryMe);
            }

            if (line.Contains("HumidityON"))
            {
                link.SetHumidityOn(tryMe);
            }

            if (line.Contains("BatteryON"))
            {
                link.SetBatteryOn(tryMe);
            }

            if (line.Contains("SignalON"))
            {
                link.SetSignalOn(tryMe);
            }

            if (line.Contains("SaveHGM") || line.Contains("LogHGM"))
            {
                link.SetSaveHGM(tryMe);
            }

            if (line.Contains("SaveDAT") || line.Contains("LogDAT"))
            {
                link.SetSaveDat(tryMe);
            }

            if (line.Contains("LogTemp"))
            {
                link.SetSaveTemp(tryMe);
            }

            if (line.Contains("LogHum"))
            {
                link.SetSaveHum(tryMe);
            }

            if (line.Contains("SaveDAT") || line.Contains("LogDAT"))
            {
                link.SetSaveDat(tryMe);
            }

            if (line.Contains("PrintHGM"))
            {
                link.SetPrintHGM(tryMe);
            }

            if (line.Contains("SaveBIN"))
            {
                link.SetSaveBin(tryMe);
            }

            if (line.Contains("PulseON"))
            {
                link.SetPulseCounterOn(tryMe);
            }

            if (line.Contains("Model Version"))
            {
                link.SetModelVersion(tryMe);
            }

            if (line.Contains("Voltage"))
            {
                link.SetVoltage(tryMe);
            }

            if (line.Contains("MaxVoltage"))
            {
                link.SetMaxVoltage(tryMe);
            }

            if (line.Contains("Gain"))
            {
                link.SetGain(tryMe);
            }

            if (line.Contains("LowerDisc") || line.Contains("DiscLow"))
            {
                link.SetLowerDisc(tryMe);
            }

            if (line.Contains("UpperDisc") || line.Contains("DiscHigh"))
            {
                link.SetUpperDisc(tryMe);
            }

            if (line.Contains("nBins"))
            {
                link.SetNBins(tryMe);
            }

            if (line.Contains("DeadTime"))
            {
                link.SetDeadTime(tryMe);
            }

            if (line.Contains("LEDMode"))
            {
                link.SetLEDMode(tryMe);
            }

            if (line.Contains("PulseLevel"))
            {
                link.SetPulseLevel(tryMe);
            }

            if (line.Contains("Name"))
            {
                link.SetName(tryMe);
            }

            if (line.Contains("QSim"))
            {
                link.SetQsim(tryMe);
            }

            if (line.Contains("Firmware Version"))
            {
                link.SetFWVersion(tryMe);
            }

            if (line.Contains("HgmMode"))
            {
                link.SetHGMMode(tryMe);
            }

            if (line.Contains("PeakMode"))
            {
                link.SetPeakMode(tryMe);
            }

            if (line.Contains("PulseSim"))
            {
                link.SetPulseSim(tryMe);
            }

            if (line.Contains("McsLength"))
            {
                link.SetMcsLen(tryMe);
            }

            if (line.Contains("McsDwell"))
            {
                link.SetMcsDwell(tryMe);
            }

            if (line.Contains("McsPasses"))
            {
                link.SetMcsPasses(tryMe);
            }

            if (line.Contains("DtResolution"))
            {
                link.SetDTRes(tryMe);
            }
        }

        internal bool GotAck()
        {
            if (gotAck)
            {
                gotAck = false;
                return true;
            } return false;
        }
    }
}
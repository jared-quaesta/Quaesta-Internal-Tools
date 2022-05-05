using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Universal_NPM_Interface.Serial
{
    class SerialNPMListener
    {
        private readonly string ACK = Encoding.UTF8.GetString(new byte[] { 0x06 });
        private readonly string NAK = Encoding.UTF8.GetString(new byte[] { 0x21 });
        bool c = false;
        bool gotNAK = false;
        bool gotCmd = false;
        bool gotACK = false;
        bool gotUpdate = false;
        string cString = "";
        string cmdString = "";
        string infoString = "";
        string serialString = "";
        string modelString = "";
        string firmwareString = "";
        string voltageString = "";
        string hgmString = "";
        string tempString = "";
        string termBuffer = "";
        string localAddress = "-";
        List<int> hgm = new List<int>();

        SerialNPMManager serialMan;

        public SerialNPMListener(SerialNPMManager serialMan)
        {
            this.serialMan = serialMan;
        }

        internal void NewData(string data, string lastCom)
        {
            //Debug.WriteLine(data);
            cmdString += data;
            if (cmdString.Contains('\n'))
            {
                gotCmd = true;
                cmdString = "";
            }
            if (lastCom.Equals("info"))
            {
                infoString += data;
            }
            if (lastCom.Equals("temperature"))
            {
                tempString += data;
            }
            if (lastCom.Equals("updatefirmware"))
            {
                cString += data;
                if (cString.Contains("CC"))
                {
                    //Debug.WriteLine(cString);
                    c = true;
                    cString = "";
                }
                if (data.Equals(NAK)) gotNAK = true;
                if (data.Equals(ACK)) gotACK = true;
            }

            if (lastCom.Equals("hgm"))
            {
                hgmString += data;
            }

            if (lastCom.Contains("voltage"))
            {
                voltageString += data;
            }


        }

        internal void RefMan(SerialNPMManager serialMan)
        {
            this.serialMan = serialMan;
        }

        internal void ClearInfo()
        {
            infoString = "";
        }

        internal string GetInfo()
        {
            return infoString;
        }

        internal List<Tuple<string, string>> ParseInfo()
        {
            List<Tuple<string, string>> ret = new List<Tuple<string, string>>();
            foreach (string line in infoString.Split('\n'))
            {
                string[] splitLine = line.Trim(' ', '\r').Split(' ');
                if (splitLine.Length < 2) continue;
                if (!double.TryParse(splitLine[splitLine.Length - 1], out double val)) continue;
                string param = "";
                for (int i = 0; i < splitLine.Length - 2; i++)
                {
                    if (splitLine[i].Equals("")) continue;
                    param += splitLine[i] + " ";
                }

                ret.Add(new Tuple<string, string>(param, val.ToString()));

            }
            infoString = "";
            return ret;
        }

        internal string GetFirmware()
        {
            return firmwareString;
        }
        internal bool GotFinishUpdatePhrase()
        {

            if (gotUpdate)
            {
                gotUpdate = false;
                return true;
            }
            else return false;
        }

        internal void ClearHGM()
        {
            hgmString = "";
            hgm.Clear();
        }

        internal bool GotC()
        {
            if (c)
            {
                c = false;
                return true;
            }
            else return false;
        }

        internal List<int> GetHGM(out int t)
        {
            t = -1;
            foreach (string line in hgmString.Split('\n'))
            {
                if (int.TryParse(line, out int val))
                {
                    hgm.Add(val);
                }
                if (line.Contains(','))
                {
                    int.TryParse(line.Split(',')[1], out t);
                }
            }
            return hgm;
        }

        internal bool GotACK()
        {
            if (gotACK)
            {
                gotACK = false;
                return true;
            }
            else return false;
        }
        internal bool GotNAK()
        {
            if (gotNAK)
            {
                gotNAK = false;
                return true;
            }
            else return false;
        }

        internal bool GotCommand()
        {
            if (gotCmd)
            {
                gotCmd = false;
                return true;
            }
            else return false;
        }

        internal void Clearinfo()
        {
            infoString = "";
        }

        internal string GetSerial()
        {
            return serialString;
        }

        internal string GetAddress()
        {
            return localAddress;
        }

        internal string GetModel()
        {
            return modelString;
        }

        internal string GetVoltage()
        {
            string ret = "UNK";
            foreach (string line in voltageString.Split('\n'))
            {
                if (line.Contains("Measured/Set:")) ret = line.Trim('\r', '\n', ' ');
            }
            return ret;
        }

        internal void ClearVoltage()
        {
            voltageString = "";
        }

        internal void ClearTemperature()
        {
            tempString = "";
        }

        internal double GetTemperature()
        {
            double ret = -1;
            foreach (string line in tempString.Split('\n'))
            {
                if (double.TryParse(line.Split(',')[0], out double temp)) return temp;
            }
            return ret;
        }
    }
}

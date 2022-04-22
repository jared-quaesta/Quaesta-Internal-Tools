﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace QIXLPTesting.SerialTools
{
    class SerialListener
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

        List<int> hgm = new List<int>();

        SerialNPMManager serialMan = null;
        internal void NewData(string data, string lastCom)
        {
            if (serialMan != null) serialMan.NewData(data);
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

        internal void ParseInfo()
        {
            foreach (string line in infoString.Split('\n'))
            {
                if (line.Contains("Firmware Version"))
                {
                    firmwareString = line.Trim().Split(' ')[line.Trim().Split(' ').Length - 1].Trim('\r', ' ');
                }
                else if (line.Contains("Serial Number"))
                {
                    serialString = line.Trim().Split(' ')[line.Trim().Split(' ').Length - 1].Trim('\r', ' ');
                    Debug.Write("");
                }
                else if (line.Contains("Model") && !line.Contains("Type"))
                {
                    modelString = line.Trim().Split(' ')[line.Trim().Split(' ').Length - 1].Trim('\r', ' ');
                }
            }

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

        internal List<int> GetHGM()
        {
            foreach (string line in hgmString.Split('\n'))
            {
                if (int.TryParse(line, out int val))
                {
                    hgm.Add(val);
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
    }
}

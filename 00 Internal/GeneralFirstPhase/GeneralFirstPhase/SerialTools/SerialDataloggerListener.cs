using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralFirstPhase.SerialTools
{
    class SerialDataloggerListener
    {
        string incomplete = "";
        int relays = 0;
        int maxCycleTemp = -1;
        int minCycleTemp = -1;
        bool flgColdCycle = false;
        bool flgHotCycle = false;
        bool flgThermalCyclingEnabled = false;
        bool flgUseCS215 = false;
        bool cs215Works = false;
        bool gotCycle = false;
        bool gotINI = false;
        bool gotCS215 = false;
        string cs215String = "";

        internal void NewData(string data, string lastCom)
        {
            if (!data.EndsWith("\n"))
            {
                incomplete += data;
                return;
            } else
            {
                data = incomplete + data;
                incomplete = "";
            }
            if (data.Contains("Main INI")) gotINI = true;
            Debug.WriteLine(data);
            if (lastCom.ToLower().Equals("showcycle"))
            {
                ParseCycle(data);
            }
            if (lastCom.Equals("showcs215"))
            {
                cs215String += data;
                if (data.Contains("CS215:")) gotCS215 = true;
            }
            
        }

        internal bool GotCycle()
        {
            if (gotCycle)
            {
                gotCycle = false;
                return true;
            }
            else return false;
        }


        private void ParseCycle(string data)
        {
            foreach (string line in data.Split('\n'))
            {
                string[] splitLine = line.Split('=');
                if (splitLine.Length < 2) continue;
                if (!int.TryParse(splitLine[1], out int val)) continue;
                if (line.Contains("flgHotCycle"))
                {
                    flgHotCycle = val == 1;
                }
                else if (line.Contains("flgColdCycle"))
                {
                    flgColdCycle = val == 1;
                }
                else if (line.Contains("MaxCycleTemp"))
                {
                    maxCycleTemp = val;
                }
                else if (line.Contains("MinCycleTemp"))
                {
                    minCycleTemp = val;
                }
                else if (line.Contains("flgThermalCyclingEnabled"))
                {
                    flgThermalCyclingEnabled = val == 1;
                }
                else if (line.Contains("flgUseCS215"))
                {
                    flgUseCS215 = val == 1;
                }
                else if (line.Contains("Relays"))
                {
                    relays = val;
                }
            }
            gotCycle = true;
        }

        internal Tuple<bool, bool, bool, bool, int, int, int> GetCycle()
        {
            return new Tuple<bool, bool, bool, bool, int, int, int>(
                flgThermalCyclingEnabled,
                flgHotCycle,
                flgColdCycle,
                flgUseCS215,
                maxCycleTemp,
                minCycleTemp,
                relays
            );
        }

        internal bool GotINI()
        {
            if (gotINI)
            {
                gotINI = false;
                return true;
            }
            else return false;
        }

        internal string GetCS215()
        {
            foreach (string line in cs215String.Split('\n'))
            {
                if (line.Contains("CS215:"))
                {
                    cs215String = "";
                    return line.Trim('\r', ' ').Split(':')[1];
                }
            }
            return "UNK";
        }

        internal bool GotCS215()
        {
            if (gotCS215)
            {
                gotCS215 = false;
                return true;
            }
            else return false;
        }
    }
}

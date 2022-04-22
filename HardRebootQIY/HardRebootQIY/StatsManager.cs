using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardRebootQIY
{
    class StatsManager
    {
        private Form1 form1;
        private Dictionary<string, string> byLine = new Dictionary<string, string>();
        internal List<string> errs = new List<string>();
        internal bool gotInfo = false;
        internal string lastStr = "";

        public StatsManager(Form1 form1)
        {
            this.form1 = form1;
        }

        internal void IncomingData(string strDat, string lastCommand)
        {
            lastStr = strDat;
            Debug.WriteLine(strDat);
            foreach (string line in strDat.Split('\n'))
            {
                if (line.Contains("Current Time") || line.Contains("Model")) continue;
                string key = line.Split(' ')[0];

                if (byLine.ContainsKey(key))
                {
                    if (!byLine[key].Equals(line))
                    {
                        Debug.WriteLine($"EXPECTED: {byLine[key]} \nGOT: {line}");
                        errs.Insert(0, strDat);
                    }
                } else
                {
                    byLine.Add(key, line);
                }

                if (line.Contains("TcpPort"))
                { 
                    gotInfo = true;
                    Debug.WriteLine(line);
                }
                
            }
        }
    }
}

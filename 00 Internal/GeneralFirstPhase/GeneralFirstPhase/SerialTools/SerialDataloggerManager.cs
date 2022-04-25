using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace QIXLPTesting.SerialTools
{
    class SerialDataloggerManager
    {
        public static List<string> GetComs()
        {
            List<string> ret = new List<string>();
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString() + p["DeviceID"].ToString());

                foreach (var i in ports)
                {
                    if (i.Contains("USB Serial Port"))
                    {
                        int start = i.IndexOf('(') + 1;
                        string parse = i.Substring(start);
                        int end = parse.IndexOf(')');
                        string com = parse.Substring(0, end);

                        string sn = i.Split('\\')[i.Split('\\').Length - 1];

                        ret.Add(com);
                    }
                }
            }
            return ret;
        }
    }
}

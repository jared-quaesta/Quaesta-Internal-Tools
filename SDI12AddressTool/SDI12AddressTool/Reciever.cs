using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDI12AddressTool
{
    class Reciever
    {
        private MainForm mainForm;
        internal string curAddress = "";
        internal string lastCommand = "";

        internal ArrayList curTestAddresses = new ArrayList();
        internal string curSn = "";
        internal string curFw = "";

        public Reciever(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        internal void AddData(string latest)
        {
            Debug.WriteLine(latest);

            if (latest.Contains("!"))
            {
                lastCommand = latest;
            }
            if (latest.Contains("SDI12 Response:") && lastCommand.Contains("?!"))
            {
                curAddress = latest.Split(':')[1].Trim();
            }
            if (latest.Contains("SDI12 Response:") && lastCommand.Contains("I!"))
            {
                // SDI12 Response: A13QuaestaI 3100 QIXSURIBLP100 NU21-0053
                for (int i = 0; i < latest.Trim().Split(' ').Length; i++)
                {
                    Debug.WriteLine(i + ": " + latest.Trim().Split(' ')[i]);
                }
                curSn = latest.Trim().Split(' ')[latest.Split(' ').Length - 1].Trim();

                int j = 2;
                curFw = latest.Trim().Split(' ')[latest.Split(' ').Length - j].Trim();
                while (curFw.Length == 0)
                {
                    j++;
                    curFw = latest.Trim().Split(' ')[latest.Split(' ').Length - j].Trim();
                }
            }
        }

        internal void AddTestAddress(string address)
        {
            curTestAddresses.Add(address);
        }

        internal void Reset()
        {
            curTestAddresses.Clear();
            curAddress = "";
            lastCommand = "";
            curSn = "";
        }
    }
}

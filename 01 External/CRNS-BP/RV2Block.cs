using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRNS_BP
{
    public class RV2Block
    {
        public Dictionary<string, object> data;
        public DateTime dt;
        public Dictionary<int, string> numberToSerial;
        public Dictionary<string, double> serialToCounts = new Dictionary<string, double>();
        public int sumCounts;
        public int sumTime;
        public int recordPeriod;
        public double[] position = null;
        public int recordNumber = -1;

        [JsonConstructor]
        public RV2Block(DateTime dt, Dictionary<string, object> data, Dictionary<int, string> numberToSerial, int recordPeriod=15)
        {
            this.dt = dt;
            this.data = data;
            this.numberToSerial = numberToSerial;
            this.recordPeriod = recordPeriod;
            ExtractCounts();
            ExtractPosition();
            ExtractRecordNumber();
        }

        private void ExtractRecordNumber()
        {
            if (int.TryParse(data["//RecordNum"].ToString(), out int val))
                recordNumber = val;
        }

        private void ExtractPosition()
        {
            if (data.ContainsKey("LatDD") && data.ContainsKey("LongDD"))
            {
                try
                {
                    position = new double[] { (double)data["LatDD"], (double)data["LongDD"] };
                }
                catch { position = null; }
            }
            else position = null;
        }

        public RV2Block(RV2Block old)
        {
            data = old.data;
            dt = old.dt;
            numberToSerial = old.numberToSerial;
            serialToCounts = old.serialToCounts;
            sumCounts = old.sumCounts;
            sumTime = old.sumTime;
            recordPeriod = old.recordPeriod;
            position = old.position;
            recordNumber = old.recordNumber;
        }

        private void ExtractCounts()
        {
            foreach (string item in data.Keys)
            {
                if (item.Contains("N") && item.Contains("C"))
                {
                    string serial = numberToSerial[Int32.Parse(item.Substring(1, 2))];
                    serialToCounts.Add(serial, (double)data[item]);
                    sumCounts +=(int)(double)data[item];
                }
            }
            // convert to cph LATER. Only store sum counts in this block.

        }

        internal double[] GetPosition()
        {
            return position;
        }
    }
}

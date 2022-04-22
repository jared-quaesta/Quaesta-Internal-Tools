using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRNS_BP
{
    class CRNSParser
    {

        private DateTime maxDate = DateTime.MinValue;
        private DateTime minDate = DateTime.MaxValue;

        // {DL Assigned number: Serial #}
        private Dictionary<int, string> numberToSerial = new Dictionary<int, string>();
        public List<RV2Block> RV2Blocks = new List<RV2Block>();

        private string metadata = "";
        private List<string> header = new List<string>();
        private int recordPeriod = -1;
        internal CRNSParser()
        {

        }

        /// <summary>
        /// Clear all stored data and reset parser
        /// </summary>
        internal void ClearData()
        {
            RV2Blocks.Clear();
            header.Clear();

            maxDate = DateTime.MinValue;
            minDate = DateTime.MaxValue;
            metadata = "";
            recordPeriod = -1;
        }

        internal string GetJson(int omit = -1)
        {
            for (int i = 0; i < RV2Blocks.Count; i++)
            {
                if (RV2Blocks[i].recordNumber == omit)
                {
                    RV2Blocks.RemoveAt(i);
                    break;
                }
            }
            string ret = JsonConvert.SerializeObject(RV2Blocks, Formatting.Indented);
            return ret;
        }

        internal string GetMetadata()
        {
            return metadata;
        }

        /// <summary>
        /// Decides the type of info the line contains and sends it to the correct parser
        /// </summary>
        /// <param name="line">line of data either read in from file or gathered from DL</param>
        internal void NewRV2Data(string line)
        {
            if (line.Contains("//")) metadata += line + "\r\n";
            if (line.Contains("//Recordperiod"))
            {
                string splitEquals = line.Split('=')[1];
                string num = splitEquals.Trim().Split(' ')[0];
                int mult = 1;
                if (line.ToLower().Contains("minutes")) mult = 60;
                if (int.TryParse(num, out int rec))
                {
                    recordPeriod = rec;
                }
                recordPeriod *= mult;
            }
            if (line.Trim(' ', '\n', '\r').Length == 0) return;
            if (line.Contains("//RecordNum")) UpdateHeader(line);
            else if (line.Contains("//NPM#") || line.Contains("//Det#")) ParseNpm(line);
            else if (Int32.TryParse(line.Split(',')[0], out int i)) ParseData(line); // First is record number always, this is always an int
        }

        /// <summary>
        /// Decides the type of info the line contains and sends it to the correct parser
        /// </summary>
        /// <param name="line">line of data either read in from file or gathered from DL</param>
        //internal void NewHGMData(string line)
        //{
        //    string[] splitLine = line.Split(',');
        //    int bins = 32;

        //    // 64 is arbatrary, this is just to detect whether there are 64 bins or 32
        //    if (splitLine.Length > 64)
        //    {
        //        bins = 64;
        //    }

        //    // last entry is sn
        //    string serial = splitLine[splitLine.Length - 1];
        //    DateTime date = DateTime.ParseExact(splitLine[0], "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
        //    int[] hgm = new int[bins];
        //    int j = 0;
        //    for (int i = splitLine.Length - bins - 1; i < splitLine.Length - 1; i++)
        //    {
        //        hgm[j] = Int32.Parse(splitLine[i]);
        //    }
        //    if (date >= minDate && date <= maxDate || minDate == DateTime.MaxValue)
        //    {
        //        HGMBlocks.Add(new HGMBlock(serial, date, hgm));
        //    }
        //    else
        //    {
        //        Debug.WriteLine($"Not in Date Range");
        //    }

        //}

        /// <summary>
        /// capture available NPMs
        /// </summary>
        /// <param name="line">line of data either read in from file or gathered from DL</param>
        private void ParseNpm(string line)
        {
            // //NPM#01: SerialNum=20120808

            int num = Int32.Parse(line.Split(':')[0].Split('#')[1]);
            string sn = line.Split(',')[0].Split('=')[1];
            // not valid NPM
            if (num == 0) return;
            if (numberToSerial.ContainsKey(num)) numberToSerial[num] = sn;
            else numberToSerial.Add(num, sn);

        }

        /// <summary>
        /// Using the header (if header is empty, skip) extract all data from this line and create a new block 
        /// </summary>
        /// <param name="line">line of data either read in from file or gathered from DL</param>
        private void ParseData(string line)
        {
            // 12158, 2021/11/07 15:39:27,00121600,932.8,938.5, 24.9, 22.7, 12.69,-99.0,109.0,5,7,10,10,12154,12159,0153957,32.2128133,-110.9469066,1,08,001,0753,000,279,A,071121

            if (header.Count == 0) return;

            string[] splitLine = line.Split(',');

            DateTime dt = DateTime.MinValue;
            Dictionary<string, object> data = new Dictionary<string, object>();
            for (int i = 0; i < header.Count; i++)
            {
                if (header[i].Equals("Date Time(UTC)"))
                {
                    string dateTime = splitLine[i].Trim();
                    dt = DateTime.ParseExact(dateTime, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);

                    // keep track of min max date
                    if (dt > maxDate)
                        maxDate = dt;
                    if (dt < minDate)
                        minDate = dt;
                }
                if (header[i].Contains("N") && header[i].Contains("C"))
                {
                    if (splitLine[i].ToLower().Contains("nan") || splitLine[i].Contains("-1"))
                    {
                        splitLine[i] = "0";
                    }
                }

                if (double.TryParse(splitLine[i], out double j))
                {
                    data.Add(header[i], j);
                }
                else
                {
                    data.Add(header[i], splitLine[i]);
                }

            }
            if (recordPeriod != -1)
            {
                RV2Blocks.Add(new RV2Block(dt, data, new Dictionary<int, string>(numberToSerial), recordPeriod));
            } 
            else
                RV2Blocks.Add(new RV2Block(dt, data, new Dictionary<int, string>(numberToSerial)));
            

        }

        /// <summary>
        /// Updates header string and appropriate indexes of info
        /// </summary>
        /// <param name="line">line of data either read in from file or gathered from DL</param>
        private void UpdateHeader(string line)
        {
            header.Clear();
            foreach (string item in line.Split(','))
            {
                header.Add(item);
            }

        }

    }
}

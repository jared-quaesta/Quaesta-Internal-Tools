using GeneralFirstPhase.Data;
using GeneralFirstPhase.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralFirstPhase.Charting
{
    public partial class HeatTestPlotView : Form
    {
        string serial;
        List<HeaterDataResults> allData;

        Dictionary<DateTime ,List<HeaterDataResults>> splitData = new Dictionary<DateTime, List<HeaterDataResults>>();

        public HeatTestPlotView(string serial)
        {
            this.serial = serial;
            InitializeComponent();
        }

        private void HeatTestPlotView_Load(object sender, EventArgs e)
        {
            serialLbl.Text = serial;
        }

        internal bool HasData()
        {
            allData = SQLManager.GetHeatData(serial);
            if (allData.Count == 0) return false;
            return true;
        }

        internal void ShowPlots()
        {
            // split data into sets by 6h delim
            // first datapoint is recorded
            HeaterDataResults last = allData[0];
            DateTime curTime = last.Time;
            splitData.Add(last.Time, new List<HeaterDataResults>() {last});
            dateBox.Items.Add(curTime.ToString("yyyy-MM-dd HH:mm:ss"));
            foreach (HeaterDataResults data in allData)
            {
                if (last == data) continue;
                if (data.Time - last.Time < TimeSpan.FromHours(6))
                {
                    splitData[curTime].Add(data);
                }
                else
                {
                    curTime = data.Time;
                    dateBox.Items.Add(curTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    splitData.Add(last.Time, new List<HeaterDataResults>() { data });
                }
                last = data;

            }

            dateBox.SelectedIndex = 0;

            DisplayData();

            Show();


        }

        private void DisplayData()
        {
            string dateStr = dateBox.Text;
            DateTime key = DateTime.ParseExact(dateStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            foreach (HeaterDataResults res in splitData[key])
            {
                ConcurrentDictionary<string, HeaterDataResults> dict = new ConcurrentDictionary<string, HeaterDataResults>();
                dict.TryAdd(res.Serial, res);
                heatPlots.UpdateCharts(dict, 500);
            }

        }
    }
}

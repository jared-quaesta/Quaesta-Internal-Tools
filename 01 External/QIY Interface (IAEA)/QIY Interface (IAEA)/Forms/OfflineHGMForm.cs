using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace QIY_Interface__IAEA_
{
    public partial class OfflineHGMForm : Form
    {
        PlotModel hgmModel = new PlotModel() { Title = "Histogram (Pulse Height Spectrum)" };
        LineSeries hgmSeries = new LineSeries() { Title = "Histogram" };

        List<DatSegment> hgms = new List<DatSegment>();
        int curHGMIndex = 0;

        public OfflineHGMForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["HGMPATH"].Length > 0)
            {
                openFileDialog.InitialDirectory = ConfigurationManager.AppSettings["HGMPATH"];
            }
            openFileDialog.ShowDialog();
            if (!File.Exists(openFileDialog.FileName)) return;
            string fileBINArr = File.ReadAllText(openFileDialog.FileName);
            Reset();
            ProcessHGMFile(fileBINArr);
            fileNameBox.Text = openFileDialog.FileName;
        }

        private void ProcessHGMFile(string fileHGMString)
        {
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            int i = 0;
            int nrecords = 0;
            int crcErrs = 0;
            string[] splitString = fileHGMString.Split('\n');
            hgms.Add(new DatSegment(DateTime.MinValue));
            foreach (string line in splitString)
            {
                string[] splitByComma = line.Trim('\r', '\n', ' ').Split(',');
                DateTime resDate = DateTime.MinValue;
                if (DateTime.TryParseExact(splitByComma[0], "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out resDate))
                {
                    if (startDate == DateTime.MinValue) startDate = resDate;
                    endDate = resDate;
                    
                    if (hgms.Count == 0)
                        hgms.Add(new DatSegment(resDate));
                    else
                        hgms.Add(new DatSegment(resDate, hgms[hgms.Count-1]));
                    nrecords++;
                }
                if (hgms.Count == 0) continue;
                DatSegment curHGM = hgms[hgms.Count-1];
                if (int.TryParse(splitByComma[0], out int bindex) && int.TryParse(splitByComma[1], out int count))
                    curHGM.Hgm.Add(new Tuple<int, int>(bindex, count));
                else if (line.Contains("Elapsed Time"))
                    curHGM.ElapsedSecs = line.Split('=')[1];
                ParseInfo(line);
            }
            hgms.RemoveAt(0);
            totalHGMBox.Text = hgms.Count.ToString();
            curHGMIndex = 0;
            startTimeBox.Text = startDate.ToString("G");
            stopTimeBox.Text = endDate.ToString("G");
            //elapsedTimeBox.Text = $"{(int)((endDate - startDate).TotalHours)}";
            
            ViewHGMS();

            //numRecordsBox.Text = nrecords.ToString();

            //DisplaySegData();
        }

        private void ParseInfo(string line)
        {
            DatSegment curHGM = hgms[hgms.Count - 1];
            string[] splitBySpace = line.Trim('\n', '\r', ' ').Split(' ');
            string param = splitBySpace[0].ToLower();
            string val = splitBySpace[splitBySpace.Length-1];
            if (param.Equals("name"))
            {
                curHGM.Name = val;
            }
            else if (param.Equals("serial"))
            {
                curHGM.Sn = val;
            }
            else if (line.Contains("Model Version"))
            {
                curHGM.Modelv = val;
            }
            else if (param.Equals("lowerdisc"))
            {
                curHGM.Ld = val;
            }
            else if (param.Equals("upperdisc"))
            {
                curHGM.Ud = val;
            }
            else if (param.Equals("voltage"))
            {
                curHGM.HighVoltage = val;
            }
            else if (param.Equals("gain"))
            {
                curHGM.Gain = val;
            }
            else if (param.Equals("nbins"))
            {
                curHGM.Nbins = val;
            }
            else if (param.Equals("recordperiod(sec)"))
            {
                curHGM.RecordPeriod = val;
            }
            else if (param.Equals("recordsperhgm"))
            {
                curHGM.RecsPerHGM = val;
            }

        }

        private void ViewHGMS()
        {
            if (hgms.Count == 0)
            {
                MessageBox.Show("No data");
                return;
            } else if (hgms.Count == 1)
            {
                backSegBtn.Enabled = false;
                advSegBtn.Enabled = false;
                trackbar.Enabled = false;
            }
            else
            {
                backSegBtn.Enabled = true;
                advSegBtn.Enabled = true;
                trackbar.Minimum = 0;
                trackbar.Maximum = hgms.Count-1;
                trackbar.Enabled = true;
                trackbar.Value = curHGMIndex;
            }
            hgmSeries.Points.Clear();
            curHGMBox.Text = (curHGMIndex + 1).ToString();
            DatSegment curhgm = hgms[curHGMIndex];
            elapsedTimeBox.Text = curhgm.ElapsedSecs;
            foreach (Tuple<int, int> hgmData in curhgm.Hgm)
            {
                hgmSeries.Points.Add(new DataPoint(hgmData.Item1, hgmData.Item2));
            }
            hgmModel.InvalidatePlot(true);

            if (curhgm.Name != null)
                nameBox.Text = curhgm.Name;
            if (curhgm.Sn != null)
                snBox.Text = curhgm.Sn;
            if (curhgm.Modelv != null)
                modelBox.Text = curhgm.Modelv;
            if (curhgm.RecordPeriod != null)
                rpbox.Text = curhgm.RecordPeriod;
            if (curhgm.RecsPerHGM != null)
                rphgmBox.Text = curhgm.RecsPerHGM;
            if (curhgm.Ld != null)
                discLowBox.Text = curhgm.Ld;
            if (curhgm.Ud != null)
                discHighBox.Text = curhgm.Ud;
            if (curhgm.HighVoltage != null)
                hvBox.Text = curhgm.HighVoltage;
            if (curhgm.Gain != null)
                gainBox.Text = curhgm.Gain;
            if (curhgm.Nbins != null)
                nbinsBox.Text = curhgm.Nbins;
            if (curhgm.HgmTime != DateTime.MinValue)
                hgmDate.Text = curhgm.HgmTime.ToString("G");

            Refresh();

            if (int.TryParse(rphgmBox.Text, out int recsPer) && int.TryParse(rpbox.Text, out int rp))
            {
                TimeSpan ts = TimeSpan.FromSeconds(rp * recsPer);
                hrsIntBox.Text = $"{ts.TotalHours:0.01}";
                secIntBox.Text = $"{ts.TotalSeconds}";
            }
            
            Refresh();
        }

        private void Reset()
        {
            hgms.Clear();
            hgmSeries.Points.Clear();
            foreach (TextBox tb in Controls.OfType<TextBox>())
            {
                tb.Clear();
            }
            foreach (TextBox tb in infoGB.Controls.OfType<TextBox>())
            {
                tb.Clear();
            }
        }

        private void HideForm(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void startTimeBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void OfflineHGMForm_Load(object sender, EventArgs e)
        {
            hgmModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Title = "Bin"
            });
            hgmModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Title = "Counts"
            });
            hgmModel.Series.Add(hgmSeries);
            plotView.Model = hgmModel;

        }

        private void advSegBtn_Click(object sender, EventArgs e)
        {
            if (hgms.Count == 0) return;
            curHGMIndex++;
            if (curHGMIndex > hgms.Count - 1) curHGMIndex = 0;
            ViewHGMS();
        }

        private void backSegBtn_Click(object sender, EventArgs e)
        {
            if (hgms.Count == 0) return;
            curHGMIndex--;
            if (curHGMIndex < 0) curHGMIndex = hgms.Count - 1;
            ViewHGMS();
        }

        private void trackbar_Scroll(object sender, EventArgs e)
        {
            curHGMIndex = trackbar.Value;
            ViewHGMS();
        }

        private void ChooseIndex(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(curHGMBox.Text, out int index))
                {
                    if (index > 0 && index <= hgms.Count)
                    {
                        curHGMIndex = index - 1;
                    }
                }
                ViewHGMS();
            }
            
        }
    }
}

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace QIY_Interface__IAEA_
{
    public partial class OfflinePlotForm : Form
    {
        // counts series
        PlotModel countsModelRP = new PlotModel() { Title = "Counts"};
        LineSeries countsSeriesRP = new LineSeries() { Title = "Counts", Color = OxyColors.Green };
        PlotModel countsModelDT = new PlotModel() { Title = "Counts" };
        LineSeries countsSeriesDT = new LineSeries() { Title = "Counts", Color = OxyColors.Green };

        // pscounts series
        LineSeries pCountsSeriesRP = new LineSeries() { Title = "Ext. Pulse Counts", Color = OxyColors.Blue};
        LineSeries pCountsSeriesDT = new LineSeries() { Title = "Ext. Pulse Counts", Color = OxyColors.Blue };


        // temp
        PlotModel tempModelRP = new PlotModel() { Title = "Temperature" };
        LineSeries tempSeriesRP = new LineSeries() { Title = "Temperature (C)", Color = OxyColors.Green };
        PlotModel tempModelDT = new PlotModel() { Title = "Temperature" };
        LineSeries tempSeriesDT = new LineSeries() { Title = "Temperature (C)", Color = OxyColors.Green };

        // rh
        PlotModel rhModelRP = new PlotModel() { Title = "Relative Humidity" };
        LineSeries rhSeriesRP = new LineSeries() { Title = "Relative Humidity (%)", Color = OxyColors.Green };
        PlotModel rhModelDT = new PlotModel() { Title = "Relative Humidity" };
        LineSeries rhSeriesDT = new LineSeries() { Title = "Relative Humidity (%)", Color = OxyColors.Green };

        // volt
        PlotModel voltModelRP = new PlotModel() { Title = "Supply Voltage" };
        LineSeries voltSeriesRP = new LineSeries() { Title = "Supply Voltage", Color = OxyColors.Green };
        PlotModel voltModelDT = new PlotModel() { Title = "Supply Voltage" };
        LineSeries voltSeriesDT = new LineSeries() { Title = "Supply Voltage", Color = OxyColors.Green };

        // signal
        PlotModel signalModelRP = new PlotModel() { Title = "Signal Input" };
        LineSeries signalSeriesRP = new LineSeries() { Title = "Signal Input", Color = OxyColors.Green };
        PlotModel signalModelDT = new PlotModel() { Title = "Signal Input" };
        LineSeries signalSeriesDT = new LineSeries() { Title = "Signal Input", Color = OxyColors.Green };

        List<DatSegment> segments = new List<DatSegment>();

        LinearAxis countsModelAxisRP = new LinearAxis()
        {
            Title = "Counts",
            Position = AxisPosition.Left,
            Minimum = 0,
            Maximum = 10,
            MajorStep = 5
        };
        LinearAxis countsModelAxisDT = new LinearAxis()
        {
            Title = "Counts",
            Position = AxisPosition.Left,
            Minimum = 0,
            Maximum = 10,
            MajorStep = 5
        };


        int curSegIndex = 0;

        public OfflinePlotForm()
        {
            InitializeComponent();
        }


        private void openBINFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["BINPATH"].Length > 0)
            {
                openFileDialog.InitialDirectory = ConfigurationManager.AppSettings["BIN"];
            }
            openFileDialog.ShowDialog();
            if (!File.Exists(openFileDialog.FileName)) return;
            if (!openFileDialog.FileName.ToLower().Contains(".bin"))
            {
                MessageBox.Show($"Wrong Filetype:\n'{openFileDialog.FileName}'");
                return;
            }
            byte[] fileBINArr = File.ReadAllBytes(openFileDialog.FileName);
            Reset();
            ProcessBINArr(fileBINArr);
            fileNameBox.Text = openFileDialog.FileName;
        }

        private void Reset()
        {
            countsSeriesDT.Points.Clear();
            countsSeriesRP.Points.Clear();
            tempSeriesDT.Points.Clear();
            tempSeriesRP.Points.Clear();
            rhSeriesDT.Points.Clear();
            rhSeriesRP.Points.Clear();
            voltSeriesDT.Points.Clear();
            voltSeriesRP.Points.Clear();
            signalSeriesDT.Points.Clear();
            signalSeriesRP.Points.Clear();

            segments.Clear();

            foreach (TextBox tb in segGB.Controls.OfType<TextBox>())
            {
                tb.Clear();
            }
            allSegsBox.Clear();
            curSegBox.Clear();
            numRecordsBox.Clear();
            crcErrBox.Clear();

        }

        private void ProcessDATArr(string fileDATString)
        {
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            int i = 0;
            int nrecords = 0;
            int crcErrs = 0;
            string[] splitString = fileDATString.Split('\n');

            foreach (string line in splitString)
            {
                if (line.Contains("www.QuaestaInstruments.com"))
                {
                    if (segments.Count == 0)
                    {
                        segments.Add(new DatSegment());
                    }
                    else if (segments[segments.Count-1].NumRecords > 0)
                        segments.Add(new DatSegment());
                }
                if (segments.Count == 0) continue;
                if (!ParseHeader(line))
                {
                    DateTime dt = ParseData(line);
                    if (dt == DateTime.MinValue) continue;
                    nrecords++;
                    if (startDate == DateTime.MinValue) startDate = dt;
                    endDate = dt;
                }
            }

            allSegsBox.Text = segments.Count.ToString();
            curSegIndex = 0;

            startTimeBox.Text = startDate.ToString("G");
            stopTimeBox.Text = endDate.ToString("G");
            elapsedTimeBox.Text = $"{(int)((endDate - startDate).TotalHours)}";
            numRecordsBox.Text = nrecords.ToString();

            DisplaySegData();
        }

        private void DisplaySegData()
        {
            foreach (TextBox tb in segGB.Controls.OfType<TextBox>())
            {
                tb.Clear();
            }
            DatSegment curSegment = segments[curSegIndex];
            curSegBox.Text = (curSegIndex + 1).ToString();
            if (curSegment.Name != null)
            {
                segNameBox.Text = curSegment.Name;
            }
            if (curSegment.Sn != null)
            {
                segSnBox.Text = curSegment.Sn;
            }
            if (curSegment.RecordPeriod != null)
            {
                segRpBox.Text = curSegment.RecordPeriod;
            }
            if (curSegment.RecsPerHGM != null)
            {
                segRecPerHGMBox.Text = curSegment.RecsPerHGM;
            }
            if (curSegment.NewFilePeriod != null)
            {
                segNfBox.Text = curSegment.NewFilePeriod;
            }
            if (curSegment.HighVoltage != null)
            {
                segHvBox.Text = curSegment.HighVoltage;
            }
            if (curSegment.HighVoltage != null)
            {
                segHvBox.Text = curSegment.HighVoltage;
            }
            if (curSegment.Gain != null)
            {
                segGainBox.Text = curSegment.Gain;
            }
            if (curSegment.Ld != null)
            {
                segDiscLowBox.Text = curSegment.Ld;
            }
            if (curSegment.Ud != null)
            {
                segDiscHighBox.Text = curSegment.Ud;
            }
            if (curSegment.LogHGM != null)
            {
                segLogHgmsBox.Text = curSegment.LogHGM;
            }
            if (curSegment.PulseThresh != null)
            {
                segPulseThresh.Text = curSegment.PulseThresh;
            }
            if (curSegment.LogExtPulses != null)
            {
                segLogExtBox.Text = curSegment.LogExtPulses;
            }
            if (curSegment.Logtdegc != null)
            {
                segLogTBox.Text = curSegment.Logtdegc;
            }
            if (curSegment.LogRh != null)
            {
                segLogRhBox.Text = curSegment.LogRh;
            }
            if (curSegment.LogBatt != null)
            {
                segLogBattBox.Text = curSegment.LogBatt;
            }
            if (curSegment.LogSig != null)
            {
                segLogSigBox.Text = curSegment.LogSig;
            }
            if (curSegment.StartTime != DateTime.MinValue)
            {
                segStartBox.Text = curSegment.StartTime.ToString("G");
            }
            if (curSegment.EndTime != DateTime.MinValue)
            {
                segStopBox.Text = curSegment.EndTime.ToString("G");
            }

        }

        private DateTime ParseData(string line)
        {
            string[] splitLine = line.Split(',');
            if (segments[segments.Count - 1].Header == null) return DateTime.MinValue;

            // find header indexes
            int recIndex = 1;
            int countsIndex = 2;
            int pCountsIndex = -1;
            int degIndex = -1;
            int rhIndex = -1;
            int batIndex = -1;
            int sigIndex = -1;


            //YYYY / MO / DD HH: MM: SS, Index,  NPM_Cnts,Pulse_Cnts, DegC,RHum, Batt,Sig
            string[] headerSplit = segments[segments.Count - 1].Header.ToLower().Split(',');
            for (int i = 3; i < headerSplit.Length; i++) 
            {
                if (headerSplit[i].Trim().ToLower().Equals("pulse_cnts"))
                {
                    pCountsIndex = i;
                }
                else if (headerSplit[i].Trim().ToLower().Equals("degc"))
                {
                    degIndex = i;
                }
                else if (headerSplit[i].Trim().ToLower().Equals("rhum"))
                {
                    rhIndex = i;
                }
                else if (headerSplit[i].Trim().ToLower().Equals("batt"))
                {
                    batIndex = i;
                }
                else if (headerSplit[i].Trim().ToLower().Equals("sig"))
                {
                    sigIndex = i;
                }
            }

            DateTime resDate = new DateTime();
            if (DateTime.TryParseExact(splitLine[0], "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out resDate))
            {
                if (uint.TryParse(splitLine[recIndex], out uint recordIndex) &&
                    uint.TryParse(splitLine[countsIndex], out uint counts))
                {
                    AppendCountsPlot(resDate, recordIndex, counts);

                    if (pCountsIndex != -1)
                    {
                        if (splitLine.Length - 2 >= pCountsIndex)
                        {
                            if (uint.TryParse(splitLine[pCountsIndex], out uint pcounts))
                                AppendPCountsPlot(resDate, recordIndex, pcounts);
                        }
                    }
                    if (degIndex != -1)
                    {
                        if (splitLine.Length - 2 >= degIndex)
                        {
                            if (double.TryParse(splitLine[degIndex], out double temp))
                                AppendTempPlot(resDate, recordIndex, temp);
                        }
                    }
                    if (rhIndex != -1)
                    {
                        if (splitLine.Length - 2 >= rhIndex)
                        {
                            if (double.TryParse(splitLine[rhIndex], out double rh))
                                AppendRHPlot(resDate, recordIndex, rh);
                        }
                    }
                    if (batIndex != -1)
                    {
                        if (splitLine.Length - 2 >= batIndex)
                        {
                            if (double.TryParse(splitLine[batIndex], out double bat))
                                AppendVoltPlot(resDate, recordIndex, bat);
                        }
                    }
                    if (sigIndex != -1)
                    {
                        if (splitLine.Length - 2 >= sigIndex)
                        {
                            if (int.TryParse(splitLine[sigIndex], out int sig))
                                AppendSignalPlot(resDate, recordIndex, sig);
                        }
                    }

                    if (segments[segments.Count - 1].StartTime == DateTime.MinValue) segments[segments.Count - 1].StartTime = resDate;
                    segments[segments.Count - 1].EndTime = resDate;
                    segments[segments.Count - 1].NumRecords++;
                    return resDate;
                }

                return DateTime.MinValue;
            }
            else return DateTime.MinValue;
        }

        private void AppendPCountsPlot(DateTime resDate, uint recordIndex, uint pcounts)
        {
            pCountsSeriesDT.Points.Add(new DataPoint(DateTimeAxis.ToDouble(resDate), pcounts));
            pCountsSeriesRP.Points.Add(new DataPoint(recordIndex, pcounts));

            AdjustYAxis(pcounts);

            countsPlot.InvalidatePlot(true);
        }

        private bool ParseHeader(string line)
        {
            string[] brokenBySpace = line.Split(' ');

            string val = brokenBySpace[brokenBySpace.Length-1].Trim(' ', '\r', '\n');
            string param = brokenBySpace[0].ToLower().Trim(' ', '\r', '\n');

            DatSegment curSeg = segments[segments.Count - 1];

            if (param.Equals("voltage"))
            {
                curSeg.HighVoltage = val;
                return true;
            }
            else if (param.Equals("name"))
            {
                curSeg.Name = val;
                return true;
            }
            else if (param.Equals("serial"))
            {
                curSeg.Sn = val;
                return true;
            }
            else if (param.Equals("recordperiod(sec)"))
            {
                curSeg.RecordPeriod = val;
                return true;
            }
            else if (param.Equals("recordsperhgm"))
            {
                curSeg.RecsPerHGM = val;
                return true;
            }
            else if (param.Equals("newfileperiod"))
            {
                curSeg.NewFilePeriod = val;
                return true;
            }
            else if (param.Equals("voltage"))
            {
                curSeg.HighVoltage = val;
                return true;
            }
            else if (param.Equals("gain"))
            {
                curSeg.Gain = val;
                return true;
            }
            else if (param.Equals("lowerdisc"))
            {
                curSeg.Ld = val;
                return true;
            }
            else if (param.Equals("upperdisc"))
            {
                curSeg.Ud = val;
                return true;
            }
            else if (param.Equals("savehgm"))
            {
                curSeg.LogHGM = val;
                return true;
            }
            else if (param.Equals("pulselevel"))
            {
                curSeg.PulseThresh = val;
                return true;
            }
            else if (param.Equals("pulseon"))
            {
                curSeg.LogExtPulses = val;
                return true;
            }
            else if (param.Equals("temperatureon"))
            {
                curSeg.Logtdegc = val;
                return true;
            }
            else if (param.Equals("humidityon"))
            {
                curSeg.LogRh = val;
                return true;
            }
            else if (param.Equals("batteryon"))
            {
                curSeg.LogBatt = val;
                return true;
            }
            else if (param.Equals("signalon"))
            {
                curSeg.LogSig = val;
                return true;
            }
            else if (line.Contains("YYYY/MO/DD"))
            {
                curSeg.Header = line.Trim(' ', '\r', '\n');
                return true;
            }
            else 
                return false;
        }

        private void ProcessBINArr(byte[] fileBINArr)
        {
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            int i = 0;
            int nrecords = 0;
            int crcErrs = 0;
            DateTimeOffset epoff = DateTimeOffset.FromUnixTimeSeconds(0);
            DateTime epoch = epoff.DateTime;
            if (newEpochRadio.Checked) epoch = epoch.AddYears(30);

            while (i < fileBINArr.Length)
            {
                try
                {
                    long secondsSinceEpoch = BitConverter.ToUInt32(fileBINArr, i);
                    DateTime date = epoch.AddSeconds(secondsSinceEpoch);
                    uint recordIndex = BitConverter.ToUInt32(fileBINArr, i + 4);
                    uint counts = BitConverter.ToUInt32(fileBINArr, i + 8);
                    uint pulseCount = BitConverter.ToUInt32(fileBINArr, i + 12);
                    uint tDegC = fileBINArr[i + 16];
                    uint rh = fileBINArr[i + 17];
                    double battV = (double)fileBINArr[i + 18] / 10;
                    int signal = fileBINArr[i + 19];
                    ushort crc = BitConverter.ToUInt16(fileBINArr, i + 20);
                    byte[] section = new byte[20];
                    Array.Copy(fileBINArr, i, section, 0, 20);
                    UInt16 compCrc = Crc16.ComputeCrc(section);

                    //Debug.WriteLine(secondsSinceEpoch + " : " + date.ToString("G")) ;
                
                if (startDate == DateTime.MinValue)
                {
                    startDate = date;
                    startTimeBox.Text = date.ToString("G");
                }
                endDate = date;

                    Debug.WriteLine(BitConverter.ToString(section).Replace('-', ' '));
                    Debug.WriteLine(BitConverter.ToString(section.Take(4).ToArray()).Replace('-', ' '));
                    Debug.WriteLine($"{secondsSinceEpoch}\n");

                    if (crc == compCrc)
                {
                    AppendCountsPlot(date, recordIndex, counts);
                    AppendPCountsPlot(date, recordIndex, pulseCount);
                    AppendTempPlot(date, recordIndex, tDegC);
                    AppendRHPlot(date, recordIndex, rh);
                    AppendVoltPlot(date, recordIndex, battV);
                    AppendSignalPlot(date, recordIndex, signal);
                    nrecords++;
                } else
                {
                    crcErrs++;
                }

                i += 22;
                }
                catch
                {
                    crcErrs++;
                    break;
                }
            }
            stopTimeBox.Text = endDate.ToString("G");
            elapsedTimeBox.Text = $"{(int)((endDate-startDate).TotalHours)}";
            numRecordsBox.Text = nrecords.ToString();
            crcErrBox.Text = crcErrs.ToString();
        }

        private void AppendSignalPlot(DateTime dt, uint recordIndex, int signal)
        {
            signalSeriesRP.Points.Add(new DataPoint(recordIndex, signal));
            signalSeriesDT.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dt), signal));


            genPlot.InvalidatePlot(true);
        }

        private void AppendVoltPlot(DateTime dt, uint recordIndex, double battV)
        {
            voltSeriesRP.Points.Add(new DataPoint(recordIndex, battV));
            voltSeriesDT.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dt), battV));

            genPlot.InvalidatePlot(true);
        }

        private void AppendRHPlot(DateTime dt, uint recordIndex, double rh)
        {
            rhSeriesRP.Points.Add(new DataPoint(recordIndex, rh));
            rhSeriesDT.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dt), rh));

            genPlot.InvalidatePlot(true);
        }

        private void AppendTempPlot(DateTime dt, uint recordIndex, double tDegC)
        {
            tempSeriesRP.Points.Add(new DataPoint(recordIndex, tDegC));
            tempSeriesDT.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dt), tDegC));

            genPlot.InvalidatePlot(true);
        }

        private void AppendCountsPlot(DateTime dt, uint recordIndex, uint counts)
        {
            countsSeriesRP.Points.Add(new DataPoint(recordIndex,counts));
            countsSeriesDT.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dt), counts));
            AdjustYAxis(counts);

            countsPlot.InvalidatePlot(true);
        }

        private void AdjustYAxis(uint counts)
        {
            if (countsModelAxisDT.Maximum < counts)
            {
                countsModelAxisDT.Maximum = counts + 10;
                countsModelAxisRP.Maximum = counts + 10;
            }
            if (countsModelAxisDT.Minimum > counts)
            {
                countsModelAxisDT.Minimum = counts - 10;
                countsModelAxisRP.Minimum = counts - 10;
            }

            countsModelAxisDT.MajorStep = (int)((countsModelAxisDT.Maximum - countsModelAxisDT.Minimum) / 3);
            countsModelAxisRP.MajorStep = (int)((countsModelAxisDT.Maximum - countsModelAxisDT.Minimum) / 3);

        }

        private void OfflinePlotForm_Load(object sender, EventArgs e)
        {
            // countsrp
            countsModelRP.Series.Add(countsSeriesRP);
            countsModelRP.Axes.Add(new LinearAxis() { Title = "Record", Position = AxisPosition.Bottom });
            countsModelRP.Axes.Add(countsModelAxisRP);
            countsPlot.Model = countsModelRP;
            // countsdt
            countsModelDT.Series.Add(countsSeriesDT);
            countsModelDT.Axes.Add(new DateTimeAxis() { Title = "Date/Time", Position = AxisPosition.Bottom });
            countsModelDT.Axes.Add(countsModelAxisDT);

            // temprp
            tempModelRP.Series.Add(tempSeriesRP);
            tempModelRP.Axes.Add(new LinearAxis() { Title = "Record", Position = AxisPosition.Bottom });
            tempModelRP.Axes.Add(new LinearAxis() { Title = "Temperature (C)", Position = AxisPosition.Left, Maximum=80, Minimum=-20 });
            genPlot.Model = tempModelRP;
            // tempdt
            tempModelDT.Series.Add(tempSeriesDT);
            tempModelDT.Axes.Add(new DateTimeAxis() { Title = "Date/Time", Position = AxisPosition.Bottom });
            tempModelDT.Axes.Add(new LinearAxis() { Title = "Temperature (C)", Position = AxisPosition.Left, Maximum = 80, Minimum = -20 });
            tempRadio.Tag = new Tuple<PlotModel, PlotModel>(tempModelRP, tempModelDT);

            // rhrp
            rhModelRP.Series.Add(rhSeriesRP);
            rhModelRP.Axes.Add(new LinearAxis() { Title = "Record", Position = AxisPosition.Bottom });
            rhModelRP.Axes.Add(new LinearAxis() { Title = "Relative Humidity", Position = AxisPosition.Left, Maximum = 100, Minimum = 0 });
            // rhdt
            rhModelDT.Series.Add(rhSeriesDT);
            rhModelDT.Axes.Add(new DateTimeAxis() { Title = "Date/Time", Position = AxisPosition.Bottom });
            rhModelDT.Axes.Add(new LinearAxis() { Title = "Relative Humidity", Position = AxisPosition.Left, Maximum = 100, Minimum = 0 });
            rhRadio.Tag = new Tuple<PlotModel, PlotModel>(rhModelRP, rhModelDT);

            // voltrp
            voltModelRP.Series.Add(voltSeriesRP);
            voltModelRP.Axes.Add(new LinearAxis() { Title = "Record", Position = AxisPosition.Bottom });
            voltModelRP.Axes.Add(new LinearAxis() { Title = "Supply Voltage", Position = AxisPosition.Left, Maximum = 20, Minimum = 0 });
            // voltdt
            voltModelDT.Series.Add(voltSeriesDT);
            voltModelDT.Axes.Add(new DateTimeAxis() { Title = "Date/Time", Position = AxisPosition.Bottom });
            voltModelDT.Axes.Add(new LinearAxis() { Title = "Supply Voltage", Position = AxisPosition.Left, Maximum = 20, Minimum =0 });
            voltRadio.Tag = new Tuple<PlotModel, PlotModel>(voltModelRP, voltModelDT);

            // signalrp
            signalModelRP.Series.Add(signalSeriesRP);
            signalModelRP.Axes.Add(new LinearAxis() { Title = "Record", Position = AxisPosition.Bottom });
            signalModelRP.Axes.Add(new LinearAxis() { Title = "Signal Input", Position = AxisPosition.Left, Maximum = 2, Minimum = -1 });
            // signaldt
            signalModelDT.Series.Add(signalSeriesDT);
            signalModelDT.Axes.Add(new DateTimeAxis() { Title = "Date/Time", Position = AxisPosition.Bottom });
            signalModelDT.Axes.Add(new LinearAxis() { Title = "Signal Input", Position = AxisPosition.Left, Maximum = 2, Minimum = -1 });
            signalRadio.Tag = new Tuple<PlotModel, PlotModel>(signalModelRP, signalModelDT);

        }

        private void CountsXAxis(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                countsPlot.Model = countsModelRP;

            } else
            {
                countsPlot.Model = countsModelDT;
            }

            countsModelRP.Series.Clear();
            countsModelDT.Series.Clear();
            if (extCountsCheck.Checked)
            {
                if (((RadioButton)sender).Checked)
                    countsPlot.Model.Series.Add(pCountsSeriesRP);
                else
                    countsPlot.Model.Series.Add(pCountsSeriesDT);
            }
            if (npmCountsCheck.Checked)
            {
                if (((RadioButton)sender).Checked)
                    countsPlot.Model.Series.Add(countsSeriesRP);
                else
                    countsPlot.Model.Series.Add(countsSeriesDT);
            }


            countsPlot.Model.InvalidatePlot(true);

        }

        private void ChangegenPlot(object sender, EventArgs e)
        {
            RadioButton s = (RadioButton)sender;
            if (s.Checked)
            {
                if (genRPRadio.Checked) 
                    genPlot.Model = ((Tuple<PlotModel, PlotModel>)s.Tag).Item1;
                else
                    genPlot.Model = ((Tuple<PlotModel, PlotModel>)s.Tag).Item2;
            }
        }

        private void genRPRadio_CheckedChanged(object sender, EventArgs e)
        {
            foreach (RadioButton btn in genPanel.Controls.OfType<RadioButton>())
            {
                if (btn.Checked)
                {
                    if (genRPRadio.Checked)
                        genPlot.Model = ((Tuple<PlotModel, PlotModel>)btn.Tag).Item1;
                    else
                        genPlot.Model = ((Tuple<PlotModel, PlotModel>)btn.Tag).Item2;
                    return;
                }
            }
        }

        private void Hide(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void openDATFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["DATPATH"].Length > 0)
            {
                openFileDialog.InitialDirectory = ConfigurationManager.AppSettings["DATPATH"];
            }
            openFileDialog.ShowDialog();
            if (!File.Exists(openFileDialog.FileName)) return;
            if (!openFileDialog.FileName.ToLower().Contains(".dat"))
            {
                MessageBox.Show($"Wrong Filetype:\n'{openFileDialog.FileName}'");
                return;
            }
            string fileDATString = File.ReadAllText(openFileDialog.FileName);
            Reset();
            ProcessDATArr(fileDATString);
            fileNameBox.Text = openFileDialog.FileName;
        }

        private void backSegBtn_Click(object sender, EventArgs e)
        {
            if (segments.Count == 0) return;
            curSegIndex--;
            if (curSegIndex < 0) curSegIndex = segments.Count - 1;
            DisplaySegData();
        }

        private void advSegBtn_Click(object sender, EventArgs e)
        {
            if (segments.Count == 0) return;
            curSegIndex++;
            if (curSegIndex > segments.Count - 1) curSegIndex = 0;
            DisplaySegData();
        }

        private void npmCounts_CheckedChanged(object sender, EventArgs e)
        {
            foreach (RadioButton btn in countsPanel.Controls.OfType<RadioButton>())
            {
                if (btn.Checked)
                {
                    if (recordRadio.Checked)
                        countsPlot.Model = ((Tuple<PlotModel, PlotModel>)btn.Tag).Item1;
                    else
                        countsPlot.Model = ((Tuple<PlotModel, PlotModel>)btn.Tag).Item2;
                    return;
                }
            }
        }

        private void npmCountsCheck_CheckedChanged(object sender, EventArgs e)
        {
            CountsXAxis(recordRadio, null);
        }

        private void refMain_Click(object sender, EventArgs e)
        {
            countsModelRP.ResetAllAxes();
            countsModelDT.ResetAllAxes();
            countsModelRP.InvalidatePlot(true);
            countsModelDT.InvalidatePlot(true);
        }

        private void dateRadio_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void refGen_Click(object sender, EventArgs e)
        {
            tempModelDT.ResetAllAxes();
            tempModelRP.ResetAllAxes();
            tempModelRP.InvalidatePlot(true);
            tempModelDT.InvalidatePlot(true);

            rhModelDT.ResetAllAxes();
            rhModelRP.ResetAllAxes();
            rhModelDT.InvalidatePlot(true);
            rhModelRP.InvalidatePlot(true);

            signalModelDT.ResetAllAxes();
            signalModelRP.ResetAllAxes();
            signalModelDT.InvalidatePlot(true);
            signalModelRP.InvalidatePlot(true);

            voltModelDT.ResetAllAxes();
            voltModelRP.ResetAllAxes();
            voltModelDT.InvalidatePlot(true);
            voltModelRP.InvalidatePlot(true);

        }


        private void newEpochRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (fileNameBox.Text.ToLower().Contains(".bin"))
            {
                byte[] fileBINArr = File.ReadAllBytes(fileNameBox.Text);
                Reset();
                ProcessBINArr(fileBINArr);
            }
        }
    }

    public class Crc16
    {
        private static ushort[] CrcTable = {
        0X0000, 0XC0C1, 0XC181, 0X0140, 0XC301, 0X03C0, 0X0280, 0XC241,
        0XC601, 0X06C0, 0X0780, 0XC741, 0X0500, 0XC5C1, 0XC481, 0X0440,
        0XCC01, 0X0CC0, 0X0D80, 0XCD41, 0X0F00, 0XCFC1, 0XCE81, 0X0E40,
        0X0A00, 0XCAC1, 0XCB81, 0X0B40, 0XC901, 0X09C0, 0X0880, 0XC841,
        0XD801, 0X18C0, 0X1980, 0XD941, 0X1B00, 0XDBC1, 0XDA81, 0X1A40,
        0X1E00, 0XDEC1, 0XDF81, 0X1F40, 0XDD01, 0X1DC0, 0X1C80, 0XDC41,
        0X1400, 0XD4C1, 0XD581, 0X1540, 0XD701, 0X17C0, 0X1680, 0XD641,
        0XD201, 0X12C0, 0X1380, 0XD341, 0X1100, 0XD1C1, 0XD081, 0X1040,
        0XF001, 0X30C0, 0X3180, 0XF141, 0X3300, 0XF3C1, 0XF281, 0X3240,
        0X3600, 0XF6C1, 0XF781, 0X3740, 0XF501, 0X35C0, 0X3480, 0XF441,
        0X3C00, 0XFCC1, 0XFD81, 0X3D40, 0XFF01, 0X3FC0, 0X3E80, 0XFE41,
        0XFA01, 0X3AC0, 0X3B80, 0XFB41, 0X3900, 0XF9C1, 0XF881, 0X3840,
        0X2800, 0XE8C1, 0XE981, 0X2940, 0XEB01, 0X2BC0, 0X2A80, 0XEA41,
        0XEE01, 0X2EC0, 0X2F80, 0XEF41, 0X2D00, 0XEDC1, 0XEC81, 0X2C40,
        0XE401, 0X24C0, 0X2580, 0XE541, 0X2700, 0XE7C1, 0XE681, 0X2640,
        0X2200, 0XE2C1, 0XE381, 0X2340, 0XE101, 0X21C0, 0X2080, 0XE041,
        0XA001, 0X60C0, 0X6180, 0XA141, 0X6300, 0XA3C1, 0XA281, 0X6240,
        0X6600, 0XA6C1, 0XA781, 0X6740, 0XA501, 0X65C0, 0X6480, 0XA441,
        0X6C00, 0XACC1, 0XAD81, 0X6D40, 0XAF01, 0X6FC0, 0X6E80, 0XAE41,
        0XAA01, 0X6AC0, 0X6B80, 0XAB41, 0X6900, 0XA9C1, 0XA881, 0X6840,
        0X7800, 0XB8C1, 0XB981, 0X7940, 0XBB01, 0X7BC0, 0X7A80, 0XBA41,
        0XBE01, 0X7EC0, 0X7F80, 0XBF41, 0X7D00, 0XBDC1, 0XBC81, 0X7C40,
        0XB401, 0X74C0, 0X7580, 0XB541, 0X7700, 0XB7C1, 0XB681, 0X7640,
        0X7200, 0XB2C1, 0XB381, 0X7340, 0XB101, 0X71C0, 0X7080, 0XB041,
        0X5000, 0X90C1, 0X9181, 0X5140, 0X9301, 0X53C0, 0X5280, 0X9241,
        0X9601, 0X56C0, 0X5780, 0X9741, 0X5500, 0X95C1, 0X9481, 0X5440,
        0X9C01, 0X5CC0, 0X5D80, 0X9D41, 0X5F00, 0X9FC1, 0X9E81, 0X5E40,
        0X5A00, 0X9AC1, 0X9B81, 0X5B40, 0X9901, 0X59C0, 0X5880, 0X9841,
        0X8801, 0X48C0, 0X4980, 0X8941, 0X4B00, 0X8BC1, 0X8A81, 0X4A40,
        0X4E00, 0X8EC1, 0X8F81, 0X4F40, 0X8D01, 0X4DC0, 0X4C80, 0X8C41,
        0X4400, 0X84C1, 0X8581, 0X4540, 0X8701, 0X47C0, 0X4680, 0X8641,
        0X8201, 0X42C0, 0X4380, 0X8341, 0X4100, 0X81C1, 0X8081, 0X4040 };

        public static UInt16 ComputeCrc(byte[] data)
        {
            ushort crc = 0xFFFF;

            foreach (byte datum in data)
            {
                crc = (ushort)((crc >> 8) ^ CrcTable[(crc ^ datum) & 0xFF]);
            }

            return crc;
        }
    }


}

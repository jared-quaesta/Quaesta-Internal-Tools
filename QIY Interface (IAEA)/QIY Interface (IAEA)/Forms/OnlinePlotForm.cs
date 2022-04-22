using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIY_Interface__IAEA_
{
    public partial class OnlinePlotForm : Form
    {
        TCPManager tcpMan;
        MainForm mainForm;

        // counts series
        PlotModel countsModelRP = new PlotModel() { Title = "Counts" };
        LineSeries countsSeriesRP = new LineSeries() { Title = "Counts", Color = OxyColors.Green };
        PlotModel countsModelDT = new PlotModel() { Title = "Counts" };
        LineSeries countsSeriesDT = new LineSeries() { Title = "Counts", Color = OxyColors.Green };

        // pscounts series
        LineSeries pCountsSeriesRP = new LineSeries() { Title = "Ext. Pulse Counts", Color = OxyColors.Blue };
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


        internal OnlinePlotForm(TCPManager tcpMan, MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.tcpMan = tcpMan;
            InitializeComponent();
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

            countsModelAxisDT.MajorStep = (int) ((countsModelAxisDT.Maximum - countsModelAxisDT.Minimum) / 3);
            countsModelAxisRP.MajorStep = (int) ((countsModelAxisDT.Maximum - countsModelAxisDT.Minimum) / 3);

        }

        private void OnlinePlotForm_Load(object sender, EventArgs e)
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
            tempModelRP.Axes.Add(new LinearAxis() { Title = "Temperature (C)", Position = AxisPosition.Left, Minimum=-20, Maximum=80  });
            genPlot.Model = tempModelRP;
            // tempdt
            tempModelDT.Series.Add(tempSeriesDT);
            tempModelDT.Axes.Add(new DateTimeAxis() { Title = "Date/Time", Position = AxisPosition.Bottom });
            tempModelDT.Axes.Add(new LinearAxis() { Title = "Temperature (C)", Position = AxisPosition.Left, Minimum = -20, Maximum = 80 });
            tempRadio.Tag = new Tuple<PlotModel, PlotModel>(tempModelRP, tempModelDT);

            // rhrp
            rhModelRP.Series.Add(rhSeriesRP);
            rhModelRP.Axes.Add(new LinearAxis() { Title = "Record", Position = AxisPosition.Bottom });
            rhModelRP.Axes.Add(new LinearAxis() { Title = "Relative Humidity", Position = AxisPosition.Left, Minimum = 0, Maximum = 100 });
            // rhdt
            rhModelDT.Series.Add(rhSeriesDT);
            rhModelDT.Axes.Add(new DateTimeAxis() { Title = "Date/Time", Position = AxisPosition.Bottom });
            rhModelDT.Axes.Add(new LinearAxis() { Title = "Relative Humidity", Position = AxisPosition.Left, Minimum = 0, Maximum = 100 });
            rhRadio.Tag = new Tuple<PlotModel, PlotModel>(rhModelRP, rhModelDT);

            // voltrp
            voltModelRP.Series.Add(voltSeriesRP);
            voltModelRP.Axes.Add(new LinearAxis() { Title = "Record", Position = AxisPosition.Bottom });
            voltModelRP.Axes.Add(new LinearAxis() { Title = "Supply Voltage", Position = AxisPosition.Left, Minimum = 0, Maximum = 20 });
            // voltdt
            voltModelDT.Series.Add(voltSeriesDT);
            voltModelDT.Axes.Add(new DateTimeAxis() { Title = "Date/Time", Position = AxisPosition.Bottom });
            voltModelDT.Axes.Add(new LinearAxis() { Title = "Supply Voltage", Position = AxisPosition.Left, Minimum = 0, Maximum = 20 });
            voltRadio.Tag = new Tuple<PlotModel, PlotModel>(voltModelRP, voltModelDT);

            // signalrp
            signalModelRP.Series.Add(signalSeriesRP);
            signalModelRP.Axes.Add(new LinearAxis() { Title = "Record", Position = AxisPosition.Bottom });
            signalModelRP.Axes.Add(new LinearAxis() { Title = "Signal Input", Position = AxisPosition.Left, Minimum = -1, Maximum = 2 });
            // signaldt
            signalModelDT.Series.Add(signalSeriesDT);
            signalModelDT.Axes.Add(new DateTimeAxis() { Title = "Date/Time", Position = AxisPosition.Bottom });
            signalModelDT.Axes.Add(new LinearAxis() { Title = "Signal Input", Position = AxisPosition.Left, Minimum = -1, Maximum = 2 });
            signalRadio.Tag = new Tuple<PlotModel, PlotModel>(signalModelRP, signalModelDT);

            GetCurParams();

        }

        internal void IncomingDat(string line)
        {
            GetCurParams();
            // 2022/03/08 00:32:25,   332,         0,         0, 27.9,13.7,11.88,0,ACBC
            string[] splitLine = line.Split(',');

            int dateIndex = 0;
            int recordIndex = 1;
            int countsIndex = 2;

            if (!DateTime.TryParseExact(splitLine[dateIndex], "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                return;
            if (!int.TryParse(splitLine[recordIndex], out int rec))
                return;
            if (!int.TryParse(splitLine[countsIndex], out int counts))
                return;

            AppendCountsPlot(dt, rec, counts);

            int curIndex = 3;
            if (tcpMan.extPulseOn)
            {
                if (!int.TryParse(splitLine[curIndex], out int extCounts))
                    return;
                else
                {
                    AppendPCountsPlot(dt, rec, extCounts);
                    curIndex++;
                }
            }
            if (tcpMan.tempOn)
            {
                if (!double.TryParse(splitLine[curIndex], out double temp))
                    return;
                else
                {
                    AppendTempPlot(dt, rec, temp);
                    curIndex++;
                }
            }
            if (tcpMan.humOn)
            {
                if (!double.TryParse(splitLine[curIndex], out double hum))
                    return;
                else
                {
                    AppendRHPlot(dt, rec, hum);
                    curIndex++;
                }
            }
            if (tcpMan.battOn)
            {
                if (!double.TryParse(splitLine[curIndex], out double batt))
                    return;
                else
                {
                    AppendVoltPlot(dt, rec, batt);
                    curIndex++;
                }
            }
            if (tcpMan.sigOn)
            {
                if (!int.TryParse(splitLine[curIndex], out int sig))
                    return;
                else
                {
                    AppendSignalPlot(dt, rec, sig);
                }
            }

        }

        private void AppendSignalPlot(DateTime dt, int recordIndex, int signal)
        {
            signalSeriesRP.Points.Add(new DataPoint(recordIndex, signal));
            signalSeriesDT.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dt), signal));

            genPlot.InvalidatePlot(true);
        }

        private void AppendVoltPlot(DateTime dt, int recordIndex, double battV)
        {
            voltSeriesRP.Points.Add(new DataPoint(recordIndex, battV));
            voltSeriesDT.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dt), battV));

            genPlot.InvalidatePlot(true);
        }

        private void AppendRHPlot(DateTime dt, int recordIndex, double rh)
        {
            rhSeriesRP.Points.Add(new DataPoint(recordIndex, rh));
            rhSeriesDT.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dt), rh));

            genPlot.InvalidatePlot(true);
        }


        private void AppendPCountsPlot(DateTime resDate, int recordIndex, int pcounts)
        {
            pCountsSeriesDT.Points.Add(new DataPoint(DateTimeAxis.ToDouble(resDate), pcounts));
            pCountsSeriesRP.Points.Add(new DataPoint(recordIndex, pcounts));
            AdjustYAxis((uint)pcounts);
            countsPlot.InvalidatePlot(true);
        }


        private void AppendTempPlot(DateTime dt, int recordIndex, double tDegC)
        {
            tempSeriesRP.Points.Add(new DataPoint(recordIndex, tDegC));
            tempSeriesDT.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dt), tDegC));

            genPlot.InvalidatePlot(true);
        }

        private void AppendCountsPlot(DateTime dt, int recordIndex, int counts)
        {
            countsSeriesRP.Points.Add(new DataPoint(recordIndex, counts));
            countsSeriesDT.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dt), counts));
            AdjustYAxis((uint)counts);
            countsPlot.InvalidatePlot(true);
        }

        private void GetCurParams()
        {
            string[] curParams = mainForm.GetCurParams().Split('|');
            segNameBox.Text = curParams[0];
            segSnBox.Text = curParams[1];
            segRpBox.Text = curParams[2];
            segRecPerHGMBox.Text = curParams[3];
            segNfBox.Text = curParams[4];
            segHvBox.Text = curParams[5];
            segGainBox.Text = curParams[6];
            segDiscLowBox.Text = curParams[7];
            segDiscHighBox.Text = curParams[8];
            segLogHgmsBox.Text = curParams[9];
            segPulseThresh.Text = curParams[10];
            segLogExtBox.Text = curParams[11];
            segLogTBox.Text = curParams[12];
            segLogRhBox.Text = curParams[13];
            segLogBattBox.Text = curParams[14];
            segLogSigBox.Text = curParams[15];

            if (curParams[16].Trim('\r', '\n', ' ').Equals("1"))
            {
                saveDatStatus.Text = "RECORDING";
                saveDatStatus.ForeColor = Color.Green;
            } else
            {
                saveDatStatus.Text = "NOT RECORDING";
                saveDatStatus.ForeColor = Color.Red;
            }
            if (curParams[17].Trim('\r', '\n', ' ').Equals("1"))
            {
                saveHGMStatus.Text = "RECORDING";
                saveHGMStatus.ForeColor = Color.Green;
            }
            else
            {
                saveHGMStatus.Text = "NOT RECORDING";
                saveHGMStatus.ForeColor = Color.Red;
            }
            if (curParams[18].Trim('\r', '\n', ' ').Equals("1"))
            {
                saveBinStatus.Text = "RECORDING";
                saveBinStatus.ForeColor = Color.Green;
            }
            else
            {
                saveBinStatus.Text = "NOT RECORDING";
                saveBinStatus.ForeColor = Color.Red;
            }

        }

        private void queryAllBtn_Click(object sender, EventArgs e)
        {
            GetCurParams();
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

        private void recordRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                countsPlot.Model = countsModelRP;

            }
            else
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

        private void genRadioChange(object sender, EventArgs e)
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

        private void npmCountsCheck_CheckedChanged(object sender, EventArgs e)
        {
            CountsXAxis(recordRadio, null);
        }

        private void extCountsCheck_CheckedChanged(object sender, EventArgs e)
        {
            CountsXAxis(recordRadio, null);
        }
        private void CountsXAxis(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                countsPlot.Model = countsModelRP;

            }
            else
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

        }

        private void queryParamsBtn_Click(object sender, EventArgs e)
        {
            GetCurParams();
        }

        private void HideForm(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}

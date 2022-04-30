using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeneralFirstPhase
{
    public partial class HGMPlotForm : Form
    {
        PlotModel rawModel = new PlotModel();
        PlotModel cumuModel = new PlotModel();
        int volt;
        Dictionary<string, Tuple<LineSeries, LineSeries>> seriesDict = new Dictionary<string, Tuple<LineSeries, LineSeries>>();
        internal bool canClose = false;
        int range;
        bool log = false;
        string yAxis;
        public HGMPlotForm(string yAxis, bool log = false)
        {
            this.yAxis = yAxis;
            this.log = log;
            InitializeComponent();
        }

        private void HGMPlotForm_Load(object sender, EventArgs e)
        {
            plotView.Model = rawModel;
            rawModel.SelectionColor = rawModel.SubtitleColor = rawModel.TextColor = rawModel.TitleColor = OxyColors.White;
            rawModel.Title = "Histograms by NPM";
            rawModel.Legends.Add(new Legend()
            {
                LegendPosition = LegendPosition.BottomRight,
                IsLegendVisible = true,
                TextColor = OxyColors.White,
                LegendTextColor = OxyColors.White
            });
            rawModel.Axes.Clear();
            if (log)
            {
                LogarithmicAxis yAx = new LogarithmicAxis()
                {
                    Title = yAxis,
                    Position = AxisPosition.Left,
                    TextColor = OxyColors.White,
                    AxislineColor = OxyColors.White,
                    TicklineColor = OxyColors.White
                };

                rawModel.Axes.Add(yAx);
            }
            else
            {
                LinearAxis yAx = new LinearAxis()
                {
                    Title = yAxis,
                    Position = AxisPosition.Left,
                    TextColor = OxyColors.White,
                    AxislineColor = OxyColors.White,
                    TicklineColor = OxyColors.White
                };

                rawModel.Axes.Add(yAx);
            }
            LinearAxis xAx = new LinearAxis()
            {
                Title = "Bin",
                Position = AxisPosition.Bottom,
                TextColor = OxyColors.White,
                AxislineColor = OxyColors.White,
                TicklineColor = OxyColors.White
            };
            rawModel.Axes.Add(xAx);





            cumuModel.SelectionColor = cumuModel.SubtitleColor = cumuModel.TextColor = cumuModel.TitleColor = OxyColors.White;
            cumuModel.Title = "Histograms by NPM (Cumulative)";
            cumuModel.Legends.Add(new Legend()
            {
                LegendPosition = LegendPosition.BottomRight,
                IsLegendVisible = true,
                TextColor = OxyColors.White,
                LegendTextColor = OxyColors.White
            });
            cumuModel.Axes.Clear();
            LinearAxis cyAx = new LinearAxis()
            {
                Title = "Counts",
                Position = AxisPosition.Left,
                TextColor = OxyColors.White,
                AxislineColor = OxyColors.White,
                TicklineColor = OxyColors.White
            };
            LinearAxis cxAx = new LinearAxis()
            {
                Title = "Bin",
                Position = AxisPosition.Bottom,
                TextColor = OxyColors.White,
                AxislineColor = OxyColors.White,
                TicklineColor = OxyColors.White
            };
            cumuModel.Axes.Add(cyAx);
            cumuModel.Axes.Add(cxAx);

        }

        public void UpdateSeries(int[] series, string serial)
        {
            List<DataPoint> rawHGM = new List<DataPoint>();
            List<DataPoint> cumHGM = new List<DataPoint>();

            for (int i = 0; i < series.Length; i++)
            {
                rawHGM.Add(new DataPoint(i, series[i]));

                if (i != 0)
                {
                    double prev = series[i - 1];
                    double cur = series[i];
                    cumHGM.Add(new DataPoint(i, prev + cur));
                    series[i] = (int)(prev + cur);
                }

            }

            if (seriesDict.ContainsKey(serial))
            {
                seriesDict[serial].Item1.Points.Clear();
                seriesDict[serial].Item2.Points.Clear();
                seriesDict[serial].Item1.Points.AddRange(rawHGM);
                seriesDict[serial].Item2.Points.AddRange(cumHGM);
            }
            else
            {
                LineSeries newRaw = new LineSeries() { Title = serial };
                LineSeries newCumu = new LineSeries() { Title = serial };
                newRaw.Points.AddRange(rawHGM);
                newCumu.Points.AddRange(cumHGM);

                seriesDict.Add(serial, new Tuple<LineSeries, LineSeries>(newRaw, newCumu));
                cumuModel.Series.Add(newCumu);
                rawModel.Series.Add(newRaw);


            }
            CorrectCumulative();
        }

        private void CorrectCumulative()
        {
            if (cumuCheck.Checked)
            {
                plotView.Model = cumuModel;
                cumuModel.InvalidatePlot(true);
            }
            else
            {
                plotView.Model = rawModel;
                rawModel.InvalidatePlot(true);
            }

        }



        private void CancelClose(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !canClose;
        }

        private void cumuCheck_CheckedChanged(object sender, EventArgs e)
        {
            CorrectCumulative();
        }
    }
}

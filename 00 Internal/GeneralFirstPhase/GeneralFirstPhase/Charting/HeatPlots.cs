using GeneralFirstPhase.Data;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralFirstPhase.Charting
{
    public partial class HeatPlots : UserControl
    {
        Dictionary<string, LineSeries> voltSeriesDict = new Dictionary<string, LineSeries>();
        Dictionary<string, LineSeries> npmSeriesDict = new Dictionary<string, LineSeries>();
        
        PlotModel cs215Model = new PlotModel();
        LineSeries cs215Series = new LineSeries();

        PlotModel voltageModel = new PlotModel();
        LineSeries VoltageSeries = new LineSeries();

        PlotModel npmTempModel = new PlotModel();
        LineSeries npmTempSeries = new LineSeries();


        public HeatPlots()
        {
            InitializeComponent();
        }

        internal void UpdateCharts(ConcurrentDictionary<string, HeaterDataResults> data)
        {
            foreach (string sn in data.Keys)
            {
                HeaterDataResults results = data[sn];
                // voltage
                if (voltSeriesDict.ContainsKey(sn))
                {
                    voltSeriesDict[sn].Points.Add(new DataPoint(DateTimeAxis.ToDouble(results.Time), results.Voltage));
                } else
                {
                    LineSeries series = new LineSeries() 
                    {
                        Title=sn
                    };
                    series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(results.Time), results.Voltage));
                    voltSeriesDict.Add(sn, series);
                    voltageModel.Series.Add(series);
                }

                // npm temp
                if (npmSeriesDict.ContainsKey(sn))
                {
                    npmSeriesDict[sn].Points.Add(new DataPoint(DateTimeAxis.ToDouble(results.Time), results.NpmTemp));
                } else
                {
                    LineSeries series = new LineSeries() 
                    {
                        Title=sn
                    };
                    series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(results.Time), results.NpmTemp));
                    npmSeriesDict.Add(sn, series);
                    npmTempModel.Series.Add(series);
                }




                cs215Series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(results.Time), results.Cs215Temp));
            }
            cs215Model.InvalidatePlot(true);
            voltageModel.InvalidatePlot(true);
            npmTempModel.InvalidatePlot(true);
        }

        private void HeatPlots_Load(object sender, EventArgs e)
        {
            varySelectBox.SelectedIndex = 0;
            // cs215 plot
            cs215Model.Axes.Clear();

            DateTimeAxis cs215X = new DateTimeAxis()
            {
                IsPanEnabled = false,
                IsZoomEnabled = false
            };
            LinearAxis cs215Y = new LinearAxis()
            {
                Maximum = 70,
                Minimum = -10,
                MajorStep = 30
            };
            cs215Model.Axes.Add(cs215X);
            cs215Model.Axes.Add(cs215Y);
            cs215Model.Series.Add(cs215Series);
            cs215Plot.Model = cs215Model;

            voltageModel.Axes.Clear();
            DateTimeAxis voltX = new DateTimeAxis()
            {
                IsPanEnabled = false,
                IsZoomEnabled = false
            };
            voltageModel.Axes.Add(voltX);
            voltageModel.Series.Add(VoltageSeries);

            npmTempModel.Axes.Clear();
            DateTimeAxis tempX = new DateTimeAxis()
            {
                IsPanEnabled = false,
                IsZoomEnabled = false
            };
            npmTempModel.Axes.Add(tempX);
            npmTempModel.Series.Add(npmTempSeries);

        }

        private void varySelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (varySelectBox.Text.Equals("Voltage"))
            {
                variablePlot.Model = voltageModel;
            }
            else
            {
                variablePlot.Model = npmTempModel;
            }
        }
    }
}

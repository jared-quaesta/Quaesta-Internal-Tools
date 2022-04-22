using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIXLPTesting
{
    public partial class HGMPlotForm : Form
    {
        PlotModel model = new PlotModel();
        int volt;
        Dictionary<string, LineSeries> seriesDict = new Dictionary<string, LineSeries>();
        internal bool canClose = false;
        int range;
        public HGMPlotForm()
        {
            InitializeComponent();
        }

        private void HGMPlotForm_Load(object sender, EventArgs e)
        {
            plotView.Model = model;
            model.SelectionColor = model.SubtitleColor = model.TextColor = model.TitleColor = OxyColors.White;
            model.Title = "Overlayed Histograms  by NPM";
            model.Legends.Add(new Legend()
            {
                LegendPosition = LegendPosition.BottomRight,
                IsLegendVisible = true,
                TextColor = OxyColors.White,
                LegendTextColor = OxyColors.White
            });
            model.Axes.Clear();
            LinearAxis yAx = new LinearAxis()
            {
                Title = "Counts",
                Position = AxisPosition.Left,
                TextColor = OxyColors.White,
                AxislineColor = OxyColors.White,
                TicklineColor = OxyColors.White
            };
            LinearAxis xAx = new LinearAxis()
            {
                Title = "Bin",
                Position = AxisPosition.Bottom,
                TextColor = OxyColors.White,
                AxislineColor = OxyColors.White,
                TicklineColor = OxyColors.White
            };
            model.Axes.Add(yAx);
            model.Axes.Add(xAx);

        }

        public void AppendSeries(LineSeries series, string serial)
        {
            if (seriesDict.ContainsKey(serial))
            {
                model.Series.Remove(seriesDict[serial]);
                seriesDict[serial] = series;
                model.Series.Add(seriesDict[serial]);
            }
            else
            {
                seriesDict.Add(serial, series);
                model.Series.Add(seriesDict[serial]);
            }
            model.InvalidatePlot(true);
        }

        private void CancelClose(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !canClose;
        }
    }
}

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
    public partial class VoltagePlotForm : Form
    {
        PlotModel model = new PlotModel();
        int volt;
        Dictionary<string, LineSeries> seriestDict = new Dictionary<string, LineSeries>();
        internal bool canClose = false;
        int range;

        public VoltagePlotForm(int volt, int range)
        {
            this.range = range;
            this.volt = volt;
            InitializeComponent();
        }

        private void VoltagePlotForm_Load(object sender, EventArgs e)
        {
            plotView.Model = model;
            model.SelectionColor = model.SubtitleColor = model.TextColor = model.TitleColor = OxyColors.White;
            model.Title = "Voltage By NPM";
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
                Title = "Voltage",
                Position = AxisPosition.Left,
                TextColor = OxyColors.White,
                AxislineColor = OxyColors.White,
                TicklineColor = OxyColors.White
            };
            LinearAxis xAx = new LinearAxis()
            {
                Title = "Time (Seconds)",
                Position = AxisPosition.Bottom,
                TextColor = OxyColors.White,
                AxislineColor = OxyColors.White,
                TicklineColor = OxyColors.White
            };
            model.Axes.Add(yAx);
            model.Axes.Add(xAx);

            seriestDict.Add("RANGE_MIN", new LineSeries()
            {
                Color = OxyColors.White,
                Title = "RANGE MINIMUM"
            });
            seriestDict.Add("RANGE_MAX", new LineSeries()
            {
                Color = OxyColors.White,
                Title = "RANGE MAXIMUM"
            });

            model.Series.Add(seriestDict["RANGE_MIN"]);
            model.Series.Add(seriestDict["RANGE_MAX"]);


        }

        internal void UpdatePlot()
        {
            model.InvalidatePlot(true);
        }


        internal void AddData(string com, DataPoint point)
        {
            if (seriestDict.ContainsKey(com))
            {
                seriestDict[com].Points.Add(point);
            }
            else
            {
                seriestDict.Add(com, new LineSeries() {Title = com });
                seriestDict[com].Points.Add(point);
                model.Series.Add(seriestDict[com]);
            }
            seriestDict["RANGE_MIN"].Points.Add(new DataPoint(point.X, volt - range));
            seriestDict["RANGE_MAX"].Points.Add(new DataPoint(point.X, volt + range));
            UpdatePlot();
        }

        private void CancelClose(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !canClose;
        }
    }
}

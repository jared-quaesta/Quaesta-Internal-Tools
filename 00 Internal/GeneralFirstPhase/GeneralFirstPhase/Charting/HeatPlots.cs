using GeneralFirstPhase.Data;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
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
        // {
        //  MODEL1: {               < selIndex=0
        //              NPM1 SERIES        
        //              NPM2 SERIES         
        //              NPM3 SERIES        
        //              NPM4 SERIES         
        //              NPM5 SERIES        
        //              NPM6 SERIES        
        //          }
        //  MODEL2: {               < selIndex=1
        //              NPM7 SERIES        
        //              NPM8 SERIES         
        //              NPM9 SERIES        
        //              NPM10 SERIES         
        //              NPM11 SERIES        
        //              NPM12 SERIES           
        //          }
        // }
        List<Tuple<PlotModel, List<LineSeries>>> voltageSplit = new List<Tuple<PlotModel, List<LineSeries>>>();
        List<Tuple<PlotModel, List<LineSeries>>> tempSplit = new List<Tuple<PlotModel, List<LineSeries>>>();

        // {
        //  MODEL1: {               < selIndex=0
        //              {Time: NPM1 SERIES}         
        //              {Time: NPM2 SERIES}
        //              {Time: NPM3 SERIES}         
        //              {Time: NPM4 SERIES}         
        //              {Time: NPM5 SERIES}         
        //              {Time: NPM6 SERIES}         
        //          }
        //  MODEL2: {               < selIndex=1
        //              {Time: NPM7 SERIES}         
        //              {Time: NPM8 SERIES}
        //              {Time: NPM9 SERIES}         
        //              {Time: NPM10 SERIES}         
        //              {Time: NPM11 SERIES}         
        //              {Time: NPM12 SERIES}             
        //          }
        // }


        // put lineseries in the correct place 
        List<Tuple<PlotModel, List<Dictionary<double, LineSeries>>>> sdevSplit = new List<Tuple<PlotModel, List<Dictionary<double, LineSeries>>>>();
        List<Tuple<PlotModel, List<Dictionary<double, LineSeries>>>> psSplit = new List<Tuple<PlotModel, List<Dictionary<double, LineSeries>>>>();


        int selIndex = 0;
        double selTime = -1;


        LinearAxis cs215Y = new LinearAxis()
        {
            Maximum = 70,
            Minimum = -10,
            MajorStep = 30,
            IsZoomEnabled = false
        };

        // directly edit the Line Series using a dictionary
        Dictionary<string, LineSeries> voltSeriesDict = new Dictionary<string, LineSeries>();
        Dictionary<string, LineSeries> npmSeriesDict = new Dictionary<string, LineSeries>();
        Dictionary<string, Dictionary<double, LineSeries>> psSeriesDict = new Dictionary<string, Dictionary<double, LineSeries>>();
        Dictionary<string, Dictionary<double, LineSeries>> sdevSeriesDict = new Dictionary<string, Dictionary<double, LineSeries>>();

        LineSeries sliderLine = new LineSeries() { Color = OxyColors.Red };

        PlotModel cs215Model = new PlotModel();
        LineSeries cs215Series = new LineSeries();


        public HeatPlots()
        {
            InitializeComponent();
        }

        internal void UpdateCharts(ConcurrentDictionary<string, HeaterDataResults> data)
        {
            bool gotFirst = false;

            foreach (string sn in data.Keys)
            {
                HeaterDataResults results = data[sn];
                double time = DateTimeAxis.ToDouble(results.Time);

                // psHGM
                LineSeries psSeries = new LineSeries() {Title = results.Serial};
                int psB = 0;
                foreach (string bin in results.PsHGM.Split(','))
                {
                    psSeries.Points.Add(new DataPoint(psB++, int.Parse(bin)));
                }
                if (psSeriesDict.ContainsKey(sn))
                {
                    psSeriesDict[sn].Add(time, psSeries);
                } else
                {
                    Dictionary<double, LineSeries> newDict = new Dictionary<double, LineSeries>();
                    newDict.Add(time, psSeries);
                    psSeriesDict.Add(sn, newDict);


                    // Add to map
                    bool added = false;
                    foreach (Tuple<PlotModel, List<Dictionary<double, LineSeries>>> rack in psSplit)
                    {
                        if (rack.Item2.Count > 4)
                        {
                            continue;
                        }
                        rack.Item2.Add(newDict);
                        added = true;
                    }
                    if (!added)
                    {
                        PlotModel newModel = new PlotModel() { IsLegendVisible = true };
                        Legend leg = new Legend() { LegendPosition = LegendPosition.TopRight };
                        newModel.Legends.Add(leg);
                        Tuple<PlotModel, List<Dictionary<double, LineSeries>>> rack = 
                            new Tuple<PlotModel, List<Dictionary<double, LineSeries>>>
                            (newModel, new List<Dictionary<double, LineSeries>>() {newDict});
                        psSplit.Add(rack);
                    }
                }

                // psHGM
                LineSeries sdevSeries = new LineSeries() { Title = results.Serial };
                int sdevB = 0;
                foreach (string bin in results.SdevHGM.Split(','))
                {
                    sdevSeries.Points.Add(new DataPoint(sdevB++, int.Parse(bin)));
                }
                if (sdevSeriesDict.ContainsKey(sn))
                {
                    sdevSeriesDict[sn].Add(time, sdevSeries);
                }
                else
                {
                    Dictionary<double, LineSeries> newDict = new Dictionary<double, LineSeries>();
                    newDict.Add(time, sdevSeries);
                    sdevSeriesDict.Add(sn, newDict);


                    // Add to map
                    bool added = false;
                    foreach (Tuple<PlotModel, List<Dictionary<double, LineSeries>>> rack in sdevSplit)
                    {
                        if (rack.Item2.Count > 4)
                        {
                            continue;
                        }
                        rack.Item2.Add(newDict);
                        added = true;
                    }
                    if (!added)
                    {
                        PlotModel newModel = new PlotModel() { IsLegendVisible = true };
                        Legend leg = new Legend() { LegendPosition = LegendPosition.TopRight };
                        newModel.Legends.Add(leg);

                        LogarithmicAxis yAx = new LogarithmicAxis();
                        newModel.Axes.Add(yAx);

                        Tuple<PlotModel, List<Dictionary<double, LineSeries>>> rack =
                            new Tuple<PlotModel, List<Dictionary<double, LineSeries>>>(newModel, new List<Dictionary<double, LineSeries>>() { newDict });
                        sdevSplit.Add(rack);
                    }


                }



                // voltage
                if (voltSeriesDict.ContainsKey(sn))
                {
                    voltSeriesDict[sn].Points.Add(new DataPoint(time, results.Voltage));
                } else
                {
                    LineSeries series = new LineSeries() 
                    {
                        Title=sn
                    };
                    series.Points.Add(new DataPoint(time, results.Voltage));
                    voltSeriesDict.Add(sn, series);

                    // Add to map
                    bool added = false;
                    foreach (Tuple<PlotModel, List<LineSeries>> rack in voltageSplit)
                    {
                        if (rack.Item2.Count > 4)
                        {
                            continue;
                        }
                        rack.Item2.Add(series);
                        rack.Item1.Series.Add(series);
                        added = true;
                    }
                    if (!added)
                    {
                        PlotModel newModel = new PlotModel() { IsLegendVisible = true };
                        DateTimeAxis voltX = new DateTimeAxis()
                        {
                            IsPanEnabled = false,
                            IsZoomEnabled = false
                        };
                        Legend leg = new Legend() { LegendPosition = LegendPosition.BottomRight};
                        newModel.Legends.Add(leg);
                        newModel.Axes.Add(voltX);
                        Tuple<PlotModel, List<LineSeries>> rack = new Tuple<PlotModel, List<LineSeries>>(newModel, new List<LineSeries>());
                        voltageSplit.Add(rack);
                        rack.Item2.Add(series);
                        rack.Item1.Series.Add(series);
                    }
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
                    // Add to map
                    bool added = false;
                    foreach (Tuple<PlotModel, List<LineSeries>> rack in tempSplit)
                    {
                        if (rack.Item2.Count > 4)
                        {
                            continue;
                        }
                        rack.Item2.Add(series);
                        rack.Item1.Series.Add(series);
                        added = true;
                    }
                    if (!added)
                    {
                        PlotModel newModel = new PlotModel() { IsLegendVisible = true};
                        DateTimeAxis voltX = new DateTimeAxis()
                        {
                            IsPanEnabled = false,
                            IsZoomEnabled = false
                        };
                        newModel.Axes.Add(voltX);
                        Tuple<PlotModel, List<LineSeries>> rack = new Tuple<PlotModel, List<LineSeries>>(newModel, new List<LineSeries>());
                        tempSplit.Add(rack);
                        Legend leg = new Legend() { LegendPosition = LegendPosition.BottomRight };
                        newModel.Legends.Add(leg);
                        rack.Item2.Add(series);
                        rack.Item1.Series.Add(series);
                    }
                }
                if (!gotFirst)
                {
                    cs215Series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(results.Time), results.Cs215Temp));
                    UpdateSlider(DateTimeAxis.ToDouble(results.Time));
                    gotFirst = true;
                } 
            }
            cs215Model.InvalidatePlot(true);
            UpdateCurPlots();
        }

        private void UpdateCurPlots()
        {
            if (voltageSplit.Count == 0) return;

            PlotModel voltPlot = voltageSplit[selIndex].Item1;
            PlotModel tempPlot = tempSplit[selIndex].Item1;
            
            if (varySelectBox.Text.Equals("Voltage"))
            {
                variablePlot.Model = voltPlot;
                voltPlot.InvalidatePlot(true);
            }
            else
            {
                variablePlot.Model = tempPlot;
                tempPlot.InvalidatePlot(true);
            }

            if (selTime == -1) return;

            if (hgmBox.Text.Equals("Pulse Sim"))
            {
                PlotModel curPSModel = psSplit[selIndex].Item1;
                curPSModel.Series.Clear();
                foreach (Dictionary<double, LineSeries> seriesDict in psSplit[selIndex].Item2)
                {
                    curPSModel.Series.Add(seriesDict[selTime]);
                }
                hgmPlot.Model = curPSModel;
                curPSModel.InvalidatePlot(true);
            } else
            {
                PlotModel curSDEVModel = sdevSplit[selIndex].Item1;
                curSDEVModel.Series.Clear();
                foreach (Dictionary<double, LineSeries> seriesDict in sdevSplit[selIndex].Item2)
                {
                    curSDEVModel.Series.Add(seriesDict[selTime]);
                }
                hgmPlot.Model = curSDEVModel;
                curSDEVModel.InvalidatePlot(true);
            }

        }

        private void UpdateSlider(double time)
        {
            sliderLine.Points.Clear();
            sliderLine.Points.Add(new DataPoint(time, -10));
            sliderLine.Points.Add(new DataPoint(time, 70));
            selTime = time;
        }

        private void HeatPlots_Load(object sender, EventArgs e)
        {
            varySelectBox.SelectedIndex = 0;
            hgmBox.SelectedIndex = 0;
            // cs215 plot
            cs215Model.Axes.Clear();

            DateTimeAxis cs215X = new DateTimeAxis()
            {
                IsPanEnabled = false,
                IsZoomEnabled = false
            };

            cs215Model.Axes.Add(cs215X);
            cs215Model.Axes.Add(cs215Y);
            cs215Model.Series.Add(cs215Series);
            cs215Model.Series.Add(sliderLine);
            cs215Plot.Model = cs215Model;


            cs215Model.MouseMove += RefSliderPos;

        }

        private void RefSliderPos(object sender, OxyMouseEventArgs ex)
        {
            if (MouseButtons != MouseButtons.Right) return;
            ex.Handled = true;
            ElementCollection<Axis> axisList = cs215Model.Axes;

            Axis xAxis = axisList.FirstOrDefault(ax => ax.Position == AxisPosition.Bottom);
            Axis yAxis = axisList.FirstOrDefault(ax => ax.Position == AxisPosition.Left);

            DataPoint dataPointp = Axis.InverseTransform(ex.Position, xAxis, yAxis);

            // find closest point
            double closestTime = 0;
            double minAbs = double.MaxValue;
            double mousePos = dataPointp.X;
            for (int i = 0; i < cs215Series.Points.Count; i++)
            {
                double calcAbs = Math.Abs(mousePos - cs215Series.Points[i].X);
                if (calcAbs < minAbs)
                {
                    minAbs = calcAbs;
                    closestTime = cs215Series.Points[i].X;
                }
            }

            UpdateSlider(closestTime);
            cs215Model.InvalidatePlot(true);
            UpdateCurPlots();
        }

        private void varySelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (voltageSplit.Count == 0) return;
            UpdateCurPlots();
        }

        private void varyBack_Click(object sender, EventArgs e)
        {
            selIndex--;
            if (selIndex < 0) selIndex = voltageSplit.Count - 1;
            UpdateCurPlots();
        }

        private void varyNext_Click(object sender, EventArgs e)
        {
            selIndex++;
            if (selIndex > voltageSplit.Count - 1) selIndex = 0;
            UpdateCurPlots();
        }

        private void hgmBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (voltageSplit.Count == 0) return;
            UpdateCurPlots();
        }
    }
}

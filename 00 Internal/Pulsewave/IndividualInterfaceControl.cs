using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pulsewave
{
    public partial class IndividualInterfaceControl : UserControl
    {
        private string com;
        private List<WaveData> dataChain = new List<WaveData>();
        private int begin = 0;

        private Terminal term;
        private SerialManager serialMan = new SerialManager();
        private Listener listener;

        private LineSeries hgmSeriesReal = new LineSeries();
        private LineSeries hgmSeriesGathered = new LineSeries();
        private PlotModel hgmModel = new PlotModel();

        private PlotModel chargeModel = new PlotModel();
        private LineSeries chargeSeries = new LineSeries();

        int nbins = 32;

        bool gotPulse = false;
        bool gotHgm = false;

        private Dictionary<string, PlotModel> modelDict = new Dictionary<string, PlotModel>()
        {
            { "plot00", new PlotModel() },
            { "plot01", new PlotModel() },
            { "plot02", new PlotModel() },
            { "plot10", new PlotModel() },
            { "plot11", new PlotModel() },
            { "plot12", new PlotModel() },
            { "plot20", new PlotModel() },
            { "plot21", new PlotModel() },
            { "plot22", new PlotModel() },
        };


        public IndividualInterfaceControl(string com)
        {
            listener = new Listener(this);
            this.com = com;
            InitializeComponent();
        }

        private void OpenTerminal(object sender, EventArgs e)
        {
            term.Show();
            term.Text = $"Terminal: {com}";
            term.Select();
        }

        internal void NewPSData(List<int> wave, string info)
        {
            foreach (int i in wave)
            {
                if (i > 3)
                {
                    WaveData newWaveData = new WaveData(wave, info);
                    dataChain.Add(newWaveData);

                    // invoke just to find bin first
                    newWaveData.GetSeries();
                    int bin = int.Parse(newWaveData.GetBin(nbins)) + 1;
                    hgmSeriesGathered.Points[bin] = new DataPoint(bin, hgmSeriesGathered.Points[bin].Y + 1);
                    hgmModel.InvalidatePlot(true);

                    // charge
                    int charge = newWaveData.GetCharge();

                    int count = chargeSeries.Points.Count;
                    while (charge + 1 > chargeSeries.Points.Count)
                    {
                        chargeSeries.Points.Add(new DataPoint(count, 0));
                        count++;
                    }
                    chargeSeries.Points[charge] = new DataPoint(charge, chargeSeries.Points[charge].Y + 1);

                    chargeModel.InvalidatePlot(true);
                    break;
                }
            }

            gotPulse = true;
            Invoke((MethodInvoker)delegate
            {
                manualBtn.Enabled = true;
                if (dataChain.Count > 9)
                {
                    slideFwdBtn.Enabled = true;
                }
            });
            RefreshCharts();
        }

        internal void NewHGMData(List<int> hgm, string info)
        {
            hgmSeriesReal.Points.Clear();
            gotHgm = true;
            for (int i = 1; i < hgm.Count+1; i++)
            {
                hgmSeriesReal.Points.Add(new DataPoint(i, hgm[i-1]));
            }
            hgmModel.InvalidatePlot(true);
        }

        private void RefreshCharts()
        {
            int y = -1;
            foreach (string plt in modelDict.Keys)
            {
                modelDict[plt].Series.Clear();
            }

            //for (int i = 0; i < nbins; i++)
            //{
            //    hgmSeriesGathered.Points.Add(new DataPoint(i, 0));
            //}
            //foreach (WaveData wave in dataChain)
            //{
            //    int bin = int.Parse(wave.GetBin(nbins));
            //    hgmSeriesGathered.Points[bin] = new DataPoint(bin, hgmSeriesGathered.Points[bin].Y + 1);
            //}

            for (int i = 0; i < dataChain.Count && i < 9; i++)
            {
                WaveData data = dataChain[i + begin];
                // where does this data display itself?
                // plots are in such a fashion
                // "plotxy"
                int x = i % 3;
                if (x == 0) y++;
                string plt = "plot" + x + y;

                // find control
                modelDict[plt].Series.Add(data.GetSeries());
                modelDict[plt].Series.Add(data.GetZeroSeries());
                modelDict[plt].InvalidatePlot(true);
                // refresh time label
                Panel p = (Panel)tableLayout.GetControlFromPosition(x, y);
                foreach (Label l in p.Controls.OfType<Label>())
                {
                    if (l.Name.Contains("time"))
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            l.Text = data.GetTime();
                        });
                    }
                    else
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            l.Text = "Bin: " + data.GetBin(nbins);
                        });
                    }
                }

            }
        }

        internal void NewData(string latest)
        {
            if (term == null) return;
            term.NewData(latest);
        }
        private void IndividualInterfaceControl_Load(object sender, EventArgs e)
        {
            // make charts assign models
            plot00.Model = modelDict["plot00"];
            plot10.Model = modelDict["plot10"];
            plot20.Model = modelDict["plot20"];
            plot01.Model = modelDict["plot01"];
            plot11.Model = modelDict["plot11"];
            plot21.Model = modelDict["plot21"];
            plot02.Model = modelDict["plot02"];
            plot12.Model = modelDict["plot12"];
            plot22.Model = modelDict["plot22"];

            foreach (PlotModel model in modelDict.Values)
            {
                model.Axes.Add(new LinearAxis() 
                { 
                    Minimum = -300,
                    Maximum = 2047,
                    MajorStep = 600,
                    Position = AxisPosition.Left
                }
                );
            }

            hgmPlot.Model = hgmModel;
            hgmModel.Series.Add(hgmSeriesReal);
            hgmModel.Series.Add(hgmSeriesGathered);

            for (int i = 1; i < nbins + 1; i++)
            {
                hgmSeriesGathered.Points.Add(new DataPoint(i, 0));
            }


            chargePlot.Model = chargeModel;
            chargeModel.Series.Add(chargeSeries);


            // connect on load
            if (serialMan.Connect(com))
            {
                connectBtn.Text = "Disconnect";
                conStatus.Text = "Connected";
                conStatus.ForeColor = Color.Green;
                termBtn.Enabled = true;

                serialMan.LinkTerm(listener);
                serialMan.SendCommand("zerohgm\r\n");

            }
            term = new Terminal(serialMan);
            term.Show();
            term.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            manualBtn.Enabled = false;
            serialMan.SendCommand("pulsewave\r\n");
        }

        private void slideFwdBtn_Click(object sender, EventArgs e)
        {
            if (begin > dataChain.Count - 10) return;
            begin++;
            if (begin > 0) slideBackBtn.Enabled = true;
            RefreshCharts();
        }

        private void slideBackBtn_Click(object sender, EventArgs e)
        {
            begin--;
            if (begin == 0) slideBackBtn.Enabled = false;
            RefreshCharts();
        }

        private void runColBtn_Click(object sender, EventArgs e)
        {
            if (colWorker.IsBusy)
            {
                colWorker.CancelAsync();
                runColBtn.Text = "Run Collection";
            }
            else
            {
                serialMan.SendCommand("zerohgm\r\n");
                runColBtn.Text = "Stop";

                progressBar.Value = 0;
                int waves = int.Parse(numWavesBox.Text);
                int secs = int.Parse(timeCombo.Text);

                progressBar.Maximum = waves;
                colWorker.RunWorkerAsync(new Tuple<int, int>(waves, secs));
            }

        }


        private void RunCollection(object sender, DoWorkEventArgs e)
        {
            Tuple<int, int> args = (Tuple<int, int>)e.Argument;
            int waveCount = args.Item1;
            int seconds = args.Item2;
            double wait = ((double)seconds / waveCount) * 1000;
            int req = 0;

            Stopwatch timer = Stopwatch.StartNew();
            while (!colWorker.CancellationPending)
            {
                if (timer.ElapsedMilliseconds > wait)
                {
                    req++;
                    timer.Restart();
                    colWorker.ReportProgress(req);
                    serialMan.SendCommand("pulsewave\r\n");
                    // await response
                    while (!gotPulse)
                    {
                        Thread.Sleep(5);
                        Application.DoEvents();
                    }
                    serialMan.SendCommand("hgm\r\n");
                    while (!gotHgm)
                    {
                        Thread.Sleep(5);
                        Application.DoEvents();
                    }
                    gotPulse = false;
                    gotHgm = false;

                }
                if (req == waveCount) return;
                Thread.Sleep(10);

            }
        }

        private void UpdateProgress(object sender, ProgressChangedEventArgs e)
        {
            
            progressBar.Value++;
        }

        private void OnCollectionRepeat(object sender, RunWorkerCompletedEventArgs e)
        {
            runColBtn.Enabled = true;
        }
    }
}

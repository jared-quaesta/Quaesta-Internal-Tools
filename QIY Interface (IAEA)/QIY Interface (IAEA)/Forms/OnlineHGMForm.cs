using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIY_Interface__IAEA_
{
    public partial class OnlineHGMForm : Form
    {
        PlotModel hgmModel = new PlotModel() { Title = "Histogram (Pulse Height Spectrum)" };
        LineSeries hgmSeries = new LineSeries() { Title = "Histogram" };
        TCPManager tcpMan;
        BackgroundWorker worker = new BackgroundWorker()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };


        internal OnlineHGMForm(TCPManager tcpMan)
        {
            this.tcpMan = tcpMan;
            InitializeComponent();
        }

        private void OnlineHGMForm_Load(object sender, EventArgs e)
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

            hgmPlot.Model = hgmModel;
            hgmModel.Series.Add(hgmSeries);
            refCB.SelectedIndex = 2;
            ipBox.Text = tcpMan.GetIP();
            nameBox.Text = tcpMan.GetName();
            //Text = $"{tcpMan.GetIP()} Live Histogram View";

            worker.DoWork += (s,e) =>
            {
                Stopwatch sw;
                while (!worker.CancellationPending)
                {
                    sw = Stopwatch.StartNew();
                    while (sw.ElapsedMilliseconds < (int)e.Argument * 1000)
                    {
                        if (worker.CancellationPending) return;
                        Thread.Sleep(10);
                    }
                    tcpMan.NewCmd("hgm\r\n");
                }
            };
        }

        private void HideForm(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            tcpMan.NewCmd("hgm\r\n");
        }

        internal void IncomingHGM(string hgm)
        {
            if (!hgm.ToLower().Contains("hgm")) return;
            hgmSeries.Points.Clear();
            if (tcpMan.hgmMode == -1)
            {
                MessageBox.Show("Unknown HGM Mode, please query the device.");
                return;
            }
            else if (tcpMan.hgmMode == 1 || tcpMan.hgmMode == 2 || tcpMan.hgmMode == 3)
            {
                string[] splitHgm = hgm.Split('\n');
                int bin = 0;
                for (int i = 0; i < splitHgm.Length; i++)
                {
                    if (splitHgm[i].Trim('\n','\r',' ').Length == 0) continue;
                    if (splitHgm[i].Trim('\n','\r',' ', '}', '{').Length == 0) continue;
                    if (splitHgm[i].ToLower().Contains("hgm"))
                        continue;
                    else if (splitHgm[i].ToLower().Contains("total"))
                    {
                        string splitOnEquals = splitHgm[i].Split('=')[1];
                        totalCntsBox.Text = splitOnEquals.Split(',')[0];
                        elapsedBox.Text = splitOnEquals.Split(',')[1].Trim('\n', '\r', ' ') + " Seconds";
                    }
                    else
                    {
                        if (tcpMan.hgmMode == 2)
                        {
                            string index = splitHgm[i].Trim().Split(' ')[0];
                            string val = splitHgm[i].Trim().Split(' ')[1];

                            if (int.TryParse(index, out int binI) && int.TryParse(val, out int valI))
                            {
                                hgmSeries.Points.Add(new DataPoint(binI, valI));
                            }
                        }
                        else
                        {
                            if (int.TryParse(splitHgm[i], out int val))
                            {
                                hgmSeries.Points.Add(new DataPoint(bin, val));
                                bin++;
                            }
                        }
                    }
                }
            }
            else
            {
                string hgmLine = "";
                foreach (string line in hgm.Split('\n'))
                {
                    if (line.Contains("Total")) hgmLine = line.Trim('\n', '\r', ' ');
                }
                if (hgmLine.Length == 0) return;

                string info = hgmLine.Split(';')[1];
                string[] hgmpart = hgmLine.Split(';')[0].Split(',');

                // total
                string splitOnEquals = info.Split('=')[1];
                totalCntsBox.Text = splitOnEquals.Split(',')[0];
                elapsedBox.Text = splitOnEquals.Split(',')[1] + " Seconds";

                // into bins
                for (int bin = 0; bin < hgmpart.Length; bin++)
                {
                    if (int.TryParse(hgmpart[bin], out int val))
                    {
                        hgmSeries.Points.Add(new DataPoint(bin, val));
                    }
                }

            }
            hgmModel.InvalidatePlot(true);

        }

        private void zeroBtn_Click(object sender, EventArgs e)
        {
            tcpMan.NewCmd("zerohgm\r\n");
            tcpMan.NewCmd("hgm\r\n");
        }

        private void queryCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (queryCheck.Checked)
            {
                worker.RunWorkerAsync(int.Parse(refCB.SelectedItem.ToString()));
            }
            else
            {
                worker.CancelAsync();
                while (worker.IsBusy)
                {
                    Thread.Sleep(10);
                    Application.DoEvents();
                }
            }
        }

        private void refCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
                while (worker.IsBusy)
                {
                    Thread.Sleep(10);
                    Application.DoEvents();
                }
                worker.RunWorkerAsync(int.Parse(refCB.SelectedItem.ToString()));
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            hgmSeries.Points.Clear();
            hgmModel.InvalidatePlot(true);
        }

        private void expBtn_Click(object sender, EventArgs e)
        {
            if (hgmSeries.Points.Count == 0)
            {
                MessageBox.Show("Nothing to capture", "ERROR");
                return;
            }
            if (ConfigurationManager.AppSettings["HGMPATH"].Length > 0)
            {
                saveFileDialog.InitialDirectory = ConfigurationManager.AppSettings["HGMPATH"];
            }
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.FileName = $"CAPTURE_{nameBox.Text}_{ipBox.Text.Replace('.', '-')}.csv";

            DialogResult dialogRes = saveFileDialog.ShowDialog();
            if (dialogRes == DialogResult.Cancel || dialogRes == DialogResult.Abort) return;
            try
            {
                string toWrite = $"//{ipBox.Text}{Environment.NewLine}" +
                    $"//{nameBox.Text}{Environment.NewLine}" +
                    $"//Time Captured: {DateTime.Now.ToString("G")}{Environment.NewLine}" +
                    $"//Total Counts: {totalCntsBox.Text}{Environment.NewLine}" +
                    $"//Time Elapsed: {elapsedBox.Text}{Environment.NewLine}";

                foreach (DataPoint dp in hgmSeries.Points)
                {
                    toWrite += dp.Y.ToString() + ",";
                }
                toWrite.Trim(',');

                using (var tw = new StreamWriter(saveFileDialog.FileName, true))
                {
                    tw.Write(toWrite);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
            }


        }
    }
}

using OxyPlot;
using OxyPlot.Series;
using QIXLPTesting.SerialTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIXLPTesting
{
    class Tests
    {
        public bool errOccurred = false;
        public double averageVoltage = -1;
        public int maxSpread = 0;

        public IEnumerable<DataPoint> VoltageTest(SerialNPMManager serialMan, int testVoltage, int range, int wait, bool rampDown)
        {
            Stopwatch watch = Stopwatch.StartNew();

            serialMan.ClearInput();
            serialMan.listener.ClearVoltage();
            serialMan.SendCommand($"maxvoltage = {testVoltage}\r\n");
            Thread.Sleep(50);
            serialMan.SendCommand($"voltage = {testVoltage}\r\n");
            Thread.Sleep(50);
            int aveCount = 0;
            while (watch.ElapsedMilliseconds / 1000 < wait)
            {
                double volts = GetVoltage(serialMan);
                averageVoltage += volts;
                aveCount++;
                yield return new DataPoint(watch.ElapsedMilliseconds / 1000, volts);
            }
            if (rampDown)
            {
                Thread.Sleep(50);
                serialMan.SendCommand($"voltage = 0\r\n");
                Thread.Sleep(50);
                watch.Restart();
                while (watch.ElapsedMilliseconds / 1000 < wait)
                {
                    yield return new DataPoint((watch.ElapsedMilliseconds + (wait * 1000)) / 1000, GetVoltage(serialMan));
                }
            }
            averageVoltage /= aveCount;
            if (averageVoltage > range + testVoltage || averageVoltage < range - testVoltage)
                errOccurred = true;

            //return new Tuple<LineSeries, bool>(series, err);
        }


        internal IEnumerable<LineSeries> PulseSimTest(SerialNPMManager serialMan, double gain, int waitSec, int range, int discLow, int discHigh, int nbins)
        {
            // set initial parameters

            serialMan.ClearInput();
            serialMan.listener.ClearVoltage();
            serialMan.SendCommand($"gain = {gain}\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"nbins={nbins}\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"disclow={discLow}\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"dischigh={discHigh}\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"ledmode = 1\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"pulsesim=1\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"hgmmode=1\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"zerohgm\r\n");
            Thread.Sleep(30);

            Stopwatch watch = Stopwatch.StartNew();

            while (watch.ElapsedMilliseconds / 1000 < waitSec)
            {
                LineSeries ls = GetHGM(serialMan, range);
                yield return ls;
            }


        }

        private LineSeries GetHGM(SerialNPMManager serialMan, int range)
        {
            serialMan.listener.ClearHGM();
            serialMan.ClearInput();
            Thread.Sleep(30);
            serialMan.SendCommand("hgm\r\n");
            Thread.Sleep(30);

            LineSeries series = new LineSeries() {Title = serialMan.GetSerial() };

            List<int> hgm = serialMan.listener.GetHGM();
            int spread = DetermineSpread(hgm);
            if (spread > range) errOccurred = true;
            if (spread > maxSpread) maxSpread = spread;
            for (int i = 0; i < hgm.Count; i++)
            {
                series.Points.Add(new DataPoint(i, hgm[i]));
            }
            return series;

        }

        private int DetermineSpread(List<int> hgm)
        {
            int spread = 0;
            int first = 0;
            int last = 0;
            for (int i = 0; i < hgm.Count; i++)
            {
                if (first == 0 && hgm[i] != 0)
                {
                    first = i;
                }
                if (hgm[i] != 0)
                {
                    last = i;
                }
            }
            return last - first;
        }

        private double GetVoltage(SerialNPMManager serialMan)
        {
            serialMan.listener.ClearVoltage();
            serialMan.ClearInput();
            serialMan.SendCommand($"voltage\r\n");
            Thread.Sleep(50);
            serialMan.SendCommand($"voltage\r\n");
            Thread.Sleep(50);

            // get first voltage
            string initVolts = serialMan.listener.GetVoltage().Replace("Measured/Set:", "M/S:");
            double initMeas = -1;
            if (initVolts.Split(' ').Length < 2)
            {
                return -1;
            }
            else if (!double.TryParse(initVolts.Split(' ')[1].Split('/')[0].Trim(), out initMeas))
            {
                return -1;
            }

            else return initMeas;

        }

        internal static void SetupLEDTest(SerialNPMManager serialMan)
        {
            serialMan.ClearInput();
            serialMan.listener.ClearVoltage();
            serialMan.SendCommand($"pulsesim = 0\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"ledmode = 0\r\n");
            Thread.Sleep(30);
        }

        internal static bool BlinkTest(SerialNPMManager serialMan)
        {
            serialMan.ClearInput();
            serialMan.listener.ClearVoltage();
            serialMan.SendCommand($"pulsesim = 1\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"ledmode = 1\r\n");
            Thread.Sleep(30);

            bool ret =  MessageBox.Show("Blinking?", serialMan.GetSerial(), MessageBoxButtons.YesNo) == DialogResult.Yes;

            SetupLEDTest(serialMan);


            return ret;
        }


    }
}

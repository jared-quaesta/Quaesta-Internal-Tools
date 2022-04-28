using GeneralFirstPhase.Data;
using OxyPlot;
using OxyPlot.Series;
using QIXLPTesting.SerialTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIXLPTesting
{
    class Tests
    {
        public bool errOccurred = false;
        public double averageVoltage = -1;
        public double averageT = -1;
        public int maxSpread = 0;
        public int maxBin = 0;

        static char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

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

        internal static async Task<bool> SetupSDI(List<SerialNPMManager> serialMans, bool disconnectAfter = true)
        {
            bool err = false;
            int sdi = 0;
            List<Thread> threads = new List<Thread>();
            foreach (SerialNPMManager serialMan in serialMans)
            {
                char sdiAddress = alphabet[sdi];
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    // connect
                    int att = 0;
                    while (!serialMan.Connect(serialMan.com))
                    {
                        Thread.Sleep(30);
                        if (att++ == 10) break;
                    }
                    if (!serialMan.IsConnected()) 
                    {
                        MessageBox.Show("Error in SDI Setup. Make sure all devices can connect.");
                        err = true;
                        return; 
                    }

                    // set sdi
                    serialMan.SendCommand($"localaddress={sdiAddress}\r\n");
                    Thread.Sleep(30);
                    serialMan.SendCommand($"localaddress={sdiAddress}\r\n");
                    Thread.Sleep(30);
                    serialMan.SetSDI(sdiAddress);
                    // disconnect
                    if (disconnectAfter)
                        serialMan.Disconnect();
                });
                sdi++;
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);
            }

            await Task.Run(() =>
            {
                foreach (var thread in threads)
                {
                    thread.Join();
                }
            });
            return err;
        }

        internal IEnumerable<int[]> SdevTest(SerialNPMManager serialMan, int waitSec, int voltage, int minBin)
        {
            serialMan.ClearInput();
            serialMan.listener.ClearVoltage();
            serialMan.SendCommand($"gain = 25.5\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"maxvoltage={voltage}\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"voltage={voltage}\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"nbins=64\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"disclow=2\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"dischigh=63\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"ledmode = 1\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"pulsesim=0\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"hgmmode=1\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"zerohgm\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"zerohgm\r\n");
            Thread.Sleep(30);

            Stopwatch watch = Stopwatch.StartNew();

            while (watch.ElapsedMilliseconds / 1000 < waitSec)
            {
                int[] ls = GetHGMSdev(serialMan, minBin);
                yield return ls;
            }

        }

        private int[] GetHGMSdev(SerialNPMManager serialMan, int minBin)
        {
            serialMan.listener.ClearHGM();
            serialMan.ClearInput();
            Thread.Sleep(30);
            serialMan.SendCommand("hgm\r\n");
            Thread.Sleep(30);

            List<int> hgm = serialMan.listener.GetHGM();

            // find max bin with counts

            for (int i = 0; i < hgm.Count; i++)
            {
                if (hgm[i] > 0) maxBin = i;
            }
            if (maxBin > minBin) errOccurred = true;
            return hgm.ToArray();

        }

        internal IEnumerable<int[]> PulseSimTest(SerialNPMManager serialMan, double gain, int waitSec, int range, int discLow, int discHigh, int nbins)
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
            serialMan.SendCommand($"zerohgm\r\n");
            Thread.Sleep(30);

            Stopwatch watch = Stopwatch.StartNew();

            while (watch.ElapsedMilliseconds / 1000 < waitSec)
            {
                int[] ls = GetHGMPS(serialMan, range);
                yield return ls;
            }


        }

        private int[] GetHGMPS(SerialNPMManager serialMan, int range)
        {
            serialMan.listener.ClearHGM();
            serialMan.ClearInput();
            Thread.Sleep(30);
            serialMan.SendCommand("hgm\r\n");
            Thread.Sleep(30);

            LineSeries series = new LineSeries() { Title = serialMan.GetSerial() };

            List<int> hgm = serialMan.listener.GetHGM();
            int spread = DetermineSpread(hgm);
            if (spread > range) errOccurred = true;
            if (spread > maxSpread) maxSpread = spread;
            return hgm.ToArray();

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

        internal static bool? BlinkTest(SerialNPMManager serialMan)
        {
            serialMan.ClearInput();
            serialMan.listener.ClearVoltage();
            serialMan.SendCommand($"pulsesim = 1\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"ledmode = 1\r\n");
            Thread.Sleep(30);
            DialogResult res = MessageBox.Show("Blinking?", serialMan.GetSerial(), MessageBoxButtons.YesNoCancel);
            Debug.WriteLine(res.ToString());

            SetupLEDTest(serialMan);
            if (res == DialogResult.Cancel)
            {
                return null;
            }
            else return res == DialogResult.Yes;

        }

        internal IEnumerable<DataPoint> TemperatureTest(SerialNPMManager serialMan, double minT, double maxT, int waitSec)
        {

            Stopwatch watch = Stopwatch.StartNew();
            int aveCount = 0;
            while (watch.ElapsedMilliseconds / 1000 < waitSec)
            {
                double temp = GetTemp(serialMan);
                averageT += temp;
                aveCount++;
                yield return new DataPoint(watch.ElapsedMilliseconds / 1000, temp);
            }
            averageT /= aveCount;

        }

        private double GetTemp(SerialNPMManager serialMan)
        {
            serialMan.listener.ClearTemperature();
            serialMan.ClearInput();
            serialMan.SendCommand($"temperature\r\n");
            Thread.Sleep(50);
            return serialMan.listener.GetTemperature();
        }

        internal static void SetupHeaterTest(List<SerialNPMManager> serialMans, int voltage)
        {
            List<Thread> threads = new List<Thread>();
            foreach (SerialNPMManager serialMan in serialMans)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    // connect
                    serialMan.SendCommand($"voltage = {voltage}\r\n");
                    Thread.Sleep(30);
                    serialMan.SendCommand($"voltage = {voltage}\r\n");
                    Thread.Sleep(30);
                    serialMan.SendCommand($"nbins=64\r\n");
                    Thread.Sleep(30);
                    serialMan.SendCommand($"nbins=64\r\n");
                    Thread.Sleep(30);
                    serialMan.SendCommand($"gain=25.5\r\n");
                    Thread.Sleep(30);
                    serialMan.SendCommand($"gain=25.5\r\n");
                    Thread.Sleep(30);
                    serialMan.SendCommand($"zerohgm\r\n");
                    Thread.Sleep(30);
                    serialMan.SendCommand($"zerohgm\r\n");
                    Thread.Sleep(30);
                    serialMan.SendCommand($"pulsesim=0\r\n");
                    Thread.Sleep(30);
                    serialMan.SendCommand($"pulsesim=0\r\n");
                    Thread.Sleep(30);
                });
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        internal static HeaterDataResults GetHeaterData(SerialNPMManager serialMan, double psGain, int voltRange, int voltage, int sdevMaxBin, int psBinRange)
        {
            HeaterDataResults res = new HeaterDataResults();

            // get sdev hgm
            serialMan.ClearInput();
            Thread.Sleep(30);
            serialMan.listener.ClearHGM();
            serialMan.SendCommand("hgm\r\n");
            Thread.Sleep(30);
            List<int> hgm = serialMan.listener.GetHGM();
            if (hgm.Count != 64)
            {
                hgm.Clear();
                for (int i = 0; i < 64; i++)
                {
                    hgm.Add(0);
                }
            }
            res.SdevHGM = string.Join(",",hgm.GetRange(0, 64));

            // get voltage
            serialMan.ClearInput();
            Thread.Sleep(30);
            serialMan.SendCommand($"voltage\r\n");
            Thread.Sleep(50);
            serialMan.listener.ClearVoltage();
            serialMan.SendCommand($"voltage\r\n");
            Thread.Sleep(50);

            string initVolts = serialMan.listener.GetVoltage().Replace("Measured/Set:", "M/S:");
            double initMeas = -1;
            if (initVolts.Split(' ').Length < 2)
            {
                res.Voltage = -1;
            }
            else if (!double.TryParse(initVolts.Split(' ')[1].Split('/')[0].Trim(), out initMeas))
            {
                res.Voltage = -1;
            }
            else
            {
                res.Voltage = initMeas;
            }

            // get temp
            serialMan.listener.ClearTemperature();
            serialMan.ClearInput();
            Thread.Sleep(30);
            serialMan.SendCommand($"temperature\r\n");
            Thread.Sleep(100);
            res.NpmTemp = (int)serialMan.listener.GetTemperature();

            // zero hgm and begin ps test
            serialMan.SendCommand($"gain={psGain}\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"gain={psGain}\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand("zerohgm\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand("zerohgm\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"pulsesim=1\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"pulsesim=1\r\n");
            Thread.Sleep(10000);

            serialMan.ClearInput();
            Thread.Sleep(30);
            serialMan.listener.ClearHGM();
            serialMan.SendCommand("hgm\r\n");
            Thread.Sleep(30);
            List<int> sdevhgm = serialMan.listener.GetHGM();
            if (sdevhgm.Count != 64)
            {
                sdevhgm.Clear();
                for (int i = 0; i < 64; i++)
                {
                    sdevhgm.Add(0);
                }
            }
            res.PsHGM = string.Join(",", sdevhgm.GetRange(0,64));

            res.Serial = serialMan.GetSerial();
            serialMan.SendCommand($"pulsesim=0\r\n");
            Thread.Sleep(30);
            serialMan.SendCommand($"pulsesim=0\r\n");
            return res;
        }
    }
}

using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsewave
{
    class WaveData
    {
        public List<int> wave;
        public string info;
        LineSeries zeroSeries = new LineSeries();
        DateTime collected = DateTime.Now;
        double maxInterpolatedX = 0;
        double maxInterpolatedY = 0;
        int dataBeforeZero = 0;
        int charge = 0;

        public WaveData(List<int> wave, string info)
        {
            this.wave = wave;
            this.info = info;
            
        }

        public LineSeries GetSeries()
        {
            LineSeries series = new LineSeries();
            List<DataPoint> points = new List<DataPoint>();
            for (int i = 0; i < wave.Count; i++)
            {
                // Swap to show interpolation v
                //zeroSeries.Points.Add(new DataPoint(i, wave[i]));
                zeroSeries.Points.Add(new DataPoint(i, 0));
                points.Add(new DataPoint(i, wave[i]));

                if (wave[i] > 0) charge += wave[i];
            }

            List<DataPoint> interpPoints = InterpolationAlgorithms.CanonicalSpline.CreateSpline(points, false, .1);

            series.Points.AddRange(interpPoints);

            // find max
            foreach (DataPoint p in interpPoints)
            {
                if (p.Y > maxInterpolatedY)
                {
                    maxInterpolatedY = p.Y;
                    maxInterpolatedX = p.X;
                }
            }
            return series;
        }

        internal Series GetZeroSeries()
        {
            return zeroSeries;
        }
        internal string GetTime()
        {
            return collected.ToString("mm:ss");
        }

        internal int GetCharge()
        {
            return charge/1000;
        }

        internal string GetBin(int nBins)
        {
            int ret = 0;
            // find pos in data
            double binPercent = maxInterpolatedY / 2048;
            ret = (int)(binPercent * (nBins-1));

            return ret.ToString();
        }
    }
}

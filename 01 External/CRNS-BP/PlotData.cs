using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRNS_BP
{
    class PlotData
    {
        internal string title;
        internal List<Tuple<double, double>> data = new List<Tuple<double, double>>();
        public PlotData(string title)
        {
            this.title = title;
        }

        public void AddData(double record, double val)
        {
            data.Add(new Tuple<double, double>(record, val));
        }
    }
}

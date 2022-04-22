using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRNS_BP
{
    class AdjustedRV2Block
    {
        public RV2Block og;
        public double sumCounts;
        public AdjustedRV2Block(RV2Block og, double adjCount)
        {
            this.og = og;
            sumCounts = adjCount;
            
        }

        public double[] GetPosition()
        {
            return og.GetPosition();
        }
    }
}

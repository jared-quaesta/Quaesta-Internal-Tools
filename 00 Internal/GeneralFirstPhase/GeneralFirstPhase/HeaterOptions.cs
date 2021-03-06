using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralFirstPhase
{
    public partial class HeaterOptions : Form
    {
        public HeaterOptions()
        {
            InitializeComponent();
        }

        private void HeaterOptions_Load(object sender, EventArgs e)
        {

        }

        internal int GetHeatVolts()
        {
            return int.Parse(voltLevel.Text);
        }
        internal int GetQueryTime()
        {
            return int.Parse(timeBox.Text);
        }
        
        internal int GetNoiseFloor()
        {
            return int.Parse(noiseFloor.Text);
        }

        internal int GetHeatVoltRange()
        {
            return int.Parse(validVoltRange.Text);
        }

        internal double GetHeatGain()
        {
            return double.Parse(gainBox.Text);
        }

        internal int GetPSBinRange()
        {
            return int.Parse(psValid.Text);
        }
        internal int GetPSCenter()
        {
            return int.Parse(center.Text) * 4;
        }
        internal int GetPSCenterSpread()
        {
            return int.Parse(centerSpread.Text);
        }

        internal int GetMaximumBin()
        {
            return int.Parse(minBinBox.Text);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

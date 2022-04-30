using GeneralFirstPhase.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralFirstPhase.Charting
{
    public partial class HeatTestPlotView : Form
    {
        string type;
        string serial;
        public HeatTestPlotView(string type, string serial)
        {
            this.type = type;
            this.serial = serial;
            InitializeComponent();
        }

        private void HeatTestPlotView_Load(object sender, EventArgs e)
        {

        }

        internal void ShowPlots()
        {
            if (type.Equals("voltage"))
            {
                // [date, cs215, voltage]
                List<Tuple<DateTime, int, double>> data = SQLManager.GetHeatVoltage(serial);

                // setup voltage plots




            }

        }

    }
}

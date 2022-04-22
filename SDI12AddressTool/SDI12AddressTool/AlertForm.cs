using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDI12AddressTool
{
    public partial class AlertForm : Form
    {
        internal bool isOkay = false;

        public AlertForm()
        {
            InitializeComponent();
        }

        private void no_Click(object sender, EventArgs e)
        {
            isOkay = false;
            Close();
        }

        private void yes_Click(object sender, EventArgs e)
        {
            isOkay = true;
            Close();
        }
    }
}

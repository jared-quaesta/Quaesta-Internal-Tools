using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pulsewave
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // discover devices on open
            RefreshTabs();
        }

        private void RefreshTabs()
        {
            List<string> coms = SerialManager.GetComs();
            foreach (string com in coms)
            {
                TabPage newPage = new TabPage(com);
                newPage.Controls.Add(new IndividualInterfaceControl(com) { Dock = DockStyle.Fill });
                tabControl.Controls.Add(newPage);
            }
        }
    }
}

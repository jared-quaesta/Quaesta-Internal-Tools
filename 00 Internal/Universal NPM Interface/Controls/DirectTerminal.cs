using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Universal_NPM_Interface.Serial;

namespace Universal_NPM_Interface.Controls
{
    public partial class DirectTerminal : UserControl
    {
        SerialNPMManager serialMan;
        internal DirectTerminal(SerialNPMManager serialMan)
        {
            this.serialMan = serialMan;
            InitializeComponent();
        }

        private void DirectTerminal_Load(object sender, EventArgs e)
        {

        }

        internal void NewData(string data)
        {
            Invoke((MethodInvoker)delegate
            {
                termOut.AppendText(data);
                termOut.SelectionStart = termOut.TextLength;
                termOut.ScrollToCaret();
            });
        }

        private void SendCommand(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                serialMan.SendCommand(termIn.Text + "\r\n");
                termIn.Text = "";
                e.Handled = true;
            }
        }
    }
}

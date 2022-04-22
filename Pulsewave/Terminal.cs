using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pulsewave
{
    public partial class Terminal : Form
    {
        SerialManager man;
        internal Terminal(SerialManager man)
        {
            this.man = man;
            InitializeComponent();
        }

        private void HideForm(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        public void NewData(string data)
        {
            Invoke((MethodInvoker)delegate
            {
                termOut.AppendText(data);

                //if (termOut.Text.Length > 10000) termOut.Text = termOut.Text.Substring(termOut.Text.Length - 10000);
                termOut.SelectionStart = termOut.TextLength;
                termOut.ScrollToCaret();
                Refresh();
            });
        }

        private void SendCommand(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                man.SendCommand(termIn.Text + "\r\n");
                termIn.Clear();
                e.SuppressKeyPress = true;
            }
        }
    }
}

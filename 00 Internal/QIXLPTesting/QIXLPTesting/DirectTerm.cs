using QIXLPTesting.SerialTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIXLPTesting
{
    public partial class DirectTerm : Form
    {
        SerialNPMManager man;
        public DirectTerm(string com, string serial)
        {
            man = new SerialNPMManager(serial, com);
            InitializeComponent();
        }

        private void DirectTerm_Load(object sender, EventArgs e)
        {
            man.SetTerm(this);

            int att = 0;
            while (att < 10 && !man.IsConnected())
            {
                man.Connect(man.com);
                Thread.Sleep(200);
                att++;
            }


            termOut.Text = "Connected: " + man.IsConnected();
        }

        private void SendCmd(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            man.SendCommand(termIn.Text + "\r\n");
            termIn.Clear();
        }

        internal void AddData(string data)
        {
            Invoke((MethodInvoker)delegate
            {

                string dataRep = data.Replace("\n", "/n\n").Replace("\r", "/r");

                termOut.AppendText(dataRep);
                termOut.SelectionStart = termOut.TextLength;
                termOut.ScrollToCaret();
                termOut.Refresh();
            });
        }

        private void DisconnectDereference(object sender, FormClosingEventArgs e)
        {
            man.Disconnect();
        }
    }
}

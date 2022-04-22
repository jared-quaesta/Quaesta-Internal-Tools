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

namespace QIY_Interface__IAEA_
{
    public partial class TerminalForm : Form
    {
        private TCPManager tcpMan;
        private MainForm main;
        private List<string> cmdhist = new List<string>();
        private int histindex = 0;

        internal TerminalForm(MainForm main)
        {
            this.main = main;
            InitializeComponent();
        }


        private void TerminalForm_Load(object sender, EventArgs e)
        {

        }

        internal void NewData(string inData)
        {
            //termOut.Focus();
            foreach (string line in inData.Split('\n'))
            {
                string trimline = line.Trim(Convert.ToChar(0)) + '\n';
                if (showBytes.Checked)
                {
                    trimline = trimline.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\n", "\\n");
                }

                termOut.AppendText($"{trimline.Trim('\r', '\n')}{Environment.NewLine}");
                
                
            }
            termOut.SelectionStart = termOut.TextLength;
            termOut.ScrollToCaret();
            termOut.Refresh();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            //if (termIn.Text.Length == 0) return;
            main.NewTermCmd(termIn.Text + "\r\n");
            cmdhist.Insert(0,termIn.Text);
            lastCom.Text = termIn.Text;
            histindex = 0;
            termIn.Clear();
        }

        private void SendCmd(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendBtn_Click(null, null);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Up && cmdhist.Count-1 > histindex)
            {
                termIn.Text = cmdhist[histindex];
                histindex++;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (histindex == 0)
                {
                    termIn.Text = "";
                }
                else
                {
                    termIn.Text = cmdhist[histindex];
                    histindex--;
                }
            }

        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            termOut.Clear();
            termOut.Refresh();
        }

        private void HideMe(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void Escape(object sender, EventArgs e)
        {
            main.ImmEscapeChar();
        }

        private void ResizeTermWindow(object sender, EventArgs e)
        {

        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            termOut.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            termOut.Font = new Font("Courier New", 11F, FontStyle.Regular, GraphicsUnit.Point);
        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            termOut.Font = new Font("Courier New", 14F, FontStyle.Regular, GraphicsUnit.Point);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lastCom_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void termIn_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void showBytes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void outputTextSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

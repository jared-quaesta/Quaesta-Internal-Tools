using GeneralFirstPhase.SerialTools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralFirstPhase
{
    public partial class DirectTerm : Form
    {
        SerialNPMManager man;
        internal DirectTerm(SerialNPMManager man)
        {
            this.man = man;
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
            descLbl.Text = $"{man.GetSerial()} | {man.com}";
            CheckConnection();
        }

        private void CheckConnection()
        {
            if (man.IsConnected())
            {
                connectedLbl.Text = "Connected";
                connectedLbl.ForeColor = Color.Green;
                connectBtn.Text = "Disconnect";
            }
            else
            {
                connectedLbl.Text = "Disconnected";
                connectedLbl.ForeColor = Color.Red;
                connectBtn.Text = "Connect";
            }
        }

        private void SendCmd(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                man.SendCommand(termIn.Text + "\r\n");
                termIn.Clear();
            }

        }

        internal void AddData(string data)
        {
            string dataRep = data;
            if (showCrLfCheck.Checked)
                dataRep = data.Replace("\n", "/n\n").Replace("\r", "/r");

            termOut.AppendText(dataRep);
            termOut.SelectionStart = termOut.TextLength;
            termOut.ScrollToCaret();
            termOut.Refresh();

        }

        private async void DisconnectDereference(object sender, FormClosingEventArgs e)
        {
            debugWorker.CancelAsync();
            await Task.Run(() =>
            {
                while (debugWorker.IsBusy)
                {
                    Thread.Sleep(100);
                }
            });
            man.Disconnect();
        }

        private async void runDebugBtn_ClickAsync(object sender, EventArgs e)
        {

            if (debugWorker.IsBusy)
            {
                debugWorker.CancelAsync();
                runDebugBtn.Text = "Run Debug Commands";

                runDebugBtn.Enabled = false;
                await Task.Run(() =>
                {
                    while (debugWorker.IsBusy)
                    {
                        Thread.Sleep(100);
                    }
                });
                runDebugBtn.Enabled = true;

            }
            else
            {
                runDebugBtn.Text = "Stop Debug";
                debugWorker.RunWorkerAsync();
            }

        }

        private void debugWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            string[] cmds = commandsBox.Text.Split('\n');
            int lineDelay = int.Parse(lineDelayBox.Text);
            int byteDelay = int.Parse(byteDelayBox.Text);
            string crlfOption = "\r\n";
            int numTimes = 1;
            int.TryParse(numTimesBox.Text, out numTimes);

            if (crRadio.Checked) crlfOption = "\r";
            else if (lfRadio.Checked) crlfOption = "\n";

            bool indef = indefiniteCheck.Checked;
            int times = 0;
            while (!debugWorker.CancellationPending)
            {
                if (times == numTimes && !indef) return;
                foreach (string cmd in cmds)
                {
                    man.SendCommand(cmd.Trim(' ', '\n', '\r') + crlfOption, byteDelay);
                    Thread.Sleep(lineDelay);
                }
                times++;

            }

        }

        private void debugWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            runDebugBtn.Text = "Run Debug Commands";
            Refresh();
        }

        private void sendCrBtn_Click(object sender, EventArgs e)
        {
            man.SendCommand("\r");
        }

        private void sendLfBtn_Click(object sender, EventArgs e)
        {
            man.SendCommand("\n");

        }

        private void flushOutBuffer_Click(object sender, EventArgs e)
        {
            man.ClearInput();
        }

        private void flushIncBuffer_Click(object sender, EventArgs e)
        {
            man.ClearInput();
        }

        private void clrOut_Click(object sender, EventArgs e)
        {
            termOut.Text = "";
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            if (!man.IsConnected())
            {
                man.Connect(man.com);
            }
            else
            {
                man.Disconnect();
            }
            CheckConnection();
        }

        private void termIn_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

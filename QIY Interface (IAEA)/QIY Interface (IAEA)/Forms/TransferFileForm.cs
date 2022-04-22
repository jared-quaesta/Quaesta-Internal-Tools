using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIY_Interface__IAEA_
{
    public partial class TransferFileForm : Form
    {
        private List<Tuple<string, long>> files;
        private bool remA;
        Tuple<string, string> nameip;
        private TCPManager tcpMan;

        internal TransferFileForm(List<Tuple<string, long>> files, bool remA, Tuple<string, string> nameip, TCPManager tcpMan)
        { 
            this.files = files;
            this.remA = remA;
            this.nameip = nameip;
            this.tcpMan = tcpMan;
            InitializeComponent();
        }


        private void TransferFileForm_Load(object sender, EventArgs e)
        {
            ipBox.Text = nameip.Item2;
            nameBox.Text = nameip.Item1;
            if (ConfigurationManager.AppSettings["HGMPATH"].Length > 0)
            {
                destPathBox.Text = Directory.GetParent(ConfigurationManager.AppSettings["HGMPATH"]).ToString();
            }
            string filename = files[0].Item1.Split('/')[files[0].Item1.Split('/').Length - 1];
            npmFilenameBox.Text = filename;
            if (ConfigurationManager.AppSettings["HGMPATH"].Length > 0 && files[0].Item1.Contains(".HGM"))
            {
                pcFilenameBox.Text = Path.Combine(ConfigurationManager.AppSettings["HGMPATH"].ToString(), filename);
            }
            if (ConfigurationManager.AppSettings["DATPATH"].Length > 0 && files[0].Item1.Contains(".DAT"))
            {
                pcFilenameBox.Text = Path.Combine(ConfigurationManager.AppSettings["DATPATH"].ToString(), filename);
            }
            if (ConfigurationManager.AppSettings["BINPATH"].Length > 0 && files[0].Item1.Contains(".BIN"))
            {
                pcFilenameBox.Text = Path.Combine(ConfigurationManager.AppSettings["BINPATH"].ToString(), filename);
            }

        }

        private void destSelectBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderD = new FolderBrowserDialog();
            folderD.ShowDialog();
            if (folderD.SelectedPath.Length != 0)
            {
                destPathBox.Text = folderD.SelectedPath;
                pcFilenameBox.Text = Path.Combine(destPathBox.Text, files[0].Item1.Split('/')[files[0].Item1.Split('/').Length - 1]);
                Refresh();
            }

        }

        private async void startBtn_ClickAsync(object sender, EventArgs e)
        {
            if (!Directory.Exists(destPathBox.Text))
            {
                MessageBox.Show("Destination does not exist.", "ERROR");
                return;
            }
            startBtn.Enabled = false;
            int count = 1;
            cancelBtn.Enabled = true;
            foreach (Tuple<string, long> file in files)
            {
                if (tcpMan.stopType)
                {
                    return;
                }
                string filename = file.Item1.Split('/')[file.Item1.Split('/').Length - 1];
                npmFilenameBox.Text = filename;
                pcFilenameBox.Text = Path.Combine(destPathBox.Text, filename);

                if (ConfigurationManager.AppSettings["HGMPATH"].Length > 0 &&
                     destPathBox.Text == Directory.GetParent(ConfigurationManager.AppSettings["HGMPATH"]).ToString())
                {

                    if (ConfigurationManager.AppSettings["HGMPATH"].Length > 0 && file.Item1.Contains(".HGM"))
                    {
                        pcFilenameBox.Text = Path.Combine(ConfigurationManager.AppSettings["HGMPATH"].ToString(), filename);
                    }
                    if (ConfigurationManager.AppSettings["DATPATH"].Length > 0 && file.Item1.Contains(".DAT"))
                    {
                        pcFilenameBox.Text = Path.Combine(ConfigurationManager.AppSettings["DATPATH"].ToString(), filename);
                    }
                    if (ConfigurationManager.AppSettings["BINPATH"].Length > 0 && file.Item1.Contains(".BIN"))
                    {
                        pcFilenameBox.Text = Path.Combine(ConfigurationManager.AppSettings["BINPATH"].ToString(), filename);
                    }
                }

                Refresh();
                fileProgressLbl.Text = $"Transferring File {count} of {files.Count}.";
                count++;
                
                await tcpMan.Type(file.Item1, file.Item2, this, pcFilenameBox.Text);
                Refresh();
               
                Thread.Sleep(100);
            }

            tcpMan.stopType = false;

            cancelBtn.Enabled = false;
            startBtn.Enabled = true;
            if (remA)
            {
                foreach (Tuple<string, long> file in files)
                {
                    tcpMan.NewCmd($"Attrib -A {file.Item1}\r\n");
                }
            }
            completeLbl.Visible = true;
            tcpMan.type = false;
        }

        internal void UpdateProgress(int progress, int bRec, int bTr, int pRec, long expected)
        {
            bytesRecievedLbl.Text = $"Bytes Recieved: {bRec} of {expected}";
            bytesTransferredLbl.Text = $"Bytes Transfered: {bTr} of {expected}";
            packetsTransferredLbl.Text = $"Packets Transferred: {pRec}";
            progressBar.Value = progress;
            Refresh();
        }

        private async void cancelBtn_Click(object sender, EventArgs e)
        {
            tcpMan.stopType = true;
            tcpMan.NewCmd("info\r\n");
            MessageBox.Show("Operation Cancelled. The current file will finish but not be stored.", "CANCELLED");
            Close();
        }
    }
}

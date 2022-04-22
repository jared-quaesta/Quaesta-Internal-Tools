using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIY_Interface__IAEA_
{
    public partial class DataRetrievalForm : Form
    {
        private TCPManager tcpMan;
        private string npmName;

        private string askDir = "";

        private TransferFileForm transferForm;

        public DataRetrievalForm()
        {
            InitializeComponent();
        }

        internal DataRetrievalForm(TCPManager tcpMan, string name)
        {
            this.tcpMan = tcpMan;
            npmName = name;
            InitializeComponent();
        }

        private async void refreshBtn_Click(object sender, EventArgs e)
        {
            tcpMan.listener.ClearDRPoints();

            tcpMan.NewCmd($"dir 1:/{askDir}\r\n");

            await Task.Run(() =>
            {
                while (!tcpMan.listener.GotDir())
                {
                    Thread.Sleep(100);
                }
            });

            tcpMan.NewCmd($"dir 0:/{askDir}\r\n");
            await Task.Run(() =>
            {
                while (!tcpMan.listener.GotDir())
                {
                    Thread.Sleep(100);
                }
            });
        }
        
        private void DataRetrievalForm_Load(object sender, EventArgs e)
        {
            ipBox.Text = tcpMan.GetIP();
            nameBox.Text = npmName;
            refreshBtn_Click(null, null);
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            askDir = ((RadioButton)sender).Name;
            if (((RadioButton)sender).Checked)
                refreshBtn_Click(null, null);
        }


        private void ValidateSelection(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox box = (CheckedListBox)sender;
            if (box.Items[e.Index].ToString().Contains("D-"))
            {
                e.NewValue = CheckState.Unchecked;
            }
            box.SelectedIndex = -1;

        }

        private void UpdateInternalDetails(object sender, MouseEventArgs e)
        {
            long bytes = 0;
            for (int i = 0; i < internalDirBox.CheckedItems.Count; i++)
            {
                //----A 2022/01/06 20:16   1168519 NPM3100E-QIY_220106.DAT
                string raw = internalDirBox.CheckedItems[i].ToString();
                string[] split = raw.Split(' ');
                List<string> gleamed = new List<string>();
                foreach (string part in split)
                {
                    if (part.Trim().Length == 0) continue;
                    gleamed.Add(part);
                }
                bytes += long.Parse(gleamed[3]);
            }
            internalBytesSelectedBox.Text = bytes.ToString();
            internalSelectedFilesBox.Text = internalDirBox.CheckedItems.Count.ToString();
        }
        private void UpdateExternalDetails(object sender, MouseEventArgs e)
        {
            long bytes = 0;
            for (int i = 0; i < externalDirBox.CheckedItems.Count; i++)
            {
                //----A 2022/01/06 20:16   1168519 NPM3100E-QIY_220106.DAT
                string raw = externalDirBox.CheckedItems[i].ToString();
                string[] split = raw.Split(' ');
                List<string> gleamed = new List<string>();
                foreach (string part in split)
                {
                    if (part.Trim().Length == 0) continue;
                    gleamed.Add(part);
                }
                bytes += long.Parse(gleamed[3]);
            }
            externalBytesSelectedBox.Text = bytes.ToString();
            externalSelectedFilesBox.Text = externalDirBox.CheckedItems.Count.ToString();
        }

        private void internalDirBox_MouseDown(object sender, MouseEventArgs e)
        {
            //CheckedListBox box = (CheckedListBox)sender;
            //Debug.WriteLine(box.IndexFromPoint(e.Location));
        }

        private void DragSelect(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CheckedListBox box = (CheckedListBox)sender;
                try
                {
                    box.SetItemChecked(box.IndexFromPoint(e.Location), true);
                }
                catch { }
            }
            if (e.Button == MouseButtons.Right)
            {
                CheckedListBox box = (CheckedListBox)sender;
                try
                {
                    box.SetItemChecked(box.IndexFromPoint(e.Location), false);
                }
                catch { }
            }
        }

        private void clearSel_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Parent.Name.Equals("internalGB"))
            {
                for (int i = 0; i < internalDirBox.Items.Count; i++)
                {
                    internalDirBox.SetItemChecked(i, false);
                    internalSelectedFilesBox.Text = "0";
                    internalBytesSelectedBox.Text = "0";
                    internalDirBox.SelectedIndex = -1;
                }
            } else
            {
                for (int i = 0; i < externalDirBox.Items.Count; i++)
                {
                    externalDirBox.SetItemChecked(i, false);
                    externalSelectedFilesBox.Text = "0";
                    externalBytesSelectedBox.Text = "0";
                    externalDirBox.SelectedIndex = -1;
                }
            }
        }

        private void retFiles_Click(object sender, EventArgs e)
        {
            Control parent = ((Button)sender).Parent;

            // get params
            string whichFiles = "";
            bool remA = false;
            foreach (RadioButton pb in parent.Controls.OfType<RadioButton>())
            {
                if (pb.Checked)
                {
                    whichFiles = pb.Name;
                    break;
                }
            }

            if (parent.Controls.OfType<CheckBox>().First().Checked)
            {
                remA = true;
            }

            if (parent.Name.Equals("internalGB"))
            {
                MakeLoadingForm(0, whichFiles, remA);
            }
            else
            {
                MakeLoadingForm(1, whichFiles, remA);
            }
        }

        private void MakeLoadingForm(int intExt, string whichFiles, bool remA)
        {
            // 0 is internal, 1 is external

            if (transferForm != null)
            {
                MessageBox.Show("Already Transferring.", "ERROR");
                return;
            }

            CheckedListBox box = externalDirBox;
            if (intExt == 0) box = internalDirBox;

            // {path, nbytes}
            List<Tuple<string, long>> files = new List<Tuple<string, long>>();
            string type = "";
            // get filenames
            if (whichFiles.Contains("all"))
            {
                foreach (string item in box.Items)
                {
                    if (item.Contains("D-")) continue;
                    string[] split = item.Split(' ');
                    List<string> gleamed = new List<string>();
                    foreach (string part in split)
                    {
                        if (part.Trim().Length == 0) continue;
                        gleamed.Add(part);
                    }
                    //----A 2022 / 01 / 06 20:16   1168519 NPM3100E - QIY_220106.DAT
                    type = gleamed[4].Split('.')[gleamed[4].Split('.').Length - 1];
                    string path = $"{intExt}:/{type}/{gleamed[4]}";
                    Tuple<string, long> entry = new Tuple<string, long>(path,long.Parse(gleamed[3]));
                    files.Add(entry);

                }
            }

            else if (whichFiles.Contains("sel"))
            {
                if (box.CheckedItems.Count == 0)
                {
                    MessageBox.Show("You haven't Selected Any Files.", "ERROR");
                }
                
                foreach (string item in box.CheckedItems)
                {

                    string[] split = item.Split(' ');
                    List<string> gleamed = new List<string>();
                    foreach (string part in split)
                    {
                        if (part.Trim().Length == 0) continue;
                        gleamed.Add(part);
                    }
                    //----A 2022 / 01 / 06 20:16   1168519 NPM3100E - QIY_220106.DAT


                    type = gleamed[4].Split('.')[gleamed[4].Split('.').Length - 1];
                    string path = $"{intExt}:/{type}/{gleamed[4]}";
                    Tuple<string, long> entry = new Tuple<string, long>(path, long.Parse(gleamed[3]));
                    files.Add(entry);

                    Debug.WriteLine(path);
                }

            }
            else
            {
                foreach (string item in box.Items)
                {
                    if (item.Contains("D-")) continue;
                    string[] split = item.Split(' ');
                    List<string> gleamed = new List<string>();
                    foreach (string part in split)
                    {
                        if (part.Trim().Length == 0) continue;
                        gleamed.Add(part);
                    }
                    //----A 2022 / 01 / 06 20:16   1168519 NPM3100E - QIY_220106.DAT
                    if (!gleamed[0].Contains("A")) continue;
                    type = gleamed[4].Split('.')[gleamed[4].Split('.').Length - 1];
                    string path = $"{intExt}:/{type}/{gleamed[4]}";
                    Tuple<string, long> entry = new Tuple<string, long>(path, long.Parse(gleamed[3]));
                    files.Add(entry);

                }
            }
            if (files.Count == 0)
            {
                MessageBox.Show("No Files Selected", "ERROR");
                return;
            }
            transferForm = new TransferFileForm(files, remA, new Tuple<string, string>(nameBox.Text, ipBox.Text), tcpMan);
            transferForm.ShowDialog();
            try { transferForm.Dispose(); } catch { }
            
            transferForm = null;
        }

        private void SelectNone(object sender, EventArgs e)
        {
            ((CheckedListBox)sender).SelectedIndex = -1;
        }

        private void allInt_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            if (!btn.Checked) return;
            if (btn.Name.Contains("Int"))
            {
                for (int i = 0; i < internalDirBox.Items.Count; i++)
                {
                    internalDirBox.SetItemChecked(i, true);
                }
            } else
            {
                for (int i = 0; i < externalDirBox.Items.Count; i++)
                {
                    externalDirBox.SetItemChecked(i, true);
                }
            }


        }

        private void nonArchiveBtnIn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            if (!btn.Checked) return;
            if (btn.Name.Contains("In"))
            {
                for (int i = 0; i < internalDirBox.Items.Count; i++)
                {
                    if (internalDirBox.Items[i].ToString().Contains("---A")) 
                        internalDirBox.SetItemChecked(i, true);
                    else
                        internalDirBox.SetItemChecked(i, false);
                }
            }
            else
            {
                for (int i = 0; i < externalDirBox.Items.Count; i++)
                {
                    if (externalDirBox.Items[i].ToString().Contains("---A"))
                        externalDirBox.SetItemChecked(i, true);
                    else
                        externalDirBox.SetItemChecked(i, false);
                }
            }
        }
    }
}

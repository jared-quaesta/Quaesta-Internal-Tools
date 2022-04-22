using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDI12AddressTool
{
    public partial class MainForm : Form
    {
        private Reciever listener;
        private DLManager dlMan = new DLManager();
        private SQLManager sqlMan = new SQLManager();

        private readonly int LISTENTIMEOUT = 1000;

        private ArrayList curSessionItems = new ArrayList();
        private string lastSDI = "";

        //#### ROW CONSTANTS ####\\

        private readonly Size SDICOLSIZE = new Size(15, 15);
        private readonly Point SDICOLLOC = new Point(15, 48);

        private readonly Size NPMSNSIZE = new Size(65, 15);
        private readonly Point NPMSNLOC = new Point(53, 48);

        private readonly Size TUBESNSIZE = new Size(66, 15);
        private readonly Point TUBESNLOC = new Point(133, 48);

        private readonly Size TUBETYPESIZE = new Size(176, 15);
        private readonly Point TUBETYPELOC = new Point(214, 48);

        private readonly Size FWSIZE = new Size(100, 15);
        private readonly Point FWLOC = new Point(410, 48);

        private readonly Size EDITBTNSIZE = new Size(41, 23);
        private readonly Point EDITBTNLOC = new Point(516, 44);

        private readonly Size REMBTNSIZE = new Size(62, 23);
        private readonly Point REMBTNLOC = new Point(563, 44);
        
        private readonly Size DIVIDERSIZE = new Size(610, 2);
        private readonly Point DIVIDERLOC = new Point(15, 70);

        private readonly int YOFFSET = 35;

        private readonly char[] AVAILABLESDI = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        //#### END CONSTANTS ####\\

        private AlertForm alertForm = new AlertForm();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listener = new Reciever(this);
            dlMan.LinkTerm(listener);
            dlMan.Connect(dlMan.GetCom());
        }

        private void queryNPMBtn_Click(object sender, EventArgs e)
        {
            dlMan.SendCommand("?!\r\n");
            if (!listenWorker.IsBusy) listenWorker.RunWorkerAsync();

        }

        private void ListenForChanges(object sender, DoWorkEventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();
            while (!listenWorker.CancellationPending && sw.ElapsedMilliseconds < LISTENTIMEOUT)
            {
                if (listener.curSn.Length != 0)
                {
                    listenWorker.ReportProgress(0, listener.curSn);
                    listener.curSn = "";
                }
                if (listener.curAddress.Length != 0)
                {
                    listenWorker.ReportProgress(1, listener.curAddress);
                    dlMan.SendCommand($"{listener.curAddress}I!\r\n");
                    listener.curAddress = "";
                }
                if (listener.curFw.Length != 0)
                {
                    listenWorker.ReportProgress(2, listener.curFw);
                    listener.curFw = "";
                }
            }
        }

        private void UpdateView(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:
                    npmSnBox.Text = e.UserState.ToString();
                    break;
                case 1:
                    sdiAddressBox.Text = e.UserState.ToString();
                    lastSDI = e.UserState.ToString();
                    break;
                case 2:
                    fwBox.Text = e.UserState.ToString();
                    break;
            }
        }

        private void UpdateList()
        {
            int offset = -1 * YOFFSET;
            listPanel.Controls.Clear();
            foreach (Entry en in curSessionItems)
            {
                Label sdi = new Label();
                sdi.Text = en.sdi;
                sdi.Size = SDICOLSIZE;
                sdi.Location = new Point(SDICOLLOC.X, SDICOLLOC.Y + offset);
                listPanel.Controls.Add(sdi);

                Label npmsn = new Label();
                npmsn.Text = en.sn;
                npmsn.Size = NPMSNSIZE;
                npmsn.Location = new Point(NPMSNLOC.X, NPMSNLOC.Y + offset);
                listPanel.Controls.Add(npmsn);

                Label tubesn = new Label();
                tubesn.Text = en.tubesn;
                tubesn.Size = TUBESNSIZE;
                tubesn.Location = new Point(TUBESNLOC.X, TUBESNLOC.Y + offset);
                listPanel.Controls.Add(tubesn);

                Label tubetype = new Label();
                tubetype.Text = en.tubetype;
                tubetype.Size = TUBETYPESIZE;
                tubetype.Location = new Point(TUBETYPELOC.X, TUBETYPELOC.Y + offset);
                listPanel.Controls.Add(tubetype);

                Label fwv = new Label();
                fwv.Text = en.firmware;
                fwv.Size = FWSIZE;
                fwv.Location = new Point(FWLOC.X, FWLOC.Y + offset);
                listPanel.Controls.Add(fwv);

                Button edit = new Button();
                edit.Text = "Edit";
                edit.Size = EDITBTNSIZE;
                edit.Location = new Point(EDITBTNLOC.X, EDITBTNLOC.Y + offset);
                edit.Click += (e, sender) =>
                {
                    npmSnBox.Text = en.sn;
                    tubeSnBox.Text = en.tubesn;
                    tubeOwnerBox.Text = en.tubeowner;
                    noteBox.Text = en.note;
                    sdiAddressBox.Text = en.sdi;
                    tubeTypeBox.Text = en.tubetype;
                    fwBox.Text = en.firmware;
                };
                listPanel.Controls.Add(edit);

                Button rem = new Button();
                rem.Text = "Remove";
                rem.ForeColor = Color.Red;
                rem.Size = REMBTNSIZE;
                rem.Location = new Point(REMBTNLOC.X, REMBTNLOC.Y + offset);
                rem.Click += (e, sender) =>
                {
                    curSessionItems.Remove(en);
                    UpdateList();
                    Refresh();
                };
                listPanel.Controls.Add(rem);


                Label div = new Label();
                div.Text = "";
                div.Size = DIVIDERSIZE;
                div.Location = new Point(DIVIDERLOC.X, DIVIDERLOC.Y + offset);
                div.BorderStyle = BorderStyle.Fixed3D;
                listPanel.Controls.Add(div);

                offset += YOFFSET;

            }
        }

        private void applySettings_Click(object sender, EventArgs e)
        {
            string sn = "";
            string sdi = "";
            string tubesn = "";
            string tubeowner = "";
            string tubetype = "";
            string note = "";
            string firmware = "";

            // if any boxes are not full replace with null
            foreach (Control control in Controls)
            {
                if (control.GetType().Name.Contains("Box") )
                {
                    ////
                    //fwBox
                    //tubeTypeBox
                    //sdiAddressBox
                    //tubeSnBox
                    //tubeOwnerBox
                    //noteBox
                    //npmSnBox
                    ///
                    string val = control.Text;

                    if (val.Length == 0 && !control.Name.Contains("note"))
                    {
                        control.Text = "NOT PROVIDED";
                    }

                    if (control.Name.Equals("fwBox"))
                    {
                        firmware = val;
                    }
                    
                    if (control.Name.Equals("tubeTypeBox"))
                    {
                        tubetype = val;
                        if (!((ComboBox)control).Items.Contains(tubetype))
                        {
                            ((ComboBox)control).Items.Add(tubetype);
                        }
                    }
                    
                    if (control.Name.Equals("sdiAddressBox"))
                    {
                        sdi = val;
                    }
                    
                    if (control.Name.Equals("tubeSnBox"))
                    {
                        tubesn = val;
                    }
                    
                    if (control.Name.Equals("tubeOwnerBox"))
                    {
                        tubeowner = val;
                    }   

                    if (control.Name.Equals("noteBox"))
                    {
                        note = val;
                    }
                    
                    if (control.Name.Equals("npmSnBox"))
                    {
                        sn = val;
                    }

                    control.Text = "";

                }
            }

            foreach (Entry en in curSessionItems)
            {
                if (en.sdi.Equals(sdi) && !en.sn.Equals(sn))
                {
                    alertForm.ShowDialog();
                    if (!alertForm.isOkay)
                    {
                        npmSnBox.Text = sn;
                        tubeSnBox.Text = tubesn;
                        tubeOwnerBox.Text = tubeowner;
                        noteBox.Text = note;
                        sdiAddressBox.Text = sdi;
                        tubeTypeBox.Text = tubetype;
                        fwBox.Text = firmware;
                        return;
                    } 
                }
            }

            /// change sdi of dev
            if (lastSDI.Length != 0)
            {
                dlMan.SendCommand($"{lastSDI}A{sdi}!\r\n");
                lastSDI = "";
            }
            

            /// apply changes
            Entry entry = new Entry(sn, sdi, tubesn,  tubeowner, tubetype, note, firmware);
            Entry remEntry = null;
            foreach (Entry en in curSessionItems)
            {
                if (en.sn.Equals(sn))
                {
                    remEntry = en;
                }
            }
            if (remEntry != null)
            {
                curSessionItems.Remove(remEntry);
            }

            curSessionItems.Add(entry);
            curSessionItems.Sort(new compare());
            UpdateList();
            Refresh();
        }

        public class compare : IComparer
        {
            int IComparer.Compare(Object a, Object b)
            {
                return ((new CaseInsensitiveComparer()).Compare(((Entry)a).sdi, ((Entry)b).sdi));
            }
        }

        private void applyAllButton_Click(object sender, EventArgs e)
        {
            foreach (Entry en in curSessionItems)
            {
                sqlMan.SetDeviceParams(en.tubesn, en.tubetype, en.tubeowner, en.sn, en.firmware, en.note, en.sdi);
            }
        }
    }
}

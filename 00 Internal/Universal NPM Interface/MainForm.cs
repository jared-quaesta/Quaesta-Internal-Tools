using System;
using System.Drawing;
using System.Windows.Forms;
using Universal_NPM_Interface.Controls;
using Universal_NPM_Interface.Serial;

namespace Universal_NPM_Interface
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void findNPMsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            foreach (string com in SerialNPMManager.GetComs("STMicroelectronics"))
            {
                MakeTab(com);
            }
        }

        private void MakeTab(string com)
        {
            
            SerialNPMManager serialMan = new SerialNPMManager("UNK", com);
            DirectTerminal term = serialMan.GetTerm();
            TabPage newTab = new TabPage(com);
            NPMParamControl paramControl = new NPMParamControl(serialMan);

            term.Location = new Point(539, 53);
            //term.Size = new Size(668, 573);


            paramControl.Location = new Point(0,0);
            paramControl.Size = new Size(536, 678);

            Button connectBtn = new Button();
            connectBtn.Text = "Connect";
            connectBtn.Size = new Size(667, 44);
            connectBtn.Location = new Point(539, 3);
            connectBtn.Click += (sender, e) => ConnectTo(sender, serialMan, paramControl);
            //
            newTab.Controls.Add(connectBtn);
            newTab.Controls.Add(paramControl);
            newTab.Controls.Add(term);
            newTab.Tag = serialMan;
            
            tabControl.Controls.Add(newTab);
            

        }

        private void ConnectTo(object sender, SerialNPMManager serialMan, NPMParamControl paramControl)
        {
            if (serialMan.IsConnected())
            {
                serialMan.Disconnect();
                ((Button)sender).Text = "Connect";
            }
            else
            {
                if (serialMan.Connect(serialMan.com))
                {
                    ((Button)sender).Text = "Disconnect";
                    paramControl.TryQuery();
                } 
                else
                {
                    MessageBox.Show("Could not connect, is TeraTerm open?", "Error");
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

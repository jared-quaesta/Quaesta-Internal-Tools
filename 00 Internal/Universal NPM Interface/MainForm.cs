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
            TabPage newTab = new TabPage(com);
            NPMParamControl paramControl = new NPMParamControl();
            Button connectBtn = new Button();

            paramControl.Location = new Point(0,0);
            paramControl.Size = new Size(536, 678);
            connectBtn.Text = "Connect";
            connectBtn.Size = new Size(667, 44);
            connectBtn.Location = new Point(539, 3);
            //
            newTab.Controls.Add(connectBtn);
            newTab.Controls.Add(paramControl);
            newTab.Tag = new SerialNPMManager("UNK", com);
            
            tabControl.Controls.Add(newTab);
            

        }
    }
}

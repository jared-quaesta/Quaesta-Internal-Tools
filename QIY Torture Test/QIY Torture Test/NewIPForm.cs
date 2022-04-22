using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIY_Torture_Test
{
    public partial class NewIPForm : Form
    {
        public NewIPForm()
        {
            InitializeComponent();
        }

        internal string ip = "";

        private void NewIPForm_Load(object sender, EventArgs e)
        {

        }

        private async void addBtn_Click(object sender, EventArgs e)
        {
            // try to connect to ip, if not show message and return, else close box and add to options

            string tryIP = $"{ipOne.Text}.{ipTwo.Text}.{ipThree.Text}.{ipFour.Text}";

            try
            {
                TCPNPMManager man = new TCPNPMManager(tryIP);
                if (await man.TryConnectionAsync())
                {
                    man.Disconnect();
                    ip = tryIP;
                    Close();
                }
                else
                {
                    MessageBox.Show("Could not connect to " + tryIP);
                }
            }
            catch
            {
                MessageBox.Show("Could not connect to " + tryIP);
            }
           

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

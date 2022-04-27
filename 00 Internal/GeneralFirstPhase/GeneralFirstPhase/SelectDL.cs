using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralFirstPhase
{
    public partial class SelectDL : Form
    {
        string[] coms;
        public string selectedCom = "";
        public SelectDL(string[] coms)
        {
            this.coms = coms;
            InitializeComponent();
        }

        private void SelectDL_Load(object sender, EventArgs e)
        {
            foreach (string com in coms)
            {
                comSel.Items.Add(com);
            }
        }

        private void HideForm(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void abortBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chooseBtn_Click(object sender, EventArgs e)
        {
            if (comSel.SelectedIndex == -1)
            {
                MessageBox.Show("No COM selected.", "Error");
                return;
            }
            selectedCom = comSel.SelectedItem.ToString();
            Close();
        }
    }
}

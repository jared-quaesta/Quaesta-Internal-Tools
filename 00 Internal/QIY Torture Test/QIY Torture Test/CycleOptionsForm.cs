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
    public partial class CycleOptionsForm : Form
    {
        public CycleOptionsForm()
        {
            InitializeComponent();
        }

        private void HideForm(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void CycleOptionsForm_Load(object sender, EventArgs e)
        {

        }

        private void newTestBtn_Click(object sender, EventArgs e)
        {
            CreateTest newTest = new CreateTest();
            newTest.ShowDialog();

            if (!newTest.testString.Equals(""))
            {
                testsCheckListBox.SetItemChecked(testsCheckListBox.Items.Add(newTest.testString), true);
            }

            newTest.Dispose();
        }
    }
}

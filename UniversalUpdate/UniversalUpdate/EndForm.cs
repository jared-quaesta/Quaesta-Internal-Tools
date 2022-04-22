using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UniversalUpdate
{
    public partial class Finished : Form
    {
        List<Tuple<string, string, string>> coms;
        int curIndex = 0;
        public Finished(List<Tuple<string, string, string>> coms)
        {
            this.coms = coms;
            InitializeComponent();
        }

        private void Finished_Load(object sender, EventArgs e)
        {
            DisplayCom();
        }

        private void DisplayCom()
        {
            comLbl.Text = coms[curIndex].Item1;
            fwLbl.Text = coms[curIndex].Item2;
            infoTxt.Text = coms[curIndex].Item3;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            curIndex--;
            if (curIndex < 0)
            {
                curIndex = coms.Count - 1;
            }
            DisplayCom();

        }

        private void fwdBtn_Click(object sender, EventArgs e)
        {
            curIndex++;
            if (curIndex > coms.Count - 1)
            {
                curIndex = 0;
            }
            DisplayCom();
        }
    }
}

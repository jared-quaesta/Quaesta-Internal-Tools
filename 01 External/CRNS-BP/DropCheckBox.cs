using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRNS_BP
{
    public partial class DropCheckBox : UserControl
    {
        int nItems = 0;
        MainForm mf;
        public PlotModel tag = null;

        public DropCheckBox()
        {
            InitializeComponent();
        }

        internal void ReferencePlotModel(PlotModel plt, MainForm mf)
        {
            this.mf = mf;
            tag = plt;
        }

        private void OpenClosePanel(object sender, MouseEventArgs e)
        {
            if (checkListPanel.Visible)
            {
                Size = new Size(Size.Width, 21);
                checkListPanel.Visible = false;
            } else
            {
                Size = new Size(Size.Width, 21 + (nItems * 18));
                checkListPanel.Visible = true;
            }
            comboBox.Focus();
            SendKeys.Send("{esc}");
            mf.UpdateChart(tag, comboBox.Text) ;
        }


        internal void AddItem(string item)
        {
            // close panel
            if (ContainsItem(item)) return;
            checkListBox.Items.Add(item);
            nItems++;
            Size = new Size(Size.Width, 21);
            checkListPanel.Visible = false;
        }

        internal void Clear()
        {
            checkListBox.Items.Clear();
            comboBox.Text = "";
            nItems = 0;
        }


        private void CancelKey(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void UpdateChecks(object sender, EventArgs e)
        {
            string sel = "";
            foreach (string item in checkListBox.CheckedItems)
            {
                sel += item + ", ";
            }
            comboBox.Text = sel.Trim(',', ' ');
        }

        private void CancelDropdown(object sender, EventArgs e)
        {
            comboBox.DroppedDown = false;
        }

        internal bool ContainsItem(string headerItem)
        {
            return checkListBox.Items.Contains(headerItem);
        }
    }
}

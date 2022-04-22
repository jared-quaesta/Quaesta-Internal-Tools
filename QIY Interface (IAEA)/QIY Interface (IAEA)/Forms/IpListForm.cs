using QIY_Interface__IAEA_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIY_Interface__IAEA_
{
    public partial class IpListForm : Form
    {
        MainForm mainForm;
        public IpListForm(MainForm main)
        {
            mainForm = main;
            InitializeComponent();
        }

        private void IpListForm_Load(object sender, EventArgs e)
        {
            FormClosing += (t, o) => { Hide();o.Cancel = true; };
        }

        private void NextBox(object sender, EventArgs e)
        {
            TextBox s = (TextBox)sender;
            int n = 3;
            if (s.Text.Length == n) ProcessTabKey(true);
        }

        private void PrevBox(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back && ((TextBox)sender).Text.Length == 0)
            {
                if (ProcessTabKey(false))
                {
                    TextBox cur = (TextBox)ActiveControl;
                    cur.SelectAll();
                }
            }
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            string ip = $"{ip0.Text}.{ip1.Text}.{ip2.Text}.{ip3.Text}";
            if (IPAddress.TryParse(ip, out IPAddress ipa))
            {
                listBox.Items.Add(ipa.ToString());
                ip0.Clear();
                ip1.Clear();
                ip2.Clear();
                ip3.Clear();
                Refresh();
            } 
            else
            {
                MessageBox.Show($"Invalid IP Address:\n'{ip}'", "ERROR");
            }
        }

        private void apply_Click(object sender, EventArgs e)
        {
            List<string> ips = new List<string>();
            foreach (string ip in listBox.Items)
            {
                ips.Add(ip);
            }
            mainForm.EditIpList(ips);

        }

        private void moveUp_Click(object sender, EventArgs e)
        {
            int selIndex = listBox.SelectedIndex;
            if (selIndex <= 0) return;
            string selIp = listBox.SelectedItem.ToString();
            listBox.Items.RemoveAt(selIndex);
            listBox.Items.Insert(selIndex - 1, selIp);
            listBox.SelectedIndex = selIndex - 1;

        }

        private void moveDown_Click(object sender, EventArgs e)
        {
            int selIndex = listBox.SelectedIndex;
            if (selIndex >= listBox.Items.Count-1) return;
            string selIp = listBox.SelectedItem.ToString();
            listBox.Items.RemoveAt(selIndex);
            listBox.Items.Insert(selIndex + 1, selIp);
            listBox.SelectedIndex = selIndex + 1;
        }

        private void rembtn_Click(object sender, EventArgs e)
        {
            int selIndex = listBox.SelectedIndex;
            if (selIndex == -1) return;
            listBox.Items.RemoveAt(selIndex);
        }

        private void discover_Click(object sender, EventArgs e)
        {
            mainForm.udpMan.SearchNeuchQIY();
        }

        private void readConfig_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            if (ConfigurationManager.AppSettings["SAVEDIPS"].Length > 0)
            {
                foreach (string ip in ConfigurationManager.AppSettings["SAVEDIPS"].Split(','))
                {
                    listBox.Items.Add(ip);
                }
            }
        }

        private void saveConfig_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string ipString = "";
            foreach (string ip in listBox.Items)
            {
                ipString += ip + ",";
            }
            ipString = ipString.Trim(',');
            config.AppSettings.Settings["SAVEDIPS"].Value = ipString;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            config = null;

        }
    }
}

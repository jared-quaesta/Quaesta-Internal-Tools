using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;

namespace QIY_Interface__IAEA_
{
    public partial class DirectoriesForm : Form
    {
        
        public DirectoriesForm()
        {
            InitializeComponent();
        }

        private void DirectoriesForm_Load(object sender, EventArgs e)
        {
            hgmBox.Text = ConfigurationManager.AppSettings["HGMPATH"];
            binBox.Text = ConfigurationManager.AppSettings["BINPATH"];
            datBox.Text = ConfigurationManager.AppSettings["DATPATH"];
        }

        private void HideForm(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["HGMPATH"].Value = hgmBox.Text; 
            config.AppSettings.Settings["BINPATH"].Value = binBox.Text;
            config.AppSettings.Settings["DATPATH"].Value = datBox.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            config = null;
        }

        

        private void chooseDat_Click(object sender, EventArgs e)
        {
            folderBrowser.ShowDialog();
            if (Directory.Exists(folderBrowser.SelectedPath))
            {
                datBox.Text = folderBrowser.SelectedPath;
            }
        }

        private void chooseHgm_Click(object sender, EventArgs e)
        {
            folderBrowser.ShowDialog();
            if (Directory.Exists(folderBrowser.SelectedPath))
            {
                hgmBox.Text = folderBrowser.SelectedPath;
            }
        }

        private void chooseBin_Click(object sender, EventArgs e)
        {
            folderBrowser.ShowDialog();
            if (Directory.Exists(folderBrowser.SelectedPath))
            {
                binBox.Text = folderBrowser.SelectedPath;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversalUpdate
{
    public partial class MenuForm : Form
    {
        string menuString;
        public MenuForm(string menu)
        {
            menuString = menu;
            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            outputBox.Text = menuString;
        }
    }
}

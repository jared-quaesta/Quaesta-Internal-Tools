using System;
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

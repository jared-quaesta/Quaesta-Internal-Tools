using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QIRestfulSwarm;

namespace SWARMVis
{
    public partial class LoginForm : Form
    {
        RestfulSwarm rs = new RestfulSwarm();
        MainForm mainForm;


        public LoginForm(MainForm main)
        {
            mainForm = main;
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private async void TryCredentials(object sender, EventArgs e)
        {
            string user = emailBox.Text;
            string pass = pwBox.Text;

            bool auth = await Task.Run(() =>
            {
                return rs.ValidateLoginAsync(user, pass);
            });

            if (auth)
            {
                mainForm.ValidateCredentials(rs);
                Close();
            } 
            else
            {
                errLbl.Text = "Invalid login info, try again."; 
            }

        }
    }
}

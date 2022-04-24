using QIXLPTesting.SerialTools;
using QIXLPTesting.SQL;
using System;
using System.Threading;
using System.Windows.Forms;

namespace QIXLPTesting
{
    public partial class ChangeSerialForm : Form
    {
        public string selectChange = "UNK";
        string old;
        public ChangeSerialForm(string old)
        {
            this.old = old;
            InitializeComponent();
        }

        private void ChangeSerialForm_Load(object sender, EventArgs e)
        {
            oldBox.Text = old;
        }

        internal static bool ChangeSerialDialog(SerialNPMManager serialMan, string old)
        {
            ChangeSerialForm form = new ChangeSerialForm(old);
            form.ShowDialog();
            if (!form.selectChange.Equals("UNK"))
            {
                if (!serialMan.IsConnected())
                {
                    return false;
                }
                serialMan.ClearInput();
                Thread.Sleep(30);
                serialMan.AllowSecret();
                Thread.Sleep(30);

                serialMan.SendCommand($"serialnumber={form.selectChange}");
                serialMan.Disconnect();


                SQLManager.EditSerial(old, form.selectChange);
                form.Dispose();
                return true;

            }
            else return false;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void changeBtn_Click(object sender, EventArgs e)
        {
            selectChange = newBox.Text;
            Hide();
        }
    }
}

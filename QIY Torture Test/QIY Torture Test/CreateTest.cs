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
    public partial class CreateTest : Form
    {
        internal string testString = "";
        public CreateTest()
        {
            InitializeComponent();
        }

        private void CreateTest_Load(object sender, EventArgs e)
        {

        }

        private void addUptime_Click(object sender, EventArgs e)
        {
            uptimeCount.Text = (Int32.Parse(uptimeCount.Text) + 1).ToString();
        }

        private void addVoltage_Click(object sender, EventArgs e)
        {
            voltageCount.Text = (Int32.Parse(voltageCount.Text) + 1).ToString();
        }

        private void addInfo_Click(object sender, EventArgs e)
        {
            infoCount.Text = (Int32.Parse(infoCount.Text) + 1).ToString();
        }

        private void addTime_Click(object sender, EventArgs e)
        {
            timeCount.Text = (Int32.Parse(timeCount.Text) + 1).ToString();
        }

        private void addPing_Click(object sender, EventArgs e)
        {
            pingCount.Text = (Int32.Parse(pingCount.Text) + 1).ToString();
        }

        private void subUptime_Click(object sender, EventArgs e)
        {
            uptimeCount.Text = (Int32.Parse(uptimeCount.Text) - 1).ToString();
        }

        private void subVoltage_Click(object sender, EventArgs e)
        {
            voltageCount.Text = (Int32.Parse(voltageCount.Text) - 1).ToString();
        }

        private void subInfo_Click(object sender, EventArgs e)
        {
            infoCount.Text = (Int32.Parse(infoCount.Text) - 1).ToString();
        }

        private void subTime_Click(object sender, EventArgs e)
        {
            timeCount.Text = (Int32.Parse(timeCount.Text) - 1).ToString();
        }

        private void subPing_Click(object sender, EventArgs e)
        {
            pingCount.Text = (Int32.Parse(pingCount.Text) - 1).ToString();
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(pingCount.Text) +
                Int32.Parse(timeCount.Text) +
                Int32.Parse(infoCount.Text) +
                Int32.Parse(voltageCount.Text) +
                Int32.Parse(uptimeCount.Text) <= 0)
                return;

            // cycle
            if (powerOffBtn.Checked) testString += "powerOff; ";
            else if (ethOffBtn.Checked) testString += "ethOff; ";
            else testString += "noCycle; ";

            // connect
            if (connectCheck.Checked) testString += "connect; ";

            // actions
            if (Int32.Parse(timeCount.Text) > 0) testString += $"time {timeCount.Text}; ";
            if (Int32.Parse(uptimeCount.Text) > 0) testString += $"uptime {uptimeCount.Text}; ";
            if (Int32.Parse(voltageCount.Text) > 0) testString += $"voltage {voltageCount.Text}; ";
            if (Int32.Parse(infoCount.Text) > 0) testString += $"info {infoCount.Text}; ";
            if (Int32.Parse(pingCount.Text) > 0) testString += $"ping {pingCount.Text}; ";

            // params
            if (saveHgmCheck.Checked) testString += "saveHgm=1; ";
            else testString += "saveHgm=0; ";
            if (saveBinCheck.Checked) testString += "saveBin=1; ";
            else testString += "saveBin=0; ";
            if (saveDatCheck.Checked) testString += "saveDat=1; ";
            else testString += "saveDat=0; ";

            // time
            testString += $"{intervalSetting.Text}h";


            Hide(); 


        }
    }
}

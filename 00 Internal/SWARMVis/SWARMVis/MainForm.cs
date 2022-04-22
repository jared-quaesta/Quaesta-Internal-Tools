using QIRestfulSwarm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using System.Globalization;

namespace SWARMVis
{
    public partial class MainForm : Form
    {

        private string lastDevString;
        private string lastMsgString;
        private RestfulSwarm rs;
        private LoginForm loginForm;

        private SQLMan sqlMan = new SQLMan();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // spawn login form
            loginForm = new LoginForm(this);
            loginForm.ShowDialog();

        }

        internal async void ValidateCredentials(RestfulSwarm rs)
        {
            this.rs = rs;
            authLbl.Text = "Authorized";
            authLbl.ForeColor = Color.Green;
            authButton.Text = "Log Out";


            // after login initial counts and message info

            string deviceInfo = await Task.Run(() =>
            {
                return rs.GetDevices();
            });
            
            int numMsg = await Task.Run(() =>
            {
                return rs.GetMsgCount();
            });

            lastMsgString = await Task.Run(() =>
            {
                return rs.GetMsgs();
            });


            lastDevString = deviceInfo;
            dynamic jObj = JsonConvert.DeserializeObject(deviceInfo);


            availDevsLbl.Text = $"Available Devices: {jObj.Count}";
            msgAvailLbl.Text = $"Available Messages: {numMsg}";


        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
            if (rs.signedIn)
            {
                rs.LogoutAsync();
                authLbl.Text = "Logged Out";
                authLbl.ForeColor = Color.Red;
                authButton.Text = "Log in";
                availDevsLbl.Text = $"Available Devices:";
                msgAvailLbl.Text = $"Available Messages:";
            }
            else
            {
                loginForm.ShowDialog();
            }   
        }

        private async void allMsgButton_Click(object sender, EventArgs e)
        {
            if (!rs.signedIn)
            {
                logOutButton_Click(null, null);
            }
            string rawOut = await Task.Run(() =>
            {
                return rs.GetMsgs();
            });

            lastMsgString = rawOut;

            string parsedOut = ParseOutput(rawOut);

            rawOutBox.Text = parsedOut + Environment.NewLine + Environment.NewLine;

            GetMessageForm getMsgForm = new GetMessageForm(lastDevString, rawOut);
            getMsgForm.ShowDialog();


        }

        private string ParseOutput(string rawOut)
        {
            string viewFriendlyString = "";
            string[] splitByMessage = rawOut.Substring(2).Split('}');

            int numObj = 0;

            viewFriendlyString += "[";
            foreach (string obj in splitByMessage)
            {
                numObj++;
                string[] individualEntrySplit = obj.Split(',');
                
                foreach (string val in individualEntrySplit)
                    viewFriendlyString += Environment.NewLine + val + ',';
                

                if (splitByMessage.Length > numObj)
                    viewFriendlyString += $"{Environment.NewLine}}}{Environment.NewLine}";

            }

            return viewFriendlyString.Substring(0, viewFriendlyString.Length-5) + 
                ']' + Environment.NewLine + Environment.NewLine;
        }

        private async void allDevsButton_Click(object sender, EventArgs e)
        {
            if (!rs.signedIn)
            {
                logOutButton_Click(null, null);
            }
            string rawOut = await Task.Run(() =>
            {
                return rs.GetDevices();
            });

            lastDevString = rawOut;

            string parsedOut = ParseOutput(rawOut);

            rawOutBox.Text = parsedOut + Environment.NewLine + Environment.NewLine;

            GetDeviceForm getDevForm = new GetDeviceForm(rawOut, lastMsgString);
            getDevForm.ShowDialog();
        }

        private async void refreshBtn_Click(object sender, EventArgs e)
        {
            if (!rs.signedIn)
            {
                logOutButton_Click(null, null);
                return;
            }

            string deviceInfo = await Task.Run(() =>
            {
                return rs.GetDevices();
            });

            int numMsg = await Task.Run(() =>
            {
                return rs.GetMsgCount();
            });

            lastMsgString = await Task.Run(() =>
            {
                return rs.GetMsgs();
            });


            lastDevString = deviceInfo;
            dynamic jObj = JsonConvert.DeserializeObject(deviceInfo);


            availDevsLbl.Text = $"Available Devices: {jObj.Count}";
            msgAvailLbl.Text = $"Available Messages: {numMsg}";
        }

        private void refServerBtn_Click(object sender, EventArgs e)
        {
            // see if server is online
            if (!sqlMan.CheckServer())
            {
                MessageBox.Show("Could not communicate with server. Is it on?");
                return;
            }

            // refresh cur client info
            refreshBtn_Click(null, null);

            // convert to json obj

            dynamic msgJsonObj = JsonConvert.DeserializeObject(lastMsgString);
            dynamic devJsonObj = JsonConvert.DeserializeObject(lastDevString);

            rawOutBox.Text = "Updating Devices on Local Server";
            // call refresh server on each msg
            foreach (dynamic obj in devJsonObj)
            {
                int deviceType = Int32.Parse(obj.deviceType.ToString());
                int deviceId = Int32.Parse(obj.deviceId.ToString());
                string deviceName = obj.deviceName.ToString();
                string comments = obj.comments.ToString();
                DateTime hiveCreationTime = FixDateTime(obj.hiveCreationTime.ToString());
                DateTime hiveLastheardTime = FixDateTime(obj.hiveLastheardTime.ToString());
                string firmwareVersion = obj.firmwareVersion.ToString();
                string hardwareVersion = obj.hardwareVersion.ToString();
                int lastTelemetryReportPacketId = Int32.Parse(obj.lastTelemetryReportPacketId.ToString());
                int lastHeardByDeviceType = Int32.Parse(obj.lastHeardByDeviceType.ToString());
                int lastHeardByDeviceId = Int32.Parse(obj.lastHeardByDeviceId.ToString());
                int counter = Int32.Parse(obj.counter.ToString());
                int dayofyear = Int32.Parse(obj.dayofyear.ToString());
                int lastHeardCounter = Int32.Parse(obj.lastHeardCounter.ToString());
                int lastHeardDayofyear = Int32.Parse(obj.lastHeardDayofyear.ToString());
                int lastHeardByGroundstationId = Int32.Parse(obj.lastHeardByGroundstationId.ToString());
                int status = Int32.Parse(obj.status.ToString());

                // bits

                int twoWayEnabled = 0;
                if (obj.twoWayEnabled.ToString().Equals("TRUE")) twoWayEnabled = 1;
                int dataEncryptionEnabled = 0;
                if (obj.dataEncryptionEnabled.ToString().Equals("TRUE")) dataEncryptionEnabled = 1;

                sqlMan.RefreshDevs(
                    deviceType,
                    deviceId,
                    deviceName,
                    comments,
                    hiveCreationTime,
                    hiveLastheardTime,
                    firmwareVersion,
                    hardwareVersion,
                    lastTelemetryReportPacketId,
                    lastHeardByDeviceType,
                    lastHeardByDeviceId,
                    counter,
                    dayofyear,
                    lastHeardCounter,
                    lastHeardDayofyear,
                    lastHeardByGroundstationId,
                    status,
                    twoWayEnabled,
                    dataEncryptionEnabled
                );
            }
            rawOutBox.Text = "Updating Messages on Local Server";
            foreach (dynamic obj in msgJsonObj)
            {

                int packetId = Int32.Parse( obj.packetId.ToString() );
                int deviceType = Int32.Parse( obj.deviceType.ToString() );
                int deviceId = Int32.Parse( obj.deviceId.ToString() );
                int viaDeviceId = Int32.Parse( obj.viaDeviceId.ToString() );
                int dataType = Int32.Parse( obj.dataType.ToString() );
                int userApplicationId = Int32.Parse( obj.userApplicationId.ToString() );
                int organizationId = Int32.Parse( obj.organizationId.ToString() );
                int len = Int32.Parse( obj.len.ToString() );

                // bits

                int ackPacketId = Int32.Parse(obj.ackPacketId.ToString());
                int status = Int32.Parse(obj.status.ToString());

                

                // datetime
                DateTime hiveRxTime = DateTime.Parse(obj.hiveRxTime.ToString());

                byte[] data = Convert.FromBase64String(obj.data.ToString());

                sqlMan.RefreshMsgs(
                    packetId,
                    deviceType,
                    deviceId,
                    viaDeviceId,
                    dataType,
                    userApplicationId,
                    organizationId,
                    len,
                    ackPacketId,
                    status,
                    hiveRxTime,
                    data);

            }

            rawOutBox.Text = "Updated Local Server";

        }

        private DateTime FixDateTime(string dt)
        {
            return DateTime.Parse(dt);
        }
    }
}

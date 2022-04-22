using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWARMVis
{
    public partial class GetDeviceForm : Form
    {
        private string devInfoRaw;
        private string msgInfoRaw;
        dynamic devJsonObj;
        dynamic msgJsonObj;

        ListBox availDevicesLBox;
        public GetDeviceForm(string devInfo, string msgInfo)
        {
            devInfoRaw = devInfo;
            msgInfoRaw = msgInfo;
            devJsonObj = JsonConvert.DeserializeObject(devInfoRaw);
            msgJsonObj = JsonConvert.DeserializeObject(msgInfoRaw);
            InitializeComponent();
        }

        private void GetDeviceForm_Load(object sender, EventArgs e)
        {
            InitDevLBox();
            exportButton.Location = new Point(270, 20);
            errMsg.Location = new Point(270, 50);
        }

        private void InitDevLBox()
        {
            // create avail devs clbox
            availDevicesLBox = new ListBox();
            availDevicesLBox.SelectionMode = SelectionMode.MultiSimple;

            // Set the location and size.
            availDevicesLBox.Location = new Point(20, 20);
            availDevicesLBox.Size = new Size(240, 540);

            // populate lb
            foreach (dynamic item in devJsonObj)
            {
                availDevicesLBox.Items.Add(item.comments + Environment.NewLine + item.deviceId);
            }

            // Turn off the scrollbar.
            availDevicesLBox.ScrollAlwaysVisible = false;

            // Set the border style to a single, flat border.
            availDevicesLBox.BorderStyle = BorderStyle.FixedSingle;

            // Set the DrawMode property to the OwnerDrawVariable value. 
            // This means the MeasureItem and DrawItem events must be 
            // handled.
            availDevicesLBox.DrawMode = DrawMode.OwnerDrawVariable;
            availDevicesLBox.MeasureItem +=
                new MeasureItemEventHandler(LBox_MeasureItem);
            availDevicesLBox.DrawItem += new DrawItemEventHandler(LBox_DrawItem);

            Controls.Add(availDevicesLBox);
        }

        private void LBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            // Cast the sender object back to ListBox type
            ListBox theListBox = (ListBox)sender;

            string itemString = theListBox.Items[e.Index].ToString();

            string[] resultStrings = itemString.Split(Environment.NewLine);

            e.ItemHeight = 19; // base height --
            e.ItemHeight *= resultStrings.Length;
        }
        //DrawItem event handler for your ListBox
        private void LBox_DrawItem(object sender, DrawItemEventArgs e)
        {

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
                Font, Brushes.White, e.Bounds.X, e.Bounds.Y);
            }
            else
            {
                // Otherwise, draw the rectangle filled in beige.
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
                Font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            errMsg.Text = "";
            if (availDevicesLBox.SelectedItems.Count == 0)
            {
                errMsg.Text = "Select at least one device to export.";
                return;
            }
            if (expPath.Text.Length == 0)
            {
                errMsg.Text = "Enter a valid filename and path below.";
                return;
            }

            // get selected items and export to csv file

            // make file
            string path = expPath.Text;
            string toWrite = GetSelectedInfo();
            using (FileStream fs = File.Create(path))
            {

                string header = $"deviceType," +
                    $"deviceId," +
                    $"deviceName," +
                    $"comments," +
                    $"hiveCreationTime," +
                    $"hiveLastheardTime," +
                    $"firmwareVersion," +
                    $"hardwareVersion," +
                    $"lastTelemetryReportPacketId," +
                    $"lastHeardByDeviceType" +
                    $",lastHeardByDeviceId" +
                    $",counter" +
                    $",dayofyear" +
                    $",lastHeardCounter" +
                    $",lastHeardDayofyear" +
                    $",lastHeardByGroundstationId" +
                    $",status" +
                    $",twoWayEnabled" +
                    $",dataEncryptionEnabled" +
                    $"\n"; ;

                byte[] info = new UTF8Encoding(true).GetBytes(toWrite);
                byte[] headerb = new UTF8Encoding(true).GetBytes(header);
                // Add some information to the file.

                fs.Write(headerb, 0, headerb.Length);
                fs.Write(info, 0, info.Length);
            }
            Close();
            OpenFolder(folderBrowser.SelectedPath);
        }

        private void OpenFolder(string folderPath)
        {
            
            ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }

        private string GetSelectedInfo()
        {
            string ret = "";
            foreach (dynamic obj in devJsonObj)
            {
                string deviceType = obj.deviceType.ToString();
                string deviceId = obj.deviceId.ToString();
                string deviceName = obj.deviceName.ToString();
                string comments = obj.comments.ToString();
                string hiveCreationTime = obj.hiveCreationTime.ToString();
                string hiveLastheardTime = obj.hiveLastheardTime.ToString();
                string firmwareVersion = obj.firmwareVersion.ToString();
                string hardwareVersion = obj.hardwareVersion.ToString();
                string lastTelemetryReportPacketId = obj.lastTelemetryReportPacketId.ToString();
                string lastHeardByDeviceType = obj.lastHeardByDeviceType.ToString();
                string lastHeardByDeviceId = obj.lastHeardByDeviceId.ToString();
                string counter = obj.counter.ToString();
                string dayofyear = obj.dayofyear.ToString();
                string lastHeardCounter = obj.lastHeardCounter.ToString();
                string lastHeardDayofyear = obj.lastHeardDayofyear.ToString();
                string lastHeardByGroundstationId = obj.lastHeardByGroundstationId.ToString();
                string status = obj.status.ToString();
                string twoWayEnabled = obj.twoWayEnabled.ToString();
                string dataEncryptionEnabled = obj.dataEncryptionEnabled.ToString();

                ret += $"{deviceType}," +
                    $"{deviceId}," +
                    $"{deviceName}," +
                    $"{comments}," +
                    $"{hiveCreationTime}," +
                    $"{hiveLastheardTime}," +
                    $"{firmwareVersion}," +
                    $"{hardwareVersion}," +
                    $"{lastTelemetryReportPacketId}," +
                    $"{lastHeardByDeviceType}" +
                    $",{lastHeardByDeviceId}" +
                    $",{counter}" +
                    $",{dayofyear}" +
                    $",{lastHeardCounter}" +
                    $",{lastHeardDayofyear}" +
                    $",{lastHeardByGroundstationId}" +
                    $",{status}" +
                    $",{twoWayEnabled}" +
                    $",{dataEncryptionEnabled}" +
                    $"\n";
            }

            return ret;
        }

        private void findPath_Click(object sender, EventArgs e)
        {
            folderBrowser.RootFolder = Environment.SpecialFolder.Desktop;
            folderBrowser.ShowDialog();
            expPath.Text = folderBrowser.SelectedPath.Trim() + @"\SWARMDDevs.csv";
        }
    }
}

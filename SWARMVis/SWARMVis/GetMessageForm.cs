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
    public partial class GetMessageForm : Form
    {
        private string devInfoRaw;
        private string msgInfoRaw;
        private dynamic msgJsonObj;
        private dynamic devJsonObj;

        private ListBox availDevicesLBox;
        private ListBox availMsgLBox;

        // annoying, but newline characters wont match up for some reason.
        // simple dictionary with { index: message (b64)}
        private Dictionary<int, string> selectionMap = new Dictionary<int, string>();


        // DEV RAW
        /*
             * [
                  {
                    "deviceType": 1,
                    "deviceId": 2885,
                    "deviceName": "F-0x00b45",
                    "comments": "QUAESTA Instruments Early Access Tile #2",
                    "hiveCreationTime": "2021-04-14T21:54:56",
                    "hiveFirstheardTime": "2021-10-20T22:18:50",
                    "hiveLastheardTime": "2021-10-22T22:41:34",
                    "firmwareVersion": "v1.0.0",
                    "hardwareVersion": "Tile A4",
                    "lastTelemetryReportPacketId": 15084641,
                    "lastHeardByDeviceType": 3,
                    "lastHeardByDeviceId": 1378,
                    "counter": 0,
                    "dayofyear": 0,
                    "lastHeardCounter": 14,
                    "lastHeardDayofyear": 293,
                    "lastHeardByGroundstationId": 177915,
                    "status": 0,
                    "twoWayEnabled": false,
                    "dataEncryptionEnabled": true
                  },
                  {
                    "deviceType": 1,
                    "deviceId": 2892,
                    "deviceName": "F-0x00b4c",
                    "comments": "QUAESTA Instruments Early Access Tile #1",
                    "hiveCreationTime": "2021-04-15T00:10:33",
                    "hiveLastheardTime": "2021-05-05T19:30:57",
                    "firmwareVersion": "v1.0.0",
                    "hardwareVersion": "Tile A4",
                    "lastTelemetryReportPacketId": 7655384,
                    "lastHeardByDeviceType": 3,
                    "lastHeardByDeviceId": 1286,
                    "counter": 0,
                    "dayofyear": 0,
                    "lastHeardCounter": 8,
                    "lastHeardDayofyear": 125,
                    "lastHeardByGroundstationId": 178439,
                    "status": 0,
                    "twoWayEnabled": false,
                    "dataEncryptionEnabled": true
                  }
                ]*/


        // MSG RAW
        /*
         * [
              {
                "packetId": 15084637,
                "deviceType": 1,
                "deviceId": 2885,
                "viaDeviceId": 177915,
                "dataType": 6,
                "userApplicationId": 2289,
                "organizationId": 2289,
                "len": 12,
                "data": "SGVsbG8gV29ybGQh",
                "ackPacketId": 0,
                "status": 0,
                "hiveRxTime": "2021-10-20T22:18:50"
              },
              {
                "packetId": 15084639,
                "deviceType": 1,
                "deviceId": 2885,
                "viaDeviceId": 177915,
                "dataType": 6,
                "userApplicationId": 2289,
                "organizationId": 2289,
                "len": 12,
                "data": "SGVsbG8gV29ybGQh",
                "ackPacketId": 0,
                "status": 0,
                "hiveRxTime": "2021-10-20T22:18:51"
              },
              {
                "packetId": 15124811,
                "deviceType": 1,
                "deviceId": 2885,
                "viaDeviceId": 177915,
                "dataType": 6,
                "userApplicationId": 2289,
                "organizationId": 2289,
                "len": 192,
                "data": "MF8wMTIzNDU2Nzg5YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXpBQkNERUZHSElKS0xNTk9QUVJTVFVWV1hZWjAxMjM0NTY3ODlhYmNkZWZnaGlqa2xtbm9wcXJzdHV2d3h5ekFCQ0RFRkdISUpLTE1OT1BRUlNUVVZXWFlaMDEyMzQ1Njc4OWFiY2RlZmdoaWprbG1ub3BxcnN0dXZ3eHl6QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVowMTIz",
                "ackPacketId": 0,
                "status": 0,
                "hiveRxTime": "2021-10-21T18:13:16"
              }
            ]
         */

        public GetMessageForm(string devInfo, string msgInfo)
        {
            devInfoRaw = devInfo;
            msgInfoRaw = msgInfo;
            devJsonObj = JsonConvert.DeserializeObject(devInfoRaw);
            msgJsonObj = JsonConvert.DeserializeObject(msgInfoRaw);
            InitializeComponent();

        }

        private void availDevicesLBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        //First add some items to your ListBox1.Items     
        //MeasureItem event handler for your ListBox
        private void LBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            // Cast the sender object back to ListBox type.
            ListBox theListBox = (ListBox)sender;

            // Get the string contained in each item.
            string itemString = theListBox.Items[e.Index].ToString();

            // Split the string at the " . "  character.
            string[] resultStrings = itemString.Split(Environment.NewLine);

            // If the string contains more than one period, increase the 
            // height by ten pixels; otherwise, increase the height by 
            // five pixels.

            e.ItemHeight = 19;
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

        private void GetMessageForm_Load(object sender, EventArgs e)
        {
            exportButton.Location = new Point(270 + 250, 20);
            decodeAscii.Location = new Point(270 + 250, 50);
            decodeAscii.Checked = true;
            decodeHex.Checked = true;
            decodeHex.Location = new Point(270 + 250, 70);
            errMsg.Location = new Point(270 + 250, 150);
            InitDevLBox();
            InitMsgLBox();
            
        }

        private void InitMsgLBox()
        {
            // create avail devs clbox
            availMsgLBox = new ListBox();
            availMsgLBox.SelectionMode = SelectionMode.MultiSimple;

            // Set the location and size.
            availMsgLBox.Location = new Point(270, 20);
            availMsgLBox.Size = new Size(240, 540);

            
            // Turn off the scrollbar.
            availMsgLBox.ScrollAlwaysVisible = false;

            // Set the border style to a single, flat border.
            availMsgLBox.BorderStyle = BorderStyle.FixedSingle;

            // Set the DrawMode property to the OwnerDrawVariable value. 
            // This means the MeasureItem and DrawItem events must be 
            // handled.
            availMsgLBox.DrawMode = DrawMode.OwnerDrawVariable;
            availMsgLBox.MeasureItem +=
                new MeasureItemEventHandler(LBox_MeasureItem);
            availMsgLBox.DrawItem += new DrawItemEventHandler(LBox_DrawItem);

            Controls.Add(availMsgLBox);
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
            availDevicesLBox.SelectedIndexChanged += new EventHandler(UpdateAvailMsgs);

            Controls.Add(availDevicesLBox);
        }

        private void UpdateAvailMsgs(object sender, EventArgs e)
        {
            availMsgLBox.Items.Clear();
            int index = 0;
            selectionMap.Clear();
            foreach (object selected in availDevicesLBox.SelectedItems)
            {
                string id = selected.ToString().Split(Environment.NewLine)[1];
                foreach (dynamic item in msgJsonObj)
                {
                    if (item.deviceId.ToString().Equals(id))
                    {
                        string formatData = "";
                        int count = 0;
                        foreach (char b in item.data.ToString())
                        {
                            formatData += b;
                            if (count == 25)
                            {
                                formatData += Environment.NewLine;
                                count = 0;
                            }
                            count++;
                        }
                        selectionMap.Add(index, item.packetId.ToString());
                        availMsgLBox.Items.Add($"From {id}: {Environment.NewLine}{formatData}");
                        index++;
                    }
            }
            }
           
        }

        private void findPath_Click(object sender, EventArgs e)
        {
            folderBrowser.RootFolder = Environment.SpecialFolder.Desktop;
            folderBrowser.ShowDialog();
            expPath.Text = folderBrowser.SelectedPath.Trim() + @"\SWARMRetrieval.csv";
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            errMsg.Text = "";
            if (availMsgLBox.SelectedItems.Count == 0)
            {
                errMsg.Text = "Select at least one message to export.";
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
                
                string header = "packetId,deviceType,deviceId,viaDeviceId,dataType,userApplicationId,organizationId,len,ackPacketId,status,hiveRxTime,data (b64)";

                if (decodeHex.Checked && decodeAscii.Checked)
                    header += ",data (ascii),data (hex)\n";
                
                else if (decodeAscii.Checked)
                    header += ",data(ascii)\n";
                
                else if (decodeHex.Checked)
                    header += ",data (hex)\n";
                
                else
                    header += '\n';
                

                byte[] info = new UTF8Encoding(true).GetBytes(toWrite);
                byte[] headerb = new UTF8Encoding(true).GetBytes(header);
                // Add some information to the file.

                fs.Write(headerb, 0, headerb.Length);
                fs.Write(info, 0, info.Length);
            }

            // Open the stream and read it back.
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
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
            foreach (string selMessage in availMsgLBox.SelectedItems)
            {
                
                int index = availMsgLBox.Items.IndexOf(selMessage);
                string id = selectionMap[index];
                foreach (dynamic msg in msgJsonObj)
                {
                    string data = msg.data.ToString();
                    string packetId = msg.packetId.ToString();
                    string deviceType = msg.deviceType.ToString();
                    string deviceId = msg.deviceId.ToString();
                    string viaDeviceId = msg.viaDeviceId.ToString();
                    string dataType = msg.dataType.ToString();
                    string userApplicationId = msg.userApplicationId.ToString();
                    string organizationId = msg.organizationId.ToString();
                    string len = msg.len.ToString();
                    string ackPacketId = msg.ackPacketId.ToString();
                    string status = msg.status.ToString();
                    string hiveRxTime = msg.hiveRxTime.ToString();


                    if (msg.packetId.ToString().Equals(id))
                    {

                        ret += $"{packetId},{deviceType},{deviceId},{viaDeviceId},{dataType},{userApplicationId},{organizationId},{len},{ackPacketId},{status},{hiveRxTime},{data}";

                        if (decodeHex.Checked && decodeAscii.Checked)
                        {
                            var base64EncodedBytes = Convert.FromBase64String(data);
                            string asciidata = Encoding.UTF8.GetString(base64EncodedBytes);

                            byte[] bytes = Convert.FromBase64String(data);
                            string hexdata = BitConverter.ToString(bytes);

                            ret += $",{asciidata},{hexdata}\n";
                        }

                        else if (decodeAscii.Checked)
                        {
                            var base64EncodedBytes = Convert.FromBase64String(data);
                            string asciidata = Encoding.UTF8.GetString(base64EncodedBytes);

                            ret += $",{asciidata}\n";
                        }

                        else if (decodeHex.Checked)
                        {
                            byte[] bytes = Convert.FromBase64String(data);
                            string hexdata = BitConverter.ToString(bytes);

                            ret += $",{hexdata}\n";
                        }

                        else
                            ret += '\n';


                    }
                }
                
            }
            return ret;
        }

        private void decodeCB_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

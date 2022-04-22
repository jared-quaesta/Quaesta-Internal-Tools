using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniversalUpdate.Serial;
using UniversalUpdate.SQL;

namespace UniversalUpdate
{
    public partial class MainForm : Form
    {
        string curFWPath = "";
        Dictionary<string, string> formatDict = new Dictionary<string, string>();
        public MainForm()
        {
            InitializeComponent();
        }

        private async void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshToolStripMenuItem.Enabled = false;
            availComs.Items.Clear();
            int finished = 0;
            List<string> validNPMs = new List<string>();
            List<SerialNPMManager> serialMans = new List<SerialNPMManager>();
            foreach (Tuple<string, string> com in SerialNPMManager.GetComs("STMicroelectronics"))
            {
                // get sn, app to com number
                serialMans.Add(new SerialNPMManager(new SerialListener(), com.Item1));
                //
            }
            int numUpdating = serialMans.Count;
            foreach (SerialNPMManager serialMan in serialMans)
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Task.Run(() =>
                {
                    int att = 0;

                    bool conn = serialMan.Connect(serialMan.com);
                    while (!serialMan.IsConnected())
                    {
                        if (att == 10)
                        {
                            finished++;
                            break;
                        }
                        att++;
                        Thread.Sleep(500);
                        conn = serialMan.Connect(serialMan.com);
                    }
                    if (!serialMan.IsConnected()) return;
                    serialMan.SendCommand("info\r\n");
                    Thread.Sleep(100);
                    att = 0;
                    while (serialMan.listener.GetInfo().Contains("Unknown") || serialMan.listener.GetInfo().Trim().Length == 0)
                    {
                        if (att == 5) break;
                        att++;
                        serialMan.listener.Clearinfo();
                        serialMan.SendCommand("info\r\n");
                        Thread.Sleep(500);
                    }

                    serialMan.listener.ParseInfo();
                    string serial = serialMan.listener.GetSerial();

                    finished++;
                    validNPMs.Add($"{serialMan.com} : {serial}");
                    serialMan.Disconnect();
                });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
            await Task.Run(() =>
            {
                while (numUpdating != finished)
                {
                    Thread.Sleep(10);
                }
            });
            validNPMs.Sort();
            foreach (string npm in validNPMs)
            {
                availComs.Items.Add(npm);
            }
            refreshToolStripMenuItem.Enabled = true;
            avail.Text = $"Available NPMs ({availComs.Items.Count} Connected)";

        }

        private void SelAll(object sender, EventArgs e)
        {
            for (int i = 0; i < availComs.Items.Count; i++)
            {
                availComs.SetItemChecked(i, true);
            }
        }

        private void SelNone(object sender, EventArgs e)
        {
            for (int i = 0; i < availComs.Items.Count; i++)
            {
                availComs.SetItemChecked(i, false);
            }
        }

        private void crpDrag_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void crpDrag_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files != null && files.Any())
            {
                crpDrag.Text = Path.GetFileName(files.First());
                curFWPath = files.First();
            }
        }

        private async void manSelectBtn_Click(object sender, EventArgs e)
        {
            DialogResult res = fileDialog.ShowDialog();

            if (res != DialogResult.Cancel && res != DialogResult.Abort)
            {
                if (File.Exists(fileDialog.FileName) && fileDialog.FileName.ToLower().Contains(".crp"))
                {
                    crpDrag.Text = Path.GetFileName(fileDialog.FileName);
                    curFWPath = fileDialog.FileName;
                }
                else
                {
                    MessageBox.Show($"Invalid File: \n'{fileDialog.FileName}'", "ERROR");
                }
            }

        }

        private async void updateBtn_ClickAsync(object sender, EventArgs e)
        {

            if (availComs.CheckedItems.Count == 0)
            {
                MessageBox.Show("No Devices Selected", "ERROR");
                return;
            }

            if (refServerCheck.Checked)
            {
                if (desSn.Text.Length == 0)
                {
                    MessageBox.Show("Indicated serial number change but has not entered a valid format.", "ERROR");
                    return;
                }
                if (desEx.Text.Contains("ERR") || prevEx.Text.Contains("ERR"))
                {
                    MessageBox.Show("Invalid format.", "ERROR");
                    return;
                }
            }
            

            if (curFWPath.Equals("") && showWarningsToolStripMenuItem.Checked)
            {
                if (MessageBox.Show("No Firmware Selected. Proceed anyway?", "WARNING", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
            }
            if (addCommandsBox.Text.Length == 0 && showWarningsToolStripMenuItem.Checked)
            {
                if (MessageBox.Show("No Additional Commands Entered. Proceed anyway?", "WARNING", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
            }



            bool serialize = refServerCheck.Checked;
            bool prevID = prevSn.Text.Length > 0;
            List<Tuple<string, string, string>> comData = new List<Tuple<string, string, string>>();
            updateBtn.Enabled = false;

            string[] commands = addCommandsBox.Text.Trim().Split('\n');
            string type = typeBox.Text;

            int numUpdating = availComs.CheckedItems.Count;
            int finished = 0;


            List<SerialNPMManager> serialManagers = new List<SerialNPMManager>();

            // make serialmans for each com port
            foreach (string com in availComs.CheckedItems)
            {
                string comport = com.Split(':')[0].Trim();
                SerialListener listener = new SerialListener();
                SerialNPMManager serialMan = new SerialNPMManager(listener, comport);
                serialManagers.Add(serialMan);
            }


            //perform pre-flight safety checks (can I connect?)

            bool canConnect = true;
            string coms = "";
            int conFinished = 0;
            int conTotal = serialManagers.Count;
            progTxt.Text = "Checking Connections....";
            foreach (SerialNPMManager serialMan in serialManagers)
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                // begin a thread for each device to update 
                Task.Run(() =>
                {
                    int att = 0;
                    while (!serialMan.IsConnected())
                    {
                        if (att == 10)
                        {
                            finished++;
                            Debug.WriteLine($"Failed to connect to {serialMan.com}");
                            coms += serialMan.com + "\n";
                            canConnect = false;
                            break;
                        }
                        att++;
                        bool conn = serialMan.Connect(serialMan.com);
                        //Debug.WriteLine($"Tried to Connect to {serialMan.com}... {conn}");
                        Thread.Sleep(1000);
                        
                    }
                    conFinished++;
                    //Debug.WriteLine($"initial Connection: {serialMan.IsConnected()}");
                });

            }
            await Task.Run(() =>
            {
                while (conTotal != conFinished)
                {
                    Thread.Sleep(10);
                }
            });
            if (!canConnect)
            {
                MessageBox.Show($"Could not connect to one or more npms: \n{coms}", "Error");
                updateBtn.Enabled = true;
                progTxt.Text = "Error";
                return;

            }
            ////////// FIRMWARE BIT
            if (!curFWPath.Equals(""))
            {
                byte[] allBytes = File.ReadAllBytes(curFWPath);
                int blockCount = allBytes.Length / 128;
                progTxt.Text = "Updating Firmware...";
                string firstcom = availComs.CheckedItems[0].ToString().Split(':')[0].Trim();
                foreach (SerialNPMManager serialMan in serialManagers)
                {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                    // begin a thread for each device to update 
                    Task.Run(() =>
                    {
                        //Debug.WriteLine($"Writing {allBytes.Length}b to {serialMan.com}");
                        int att = 0;
                        while (!serialMan.IsConnected())
                        {
                            if (att == 10)
                            {
                                finished++;
                                Debug.WriteLine($"Failed to connect to {serialMan.com}");
                                return;
                            }
                            att++;
                            bool conn = serialMan.Connect(serialMan.com);
                            //Debug.WriteLine($"Tried to Connect to {serialMan.com}... {conn}");
                            Thread.Sleep(1000);
                        }
                        //Debug.WriteLine($"initial Connection: {serialMan.IsConnected()}");
                        serialMan.ClearInput();

                        Stopwatch timeout = new Stopwatch();
                        bool gotC = false;

                        for (int i = 0; i < 3; i++)
                        {
                            timeout.Restart();
                            //Debug.WriteLine($"UpdateFirmware: {serialMan.com}");
                            serialMan.SendCommand("updatefirmware\r\n", 5);

                            //Debug.WriteLine($"Waiting for C {serialMan.com}");
                            while (!gotC)
                            {
                                if (timeout.ElapsedMilliseconds > 5000) break;
                                gotC = serialMan.listener.GotC();
                                Thread.Sleep(10);
                            }
                            if (gotC)
                                break;
                        }
                        //Debug.WriteLine($"Got C: {gotC}: {serialMan.com}");
                        if (!gotC)
                        {
                            // update error, too long to respond
                            // Debug.WriteLine($"C Timeout. {serialMan.com}");
                            finished++;
                            return;
                        }

                        int blockNum = 1;
                        //Debug.WriteLine($"Beginning transfer... {serialMan.com}");
                        for (int i = 0; i < allBytes.Length; i += 128)
                        {
                            byte[] buffer = new byte[128];

                            // get package from fw file bytes
                            for (int b = 0; b < 128; b++)
                            {
                                buffer[b] = allBytes[i + b];
                            }

                            byte[] block = new byte[133];

                            // make padding
                            block[0] = 0x01;
                            block[1] = 0x00;
                            block[2] = 0x00;
                            block[131] = 0x00;
                            block[132] = 0x00;

                            // fill in data
                            for (int j = 0; j < 128; j++)
                            {
                                block[j + 3] = buffer[j];
                            }

                            //NewCmd("", block);
                            serialMan.SendBytes(block, 0);
                            while (!serialMan.listener.GotACK())
                            {
                                Thread.Sleep(1);
                            }
                            //Debug.WriteLine($"Success {++blockNum} / {blockCount}");
                            Invoke((MethodInvoker)delegate
                                {
                                    progTxt.Text = $"Sending block {blockNum}/{blockCount}";
                                    progTxt.Refresh();
                                });
                            blockNum++;

                            // finish transfer
                        }

                        // Debug.WriteLine("Finishing Transfer...");
                        serialMan.SendBytes(new byte[] { 0x04 });
                        Thread.Sleep(100);
                        serialMan.SendBytes(new byte[] { 0x04 });

                        // Debug.WriteLine($"Waiting for confirmation... {serialMan.com}");
                        Invoke((MethodInvoker)delegate
                        {
                            progTxt.Text = "Finishing Update, Reconnecting...";
                            progTxt.Refresh();
                        });
                        Thread.Sleep(15000);

                        //Debug.WriteLine($"Got confirmation... {serialMan.com}");

                        try
                        {
                            serialMan.SendCommand("Y");
                            Thread.Sleep(100);

                        }
                        catch
                        {
                            // dev disconnected, no need for Y
                            Debug.WriteLine("Disconnect");
                        }

                        // reconnect now (device reboot doesnt update comport status)
                        try
                        {
                            while (!serialMan.Connect(serialMan.com))
                            {
                                Thread.Sleep(2000);
                                Debug.WriteLine("Reconnecting...");
                                
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                        // open comport
                        serialMan.Disconnect();
                        finished++;

                    });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                }
            }
            else
            {
                finished = numUpdating;
            }

            // Pause. Wait for update to finish. 
            await Task.Run(() =>
            {
                while (numUpdating != finished)
                {
                    Thread.Sleep(10);
                }
            });

            ///////////////////////////// BEGIN Extra CMDs ////////////////////////////////

            if (addCommandsBox.Text.Length != 0)
            {

                progTxt.Text = "Starting Extra Commands....";
                finished = 0;
                foreach (SerialNPMManager serialMan in serialManagers)
                {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    Task.Run(() =>
                    {
                        int att = 0;

                        bool conn = serialMan.Connect(serialMan.com);
                        while (!serialMan.IsConnected())
                        {
                            if (att == 10)
                            {
                                finished++;
                                break;
                            }
                            att++;
                            Thread.Sleep(1000);
                            conn = serialMan.Connect(serialMan.com);
                        }

                        if (!serialMan.IsConnected())
                        {
                            finished++;
                            return;
                        }

                        try
                        {

                            serialMan.ClearInput();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }



                        Stopwatch timeout = new Stopwatch();

                        if (secretCheck.Checked)
                        {
                            serialMan.AllowSecret();
                        }

                        foreach (string command in commands)
                        {
                            string fixedCmd = command.Trim('\r', '\n', ' ') + "\r\n";
                            serialMan.SendCommand(fixedCmd, 5);

                            Thread.Sleep(1000);

                            Invoke((MethodInvoker)delegate
                            {
                                progTxt.Text = $"Sending Command: {fixedCmd.Trim('\r', '\n', ' ')}";
                                progTxt.Refresh();
                            });
                        }


                        finished++;

                    });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                }
            }
            else
            {
                finished = numUpdating;
            }
            await Task.Run(() =>
            {
                while (numUpdating > finished)
                {
                    Thread.Sleep(10);
                }
            });


            /////////////////////////////// BEGIN SERIALIZATION

            int inc = 0;
            if (refServerCheck.Checked)
            {

                List<string> allSerials = SQLManager.GetAllSerials();
                foreach (SerialNPMManager serialMan in serialManagers)
                {
                    // connect
                    int att = 0;

                    bool conn = serialMan.Connect(serialMan.com);
                    while (!serialMan.IsConnected())
                    {
                        if (att == 10)
                        {
                            finished++;
                            break;
                        }
                        att++;
                        Thread.Sleep(1000);
                        conn = serialMan.Connect(serialMan.com);
                    }
                    serialMan.ClearInput();
                    serialMan.AllowSecret();
                    serialMan.ClearInput();

                    if (prevSn.Text.Length != 0)
                    {
                        serialMan.SendCommand("info\r\n");
                        Thread.Sleep(100);
                        att = 0;
                        while (serialMan.listener.GetInfo().Contains("Unknown") || serialMan.listener.GetInfo().Trim().Length == 0)
                        {
                            if (att == 5) break;
                            att++;
                            serialMan.listener.Clearinfo();
                            serialMan.SendCommand("info\r\n");
                            Thread.Sleep(500);
                        }
                        serialMan.listener.ParseInfo();
                        string curSerial = serialMan.listener.GetSerial();
                        string newSerial = ParseSerial(prevSn.Text, desSn.Text, allSerials, curSerial);
                        allSerials.Add(newSerial);
                        serialMan.ClearInput();
                        SQLManager.EditSerial(curSerial, newSerial);

                        progTxt.Text = $"Setting Serial #: {newSerial}";
                        progTxt.Update();

                        serialMan.SendCommand($"serialnumber={newSerial}\r\n");
                        serialMan.listener.ClearInfo();
                    }
                    else
                    {
                        string newSerial = ParseSerial(prevSn.Text, desSn.Text, allSerials);
                        allSerials.Add(newSerial);
                        serialMan.ClearInput();
                        Debug.WriteLine($"serialnumber={newSerial}\r\n");
                        serialMan.SendCommand($"serialnumber={newSerial}\r\n");

                        progTxt.Text = $"Setting Serial #: {newSerial}";
                        progTxt.Refresh();
                    }
                    serialMan.ClearInput();
                    serialMan.Disconnect();
                }
            }

            //////////////////// BEGIN GET INFO, ADD/EDIT SERVER ENTRY ///////

            finished = 0;
            foreach (SerialNPMManager serialMan in serialManagers)
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Task.Run(() =>
                {
                    int att = 0;

                    bool conn = serialMan.Connect(serialMan.com);
                    while (!serialMan.IsConnected())
                    {
                        if (att == 10)
                        {
                            finished++;
                            break;
                        }
                        att++;
                        Thread.Sleep(1000);
                        conn = serialMan.Connect(serialMan.com);
                    }
                    serialMan.SendCommand("info\r\n");
                    Thread.Sleep(100);
                    att = 0;
                    while (serialMan.listener.GetInfo().Contains("Unknown") || serialMan.listener.GetInfo().Trim().Length == 0)
                    {
                        if (att == 5) break;
                        att++;
                        serialMan.listener.Clearinfo();
                        serialMan.SendCommand("info\r\n");
                        Thread.Sleep(500);
                    }
                    
                    serialMan.listener.ParseInfo();
                    string serial = serialMan.listener.GetSerial();
                    string firmware = serialMan.listener.GetFirmware();
                    string model = serialMan.listener.GetModel();
                    comData.Add(new Tuple<string, string, string>(serialMan.com, firmware, serialMan.listener.GetInfo()));
                    SQLManager.AddRow(serial, type, model, firmware, DateTime.Now);
                    Invoke((MethodInvoker)delegate
                    {
                        progTxt.Text = $"Getting Info...";
                        progTxt.Refresh();
                    });
                    finished++;
                    serialMan.Disconnect();

                });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
            await Task.Run(() =>
            {
                while (numUpdating != finished)
                {
                    Thread.Sleep(10);
                }
            });

            ///////////////// FINISH
            if (comData.Count > 0)
            {
                Finished summaryPage = new Finished(comData);
                summaryPage.Show();
            }
            else
            {
                Debug.WriteLine("???");
            }
            progTxt.Text = "Waiting....";
            SelNone(null, null);

            updateBtn.Enabled = true;
            refreshToolStripMenuItem_Click(null, null);
        }

        private string ParseSerial(string prevSerialFormat, string desiredSerialFormat, List<string> allSerials, string curSerial = "", int overrideNext = -1)
        {
            bool reserialize = prevSerialFormat.Length > 0;
            int prevNext = 0;
            int next = 0;
            int checkRealIndex = 0;
            int len = 0;
            string ret = "";
            if (overrideNext != -1) next = overrideNext;

            if (!reserialize || overrideNext != -1)
            {
                do
                {
                    ret = "";
                    for (int i = 0; i < desiredSerialFormat.Length; i++)
                    {
                        if (desiredSerialFormat[i] != '{')
                        {
                            ret += desiredSerialFormat[i];
                        }
                        else
                        {
                            // check where next } is
                            string formatString = desSn.Text.Substring(i);

                            int endIndex = formatString.IndexOf('}');
                            i += endIndex;
                            formatString = formatString.Substring(1, endIndex - 1);

                            string[] splitDelim = formatString.Split(':');

                            string arg = "0";
                            if (splitDelim.Length > 1)
                            {
                                arg = splitDelim[1];
                                formatString = splitDelim[0];
                            }

                            if (formatString.ToLower().Equals("next"))
                            {
                                ret += $"{next.ToString().PadLeft(arg.Length, '0')}";
                            }
                            else
                            {
                                ret += $"{formatDict[formatString.ToLower()].PadLeft(arg.Length, '0')}";
                            }
                        }
                    }
                    if (overrideNext != -1)
                    {
                        //SQLManager.EditSerial(curSerial, ret);
                        return ret;
                    }

                    next++;

                } while (allSerials.Contains(ret));

                //SQLManager.AddRow($"{ret}", "test", "test", "test", DateTime.Now, "");
                return ret;
            }
            else
            {
                // test-0041
                int oldIndex = prevSerialFormat.IndexOf("old");
                int colIndex = prevSerialFormat.IndexOf(":");
                int endBracketIndex = prevSerialFormat.IndexOf("}");
                int argSub = prevSerialFormat.Substring(oldIndex, endBracketIndex - colIndex - 1).Length;
                if (int.TryParse(curSerial.Substring(oldIndex - 1, argSub), out int oldNum))
                {
                    return ParseSerial(prevSerialFormat, desiredSerialFormat, allSerials, "", oldNum);
                }
                else
                {
                    return (ParseSerial("", desiredSerialFormat, allSerials));
                }
            }
        }

        private void clrFWBtn_Click(object sender, EventArgs e)
        {
            curFWPath = "";
            crpDrag.Clear();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            refreshToolStripMenuItem_Click(null, null);
            formatDict.Add("next", "17");
            formatDict.Add("old", "17");
            formatDict.Add("yy", DateTime.Now.ToString("yy"));
            formatDict.Add("old_yy", DateTime.Now.ToString("yy"));
            formatDict.Add("yyyy", DateTime.Now.ToString("yyyy"));
            formatDict.Add("old_yyyy", DateTime.Now.ToString("yyyy"));
            formatDict.Add("dd", DateTime.Now.ToString("dd"));
            formatDict.Add("old_dd", DateTime.Now.ToString("dd"));
            formatDict.Add("mm", DateTime.Now.ToString("MM"));
            formatDict.Add("old_mm", DateTime.Now.ToString("MM"));
            formatDict.Add("easter", DateTime.Now.ToString(":)"));

        }

        private void showWarningsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
            optionsToolStripMenuItem.ShowDropDown();

        }

        private void refServerCheck_CheckedChanged(object sender, EventArgs e)
        {
            serverPanel.Visible = refServerCheck.Checked;
            if (refServerCheck.Checked && !secretCheck.Checked) secretCheck.Checked = true;
        }

        private void prevSn_TextChanged(object sender, EventArgs e)
        {
            // parse string in prevSn

            prevEx.Text = "ex: ";
            for (int i = 0; i < prevSn.Text.Length; i++)
            {
                if (prevSn.Text[i] != '{')
                {
                    prevEx.Text += prevSn.Text[i];
                }
                else
                {
                    // check where next } is
                    string formatString = prevSn.Text.Substring(i);
                    if (!formatString.Contains('}'))
                    {
                        return;
                    }
                    else
                    {
                        int endIndex = formatString.IndexOf('}');
                        i += endIndex;
                        formatString = formatString.Substring(1, endIndex - 1);

                        string[] splitDelim = formatString.Split(':');

                        string arg = "0";
                        if (splitDelim.Length > 1)
                        {
                            arg = splitDelim[1];
                            formatString = splitDelim[0];
                        }

                        if (formatString.ToLower().Equals("old"))
                        {
                            prevEx.Text += $"{formatDict[formatString.ToLower()].PadLeft(arg.Length, '0')}";
                        }
                        else
                        {
                            try
                            {


                                prevEx.Text += formatDict[formatString.ToLower()].PadLeft(arg.Length, '0');
                        } catch
                        {
                                prevEx.Text += "{ERR}";
                        }
                        }

                    }
                }
            }

        }

        private void desSn_TextChanged(object sender, EventArgs e)
        {
            desEx.Text = "ex: ";
            for (int i = 0; i < desSn.Text.Length; i++)
            {
                if (desSn.Text[i] != '{')
                {
                    desEx.Text += desSn.Text[i];
                }
                else
                {
                    // check where next } is
                    string formatString = desSn.Text.Substring(i);
                    if (!formatString.Contains('}'))
                    {
                        return;
                    }
                    else
                    {
                        int endIndex = formatString.IndexOf('}');
                        i += endIndex;
                        formatString = formatString.Substring(1, endIndex - 1);

                        string[] splitDelim = formatString.Split(':');

                        string arg = "0";
                        if (splitDelim.Length > 1)
                        {
                            arg = splitDelim[1];
                            formatString = splitDelim[0];
                        }

                        try
                        {
                            desEx.Text += $"{formatDict[formatString.ToLower()].PadLeft(arg.Length, '0')}";
                        }
                        catch (Exception ex)
                        {
                            desEx.Text += "{ERR}";
                            Debug.WriteLine(ex.Message);
                        }

                    }
                }
            }

        }

        private void secretCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (refServerCheck.Checked) secretCheck.Checked = true;
        }

        private void sTLinkFlashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            STLinkForm stlUtil = new STLinkForm();
            stlUtil.ShowDialog();
        }
    }
}

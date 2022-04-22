using System;
using System.Collections.Concurrent;
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
        Dictionary<char, string> formatDict = new Dictionary<char, string>();
        public MainForm()
        {
            InitializeComponent();
        }

        private async void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshToolStripMenuItem.Enabled = false;
            availComs.Items.Clear();
            List<SerialNPMManager> serialMans = new List<SerialNPMManager>();
            foreach (Tuple<string, string> com in SerialNPMManager.GetComs("STMicroelectronics"))
            {
                // get sn, app to com number
                serialMans.Add(new SerialNPMManager("unk", com.Item1));
                //
            }

            List<Thread> threads = new List<Thread>();
            BlockingCollection<string> coms = new BlockingCollection<string>();
            foreach (SerialNPMManager serialMan in serialMans)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    // connect
                    int att = 0;

                    serialMan.Connect(serialMan.com);
                    while (!serialMan.IsConnected())
                    {
                        if (att == 10)
                        {
                            break;
                        }
                        att++;
                        Thread.Sleep(500);
                        serialMan.Connect(serialMan.com);
                    }
                    if (!serialMan.IsConnected())
                    {
                        return;
                    }
                    // get info
                    serialMan.ClearInput();
                    serialMan.SendCommand("info\r");
                    Thread.Sleep(100);
                    att = 0;
                    while (serialMan.listener.GetSerial().Length == 0)
                    {
                        if (att == 10)
                        {
                            Debug.WriteLine("Unable to get " + serialMan.com);
                            break;
                        };
                        att++;
                        serialMan.listener.Clearinfo();
                        Thread.Sleep(30);
                        serialMan.SendCommand("info\r");
                        Thread.Sleep(30);
                        serialMan.SendCommand("info\r");
                        Thread.Sleep(30);
                        serialMan.listener.ParseInfo();
                    }

                    string serial = serialMan.listener.GetSerial();
                    att = 0;
                    while (!coms.TryAdd(serial + " : " + serialMan.com))
                    {
                        if (att++ == 10) break;
                        Thread.Sleep(30);
                    }
                    serialMan.Disconnect();
                });
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);
            }

            await Task.Run(() =>
            {
                foreach (var thread in threads)
                {
                    thread.Join();
                }
            });

            List<string> sortMe = new List<string>();
            foreach (string npm in coms)
            {
                sortMe.Add(npm);
            }
            sortMe.Sort();
            foreach (string i in sortMe)
            {
                availComs.Items.Add(i);
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
            updateBtn.Enabled = false;

            string[] commands = addCommandsBox.Text.Trim().Split('\n');
            string type = typeBox.Text;

            int numUpdating = availComs.CheckedItems.Count;
            int finished = 0;
            List<Thread> threads = new List<Thread>();

            List<SerialNPMManager> serialManagers = new List<SerialNPMManager>();

            // make serialmans for each com port
            foreach (string com in availComs.CheckedItems)
            {
                string comport = com.Split(':')[1].Trim();
                string serial = com.Split(':')[0].Trim();
                SerialListener listener = new SerialListener();
                SerialNPMManager serialMan = new SerialNPMManager(serial, comport);
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
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    // Connect to device.
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
                        Thread.Sleep(500);

                    }
                });
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);
            }

            await Task.Run(() =>
            {
                foreach (Thread t in threads)
                {
                    t.Join();
                }
            });
            threads.Clear();

            ////////// FIRMWARE BIT
            if (!curFWPath.Equals(""))
            {
                byte[] allBytes = File.ReadAllBytes(curFWPath);
                int blockCount = allBytes.Length / 128;
                progTxt.Text = "Updating Firmware...";
                string firstcom = availComs.CheckedItems[0].ToString().Split(':')[1].Trim();
                foreach (SerialNPMManager serialMan in serialManagers)
                {

                    ThreadStart threadDelegate = new ThreadStart(() =>
                    {
                        // Connect to device.
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
                            Thread.Sleep(500);

                        }

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

                            serialMan.SendBytes(block, 0);
                            while (!serialMan.listener.GotACK())
                            {
                                Thread.Sleep(1);
                            }
                            Invoke((MethodInvoker)delegate
                            {
                                progTxt.Text = $"Sending block {blockNum}/{blockCount}";
                                progTxt.Refresh();
                            });
                            blockNum++;

                            // finish transfer
                        }

                        serialMan.SendBytes(new byte[] { 0x04 });
                        Thread.Sleep(100);
                        serialMan.SendBytes(new byte[] { 0x04 });

                        Invoke((MethodInvoker)delegate
                        {
                            progTxt.Text = "Finishing Update, Reconnecting...";
                            progTxt.Refresh();
                        });
                        Thread.Sleep(15000);


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
                    });
                    Thread thread = new Thread(threadDelegate);
                    thread.Start();
                    threads.Add(thread);

                }
            }

            await Task.Run(() =>
            {
                foreach (Thread t in threads)
                {
                    t.Join();
                }
            });
            threads.Clear();


            ///////////////////////////// BEGIN Extra CMDs ////////////////////////////////

            if (addCommandsBox.Text.Length != 0)
            {

                progTxt.Text = "Starting Extra Commands....";
                finished = 0;
                foreach (SerialNPMManager serialMan in serialManagers)
                {
                    ThreadStart threadDelegate = new ThreadStart(() =>
                    {
                        // Connect to device.
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
                            Thread.Sleep(500);

                        }

                        serialMan.ClearInput();
                        Thread.Sleep(30);

                        if (secretCheck.Checked)
                        {
                            serialMan.AllowSecret();
                        }
                        serialMan.ClearInput();
                        Thread.Sleep(30);
                        foreach (string command in commands)
                        {
                            string fixedCmd = command.Trim('\r', '\n', ' ') + "\r\n";
                            serialMan.SendCommand(fixedCmd);
                            Thread.Sleep(30);
                            serialMan.SendCommand(fixedCmd);
                            Thread.Sleep(30);
                            serialMan.SendCommand(fixedCmd);
                            Thread.Sleep(30);

                            //Invoke((MethodInvoker)delegate
                            //{
                            //    progTxt.Text = $"Sending Command: {fixedCmd.Trim('\r', '\n', ' ')}";
                            //    progTxt.Refresh();
                            //});
                        }
                        Debug.WriteLine(serialMan.IsConnected() + ": " + serialMan.com);
                    });
                    Thread thread = new Thread(threadDelegate);
                    thread.Start();
                    threads.Add(thread);
                }

            }
            await Task.Run(() =>
            {
                foreach (Thread t in threads)
                {
                    t.Join();
                }
            });
            threads.Clear();


            if (refServerCheck.Checked)
            {
                /////////////////////////////// BEGIN SERIALIZATION
                ConcurrentDictionary<string, string> serialDict = new ConcurrentDictionary<string, string>();
                if (refServerCheck.Checked)
                {
                    List<string> allSerials = SQLManager.GetAllSerials();
                    string newSerial = "";
                    foreach (SerialNPMManager serialMan in serialManagers)
                    {
                        string curSerial = serialMan.GetSerial();
                        if (prevSn.Text.Length != 0)
                        {
                            newSerial = ParseSerial(prevSn.Text, desSn.Text, allSerials, curSerial);
                            allSerials.Add(newSerial);
                            serialMan.ClearInput();
                            SQLManager.EditSerial(curSerial, newSerial);

                            progTxt.Text = $"Setting Serial #: {newSerial}";
                            progTxt.Update();

                            //serialMan.SendCommand($"serialnumber={newSerial}\r\n");
                            serialMan.listener.ClearInfo();
                        }
                        else
                        {
                            newSerial = ParseSerial(prevSn.Text, desSn.Text, allSerials);
                            allSerials.Add(newSerial);
                            serialMan.ClearInput();
                            Debug.WriteLine($"serialnumber={newSerial}\r\n");
                            //serialMan.SendCommand($"serialnumber={newSerial}\r\n");

                            progTxt.Text = $"Setting Serial #: {newSerial}";
                            progTxt.Refresh();
                        }

                        while (!serialDict.TryAdd(serialMan.com, newSerial))
                            Thread.Sleep(100);
                    }

                }

                //////////////////// BEGIN GET INFO, ADD/EDIT SERVER ENTRY ///////

                foreach (SerialNPMManager serialMan in serialManagers)
                {
                    ThreadStart threadDelegate = new ThreadStart(() =>
                    {
                    // Connect to device.
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
                            Thread.Sleep(500);

                        }

                        serialMan.ClearInput();
                        Thread.Sleep(30);

                        serialMan.AllowSecret();
                        Thread.Sleep(30);
                        serialMan.ClearInput();
                        Thread.Sleep(30);
                        serialMan.SendCommand($"serialnumber = {serialDict[serialMan.com]}\r\n");

                    });
                    Thread thread = new Thread(threadDelegate);
                    thread.Start();
                    threads.Add(thread);
                }

                await Task.Run(() =>
                {
                    foreach (Thread t in threads)
                    {
                        t.Join();
                    }
                });
                threads.Clear();
            }

            BlockingCollection<Tuple<string, string, string>> comData = new BlockingCollection<Tuple<string, string, string>>();
            foreach (SerialNPMManager serialMan in serialManagers)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
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
                    serialMan.listener.ClearInfo();
                    serialMan.SendCommand("info\r\n");
                    Thread.Sleep(100);
                    att = 0;
                    while (serialMan.listener.GetSerial().Length == 0)
                    {
                        if (att == 10)
                        {
                            Debug.WriteLine("Unable to get " + serialMan.com);
                            break;
                        };
                        att++;
                        serialMan.listener.Clearinfo();
                        Thread.Sleep(30);
                        serialMan.SendCommand("info\r");
                        Thread.Sleep(30);
                        serialMan.listener.ParseInfo();
                    }

                    serialMan.listener.ParseInfo();
                    string serial = serialMan.listener.GetSerial();
                    string firmware = serialMan.listener.GetFirmware();
                    string model = serialMan.listener.GetModel();

                    while (!comData.TryAdd(new Tuple<string, string, string>(serialMan.com, firmware, serialMan.listener.GetInfo())))
                        Thread.Sleep(30);

                    SQLManager.AddRow(serial, type, model, firmware, DateTime.Now);
                    Invoke((MethodInvoker)delegate
                    {
                        progTxt.Text = $"Getting Info...";
                        progTxt.Refresh();
                    });
                    finished++;
                    serialMan.Disconnect();

                });
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);
            }

            await Task.Run(() =>
            {
                foreach (Thread t in threads)
                {
                    t.Join();
                }
            });
            threads.Clear();


            ///////////////// FINISH
            if (comData.Count > 0)
            {
                Finished summaryPage = new Finished(comData.ToList());
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

                            if (formatString.ToLower()[0] == 'n')
                            {
                                ret += next.ToString().PadLeft(formatString.Length, '0');
                            }
                            else if (formatString.ToLower()[0] == 'y')
                            {
                                ret += DateTime.Now.ToString(formatString);
                            }
                            else if (formatString.ToLower()[0] == 'm')
                            {
                                ret += DateTime.Now.ToString("MM").PadLeft(formatString.Length, '0');
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
                // test21-0041
                // test{yy}-{nnnn}
                int oldIndex = 0;
                int length = 0;
                string oldYear = "";

                // find relative start of "old" serial number
                string newSerial = "";
                int realIndex = 0;
                bool err = false;

                string year = "";
                string month = "";
                string num = "";


                for (int i = 0; i < prevSerialFormat.Length; i++)
                {
                    if (prevSerialFormat[i].Equals('{'))
                    {
                        // find closing bracket
                        string formSub = prevSerialFormat.Substring(i+1);
                        int closing = formSub.IndexOf('}');
                        string format = prevSerialFormat.Substring(i + 1, closing);

                        string curSub = curSerial.Substring(realIndex, format.Length);
                        if (!int.TryParse(curSub, out int res))
                        {
                            err = true;
                            break;
                        }
                        if (format.ToLower()[0] == 'n')
                        {
                            num = curSub;
                        }
                        else if (format.ToLower()[0] == 'y')
                        {
                            year = curSub;
                        }
                        else if (format.ToLower()[0] == 'm')
                        {
                            month = curSub;
                        }

                        i += closing + 1;
                        realIndex += curSub.Length;
                    }
                    else
                    {
                        if (prevSerialFormat[i] != curSerial[realIndex])
                        {
                            err = true;
                            break;
                        }
                        realIndex++;
                    }

                }

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

                        if (formatString.ToLower()[0] == 'n')
                        {
                            ret += num.ToString().PadLeft(formatString.Length, '0');
                        }
                        else if (formatString.ToLower()[0] == 'y')
                        {
                            ret += year;
                        }
                        else if (formatString.ToLower()[0] == 'm')
                        {
                            ret += month;
                        }
                    }
                }

                if (allSerials.Contains(ret))
                {
                    MessageBox.Show($"Desired serial number already exists in the DB. Try again:\n{prevSerialFormat}\n{curSerial}", "ERROR");
                    return "";
                }

                if (!err)
                {
                    SQLManager.EditSerial(curSerial, ret);
                    return ret;
                }
                else
                {
                    MessageBox.Show($"Incorrect prev format. Try again:\n{prevSerialFormat}\n{curSerial}", "ERROR");
                    return "";
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
            formatDict.Add('n', "17");

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

                        string arg = "0";
                        if (formatString.Length < 2)
                        {
                            prevEx.Text += "{ERR}";
                        } 
                        else
                        {
                            try
                        {

                            if (formatString.ToLower()[0] == 'n')
                            {
                                prevEx.Text += 17.ToString().PadLeft(formatString.Length, '0');
                            }
                            else if (formatString.ToLower()[0] == 'y')
                            {
                                prevEx.Text += DateTime.Now.ToString(formatString);
                            }
                            else if (formatString.ToLower()[0] == 'm')
                            {
                                prevEx.Text += DateTime.Now.ToString("MM").PadLeft(formatString.Length, '0') ;
                            }

                            else
                            {
                                prevEx.Text += "{ERR}";
                            }
                        }
                        catch { prevEx.Text += "{ERR}"; }
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

                        string arg = "0";
                        if (formatString.Length < 2)
                        {
                            desEx.Text += "{ERR}";
                        }
                        else
                        {
                            try
                            {

                                if (formatString.ToLower()[0] == 'n')
                                {
                                    desEx.Text += 17.ToString().PadLeft(formatString.Length, '0');
                                }
                                else if (formatString.ToLower()[0] == 'y')
                                {
                                    desEx.Text += DateTime.Now.ToString(formatString);
                                }
                                else if (formatString.ToLower()[0] == 'm')
                                {
                                    desEx.Text += DateTime.Now.ToString("MM").PadLeft(formatString.Length, '0');
                                }

                                else
                                {
                                    desEx.Text += "{ERR}";
                                }
                            }
                            catch { desEx.Text += "{ERR}"; }
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

        private void BlinkSelectedLED(object sender, EventArgs e)
        {
            // turn off all LEDs
            if (!blinkOnSelectionToolStripMenuItem.Checked) return;
            if (availComs.SelectedIndex == -1) return;
            string selCom = availComs.SelectedItem.ToString().Split(':')[1].Trim();
            string serial = availComs.SelectedItem.ToString().Split(':')[0].Trim();
            List<SerialNPMManager> serialManagers = new List<SerialNPMManager>();
            foreach (string com in availComs.Items)
            {
                string comport = com.Split(':')[1].Trim();
                SerialListener listener = new SerialListener();
                SerialNPMManager serialMan = new SerialNPMManager(serial, comport);
                serialManagers.Add(serialMan);
            }
            List<Thread> threads = new List<Thread>();

            // start threads
            foreach (SerialNPMManager serialMan in serialManagers)
            {
                ThreadStart threadDelegate = new ThreadStart(() =>
                {
                    // connect
                    int att = 0;
                    serialMan.Connect(serialMan.com);
                    while (!serialMan.IsConnected())
                    {
                        if (att == 10)
                        {
                            break;
                        }
                        att++;
                        Thread.Sleep(100);
                        serialMan.Connect(serialMan.com);
                    }
                    if (!serialMan.IsConnected())
                    {
                        Debug.WriteLine("Could not connect: " + serialMan.com);
                        return;
                    }

                    serialMan.ClearInput();
                    Thread.Sleep(30);
                    if (!serialMan.com.Equals(selCom))
                    {
                        serialMan.SendCommand("pulsesim=0\r\n");
                        Thread.Sleep(30);
                        serialMan.SendCommand("ledmode=0\r\n");
                    }
                    else
                    {
                        serialMan.SendCommand("pulsesim=100\r\n");
                        Thread.Sleep(30);
                        serialMan.SendCommand("ledmode=1\r\n");
                        Debug.WriteLine("Turned on: " + serialMan.com + " : " + serialMan.IsConnected());
                    }

                    Thread.Sleep(100);
                    serialMan.Disconnect();
                });
                Thread thread = new Thread(threadDelegate);
                thread.Start();
                threads.Add(thread);

            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

        private void blinkOnSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
            optionsToolStripMenuItem.ShowDropDown();
        }
    }
}

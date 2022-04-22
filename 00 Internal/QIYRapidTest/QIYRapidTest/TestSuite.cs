using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QIYRapidTest
{
    class TestSuite
    {
        private ArrayList connected;
        private CancellationTokenSource _cancelationTokenSource;
        public Dictionary<string, ArrayList> TxRxStats = new Dictionary<string, ArrayList>();
        public Dictionary<string, ArrayList> DcRcStats = new Dictionary<string, ArrayList>();
        private Dictionary<string, TCPManager> connectedDict = new Dictionary<string, TCPManager>();

        private Dictionary<string, ArrayList> lastData = new Dictionary<string, ArrayList>();

        private ArrayList cmds;

        private string testPath;

        public event EventHandler<TestArgs> newData;


        private Stopwatch mainClock = Stopwatch.StartNew();

        public TestSuite(ArrayList connected, ArrayList cmds, string selectedPath)
        {
            testPath = selectedPath;
            this.cmds = cmds;
            this.connected = connected;
            foreach (TCPManager connection in connected)
            {
                connectedDict.Add(connection.GetIP(), connection);
                TxRxStats.Add(connection.GetIP(), new ArrayList());
                DcRcStats.Add(connection.GetIP(), new ArrayList());
                lastData.Add(connection.GetIP(), new ArrayList());
                // create test files

                if (File.Exists(testPath + @"\" + connection.GetIP() + "ASYNC.csv")) continue;
                File.Create(testPath + @"\" + connection.GetIP() + "ASYNC.csv").Close();
                File.AppendAllText(testPath + @"\" + connection.GetIP() + "ASYNC.csv", "Time, Reconnect Attempts, Min Command Time, Max Command Time, Record Num" + Environment.NewLine);
                // ret = $"{time}, {connectTime}, {minMax[0]}, {minMax[1]}, {count}";
            }
        }

        public void RunTest()
        {
            _cancelationTokenSource = new CancellationTokenSource();

            foreach (string ip in TxRxStats.Keys)
            {
                new Task(() => RunTestLoop(ip), _cancelationTokenSource.Token, TaskCreationOptions.LongRunning).Start();
            }

        }

        private async void RunTestLoop(string ip)
        {
            Stopwatch sw = Stopwatch.StartNew();
            int j = 0;
            while (!_cancelationTokenSource.IsCancellationRequested)
            {
                int i = 0;
                Debug.WriteLine("Waiting for main clock  : " + ip);
                while (mainClock.ElapsedMilliseconds % 2000 > 50)
                {
                    //Debug.WriteLine("waiting: " + i);
                    //Debug.WriteLine((nextTime - DateTime.Now).ToString("ss"));
                    Thread.Sleep(1);
                    i++;
                }

                int connectionAttempts = await ConnectDevice(ip);
                // [min/max]
                if (connectionAttempts == 3)
                {
                    Debug.WriteLine("Failed to Connect       : " + ip);
                }


                int[] commandMinMax = await SendCommands(ip);

                if (commandMinMax[0] > 250)
                {
                    Debug.WriteLine("Failed to Send Command : " + ip);
                }
                DisconnectDev(ip);

                writeData(ip, connectionAttempts, commandMinMax, j);
                j++;
                EventArgs args = new EventArgs();
                 
                newData?.Invoke(this, new TestArgs(connectionAttempts, commandMinMax, sw.ElapsedMilliseconds, ip));

                Thread.Sleep(200);
            };
        }

        private void DisconnectDev(string ip)
        {
            TCPManager man = connectedDict[ip];
            man.Disconnect();
        }

        private async Task<int[]> SendCommands(string ip)
        {
            // send uptime, then time
            int[] minMax = new int[] { int.MaxValue, int.MinValue };
            TCPManager man = connectedDict[ip];

            Stopwatch sw = new Stopwatch();


            // ~2.5 sec max
            for (int i = 0; i < 5; i++)
            {

                //// CMD 1/////
                

                // clear buffer
                man.GetData();

                // start timer
                sw.Restart();

                // send command
                await man.SendCommand("uptime\r\n");
                string data = man.GetData();

                while (!data.Contains("="))
                {
                    if (sw.ElapsedMilliseconds > 250) break;
                    data += man.GetData();
                    Thread.Sleep(1);
                }
                
                // determine time
                long time = sw.ElapsedMilliseconds;
                if (time < minMax[0]) minMax[0] = (int)time;
                if (time > minMax[1]) minMax[1] = (int)time;


                //// CMD 2/////
                

                //2000 / 01 / 08 22:30:47
                // clear buffer
                man.GetData();

                // start timer
                sw.Restart();

                // send command
                await man.SendCommand("time\r\n");
                data = man.GetData();
                while (!data.Contains("/"))
                {
                    if (sw.ElapsedMilliseconds > 250) break;
                    data += man.GetData();
                    Thread.Sleep(1);
                }

                // determine time
                time = sw.ElapsedMilliseconds;
                if (time < minMax[0]) minMax[0] = (int)time;
                if (time > minMax[1]) minMax[1] = (int)time;

            }

            return minMax;


        }

        private async Task<int> ConnectDevice(string ip)
        {
            bool success = false;
            TCPManager man = connectedDict[ip];
            int i;
            for (i = 0; i < 3; i++)
            {
                success = await man.TryConnection();
                if (success) return i;
            }
            
            return 3;

        }

        private void writeData(string ip, int connectTime, int[] minMax, int count)
        {
            File.AppendAllText(testPath + @"\" + ip + "ASYNC.csv", CreateCSVLine(ip, connectTime, minMax, count) + Environment.NewLine);
        }

        private string CreateCSVLine(string ip, int connectTime, int[] minMax, int count)
        {
            string ret = "";
            string time = DateTime.Now.ToString("HH:mm:ss:fff");


            int parseuptime;
            ret = $"{time}, {connectTime}, {minMax[0]}, {minMax[1]}, {count}";


            return ret;
        }


        public void StopTest()
        {
            _cancelationTokenSource.Cancel();
        }

    }
    public class TestArgs : EventArgs
    {
        public TestArgs(int connectAttempts, int[] minMax, double uptime, string ip)
        {
            UpTime = uptime;
            this.connectAttempts = connectAttempts;
            this.minMax = minMax;
            this.ip = ip;
        }

        public int connectAttempts { get; set; }
        public int[] minMax { get; set; }
        public string ip { get; set; }
        public double UpTime { get; set; }
    }
}

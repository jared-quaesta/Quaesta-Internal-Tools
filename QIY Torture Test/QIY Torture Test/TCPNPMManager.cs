using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QIY_Torture_Test
{
    class TCPNPMManager
    {
        private IPAddress ip;
        private Socket socket;
        private readonly int PORT = 10001;

        private bool gotACK = false;
        private bool gotNACK = false;
        private bool gotC = false;

        private bool recievedData = false;
        private int lastPlace = 0;

        internal string lastCommand = "";
        private string lastVolt = "";
        private string lastUptime = "";
        private string lastTime = "";
        private int connectionRetries = 0;

        private Dictionary<string, string> referenceInfoDict = new Dictionary<string, string>();
        private List<string> infoErrCollect = new List<string>();

        Ping pingSender = new Ping();
        PingOptions options = new PingOptions();

        private byte[] buffer = new byte[2048];

        private string data = "";

        int voltAttempts = 1;
        int uptimeAttempts = 1;
        int timeAttempts = 1;

        public bool bufferIsEmpty = false;
        public bool receiving = false;

        private bool updating = false;

        internal TCPListener listener;

        List<byte[]> cmdBuffer = new List<byte[]>();

        private Task listen;

        internal int errCount = 0;
        internal int diffCount = 0;


        private byte[] SOH = new byte[] { 0x01 };

        public TCPNPMManager(string ip)
        {
            socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            this.ip = IPAddress.Parse(ip);

            //listen = new Task(() => StartListeningTCPAsync(), tcpCancellationToken.Token, TaskCreationOptions.LongRunning);

        }

        internal async Task<int> SendBytes(byte[] buffer)
        {

            return socket.Send(buffer);
        }

        override public string ToString()
        {
            return GetIP();
        }

        internal void NewCmd(string cmd = "", byte[] bytes = null)
        {
            Debug.WriteLine("New cmd added:" + cmd);
            if (cmd.Length != 0)
            {
                bytes = Encoding.ASCII.GetBytes(cmd);
            }
            cmdBuffer.Add(bytes);
        }


        public async Task<bool> Reboot()
        {
            await SendCommandAsync("reboot\r\n");
            Thread.Sleep(4000);
            return true;
        }

        /// <summary>
        /// Attempts to connect to the address asyncronously
        /// </summary>
        /// 
        /// <param name="msTimeout">ms to wait before timeout. False if timeout</param>
        /// <returns>True if connection is successful, false otherwise (socket is bounc, timeout occurs, etc) </returns>
        public async Task<bool> TryConnectionAsync(int msTimeout = 50)
        {
            socket.Close();
            socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            var clientDone = new ManualResetEvent(false);
            return await Task.Run(() =>
            {
                using (SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs() { RemoteEndPoint = new IPEndPoint(this.ip, PORT) })
                {
                    IAsyncResult result = socket.BeginConnect(ip, PORT, null, null);

                    bool success = result.AsyncWaitHandle.WaitOne(msTimeout, true);

                    if (socket.Connected)
                    {
                        socket.EndConnect(result);
                        return true;
                    }
                    else
                    {
                        // NOTE, MUST CLOSE THE SOCKET

                        socket.Close();
                        socket = new Socket(AddressFamily.InterNetwork,
                            SocketType.Stream, ProtocolType.Tcp);
                        return false;
                    }

                }
            });
        }

        internal int GetConnectionAttempts()
        {
            int ret = connectionRetries;
            connectionRetries = 0;
            return ret;
        }

        internal bool TryConnectionSync(int msTimeout = 150)
        {
            connectionRetries++;
            socket.Close();
            socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            var clientDone = new ManualResetEvent(false);

            using (SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs() { RemoteEndPoint = new IPEndPoint(ip, PORT) })
            {
                IAsyncResult result = socket.BeginConnect(ip, PORT, null, null);

                bool success = result.AsyncWaitHandle.WaitOne(msTimeout, true);

                if (socket.Connected)
                {
                    socket.EndConnect(result);
                    return true;
                }
                else
                {
                    // NOTE, MUST CLOSE THE SOCKET
                    socket.Close();
                    socket = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);
                    return false;
                }
            }
        }

        internal bool GotVoltage()
        {
            socket.ReceiveTimeout = 1000;
            buffer = new byte[2048];
            byte[] vcmd = Encoding.ASCII.GetBytes("voltage\r\n");

            foreach (byte b in vcmd)
            {
                try
                {
                    socket.Send(new byte[] { b });
                    Thread.Sleep(10);
                }
                catch { }
            }

            string res = Encoding.Default.GetString(buffer).Replace("\0", string.Empty);

            int timeOut = 1;

            while (!res.Contains("/"))
            {
                try { socket.Receive(buffer); }
                catch { }
                res = Encoding.Default.GetString(buffer).Replace("\0", string.Empty);
                if (timeOut % 3 == 0)
                {
                    voltAttempts++;
                    try
                    {
                        foreach (byte b in vcmd)
                        {
                            socket.Send(new byte[] { b });
                            Thread.Sleep(10);
                        }
                    }
                    catch { return false; }
                }
                if (timeOut % 50 == 0)
                {
                    return false;
                }
                timeOut++;
                buffer = new byte[2048];
            }
            lastVolt = res.Split(' ')[res.Split(' ').Length - 1].Trim('\n', '\r').Replace('}', ' ');

            return true;

        }

        internal void ClearBuffer()
        {
            socket.ReceiveTimeout = 100;
            byte[] buf = new byte[2048];

            for (int i = 1; i < 5; i++)
            {
                try
                {
                    socket.Receive(buf);
                } catch
                {
                    return;
                }
            }
        }

        internal bool GotUptime()
        {
            socket.ReceiveTimeout = 1000;
            buffer = new byte[2048];
            byte[] vcmd = Encoding.ASCII.GetBytes("uptime\r\n");
            foreach (byte b in vcmd)
            {
                socket.Send(new byte[] { b });
                Thread.Sleep(10);
            }

            string res = Encoding.Default.GetString(buffer).Replace("\0", string.Empty);
            int timeOut = 1;

            while (!res.Contains("="))
            {
                try { socket.Receive(buffer); }
                catch { }
                res = Encoding.Default.GetString(buffer).Replace("\0", string.Empty);

                if (timeOut % 3 == 0)
                {
                    uptimeAttempts++;
                    try
                    {
                        foreach (byte b in vcmd)
                        {
                            socket.Send(new byte[] { b });
                            Thread.Sleep(10);
                        }
                    }
                    catch { return false; }
                }
                if (timeOut % 50 == 0)
                {
                    return false;
                }
                timeOut++;

                buffer = new byte[2048];
            }
            lastUptime = res.Replace('}', ' ').Split('=')[res.Split('=').Length - 1];
            Debug.WriteLine(res + "       " + lastUptime);
            return true;
        }

        internal string GetVoltage()
        {
            string[] split = lastVolt.Split('/');

            if (!double.TryParse(split[0], out double measured))
            {
                return "Err";
            }
            if (!double.TryParse(split[0], out double set))
            {
                return "Err";
            }
            string parsed = $"{measured};{set}";
            return parsed;
        }

        internal bool GotInfo()
        {
            socket.ReceiveTimeout = 1000;
            buffer = new byte[2048];
            byte[] vcmd = Encoding.ASCII.GetBytes("info\r\n");
            foreach (byte b in vcmd)
            {
                socket.Send(new byte[] { b });
                Thread.Sleep(10);
            }

            string res = Encoding.Default.GetString(buffer).Replace("\0", string.Empty);
            int timeOut = 1;


            string infoStr = "";
            while (!(res.Contains("TcpPort") || res.Contains("Current Time")))
            {
                try { socket.Receive(buffer); }
                catch { }
                res = Encoding.Default.GetString(buffer).Replace("\0", string.Empty);
                infoStr += res;

                if (timeOut % 5 == 0)
                {
                    try
                    {
                        foreach (byte b in vcmd)
                        {
                            socket.Send(new byte[] { b });
                            Thread.Sleep(10);
                        }
                    }
                    catch { return false; }
                }
                if (timeOut % 50 == 0)
                {
                    return false;
                }
                timeOut++;

                buffer = new byte[2048];
            }

            string[] splitres = infoStr.Split('\n');
            foreach (string line in splitres)
            {
                string lineTrimmed = line.Trim('\r', '\n');
                string key = line.Split(' ')[0];

                if (key.Contains("Current")) continue;
                if (key.Contains("Model")) continue;
                if (key.Contains("//")) continue;
                if (line.Contains("Measured")) continue;
                if (line.Contains("VibeMode")) continue;
                if (line.Contains("Drive")) continue;
                if (key.Length == 0) continue;
                if (referenceInfoDict.ContainsKey(key))
                {
                    if (!referenceInfoDict[key].Equals(lineTrimmed))
                    {
                        Debug.WriteLine($"Expected: {referenceInfoDict[key]} Got: {lineTrimmed}\n\nKEY[{key}]");
                        infoErrCollect.Add(lineTrimmed);
                        errCount++;
                    }
                }
                else
                {
                    referenceInfoDict.Add(key, lineTrimmed);
                }

            }

            return true;

        }

        internal void Initialize(string[] p)
        {
            int timeout = 0;
            while (!TryConnectionSync())
            {
                Thread.Sleep(100);
                timeout++;
                if (timeout == 5) return;
            }

            foreach (string cmd in p)
            {
                byte[] vcmd = Encoding.ASCII.GetBytes(cmd + "\r\n");
                try
                {
                    foreach (byte b in vcmd)
                    {
                        socket.Send(new byte[] { b });
                        Thread.Sleep(10);
                    }
                }
                catch { return; }

                Thread.Sleep(100);

                try
                {
                    foreach (byte b in vcmd)
                    {
                        socket.Send(new byte[] { b });
                        Thread.Sleep(10);
                    }
                }
                catch { return; }
            }
        }

        internal int GetTimeAttempts()
        {
            int ret = timeAttempts;
            timeAttempts = 0;
            return ret;
        }

        internal int GetUptimeAttempts()
        {
            int ret = uptimeAttempts;
            uptimeAttempts = 0;
            return ret;
        }

        internal int GetVoltAttempts()
        {
            int ret = voltAttempts;
            voltAttempts = 0;
            return ret;
        }

        internal string GetTime()
        {
            return lastTime;
        }

        internal bool GotTime()
        {
            socket.ReceiveTimeout = 1000;
            buffer = new byte[2048];
            byte[] vcmd = Encoding.ASCII.GetBytes("time\r\n");
            foreach (byte b in vcmd)
            {
                socket.Send(new byte[] { b });
                Thread.Sleep(10);
            }

            string res = Encoding.Default.GetString(buffer).Replace("\0", string.Empty);
            int timeOut = 1;

            while (!res.Contains(":"))
            {

                try { socket.Receive(buffer); }
                catch { }
                res = Encoding.Default.GetString(buffer).Replace("\0", string.Empty);

                if (res.Contains("?") || res.Contains("SignalOn") || res.Contains("Drive"))
                {
                    res = "";
                    Thread.Sleep(1000);
                    continue;
                }
                Debug.WriteLine(res);
                if (timeOut % 3 == 0)
                {
                    timeAttempts++;
                    try
                    {
                        foreach (byte b in vcmd)
                        {
                            socket.Send(new byte[] { b });
                            Thread.Sleep(5);
                        }
                    }
                    catch { return false; }
                }
                if (timeOut % 50 == 0)
                {
                    Debug.WriteLine("Timeout");
                    return false;
                }
                timeOut++;

                buffer = new byte[2048];
            }
            lastTime = res.Replace("\n", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("time", string.Empty)
                .Replace("}", string.Empty)
                .Replace(",", " ");
            return true;
        }

        public async Task<bool> SendCommandAsync(string cmd, int msTimeout = 500)
        {
            // try to connect if not bound already.
            //// If cannot connect, return false
            //Debug.WriteLine("Cancellation: " + listen.IsCanceled);
            //Debug.WriteLine("Fault       : " + listen.IsFaulted);
            //Debug.WriteLine("Completed   : " + listen.IsCompleted);
            if (!socket.Connected)
            {
                return false;

                //retry connection?

                //Debug.WriteLine("Port not connected, trying to connect..");
                //return await Task.Run(() =>
                //{
                //    return TryConnectionAsync(msTimeout);
                //});
            }

            lastCommand = cmd.ToLower().Trim('\r', '\n');
            // convert data to bytes
            byte[] data = Encoding.ASCII.GetBytes(cmd);

            return await Task.Run(async () =>
            {
                try
                {
                    int success = socket.Send(data);

                    if (success != data.Length)
                    {
                        Debug.WriteLine("Did not match expected output");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e + "\n\n");
                    return false;
                }
                //Debug.WriteLine("Sent Command to " + ip.ToString());
                return true;
            });


        }

        internal string GetIP()
        {
            return ip.ToString();
        }

        internal void Disconnect()
        {
            socket.Close();
            socket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
        }

        internal bool IsConnectedShallow()
        {
            return socket.Connected;
        }

        internal bool AwaitReboot()
        {
            Stopwatch sw = Stopwatch.StartNew();
            while (!Ping())
            {
                Thread.Sleep(10);
                if (sw.ElapsedMilliseconds > 10000) return false;
            }
            return true;
                
        }


        internal bool Ping(int timeout = 5000)
        {
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            
            try
            {
                PingReply reply = pingSender.Send(ip, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch { return false; }
            
            return false;
        }

        internal async Task<bool> IsConnectedDeep()
        {
            bool pingresult = await Task.Run(() =>
            {
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 100;
                PingReply reply = pingSender.Send(ip, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                return false;
            });

            return socket.Connected && pingresult;
        }

        internal string GetDiff()
        {
            string ret = "";
            foreach (string diff in infoErrCollect)
            {
                ret += diff.Replace('\n', ' ').Replace('\r', ' ').Replace("\0", string.Empty).Trim() + ',';
            }
            infoErrCollect.Clear();
            return ret.Trim(',');
        }

        internal string GetUptime()
        {
            if (!Int32.TryParse(lastUptime, out int uptime))
            {
                return "err";
            }

            return uptime.ToString();
        }
    }
}

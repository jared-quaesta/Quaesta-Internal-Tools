using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIY_Interface__IAEA_
{
    class TCPManager
    {
        private IPAddress ip;
        private Socket socket;
        private readonly int PORT = 10001;

        private bool enableSend = true;

        private bool gotACK = false;
        private bool gotNACK = false;
        private bool gotC = false;

        private int listenerCount = 0;

        private bool recievedData = false;
        private int lastPlace = 0;

        internal string lastCommand = "";
        private bool listenerStarted = false;

        private FWUpgradeTCP fwForm;
        private ProgressBar pb;
        private Label pt;

        internal Task listenerTask;

        Ping pingSender = new Ping();

        PingOptions options = new PingOptions();

        private byte[] buffer = new byte[2048];

        private string data = "";

        public bool bufferIsEmpty = false;
        public bool receiving = false;

        private bool updating = false;
        internal bool type = false;

        private bool endTask = false;

        internal TCPListener listener;

        List<byte[]> cmdBuffer = new List<byte[]>();

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token = new CancellationToken();

        private Task listen;


        private byte[] SOH = new byte[] { 0x01 };

        private int sumBytes = 0;
        private string name = "";
        internal int hgmMode = -1;

        internal bool extPulseOn = false;
        internal bool tempOn = false;
        internal bool humOn = false;
        internal bool battOn = false;
        internal bool sigOn = false;

        internal MainForm mainForm;
        

        internal bool stopType = false;
        public TCPManager(string ip, MainForm main)
        {
            socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            this.ip = IPAddress.Parse(ip);
            mainForm = main;

        }

        internal void EnableSend(bool enable)
        {
            enableSend = enable;
        }

        internal string GetName()
        {
            return name;
        }

        internal async Task<int> SendBytes(byte[] buffer)
        {
            return await socket.SendAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
        }

        internal async Task<bool> GotAck()
        {
            while (!gotACK)
            {
                Thread.Sleep(10);
            }
            return true;
        }

        internal async Task<bool> GotNACK()
        {
            while (!gotNACK)
            {
                Thread.Sleep(10);
            }
            return true;
        }


        internal async Task<int> SendByte(byte b)
        {
            lastCommand = "nullcmd";
            return await socket.SendAsync(new ArraySegment<byte>(new byte[] { b }), SocketFlags.None);
        }

        internal async Task<int> SendSOH()
        {
            return await socket.SendAsync(new ArraySegment<byte>(SOH), SocketFlags.None);
        }


        private async Task ListenerTaskFunc()
        {
            while (!endTask)
            {
                bool connected = IsConnectedShallow();
                listener.UpdateConnected(connected);
                if (!connected && updating)
                {
                    if (await TryConnectionAsync())
                    {
                        Debug.WriteLine("Reconnected!");
                        updating = false;
                        NewCmd("info\r\n");
                        fwForm.Invoke((MethodInvoker)delegate
                        {
                            fwForm.Close();
                        });
                        continue;
                    }
                    else
                    {
                        pb.Invoke((MethodInvoker)delegate
                        {
                            pb.Value = pb.Maximum;
                            pt.Text = $"Reconnecting...";

                            fwForm.Refresh();
                        });
                    }
                }

                if ((!connected || type) && !updating)
                {
                    Debug.WriteLine("Typing");
                    Thread.Sleep(100);
                    continue;
                }


                byte[] cmd = GetNextCommand();

                if (cmd != null && enableSend)
                {
                    await socket.SendAsync(new ArraySegment<byte>(cmd), SocketFlags.None);
                    if (!updating) Thread.Sleep(100);
                }

                // listen for bytes
                socket.ReceiveTimeout = 200;

                if (cmd != null && !updating)
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    while (sw.ElapsedMilliseconds < 1000)
                    {
                        try
                        {
                            if (socket.Receive(buffer) > 0)
                                break;
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    try
                    {
                        socket.Receive(buffer);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }


                //try
                //{
                //    socket.Receive(buffer);
                //}
                //catch (Exception e)
                //{
                //    continue;
                //}

                string strDat = Encoding.Default.GetString(buffer);

                // TODO look into updating catch phrase
                if (strDat.Contains("Upload") && updating)
                {
                    socket.Close();
                    socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                    continue;
                }
                if (!updating)
                {
                    listener.IncomingData(strDat, lastCommand);
                    Thread.Sleep(100);
                }
                else if (updating)
                {
                    pb.Invoke((MethodInvoker)delegate
                    {
                        try
                        {
                            pb.Value = pb.Maximum - cmdBuffer.Count();
                            pt.Text = $"Sending Block {pb.Value} of {pb.Maximum}";
                        }
                        catch { }
                        fwForm.Refresh();
                    });
                }

                buffer = new byte[2048];
                //Application.DoEvents();
            }
            Debug.WriteLine("Ended Task");

        }

        internal void AddListener(TCPListener listener)
        {
            this.listener = listener;

            socket.SendTimeout = 200;
            socket.ReceiveTimeout = 100;
            // run main listen loop after listener/parser is connected
            listenerTask = Task.Run(ListenerTaskFunc);
        }

        private byte[] GetNextCommand()
        {
            if (cmdBuffer.Count == 0) return null;
            byte[] cmd = cmdBuffer[0];
            lastCommand = Encoding.Default.GetString(cmd).Trim('\n', '\r').ToLower();
            cmdBuffer.RemoveAt(0);
            return cmd;

        }

        internal void NewCmd(string cmd = "", byte[] bytes = null)
        {
            if (cmd.Length != 0)
            {
                bytes = Encoding.ASCII.GetBytes(cmd);
            }
            cmdBuffer.Add(bytes);
        }

        internal Task<bool> Type(string path, long expected, TransferFileForm transferFileForm, string fileName)
        {
            // go thru pending cmds
            int cmdlen = Encoding.ASCII.GetBytes("type " + path + "\r\n").Length;
            expected += cmdlen;
            type = true;
            Thread.Sleep(500);
            byte[] cmd = Encoding.ASCII.GetBytes($"type {path}\r\n");
            buffer = new byte[2048];
            socket.ReceiveBufferSize = 2048;
            long count = 0;
            List<byte> read = new List<byte>();
            stopType = false;
            return Task.Run(() =>
            {
                socket.SendAsync(new ArraySegment<byte>(cmd), SocketFlags.None);
                int packets = 0;
                while (read.Count < expected || stopType)
                {
                    
                    try
                    {
                        int numBytes = socket.Receive(buffer); 
                        for (int i = 0; i < numBytes; i++)
                        {
                            read.Add(buffer[i]);
                        }
                        packets++;
                    }
                    catch { }
                    
                    buffer = new byte[2028];

                    try
                    {
                        transferFileForm.Invoke((MethodInvoker)delegate
                        {
                            int prog = (int)((double)read.Count / expected * 100);
                            if (prog > 100) prog = 100;
                            transferFileForm.UpdateProgress(prog, (int)(read.Count - cmdlen), read.Count - cmdlen, packets, expected - cmdlen);
                        });
                    }

                    catch (ObjectDisposedException e)
                    {
                        type = false;
                        Debug.WriteLine("????");
                        return true;
                    }

                    catch (InvalidOperationException e)
                    {
                        type = false;
                        Debug.WriteLine("????");
                        return true;
                    }

                    catch (Exception e)
                    {
                        Debug.WriteLine(e.GetType());
                    }

                if (stopType == true)
                    {
                        type = false;
                        return true;
                    }

                }
                if (stopType == true)
                {
                    type = false;
                    return true;
                }
                // Write to file
                try
                {
                    if (stopType == true)
                    {
                        type = false;
                        return true;
                    }
                    byte[] byteArray = read.Skip(cmdlen).ToArray();
                    using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(byteArray, 0, (int)(expected - cmdlen));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in process: {0}", ex);
                }

                type = false;
                return true;
            });

        }

        private byte[] TrimByte(byte[] input)
        {
            if (input[0] == 0x00) return Array.Empty<byte>();
            if (input.Length > 1)
            {
                int byteCounter = input.Length - 1;
                while (input[byteCounter] == 0x00)
                {
                    byteCounter--;
                }
                byte[] rv = new byte[(byteCounter + 1)];
                for (int byteCounter1 = 0; byteCounter1 < (byteCounter + 1); byteCounter1++)
                {
                    rv[byteCounter1] = input[byteCounter1];
                }
                return rv;
            }
            return Array.Empty<byte>();
        }

        internal void EndType(string path)
        {
            type = false;
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

        internal void UpdateFirmware(string filename, FWUpgradeTCP fwForm)
        {
            if (updating) return;
            this.fwForm = fwForm;
            pb = fwForm.getPB();
            pt = fwForm.getPt();
            fwForm.getPB().Maximum = (int)new FileInfo(filename).Length / 128;
            ClearCmdBuff();
            updating = true;
            NewCmd("updatefirmware\r\n");

            using (Stream source = File.OpenRead(filename))
            {
                byte[] buffer = new byte[128];
                int bytesRead;
                Debug.WriteLine("Reading bytes from file:");

                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    byte[] block = new byte[133];

                    // make padding
                    block[0] = 0x01;
                    block[1] = 0x00;
                    block[2] = 0x00;
                    block[131] = 0x00;
                    block[132] = 0x00;

                    // fill in data
                    for (int i = 0; i < 128; i++)
                    {
                        block[i + 3] = buffer[i];
                    }

                    NewCmd("", block);

                }

                // finish transfer


                NewCmd("", new byte[] { 0x04 });
                NewCmd("", new byte[] { 0x04 });

            }

        }

        internal void SetName(string data)
        {
            name = data;
        }

        private void ClearCmdBuff()
        {
            cmdBuffer.Clear();
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
                    int success = await socket.SendAsync(new ArraySegment<byte>(data), SocketFlags.None);

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
            endTask = true;
            while (!listenerTask.IsCompleted)
            {
                Thread.Sleep(100);
                Application.DoEvents();
            }
            socket.Close();
            socket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
        }

        internal bool IsConnectedShallow()
        {
            return socket.Connected;
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

    }
}

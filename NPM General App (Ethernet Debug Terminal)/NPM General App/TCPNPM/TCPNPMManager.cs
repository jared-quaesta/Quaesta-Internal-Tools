using NPM_General_App.Utilities;
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

namespace NPM_General_App
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


        Ping pingSender = new Ping();
        PingOptions options = new PingOptions();

        private byte[] buffer = new byte[2048];

        private string data = "";

        public bool bufferIsEmpty = false;
        public bool receiving = false;

        private bool updating = false;

        private TCPListener listener;

        List<byte[]> cmdBuffer = new List<byte[]>();

        private CancellationTokenSource tcpCancellationToken;

        private Task listen;

        private FWUpgradeTCP fwForm;
        private ProgressBar pb;
        private Label pt;

        private byte[] SOH = new byte[] { 0x01 };

        public TCPNPMManager(string ip)
        {
            socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            this.ip = IPAddress.Parse(ip);

            tcpCancellationToken = new CancellationTokenSource();

            //listen = new Task(() => StartListeningTCPAsync(), tcpCancellationToken.Token, TaskCreationOptions.LongRunning);
            
        }

        internal async Task<int> SendBytes(byte[] buffer)
        {
            return await socket.SendAsync(buffer, SocketFlags.None);
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
            return await socket.SendAsync(new byte[] { b }, SocketFlags.None);

        }

        internal async Task<int> SendSOH()
        {
            return await socket.SendAsync(SOH, SocketFlags.None);
        }

        internal void AddListener(TCPListener listener)
        {
            this.listener = listener;

            socket.SendTimeout = 200;
            socket.ReceiveTimeout = 100;
            // run main listen loop after listener/parser is connected
            Task.Run(async () =>
            {
                while (true)
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
                        } else
                        {
                            pb.Invoke((MethodInvoker)delegate
                            {
                                pb.Value = pb.Maximum;
                                pt.Text = $"Reconnecting...";
                                
                                fwForm.Refresh();
                            });
                        }
                    }

                    if (!connected)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    byte[] cmd = GetNextCommand();
                    if (cmd != null)
                    {
                        await socket.SendAsync(cmd, SocketFlags.None);
                    }

                    // listen for bytes
                    socket.ReceiveTimeout = 200;
                    try
                    {
                        socket.Receive(buffer);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                    string strDat = Encoding.Default.GetString(buffer);
                    if (strDat.Contains("Upload"))
                        Disconnect();

                    if (!updating)
                    {
                        listener.IncomingData(strDat, lastCommand);
                        Thread.Sleep(100);
                    }
                    else
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
                }
            });
        }

        private byte[] GetNextCommand()
        {
            if (cmdBuffer.Count == 0) return null;
            byte[] cmd = cmdBuffer[0];
            lastCommand = Encoding.Default.GetString(cmd).Trim('\n', '\r');
            cmdBuffer.RemoveAt(0);
            return cmd;

        }

        internal void NewCmd(string cmd="", byte[] bytes = null)
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

        internal void UpdateFirmware(string filename, FWUpgradeTCP fwForm)
        {
            if (updating) return;
            this.fwForm = fwForm;
            pb = fwForm.getPB();
            pt = fwForm.getPt();
            fwForm.getPB().Maximum = (int) new FileInfo(filename).Length / 128;
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

            lastCommand = cmd.ToLower().Trim('\r','\n');
            // convert data to bytes
            byte[] data = Encoding.ASCII.GetBytes(cmd);

            return await Task.Run(async () =>
            {
                try
                {
                    int success = await socket.SendAsync(data, SocketFlags.None);
                    
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
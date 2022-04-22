using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HardRebootQIY
{
    class TCPNPMManager
    {
        private IPAddress ip;
        private Socket socket;
        private readonly int PORT = 10001;


        internal string lastCommand = "";


        private byte[] buffer = new byte[2048];

        public bool bufferIsEmpty = false;
        public bool receiving = false;

        List<byte[]> cmdBuffer = new List<byte[]>();


        internal bool rebooting = true;


        private byte[] SOH = new byte[] { 0x01 };
        private StatsManager listener;

        public TCPNPMManager(string ip)
        {
            socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            this.ip = IPAddress.Parse(ip);


            //listen = new Task(() => StartListeningTCPAsync(), tcpCancellationToken.Token, TaskCreationOptions.LongRunning);

        }


        internal void AddListener(StatsManager listener)
        {
            this.listener = listener;

            socket.SendTimeout = 200;
            socket.ReceiveTimeout = 100;
            // run main listen loop after listener/parser is connected
            Task.Run(async () =>
            {
                while (true)
                {
                    // power shutoff, attempt to reconnect.
                    if (rebooting)
                    {
                        if (await TryConnectionAsync())
                        {
                            Debug.WriteLine("Reconnected: " + GetIP());
                            rebooting = false;
                            NewCmd("info\r\n");
                            continue;
                        }
                        else
                        {
                            Thread.Sleep(100);
                            continue;
                        }
                    }


                    // listen for bytes
                    socket.ReceiveTimeout = 100;
                    try
                    {
                        socket.Receive(buffer);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                    string strDat = Encoding.Default.GetString(buffer);
                    if (strDat.Length > 0) 
                    listener.IncomingData(strDat, lastCommand);
                    Thread.Sleep(100);
                    
                   
                    buffer = new byte[2048];
                }
            });
        }

        internal string getLast()
        {
            return listener.lastStr;
        }

        internal void NewCmd(string cmd = "", byte[] bytes = null)
        {
            socket.SendTimeout = 100;
            bytes = Encoding.ASCII.GetBytes(cmd);
            socket.Send(bytes);
        }

        internal async Task<bool> GotInfo()
        {
            return await Task.Run(() => {

                while (!listener.gotInfo)
                {
                    Thread.Sleep(100);
                }
                listener.gotInfo = false;
                return true;
            
            });
        }

        internal List<string> GetErrs()
        {
            return listener.errs;
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

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QIYRapidTest
{

    // This class is observable by main
    class TCPManager
    {
        private IPAddress ip;
        private Socket socket;
        private readonly int PORT = 10001;

        private bool recievedData = false;
        private int lastPlace = 0;
        
        private byte[] buffer = new byte[2048];

        private string data = "";

        public bool bufferIsEmpty = false;
        public bool receiving = false;
        
        private CancellationTokenSource _cancelationTokenSource = new CancellationTokenSource();

        public TCPManager(string ip)
        {
            socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            socket.ReceiveTimeout = 5;
            socket.SendTimeout = 5000;
            this.ip = IPAddress.Parse(ip);

            _cancelationTokenSource = new CancellationTokenSource();
            new Task(() => StartListening(), _cancelationTokenSource.Token, TaskCreationOptions.LongRunning).Start();

        }

        private void StartListening()
        {
            while (!_cancelationTokenSource.Token.IsCancellationRequested)
            {
                bool success = ReadBuffer();
                Thread.Sleep(50);
            }
        }

        internal Task<bool> RecievedData()
        {
            throw new NotImplementedException();
        }

        internal async Task<int> DcRc()
        {
            socket.Close();
            while (socket.Connected) { }
            socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            socket.ReceiveTimeout = 10; socket.SendTimeout = 5000;

            return await Task.Run(async () =>
            {
                int timeout = 0;
                bool connected = await TryConnection();
                while (!connected)
                {
                    Debug.WriteLine(timeout + ":  " + ip.ToString());
                    if (timeout == 3) return 3;
                    connected = await TryConnection();
                    timeout++;
                }
                return timeout;
            });

        }

        /// <summary>
        /// Attempts to connect to the address asyncronously
        /// </summary>
        /// 
        /// <param name="msTimeout">ms to wait before timeout. False if timeout</param>
        /// <returns>True if connection is successful, false otherwise (socket is bounc, timeout occurs, etc) </returns>
        public async Task<bool> TryConnection(int msTimeout = 500)
        {
            if (socket.Connected) {
                socket.Close();
                socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            }
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


                    //try
                    //{
                    //    socket.Connect(socketEventArg.RemoteEndPoint);
                    //} catch
                    //{
                    //    return false;
                    //}
                    //clientDone.WaitOne(5);
                    //if (!socket.Connected)
                    //    clientDone.WaitOne(msTimeout);

                    //return socket.Connected;
                }
            });
        }

        internal void ClearData()
        {
            data = "";
            lastPlace = 0;
        }

        public string GetData()
        {
            string ret = data.Substring(lastPlace);
            lastPlace = data.Length;
            //Debug.WriteLine(data.Length);
            return ret;
        }

        public async Task<bool> SendCommand(string cmd, int msTimeout=500)
        {
            // try to connect if not bound already.
            // If cannot connect, return false

            if (!socket.Connected)
            {
                return false;
            }

            socket.SendTimeout = msTimeout;
            // convert data to bytes
            byte[] data = Encoding.ASCII.GetBytes(cmd);

            socket.Send(data);
            foreach (byte b in data)
            {
                byte[] onebyte = new byte[] { b };
                Debug.WriteLine(socket.Send(onebyte));
                Debug.WriteLine(b.ToString());
                //Thread.Sleep(30);
            }

            return true;




            return await Task.Run(async () =>
            {
                try
                {
                    foreach (byte b in data)
                    {
                        byte[] onebyte = new byte[] {b };
                        Debug.WriteLine(socket.Send(onebyte));
                        Debug.WriteLine(b.ToString());
                        Thread.Sleep(30);
                    }


                }
                catch (Exception e)
                {
                    
                    Debug.WriteLine(e + "\n\n");

                    return false;
                }
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
            while (socket.Connected) Thread.Sleep(1);
            socket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
        }

        internal bool IsConnected()
        {
            return socket.Connected;
        }

        public bool ReadBuffer()
        {
            if (!socket.Connected) return false;

            try
            {
                socket.Receive(buffer);
            }
            catch (Exception e)
            {
                return false;
            }
            data += Encoding.Default.GetString(buffer).Trim() + "\n";
            //Debug.WriteLine(data.Length);
            buffer = new byte[2048];
            return true;

        }

        
    }
}

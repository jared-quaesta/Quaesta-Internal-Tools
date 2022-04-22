using NPM_General_App.SerialNPM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;

namespace NPM_General_App
{
    class SerialNPMManager
    {
        private string com;
        private SerialListener listener;
        private SerialPort _serialPort;
        private bool connected = false;
        private string curConnected = "";

        public SerialNPMManager(SerialListener listener, string com)
        {
            this.com = com;
            this.listener = listener;
            _serialPort = new SerialPort
            {
                ReadTimeout = 200,
                WriteTimeout = 200,
                WriteBufferSize = 1024,
                BaudRate = 115200
            };
            _serialPort.DataReceived += (sender, e) => listener.NewData(_serialPort.ReadExisting());
        }

        public string GetCurConnected()
        {
            if (connected)
            {
                return curConnected;
            }
            return null;
        }

        /***
         * Requests all available COM ports to connect to.
         * 
         * Returns ArrayList of strings 
         *              ex// ["COM1" , "COM2" , "COM3]
         */
        public static List<Tuple<string, string>> GetComs(string match)
        {
            List<Tuple<string, string>> ret = new List<Tuple<string, string>>();
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString() + p["DeviceID"].ToString());
                
                foreach (var i in ports)
                {
                    if (i.Contains(match))
                    {
                        int start = i.IndexOf('(') + 1;
                        string parse = i.Substring(start);
                        int end = parse.IndexOf(')');
                        string com = parse.Substring(0, end);

                        string sn = i.Split('\\')[^1];


                        Debug.WriteLine($"{com}: {sn}");
                        ret.Add(new Tuple<string, string>(com, sn));
                    }
                }
            }
            return ret;
        }

        public bool Connect(string comPort)
        {
            //Console.WriteLine("COMPORT: " + comPort);
            if (comPort.Length < 2)
            {
                //Console.WriteLine("Failed to connect, select a port");
                return false;
            }
            // Close port if already open
            if (connected)
            {
                // Will only trip if device is unplugged
                try
                {
                    _serialPort.Close();

                }
                catch { }
                connected = false;
            }
            // connect to comport 

            try
            {
                _serialPort.PortName = comPort;
                _serialPort.Open();
            }
            catch
            {
                return false;
            }
            connected = true;
            curConnected = comPort;
            return true;
        }


        /***
         * Disconnect from com port if one is open
         */
        public void Disconnect()
        {
            if (connected)
            {
                _serialPort.Close();
            }
            connected = false;
        }

        /***
         * Send a command to the device
         * 
         * Param command        ::      string command to send to device
         */
        public void SendCommand(string command, int buffer = 5)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(command);
            Stopwatch watch = new Stopwatch();
            watch.Start();

            if (connected)
            {
                Debug.WriteLine("Byte Count: " + bytes.Length);
                foreach (byte b in bytes)
                {
                    byte[] by = new byte[] { b };
                    _serialPort.Write(by, 0, by.Length);
                    while (watch.ElapsedMilliseconds < buffer)
                    { }
                    watch.Restart();
                }


            }

        }
        
        public void SendBytes(byte[] data, int buffer = 5)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            if (connected)
            {
                Debug.WriteLine("Byte Count: " + data.Length);
                _serialPort.Write(data, 0, data.Length);
                //foreach (byte b in data)
                //{
                //    byte[] by = new byte[] { b };
                //    _serialPort.Write(by, 0, by.Length);
                //    while (watch.ElapsedMilliseconds < buffer)
                //    { }
                //    watch.Restart();
                //}


            }

        }

        public bool IsConnected()
        {
            return connected;
        }

        public string Read(int counts = 30)
        {
            string lines = "";
            if (!connected)
            {
                return "";
            }
            try
            {

                Thread.Sleep(counts);
                lines += _serialPort.ReadExisting();
                //for (int i= 0; i< count; i++)
                //    lines += _serialPort.ReadLine() + '\n';
            }
            catch (TimeoutException)
            {
                Console.WriteLine("readline timeout");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return lines;
        }

        internal void AllowSecret()
        {
            byte[] esc = new byte[1];
            esc[0] = (byte)0x1B;

            _serialPort.Write(esc, 0, 1); Thread.Sleep(150);
            _serialPort.Write(Encoding.ASCII.GetBytes("e"), 0, 1); Thread.Sleep(150);
            _serialPort.Write(esc, 0, 1); Thread.Sleep(150);
            _serialPort.Write(Encoding.ASCII.GetBytes("n"), 0, 1); Thread.Sleep(150);
            _serialPort.Write(esc, 0, 1); Thread.Sleep(150);
            _serialPort.Write(Encoding.ASCII.GetBytes("i"), 0, 1); Thread.Sleep(150);
            _serialPort.Write(esc, 0, 1); Thread.Sleep(150);
            _serialPort.Write(Encoding.ASCII.GetBytes("g"), 0, 1); Thread.Sleep(150);
            _serialPort.Write(esc, 0, 1); Thread.Sleep(150);
            _serialPort.Write(Encoding.ASCII.GetBytes("m"), 0, 1); Thread.Sleep(150);
            _serialPort.Write(esc, 0, 1); Thread.Sleep(150);
            _serialPort.Write(Encoding.ASCII.GetBytes("a"), 0, 1); Thread.Sleep(150);
            _serialPort.Write(esc, 0, 1); Thread.Sleep(150);
            _serialPort.Write(Encoding.ASCII.GetBytes("\r"), 0, 1); Thread.Sleep(150);
            // Secret command takes a bit of time to register for whatever reason
            Thread.Sleep(10);
        }


    }
}

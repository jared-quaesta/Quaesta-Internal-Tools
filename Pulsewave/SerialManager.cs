using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pulsewave
{
    class SerialManager
    {
        private string com;
        private SerialPort _serialPort;
        private string lastCommand = "";
        Listener listener;
        private bool connected = false;
        public string latest = "";

        public SerialManager()
        {
            //com = GetCom();
            _serialPort = new SerialPort
            {
                ReadTimeout = 200,
                WriteTimeout = 200,
                WriteBufferSize = 1024,
                BaudRate = 115200,
            };
        }


        public static List<string> GetComs()
        {
            List<string> coms = new List<string>();
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

                foreach (var i in ports)
                {
                    if (i.Contains("STMicroelectronics"))
                    {
                        int start = i.IndexOf('(') + 1;
                        string parse = i.Substring(start);
                        int end = parse.IndexOf(')');
                        coms.Add(parse.Substring(0, end));
                    }
                }
            }
            return coms ;
        }

        private void DataReceivedHandler(
                       object sender,
                       SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            //Debug.WriteLine("Data Received:");
            //Debug.Write(indata);

            listener.AddData(indata, lastCommand);
            //Debug.WriteLine(indata);

        }

        public SerialPort LinkTerm(Listener listener)
        {
            this.listener = listener;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            return _serialPort;
        }


        public void Disconnect()
        {
            if (connected)
            {
                _serialPort.Close();
            }
            connected = false;
        }

        public void SendCommand(string command, string type = "")
        {
            byte[] bytes = Encoding.ASCII.GetBytes(command + "\r\n");
            lastCommand = command.ToLower().Trim('\n','\r',' ');
            if (connected)
            {
                //Console.WriteLine(command);
                //_serialPort.WriteLine(command);

                foreach (byte b in bytes)
                {
                    _serialPort.Write(new byte[] {b}, 0, 1);
                    //Thread.Sleep();
                }

            }

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
                Thread.Sleep(100);
                connected = false;
                return false;
            }
            connected = true;


            return true;

        }

        public string Read(int counts = 100)
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
    
    }
}

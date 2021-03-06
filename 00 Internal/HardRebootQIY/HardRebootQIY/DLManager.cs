using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;

namespace HardRebootQIY
{
    class DLManager
    {
        private string com;
        private SerialPort _serialPort;

        private bool connected = false;
        public string latest = "";

        public DLManager()
        {
            com = GetCom();
            _serialPort = new SerialPort
            {
                ReadTimeout = 200,
                WriteTimeout = 200,
                WriteBufferSize = 1024,
                BaudRate = 115200
            };
        }

        public string GetCom()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

                foreach (var i in ports)
                {
                    if (i.Contains("USB Serial Port"))
                    {
                        int start = i.IndexOf('(') + 1;
                        string parse = i.Substring(start);
                        int end = parse.IndexOf(')');
                        com = parse.Substring(0, end);
                        return com;
                    }
                }
            }
            return "None";
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

            if (connected)
            {
                //Console.WriteLine(command);
                //_serialPort.WriteLine(command);
                _serialPort.Write(bytes, 0, bytes.Length);

                string handshake = "";
                if (type.Equals("ini"))
                {
                    handshake += Read(100);
                    while (!handshake.Contains("Main INI param updated"))
                    {
                        Thread.Sleep(100);
                        _serialPort.Write(Encoding.ASCII.GetBytes("\r\n"), 0, Encoding.ASCII.GetBytes("\r\n").Length);
                        handshake += Read(100);
                    }
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
                Debug.WriteLine("Sleeping 100ms");
                connected = false;
                return false;
            }
            Debug.WriteLine("Opened Port " + comPort);
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

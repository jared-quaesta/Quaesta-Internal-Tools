using GeneralFirstPhase.SerialTools;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralFirstPhase.SerialTools
{
    class SerialDataloggerManager
    {
        internal string com;
        internal SerialDataloggerListener listener;
        private SerialPort _serialPort;
        private bool connected = false;
        private string curConnected = "";
        private string lastCom = "";
        string serial;

        public SerialDataloggerManager()
        {
            listener = new SerialDataloggerListener();
            _serialPort = new SerialPort
            {
                ReadTimeout = 200,
                WriteTimeout = 200,
                WriteBufferSize = 1024,
                BaudRate = 115200,
                DtrEnable = true
            };
            _serialPort.DataReceived += (sender, e) => listener.NewData(_serialPort.ReadExisting(), lastCom);
        }
        public static List<string> GetComs()
        {
            List<string> ret = new List<string>();
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString() + p["DeviceID"].ToString());

                foreach (var i in ports)
                {
                    if (i.Contains("USB Serial Port"))
                    {
                        int start = i.IndexOf('(') + 1;
                        string parse = i.Substring(start);
                        int end = parse.IndexOf(')');
                        string com = parse.Substring(0, end);

                        string sn = i.Split('\\')[i.Split('\\').Length - 1];

                        ret.Add(com);
                    }
                }
            }
            return ret;
        }

        public bool Connect(string comPort)
        {
            if (comPort.Length < 2)
            {
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
        public void Disconnect()
        {
            if (connected)
            {
                _serialPort.Close();
            }
            connected = false;
        }

        public void SendCommand(string command, int buffer = 3)
        {
            // is port open? Will crash if not.

            byte[] bytes = Encoding.ASCII.GetBytes(command);
            lastCom = command.ToLower().Trim('\r', '\n', ' ');
            if (connected)
            {
                try
                {
                    if (buffer == 0)
                    {
                        _serialPort.Write(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        foreach (byte b in bytes)
                        {
                            byte[] by = new byte[] { b };
                            _serialPort.Write(by, 0, by.Length);
                            Thread.Sleep(buffer);
                        }
                    }

                }
                catch
                {
                    Debug.WriteLine($"Unable to send command: {command.Trim('\r', '\n', ' ')}");
                }

            }

        }

        public bool IsConnected()
        {
            return _serialPort.IsOpen && connected;
        }

        internal async Task<bool> QueryCycle()
        {
            SendCommand("showcycle\r\n", 0);
            return await Task.Run(() =>
            {
                Stopwatch watch = Stopwatch.StartNew();
                while (!listener.GotCycle())
                {
                    if (watch.ElapsedMilliseconds > 10000)
                    {
                        return false;
                    }
                    Thread.Sleep(20);
                }
                return true;
            });
        }

        internal async Task Relays(string relay)
        {
            SendCommand($"relays={relay}\r\n", 0);
            await Task.Run(() =>
            {
                Stopwatch watch = Stopwatch.StartNew();
                while (!listener.GotINI())
                {
                    if (watch.ElapsedMilliseconds > 10000)
                    {
                        return false;
                    }
                    Thread.Sleep(20);
                }
                return true;
            });
        }

        internal async Task INICommand(string cmd)
        {
            SendCommand(cmd, 0);
            Thread.Sleep(10);
            SendCommand("\r\n");
            await Task.Run(() =>
            {
                int tries = 0;
                Stopwatch watch = Stopwatch.StartNew();
                while (!listener.GotINI())
                {
                    if (watch.ElapsedMilliseconds > 3000)
                    {
                        tries++;
                        watch.Restart();
                        SendCommand(cmd, 0);
                    }
                    if (tries == 5)
                    {
                        return false;
                    }
                    Thread.Sleep(20);
                }
                return true;
            });
        }

        internal async Task<string> GetCS215()
        {
            string cmd = "showcs215\r\n";
            SendCommand(cmd, 0);
            await Task.Run(() =>
            {
                int tries = 0;
                Stopwatch watch = Stopwatch.StartNew();
                while (!listener.GotCS215())
                {
                    if (watch.ElapsedMilliseconds > 3000)
                    {
                        tries++;
                        watch.Restart();
                        SendCommand(cmd, 0);
                    }
                    if (tries == 5)
                    {
                        return false;
                    }
                    Thread.Sleep(20);
                }
                return true;
            });
            return listener.GetCS215();
        }

        internal async Task<string> GetR8(char addr)
        {
            string cmd = $"{addr}R8!\r\n";
            SendCommand(cmd, 0);
            await Task.Run(() =>
            {
                int tries = 0;
                Stopwatch watch = Stopwatch.StartNew();
                while (!listener.GotSDIResponse())
                {
                    if (watch.ElapsedMilliseconds > 3000)
                    {
                        tries++;
                        watch.Restart();
                        SendCommand(cmd, 0);
                    }
                    if (tries == 5)
                    {
                        return false;
                    }
                    Thread.Sleep(20);
                }
                return true;
            });
            return listener.GetR8();
        }

        internal string GetCS215Sync()
        {
            string cmd = "showcs215\r\n";
            SendCommand(cmd, 0);

            int tries = 0;
            Stopwatch watch = Stopwatch.StartNew();
            while (!listener.GotCS215())
            {
                if (watch.ElapsedMilliseconds > 3000)
                {
                    tries++;
                    watch.Restart();
                    SendCommand(cmd, 0);
                }
                if (tries == 5)
                {
                    break;
                }
                Thread.Sleep(20);
            }

            return listener.GetCS215();
        }
    }
}

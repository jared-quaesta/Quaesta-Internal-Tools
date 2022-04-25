using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversalUpdate.TCP_UDP
{
    class UDPManager
    {
        private UdpClient udpClient = new UdpClient();
        private int DISCPORT = 15334;
        private int LANPORT = 30718;

        internal bool listening = false;

        private MainForm main;

        private string data = "";

        public UDPManager(MainForm main)
        {
            this.main = main;
        }
        public void SearchNeuchQIY()
        {
            if (!listening)
                StartListeningQIYAsync();

            var QIY = Encoding.UTF8.GetBytes("neuchrometer report\r\n");
            udpClient.Send(QIY, QIY.Length, "255.255.255.255", DISCPORT);
        }
        public void SearchNeuchLantronix()
        {

            if (!listening)
                StartListeningLANAsync();
            var LANTRONIX = Encoding.UTF8.GetBytes("neuchrometer\r\n");
            udpClient.Send(LANTRONIX, LANTRONIX.Length, "255.255.255.255", LANPORT);
        }

        private async Task<bool> StartListeningQIYAsync(int timeoutms = 5000)
        {
            listening = true;
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, DISCPORT));
            udpClient.Client.ReceiveTimeout = 100;

            var from = new IPEndPoint(0, 0);

            await Task.Run(() =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < timeoutms)
                {
                    try
                    {
                        var recvBuffer = udpClient.Receive(ref from);
                        data += Encoding.UTF8.GetString(recvBuffer).Trim() + "\n";
                        main.Invoke((MethodInvoker)delegate
                        {
                            main.ParseUDP(data);
                        });
                    }
                    catch { }
                }
            });
            listening = false;
            Debug.WriteLine("Stopped listening.");
            udpClient.Close();
            udpClient = new UdpClient();
            return true;
        }
        private async void StartListeningLANAsync(int timeoutms = 500)
        {
            listening = true;
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, LANPORT));
            udpClient.Client.ReceiveTimeout = 100;

            var from = new IPEndPoint(0, 0);

            await Task.Run(() =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < timeoutms)
                {

                    try
                    {
                        var recvBuffer = udpClient.Receive(ref from);
                        data += Encoding.UTF8.GetString(recvBuffer).Trim() + "\n";
                        Debug.WriteLine(data);
                        main.Invoke((MethodInvoker)delegate
                        {
                            main.ParseUDP(data);
                        });
                    }
                    catch { }
                }
            });

            listening = false;
            Debug.WriteLine("Stopped listening.");
            udpClient.Close();
            udpClient = new UdpClient();
        }

        public string GetData()
        {
            string ret = data;
            data = "";
            return ret;
        }
    }
}

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Universal_NPM_Interface.Serial;

namespace Universal_NPM_Interface.Controls
{
    public partial class NPMParamControl : UserControl
    {
        private SerialNPMManager serialMan;

        public NPMParamControl()
        {
            InitializeComponent();
        }

        internal NPMParamControl(SerialNPMManager serialMan)
        {
            this.serialMan = serialMan;
            InitializeComponent();
        }

        internal async void TryQuery()
        {
            paramFlow.Controls.Clear();
            if (serialMan.IsConnected())
            {
                serialMan.listener.ClearInfo();
                serialMan.SendCommand("info\r\n");
                await Task.Run(() => 
                {
                    Thread.Sleep(100);
                });
                foreach (Tuple<string, string> pair in serialMan.listener.ParseInfo())
                {
                    SingleParameterControl newParam = new SingleParameterControl();
                    newParam.SetData(pair);
                    paramFlow.Controls.Add(newParam);
                }
            }
            
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            TryQuery();
        }
    }
}

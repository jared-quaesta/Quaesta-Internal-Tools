using System;
using System.Windows.Forms;

namespace Universal_NPM_Interface.Controls
{
    public partial class SingleParameterControl : UserControl
    {
        public SingleParameterControl()
        {
            InitializeComponent();
        }

        internal void SetData(Tuple<string, string> pair)
        {
            paramName.Text = pair.Item1;
            paramValue.Text = pair.Item2;
        }
    }
}

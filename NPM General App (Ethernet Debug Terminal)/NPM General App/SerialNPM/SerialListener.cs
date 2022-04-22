using System;
using System.Collections.Generic;
using System.Text;

namespace NPM_General_App.SerialNPM
{
    class SerialListener
    {
        internal SerialNPMLink link;

        internal SerialListener(SerialNPMLink link)
        {
            this.link = link;
        }

        internal void NewData(string data)
        {
            link.NewData(data);
        }

    }
}

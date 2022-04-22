using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDI12AddressTool
{
    class Entry
    {
        internal string sn;
        internal string sdi;
        internal string tubesn;
        internal string tubeowner;
        internal string tubetype;
        internal string note;
        internal string firmware;

        public Entry(string sn, string sdi, string tubesn, string tubeowner, string tubetype, string note, string firmware)
        {
            this.sn = sn;
            this.sdi = sdi;
            this.tubesn = tubesn;
            this.tubeowner = tubeowner;
            this.tubetype = tubetype;
            this.note = note;
            this.firmware = firmware;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralFirstPhase.Data
{
    class HeaterDataResults
    {
        private string serial;
        private DateTime time;
        private double voltage;
        private int npmTemp;
        private int cs215Temp;
        private string sdevHGM;
        private string psHGM;

        internal string Serial { get => serial; set => serial = value; }
        internal DateTime Time { get => time; set => time = value; }
        internal double Voltage { get => voltage; set => voltage = value; }
        internal int NpmTemp { get => npmTemp; set => npmTemp = value; }
        internal int Cs215Temp { get => cs215Temp; set => cs215Temp = value; }
        internal string SdevHGM { get => sdevHGM; set => sdevHGM = value; }
        internal string PsHGM { get => psHGM; set => psHGM = value; }
    }
}

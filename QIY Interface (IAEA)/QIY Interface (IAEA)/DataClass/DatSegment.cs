using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIY_Interface__IAEA_
{
    class DatSegment
    {
        // ts
        private DateTime startTime = DateTime.MinValue;
        private DateTime endTime = DateTime.MinValue;
        private string name;
        private string sn;
        private string recordPeriod;
        private string recsPerHGM;
        private string newFilePeriod;
        private string highVoltage;
        private string gain;
        private string ld;
        private string ud;
        private string logHGM;
        private string pulseThresh;
        private string logExtPulses;
        private string logtdegc;
        private string logRh;
        private string logBatt;
        private string logSig;
        private string header;
        private int numRecords = 0;
        private string pcnts;

        // hgm specific
        private string nbins;
        private string modelv;
        private string hrInterval;
        private string secInterval;
        private List<Tuple<int, int>> hgm = new List<Tuple<int, int>>();
        private DateTime hgmTime = DateTime.MinValue;
        private string elapsedSecs;

        public DatSegment(DateTime resDate)
        {
            hgmTime = resDate;
        }


        public DatSegment(DateTime resDate, DatSegment datSegment)
        {
            hgmTime = resDate;
            Name = datSegment.Name;
            Sn = datSegment.Sn;
            modelv = datSegment.Modelv;
            recordPeriod = datSegment.RecordPeriod;
            recsPerHGM = datSegment.RecsPerHGM;
            Ld = datSegment.Ld;
            Ud = datSegment.Ud;
            highVoltage = datSegment.HighVoltage;
            gain = datSegment.Gain;
            nbins = datSegment.Nbins;
        }

        public DatSegment()
        {
        }

        public string Header { get => header; set => header = value; }
        public string Nbins { get => nbins; set => nbins = value; }
        public string Modelv { get => modelv; set => modelv = value; }
        public string HrInterval { get => hrInterval; set => hrInterval = value; }
        public string SecInterval { get => secInterval; set => secInterval = value; }
        public List<Tuple<int, int>> Hgm { get => hgm; set => hgm = value; }
        internal DateTime StartTime { get => startTime; set => startTime = value; }
        internal DateTime EndTime { get => endTime; set => endTime = value; }
        internal string Name { get => name; set => name = value; }
        internal string Sn { get => sn; set => sn = value; }
        internal string RecordPeriod { get => recordPeriod; set => recordPeriod = value; }
        internal string RecsPerHGM { get => recsPerHGM; set => recsPerHGM = value; }
        internal string NewFilePeriod { get => newFilePeriod; set => newFilePeriod = value; }
        internal string HighVoltage { get => highVoltage; set => highVoltage = value; }
        internal string Gain { get => gain; set => gain = value; }
        internal string Ld { get => ld; set => ld = value; }
        internal string Ud { get => ud; set => ud = value; }
        internal string LogHGM { get => logHGM; set => logHGM = value; }
        internal string PulseThresh { get => pulseThresh; set => pulseThresh = value; }
        internal string LogExtPulses { get => logExtPulses; set => logExtPulses = value; }
        internal string Logtdegc { get => logtdegc; set => logtdegc = value; }
        internal string LogRh { get => logRh; set => logRh = value; }
        internal string LogBatt { get => logBatt; set => logBatt = value; }
        internal string LogSig { get => logSig; set => logSig = value; }
        public int NumRecords { get => numRecords; set => numRecords = value; }
        public DateTime HgmTime { get => hgmTime; set => hgmTime = value; }
        public string ElapsedSecs { get => elapsedSecs; set => elapsedSecs = value; }
        public string Pcnts { get => pcnts; set => pcnts = value; }
    }
}

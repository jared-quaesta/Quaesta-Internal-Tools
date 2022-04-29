using System;

namespace QIXLPTesting
{
    class NPMData
    {
        // info
        private string sn;
        private string family;
        private string model;
        private string firmware;
        private DateTime edited;
        private string note;
        private string tube;

        // tests
        private bool? volt;
        private bool? led;
        private bool? sdev;
        private bool? pulsesim;
        private bool? temp;
        private bool? sdi;
        private bool? heatNoise;
        private bool? heatTemp;
        private bool? heatPulsesim;
        private bool? heatVolt;


        public string Sn { get => sn; set => sn = value; }
        public string Family { get => family; set => family = value; }
        public string Model { get => model; set => model = value; }
        public string Firmware { get => firmware; set => firmware = value; }
        public DateTime Edited { get => edited; set => edited = value; }
        public string Note { get => note; set => note = value; }
        public string Tube { get => tube; set => tube = value; }
        public bool? Volt { get => volt; set => volt = value; }
        public bool? Led { get => led; set => led = value; }
        public bool? Sdev { get => sdev; set => sdev = value; }
        public bool? Pulsesim { get => pulsesim; set => pulsesim = value; }
        public bool? Temp { get => temp; set => temp = value; }
        public bool? Sdi { get => sdi; set => sdi = value; }
        public bool? HeatNoise { get => heatNoise; set => heatNoise = value; }
        public bool? HeatTemp { get => heatTemp; set => heatTemp = value; }
        public bool? HeatPulsesim { get => heatPulsesim; set => heatPulsesim = value; }
        public bool? HeatVolt { get => heatVolt; set => heatVolt = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pulsewave
{
    public class Listener
    {
        private IndividualInterfaceControl controller;
        string lastCommand = "";
        string pulsewave = "";
        string hgm = "";
        string incomplete = "";

        bool gotPulse = false;
        public Listener(IndividualInterfaceControl control)
        {
            controller = control;
        }

        internal void AddData(string latest, string lastCommand)
        {
            controller.NewData(latest);
            this.lastCommand = lastCommand;

            if (!latest.EndsWith("\n"))
            {
                incomplete += latest;
                return;
            } else
            {
                latest = incomplete + latest;
                incomplete = "";
            }

            if (lastCommand.Contains("pulsewave"))
            {
                ParsePulsewave(latest);
            }
            else if (lastCommand.Equals("hgm"))
            {
                ParseHGM(latest);
            }
        }

        private void ParseHGM(string latest)
        {
            if (!latest.Contains(",")) hgm += latest;
            else
            {
                string[] splitLines = (this.hgm + latest).Split('\n');
                List<int> hgm = new List<int>();
                string info = "";
                foreach (string line in splitLines)
                {
                    string lineTrimmed = line.Trim();
                    string[] splitTrimmed = lineTrimmed.Split(' ');
                    if (line.Contains(","))
                    {
                        info = line.Trim('\r', ' ');
                    }
                    if (!int.TryParse(splitTrimmed[0], out int index)) continue;
                    int val = int.Parse(splitTrimmed[splitTrimmed.Length - 1]);
                    hgm.Add(val);
                }
                controller.NewHGMData(hgm, info);
                this.hgm = "";
            }
        }

        private void ParsePulsewave(string latest)
        {
            if (!latest.Contains("AdcLoops")) pulsewave += latest;
            else
            {
                string[] splitLines = (pulsewave + latest).Split('\n');
                List<int> wave = new List<int>();
                string info = "";
                foreach(string line in splitLines)
                {
                    //nWavePoints = 396 AdcLoops = 33
                    //pulsewave
                    //0 688
                    //1 739
                    //2 785
                    //3 826
                    //4 863
                    //5 894
                    string lineTrimmed = line.Trim();
                    string[] splitTrimmed = lineTrimmed.Split(' ');
                    if (line.Contains("AdcLoops"))
                    {
                        info = line.Trim('\r', ' ');
                    }
                    if (!int.TryParse(splitTrimmed[0], out int index)) continue;
                    if (int.TryParse(splitTrimmed[splitTrimmed.Length-1], out int val))
                        wave.Add(val);
                }
                controller.NewPSData(wave, info);

                pulsewave = "";
            }
        }

        internal bool GotPW()
        {
            if (gotPulse)
            {
                gotPulse = false;
                return true;
            }
            else return false;
        }
    }
}
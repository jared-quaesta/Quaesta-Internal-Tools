using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace QIY_Interface__IAEA_
{
    internal class TCPListener
    {
        internal MainForm mainForm;
        private bool gotDir = false;

        private string partialDat = "";
        internal TerminalForm term;

        internal string retrieved = "";
        private string partialHGM = "";

        public TCPListener(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        public TCPListener(MainForm mainForm, TerminalForm term)
        {
            this.mainForm = mainForm;
        }

        internal void UpdateConnected(bool connected)
        {
            //throw new NotImplementedException();
        }

        internal void IncomingData(string strDat, string lastCommand)
        {
            //Debug.WriteLine("NEWDATA\n" + strDat);

            strDat = strDat.Trim(Convert.ToChar(0));



            if (!strDat.EndsWith("\n"))
            {
                partialDat += strDat;
                return;
            }
            if (partialDat.Length > 0)
            {
                strDat = partialDat + strDat;
                partialDat = "";
            }
            mainForm.Invoke((MethodInvoker)delegate
            {
                mainForm.NewTermData(strDat);
            });
            if (lastCommand.Equals("info") || lastCommand.Equals("status"))
            {
                ParseInfo(strDat, lastCommand.Equals("status"), lastCommand);
            }
            else if (lastCommand.Contains("/"))
            {
                ParseDir(strDat, lastCommand);
            }
            else if (lastCommand.Equals("time"))
            {
                ParseTime(strDat);
            }
            else if (lastCommand.Equals("diskinfo"))
            {
                ParseDiskInfo(strDat);
            }
            else if (lastCommand.Equals("hgm"))
            {
                ParseHGM(strDat);
            } else
            {
                string[] splitStr = strDat.Split('\n');
                foreach (string line in splitStr)
                {
                    CheckForPrintDat(line);
                }
            }
        }

        private void ParseHGM(string strDat)
        {
            string[] splitStr = strDat.Split('\n');
            foreach (string line in splitStr)
            {
                CheckForPrintDat(line);
            }

            if (!strDat.Contains("Total="))
            {
                partialHGM += strDat;
            } else
            {
                mainForm.Invoke((MethodInvoker)delegate
                {
                    mainForm.NewHGM(partialHGM + strDat);
                    
                });
                partialHGM = "";
            }
        }

        private void ParseDiskInfo(string strDat)
        {
            string[] splitStr = strDat.Split('\n');
            foreach (string line in splitStr)
            {
                CheckForPrintDat(line);
                if (line.Contains("0:/"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateInternalDisk(line.Trim());
                    });
                } 
                else if (line.Contains("1:/"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateExternalDisk(line.Trim());
                    });
                }
            }
        }

        private void CheckForPrintDat(string line)
        {
            // printdat
            // 2022/03/07 11:26:13,  2822,         0, 27.7,14.1,11.85,0,2579\r\n
            // 2022/03/07 11:27:06,  2875,         0,11.84,0,46D1\r\n

            if (!line.Contains(",")) return;

            string[] splitLine = line.Split(',');

            if (DateTime.TryParseExact(splitLine[0], "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
            {
                mainForm.Invoke((MethodInvoker)delegate
                {
                    mainForm.IncomingPrintDat(line.Trim('\r','\n',' '));

                });
            }
        }

        private void ParseTime(string strDat)
        {
            string[] splitStr = strDat.Split('\n');
            foreach (string line in splitStr)
            {
                CheckForPrintDat(line);
                string val = line.Trim('\n', '\r', ' ');
                // 2000/01/09 08:00:25
                if (DateTime.TryParseExact(val, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateDate(dt.ToString("yyyy/MM/dd hh:mm:ss"));
                    });
                }
                else if (DateTime.TryParseExact(val, "yyyy/MM/dd,HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt2))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateDate(dt2.ToString("yyyy/MM/dd hh:mm:ss"));
                    });
                }
            }
        }

        internal void ClearDRPoints()
        {
            mainForm.Invoke((MethodInvoker)delegate
            {
                mainForm.UpdateExternalInfoDRBox("", true);
                mainForm.UpdateExternalDRBox("", true);
                mainForm.UpdateInternalInfoDRBox("", true);
                mainForm.UpdateInternalDRBox("", true);
            });
        }

        private void ParseDir(string strDat, string lastCommand)
        {
            if (strDat.Trim().ToLower().Equals(lastCommand)) return;
            if (strDat.Contains("SD Card") || strDat.Contains("ERROR")) gotDir = true;
            mainForm.Invoke((MethodInvoker)delegate
            {
                foreach (string strDatItem in strDat.Split('\n'))
                {
                    CheckForPrintDat(strDatItem);
                    if (strDatItem.Trim().Length == 0) continue;
                    if (strDatItem.Trim('\n', '\r').Length == 0) continue;
                    if (strDatItem.Contains("-"))
                    {
                        if (lastCommand.Contains("0"))
                            mainForm.UpdateInternalDRBox(strDatItem);
                        else mainForm.UpdateExternalDRBox(strDatItem);
                    }
                    else
                    {
                        if (lastCommand.Contains("0"))
                            mainForm.UpdateInternalInfoDRBox(strDatItem);
                        else mainForm.UpdateExternalInfoDRBox(strDatItem);

                        if (strDatItem.Contains("File(s)"))
                        {
                            if (lastCommand.Contains("0"))
                                mainForm.UpdateInternalDRDetails(strDatItem.Trim().Split(' ')[0]);
                            else
                                mainForm.UpdateExternalDRDetails(strDatItem.Trim().Split(' ')[0]);
                        }
                    }
                }
                
            });

        }

        internal bool GotDir()
        {
            if (gotDir)
            {
                gotDir = false;
                return true;
            } return false;
        }

        private void ParseInfo(string strDat, bool status, string lastcommand)
        {
            foreach (string line in strDat.Split('\n'))
            {
                string[] splitLine = line.Split(' ');
                string data = splitLine[splitLine.Length - 1].Trim('\r', '\n');
                CheckForPrintDat(line);
                if (line.Contains("Serial Number"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenSN(data);
                    });
                }

                else if (line.Contains("RecordPeriod(Sec)"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateRecordPeriod(data);
                        if (status)
                        {
                            mainForm.UpdateStatRecordPeriod(data);
                        }
                    });
                } 
                else if (line.Contains("HgmMode"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateHGMMode(data);
                    });
                }

                else if (line.Contains("RecordsPerHGM"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateRecordsPerHGM(data);
                        if (status)
                        {
                            mainForm.UpdateStatRecordsPerHGM(data);
                        }
                    });
                }

                else if (line.Contains("NewFilePeriod"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateNewFilePeriod(data);
                        if (status)
                        {
                            mainForm.UpdateStatNewFilePeriod(data);
                        }
                    });
                }

                else if (line.Contains("PrintDAT"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenPrintDAT(data);
                    });
                }

                else if (line.Contains("TemperatureON"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateTemperatureOn(data);
                    });
                }
                else if (line.Contains("Current Record"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateStatCurrRec(data);
                    });
                }

                else if (line.Contains("UpTime"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateUptime(data);
                    });
                }

                else if (line.Contains("HumidityON"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateHumidityOn(data);
                    });
                }
                else if (line.Contains("BatteryON"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {

                        mainForm.UpdateBatteryOn(data);
                    });
                }
                else if (line.Contains("Battery"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        if (status)
                        {
                            mainForm.UpdateStatBatt(data);
                        }
                    });
                }

                else if (line.Contains("SignalON"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateSignalOn(data);
                    });
                }

                else if (line.Contains("SaveHGM") || line.Contains("LogHGM"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenSaveHGM(data);
                        if (status)
                        {
                            mainForm.UpdateStatSaveHGM(data);
                        }
                    });
                }


                else if (line.Contains("LogTemp"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateTemperatureOn(data);
                    });
                }

                else if (line.Contains("LogHum"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateHumidityOn(data);
                    });
                }

                else if (line.Contains("SaveDAT") || line.Contains("LogDAT"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenSaveDAT(data);
                        if (status)
                        {
                            mainForm.UpdateStatSaveDat(data);
                        }
                    });
                }

                else if (line.Contains("PrintHGM"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenPrintHGM(data);
                    });
                }

                else if (line.Contains("SaveBIN"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenSaveBin(data);
                        if (status)
                        {
                            mainForm.UpdateStatSaveBin(data);
                        }
                    });
                }

                else if (line.Contains("PulseON")||line.Contains("PulseCounterON"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdatePulseCounterOn(data);
                    });
                }

                else if (line.Contains("Model Version"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenMV(data);
                        
                    });
                }
                else if (line.Contains("MaxVoltage"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenMaxVoltage(data);
                    });
                }
                
                else if (line.Contains("Set Voltage"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateStatSetHV(data);
                    });
                }
                else if (line.Contains("Measured Voltage"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateStatMeasuredHV(data);
                    });
                }
                else if (line.Contains("Voltage"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenVoltage(data);
                    });
                }
                else if (line.Contains("Gain"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenGain(data);
                    });
                }
                else if (line.Contains("Temperature"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateStatTemp(data);
                    });
                }
                else if (line.Contains("Humidity"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateStatRH(data);
                    });
                }
                
                else if (line.Contains("Signal"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateStatSignal(data);
                    });
                }
                else if (line.Contains("LowerDisc") || line.Contains("DiscLow"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenDiscLow(data);
                    });
                }

                else if (line.Contains("UpperDisc") || line.Contains("DiscHigh"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenDiscHigh(data);
                    });
                }

                else if (line.Contains("nBins"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenNBins(data);
                    });
                }

                else if (line.Contains("DeadTime"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenDeadTime(data);
                    });
                }

                else if (line.Contains("LEDMode"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenLEDMode(data);
                    });
                }

                else if (line.Contains("PulseLevel"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenPulseLevel(data);
                        if (status)
                        {
                            mainForm.UpdateStatPulselevel(data);
                        }
                    });
                }

                else if (line.Contains("DAT FileName"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        string fileData = "";
                        for (int i = 2; i < splitLine.Length; i++)
                        {
                            if (splitLine[i].Trim().Length == 0) continue;
                            fileData += splitLine[i] + " ";
                        }
                        mainForm.UpdateStatDatFile(fileData.Trim());
                    });
                }
                else if (line.Contains("BIN FileName"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        string fileData = "";
                        for (int i = 2; i < splitLine.Length; i++)
                        {
                            if (splitLine[i].Trim().Length == 0) continue;
                            fileData += splitLine[i] + " ";
                        }
                        mainForm.UpdateStatBinFile(fileData);
                    });
                }
                else if (line.Contains("HGM FileName"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        string fileData = "";
                        for (int i = 2; i < splitLine.Length; i++)
                        {
                            if (splitLine[i].Trim().Length == 0) continue;
                            fileData += splitLine[i] + " ";
                        }
                        mainForm.UpdateStatHGMFile(fileData);
                    });
                }
                else if (line.Contains("Name"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenName(data);
                        
                        if (status)
                        {
                            mainForm.UpdateStatName(data);
                        }
                    });
                }

                else if (line.Contains("QSim"))
                {
                   // link.SetQsim(tryMe);
                }

                else if (line.Contains("Firmware Version"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenFW(data);
                        if (status)
                        {
                            mainForm.UpdateStatFwVersion(data);
                        }
                    });
                }

                else if (line.Contains("HgmMode"))
                {
                   // link.SetHGMMode(tryMe);
                }

                else if (line.Contains("PeakMode"))
                {
                   // link.SetPeakMode(tryMe);
                }

                else if (line.Contains("PulseSim"))
                {
                   // link.SetPulseSim(tryMe);
                }

                else if (line.Contains("McsLength"))
                {
                   // link.SetMcsLen(tryMe);
                }

                else if (line.Contains("McsDwell"))
                {
                   // link.SetMcsDwell(tryMe);
                }

                else if (line.Contains("McsPasses"))
                {
                   // link.SetMcsPasses(tryMe);
                }

                else if (line.Contains("DtResolution"))
                {
                   // link.SetDTRes(tryMe);
                }
                else if (line.Contains("MAC"))
                {
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.UpdateGenMAC(data);
                    });
                }
            }
            
        }
        private string[] SubArray(string[] data, int index, int length)
        {
            string[] result = new string[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
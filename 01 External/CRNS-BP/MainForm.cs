using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Core;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRNS_BP
{
    public partial class MainForm : Form
    {
        internal CRNSParser parser;
        private OpenFileDialog fileDialog = new OpenFileDialog();
        private FolderBrowserDialog folderDialog = new FolderBrowserDialog();
        string dataDirectory;
        string tileDirectory;
        string binDirectory;
        List<string> loadedFiles = new List<string>();

        //heatmaps
        HeatMapSeries countSeries = new HeatMapSeries()
        {
            X0 = 0,
            X1 = 99,
            Y0 = 0,
            Y1 = 99,
            Interpolate = true,
            RenderMethod = HeatMapRenderMethod.Bitmap,
            Data = new double[5,5]
        };

       

        PlotModel countModel = new PlotModel() 
        {
            Title = "Counts Per Hour"
        };

        // Plots
        LineSeries seriesTL = new LineSeries();
        LineSeries seriesTR = new LineSeries();
        LineSeries seriesBL = new LineSeries();
        LineSeries seriesBR = new LineSeries();
        PlotModel modelTL = new PlotModel();
        PlotModel modelTR = new PlotModel();
        PlotModel modelBL = new PlotModel();
        PlotModel modelBR = new PlotModel();

        Dictionary<string, PlotData> plotDataDictionary = new Dictionary<string, PlotData>();
        Bitmap curMap;
        HeatMapProcessor heatMapProcessor = new HeatMapProcessor();

        private string selected = "";

        public MainForm()
        {
            InitializeComponent();
        }

        private void importEntireCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = folderDialog.ShowDialog();
            if (res == DialogResult.Cancel | res == DialogResult.Abort) return;
            if (!Directory.Exists(folderDialog.SelectedPath)) return;
            foreach (string filePath in Directory.GetFiles(folderDialog.SelectedPath))
            {
                if (!filePath.ToLower().Contains(".rv")) continue;
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    parser.NewRV2Data(line);
                }
                if (parser.RV2Blocks.Count > 0)
                {
                    string metadata = parser.GetMetadata();
                    string json = parser.GetJson();
                    string[] original = lines;
                    BuildDirectory(json, metadata, lines, Path.GetFileNameWithoutExtension(filePath));
                    UpdateMapPins();
                }

                parser.ClearData();
            }

        }

        private void importIndividualRV2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = fileDialog.ShowDialog();
            if (res == DialogResult.Cancel | res == DialogResult.Abort) return;
            if (!File.Exists(fileDialog.FileName)) return;
            string[] lines = File.ReadAllLines(fileDialog.FileName);
            foreach (string line in lines)
            {
                parser.NewRV2Data(line);
            }
            if (parser.RV2Blocks.Count > 0)
            {
                string metadata = parser.GetMetadata();
                string json = parser.GetJson();
                BuildDirectory(json, metadata, lines, Path.GetFileNameWithoutExtension(fileDialog.FileName));
                UpdateMapPins();
            }
            else
            {
                MessageBox.Show("Could not extract data.", "ERROR");
            }

            parser.ClearData();
        }

        private void BuildDirectory(string json, string meta, string[] original, string name)
        {
            string path = Path.Combine(dataDirectory, name);
            if (Directory.Exists(path)) Directory.Delete(path, true);

            string jsonName = $"{name}.json";
            string metaName = $"{name}.meta";
            Directory.CreateDirectory(path);
            File.WriteAllText(Path.Combine(path, jsonName),json);
            File.WriteAllText(Path.Combine(path, metaName),meta);
            File.WriteAllLines(Path.Combine(path, name + ".RV2"), original);
        }

        internal void SelectedData(string path)
        {
            selected = path;
            selectedCombo.Text = path;
            selectPanel.Visible = false;

            if (!Directory.Exists(path))
            {
                MessageBox.Show("Could Not Display Data. Has the stored directory been removed?");
                return;
            }
            string name = Path.GetFileName(path);
            string jsonPath = Path.Combine(path, $"{name}.json");
            if (!File.Exists(jsonPath))
            {
                MessageBox.Show("Could not display data. Has the stored file been removed?");
                return;
            }

            string json = File.ReadAllText(jsonPath);

            // deserialize
            List<RV2Block> blocks = JsonConvert.DeserializeObject<List<RV2Block>>(json);
            MakeHeaderPlots(blocks);
            List<double[]> locs = new List<double[]>();
            foreach (RV2Block block in blocks)
            {
                if (block.GetPosition() == null) continue;
                locs.Add(block.GetPosition());
            }
            rpCombo.Items.Clear();
            int rp = blocks[0].recordPeriod;
            rpCombo.Text = rp.ToString();
            for (int m = 1; m < 5; m++)
            {
                rpCombo.Items.Add((m*rp).ToString());
            }

            mapContainer.RefreshLineLayer(locs);
            RefreshMap();
        }

        private async void MakeHeatBitmap(List<RV2Block> blocks, string name, double p, int roi)
        {
            

        }

        private void MakeHeaderPlots(List<RV2Block> blocks)
        {
            dropCheckBoxBL.Clear();
            dropCheckBoxBR.Clear();
            dropCheckBoxTL.Clear();
            dropCheckBoxTR.Clear();

            plotDataDictionary.Clear();
            foreach (RV2Block block in blocks)
            {
                double record = (double)block.data["//RecordNum"];
                foreach (string headerItem in block.data.Keys)
                {
                    if (headerItem.Equals("//RecordNum")) continue;
                    if (headerItem.Equals("Date")) continue;
                    if (!double.TryParse(block.data[headerItem].ToString(), out double val))
                        continue;

                    if (plotDataDictionary.ContainsKey(headerItem))
                    {
                        plotDataDictionary[headerItem].AddData(record, val);
                    } else
                    {
                        plotDataDictionary.Add(headerItem, new PlotData(headerItem));
                    }

                    if (!dropCheckBoxBL.ContainsItem(headerItem))
                    {
                        dropCheckBoxBL.AddItem(headerItem);
                        dropCheckBoxBR.AddItem(headerItem);
                        dropCheckBoxTL.AddItem(headerItem);
                        dropCheckBoxTR.AddItem(headerItem);

                        //dropCheckBox1.AddItem(headerItem);
                    }
                }
            }
            modelBL.Series.Clear();
            modelTL.Series.Clear();
            modelBR.Series.Clear();
            modelTR.Series.Clear();
            modelBL.InvalidatePlot(true);
            modelTL.InvalidatePlot(true);
            modelBR.InvalidatePlot(true);
            modelTR.InvalidatePlot(true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            dispCheckList.SetItemChecked(4, true);

            parser = new CRNSParser();

            // heatplotsetup

            countModel.Axes.Add(new LinearColorAxis
            {
                Palette = OxyPalette.Interpolate(256, new OxyColor[] {OxyColors.White, OxyColors.Yellow, OxyColors.Red, OxyColors.Black})
            }) ;
            countModel.Series.Add(countSeries);
            //countHeatPlt.Model = countModel;

            // setup plots on second tab
            plotTL.Model = modelTL;
            plotTR.Model = modelTR;
            plotBL.Model = modelBL;
            plotBR.Model = modelBR;

            dropCheckBoxBL.ReferencePlotModel(modelBL, this);
            dropCheckBoxBR.ReferencePlotModel(modelBR, this);
            dropCheckBoxTL.ReferencePlotModel(modelTL, this);
            dropCheckBoxTR.ReferencePlotModel(modelTR, this);


            // create directory structure if neccessary
            string startupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CRNS Backpack Mapping");
            dataDirectory = Path.Combine(startupPath, "Imported_Data");
            if (!Directory.Exists(dataDirectory)) Directory.CreateDirectory(dataDirectory);
            mapContainer.ReferenceMain(this);
            UpdateMapPins();

            tileDirectory = Path.Combine(dataDirectory, "Tiles");
            if (!Directory.Exists(tileDirectory)) Directory.CreateDirectory(tileDirectory);
            RefreshMap();

            
        }

        private void UpdateMapPins()
        {
            foreach (string path in Directory.GetDirectories(dataDirectory))
            {
                string name = Path.GetFileName(path);
                if (loadedFiles.Contains(name)) continue;
                if (!name.Contains("Tiles"))
                selectedCombo.Items.Add(path);
                loadedFiles.Add(name);

                // find json ser in folder and read data
                foreach (string file in Directory.GetFiles(path))
                {
                    if (file.Contains(".json"))
                    {
                        string json = File.ReadAllText(file);
                        List<RV2Block> block = JsonConvert.DeserializeObject<List<RV2Block>>(json);
                        if (block.Count > 0)
                        {
                            if (block[0].GetPosition() == null) continue;
                            double[] latLong = block[0].GetPosition();

                            if (latLong != null)
                                mapContainer.AddPin(latLong[0], latLong[1], Directory.GetParent(file).FullName);
                        }
                    }
                }

            }
        }

        internal void UpdateChart(PlotModel model, string text)
        {
            model.Series.Clear();
            if (text.Length == 0)
            {
                model.InvalidatePlot(true);
                return;
            }

            string[] values = text.Split(',');
            model.Axes.Clear();

            LinearAxis newX = new LinearAxis()
            {
                Title = "Record #",
                Position = AxisPosition.Bottom
            };
            LinearAxis newY = new LinearAxis()
            {
                Position = AxisPosition.Left
            };

            model.Axes.Add(newX);
            model.Axes.Add(newY);

            foreach (string item in values)
            {
                string title = item.Trim();

                if (!plotDataDictionary.ContainsKey(title)) continue;
                LineSeries newSeries = new LineSeries();
                newSeries.Title = title;
                PlotData plotData = plotDataDictionary[title];
                foreach (Tuple<double, double> datapoint in plotData.data)
                {
                    newSeries.Points.Add(new DataPoint(datapoint.Item1, datapoint.Item2));
                }
                model.Series.Add(newSeries);

            }
            model.Legends.Add(new Legend() { LegendPosition = LegendPosition.BottomRight});
            model.IsLegendVisible = true;
            model.InvalidatePlot(true);

        }

        private void comboTL_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            PlotModel model = (PlotModel)box.Tag;

            if (!plotDataDictionary.ContainsKey(box.SelectedItem.ToString())) return;

            LineSeries series = (LineSeries)model.Series[0];
            series.Points.Clear();
            PlotData plotData = plotDataDictionary[box.SelectedItem.ToString()];
            foreach (Tuple<double, double> datapoint in plotData.data)
            {
                series.Points.Add(new DataPoint(datapoint.Item1, datapoint.Item2));
            }
            model.Title = plotData.title;
            model.Axes.Clear();

            LinearAxis newX = new LinearAxis()
            {
                Title = "Record #",
                Position = AxisPosition.Bottom
            };
            LinearAxis newY = new LinearAxis()
            {
                Title = plotData.title,
                Position = AxisPosition.Left
            };

            model.Axes.Add(newX);
            model.Axes.Add(newY);


            model.InvalidatePlot(true);

        }

        internal void SetSelectedPinText(string str)
        {
            dispRecordLbl.Text = str;
        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void pSlider_Scroll(object sender, EventArgs e)
        {
            TrackBar pSlider = (TrackBar)sender;
            double p = (double)pSlider.Value / 100;
            pText.Text = $"{p:0.00}";
            pText.Refresh();
        }

        private async void genBtn_Click(object sender, EventArgs e)
        {
            string path = selected;
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Could Not Display Data. Has the stored directory been removed?");
                return;
            }
            string name = Path.GetFileName(path);
            string jsonPath = Path.Combine(path, $"{name}.json");
            if (!File.Exists(jsonPath))
            {
                MessageBox.Show("Could not display data. Has the stored file been removed?");
                return;
            }

            string json = File.ReadAllText(jsonPath);

            // deserialize
            List<RV2Block> blocks = JsonConvert.DeserializeObject<List<RV2Block>>(json);

            // Fill in plots
            
            double p = double.Parse(pText.Text);
            int r = int.Parse(roiText.Text);

            genCtsBtn.Enabled = false;
            resetData.Enabled = false;
            button1.Enabled = false;
            int rp = int.Parse(rpCombo.Text);
            await Task.Run(() =>
            {
                Tuple<double, double, List<Tuple<double[], string>>> minMax = 
                HeatMapProcessor.MakeTilesInverseDistance(blocks, tileDirectory, selected, p, r, cphGradientControl.GetGradient(),rp);
                Invoke((MethodInvoker)delegate
                {
                    if (minMax != null)
                    {

                        mapContainer.ReferenceData(minMax.Item3);
                        RefreshMap();
                        highCphBox.Text = $"{minMax.Item2:0.00}";
                        lowCphBox.Text = $"{minMax.Item1:0.00}";

                        cphGradientControl.SetMinMax(minMax.Item1, minMax.Item2);

                        //countSeries.Data = mapg;
                        //countModel.InvalidatePlot(true);
                        selectPanel.Visible = true;
                        recordSlider.Maximum = minMax.Item3.Count - 1;
                        mapContainer.AdjustIndex(0);
                    }

                    genCtsBtn.Enabled = true;
                    resetData.Enabled = true;
                    button1.Enabled = true;

                });
            });

        }

        private void roiSlider_Scroll(object sender, EventArgs e)
        {
            TrackBar pSlider = (TrackBar)sender;
            int r = pSlider.Value;
            roiText.Text = r.ToString();
            roiText.Refresh();
        }


        private async void genSMBtn_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(selected)) return;
            if (!double.TryParse(aBox.Text, out double a) ||
                !double.TryParse(bBox.Text, out double b) ||
                !double.TryParse(cBox.Text, out double c) ||
                !double.TryParse(socBox.Text, out double soc) ||
                !double.TryParse(lwBox.Text, out double lw) ||
                !double.TryParse(pdBox.Text, out double pd)
                )
                return;

            if (!double.TryParse(n0Box.Text, out double n0))
                n0 = -1;

            genSMBtn.Enabled = false;
            await Task.Run(() =>
            {
                if (!double.TryParse(highSmBox.Text, out double smMax) ||
                (!double.TryParse(lowSmBox.Text, out double smMin)))
                {
                    MessageBox.Show("Gradient max and min could not parse. Values must be numeric or decimal.", "Error");
                    return;
                }
                Invoke((MethodInvoker)delegate
                {
                    smGradientControl.SetMinMax(smMin, smMax);
                });
                Tuple<double, double, double> highLowSMr = HeatMapProcessor.MakeSoilMoisture(a, b, c, n0, lw, soc, pd, selected, tileDirectory, smGradientControl.GetGradient(), smMin, smMax);
                Invoke((MethodInvoker)delegate
                {
                    RefreshMap();
                    genSMBtn.Enabled = true;
                    if (highLowSMr.Item3 != -1)
                        n0Box.Text = highLowSMr.Item3.ToString();
                    if (smScaleCheck.Checked)
                    {
                        highSmBox.Text = $"{highLowSMr.Item1:0.00}";
                        lowSmBox.Text = $"{highLowSMr.Item2:0.00}";

                        smGradientControl.SetMinMax(highLowSMr.Item2, highLowSMr.Item1);
                    }


                });
            });
        }

        private void MapSelection(object sender, EventArgs e)
        {
            RefreshMap();
        }

        private void SmMouseMove(object sender, MouseEventArgs e)
        {
            Debug.WriteLine(((Control)sender).Size.Height + " : " + e.Location.Y);
            
        }

        private void smGradientControl_Load(object sender, EventArgs e)
        {
            smGradientControl.SetColors(Color.Black, Color.DarkBlue, Color.Blue, Color.LightBlue, Color.Black, Color.White);
            cphGradientControl.SetColors(Color.Red, Color.Orange, Color.Yellow, Color.LightYellow, Color.Black, Color.White);
            smGradientControl.DisplayGradient();
            cphGradientControl.DisplayGradient();

            smGradientControl.SetMinMax(0, .5);

        }

        private void Display_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RefreshMap()
        {
            int smIndex = dispCheckList.Items.IndexOf("Soil Moisture Map");
            int cphIndex = dispCheckList.Items.IndexOf("Counts Per Hour Map");
            int pathIndex = dispCheckList.Items.IndexOf("Path");
            int pointsIndex = dispCheckList.Items.IndexOf("Points");
            int selIndex = dispCheckList.Items.IndexOf("Selected Record");

            mapContainer.RefreshImageLayer(tileDirectory,
                smOpacitySlide.Value, cphOpacitySlide.Value);
            mapContainer.RefreshPositions(smIndex, cphIndex, pathIndex, 
                dispCheckList.GetItemChecked(smIndex), 
                dispCheckList.GetItemChecked(cphIndex), 
                dispCheckList.GetItemChecked(pathIndex), 
                dispCheckList.GetItemChecked(pointsIndex), 
                dispCheckList.GetItemChecked(selIndex));
        }

        private void refMapBtn_Click(object sender, EventArgs e)
        {
            RefreshMap();
        }

        private void upBtn_Click(object sender, EventArgs e)
        {
            int index = dispCheckList.SelectedIndex;
            if (index == -1) return;
            if (index == 0) return;

            string val = dispCheckList.Items[index].ToString();
            if (val.Equals("Points") || val.Equals("Selected Record")) return;
            bool isChecked = dispCheckList.GetItemChecked(index);
            dispCheckList.Items.RemoveAt(index);
            dispCheckList.Items.Insert(index-1,val);
            dispCheckList.SelectedIndex = index - 1;
            dispCheckList.SetItemChecked(index - 1, isChecked);
            RefreshMap();
        }

        private void downBtn_Click(object sender, EventArgs e)
        {
            int index = dispCheckList.SelectedIndex;
            if (index == -1) return;
            if (index == 2) return;

            string val = dispCheckList.Items[index].ToString();
            if (val.Equals("Points") || val.Equals("Selected Record")) return;
            bool isChecked = dispCheckList.GetItemChecked(index);
            dispCheckList.Items.RemoveAt(index);
            dispCheckList.Items.Insert(index + 1, val);
            dispCheckList.SelectedIndex = index + 1;
            dispCheckList.SetItemChecked(index + 1, isChecked);
            RefreshMap();
        }

        private void smOpacitySlide_Scroll(object sender, EventArgs e)
        {
            smOpacityBox.Text = smOpacitySlide.Value.ToString();
            RefreshMap();
        }

        private void cphOpacitySlide_Scroll(object sender, EventArgs e)
        {
            cphOpacityBox.Text = cphOpacitySlide.Value.ToString();
            RefreshMap();
        }

        private void satCheck_CheckedChanged(object sender, EventArgs e)
        {
            mapContainer.ChangeMode(satCheck.Checked);
        }

        private void recBack_Click(object sender, EventArgs e)
        {
            if (mapContainer.AdjustIndex(-1))
            {
                recordSlider.Value -= 1;
            }

        }

        private void recForward_Click(object sender, EventArgs e)
        {
            if (mapContainer.AdjustIndex(1))
            {
                recordSlider.Value += 1;
            }
        }

        private void removeData(object sender, EventArgs e)
        {
            string jsonPath = Path.Combine(selected, Path.GetFileName(selected) + ".json");
            List<RV2Block> blocks = JsonConvert.DeserializeObject<List<RV2Block>>(File.ReadAllText(jsonPath));
            parser.RV2Blocks = blocks;
            int remRecord = mapContainer.GetCurrectSelectedRecord();
            if (remRecord == -1) return;
            string metadata = parser.GetMetadata();
            string json = parser.GetJson(remRecord);
            string[] original = File.ReadAllLines(Path.Combine(selected, Path.GetFileName(selected) + ".RV2"));
            BuildDirectory(json, metadata, original, Path.GetFileNameWithoutExtension(selected));
            genBtn_Click(null, null);
            parser.ClearData();
            mapContainer.AdjustIndex(0);
        }

        private void resetData_Click(object sender, EventArgs e)
        {
            foreach (string filePath in Directory.GetFiles(selected))
            {
                if (!filePath.ToLower().Contains(".rv")) continue;
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    parser.NewRV2Data(line);
                }
                if (parser.RV2Blocks.Count > 0)
                {
                    string metadata = parser.GetMetadata();
                    string json = parser.GetJson();

                    BuildDirectory(json, metadata, lines, Path.GetFileNameWithoutExtension(filePath));
                    UpdateMapPins();
                }

                parser.ClearData();
            }
            genBtn_Click(null, null);
            mapContainer.AdjustIndex(0);

        }

        private void dispCheckList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void selectedCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

            SelectedData(selectedCombo.SelectedItem.ToString());
        }

        private void UpdateText(object sender, EventArgs e)
        {
        }

        private void openDirectoryBtn_Click(object sender, EventArgs e)
        {
            string path = selectedCombo.Text;
            if (Directory.Exists(path))
            {
                Process.Start("explorer.exe", path);
            }
        }

        private void recordSlider_Scroll(object sender, EventArgs e)
        {
            mapContainer.SetIndex(recordSlider.Value);
        }
    }
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

}

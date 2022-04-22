using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRNS_BP
{
    /// <summary>
    /// Interaction logic for MapContainer.xaml
    /// </summary>
    public partial class MapContainer : UserControl
    {
        ToolTip tt = new ToolTip();
        MainForm mf;
        MapLayer cphMapLayer = new MapLayer();
        MapLayer soilMoistureMapLayer = new MapLayer();
        MapLayer pathMapLayer = new MapLayer();
        MapLayer dataPointMapLayer = new MapLayer();
        MapLayer selectedPinLayer = new MapLayer();

        public int selectedIndex = 0;
        List<Tuple<double[], string>> data = null;

        public MapContainer()
        {
            InitializeComponent();
            
        }

        public void ReferenceMain(MainForm mf)
        {
            this.mf = mf;
        }

        public void ReferenceData(List<Tuple<double[], string>> data)
        {
            this.data = data;
        }


        internal void AddPin(double lat, double lon, string path)
        {
            Pushpin pin = new Pushpin();
            pin.Location = new Location(lat, lon);
            pin.ToolTip = tt;
            pin.MouseEnter += (sender, e) =>
            {
                tt.Content = path;
            };
            pin.MouseUp += (sender, e) =>
            {
                MessageBoxResult res = MessageBox.Show($"Display data for this set?\n'{path}'", "Data Selected", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    mf.SelectedData(path);
                    selectedIndex = 0;
                }

            };
            mapControl.Children.Add(pin);
            pin.Tag = path;
        }

        

        private void MapLoaded(object sender, RoutedEventArgs e)
        {
            mapControl.Mode = new AerialMode();
            mapControl.Children.Add(selectedPinLayer);
        }

        internal void ChangeMode(bool isSat)
        {
            if (isSat)
                mapControl.Mode = new AerialMode();
            else
                mapControl.Mode = new RoadMode();
        }
        
        internal void AddTerrainTileOverlay(string uriFormat, MapLayer imageMapLayer)
        {
            MapTileLayer tileLayer = new MapTileLayer();

            // The source of the overlay.
            TileSource tileSource = new TileSource();
            
            tileSource.UriFormat = uriFormat;

            // Add the tile overlay to the map layer
            tileLayer.TileSource = tileSource;

            // Add the map layer to the map
            if (!imageMapLayer.Children.Contains(tileLayer))
            {
                imageMapLayer.Children.Add(tileLayer);
            }
            tileLayer.Opacity = .4;
        }

        internal void RefreshLineLayer(List<double[]> path)
        {
            try
            {
                mapControl.Children.Remove(pathMapLayer);
            }
            catch { }
            pathMapLayer = new MapLayer();
            MapPolyline polyLine = new MapPolyline();
            polyLine.Stroke = new SolidColorBrush(Colors.Black);
            polyLine.StrokeThickness = 3;
            polyLine.Locations = new LocationCollection();
            polyLine.Opacity = .5;

            foreach (double[] point in path)
            {
                polyLine.Locations.Add(new Location(point[0], point[1]));
            }
            pathMapLayer.Children.Add(polyLine);
        }

        /// <summary>
        /// Refresh both image and soil moisture overlay layers
        /// </summary>
        /// <param name="uriFormat">Path to tiles directory</param>
        internal void RefreshImageLayer(string uriFormat, int smOpacity, int cphOpacity)
        {
            // remove both layers to clear cached tiles
            if (mapControl.Children.Contains(cphMapLayer))
                mapControl.Children.Remove(cphMapLayer);
            if (mapControl.Children.Contains(soilMoistureMapLayer))
                mapControl.Children.Remove(soilMoistureMapLayer);
            if (mapControl.Children.Contains(dataPointMapLayer))
                mapControl.Children.Remove(dataPointMapLayer);

            // regen maplayers
            cphMapLayer = new MapLayer();
            cphMapLayer.Children.Clear();
            soilMoistureMapLayer = new MapLayer();
            soilMoistureMapLayer.Children.Clear();
            dataPointMapLayer = new MapLayer();
            dataPointMapLayer.Children.Clear();

            MapTileLayer countsTileLayer = new MapTileLayer();
            MapTileLayer moistureTileLayer = new MapTileLayer();
            MapTileLayer dataPointTileLayer = new MapTileLayer();

            // The source of the overlay.
            TileSource countstileSource = new TileSource();
            TileSource moistureTileSource = new TileSource();
            TileSource dataPointTileSource = new TileSource();
            countstileSource.UriFormat = System.IO.Path.Combine(uriFormat, "countTile_{quadkey}.png");
            moistureTileSource.UriFormat = System.IO.Path.Combine(uriFormat, "smTile_{quadkey}.png");
            dataPointTileSource.UriFormat = System.IO.Path.Combine(uriFormat, "dpTile_{quadkey}.png");
            

            // Add the tile overlays to the map layers
            countsTileLayer.TileSource = countstileSource;
            moistureTileLayer.TileSource = moistureTileSource;
            dataPointTileLayer.TileSource = dataPointTileSource;

            // Add the map layer to the map
            cphMapLayer.Children.Add(countsTileLayer);
            soilMoistureMapLayer.Children.Add(moistureTileLayer);
            dataPointMapLayer.Children.Add(dataPointTileLayer);

            //if (cphChecked)
            //    mapControl.Children.Add(imageMapLayer);
            //if (smChecked)
            //    mapControl.Children.Add(soilMoistureMapLayer);

            countsTileLayer.Opacity = (double)cphOpacity/100;
            soilMoistureMapLayer.Opacity = (double)smOpacity/100;
            dataPointTileLayer.Opacity = 1;
        }

        internal void RefreshPositions(int smIndex, int cphIndex, int pathIndex, bool smCheck, bool cphCheck, bool pathCheck, bool showPointsCheck, bool showSelected)
        {

            // clear all layers (clear cache)
            if (mapControl.Children.Contains(cphMapLayer))
                mapControl.Children.Remove(cphMapLayer);
            if (mapControl.Children.Contains(soilMoistureMapLayer))
                mapControl.Children.Remove(soilMoistureMapLayer);
            if (mapControl.Children.Contains(pathMapLayer))
                mapControl.Children.Remove(pathMapLayer);
            if (mapControl.Children.Contains(dataPointMapLayer))
                mapControl.Children.Remove(dataPointMapLayer);

            

            // Cant think of a better way to do this
            if (smIndex == 2)
            {
                if (cphIndex == 1)
                {
                    // path must be 0
                    if (smCheck)
                        mapControl.Children.Add(soilMoistureMapLayer);
                    if (cphCheck)
                        mapControl.Children.Add(cphMapLayer);
                    if (pathCheck)
                        mapControl.Children.Add(pathMapLayer);
                }
                else
                {
                    // cph must be 0
                    if (smCheck)
                        mapControl.Children.Add(soilMoistureMapLayer);
                    if (pathCheck)
                        mapControl.Children.Add(pathMapLayer);
                    if (cphCheck)
                        mapControl.Children.Add(cphMapLayer);
                }
            }

            else if (cphIndex == 2)
            {
                if (smIndex == 1)
                {
                    // path must be 0
                    if (cphCheck)
                        mapControl.Children.Add(cphMapLayer);
                    if (smCheck)
                        mapControl.Children.Add(soilMoistureMapLayer);
                    if (pathCheck)
                        mapControl.Children.Add(pathMapLayer);
                }
                else
                {
                    // sm must be 0
                    if (cphCheck)
                        mapControl.Children.Add(cphMapLayer);
                    if (pathCheck)
                        mapControl.Children.Add(pathMapLayer);
                    if (smCheck)
                        mapControl.Children.Add(soilMoistureMapLayer);
                }
            }

            else if (pathIndex == 2)
            {
                if (smIndex == 1)
                {
                    // cph must be 0
                    if (pathCheck)
                        mapControl.Children.Add(pathMapLayer);
                    if (smCheck)
                        mapControl.Children.Add(soilMoistureMapLayer);
                    if (cphCheck)
                        mapControl.Children.Add(cphMapLayer);
                }
                else
                {
                    // sm must be 0
                    if (pathCheck)
                        mapControl.Children.Add(pathMapLayer);
                    if (cphCheck)
                        mapControl.Children.Add(cphMapLayer);
                    if (smCheck)
                        mapControl.Children.Add(soilMoistureMapLayer);
                }
            }

            if (showPointsCheck) mapControl.Children.Add(dataPointMapLayer);
            if (showSelected) selectedPinLayer.Visibility = Visibility.Visible;
            else selectedPinLayer.Visibility = Visibility.Hidden;
        }

        internal bool AdjustIndex(int inc)
        {
            if (data == null) return false;
            if (selectedIndex + inc < 0 ||
                selectedIndex + inc > data.Count - 1) return false;
            selectedIndex += inc;
            // refresh selected index

            selectedPinLayer.Children.Clear();
            Pushpin newPin = new Pushpin();
            newPin.Location = new Location(data[selectedIndex].Item1[0], data[selectedIndex].Item1[1]);
            selectedPinLayer.Children.Add(newPin);
            mf.SetSelectedPinText(data[selectedIndex].Item2);
            return true;
        }

        internal int GetCurrectSelectedRecord()
        {
            if (data == null) return -1;
            //Record {curBlock.recordNumber};
            string txt = data[selectedIndex].Item2.Split(';')[0].Split(' ')[1];
            if (int.TryParse(txt, out int rn)) return rn;
            else return -1;
        }

        internal void SetIndex(int val)
        {
            selectedIndex = val;
            // refresh selected index

            selectedPinLayer.Children.Clear();
            Pushpin newPin = new Pushpin();
            newPin.Location = new Location(data[selectedIndex].Item1[0], data[selectedIndex].Item1[1]);
            selectedPinLayer.Children.Add(newPin);
            mf.SetSelectedPinText(data[selectedIndex].Item2);
        }
    }


}

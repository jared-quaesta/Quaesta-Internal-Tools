using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using System.Runtime.Serialization.Formatters.Binary;

namespace CRNS_BP
{
    class HeatMapProcessor
    {

        public HeatMapProcessor()
        {
            // generate gradient
        }

        public static Tuple<double, double, List<Tuple<double[], string>>> MakeTilesInverseDistance
            (List<RV2Block> blocksOG, string tileDirectory, string dataDirectory, double p, int roi, List<Color> gradient, int desiredRP)
        {
            double min = double.MaxValue;
            double max = double.MinValue;
            List<Tuple<double[], string>> data = new List<Tuple<double[], string>>();
           // Make VERY large array encompassing entire bounded rectangle. On the order of 5m/entry
           // set smoothing to entire array at a time
           // split up array to individual bitmaps
           // save individual bitmaps 

           // if bin directory does not exist, make one inside datadirectory
            string binDirectory = Path.Combine(dataDirectory, "Bin");
            if (!Directory.Exists(binDirectory)) Directory.CreateDirectory(binDirectory);

            // trim list iff desiredRP deems neccessary

            List<RV2Block> blocks = AdjustRP(blocksOG, desiredRP);

            // generate counts gradient
            LocationRect locRect = FindBoundingRectangle(blocks);
            Location northwest = locRect.Northwest;
            Location southeast = locRect.Southeast;
            int zoom = 15;
            TileSystem.LatLongToPixelXY(northwest.Latitude, northwest.Longitude, zoom, out int xNW, out int yNW);
            TileSystem.LatLongToPixelXY(southeast.Latitude, southeast.Longitude, zoom, out int xSE, out int ySE);
            

            int gridx = xSE - xNW;
            int gridy = ySE - yNW;


            int[] xyTL = new int[] {xNW, yNW};
            double[,,] grid = new double[1, 1, 3];
            try
            {
                grid = new double[gridx, gridy, 3];
            } catch
            {
                System.Windows.MessageBox.Show("MEMORY OVERLOAD! Plotting is available for this dsataset, but mapping is not yet supported.", "ERROR");
                return null;
            }

            // find range of values and gleam outliers if neccessary
            // gleam outliers
            List<int> ucounts = new List<int>();

            foreach (RV2Block block in blocks)
            {
                ucounts.Add(block.sumCounts);
            }
            
            ucounts.Sort();

            int median = ucounts[(int)(.5 * (ucounts.Count-1))];
            int Q1 = ucounts[(int)(.25 * (ucounts.Count - 1))];
            int Q3 = ucounts[(int)(.75 * (ucounts.Count - 1))];
            int IQR = Q3 - Q1;

            double minAcceptedCount = Q1 - (1.5 * IQR);
            double maxAcceptedCount = Q3 + (1.5 * IQR);

            List<Location> convexHull = FindConvexHull(blocks, maxAcceptedCount, minAcceptedCount, zoom);

            double range = maxAcceptedCount - minAcceptedCount;
            if (range <= 0) return null;

            List<AdjustedRV2Block> adjBlocks = new List<AdjustedRV2Block>();
            // First pass over blocks will adjust the average counts per grid location, 
            // reducing target effect 
            foreach (RV2Block curBlock in blocks)
            {
                // going to manually edit the data for mapping specifically
                // original blocks will not be affected

                if (curBlock.GetPosition() == null) continue;
                if (curBlock.sumCounts > maxAcceptedCount || curBlock.sumCounts < minAcceptedCount) continue;

                // set initial datapoints
                double[] blockLatLon = curBlock.GetPosition();

                string info = $"Record {curBlock.recordNumber}; Sum Counts: {curBlock.sumCounts}; Record Period {curBlock.recordPeriod}s";
                data.Add(new Tuple<double[], string>(blockLatLon, info));

                double blockLat = blockLatLon[0];
                double blockLon = blockLatLon[1];

                // surrounding points?
                double sumWeights = 0;
                double sumCountsXSumWeights = 0;
                foreach (RV2Block checkBlock in blocks)
                {
                    //if (checkBlock == curBlock) continue;
                    if (checkBlock.GetPosition() == null) continue;
                    if (checkBlock.sumCounts > maxAcceptedCount || checkBlock.sumCounts < minAcceptedCount) continue;

                    // set initial datapoints
                    double[] checkBlockLatLon = checkBlock.GetPosition();
                    if (checkBlockLatLon == null) continue;

                    double checkLat = checkBlockLatLon[0];
                    double checkLon = checkBlockLatLon[1];
                    double dist = DistanceTo(blockLat, blockLon, checkLat, checkLon);

                    // really only want to catch points which are very close together.
                    if (dist <= roi/3)
                    {
                        if (dist == 0)
                        {
                            dist = 0.5;
                        }
                        double weight = 1 / Math.Pow(dist, p);
                        double counts = checkBlock.sumCounts;

                        // comment out below to distance weight
                        weight = 1;

                        sumWeights += weight;
                        sumCountsXSumWeights += counts * weight;
                    }

                }
                // create new ds with adjusted counts
                int adjustedCPH = (int)(((double)curBlock.sumCounts) / curBlock.recordPeriod * 3600);

                if (sumWeights == 0) 
                    adjBlocks.Add(new AdjustedRV2Block(curBlock, adjustedCPH));
                else
                {
                    double adjCounts = (sumCountsXSumWeights / sumWeights) / curBlock.recordPeriod * 3600;

                    adjBlocks.Add(new AdjustedRV2Block(curBlock, adjCounts));
                }

            }

            foreach (AdjustedRV2Block curBlock in adjBlocks)
            {
                if (curBlock.sumCounts < min) min = curBlock.sumCounts;
                if (curBlock.sumCounts > max) max = curBlock.sumCounts;
                if (curBlock.GetPosition() == null) continue;
                //if (curBlock.sumCounts > maxAcceptedCount || curBlock.sumCounts < minAcceptedCount) continue;

                // set initial datapoints
                double[] blockLatLon = curBlock.GetPosition();
                if (blockLatLon == null) continue;

                double blockLat = blockLatLon[0];
                double blockLon = blockLatLon[1];

                TileSystem.LatLongToPixelXY(blockLat, blockLon, zoom, out int px, out int py);

                int[] blockXY = new int[] { px, py };
                blockXY[0] -= xyTL[0];
                blockXY[1] -= xyTL[1];

                if (blockXY[0] > gridx - 1 || blockXY[1] > gridy - 1 || blockXY[1] < 0 || blockXY[0] < 0) continue;
                AppendGrid(curBlock.sumCounts, 1, 1, grid, blockXY[0], blockXY[1]);


                int pixelsPerMeter = (int)TileSystem.GroundResolution(curBlock.GetPosition()[0], zoom);
                
                List<double[]> mask = MakeMask(pixelsPerMeter, roi, p);
                //Stopwatch sw = Stopwatch.StartNew();

                SmoothArea(grid, blockXY[0], blockXY[1], mask, gridx, gridy, curBlock.sumCounts);

                //SmoothImmediateRadius();

                //Debug.WriteLine(sw.ElapsedMilliseconds);

            }
            range = max - min;
            // make counts map tiles
            Dictionary<string, DirectBitmap> keysMaps = SplitToTiles(grid, zoom, range, min, xyTL, gradient, convexHull, tileDirectory);

            // serialize and store grid to bindirevctory/countsgrid.bin
            string binFile = Path.Combine(binDirectory, Path.GetFileName($"{xyTL[0]}-{xyTL[1]}-cgrid.bin"));
            // remove all files in bin folder
            foreach (FileInfo file in new DirectoryInfo(binDirectory).GetFiles())
            {
                file.Delete();
            }
            // make file
            File.Create(binFile).Close();
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, grid);
            using (var fs = new FileStream(binFile, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                ms.WriteTo(fs);
            }

            foreach (string key in keysMaps.Keys)
            {
                Uri uria = new Uri(Path.Combine(tileDirectory, $"countTile_{key}.png"));

                keysMaps[key].Bitmap.Save(uria.OriginalString, ImageFormat.Png);
                keysMaps[key].Dispose();

            }

            DirectBitmap map = new DirectBitmap(gridx, gridy);
            range = max - min;
            for (int y = 0; y < gridy-1; y++)
            {
                for (int x = 0; x < gridx-1; x++)
                {
                    // no counts
                    if (grid[x, y, 2] == 0)
                    {
                        map.SetPixel(x, y, Color.Transparent);
                        continue;
                    }
                    else if (grid[x, y, 0] == 1)
                    {
                        map.SetPixel(x, y, Color.Green);
                        continue;
                    }
                    double counts = grid[x, y, 1] / grid[x,y,2];
                    //Debug.WriteLine(grid[x, y, 2]);
                    //Debug.WriteLine(grid[x, y, 1]);
                    // normalize to range
                    counts = (int)(255 * ((double)(counts - min) / range));
                    Color col = gradient[(int)counts];
                    //Debug.WriteLine(counts);
                    map.SetPixel(x, y, col);
                }
            }
            Uri uri = new Uri(Path.Combine(tileDirectory, $"smTile_.png"));
            Bitmap retMap = new Bitmap(map.Bitmap);
            map.Bitmap.Save(uri.OriginalString, ImageFormat.Png);
            map.Dispose();

            double[,] cphGrid = new double[gridx, gridy];

            int xLen = grid.GetLength(1);
            for (int x = xLen; x > 0; x--)
            {
                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    int revX = xLen - x;
                    cphGrid[y, x-1] = grid[y, revX, 1] / grid[y, revX, 2];
                }
            }
            return new Tuple<double, double, List<Tuple<double[], string>>>(min, max, data);
        }

        private static List<RV2Block> AdjustRP(List<RV2Block> blocksOG, int desiredRP)
        {
            // compare desiredRP with actual, return new list of blocks with correct adjusted record period
            // ie
            // rp, counts
            // 15, 45
            // 15, 45
            // ------
            // 30, 90

            int originalRP = blocksOG[0].recordPeriod;
            int difference = desiredRP / originalRP;

            List<RV2Block> ret = new List<RV2Block>();

            RV2Block cur = new RV2Block(blocksOG[0]);
            int count = 1;
            double[] prevLoc = blocksOG[0].GetPosition();

            for (int i = 1; i < blocksOG.Count; i++)
            {
                if (count == difference)
                {
                    ret.Add(cur);
                    cur = new RV2Block(blocksOG[i]);
                    count = 0;
                } else
                {
                    cur.sumCounts += blocksOG[i].sumCounts;
                    cur.recordPeriod += blocksOG[i].recordPeriod;
                    cur.position = FindMidPoint(prevLoc, blocksOG[i].position);
                    prevLoc = blocksOG[i].position;
                }
                count++;
            }
            ret.Add(cur);

            return ret;

        }

        private static double[] FindMidPoint(double[] prevLoc, double[] position)
        {
            double lat1 = prevLoc[0];
            double lon1 = prevLoc[1];
            double lat2 = position[0];
            double lon2 = position[1];

            TileSystem.LatLongToPixelXY(lat1, lon1, 17, out int pixX1, out int pixY1);
            TileSystem.LatLongToPixelXY(lat2, lon2, 17, out int pixX2, out int pixY2);

            TileSystem.PixelXYToLatLong((pixX1 + pixX2) / 2, (pixY1 + pixY2) / 2, 17, out double retLat, out double retLon);
            return new double[] {retLat, retLon};
           
        }

        static bool PointInPolygon(List<Location> polyPoints, Location point)
        {

            if (polyPoints.Count < 3)
            {
                return false;
            }

            bool inside = false;
            Location p1, p2;

            if (polyPoints.Contains(point)) return true;

            //iterate each side of the polygon
            Location oldPoint = polyPoints[polyPoints.Count - 1];

            foreach (Location newPoint in polyPoints)
            {
                //order points so p1.lat <= p2.lat;
                if (newPoint.Latitude > oldPoint.Latitude)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                //test if the line is crossed and if so invert the inside flag.
                if ((newPoint.Latitude < point.Latitude) == (point.Latitude <= oldPoint.Latitude)
                    && (point.Longitude - p1.Longitude) * (p2.Latitude - p1.Latitude)
                     < (p2.Longitude - p1.Longitude) * (point.Latitude - p1.Latitude))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
        }
        private static List<Location> FindConvexHull(List<RV2Block> blocks, double maxAcceptedCount, double minAcceptedCount, int zoom)
        {
            List<Point> pointList = new List<Point>();
            List<Location> retPoly = new List<Location>();
            foreach (RV2Block curBlock in blocks)
            {
                if (curBlock.GetPosition() == null) continue;
                if (curBlock.sumCounts > maxAcceptedCount || curBlock.sumCounts < minAcceptedCount) continue;

                // set initial datapoints
                double[] blockLatLon = curBlock.GetPosition();
                if (blockLatLon == null) continue;

                double blockLat = blockLatLon[0];
                double blockLon = blockLatLon[1];

                TileSystem.LatLongToPixelXY(blockLat, blockLon, zoom, out int px, out int py);

                pointList.Add(new Point(px, py));
            }

            List<Point> cHull = ConvexHull.GetConvexHull(pointList);

            foreach (Point p in cHull)
            {
                TileSystem.PixelXYToLatLong(p.X, p.Y, zoom, out double lat, out double lon);
                retPoly.Add(new Location(lat, lon));
            }

            return retPoly;
        }

        

        private static double DistanceTo(double lat1, double lon1, double lat2, double lon2)
        {
            var d1 = lat1 * (Math.PI / 180.0);
            var num1 = lon1 * (Math.PI / 180.0);
            var d2 = lat2 * (Math.PI / 180.0);
            var num2 = lon2 * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        public static Tuple<double, double, double> MakeSoilMoisture(
            double a, 
            double b, 
            double c, 
            double n0, 
            double latticeWater, 
            double soc, 
            double pd, 
            string dataDirectory, 
            string tileDirectory, 
            List<Color> gradient,
            double smMin,
            double smMax)
        {
            // deserialize stored grid
            string binDirectory = Path.Combine(dataDirectory, "Bin");
            DirectoryInfo di = new DirectoryInfo(binDirectory);
            FileInfo[] files = di.GetFiles();
            if (files.Length == 0) return new Tuple<double, double, double>(0,0,-1);
            FileInfo gridFile = files[0];
            // pos parse
            string[] pos = gridFile.Name.Split('-');
            int[] xyOff = new int[]
            {
                int.Parse(pos[0]),
                int.Parse(pos[1])
            };
            BinaryFormatter bf = new BinaryFormatter();
            double[,,] cphGrid;
            using (var fs = new FileStream(gridFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                cphGrid = (double[,,])bf.Deserialize(fs);
            }

            // if n0 set to auto, find n0 (dryest)
            if (n0 == -1)
            {
                List<double> allCountsPerHour = new List<double>();
                for (int y = 0; y < cphGrid.GetLength(1); y++)
                {
                    for (int x = 0; x < cphGrid.GetLength(0); x++)
                    {
                        if (cphGrid[x, y, 0] != 1) continue;

                        double countsPerHour = cphGrid[x, y, 1] / cphGrid[x, y, 2];
                        allCountsPerHour.Add(countsPerHour);
                    }
                }

                // NOT FINAL CALC, JUST FOR DEV. AUTO LIKELY NOT AN OPTION
                // TODO
                n0 = allCountsPerHour.Max() * 1.15;
            }

            // make smGrid in place
            double minSm = double.MaxValue;
            double maxSm = double.MinValue;

            for (int y = 0; y < cphGrid.GetLength(1); y++)
            {
                for (int x = 0; x < cphGrid.GetLength(0); x++)
                {
                    if (cphGrid[x, y, 0]  == 0) continue;

                    double nCor = cphGrid[x, y, 1] / cphGrid[x, y, 2];

                    double sm = (a / ((nCor / n0) - b) - c - latticeWater - soc) * pd;

                    if (sm < minSm) minSm = sm;
                    if (sm > maxSm) maxSm = sm;

                    cphGrid[x, y, 1] = sm;
                    cphGrid[x, y, 2] = 1;
                }
            }

            // make gradient
            double range = smMax - smMin;
            Dictionary<string, DirectBitmap> keysMaps = SplitToTiles(cphGrid, 15, range, smMin, xyOff, gradient, null, tileDirectory);
            foreach (string key in keysMaps.Keys)
            {
                Uri uria = new Uri(Path.Combine(tileDirectory, $"smTile_{key}.png"));

                keysMaps[key].Bitmap.Save(uria.OriginalString, ImageFormat.Png);

            }

            return new Tuple<double, double, double>(maxSm, minSm, n0);
        }


        private static Dictionary<string, DirectBitmap> SplitToTiles(
            double[,,] grid, 
            int gridDetail,
            double range, 
            double minCounts, 
            int[] offsetXY,
            List<Color> gradient,
            List<Location> cHull,
            string tileDirectory)
        {
            // Take big grid and split into heatmap tiles
            // TileSystem.
            Dictionary<string, DirectBitmap> quadkeyToCountsBitmap = new Dictionary<string, DirectBitmap>();
            Dictionary<string, DirectBitmap> quadkeyToDPBitmap = new Dictionary<string, DirectBitmap>();

            // make point mask: adjust r to fit. Variable?
            int r = 1;
            List<double[]> pointMask = MakeMask(-1, -1, 1, r);

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    // foreach zoom level, determine where this pixel would stand, if at all.
                    // lower levels of detail may not include pixel at all
                    int pixX = x + offsetXY[0];
                    int pixY = y + offsetXY[1];

                    // pixX and pixY are actual pixels on map

                    TileSystem.PixelXYToLatLong(pixX, pixY, gridDetail, out double pointLat, out double pointLon);
                    // get coordinates of each point at GRID resolution
                    if (cHull != null && !PointInPolygon(cHull, new Location(pointLat, pointLon)))
                    {
                        grid[x, y, 0] = 0;
                        continue;
                    }

                    // zoom level will rarely be neccessary past 10 in this case.
                    for (int tileZoom = gridDetail; tileZoom > 10; tileZoom--)
                    {
                        // get pixel value of gridxy on MAP at req zoom level  
                        TileSystem.LatLongToPixelXY(pointLat, pointLon, tileZoom, out int adjPixX, out int adjPixY);

                        // get top corner INDEX of containing tile given gridXY MAP index 
                        TileSystem.PixelXYToTileXY(adjPixX, adjPixY, out int tileX, out int tileY);

                        // at this point, all that needs to be done is:
                        //  1. get difference between tilecorner index and adjusted pixel xy 
                        //  2. determine color of pixel
                        //  3. add to bitmap at correct index

                        // generate quadkey for tile 
                        string key = TileSystem.TileXYToQuadKey(tileX, tileY, tileZoom);

                        //TileSystem.PixelXYToLatLong(tileX, tileY, tileZoom, out double tileCornerLat, out double tileCornerLong);


                        int mapX = adjPixX - tileX * 256;
                        int mapY = adjPixY - tileY * 256;

                        // get location within tile
                        //int mapX = adjPixX - tileCornerX;
                        //int mapY = adjPixY - tileCornerY;

                        Color col = Color.Transparent;
                        // get color of pixel
                        if (grid[x, y, 0] != 0)
                        {
                            double countsPerHour = grid[x, y, 1] / grid[x, y, 2];

                            //Debug.WriteLine(grid[x, y, 1]);
                            //Debug.WriteLine(grid[x, y, 2]);
                            ///Debug.WriteLine(grid[x, y, 1]);
                            //.WriteLine(grid[x, y, 2]);
                            // normalize to range
                            int normalized = (int)((gradient.Count -1) * ((double)(countsPerHour - minCounts) / range));
                            if (gradient.Count > normalized && normalized > 0)
                                col = gradient[normalized];
                        }

                        if (quadkeyToCountsBitmap.ContainsKey(key))
                        {
                            quadkeyToCountsBitmap[key].SetPixel(mapX, mapY, col);

                        }
                        else
                        {
                            DirectBitmap newBitmap = new DirectBitmap(256, 256);
                            newBitmap.SetPixel(mapX, mapY, col);
                            quadkeyToCountsBitmap.Add(key, newBitmap);

                        }

                        if (grid[x, y, 0] == 1 && tileZoom > 14)
                        {
                            if (quadkeyToDPBitmap.ContainsKey(key))
                            {
                                
                                    foreach (double[] maskPoint in pointMask)
                                    {
                                        int offX = (int)maskPoint[0];
                                        int offY = (int)maskPoint[1];

                                        int dpPixX = mapX + offX;
                                        int dpPixY = mapY + offY;

                                        if (dpPixX < 0 || dpPixY < 0 ||
                                            dpPixX > 255 || dpPixY > 255)
                                            continue;

                                        quadkeyToDPBitmap[key].SetPixel(dpPixX, dpPixY, Color.Black);

                                    }
                                
                            }
                            else
                            {
                                DirectBitmap newBitmap = new DirectBitmap(256, 256);
                                quadkeyToDPBitmap.Add(key, newBitmap);
                                foreach (double[] maskPoint in pointMask)
                                    {
                                        int offX = (int)maskPoint[0];
                                        int offY = (int)maskPoint[1];

                                        int dpPixX = mapX + offX;
                                        int dpPixY = mapY + offY;

                                        if (dpPixX < 0 || dpPixY < 0 ||
                                            dpPixX > 255 || dpPixY > 255)
                                            continue;

                                        quadkeyToDPBitmap[key].SetPixel(dpPixX, dpPixY, Color.Black);

                                    }
                                }
                            
                        }

                    }

                }
            }

            RefDPBitMap(quadkeyToDPBitmap, tileDirectory);

            return quadkeyToCountsBitmap;
        }

        private static void RefDPBitMap(Dictionary<string, DirectBitmap> quadkeyToDPBitmap, string tileDirectory)
        {
            foreach (string key in quadkeyToDPBitmap.Keys)
            {
                Uri uria = new Uri(Path.Combine(tileDirectory, Path.GetFileName($"dpTile_{key}.png")));

                quadkeyToDPBitmap[key].Bitmap.Save(uria.OriginalString, ImageFormat.Png);
                quadkeyToDPBitmap[key].Dispose();
            }
        }

        private static void SmoothArea(double[,,] grid, int px, int py, List<double[]> mask, int boundx, int boundy, double blockCounts)
        {
            foreach (double[] maskPoint in mask)
            {
                int maskX = (int)maskPoint[0] + px;
                int maskY = (int)maskPoint[1] + py;
                double weight = maskPoint[2];
                // ignore points outside of grid
                if (maskX > boundx-1 || maskX < 0) continue;
                if (maskY > boundy-1 || maskY < 0) continue;
                // ignore actual data points
                if (grid[maskX, maskY, 0] == 1) continue;

                AppendGrid(blockCounts, weight, 2, grid, maskX, maskY);

            }

        }



        private static void AppendGrid(double blockCounts, double weight, int type, double[,,] grid, int xI, int yI)
        {
            
            grid[xI, yI, 0] = type;

            if (type == 1)
            {
                grid[xI, yI, 1] = blockCounts;
                grid[xI, yI, 2] = 1;
            } else
            {
                grid[xI, yI, 1] += blockCounts * weight;
                grid[xI, yI, 2] += weight;
            }

        }

    
        internal double MetersPerPx(double lat, int zoom)
        {
            return 156543.03392 * Math.Cos(lat * Math.PI / 180) / Math.Pow(2, zoom);
        }

        private static LocationRect FindBoundingRectangle(List<RV2Block> blocks)
        {
            LocationRect ret = new LocationRect();

            double uppermostlat = double.MinValue;
            double leftmostlon = double.MaxValue;
            double rightmostlon = double.MinValue;
            double southmostlat = double.MaxValue;

            foreach (RV2Block block in blocks)
            {
                if (block.GetPosition() == null) continue;
                double[] blockLatLon = block.GetPosition();
                if (blockLatLon == null) continue;

                double blockLat = blockLatLon[0];
                double blockLon = blockLatLon[1];

                if (blockLat > uppermostlat) uppermostlat = blockLat;
                if (blockLat < southmostlat) southmostlat = blockLat;
                if (blockLon < leftmostlon) leftmostlon = blockLon;
                if (blockLon > rightmostlon) rightmostlon = blockLon;

            }

            ret.North = uppermostlat;
            ret.West = leftmostlon;
            ret.East = rightmostlon;
            ret.South = southmostlat;
            return ret;

        }

        private static List<double[]> MakeMask(int pixelsPerMeter, int desiredRadius, double p, int overrideR = -1)
        {
            // irrelevant init value for r.
            int r = 5;
            if (overrideR != -1)
                r = overrideR;
            else
                r = desiredRadius / pixelsPerMeter;

            List<double[]> mask = new List<double[]>();
            for (int m = -1 * r; m < r + 1; m++)
            {
                for (int n = -1 * r; n < r + 1; n++)
                {
                    if (Math.Pow(m, 2) + Math.Pow(n, 2) <= Math.Pow(r, 2))
                    {
                        double distance = Math.Sqrt(Math.Pow(m, 2) + Math.Pow(n, 2));
                        double weight = 1 / Math.Pow(distance , p);
                        if (double.IsInfinity(weight))
                        {
                            weight = .5;
                        }
                        mask.Add(new double[] { m, n, weight });
                    }
                }
            }
            return mask;
        }

        private static double DegToRad(double deg)
        {
            return (Math.PI / 180) * deg;
        }

        private static double RadToDeg(double rad)
        {
            return (180 / Math.PI) * rad;
        }

        public static IEnumerable<Color> MakeGradient(Color start, Color end, int steps)
        {
            int stepA = ((end.A - start.A) / (steps - 1));
            int stepR = ((end.R - start.R) / (steps - 1));
            int stepG = ((end.G - start.G) / (steps - 1));
            int stepB = ((end.B - start.B) / (steps - 1));

            for (int i = 0; i < steps; i++)
            {
                yield return Color.FromArgb(start.A + (stepA * i),
                                            start.R + (stepR * i),
                                            start.G + (stepG * i),
                                            start.B + (stepB * i));
            }
        }

    }
    class ConvexHull
    {
        public static double cross(Point O, Point A, Point B)
        {
            return (A.X - O.X) * (B.Y - O.Y) - (A.Y - O.Y) * (B.X - O.X);
        }

        public static List<Point> GetConvexHull(List<Point> points)
        {
            if (points == null)
                return null;

            if (points.Count() <= 1)
                return points;

            int n = points.Count(), k = 0;
            List<Point> H = new List<Point>(new Point[2 * n]);

            points.Sort((a, b) =>
                 a.X == b.X ? a.Y.CompareTo(b.Y) : a.X.CompareTo(b.X));

            // Build lower hull
            for (int i = 0; i < n; ++i)
            {
                while (k >= 2 && cross(H[k - 2], H[k - 1], points[i]) <= 0)
                    k--;
                H[k++] = points[i];
            }

            // Build upper hull
            for (int i = n - 2, t = k + 1; i >= 0; i--)
            {
                while (k >= t && cross(H[k - 2], H[k - 1], points[i]) <= 0)
                    k--;
                H[k++] = points[i];
            }

            return H.Take(k - 1).ToList();
        }
    }
}

////gradient 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRNS_BP
{
    public partial class GradientControl : UserControl
    {
        Dictionary<string, Color> btnColorDictionary = new Dictionary<string, Color>();
        Color top = Color.Black;
        Color bottom = Color.White;
        List<Color> gradient;

        // to keep buttons in right place when resizing
        double bt1RelativePos = 0.15;
        double bt2RelativePos = 0.20;
        double bt3RelativePos = 0.35;
        double bt4RelativePos = 0.40;

        double min = 0;
        double max = 100;


        public GradientControl()
        {
            btnColorDictionary.Add("bt1", Color.Purple);
            btnColorDictionary.Add("bt2", Color.Yellow);
            btnColorDictionary.Add("bt3", Color.Green);
            btnColorDictionary.Add("bt4", Color.Red);


            InitializeComponent();
        }

        internal void SetMinMax(double min, double max)
        {
            this.min = min;
            this.max = max;
            lb1.Location = new Point(lb1.Location.X, (int)(bt1RelativePos * gradientPicture.Size.Height));
            lb1.Text = $"{(max - min) * Math.Abs(bt1RelativePos - 1) + min:0.00}";

            lb2.Location = new Point(lb2.Location.X, (int)(bt2RelativePos * gradientPicture.Size.Height));
            lb2.Text = $"{(max - min) * Math.Abs(bt2RelativePos - 1) + min:0.00}";

            lb3.Location = new Point(lb3.Location.X, (int)(bt3RelativePos * gradientPicture.Size.Height));
            lb3.Text = $"{(max - min) * Math.Abs(bt3RelativePos - 1) + min:0.00}";

            lb4.Location = new Point(lb4.Location.X, (int)(bt4RelativePos * gradientPicture.Size.Height));
            lb4.Text = $"{(max - min) * Math.Abs(bt4RelativePos - 1) + min:0.00}";
        }

        internal void SetColors(Color c1, Color c2, Color c3, Color c4, Color top, Color bottom)
        {
            btnColorDictionary["bt1"] = c1;
            btnColorDictionary["bt2"] = c2;
            btnColorDictionary["bt3"] = c3;
            btnColorDictionary["bt4"] = c4;
            this.top = top;
            this.bottom = bottom;
        }

        private void MoveBtn(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            if (e.Button == MouseButtons.Left)
            {
                int mY = gradientPicture.PointToClient(Cursor.Position).Y;

                if (btn.Name.Equals("bt1"))
                {
                    if (mY <= 0) return;
                    if (mY >= bt2.Location.Y) return;

                    lb1.Location = new Point(lb1.Location.X, (int)(bt1RelativePos * gradientPicture.Size.Height));
                    lb1.Text = $"{(max - min) * Math.Abs(bt1RelativePos - 1) + min:0.00}";

                }
                if (btn.Name.Equals("bt2"))
                {
                    if (mY <= bt1.Location.Y) return;
                    if (mY >= bt3.Location.Y) return;

                    lb2.Location = new Point(lb2.Location.X, (int)(bt2RelativePos * gradientPicture.Size.Height));
                    lb2.Text = $"{(max - min) * Math.Abs(bt2RelativePos - 1) + min:0.00}";

                }
                if (btn.Name.Equals("bt3"))
                {
                    if (mY <= bt2.Location.Y) return;
                    if (mY >= bt4.Location.Y) return;

                    lb3.Location = new Point(lb3.Location.X, (int)(bt3RelativePos * gradientPicture.Size.Height));
                    lb3.Text = $"{(max - min) * Math.Abs(bt3RelativePos - 1) + min:0.00}";
                }
                if (btn.Name.Equals("bt4"))
                {
                    if (mY <= bt3.Location.Y) return;
                    if (mY >= gradientPicture.Height - bt4.Height) return;

                    lb4.Location = new Point(lb4.Location.X, (int)(bt4RelativePos * gradientPicture.Size.Height));
                    lb4.Text = $"{(max - min) * Math.Abs(bt4RelativePos - 1) + min:0.00}";
                }


                btn.Location = new Point(btn.Location.X, mY);
                DisplayGradient();

            }
        }

        private void GradientControl_Load(object sender, EventArgs e)
        {
            DisplayGradient();
        }

        internal List<Color> GetGradient()
        {
            return gradient.Reverse<Color>().ToList();
        }

        internal void DisplayGradient()
        {
            int posBtn1 = bt1.Location.Y;
            int posBtn2 = bt2.Location.Y;
            int posBtn3 = bt3.Location.Y;
            int posBtn4 = bt4.Location.Y;


            bt1RelativePos = (double)bt1.Location.Y / gradientPicture.Height;
            bt2RelativePos = (double)bt2.Location.Y / gradientPicture.Height;
            bt3RelativePos = (double)bt3.Location.Y / gradientPicture.Height;
            bt4RelativePos = (double)bt4.Location.Y / gradientPicture.Height;


            int grBoxHeight = gradientPicture.Height;
            int grBoxWidth = gradientPicture.Width;
            DirectBitmap map = new DirectBitmap(grBoxWidth, grBoxHeight);
            gradient = CreateGradient(top, btnColorDictionary["bt1"], posBtn1).ToList();
            foreach (Color col in CreateGradient(btnColorDictionary["bt1"], btnColorDictionary["bt2"], posBtn2 - posBtn1)) gradient.Add(col);
            foreach (Color col in CreateGradient(btnColorDictionary["bt2"], btnColorDictionary["bt3"], posBtn3 - posBtn2)) gradient.Add(col);
            foreach (Color col in CreateGradient(btnColorDictionary["bt3"], btnColorDictionary["bt4"], posBtn4 - posBtn3)) gradient.Add(col);
            foreach (Color col in CreateGradient(btnColorDictionary["bt4"], bottom, grBoxHeight - posBtn4)) gradient.Add(col);

            try
            {
                for (int y = 0; y < posBtn1; y++)
                {
                    for (int x = 0; x < grBoxWidth; x++)
                    {
                        map.SetPixel(x, y, gradient[y]);
                    }
                }

                for (int y = posBtn1; y < posBtn2; y++)
                {
                    for (int x = 0; x < grBoxWidth; x++)
                    {
                        map.SetPixel(x, y, gradient[y]);
                    }
                }

                for (int y = posBtn2; y < posBtn3; y++)
                {
                    for (int x = 0; x < grBoxWidth; x++)
                    {
                        map.SetPixel(x, y, gradient[y]);
                    }
                }
                for (int y = posBtn3; y < posBtn4; y++)
                {
                    for (int x = 0; x < grBoxWidth; x++)
                    {
                        map.SetPixel(x, y, gradient[y]);
                    }
                }

                for (int y = posBtn4; y < grBoxHeight; y++)
                {
                    for (int x = 0; x < grBoxWidth; x++)
                    {
                        map.SetPixel(x, y, gradient[y]);
                    }
                }
            }
            catch
            {
                Debug.WriteLine("Something went wrong...");
            }


            gradientPicture.Image = new Bitmap(map.Bitmap);
            map.Dispose();
            Refresh();
        }

        public static IEnumerable<Color> CreateGradient(Color start, Color end, int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                double percent = (double)i / steps;
                double resultRed = start.R + percent * (end.R - start.R);
                double resultGreen = start.G + percent * (end.G - start.G);
                double resultBlue = start.B + percent * (end.B - start.B);


                yield return Color.FromArgb((int)resultRed, (int)resultGreen, (int)resultBlue);
            }
        }

        private void SelectColor(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                ColorDialog cD = new ColorDialog();
                DialogResult res = cD.ShowDialog();
                if (res != DialogResult.Cancel && res != DialogResult.Abort)
                {
                    Button btn = (Button)sender;
                    btnColorDictionary[btn.Name] = cD.Color;

                    DisplayGradient();
                }
            }
        }

        private void ResizeFixButtonPositions(object sender, EventArgs e)
        {
            bt1.Location = new Point(bt1.Location.X, (int)(bt1RelativePos * gradientPicture.Size.Height));
            lb1.Location = new Point(lb1.Location.X, (int)(bt1RelativePos * gradientPicture.Size.Height));
            lb1.Text = $"{(max - min) * Math.Abs(bt1RelativePos - 1) + min:0.00}";

            bt2.Location = new Point(bt2.Location.X, (int)(bt2RelativePos * gradientPicture.Size.Height));
            lb2.Location = new Point(lb2.Location.X, (int)(bt2RelativePos * gradientPicture.Size.Height));
            lb2.Text = $"{(max - min) * Math.Abs(bt2RelativePos - 1) + min:0.00}";

            bt3.Location = new Point(bt3.Location.X, (int)(bt3RelativePos * gradientPicture.Size.Height));
            lb3.Location = new Point(lb3.Location.X, (int)(bt3RelativePos * gradientPicture.Size.Height));
            lb3.Text = $"{(max - min) * Math.Abs(bt3RelativePos - 1) + min:0.00}";

            bt4.Location = new Point(bt4.Location.X, (int)(bt4RelativePos * gradientPicture.Size.Height));
            lb4.Location = new Point(lb4.Location.X, (int)(bt4RelativePos * gradientPicture.Size.Height));
            lb4.Text = $"{(max - min) * Math.Abs(bt4RelativePos - 1) + min:0.00}";

            DisplayGradient();
        }

        internal void SetMinMaxSMSpeific(double min, double max)
        {
            //throw new NotImplementedException();
        }
    }
}

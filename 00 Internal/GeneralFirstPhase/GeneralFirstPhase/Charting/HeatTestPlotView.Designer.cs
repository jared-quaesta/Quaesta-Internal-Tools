
namespace GeneralFirstPhase.Charting
{
    partial class HeatTestPlotView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainPlot = new OxyPlot.WindowsForms.PlotView();
            this.cs215Plot = new OxyPlot.WindowsForms.PlotView();
            this.SuspendLayout();
            // 
            // mainPlot
            // 
            this.mainPlot.Location = new System.Drawing.Point(12, 12);
            this.mainPlot.Name = "mainPlot";
            this.mainPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.mainPlot.Size = new System.Drawing.Size(1162, 442);
            this.mainPlot.TabIndex = 0;
            this.mainPlot.Text = "plotView1";
            this.mainPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.mainPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.mainPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // cs215Plot
            // 
            this.cs215Plot.Location = new System.Drawing.Point(12, 461);
            this.cs215Plot.Name = "cs215Plot";
            this.cs215Plot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.cs215Plot.Size = new System.Drawing.Size(1162, 156);
            this.cs215Plot.TabIndex = 1;
            this.cs215Plot.Text = "plotView1";
            this.cs215Plot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.cs215Plot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.cs215Plot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // HeatTestPlotView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 629);
            this.Controls.Add(this.cs215Plot);
            this.Controls.Add(this.mainPlot);
            this.Name = "HeatTestPlotView";
            this.Text = "HeatTestPlotView";
            this.Load += new System.EventHandler(this.HeatTestPlotView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView mainPlot;
        private OxyPlot.WindowsForms.PlotView cs215Plot;
    }
}
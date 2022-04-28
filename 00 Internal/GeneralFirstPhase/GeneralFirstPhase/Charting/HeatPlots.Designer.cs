
namespace GeneralFirstPhase.Charting
{
    partial class HeatPlots
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.varySelectBox = new System.Windows.Forms.ComboBox();
            this.hgmPlot = new OxyPlot.WindowsForms.PlotView();
            this.variablePlot = new OxyPlot.WindowsForms.PlotView();
            this.cs215Plot = new OxyPlot.WindowsForms.PlotView();
            this.SuspendLayout();
            // 
            // varySelectBox
            // 
            this.varySelectBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.varySelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.varySelectBox.FormattingEnabled = true;
            this.varySelectBox.Items.AddRange(new object[] {
            "Voltage",
            "Temperature/Humidity"});
            this.varySelectBox.Location = new System.Drawing.Point(623, 218);
            this.varySelectBox.Name = "varySelectBox";
            this.varySelectBox.Size = new System.Drawing.Size(153, 21);
            this.varySelectBox.TabIndex = 35;
            this.varySelectBox.SelectedIndexChanged += new System.EventHandler(this.varySelectBox_SelectedIndexChanged);
            // 
            // hgmPlot
            // 
            this.hgmPlot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hgmPlot.Location = new System.Drawing.Point(3, 3);
            this.hgmPlot.Name = "hgmPlot";
            this.hgmPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.hgmPlot.Size = new System.Drawing.Size(773, 209);
            this.hgmPlot.TabIndex = 34;
            this.hgmPlot.Text = "plotView3";
            this.hgmPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.hgmPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.hgmPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // variablePlot
            // 
            this.variablePlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.variablePlot.Location = new System.Drawing.Point(3, 218);
            this.variablePlot.Name = "variablePlot";
            this.variablePlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.variablePlot.Size = new System.Drawing.Size(773, 261);
            this.variablePlot.TabIndex = 33;
            this.variablePlot.Text = "plotView2";
            this.variablePlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.variablePlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.variablePlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // cs215Plot
            // 
            this.cs215Plot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cs215Plot.Location = new System.Drawing.Point(3, 485);
            this.cs215Plot.Name = "cs215Plot";
            this.cs215Plot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.cs215Plot.Size = new System.Drawing.Size(773, 104);
            this.cs215Plot.TabIndex = 32;
            this.cs215Plot.Text = "plotView1";
            this.cs215Plot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.cs215Plot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.cs215Plot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // HeatPlots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.varySelectBox);
            this.Controls.Add(this.hgmPlot);
            this.Controls.Add(this.variablePlot);
            this.Controls.Add(this.cs215Plot);
            this.Name = "HeatPlots";
            this.Size = new System.Drawing.Size(779, 592);
            this.Load += new System.EventHandler(this.HeatPlots_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox varySelectBox;
        private OxyPlot.WindowsForms.PlotView hgmPlot;
        private OxyPlot.WindowsForms.PlotView variablePlot;
        private OxyPlot.WindowsForms.PlotView cs215Plot;
    }
}

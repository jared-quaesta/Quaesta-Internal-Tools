
namespace QIXLPTesting
{
    partial class HGMPlotForm
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
            this.plotView = new OxyPlot.WindowsForms.PlotView();
            this.cumuCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // plotView
            // 
            this.plotView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(47)))), ((int)(((byte)(59)))));
            this.plotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotView.Location = new System.Drawing.Point(0, 0);
            this.plotView.Name = "plotView";
            this.plotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView.Size = new System.Drawing.Size(800, 450);
            this.plotView.TabIndex = 1;
            this.plotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // cumuCheck
            // 
            this.cumuCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cumuCheck.AutoSize = true;
            this.cumuCheck.Location = new System.Drawing.Point(666, 0);
            this.cumuCheck.Name = "cumuCheck";
            this.cumuCheck.Size = new System.Drawing.Size(134, 17);
            this.cumuCheck.TabIndex = 2;
            this.cumuCheck.Text = "Show Cumulative Data";
            this.cumuCheck.UseVisualStyleBackColor = true;
            this.cumuCheck.CheckedChanged += new System.EventHandler(this.cumuCheck_CheckedChanged);
            // 
            // HGMPlotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cumuCheck);
            this.Controls.Add(this.plotView);
            this.Name = "HGMPlotForm";
            this.Text = "HGMPlotForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CancelClose);
            this.Load += new System.EventHandler(this.HGMPlotForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView plotView;
        private System.Windows.Forms.CheckBox cumuCheck;
    }
}
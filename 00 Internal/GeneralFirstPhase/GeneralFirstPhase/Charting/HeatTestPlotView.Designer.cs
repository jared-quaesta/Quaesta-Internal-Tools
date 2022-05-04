
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
            this.dateBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serialLbl = new System.Windows.Forms.Label();
            this.heatPlots = new GeneralFirstPhase.Charting.HeatPlots();
            this.SuspendLayout();
            // 
            // dateBox
            // 
            this.dateBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateBox.FormattingEnabled = true;
            this.dateBox.Location = new System.Drawing.Point(887, 12);
            this.dateBox.Name = "dateBox";
            this.dateBox.Size = new System.Drawing.Size(287, 21);
            this.dateBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(801, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Detected Tests";
            // 
            // serialLbl
            // 
            this.serialLbl.AutoSize = true;
            this.serialLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.serialLbl.Location = new System.Drawing.Point(12, 15);
            this.serialLbl.Name = "serialLbl";
            this.serialLbl.Size = new System.Drawing.Size(51, 18);
            this.serialLbl.TabIndex = 5;
            this.serialLbl.Text = "Serial";
            // 
            // heatPlots
            // 
            this.heatPlots.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.heatPlots.Location = new System.Drawing.Point(12, 39);
            this.heatPlots.Name = "heatPlots";
            this.heatPlots.Size = new System.Drawing.Size(1162, 603);
            this.heatPlots.TabIndex = 4;
            // 
            // HeatTestPlotView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 654);
            this.Controls.Add(this.serialLbl);
            this.Controls.Add(this.heatPlots);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateBox);
            this.Name = "HeatTestPlotView";
            this.Text = "HeatTestPlotView";
            this.Load += new System.EventHandler(this.HeatTestPlotView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox dateBox;
        private System.Windows.Forms.Label label1;
        private HeatPlots heatPlots;
        private System.Windows.Forms.Label serialLbl;
    }
}
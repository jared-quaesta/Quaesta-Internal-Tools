
namespace QIY_Interface__IAEA_
{
    partial class OnlineHGMForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OnlineHGMForm));
            this.hgmPlot = new OxyPlot.WindowsForms.PlotView();
            this.label1 = new System.Windows.Forms.Label();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.elapsedBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.totalCntsBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.queryBtn = new System.Windows.Forms.Button();
            this.zeroBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.expBtn = new System.Windows.Forms.Button();
            this.refCB = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.queryCheck = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // hgmPlot
            // 
            this.hgmPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hgmPlot.BackColor = System.Drawing.SystemColors.Control;
            this.hgmPlot.Location = new System.Drawing.Point(10, 55);
            this.hgmPlot.Name = "hgmPlot";
            this.hgmPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.hgmPlot.Size = new System.Drawing.Size(665, 286);
            this.hgmPlot.TabIndex = 0;
            this.hgmPlot.Text = "plotView1";
            this.hgmPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.hgmPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.hgmPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP:";
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(49, 5);
            this.ipBox.Name = "ipBox";
            this.ipBox.ReadOnly = true;
            this.ipBox.Size = new System.Drawing.Size(86, 20);
            this.ipBox.TabIndex = 2;
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(49, 30);
            this.nameBox.Name = "nameBox";
            this.nameBox.ReadOnly = true;
            this.nameBox.Size = new System.Drawing.Size(86, 20);
            this.nameBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Name:";
            // 
            // elapsedBox
            // 
            this.elapsedBox.Location = new System.Drawing.Point(367, 30);
            this.elapsedBox.Name = "elapsedBox";
            this.elapsedBox.ReadOnly = true;
            this.elapsedBox.Size = new System.Drawing.Size(86, 20);
            this.elapsedBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(297, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Elapsed Time:";
            // 
            // totalCntsBox
            // 
            this.totalCntsBox.Location = new System.Drawing.Point(367, 5);
            this.totalCntsBox.Name = "totalCntsBox";
            this.totalCntsBox.ReadOnly = true;
            this.totalCntsBox.Size = new System.Drawing.Size(86, 20);
            this.totalCntsBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(297, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Total Counts:";
            // 
            // queryBtn
            // 
            this.queryBtn.Location = new System.Drawing.Point(141, 5);
            this.queryBtn.Name = "queryBtn";
            this.queryBtn.Size = new System.Drawing.Size(152, 20);
            this.queryBtn.TabIndex = 9;
            this.queryBtn.Text = "Query Histogram";
            this.queryBtn.UseVisualStyleBackColor = true;
            this.queryBtn.Click += new System.EventHandler(this.queryBtn_Click);
            // 
            // zeroBtn
            // 
            this.zeroBtn.Location = new System.Drawing.Point(141, 30);
            this.zeroBtn.Name = "zeroBtn";
            this.zeroBtn.Size = new System.Drawing.Size(152, 20);
            this.zeroBtn.TabIndex = 10;
            this.zeroBtn.Text = "Zero Histogram";
            this.zeroBtn.UseVisualStyleBackColor = true;
            this.zeroBtn.Click += new System.EventHandler(this.zeroBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearBtn.Location = new System.Drawing.Point(10, 347);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(124, 33);
            this.clearBtn.TabIndex = 11;
            this.clearBtn.Text = "Clear Chart";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // expBtn
            // 
            this.expBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.expBtn.Location = new System.Drawing.Point(141, 347);
            this.expBtn.Name = "expBtn";
            this.expBtn.Size = new System.Drawing.Size(535, 33);
            this.expBtn.TabIndex = 13;
            this.expBtn.Text = "Save Histogram";
            this.expBtn.UseVisualStyleBackColor = true;
            this.expBtn.Click += new System.EventHandler(this.expBtn_Click);
            // 
            // refCB
            // 
            this.refCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.refCB.FormattingEnabled = true;
            this.refCB.Items.AddRange(new object[] {
            "5",
            "10",
            "30",
            "60",
            "120",
            "240"});
            this.refCB.Location = new System.Drawing.Point(468, 31);
            this.refCB.Name = "refCB";
            this.refCB.Size = new System.Drawing.Size(104, 21);
            this.refCB.TabIndex = 14;
            this.refCB.SelectedIndexChanged += new System.EventHandler(this.refCB_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = global::QIY_Interface__IAEA_.Properties.Resources.QI_justQ;
            this.pictureBox1.Location = new System.Drawing.Point(624, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // queryCheck
            // 
            this.queryCheck.AutoSize = true;
            this.queryCheck.Location = new System.Drawing.Point(468, 10);
            this.queryCheck.Name = "queryCheck";
            this.queryCheck.Size = new System.Drawing.Size(109, 17);
            this.queryCheck.TabIndex = 16;
            this.queryCheck.Text = "Query After Every";
            this.queryCheck.UseVisualStyleBackColor = true;
            this.queryCheck.CheckedChanged += new System.EventHandler(this.queryCheck_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(575, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Seconds";
            // 
            // OnlineHGMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 386);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.queryCheck);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.refCB);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.expBtn);
            this.Controls.Add(this.zeroBtn);
            this.Controls.Add(this.queryBtn);
            this.Controls.Add(this.elapsedBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.totalCntsBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hgmPlot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OnlineHGMForm";
            this.Text = "HGM Online";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HideForm);
            this.Load += new System.EventHandler(this.OnlineHGMForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView hgmPlot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox elapsedBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox totalCntsBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button queryBtn;
        private System.Windows.Forms.Button zeroBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button expBtn;
        private System.Windows.Forms.ComboBox refCB;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox queryCheck;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}
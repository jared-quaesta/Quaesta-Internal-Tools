
namespace QIY_Interface__IAEA_
{
    partial class DirectoriesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectoriesForm));
            this.datBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chooseDat = new System.Windows.Forms.Button();
            this.chooseHgm = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.hgmBox = new System.Windows.Forms.TextBox();
            this.chooseBin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.binBox = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // datBox
            // 
            this.datBox.Location = new System.Drawing.Point(10, 31);
            this.datBox.Name = "datBox";
            this.datBox.Size = new System.Drawing.Size(608, 20);
            this.datBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Transferred DAT File Directory:";
            // 
            // chooseDat
            // 
            this.chooseDat.Location = new System.Drawing.Point(623, 30);
            this.chooseDat.Name = "chooseDat";
            this.chooseDat.Size = new System.Drawing.Size(52, 20);
            this.chooseDat.TabIndex = 2;
            this.chooseDat.Text = "Choose";
            this.chooseDat.UseVisualStyleBackColor = true;
            this.chooseDat.Click += new System.EventHandler(this.chooseDat_Click);
            // 
            // chooseHgm
            // 
            this.chooseHgm.Location = new System.Drawing.Point(624, 88);
            this.chooseHgm.Name = "chooseHgm";
            this.chooseHgm.Size = new System.Drawing.Size(52, 20);
            this.chooseHgm.TabIndex = 5;
            this.chooseHgm.Text = "Choose";
            this.chooseHgm.UseVisualStyleBackColor = true;
            this.chooseHgm.Click += new System.EventHandler(this.chooseHgm_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Transferred HGM File Directory:";
            // 
            // hgmBox
            // 
            this.hgmBox.Location = new System.Drawing.Point(11, 89);
            this.hgmBox.Name = "hgmBox";
            this.hgmBox.Size = new System.Drawing.Size(608, 20);
            this.hgmBox.TabIndex = 3;
            // 
            // chooseBin
            // 
            this.chooseBin.Location = new System.Drawing.Point(625, 149);
            this.chooseBin.Name = "chooseBin";
            this.chooseBin.Size = new System.Drawing.Size(52, 20);
            this.chooseBin.TabIndex = 8;
            this.chooseBin.Text = "Choose";
            this.chooseBin.UseVisualStyleBackColor = true;
            this.chooseBin.Click += new System.EventHandler(this.chooseBin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Transferred BIN File Directory:";
            // 
            // binBox
            // 
            this.binBox.Location = new System.Drawing.Point(11, 149);
            this.binBox.Name = "binBox";
            this.binBox.Size = new System.Drawing.Size(608, 20);
            this.binBox.TabIndex = 6;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(10, 175);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(667, 28);
            this.saveBtn.TabIndex = 9;
            this.saveBtn.Text = "Save Directory Settings";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // DirectoriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 213);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.chooseBin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.binBox);
            this.Controls.Add(this.chooseHgm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hgmBox);
            this.Controls.Add(this.chooseDat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.datBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DirectoriesForm";
            this.Text = "PC Working File Transfer Directories";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HideForm);
            this.Load += new System.EventHandler(this.DirectoriesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox datBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button chooseDat;
        private System.Windows.Forms.Button chooseHgm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox hgmBox;
        private System.Windows.Forms.Button chooseBin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox binBox;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
    }
}
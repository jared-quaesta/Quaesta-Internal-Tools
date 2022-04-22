
using System.Windows.Forms;

namespace SWARMVis
{
    partial class GetMessageForm
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
            this.expPath = new System.Windows.Forms.TextBox();
            this.findPath = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.exportButton = new System.Windows.Forms.Button();
            this.decodeAscii = new System.Windows.Forms.CheckBox();
            this.errMsg = new System.Windows.Forms.Label();
            this.decodeHex = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // expPath
            // 
            this.expPath.Location = new System.Drawing.Point(12, 565);
            this.expPath.Name = "expPath";
            this.expPath.Size = new System.Drawing.Size(699, 23);
            this.expPath.TabIndex = 0;
            // 
            // findPath
            // 
            this.findPath.Location = new System.Drawing.Point(718, 565);
            this.findPath.Name = "findPath";
            this.findPath.Size = new System.Drawing.Size(28, 23);
            this.findPath.TabIndex = 1;
            this.findPath.Text = "...";
            this.findPath.UseVisualStyleBackColor = true;
            this.findPath.Click += new System.EventHandler(this.findPath_Click);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(526, 22);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(220, 23);
            this.exportButton.TabIndex = 2;
            this.exportButton.Text = "Export Selected to CSV";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // decodeAscii
            // 
            this.decodeAscii.AutoSize = true;
            this.decodeAscii.Location = new System.Drawing.Point(526, 68);
            this.decodeAscii.Name = "decodeAscii";
            this.decodeAscii.Size = new System.Drawing.Size(112, 19);
            this.decodeAscii.TabIndex = 3;
            this.decodeAscii.Text = "Decode To ASCII";
            this.decodeAscii.UseVisualStyleBackColor = true;
            this.decodeAscii.CheckedChanged += new System.EventHandler(this.decodeCB_CheckedChanged);
            // 
            // errMsg
            // 
            this.errMsg.ForeColor = System.Drawing.Color.Red;
            this.errMsg.Location = new System.Drawing.Point(526, 137);
            this.errMsg.Name = "errMsg";
            this.errMsg.Size = new System.Drawing.Size(198, 137);
            this.errMsg.TabIndex = 4;
            // 
            // decodeHex
            // 
            this.decodeHex.AutoSize = true;
            this.decodeHex.Location = new System.Drawing.Point(526, 93);
            this.decodeHex.Name = "decodeHex";
            this.decodeHex.Size = new System.Drawing.Size(153, 19);
            this.decodeHex.TabIndex = 5;
            this.decodeHex.Text = "Decode To Hexadecimal";
            this.decodeHex.UseVisualStyleBackColor = true;
            // 
            // GetMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 597);
            this.Controls.Add(this.decodeHex);
            this.Controls.Add(this.errMsg);
            this.Controls.Add(this.decodeAscii);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.findPath);
            this.Controls.Add(this.expPath);
            this.Name = "GetMessageForm";
            this.Text = "Export Messages";
            this.Load += new System.EventHandler(this.GetMessageForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox listBox1;
        private TextBox expPath;
        private Button findPath;
        private FolderBrowserDialog folderBrowser;
        private Button exportButton;
        private CheckBox decodeAscii;
        private Label errMsg;
        private CheckBox decodeHex;
    }
}
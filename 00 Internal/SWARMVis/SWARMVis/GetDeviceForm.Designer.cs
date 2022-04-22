
namespace SWARMVis
{
    partial class GetDeviceForm
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
            this.exportButton = new System.Windows.Forms.Button();
            this.expPath = new System.Windows.Forms.TextBox();
            this.findPath = new System.Windows.Forms.Button();
            this.errMsg = new System.Windows.Forms.Label();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(198, 125);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(149, 23);
            this.exportButton.TabIndex = 0;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // expPath
            // 
            this.expPath.Location = new System.Drawing.Point(12, 572);
            this.expPath.Name = "expPath";
            this.expPath.Size = new System.Drawing.Size(388, 23);
            this.expPath.TabIndex = 1;
            // 
            // findPath
            // 
            this.findPath.Location = new System.Drawing.Point(406, 572);
            this.findPath.Name = "findPath";
            this.findPath.Size = new System.Drawing.Size(24, 23);
            this.findPath.TabIndex = 2;
            this.findPath.Text = "...";
            this.findPath.UseVisualStyleBackColor = true;
            this.findPath.Click += new System.EventHandler(this.findPath_Click);
            // 
            // errMsg
            // 
            this.errMsg.ForeColor = System.Drawing.Color.Red;
            this.errMsg.Location = new System.Drawing.Point(275, 278);
            this.errMsg.Name = "errMsg";
            this.errMsg.Size = new System.Drawing.Size(134, 176);
            this.errMsg.TabIndex = 3;
            // 
            // GetDeviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 604);
            this.Controls.Add(this.errMsg);
            this.Controls.Add(this.findPath);
            this.Controls.Add(this.expPath);
            this.Controls.Add(this.exportButton);
            this.Name = "GetDeviceForm";
            this.Text = "Export Devices";
            this.Load += new System.EventHandler(this.GetDeviceForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.TextBox expPath;
        private System.Windows.Forms.Button findPath;
        private System.Windows.Forms.Label errMsg;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
    }
}
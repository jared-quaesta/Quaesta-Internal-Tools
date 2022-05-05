
namespace Universal_NPM_Interface.Controls
{
    partial class SingleParameterControl
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
            this.paramName = new System.Windows.Forms.Label();
            this.paramValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // paramName
            // 
            this.paramName.Location = new System.Drawing.Point(-3, 0);
            this.paramName.Name = "paramName";
            this.paramName.Size = new System.Drawing.Size(100, 20);
            this.paramName.TabIndex = 14;
            this.paramName.Text = "Voltage Set";
            this.paramName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // paramValue
            // 
            this.paramValue.Location = new System.Drawing.Point(103, 0);
            this.paramValue.Name = "paramValue";
            this.paramValue.Size = new System.Drawing.Size(145, 20);
            this.paramValue.TabIndex = 15;
            // 
            // ParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.paramName);
            this.Controls.Add(this.paramValue);
            this.Name = "ParameterControl";
            this.Size = new System.Drawing.Size(249, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label paramName;
        private System.Windows.Forms.TextBox paramValue;
    }
}

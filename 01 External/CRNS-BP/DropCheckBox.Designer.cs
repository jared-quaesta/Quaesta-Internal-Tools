
namespace CRNS_BP
{
    partial class DropCheckBox
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
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.checkListPanel = new System.Windows.Forms.Panel();
            this.checkListBox = new System.Windows.Forms.CheckedListBox();
            this.checkListPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComboBox
            // 
            this.comboBox.DropDownHeight = 1;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.IntegralHeight = false;
            this.comboBox.Location = new System.Drawing.Point(0, 0);
            this.comboBox.Name = "ComboBox";
            this.comboBox.Size = new System.Drawing.Size(121, 21);
            this.comboBox.TabIndex = 0;
            this.comboBox.DropDown += new System.EventHandler(this.CancelDropdown);
            this.comboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CancelKey);
            this.comboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OpenClosePanel);
            // 
            // checkListPanel
            // 
            this.checkListPanel.Controls.Add(this.checkListBox);
            this.checkListPanel.Location = new System.Drawing.Point(3, 20);
            this.checkListPanel.Name = "checkListPanel";
            this.checkListPanel.Size = new System.Drawing.Size(115, 264);
            this.checkListPanel.TabIndex = 1;
            this.checkListPanel.Visible = false;
            // 
            // checkListBox
            // 
            this.checkListBox.CheckOnClick = true;
            this.checkListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkListBox.FormattingEnabled = true;
            this.checkListBox.Location = new System.Drawing.Point(0, 0);
            this.checkListBox.Name = "checkListBox";
            this.checkListBox.Size = new System.Drawing.Size(115, 264);
            this.checkListBox.TabIndex = 0;
            this.checkListBox.SelectedIndexChanged += new System.EventHandler(this.UpdateChecks);
            // 
            // DropCheckBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.checkListPanel);
            this.Controls.Add(this.comboBox);
            this.Name = "DropCheckBox";
            this.Size = new System.Drawing.Size(121, 284);
            this.checkListPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Panel checkListPanel;
        private System.Windows.Forms.CheckedListBox checkListBox;
    }
}

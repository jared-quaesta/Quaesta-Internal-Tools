
namespace SWARMVis
{
    partial class LoginForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.emailBox = new System.Windows.Forms.TextBox();
            this.pwBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.logInButton = new System.Windows.Forms.Button();
            this.errLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // emailBox
            // 
            this.emailBox.Location = new System.Drawing.Point(13, 54);
            this.emailBox.Name = "emailBox";
            this.emailBox.Size = new System.Drawing.Size(323, 23);
            this.emailBox.TabIndex = 2;
            this.emailBox.Text = "shamann@quaestainstruments.com";
            // 
            // pwBox
            // 
            this.pwBox.Location = new System.Drawing.Point(13, 102);
            this.pwBox.Name = "pwBox";
            this.pwBox.Size = new System.Drawing.Size(323, 23);
            this.pwBox.TabIndex = 3;
            this.pwBox.Text = "quaesta2021";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Log In To SWARM";
            // 
            // logInButton
            // 
            this.logInButton.Location = new System.Drawing.Point(261, 131);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(75, 23);
            this.logInButton.TabIndex = 5;
            this.logInButton.Text = "Log in";
            this.logInButton.UseVisualStyleBackColor = true;
            this.logInButton.Click += new System.EventHandler(this.TryCredentials);
            // 
            // errLbl
            // 
            this.errLbl.AutoSize = true;
            this.errLbl.ForeColor = System.Drawing.Color.Red;
            this.errLbl.Location = new System.Drawing.Point(13, 137);
            this.errLbl.Name = "errLbl";
            this.errLbl.Size = new System.Drawing.Size(0, 15);
            this.errLbl.TabIndex = 6;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 161);
            this.Controls.Add(this.errLbl);
            this.Controls.Add(this.logInButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pwBox);
            this.Controls.Add(this.emailBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LoginForm";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox emailBox;
        private System.Windows.Forms.TextBox pwBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button logInButton;
        private System.Windows.Forms.Label errLbl;
    }
}
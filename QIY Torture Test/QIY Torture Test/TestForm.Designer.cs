
namespace QIY_Torture_Test
{
    partial class TestForm
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
            this.dlConLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.relayLbl = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.runtimeLbl = new System.Windows.Forms.Label();
            this.testLoopWorker = new System.ComponentModel.BackgroundWorker();
            this.label4 = new System.Windows.Forms.Label();
            this.cyclesLbl = new System.Windows.Forms.Label();
            this.stageTimerWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Datalogger:";
            // 
            // dlConLbl
            // 
            this.dlConLbl.AutoSize = true;
            this.dlConLbl.ForeColor = System.Drawing.Color.Green;
            this.dlConLbl.Location = new System.Drawing.Point(74, 6);
            this.dlConLbl.Name = "dlConLbl";
            this.dlConLbl.Size = new System.Drawing.Size(59, 13);
            this.dlConLbl.TabIndex = 1;
            this.dlConLbl.Text = "Connected";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Relay:";
            // 
            // relayLbl
            // 
            this.relayLbl.AutoSize = true;
            this.relayLbl.ForeColor = System.Drawing.Color.Green;
            this.relayLbl.Location = new System.Drawing.Point(74, 19);
            this.relayLbl.Name = "relayLbl";
            this.relayLbl.Size = new System.Drawing.Size(23, 13);
            this.relayLbl.TabIndex = 3;
            this.relayLbl.Text = "ON";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(156, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Runtime:";
            // 
            // runtimeLbl
            // 
            this.runtimeLbl.AutoSize = true;
            this.runtimeLbl.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runtimeLbl.Location = new System.Drawing.Point(160, 19);
            this.runtimeLbl.Name = "runtimeLbl";
            this.runtimeLbl.Size = new System.Drawing.Size(130, 22);
            this.runtimeLbl.TabIndex = 11;
            this.runtimeLbl.Text = "0:00:00:00";
            // 
            // testLoopWorker
            // 
            this.testLoopWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.RunTestLoop);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Cycles:";
            // 
            // cyclesLbl
            // 
            this.cyclesLbl.AutoSize = true;
            this.cyclesLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cyclesLbl.Location = new System.Drawing.Point(74, 32);
            this.cyclesLbl.Name = "cyclesLbl";
            this.cyclesLbl.Size = new System.Drawing.Size(13, 13);
            this.cyclesLbl.TabIndex = 13;
            this.cyclesLbl.Text = "0";
            // 
            // stageTimerWorker
            // 
            this.stageTimerWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.stageTimerWorker_DoWork);
            // 
            // StageOneTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 285);
            this.Controls.Add(this.cyclesLbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.runtimeLbl);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.relayLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dlConLbl);
            this.Controls.Add(this.label1);
            this.Name = "StageOneTestForm";
            this.Text = "Stage One";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseDown);
            this.Load += new System.EventHandler(this.StageOneTestForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label dlConLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label relayLbl;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label runtimeLbl;
        private System.ComponentModel.BackgroundWorker testLoopWorker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label cyclesLbl;
        private System.ComponentModel.BackgroundWorker stageTimerWorker;
    }
}
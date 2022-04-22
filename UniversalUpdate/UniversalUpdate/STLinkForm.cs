using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using UniversalUpdate.Properties;

namespace UniversalUpdate
{
    public partial class STLinkForm : Form
    {
        string tempPath = Path.Combine(Path.GetTempPath(), "fw.hex");
        public STLinkForm()
        {
            InitializeComponent();
        }

        private void STLinkForm_Load(object sender, EventArgs e)
        {

        }

        private void flashBtn_Click(object sender, EventArgs e)
        {
            if (fwBox.Text.Length == 0) return;
            byte[] hex = new byte[1];

            if (fwBox.Text.Equals("QIX Suri LP"))
            {
                hex = Resources.QIX_SURI_B_LP_100;
            }

            // write hex to temp file
            using (FileStream fs = new FileStream(tempPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                fs.Write(hex, 0, hex.Length);
            }
            RunBatch();
            File.Delete(tempPath);
        }

        private void RunBatch()
        {
            //st - link_cli - C SWD UR FREQ = 1800 - ME
            //st - link_cli - C SWD UR Hrst FREQ = 1800 - OB BOR_LEV = 4 nBoot0_SW_Cfg = 0 - P QIX_SURI_B_LP_100.hex - V - HardRst
            //st - link_cli - C SWD UR FREQ = 1800 - CmpFile QIX_SURI_B_LP_100.hex 0x08000000
            progressTxt.ForeColor = Color.Black;
            progressTxt.Text = "Running Task...";

            int exitCode;
            string done = "Complete!";
            ProcessStartInfo processInfo;

            processInfo = new ProcessStartInfo("cmd.exe", "/c" + "st-link_cli -C SWD UR FREQ=1800 -ME&&" +
                $"st-link_cli -C SWD UR Hrst FREQ=1800 -OB BOR_LEV=4 nBoot0_SW_Cfg=0 -P {tempPath} -V -HardRst&&" +
                $"st-link_cli -C SWD UR FREQ=1800 -CmpFile {tempPath} 0x08000000");
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;


            var process = Process.Start(processInfo);
            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                if (e.Data != null && e.Data.Contains("No target connected"))
                {
                    done = "No Target";
                    process.Close();
                }
                //outputBox.Invoke((MethodInvoker)delegate
                //{
                //    outputBox.AppendText(e.Data);
                //    outputBox.Refresh();
                //});
            };
            process.BeginOutputReadLine();

            //process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            //{
            //    //Invoke((MethodInvoker)delegate
            //    //{
            //    //    outputBox.AppendText(e.Data);
            //    //    outputBox.Refresh();
            //    //});
            //    Debug.WriteLine("ERR " + e.Data);
            //};
            process.BeginErrorReadLine();

            process.WaitForExit();
            //Invoke((MethodInvoker)delegate
            //{
            //    outputBox.AppendText(process.ExitCode.ToString());
            //    outputBox.Refresh();
            //});
            process.Close();
            if (done.Equals("Complete!"))
                progressTxt.ForeColor = Color.Green;
            else
                progressTxt.ForeColor = Color.Red;
            progressTxt.Text = done;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pingNetwork {

    public class CommandPrompt {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static bool Write(string cmd) {
            try {
                var p = new System.Diagnostics.Process();
                p.StartInfo.FileName = @"c:\Windows\Sysnative\cmd.exe";
                p.StartInfo.Arguments = @"/c " + cmd;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardInput = false;
                p.Start();
                p.WaitForExit();
                return true;

            }
            catch { return false; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static string Query(string cmd) {
            try {
                string strOutput = "";
                var p = new System.Diagnostics.Process();
                p.StartInfo.FileName = @"c:\Windows\Sysnative\cmd.exe";
                p.StartInfo.Arguments = @"/c " + cmd;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardInput = false;
                p.OutputDataReceived += (a, b) => strOutput += b.Data + "\n";
                p.ErrorDataReceived += (a, b) => strOutput += b.Data + "\n";
                p.Start();
                p.BeginErrorReadLine();
                p.BeginOutputReadLine();
                p.WaitForExit();
                return strOutput;
            }
            catch { return null; }
        }


        public static bool Query(string cmd, out string message) {
            message = "";
            string data = "";
            try {
                var p = new System.Diagnostics.Process();
                p.StartInfo.FileName = @"c:\Windows\Sysnative\cmd.exe";
                //p.StartInfo.FileName = @"C:\windows\system32\cmd.exe";
                p.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                //p.StartInfo.Arguments = @"/c " + cmd;
                p.StartInfo.Arguments = "/c ping -t " + "192.168.88.1" + " ";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardInput = false;
                p.OutputDataReceived += (a, b) => data += b.Data + "\n";
                p.ErrorDataReceived += (a, b) => data += b.Data + "\n";
                p.Start();
                p.BeginErrorReadLine();
                p.BeginOutputReadLine();
                p.WaitForExit();
                message = data;
                return true;
            }
            catch { return false; }
        }
    }

}

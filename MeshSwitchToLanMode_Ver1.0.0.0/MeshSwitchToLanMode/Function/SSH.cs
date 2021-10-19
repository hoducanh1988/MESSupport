using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeshSwitchToLanMode.Function {

    public class SSH {

        private ShellStream shellStreamSSH;
        private SshClient sshClient;

        public bool Login(string IPAddress, string Username, string Pass) {
            try {
                this.sshClient = new SshClient(IPAddress, 22, Username, Pass);

                //Thực hiện kết nối
                this.sshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(10);
                this.sshClient.Connect();

                // tạo shell stream để điều khiển command ssh
                this.shellStreamSSH = this.sshClient.CreateShellStream("vt100", 80, 60, 800, 600, 65535);

                return true;
            }
            catch {
                return false;
            }
        }

        public void Write(string cmd) {
            this.shellStreamSSH.Write(cmd);
            this.shellStreamSSH.Flush();
        }

        public void WriteLine(string cmd) {
            this.Write(cmd + "\r\n");
        }

        public string Read() {
            string value = "NULL";
            try {
                value = shellStreamSSH.Read();
                myGlobal.myTesting.logSsh += value;
                return value;
            }
            catch { return value; }
        }

        public string Query(string cmd, int delay_ms) {
            this.WriteLine(cmd);
            Thread.Sleep(delay_ms);
            return this.Read();
        }

        public bool Query(string cmd, int timeout_sec, string pattern) {
            bool r = false;
            int count = 0;
            int max_count = (timeout_sec * 1000) / 100;
            string data = "";

            this.WriteLine(cmd);
        RE:
            count++;
            Thread.Sleep(100);
            data += this.Read();
            r = data.ToLower().Contains(pattern.ToLower());
            if (!r) { if (count < max_count) goto RE; }
            return r;
        }

        public bool Query(string cmd, int timeout_sec, params string[] patterns) {
            bool r = false;
            int count = 0;
            int max_count = (timeout_sec * 1000) / 100;
            string data = "";

            this.WriteLine(cmd);
        RE:
            count++;
            Thread.Sleep(100);
            data += this.Read();
            foreach (var p in patterns) {
                r = data.ToLower().Contains(p.ToLower());
                if (!r) break;
            }
            if (!r) { if (count < max_count) goto RE; }
            return r;
        }

        public bool Query(string cmd, int timeout_sec, out string msg, string end_with) {
            bool r = false;
            int count = 0;
            int max_count = (timeout_sec * 1000) / 100;
            string data = "";

            this.WriteLine(cmd);
        RE:
            count++;
            Thread.Sleep(100);
            data += this.Read();
            r = data.ToLower().Replace("\r", "").Replace("\n", "").Trim().EndsWith(end_with.ToLower());
            if (!r) { if (count < max_count) goto RE; }
            msg = data;
            return r;
        }




        public void Disconnect() {
            if (this.sshClient != null) this.sshClient.Disconnect();
        }

        public void Close() {
            if (this.sshClient != null) this.sshClient.Dispose();
        }

        public void CloseShellStream() {
            if (this.shellStreamSSH != null) this.shellStreamSSH.Close();
        }

    }
}

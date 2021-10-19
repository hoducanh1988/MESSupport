using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeshConfigDhcpServer.Function.Protocol {

    public class SSH<T> where T : class, new() {

        T t = null;
        private ShellStream shellStreamSSH;
        private SshClient sshClient;

        public SSH(T _t) {
            this.t = _t;
        }

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
            //Thread.Sleep(500);
            try {
                //Thread.Sleep(500);
                value = shellStreamSSH.Read();

                var sshlog_property = t.GetType().GetProperty("logSystem");
                string data = (string)sshlog_property.GetValue(t, null);
                data += value;
                sshlog_property.SetValue(t, Convert.ChangeType(data, sshlog_property.PropertyType), null);

                //Thread.Sleep(500);
                return value;
            }
            catch {
                return value;
            };
        }

        public string Query(string cmd, int delay_time) {
            this.WriteLine(cmd);
            Thread.Sleep(delay_time);
            return this.Read();
        }

        public bool Query(string cmd, string pattern, int timeout_ms) {
            this.WriteLine(cmd);

            bool r = false;
            int count = 0;
            int max_count = timeout_ms / 100;
            string data = "";

        RE:
            count++;
            data += this.Read();
            r = data.ToLower().Contains(pattern.ToLower());
            if (!r) {
                if (count < max_count) {
                    Thread.Sleep(100);
                    goto RE;
                }
            }

            return r;
        }

        public bool Query(string cmd, string pattern, int timeout_ms, out string data_feedback) {
            this.WriteLine(cmd);

            bool r = false;
            int count = 0;
            int max_count = timeout_ms / 100;
            string data = "";
            data_feedback = "";

        RE:
            count++;
            data += this.Read();
            r = data.ToLower().Contains(pattern.ToLower());
            if (!r) {
                if (count < max_count) {
                    Thread.Sleep(100);
                    goto RE;
                }
            }
            data_feedback = data;
            return r;
        }

        public bool Query(string cmd, string pattern, int timeout_ms, int retry_time, out string data_feedback) {
            data_feedback = "";

            string data_out = "";
            bool r = false;
            int count = 0;

        RE:
            count++;
            r = this.Query(cmd, pattern, timeout_ms, out data_out);
            data_feedback += data_out;
            if (!r) {
                if (count < retry_time) {
                    goto RE;
                }
            }
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
            //Thread.Sleep(500);
            try {
                //Thread.Sleep(500);
                value = shellStreamSSH.Read();
                //Thread.Sleep(500);
                return value;
            }
            catch {
                return value;
            };
        }

        public string Query(string cmd, int delay_time) {
            this.WriteLine(cmd);
            Thread.Sleep(delay_time);
            return this.Read();
        }

        public bool Query(string cmd, string pattern, int timeout_ms, out string data_feedback) {
            this.WriteLine(cmd);

            bool r = false;
            int count = 0;
            int max_count = timeout_ms / 100;
            string data = "";
            data_feedback = "";

        RE:
            count++;
            data += this.Read();
            r = data.ToLower().Contains(pattern.ToLower());
            if (!r) {
                if (count < max_count) {
                    Thread.Sleep(100);
                    goto RE;
                }
            }
            data_feedback = data;
            return r;
        }

        public bool Query(string cmd, string pattern, int timeout_ms, int retry_time, out string data_feedback) {
            data_feedback = "";

            string data_out = "";
            bool r = false;
            int count = 0;

        RE:
            count++;
            r = this.Query(cmd, pattern, timeout_ms, out data_out);
            data_feedback += data_out;
            if (!r) {
                if (count < retry_time) {
                    goto RE;
                }
            }
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

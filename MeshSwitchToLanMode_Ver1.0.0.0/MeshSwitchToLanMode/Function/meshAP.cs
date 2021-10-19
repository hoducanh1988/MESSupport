using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeshSwitchToLanMode.Function {
    public class meshAP : IDisposable {

        SSH mesh = null;
        string ip, user, pass;
        bool isConnect = false;

        public meshAP(string ip, string user, string pass) {
            this.ip = ip;
            this.user = user;
            this.pass = pass;
        }

        public bool Login(int retry_time) {
            if (mesh == null) mesh = new SSH();
            int count = 0;
            bool r = false;
        RE:
            count++;
            r = mesh.Login(ip, user, pass);
            if (!r) {
                if (count < retry_time) {
                    Thread.Sleep(1000);
                    goto RE;
                }
            }

            isConnect = r;
            return r;
        }

        public bool switchToLanMode() {
            bool r = false;

            //login to ONT
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return false;

            //switch to mode lan
            r = mesh.Query("uci set network.lan.ifname=eth0", 3, "root@VNPT:~#");
            if (!r) return false;
            r = mesh.Query("uci delete network.wan", 3, "root@VNPT:~#");
            if (!r) return false;
            r = mesh.Query("uci commit network", 3, "root@VNPT:~#");
            if (!r) return false;
            r = mesh.Query("rm /etc/init.d/checkmode", 3, "root@VNPT:~#");
            if (!r) return false;
            r = mesh.Query("uci set repacd.repacd.Enable=0", 3, "root@VNPT:~#");
            if (!r) return false;
            r = mesh.Query("uci commit repacd", 3, "root@VNPT:~#");

            //if (!r) return false;
            //r = mesh.Query("/etc/init.d/network restart && /etc/init.d/dnsmasq restart", 60, "root@VNPT:~#");

            return r;
        }

        public bool getMeshMode() {
            bool r = false;

            //login to ONT
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return false;

            string data = "";
            int count = 0;
            string mode = "";
        RE:
            count++;
            data = mesh.Query("uci get network.lan.ifname", 300).ToLower();
            if (!data.Contains("root@vnpt:~#") || !data.Contains("uci get network.lan.ifname")) {
                if (count < 3) goto RE;
                else return false;
            }

            try {
                mode = data.Split(new string[] { "uci get network.lan.ifname" }, StringSplitOptions.None)[1];
                mode = mode.Split(new string[] { "root@vnpt:~#" }, StringSplitOptions.None)[0];
                mode = mode.Replace("\r", "").Replace("\n", "").Replace(":", "").Replace("root@vnpt:~#", "").Trim();
            }
            catch {
                if (count < 3) goto RE;
                else return false;
            }
            r = mode.ToLower().Equals("eth0");
            
            return r;
        }

        public bool changeIpLanPort(string new_ip) {
            bool r = false;

            //login to ONT
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return false;

            //change lan ip
            mesh.WriteLine(string.Format("ifconfig br-lan {0}", new_ip));
            Thread.Sleep(100);
            return r;
        }

        public bool waitNewIPApplied(string new_ip, int timeout_sec) {
            bool r = false;
            int count = 0;

        RE:
            count++;
            Thread.Sleep(1000);
            r = pingToMesh(new_ip);
            if (!r) {
                if (count < timeout_sec) goto RE;
            }

            return r;
        }

        public string getMacEthernet() {
            //login to ONT
            bool r = false;
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return null;

            int count = 0;
            string mac = "";
            string data = "";
        RE:
            count++;
            data = mesh.Query("cat /sys/class/net/eth0/address", 300).ToLower();
            if (!data.Contains("root@vnpt:~#") || !data.Contains("cat /sys/class/net/eth0/address")) {
                if (count < 3) goto RE;
                else return null;
            }

            try {
                mac = data.Split(new string[] { "cat /sys/class/net/eth0/address" }, StringSplitOptions.None)[1];
                mac = mac.Split(new string[] { "root@vnpt:~#" }, StringSplitOptions.None)[0];
                mac = mac.Replace("\r", "").Replace("\n", "").Replace(":", "").Replace("root@vnpt:~#", "");
                mac = mac.Replace(":", "").Replace("-", "").Trim();
                if (mac.Length != 12) { if (count < 3) goto RE; }
            }
            catch {
                if (count < 3) goto RE;
                else return null;
            }

            return mac;
        }

        public string getMacWlan2G() {
            //login to ONT
            bool r = false;
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return null;

            int count = 0;
            string mac = "";
            string data = "";
        RE:
            count++;
            data = mesh.Query("cat /sys/class/net/wifi0/address", 300).ToLower();
            if (!data.Contains("root@vnpt:~#") || !data.Contains("cat /sys/class/net/wifi0/address")) {
                if (count < 3) goto RE;
                else return null;
            }

            try {
                mac = data.Split(new string[] { "cat /sys/class/net/wifi0/address" }, StringSplitOptions.None)[1];
                mac = mac.Split(new string[] { "root@vnpt:~#" }, StringSplitOptions.None)[0];
                mac = mac.Replace("\r", "").Replace("\n", "").Replace(":", "").Replace("root@vnpt:~#", "");
                mac = mac.Replace(":", "").Replace("-", "").Trim();
                if (mac.Length != 12) { if (count < 3) goto RE; }
            }
            catch {
                if (count < 3) goto RE;
                else return null;
            }

            return mac;
        }

        public string getMacWlan5G() {
            //login to ONT
            bool r = false;
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return null;

            int count = 0;
            string mac = "";
            string data = "";
        RE:
            count++;
            data = mesh.Query("cat /sys/class/net/wifi1/address", 300).ToLower();
            if (!data.Contains("root@vnpt:~#") || !data.Contains("cat /sys/class/net/wifi1/address")) {
                if (count < 3) goto RE;
                else return null;
            }

            try {
                mac = data.Split(new string[] { "cat /sys/class/net/wifi1/address" }, StringSplitOptions.None)[1];
                mac = mac.Split(new string[] { "root@vnpt:~#" }, StringSplitOptions.None)[0];
                mac = mac.Replace("\r", "").Replace("\n", "").Replace(":", "").Replace("root@vnpt:~#", "");
                mac = mac.Replace(":", "").Replace("-", "").Trim();
                if (mac.Length != 12) { if (count < 3) goto RE; }
            }
            catch {
                if (count < 3) goto RE;
                else return null;
            }

            return mac;
        }

        public string getHardwareVersion() {
            //login to ONT
            bool r = false;
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return null;

            int count = 0;
            string hw_ver = "";
            string data = "";
        RE:
            count++;
            data = mesh.Query("fw_printenv", 300).ToLower();
            if (!data.Contains("hardwareversion")) {
                if (count < 3) goto RE;
                else return null;
            }

            try {
                string[] buffer = data.Split('\n');
                foreach (var b in buffer) {
                    if (b.Contains("hardwareversion")) {
                        hw_ver = b;
                        break;
                    }
                }
                hw_ver = hw_ver.Split(new string[] { "hardwareversion=" }, StringSplitOptions.None)[1];
                hw_ver = hw_ver.Replace("\n", "").Replace("\r", "").Trim();
            }
            catch {
                if (count < 3) goto RE;
                else return null;
            }

            return hw_ver;
        }

        public string getModelNumber() {
            //login to ONT
            bool r = false;
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return null;

            int count = 0;
            string model = "";
            string data = "";
        RE:
            count++;
            data = mesh.Query("fw_printenv", 300).ToLower();
            if (!data.Contains("modelnumber")) {
                if (count < 3) goto RE;
                else return null;
            }

            try {
                string[] buffer = data.Split('\n');
                foreach (var b in buffer) {
                    if (b.Contains("modelnumber")) {
                        model = b;
                        break;
                    }
                }
                model = model.Split(new string[] { "modelnumber=" }, StringSplitOptions.None)[1];
                model = model.Replace("\n", "").Replace("\r", "").Trim();
            }
            catch {
                if (count < 3) goto RE;
                else return null;
            }

            return model;
        }

        public string getSerialNumber() {
            //login to ONT
            bool r = false;
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return null;

            int count = 0;
            string serial = "";
            string data = "";
        RE:
            count++;
            data = mesh.Query("fw_printenv", 300).ToLower();
            if (!data.Contains("serialnumber")) {
                if (count < 3) goto RE;
                else return null;
            }

            try {
                string[] buffer = data.Split('\n');
                foreach (var b in buffer) {
                    if (b.Contains("serialnumber")) {
                        serial = b;
                        break;
                    }
                }
                serial = serial.Split(new string[] { "serialnumber=" }, StringSplitOptions.None)[1];
                serial = serial.Replace("\n", "").Replace("\r", "").Trim();
            }
            catch {
                if (count < 3) goto RE;
                else return null;
            }

            return serial;
        }

        public string getFirmwareVersion() {
            //login to ONT
            bool r = false;
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return null;

            int count = 0;
            string fw_ver = "";
            string data = "";
        RE:
            count++;
            data = mesh.Query("cat /etc/fw_info", 300).ToLower();
            if (!data.Contains("root@vnpt:~#") || !data.Contains("cat /etc/fw_info")) {
                if (count < 3) goto RE;
                else return null;
            }

            try {
                fw_ver = data.Split(new string[] { "cat /etc/fw_info" }, StringSplitOptions.None)[1];
                fw_ver = fw_ver.Split(new string[] { "root@vnpt:~#" }, StringSplitOptions.None)[0];
                fw_ver = fw_ver.Replace("\r", "").Replace("\n", "").Replace("root@vnpt:~#", "");
                fw_ver = fw_ver.Replace("firmware version:", "").Trim();
            }
            catch {
                if (count < 3) goto RE;
                else return null;
            }

            return fw_ver;
        }

        public string getFirmwareBuildTime() {
            //login to ONT
            bool r = false;
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return null;

            int count = 0;
            string fw_bt = "";
            string data = "";
        RE:
            count++;
            data = mesh.Query("cat /proc/version", 300).ToLower();
            if (!data.Contains("root@vnpt:~#") || !data.Contains("cat /proc/version")) {
                if (count < 3) goto RE;
                else return null;
            }

            try {
                fw_bt = data.Split(new string[] { "cat /proc/version" }, StringSplitOptions.None)[1];
                fw_bt = fw_bt.Split(new string[] { "root@vnpt:~#" }, StringSplitOptions.None)[0];
                fw_bt = fw_bt.Split(new string[] { ") )" }, StringSplitOptions.None)[1];
                fw_bt = fw_bt.Replace("\r", "").Replace("\n", "").Replace("root@vnpt:~#", "").Trim();
            }
            catch {
                if (count < 3) goto RE;
                else return null;
            }

            return fw_bt;
        }

        public bool downloadFileFromPc(string file, string pc_ip, int timeout_sec) {
            //login to ONT
            bool r = false;
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return false;

            int count = 0;
            string data = "";
            string cmd = $"tftp -l /tmp/{file} -r {file} -g {pc_ip} 69";
            data = mesh.Read();
            Thread.Sleep(300);
        RE:
            count++;
            r = mesh.Query(cmd, timeout_sec, out data, "root@vnpt:~#");
            if (!r) { if (count < 3) goto RE; }

            data = data.ToLower();
            data = data.Split(new string[] { $"{pc_ip} 69" }, StringSplitOptions.None)[1];
            data = data.Split(new string[] { "root@vnpt:~#" }, StringSplitOptions.None)[0];
            data = data.Replace("\r", "").Replace("\n", "").Trim();
            r = data.Length == 0;
            return r;
        }

        public bool checkFileIntegrity(string file, string md5, int retry_time) {
            //login to ONT
            bool r = false;
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return false;

            int count = 0;
            string data = "";
            string cmd = $"md5sum /tmp/{file}";
            data = mesh.Read();
            Thread.Sleep(300);
        RE:
            count++;
            r = mesh.Query(cmd, 10, out data, "root@vnpt:~#");
            if (!r) { if (count < retry_time) goto RE; }

            data = data.ToLower();
            data = data.Split(new string[] { cmd.ToLower() }, StringSplitOptions.None)[1];
            data = data.Split(new string[] { "root@vnpt:~#" }, StringSplitOptions.None)[0];
            data = data.Replace($"/tmp/{file.ToLower()}", "");
            data = data.Replace("\r", "").Replace("\n", "").Trim();
            Console.WriteLine(data);
            r = data.Equals(md5.ToLower());
            if (!r) { if (count < retry_time) goto RE; }

            return r;
        }

        public bool setChmode(string file, int retry_time) {
            //login to ONT
            bool r = false;
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return false;

            int count = 0;
            string cmd = $"chmod 777 /tmp/{file}";
            string data = "";
        RE:
            count++;
            r = mesh.Query(cmd, 10, out data, "root@vnpt:~#");
            if (!r) { if (count < retry_time) goto RE; }

            data = data.ToLower();
            data = data.Split(new string[] { $"root@vnpt:~# {cmd.ToLower()}" }, StringSplitOptions.None)[1];
            data = data.Split(new string[] { "root@vnpt:~#" }, StringSplitOptions.None)[0];
            data = data.Replace("\r", "").Replace("\n", "").Trim();
            r = data.Length == 0;
            if (!r) { if (count < retry_time) goto RE; }

            return r;
        }

        public bool upgradeFirmware(string file, int time_out) {
            //login to ONT
            bool r = false;
            if (isConnect == false) r = Login(3);
            else r = true;
            if (!r) return false;

            int count = 0;
            string data = "";
            string cmd = $"/tmp/upfw.sh /tmp/{file} 0";

        RE:
            count++;
            r = mesh.Query(cmd, time_out, out data, "upgrade completed");
            if (!r) { if (count < 3) goto RE; }

            return r;
        }

        bool pingToMesh(string ip) {
            System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
            PingOptions options = new PingOptions();
            // Use the default Ttl value which is 128, 
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted. 
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 60;
            try {
                PingReply reply = pingSender.Send(ip, timeout, buffer, options);
                if (reply.Status == IPStatus.Success) {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch {
                return false;
            }
        }

        public void Dispose() {
            if (mesh != null) mesh.Close();
        }

    }

}

using SimpleWifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeshSwitchToLanMode.Function {

    public class WiFiHelper : IDisposable {

        SimpleWifi.Wifi wfi = null;
        IEnumerable<AccessPoint> accessPoints = null;

        public WiFiHelper() {
            wfi = new SimpleWifi.Wifi();
        }

        public bool startConnect(string ssid, string ssid_pass, string ip, int timeout_sec) {
            //scan to find out ssid name
            if (!FindSSID(ssid, timeout_sec)) return false;
            if (!ConnectWiFi(ssid, ssid_pass, ip, timeout_sec)) return false;
            return true;
        }

        public bool FindSSID(string ssid, int timeout_sec) {
            bool r = false;
            int count = 0;
        RE:
            count++;
            r = scanSsidByName(ssid);
            if (!r) {
                if (count < timeout_sec) {
                    Thread.Sleep(1000);
                    goto RE;
                }
            }
            return r;
        }

        public bool ConnectWiFi(string ssid, string ssid_pass, string ip, int timeout_sec) {
            int count = 0;
            bool r = false;

        RE:
            count++;
            r = connectBySSidName(ssid, ssid_pass);
            Thread.Sleep(1000);
            r = isConnected(ip);
            if (!r) {
                if (count < timeout_sec) goto RE;
            }
            myGlobal.myTesting.logSystem += "\n";
            return r;
        }

        bool scanSsidByName(string ssid) {
            try {
                accessPoints = wfi.GetAccessPoints();
                var ap = accessPoints.Where(x => x.Name.ToLower().Contains(ssid.ToLower())).FirstOrDefault();
                foreach (var p in accessPoints) {
                    myGlobal.myTesting.logSystem += p.Name + "\n";
                }
                return (ap != null);
            }
            catch { return false; }
        }

        bool connectBySSidName(string ssid, string password) {
            try {
                bool r = false;
                foreach (var ap in accessPoints) {
                    if (ap.Name.ToLower().Contains(ssid.ToLower())) {
                        AuthRequest authRequest = new AuthRequest(ap);
                        if (authRequest.IsUsernameRequired == true) authRequest.Username = "user";
                        if (authRequest.IsPasswordRequired == true) authRequest.Password = password;
                        ap.Connect(authRequest);
                        r = true;
                        break;
                    }
                }
                return r;
            }
            catch { return false; }
        }

        bool isConnected(string ip) {
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
            wfi.Disconnect();
        }
    }

}

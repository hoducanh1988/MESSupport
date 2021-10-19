using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MeshConfigDhcpServer.Function.Custom;
using SimpleWifi;

namespace MeshConfigDhcpServer.Function.Excute {

    public class wifi {

        public static bool Connect_Wifi(TestingInformation testing, int timeout_miliseconds, int retry_time) {
            int retry = 0;
        STA:
            retry++;
            bool kq = false;
            Thread t = new Thread(new ThreadStart(() => {
                kq = Connect_Wifi(testing);
            }));
            t.IsBackground = true;
            t.Start();

            int count = 0;
            int max = timeout_miliseconds / 100;
        RE:
            count++;
            if (count < max) {
                if (t.IsAlive == true) {
                    Thread.Sleep(100);
                    testing.logSystem += string.Format("...{0}/{1}\r\n", count, max);
                    goto RE;
                }
                else {
                    if (!kq) {
                        if (retry < retry_time) {
                            Thread.Sleep(1000);
                            testing.logSystem += string.Format("...Delay 1000 ms\r\n");
                            testing.logSystem += string.Format("...Retry {0}/{1}\r\n", retry, retry_time);
                            goto STA;
                        }
                    }
                }
            }
            else {
                testing.logSystem += string.Format("...Status = Disconnected\r\n");
                t.Abort();
                if (retry < retry_time) {
                    Thread.Sleep(1000);
                    testing.logSystem += string.Format("...Delay 1000 ms\r\n");
                    testing.logSystem += string.Format("...Retry {0}/{1}\r\n", retry, retry_time);
                    goto STA;
                }
            }

            return kq;
        }


        public static bool Connect_Wifi(TestingInformation testing) {
            try {
                //wifi object
                Wifi wifi = new Wifi();

                //disconnect wifi
                wifi.Disconnect();
                testing.logSystem += string.Format("...Disconnect wifi\r\n");

                //delay
                Thread.Sleep(250);
                testing.logSystem += string.Format("...Delay 250 ms\r\n");

                //get list of access points
                IEnumerable<AccessPoint> accessPoints = wifi.GetAccessPoints();
                testing.logSystem += string.Format("...Get list of access points\r\n");

                //show list access point
                foreach (var ap in accessPoints) {
                    testing.logSystem += string.Format("...............{0}\r\n", ap.Name);
                }

                //for each access point from list
                foreach (var ap in accessPoints) {
                    if (ap.Name.Contains(testing.macAddress.Substring(6, 6).ToLower())) {
                        if (!ap.IsConnected) {

                            //connect if not connected
                            AuthRequest authRequest = new AuthRequest(ap);
                            if (authRequest.IsUsernameRequired == true) authRequest.Username = "user";
                            if (authRequest.IsPasswordRequired == true) authRequest.Password = "EW@" + testing.macAddress.Substring(6, 6).ToLower();
                            testing.logSystem += string.Format("...Connecting to AP wifi\r\n");

                            ap.Connect(authRequest);
                            testing.logSystem += string.Format("...Connected\r\n");
                        }
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex) {
                testing.logSystem += string.Format("...{0}\r\n", ex.ToString());
                return false;
            }

        }

    }
}

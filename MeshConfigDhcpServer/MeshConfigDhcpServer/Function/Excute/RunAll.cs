using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using MeshConfigDhcpServer.Function.Custom;
using MeshConfigDhcpServer.Function.Protocol;

namespace MeshConfigDhcpServer.Function.Excute {

    public class RunAll {

        TestingInformation testing = null;
        SettingInformation setting = null;
        SSH<TestingInformation> ssh = null;
        int c = 0;
        int retry_time = 0;
        int time_out = 30000;
        int wait_time = 0;
        string data = "";

        public RunAll(TestingInformation _testing, SettingInformation _setting) {
            this.testing = _testing;
            this.setting = _setting;
            retry_time = int.Parse(setting.retryTime);
            wait_time = int.Parse(setting.waitReboot);
        }

        public bool Excute() {

            //init control
            testing.init_Value();
            testing.wait_Result();
            testing.progressMax = 9;

            //check mac inputted
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
            if (!check_mac_inputted(1)) goto NG;

            //Connect wifi to AP MESH - retry 3 times
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
            if (!connect_wifi(2)) goto NG;

            //login ssh to mesh via wifi - retry 3 times
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
            if (!login_ssh_via_wifi(setting.meshIP, 3)) goto NG;

            //check firmware version
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
            if (!check_firmware_version(4)) goto NG;

            //check mac ethernet
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
            if (!check_mac_ethernet(5)) goto NG;

            //config dhcp server
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
            if (!config_dhcp_server(6)) goto NG;

            //wait reboot
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
            wait_reboot(7);

            //connect wifi
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
            if (!connect_wifi(8)) goto NG;


            //check ping new ip
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
            if (!ping_to_dhcp_server(30, 9)) goto NG;
            goto OK;

        OK:
            testing.pass_Result();
            close_ssh(true);
            return true;

        NG:
            testing.fail_Result();
            close_ssh(false);
            return false;

        }

        #region sub function

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool check_mac_inputted(int progress) {
            bool ret = false;
            testing.progressValue = progress;
            testing.logSystem += string.Format("\r\n+++++++++++++++++++++++++++++++++++++++++++++++\r\n");
            testing.logSystem += string.Format("Check mac input...{0}\r\n", testing.macInput);
            try {
                ret = UtilityPack.Validation.Parse.IsMacAddress(testing.macInput);
            } catch { ret = false; }
            testing.logSystem += string.Format("...Result = {0}\r\n", ret == true ? "PASS" : "FAIL");
            return ret;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool connect_wifi(int progress) {
            bool ret = false;
            testing.progressValue = progress;
            testing.logSystem += string.Format("\r\n+++++++++++++++++++++++++++++++++++++++++++++++\r\n");
            testing.logSystem += string.Format("Connecting wifi to mesh...\r\n");
            testing.macAddress = testing.macInput.ToUpper();
            ret = wifi.Connect_Wifi(testing, time_out, retry_time);
            testing.logSystem += string.Format("...Result = {0}\r\n", ret == true ? "PASS" : "FAIL");
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip_address"></param>
        /// <returns></returns>
        bool login_ssh_via_wifi(string ip_address, int progress) {
            bool ret = false;
            testing.progressValue = progress;
            testing.logSystem += string.Format("\r\n+++++++++++++++++++++++++++++++++++++++++++++++\r\n");
            testing.logSystem += string.Format("Login ssh to mesh {0},{1},{2} ...\r\n", ip_address, setting.meshUser, setting.meshPass);
            Thread.Sleep(3000);

            c = 0;
        RE_LOGIN:
            c++;
            ret = pingToHost(setting.meshIP);
            if (!ret) {
                if (c < retry_time) {
                    testing.logSystem += string.Format("...[{0}], Result = {1}\r\n", c, ret == true ? "PASS" : "FAIL");
                    Thread.Sleep(1000);
                    goto RE_LOGIN;
                }
                else return false;
            }

            testing.logSystem += "\r\n\r\n";
            ssh = new SSH<TestingInformation>(testing);
            ret = ssh.Login(setting.meshIP, setting.meshUser, setting.meshPass);
            testing.logSystem += string.Format("...[{0}], Result = {1}\r\n", c, ret == true ? "PASS" : "FAIL");
            if (c < retry_time && ret == false) goto RE_LOGIN;

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        bool check_firmware_version(int progress) {
            bool ret = true;
            testing.progressValue = progress;
            testing.logSystem += string.Format("\r\n+++++++++++++++++++++++++++++++++++++++++++++++\r\n");
            testing.logSystem += string.Format("Check firmware version...\r\n");

            c = 0;
        RE_CHECKFIRMWARE:
            c++;
            testing.logSystem += "\r\n\r\n";
            //-------get firmware version
            testing.logSystem += string.Format("...[{0}], Get firmware version from mesh :", c);
            ssh.Write("\n");
            Thread.Sleep(300);
            ssh.WriteLine("cat /etc/fw_info");
            Thread.Sleep(100);
            data = ssh.Read();
            if (data == null || data == "" || data == string.Empty) data = "null";
            string fw_version = data;
            //testing.logSystem += string.Format("......{0}\r\n", data);

            //-------get firmware version in setting
            testing.logSystem += string.Format("\r\n...[{0}], Firmware version in setting :\r\n", c);
            testing.logSystem += string.Format("......{0}\r\n", setting.meshFirmwareVersion);

            //-------compare firmware version
            testing.logSystem += string.Format("...[{0}], Compare firmware version :\r\n", c);
            ret = false;
            try {
                ret = data.ToLower().Replace(":", "").Contains(setting.meshFirmwareVersion.ToLower());
            }
            catch { }
            testing.logSystem += string.Format("......Result = {0}\r\n", ret == true ? "PASS" : "FAIL");
            if (c < retry_time && ret == false) goto RE_CHECKFIRMWARE;

            return ret;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool check_mac_ethernet(int progress) {
            bool ret = false;

            testing.progressValue = progress;
            testing.logSystem += string.Format("\r\n+++++++++++++++++++++++++++++++++++++++++++++++\r\n");
            testing.logSystem += string.Format("Compare mac in mesh with mac inputted...\r\n");

            c = 0;
        RE_CHECKETHERNET:
            c++;
            testing.logSystem += "\r\n\r\n";
            //-------get mac address of ethernet
            testing.logSystem += string.Format("...[{0}], Get mac ethernet from mesh :\r\n", c);
            ssh.Write("\n");
            Thread.Sleep(300);
            ssh.WriteLine("cat /sys/class/net/eth0/address");
            Thread.Sleep(100);
            data = ssh.Read();
            if (data == null || data == "" || data == string.Empty) data = "null";
            string mac_ethernet = data;
            testing.logSystem += string.Format("......{0}\r\n", data);

            //-------get mac address in label on AP
            testing.logSystem += string.Format("...[{0}], Mac inputted :\r\n", c);
            testing.logSystem += string.Format("......{0}\r\n", testing.macInput);

            //-------compare ethernet mac
            testing.logSystem += string.Format("...[0], Compare mac ethernet :\r\n", c);
            ret = false;
            try {
                ret = data.ToLower().Replace(":", "").Contains(testing.macInput.ToLower());
            }
            catch { }
            testing.logSystem += string.Format("......Result = {0}\r\n", ret == true ? "PASS" : "FAIL");
            if (c < retry_time && ret == false) goto RE_CHECKETHERNET;

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool config_dhcp_server(int progress) {
            testing.progressValue = progress;
            testing.logSystem += string.Format("\r\n+++++++++++++++++++++++++++++++++++++++++++++++\r\n");
            testing.logSystem += string.Format("Config mesh to dhcp server...\r\n");

            if (!ssh.Query("uci set network.wan.ifname=eth0.4001", "root@VNPT:~#", 1000)) return false;
            if (!ssh.Query("uci set network.lan.ifname=eth0", "root@VNPT:~#", 1000)) return false;
            if (!ssh.Query(string.Format("uci set network.lan.ipaddr={0}", setting.dhcpIP), "root@VNPT:~#", 1000)) return false;
            if (!ssh.Query("uci commit network", "root@VNPT:~#", 1000)) return false;

            if (!ssh.Query(string.Format("uci set dhcp.lan.start={0}", setting.dhcpStart), "root@VNPT:~#", 1000)) return false;
            if (!ssh.Query(string.Format("uci set dhcp.lan.limit={0}", setting.maxClient), "root@VNPT:~#", 1000)) return false;
            if (!ssh.Query(string.Format("uci set dhcp.lan.leasetime={0}", setting.timeRefresh), "root@VNPT:~#", 1000)) return false;
            if (!ssh.Query("uci commit dhcp", "root@VNPT:~#", 1000)) return false;

            ssh.WriteLine("/etc/init.d/network restart && /etc/init.d/dnsmasq restart");
            Thread.Sleep(1000);

            return true;
        }

        bool wait_reboot(int progress) {
            testing.progressValue = progress;
            testing.logSystem += string.Format("\r\n+++++++++++++++++++++++++++++++++++++++++++++++\r\n");
            testing.logSystem += string.Format("Wait dhcp server restart config...{0}\r\n", wait_time);

            for (int i = 0; i < wait_time; i++) {
                testing.logSystem += string.Format("{0}..", i);
                Thread.Sleep(1000);
            }
            testing.logSystem += string.Format("\r\n");
            return true;
        }

        bool ping_to_dhcp_server(int retry_time, int progress) {
            bool ret = false;
            testing.progressValue = progress;
            testing.logSystem += string.Format("\r\n+++++++++++++++++++++++++++++++++++++++++++++++\r\n");
            testing.logSystem += string.Format("Ping to dhcp server...{0}, timeout={1} sec\r\n", setting.dhcpIP, retry_time);

            c = 0;
        RE:
            c++;
            ret = pingToHost(setting.dhcpIP);
            testing.logSystem += ret ? string.Format("Reply from {0}: bytes=32 time=5ms TTL=64\r\n", setting.dhcpIP) : string.Format("Request timed out.\r\n");
            if (!ret) {
                if (c < retry_time) {
                    Thread.Sleep(1000);
                    goto RE;
                }
            }

            return ret;
        }

        bool pingToHost(string hostAddress) {
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
                PingReply reply = pingSender.Send(hostAddress, timeout, buffer, options);
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


        bool close_ssh(bool ret) {
            testing.logSystem += string.Format("\r\n+++++++++++++++++++++++++++++++++++++++++++++++\r\n");
            testing.logSystem += string.Format("Total result: {0}\r\n", ret ? "Passed" : "Failed");
            testing.logSystem += string.Format("Total time: {0}\r\n", testing.totalTime);
            if (ssh != null) ssh.Close();
            testing.progressValue = testing.progressMax;
            return save_log();
        }

        bool save_log() {
            IO.FileLog log = new IO.FileLog(testing);
            return log.Save();
        }

        #endregion

    }
}

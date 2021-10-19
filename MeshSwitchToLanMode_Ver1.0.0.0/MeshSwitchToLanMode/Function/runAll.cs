using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;

namespace MeshSwitchToLanMode.Function {
    public class runAll {

        meshAP mesh = null;
        WiFiHelper wifi = null;
        string mesh_ip = "";
        string mesh_user = "";
        string mesh_pass = "";
        TestingInformation myTesting = myGlobal.myTesting;

        public runAll() {
            string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
            mesh_ip = lines[0].Split('=')[1];
            mesh_user = lines[1].Split('=')[1];
            mesh_pass = lines[2].Split('=')[1];

            wifi = new WiFiHelper();
            mesh = new meshAP(mesh_ip, mesh_user, mesh_pass);
        }

        public bool Excute(string mac) {
            bool ret = false;
            myGlobal.myTesting.Initial();
            myGlobal.myTesting.waitParam();

            //check mac format
            ret = macIsTrue(mac);
            if (!ret) goto END;

            //find mesh ssid
            ret = findMeshBySsidName(mac);
            if (!ret) goto END;

            //connect mesh ssid
            ret = connectMeshSsid(mac);
            if (!ret) goto END;

            //login ssh
            ret = loginSshToMesh();
            if (!ret) goto END;

            //switch mesh to lan mode
            ret = switchMeshToLanMode();
            if (!ret) goto END;

            //verify after switch
            ret = verifyMeshMode();
            if (!ret) goto END;


            END:
            bool ___ = ret ? myGlobal.myTesting.passParam() : myGlobal.myTesting.failParam();
            if (mesh != null) mesh.Dispose();
            if (wifi != null) wifi.Dispose();
            return ret;
        }

        bool macIsTrue(string mac) {
            Stopwatch st = new Stopwatch();
            st.Start();
            bool r = false;
            myTesting.checkMacFormat = "Waiting...";

            myTesting.logSystem += "\n+++ Check mac format +++++++++++++++++++++++++++\n";
            myTesting.logSystem += string.Format("... Mac inputed = \"{0}\"\n", mac);
            r = mac.Length == 12;
            myTesting.logSystem += string.Format("... Mac length [12] = {0}\n", r ? "Passed" : "Failed");
            if (!r) {
                goto END;
            }

            r = Regex.IsMatch(mac, "^[0-9,A-F]{12}$", RegexOptions.IgnoreCase);
            myTesting.logSystem += string.Format("... Mac format [0-9,A-F] = {0}\n", r ? "Passed" : "Failed");
            if (!r) {
                goto END;
            }

        END:
            myTesting.macAddress = mac;
            myTesting.checkMacFormat = r ? "Passed" : "Failed";
            myTesting.logSystem += string.Format("... Result: {0}\n", myTesting.checkMacFormat);
            st.Stop();
            myTesting.logSystem += string.Format("... Time elapsed: {0} ms\n", st.ElapsedMilliseconds);
            return r;
        }

        bool findMeshBySsidName(string mac) {
            Stopwatch st = new Stopwatch();
            st.Start();
            bool r = false;
            myTesting.findSsid = "Waiting...";

            myTesting.logSystem += "\n+++ Find mesh wifi by ssid name ++++++++++++++++++++\n";
            string ssid = string.Format("EW_{0}", mac.Substring(6,6).ToLower());
            myTesting.logSystem += string.Format("... SSID Name = {0}\n", ssid);
            myTesting.logSystem += string.Format("... \n");
            r = wifi.FindSSID(ssid, 30);

            myTesting.findSsid = r ? "Passed" : "Failed";

            myTesting.logSystem += string.Format("... \n");
            myTesting.logSystem += string.Format("... Result: {0}\n", myTesting.findSsid);
            st.Stop();
            myTesting.logSystem += string.Format("... Time elapsed: {0} ms\n", st.ElapsedMilliseconds);
            return r;
        }

        bool connectMeshSsid(string mac) {
            Stopwatch st = new Stopwatch();
            st.Start();
            bool r = false;
            myTesting.connectSsid = "Waiting...";

            myTesting.logSystem += "\n+++ Connect mesh wifi  ++++++++++++++++++++++++++\n";

            string ssid = string.Format("EW_{0}", mac.Substring(6, 6).ToLower());
            string pass = string.Format("EW@{0}", mac.Substring(6, 6).ToLower());

            myTesting.logSystem += string.Format("... SSID Name = {0}\n", ssid);
            myTesting.logSystem += string.Format("... SSID Pass = {0}\n", pass);

            r = wifi.ConnectWiFi(ssid, pass, mesh_ip, 30);

            myTesting.connectSsid = r ? "Passed" : "Failed";
            myTesting.logSystem += string.Format("... Result: {0}\n", myTesting.connectSsid);
            st.Stop();
            myTesting.logSystem += string.Format("... Time elapsed: {0} ms\n", st.ElapsedMilliseconds);
            return r;
        }

        bool loginSshToMesh() {
            Stopwatch st = new Stopwatch();
            st.Start();
            bool r = false;
            myTesting.loginSsh = "Waiting...";

            myTesting.logSystem += "\n+++ Login ssh to mesh  ++++++++++++++++++++++++++\n";

            r = mesh.Login(5);

            myTesting.loginSsh = r ? "Passed" : "Failed";
            myTesting.logSystem += string.Format("... Result: {0}\n", myTesting.loginSsh);
            st.Stop();
            myTesting.logSystem += string.Format("... Time elapsed: {0} ms\n", st.ElapsedMilliseconds);
            return r;
        }

        bool switchMeshToLanMode() {
            Stopwatch st = new Stopwatch();
            st.Start();
            bool r = false;
            myTesting.switchToLan = "Waiting...";

            myTesting.logSystem += "\n+++ Switch mesh to Lan mode  +++++++++++++++++++++\n";

            r = mesh.switchToLanMode();

            myTesting.switchToLan = r ? "Passed" : "Failed";
            myTesting.logSystem += string.Format("... Result: {0}\n", myTesting.switchToLan);
            st.Stop();
            myTesting.logSystem += string.Format("... Time elapsed: {0} ms\n", st.ElapsedMilliseconds);
            return r;
        }

        bool verifyMeshMode() {
            Stopwatch st = new Stopwatch();
            st.Start();
            bool r = false;
            myTesting.verifyAfterSwitch = "Waiting...";

            myTesting.logSystem += "\n+++ Verify mesh mode after change  +++++++++++++++++\n";

            r = mesh.getMeshMode();

            myTesting.verifyAfterSwitch = r ? "Passed" : "Failed";
            myTesting.logSystem += string.Format("... Result: {0}\n", myTesting.verifyAfterSwitch);
            st.Stop();
            myTesting.logSystem += string.Format("... Time elapsed: {0} ms\n", st.ElapsedMilliseconds);
            return r;
        }


    }
}

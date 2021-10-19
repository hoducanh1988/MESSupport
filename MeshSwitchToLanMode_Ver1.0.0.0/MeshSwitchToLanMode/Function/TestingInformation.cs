using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshSwitchToLanMode.Function {

    public class TestingInformation : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public TestingInformation() {
            Initial();
        }

        public void Initial() {
            macAddress = "";

            checkMacFormat = "-";
            findSsid = "-";
            connectSsid = "-";
            loginSsh = "-";
            switchToLan = "-";
            verifyAfterSwitch = "-";
            totalResult = "-";

            logSsh = "";
            logSystem = "";
        }

        public bool waitParam() {
            totalResult = "Waiting";
            return true;
        }

        public bool failParam() {
            totalResult = "Failed";
            return true;
        }

        public bool passParam() {
            totalResult = "Passed";
            return true;
        }


        string _mac_address;
        public string macAddress {
            get { return _mac_address; }
            set {
                _mac_address = value;
                OnPropertyChanged(nameof(macAddress));
            }
        }
        string _check_mac_format;
        public string checkMacFormat {
            get { return _check_mac_format; }
            set {
                _check_mac_format = value;
                OnPropertyChanged(nameof(checkMacFormat));
            }
        }
        string _find_ssid;
        public string findSsid {
            get { return _find_ssid; }
            set {
                _find_ssid = value;
                OnPropertyChanged(nameof(findSsid));
            }
        }
        string _connect_ssid;
        public string connectSsid {
            get { return _connect_ssid; }
            set {
                _connect_ssid = value;
                OnPropertyChanged(nameof(connectSsid));
            }
        }
        string _login_ssh;
        public string loginSsh {
            get { return _login_ssh; }
            set {
                _login_ssh = value;
                OnPropertyChanged(nameof(loginSsh));
            }
        }
        string _switch_to_lan;
        public string switchToLan {
            get { return _switch_to_lan; }
            set {
                _switch_to_lan = value;
                OnPropertyChanged(nameof(switchToLan));
            }
        }
        string _verify_after_switch;
        public string verifyAfterSwitch {
            get { return _verify_after_switch; }
            set {
                _verify_after_switch = value;
                OnPropertyChanged(nameof(verifyAfterSwitch));
            }
        }
        string _total_result;
        public string totalResult {
            get { return _total_result; }
            set {
                _total_result = value;
                OnPropertyChanged(nameof(totalResult));
            }
        }
        string _log_system;
        public string logSystem {
            get { return _log_system; }
            set {
                _log_system = value;
                OnPropertyChanged(nameof(logSystem));
            }
        }
        string _log_ssh;
        public string logSsh {
            get { return _log_ssh; }
            set {
                _log_ssh = value;
                OnPropertyChanged(nameof(logSsh));
            }
        }
    }
}

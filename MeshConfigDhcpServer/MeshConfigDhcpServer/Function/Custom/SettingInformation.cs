using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshConfigDhcpServer.Function.Custom {

    public class SettingInformation : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public SettingInformation() {

            meshIP = "192.168.88.1";
            meshUser = "root";
            meshPass = "vnpt";
            meshFirmwareVersion = "EW12_02RTM_RC02";

            dhcpIP = "192.168.99.101";
            dhcpStart = "102";
            maxClient = "100";
            timeRefresh = "300";

            retryTime = "3";
            waitReboot = "60";
        }

        #region config mesh
        
        string _mesh_ip;
        public string meshIP {
            get { return _mesh_ip; }
            set { _mesh_ip = value; OnPropertyChanged(nameof(meshIP)); }
        }
        string _mesh_user;
        public string meshUser {
            get { return _mesh_user; }
            set { _mesh_user = value; OnPropertyChanged(nameof(meshUser)); }
        }
        string _mesh_pass;
        public string meshPass {
            get { return _mesh_pass; }
            set { _mesh_pass = value; OnPropertyChanged(nameof(meshPass)); }
        }
        string _mesh_fw_ver;
        public string meshFirmwareVersion {
            get { return _mesh_fw_ver; }
            set { _mesh_fw_ver = value; OnPropertyChanged(nameof(meshFirmwareVersion)); }
        }

        #endregion

        #region config dhcp server

        string _dhcp_ip;
        public string dhcpIP {
            get { return _dhcp_ip; }
            set { _dhcp_ip = value; OnPropertyChanged(nameof(dhcpIP)); }
        }
        string _dhcp_start;
        public string dhcpStart {
            get { return _dhcp_start; }
            set { _dhcp_start = value; OnPropertyChanged(nameof(dhcpStart)); }
        }
        string _max_client;
        public string maxClient {
            get { return _max_client; }
            set { _max_client = value; OnPropertyChanged(nameof(maxClient)); }
        }
        string _time_refresh;
        public string timeRefresh {
            get { return _time_refresh; }
            set { _time_refresh = value; OnPropertyChanged(nameof(timeRefresh)); }
        }

        #endregion

        #region config test

        string _retry_time;
        public string retryTime {
            get { return _retry_time; }
            set { _retry_time = value; OnPropertyChanged(nameof(retryTime)); }
        }
        string _wait_reboot;
        public string waitReboot {
            get { return _wait_reboot; }
            set { _wait_reboot = value; OnPropertyChanged(nameof(waitReboot)); }
        }

        #endregion

    }
}

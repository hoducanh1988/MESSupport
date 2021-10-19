using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshConfigDhcpServer.Function.Custom {

    public class TestingInformation : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public TestingInformation() {
            init_Value();
        }

        public void init_Value() {
            logSystem = "";
            totalTime = "00:00:00";
            totalResult = "--";
            errorMessage = "";
            macAddress = "";
            buttonContent = "Start";
            buttonEnable = true;
            textboxEnable = true;

            progressValue = 0;
            progressMax = 1;
        }

        public bool wait_Result() {
            totalResult = "Waiting...";
            buttonContent = "Stop";
            buttonEnable = false;
            textboxEnable = false;
            return true;
        }

        public bool pass_Result() {
            totalResult = "Passed";
            buttonContent = "Start";
            buttonEnable = true;
            macInput = "";
            textboxEnable = true;
            return true;
        }

        public bool fail_Result() {
            totalResult = "Failed";
            buttonContent = "Start";
            buttonEnable = true;
            macInput = "";
            textboxEnable = true;
            return true;
        }

        string _mac_input;
        public string macInput {
            get { return _mac_input; }
            set { _mac_input = value; OnPropertyChanged(nameof(macInput)); }
        }
        bool _textbox_enable;
        public bool textboxEnable {
            get { return _textbox_enable; }
            set { _textbox_enable = value; OnPropertyChanged(nameof(textboxEnable)); }
        }
        string _button_content;
        public string buttonContent {
            get { return _button_content; }
            set { _button_content = value; OnPropertyChanged(nameof(buttonContent)); }
        }
        bool _button_enable;
        public bool buttonEnable {
            get { return _button_enable; }
            set { _button_enable = value; OnPropertyChanged(nameof(buttonEnable)); }
        }
        string _log_system;
        public string logSystem {
            get { return _log_system; }
            set { _log_system = value; OnPropertyChanged(nameof(logSystem)); }
        }
        string _total_time;
        public string totalTime {
            get { return _total_time; }
            set { _total_time = value; OnPropertyChanged(nameof(totalTime)); }
        }
        string _total_result;
        public string totalResult {
            get { return _total_result; }
            set { _total_result = value; OnPropertyChanged(nameof(totalResult)); }
        }
        string _mac_address;
        public string macAddress {
            get { return _mac_address; }
            set { _mac_address = value; OnPropertyChanged(nameof(macAddress)); }
        }
        int _progress_value;
        public int progressValue {
            get { return _progress_value; }
            set { _progress_value = value; OnPropertyChanged(nameof(progressValue)); }
        }
        int _progress_max;
        public int progressMax {
            get { return _progress_max; }
            set { _progress_max = value; OnPropertyChanged(nameof(progressMax)); }
        }
        string _error_message;
        public string errorMessage {
            get { return _error_message; }
            set { _error_message = value; OnPropertyChanged(nameof(errorMessage)); }
        }
    }
}

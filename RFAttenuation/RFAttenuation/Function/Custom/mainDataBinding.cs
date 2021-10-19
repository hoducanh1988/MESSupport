using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFAttenuation.Function.Custom {
    
    public class mainDataBinding : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
                Properties.Settings.Default.Save();
            }
        }

        public mainDataBinding() {
            stationName = "PHẦN MỀM ĐO SUY HAO RF BẰNG MẠCH CALIBRATION KIT";
            productName = "SẢN PHẨM ES12S";
            appTitle = $"Version: MES001VN0U0001 - Build time: 24/02/2021 10:12 - Copyright of VNPT Technology 2021";
            isMinimize = false;
            isMaximize = false;
            isClose = false;
            Opacity = 1;
        }

        string _apptitle;
        public string appTitle {
            get { return _apptitle; }
            set {
                _apptitle = value;
                OnPropertyChanged(nameof(appTitle));
            }
        }
        string _stationname;
        public string stationName {
            get { return _stationname; }
            set {
                _stationname = value;
                OnPropertyChanged(nameof(stationName));
            }
        }
        string _productname;
        public string productName {
            get { return _productname; }
            set {
                _productname = value;
                OnPropertyChanged(nameof(productName));
            }
        }
        bool _isminimize;
        public bool isMinimize {
            get { return _isminimize; }
            set {
                _isminimize = value;
                OnPropertyChanged(nameof(isMinimize));
            }
        }
        bool _ismaximize;
        public bool isMaximize {
            get { return _ismaximize; }
            set {
                _ismaximize = value;
                OnPropertyChanged(nameof(isMaximize));
            }
        }
        bool _isclose;
        public bool isClose {
            get { return _isclose; }
            set {
                _isclose = value;
                OnPropertyChanged(nameof(isClose));
            }
        }
        double _opacity;
        public double Opacity {
            get { return _opacity; }
            set {
                _opacity = value;
                OnPropertyChanged(nameof(Opacity));
            }
        }
    }
}

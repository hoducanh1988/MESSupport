using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFAttenuation.Function.Custom {

    public class settingDataBinding : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
                Properties.Settings.Default.Save();
            }
        }

        public settingDataBinding() {
            instrumentType = "";
            GPIB = "";
            powerTransmit = "-20";
            NOM = "10";
            torStandard = "0.5";
            torMax = "1.0";
            passRate = "80";
            filePathloss = "";

            portTransmitterKit = "PORT1";
            portReceiverKit1 = "PORT3";
            portReceiverKit2 = "PORT4";

            portTransmitterAtt = "PORT1";
            portReceiverAtt1 = "PORT3";
            portReceiverAtt2 = "PORT4";
            Connector = "0";
        }

        string _instrtype;
        public string instrumentType {
            get { return _instrtype; }
            set {
                _instrtype = value;
                OnPropertyChanged(nameof(instrumentType));
            }
        }
        string _gpib;
        public string GPIB {
            get { return _gpib; }
            set {
                _gpib = value;
                OnPropertyChanged(nameof(GPIB));
            }
        }
        string _powertransmit;
        public string powerTransmit {
            get { return _powertransmit; }
            set {
                _powertransmit = value;
                OnPropertyChanged(nameof(powerTransmit));
            }
        }
        string _nom;
        public string NOM {
            get { return _nom; }
            set {
                _nom = value;
                OnPropertyChanged(nameof(NOM));
            }
        }
        string _torstandard;
        public string torStandard {
            get { return _torstandard; }
            set {
                _torstandard = value;
                OnPropertyChanged(nameof(torStandard));
            }
        }
        string _tormax;
        public string torMax {
            get { return _tormax; }
            set {
                _tormax = value;
                OnPropertyChanged(nameof(torMax));
            }
        }
        string _passrate;
        public string passRate {
            get { return _passrate; }
            set {
                _passrate = value;
                OnPropertyChanged(nameof(passRate));
            }
        }
        string _filepathloss;
        public string filePathloss {
            get { return _filepathloss; }
            set {
                _filepathloss = value;
                OnPropertyChanged(nameof(filePathloss));
            }
        }
        string _porttransmitterkit;
        public string portTransmitterKit {
            get { return _porttransmitterkit; }
            set {
                _porttransmitterkit = value;
                OnPropertyChanged(nameof(portTransmitterKit));
            }
        }
        string _portreceiverkit1;
        public string portReceiverKit1 {
            get { return _portreceiverkit1; }
            set {
                _portreceiverkit1 = value;
                OnPropertyChanged(nameof(portReceiverKit1));
            }
        }
        string _portreceiverkit2;
        public string portReceiverKit2 {
            get { return _portreceiverkit2; }
            set {
                _portreceiverkit2 = value;
                OnPropertyChanged(nameof(portReceiverKit2));
            }
        }
        string _porttransmitteratt;
        public string portTransmitterAtt {
            get { return _porttransmitteratt; }
            set {
                _porttransmitteratt = value;
                OnPropertyChanged(nameof(portTransmitterAtt));
            }
        }
        string _portreceiveratt1;
        public string portReceiverAtt1 {
            get { return _portreceiveratt1; }
            set {
                _portreceiveratt1 = value;
                OnPropertyChanged(nameof(portReceiverAtt1));
            }
        }
        string _portreceiveratt2;
        public string portReceiverAtt2 {
            get { return _portreceiveratt2; }
            set {
                _portreceiveratt2 = value;
                OnPropertyChanged(nameof(portReceiverAtt2));
            }
        }
        string _connector;
        public string Connector {
            get { return _connector; }
            set {
                _connector = value;
                OnPropertyChanged(nameof(Connector));
            }
        }
    }
}

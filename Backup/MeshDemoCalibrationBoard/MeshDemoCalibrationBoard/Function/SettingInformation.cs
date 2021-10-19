using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshDemoCalibrationBoard.Function {

    public class SettingInformation : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
                Properties.Settings.Default.Save();
            }
        }


        public string instrumentType {
            get { return Properties.Settings.Default.instrumentType; }
            set {
                Properties.Settings.Default.instrumentType = value;
                OnPropertyChanged(nameof(instrumentType));
            }
        }
        public string GPIB {
            get { return Properties.Settings.Default.GPIB; }
            set {
                Properties.Settings.Default.GPIB = value;
                OnPropertyChanged(nameof(GPIB));
            }
        }
        public string powerTransmit {
            get { return Properties.Settings.Default.powerTransmit; }
            set {
                Properties.Settings.Default.powerTransmit = value;
                OnPropertyChanged(nameof(powerTransmit));
            }
        }
        public string NOM {
            get { return Properties.Settings.Default.NOM; }
            set {
                Properties.Settings.Default.NOM = value;
                OnPropertyChanged(nameof(NOM));
            }
        }


        public string Frequencies {
            get { return Properties.Settings.Default.Frequencies; }
            set {
                Properties.Settings.Default.Frequencies = value;
                OnPropertyChanged(nameof(Frequencies));
            }
        }


        public string filePathloss {
            get { return Properties.Settings.Default.filePathloss; }
            set {
                Properties.Settings.Default.filePathloss = value;
                OnPropertyChanged(nameof(filePathloss));
            }
        }


    }
}

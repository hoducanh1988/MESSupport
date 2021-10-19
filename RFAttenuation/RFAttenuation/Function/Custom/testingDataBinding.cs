using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFAttenuation.Function.Custom {
    public class testingDataBinding : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
                Properties.Settings.Default.Save();
            }
        }

        public testingDataBinding() {
            Init();
            isMeasureKit = false;
        }

        public void Init() {
            logSystem = "";
            logInstrument = "";
            ID = kit1Result = kit2Result = firstResult = secondResult = thirdResult = fourthResult = fifthResult = sixthResult = seventhResult = eighthResult = ninthResult = tenthResult = totalResult = "--";
            errorRate = "0";
            totalTime = "00:00:00";
            buttonContent = "Start";
            buttonEnable = true;
        }

        public void Wait() {
            ID = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            buttonContent = "Stop";
            buttonEnable = false;
            totalResult = "Waiting...";
        }
        public bool Pass() {
            buttonContent = "Start";
            buttonEnable = true;
            totalResult = "Passed";
            return true;
        }
        public bool Fail() {
            buttonContent = "Start";
            buttonEnable = true;
            totalResult = "Failed";
            return true;
        }

        bool _is_measure_kit;
        public bool isMeasureKit {
            get { return _is_measure_kit; }
            set {
                _is_measure_kit = value;
                OnPropertyChanged(nameof(isMeasureKit));
            }
        }
        string _id;
        public string ID {
            get { return _id; }
            set { 
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }
        string _kit1result;
        public string kit1Result {
            get { return _kit1result; }
            set {
                _kit1result = value;
                OnPropertyChanged(nameof(kit1Result));
            }
        }
        string _kit2result;
        public string kit2Result {
            get { return _kit2result; }
            set {
                _kit2result = value;
                OnPropertyChanged(nameof(kit2Result));
            }
        }
        string _firstresult;
        public string firstResult {
            get { return _firstresult; }
            set {
                _firstresult = value;
                OnPropertyChanged(nameof(firstResult));
            }
        }
        string _secondresult;
        public string secondResult {
            get { return _secondresult; }
            set {
                _secondresult = value;
                OnPropertyChanged(nameof(secondResult));
            }
        }
        string _thirdresult;
        public string thirdResult {
            get { return _thirdresult; }
            set {
                _thirdresult = value;
                OnPropertyChanged(nameof(thirdResult));
            }
        }
        string _fourthresult;
        public string fourthResult {
            get { return _fourthresult; }
            set {
                _fourthresult = value;
                OnPropertyChanged(nameof(fourthResult));
            }
        }
        string _fifthresult;
        public string fifthResult {
            get { return _fifthresult; }
            set {
                _fifthresult = value;
                OnPropertyChanged(nameof(fifthResult));
            }
        }
        string _sixthresult;
        public string sixthResult {
            get { return _sixthresult; }
            set {
                _sixthresult = value;
                OnPropertyChanged(nameof(sixthResult));
            }
        }
        string _seventhresult;
        public string seventhResult {
            get { return _seventhresult; }
            set {
                _seventhresult = value;
                OnPropertyChanged(nameof(seventhResult));
            }
        }
        string _eighthresult;
        public string eighthResult {
            get { return _eighthresult; }
            set {
                _eighthresult = value;
                OnPropertyChanged(nameof(eighthResult));
            }
        }
        string _ninthresult;
        public string ninthResult {
            get { return _ninthresult; }
            set {
                _ninthresult = value;
                OnPropertyChanged(nameof(ninthResult));
            }
        }
        string _tenthresult;
        public string tenthResult {
            get { return _tenthresult; }
            set {
                _tenthresult = value;
                OnPropertyChanged(nameof(tenthResult));
            }
        }
        string _totalresult;
        public string totalResult {
            get { return _totalresult; }
            set {
                _totalresult = value;
                OnPropertyChanged(nameof(totalResult));
            }
        }
        string _totaltime;
        public string totalTime {
            get { return _totaltime; }
            set {
                _totaltime = value;
                OnPropertyChanged(nameof(totalTime));
            }
        }
        string _errorrate;
        public string errorRate {
            get { return _errorrate; }
            set {
                _errorrate = value;
                OnPropertyChanged(nameof(errorRate));
            }
        }
        string _logsystem;
        public string logSystem {
            get { return _logsystem; }
            set {
                _logsystem = value;
                OnPropertyChanged(nameof(logSystem));
            }
        }
        string _loginstrument;
        public string logInstrument {
            get { return _loginstrument; }
            set {
                _loginstrument = value;
                OnPropertyChanged(nameof(logInstrument));
            }
        }
        string _buttoncontent;
        public string buttonContent {
            get { return _buttoncontent; }
            set {
                _buttoncontent = value;
                OnPropertyChanged(nameof(buttonContent));
            }
        }
        bool _buttonenable;
        public bool buttonEnable {
            get { return _buttonenable; }
            set {
                _buttonenable = value;
                OnPropertyChanged(nameof(buttonEnable));
            }
        }
    }
}

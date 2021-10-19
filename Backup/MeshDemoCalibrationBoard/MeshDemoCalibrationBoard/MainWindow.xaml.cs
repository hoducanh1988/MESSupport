using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MeshDemoCalibrationBoard.Function;
using Microsoft.Win32;

namespace MeshDemoCalibrationBoard {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        TestingInformation myTesting = myGlobal.myTesting;
        SettingInformation mySetting = myGlobal.mySetting;
        IInstrument instrument = null;
        volatile bool isScrollViewer = false;

        public MainWindow() {
            InitializeComponent();

            this.cbb_instrtype.ItemsSource = new List<string>() { "MT8870A", "E6640A" };
            bindingData();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (e, s) => {
                if (isScrollViewer)
                    this.scr_system.ScrollToEnd();
            };
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            string b_tag = (string)b.Tag;

            switch (b_tag) {
                case "browser_file_pathloss": {
                        OpenFileDialog dlg = new OpenFileDialog();
                        dlg.Filter = "*.xml|*.xml";
                        dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

                        if (dlg.ShowDialog() == true) {
                            mySetting.filePathloss = dlg.FileName;
                        }
                        break;
                    }
                case "board1": {
                        Thread t = new Thread(new ThreadStart(() => {
                            bool r = measureAttenuation(myTesting.Board1);
                            MessageBox.Show(r ? "Successed." : "Failured");
                        }));
                        t.IsBackground = true;
                        t.Start();
                        break;
                    }
                case "board2": {
                        Thread t = new Thread(new ThreadStart(() => {
                            bool r = measureAttenuation(myTesting.Board2);
                            MessageBox.Show(r ? "Successed." : "Failured");
                        }));
                        t.IsBackground = true;
                        t.Start();
                        break;
                    }
                case "antenna11": {
                        Thread t = new Thread(new ThreadStart(() => {
                            bool r = measureAttenuation(myTesting.Antenna11);
                            MessageBox.Show(r ? "Successed." : "Failured");
                        }));
                        t.IsBackground = true;
                        t.Start();
                        break;
                    }
                case "antenna21": {
                        Thread t = new Thread(new ThreadStart(() => {
                            bool r = measureAttenuation(myTesting.Antenna21);
                            MessageBox.Show(r ? "Successed." : "Failured");
                        }));
                        t.IsBackground = true;
                        t.Start();
                        break;
                    }
                case "antenna12": {
                        Thread t = new Thread(new ThreadStart(() => {
                            bool r = measureAttenuation(myTesting.Antenna12);
                            MessageBox.Show(r ? "Successed." : "Failured");
                        }));
                        t.IsBackground = true;
                        t.Start();
                        break;
                    }
                case "antenna22": {
                        Thread t = new Thread(new ThreadStart(() => {
                            bool r = measureAttenuation(myTesting.Antenna22);
                            MessageBox.Show(r ? "Successed." : "Failured");
                        }));
                        t.IsBackground = true;
                        t.Start();
                        break;
                    }
                case "save_path_loss": {
                        Thread t = new Thread(new ThreadStart(() => {
                            bool r = calculateAttenuation();
                            MessageBox.Show(r ? "Successed." : "Failured");
                        }));
                        t.IsBackground = true;
                        t.Start();
                        break;
                    }
                case "reset": {
                        bool r = MessageBox.Show("Bạn muốn đo lại suy hao từ đầu phải không?\nYes = đồng ý, No = thoát.", "Đo lại suy hao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
                        if (r) {
                            myTesting.Init();
                            cleanDataGrid();
                            bindingData();
                        }
                        break;
                    }
            }
        }

        private void Dg_Viewer_LostFocus(object sender, RoutedEventArgs e) {
            DataGrid dg = sender as DataGrid;
            dg.UnselectAll();
            dg.UnselectAllCells();
        }

        #region Sub function

        void bindingData() {
            this.tab_runall.DataContext = myTesting;
            this.sp_setting.DataContext = mySetting;
            this.wp_board1.DataContext = myTesting.Board1;
            this.wp_board2.DataContext = myTesting.Board2;
            this.wp_antenna11.DataContext = myTesting.Antenna11;
            this.wp_antenna21.DataContext = myTesting.Antenna21;
            this.wp_antenna12.DataContext = myTesting.Antenna12;
            this.wp_antenna22.DataContext = myTesting.Antenna22;
            this.dg_Viewer.ItemsSource = myGlobal.listResult;
            this.tab_setting.DataContext = mySetting;
        }

        void cleanDataGrid() {
            foreach (var item in myGlobal.listResult) {
                item.at1Att = item.at1Board1 = item.at1Board2 = item.at1Dif = item.at2Att = item.at2Board1 = item.at2Board2 = item.at2Dif = item.Board1 = item.Board2 = "";
            }
        }

        bool measureAttenuation(ItemInfo tf) {
            bool ret = false;
            isScrollViewer = true;

            try {
                myTesting.SystemLog += $"\n+++ {tf.Name} ++++++++++++++++++++++++++\n";
                tf.Status = "Waiting...";
                string port_tran = mySetting.instrumentType.Equals("MT8870A") ? "PORT" + tf.portTransmitter : "RFIO" + tf.portTransmitter;
                string port_rev = mySetting.instrumentType.Equals("MT8870A") ? "PORT" + tf.portReceiver : "RFIO" + tf.portReceiver;

                myTesting.SystemLog += $"...Connecting to instrument {mySetting.instrumentType} - address {mySetting.GPIB} = ";
                if (instrument == null) {
                    if (mySetting.instrumentType.Equals("MT8870A")) {
                        instrument = new MT8870A(mySetting.GPIB, mySetting.powerTransmit, port_rev, port_tran);
                    }
                }
                myTesting.SystemLog += $"{instrument.isConnected}\n";
                if (instrument.isConnected == false) goto END;

                foreach (var item in myGlobal.listFrequency) {
                    int count = 0;
                    bool r = false;

                RE:
                    count++;
                    myTesting.SystemLog += $"\n...Frequency {item} - Transmitter {port_tran} - Receiver {port_rev}\n";
                    myTesting.SystemLog += $"Công suất phát: {mySetting.powerTransmit} dBm\n";
                    instrument.config_HT20_TxTest_Transmitter(item);
                    string data = instrument.config_HT20_RxTest_Receiver(item);
                    myTesting.SystemLog += data;

                    //kiem tra dinh dang du lieu doc ve
                    r = data.Contains(",");
                    if (r == false) {
                        if (count < 3) goto RE;
                        else goto END;
                    }
                    string[] buffer = data.Split(',');
                    r = buffer.Length >= 3;
                    if (r == false) {
                        if (count < 3) goto RE;
                        else goto END;
                    }
                    string pwr_str = buffer[2];
                    double pwr_dbl = 0;
                    r = double.TryParse(pwr_str, out pwr_dbl);
                    if (r == false) {
                        if (count < 3) goto RE;
                        else goto END;
                    }


                    myTesting.SystemLog += $"Công suất thu được: {pwr_dbl} dBm\n";
                    double att = double.Parse(mySetting.powerTransmit) - pwr_dbl;
                    myTesting.SystemLog += $"Giá trị suy hao: {att} dBm\n";

                    var itr = myGlobal.listResult.Where(x => x.Freq == item).FirstOrDefault();
                    switch (tf.Name) {
                        case "Calibration board 1": { itr.Board1 = $"{att}"; break; }
                        case "Calibration board 2": { itr.Board2 = $"{att}"; break; }
                        case "Antenna 1 +  Board 1": { itr.at1Board1 = $"{att}"; break; }
                        case "Antenna 2 +  Board 1": { itr.at2Board1 = $"{att}"; break; }
                        case "Antenna 1 +  Board 2": { itr.at1Board2 = $"{att}"; break; }
                        case "Antenna 2 +  Board 2": { itr.at2Board2 = $"{att}"; break; }
                    }
                }

                ret = true;
            }
            catch { ret = false; }


        END:
            isScrollViewer = false;
            tf.Status = ret ? "Passed" : "Failed";
            myTesting.SystemLog += $"Kết quả : {tf.Status}\n";
            myTesting.SystemLog += $"...Disconnect instrument.\n";
            if (instrument != null && instrument.isConnected == true) {
                instrument.Dispose();
                instrument = null;
            }
            return ret;
        }

        bool calculateAttenuation() {
            try {
                foreach (var item in myGlobal.listResult) {
                    double at11 = double.Parse(item.at1Board1) - double.Parse(item.Board1);
                    double at12 = double.Parse(item.at1Board2) - double.Parse(item.Board2);
                    item.at1Dif = Math.Abs(at12 - at11).ToString();
                    item.at1Att = ((at11 + at12) / 2).ToString();

                    double at21 = double.Parse(item.at2Board1) - double.Parse(item.Board1);
                    double at22 = double.Parse(item.at2Board2) - double.Parse(item.Board2);
                    item.at2Dif = Math.Abs(at22 - at21).ToString();
                    item.at2Att = ((at21 + at22) / 2).ToString();
                }
                return true;
            } catch { return false; }
        }

        #endregion

       
    }
}

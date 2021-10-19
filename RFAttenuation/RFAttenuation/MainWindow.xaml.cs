using RFAttenuation.Function.Custom;
using RFAttenuation.Function.Global;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using UtilityPack.IO;

namespace RFAttenuation {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        DispatcherTimer timer_state = null;

        public MainWindow() {
            InitializeComponent();
            this.DataContext = myGlobal.mainContext;
            ControlWindowState();
            LoadCalibInfo();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) 
                this.DragMove();
        }

        private void ControlWindowState() {
            timer_state = new DispatcherTimer();
            timer_state.Interval = TimeSpan.FromMilliseconds(500);
            timer_state.Tick += (s, e) => {
                if (myGlobal.mainContext.isMinimize) {
                    this.WindowState = WindowState.Minimized;
                    myGlobal.mainContext.isMinimize = false;
                }
                if (myGlobal.mainContext.isMaximize) {
                    this.WindowState = this.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
                    myGlobal.mainContext.isMaximize = false;
                }
                if (myGlobal.mainContext.isClose) {
                    this.Close();
                }
            };
            timer_state.Start();
        }

        private void LoadCalibInfo() {
            if (File.Exists("attenuation.dll") == false) return;
            string[] buffer = File.ReadAllLines("attenuation.dll");

            myGlobal.calibInfo.remainFile = buffer[0].Split(',')[0];
            myGlobal.calibInfo.Value = buffer[0].Split(',')[1];
            myGlobal.calibInfo.Product = myGlobal.calibInfo.remainFile.Split('_')[0];

            myGlobal.mainContext.productName = myGlobal.calibInfo.Product;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (timer_state != null) timer_state.Stop();
        }
    }
}

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
using MeshSwitchToLanMode.Function;
using System.Diagnostics;

namespace MeshSwitchToLanMode {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            this.DataContext = myGlobal.myTesting;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                TextBox tb = sender as TextBox;
                string txt = tb.Text;

                Thread t = new Thread(new ThreadStart(() => {
                    Dispatcher.Invoke(new Action(() => { tb.IsEnabled = false; }));
                    Stopwatch st = new Stopwatch();
                    st.Start();

                    var run = new runAll();
                    bool r = run.Excute(txt);

                    st.Stop();
                    myGlobal.myTesting.logSystem += "\n+++ Finish  ++++++++++++++++++++++++++++++++++\n";
                    myGlobal.myTesting.logSystem += string.Format("... Total result: {0}\n", r ? "Passed" : "Failed");
                    myGlobal.myTesting.logSystem += string.Format("... Time elapsed: {0} ms\n", st.ElapsedMilliseconds);
                    var log = new LogFile();
                    log.saveLogSystem(myGlobal.myTesting.logSystem, myGlobal.myTesting.macAddress, myGlobal.myTesting.totalResult);
                    log.saveLogSsh(myGlobal.myTesting.logSsh, myGlobal.myTesting.macAddress, myGlobal.myTesting.totalResult);

                    Dispatcher.Invoke(new Action(() => { tb.Clear(); tb.IsEnabled = true; }));
                }));
                t.IsBackground = true;
                t.Start();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            MenuItem mi = sender as MenuItem;
            string m_tag = (string)mi.Tag;

            switch (m_tag) {
                case "exit": {
                        this.Close();
                        break;
                    }
                case "open_log_folder": {
                        string dir = AppDomain.CurrentDomain.BaseDirectory;
                        Process.Start(dir);
                        break;
                    }
            }

        }
    }
}

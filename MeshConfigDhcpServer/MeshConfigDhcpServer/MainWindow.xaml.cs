using System;
using System.Collections.Generic;
using System.IO;
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
using UtilityPack.IO;
using MeshConfigDhcpServer.Function.Custom;
using MeshConfigDhcpServer.Function.Global;
using MeshConfigDhcpServer.Function.Excute;
using System.Windows.Threading;
using System.Diagnostics;

namespace MeshConfigDhcpServer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        volatile bool flag_counter = false;

        public MainWindow() {

            //
            InitializeComponent();

            //load setting file
            load_setting();

            //load combobox data
            load_combobox();

            //load history
            load_history();

            //binding data
            binding_data();

            //scroll log
            scroll_log();
            
        }


        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            string b_tag = (string)b.Tag;

            switch (b_tag) {

                case "save_setting": {
                        XmlHelper<SettingInformation>.ToXmlFile(myGlobal.mySetting, myGlobal.settingFileFullName); //save setting to xml file
                        MessageBox.Show("Success.", "Save Setting", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }

                case "start": {
                        Thread t = new Thread(new ThreadStart(() => {
                            var run = new RunAll(myGlobal.myTesting, myGlobal.mySetting);
                            flag_counter = true;

                            //count time
                            Thread z = new Thread(new ThreadStart(() => {
                                Stopwatch st = new Stopwatch();
                                st.Start();
                                while (flag_counter) {
                                    Thread.Sleep(100);
                                    myGlobal.myTesting.totalTime = UtilityPack.Converter.myConverter.intToTimeSpan((int)st.ElapsedMilliseconds);
                                }
                            }));
                            z.IsBackground = true;
                            z.Start();

                            //excute
                            run.Excute();

                            //
                            flag_counter = false;
                        }));
                        t.IsBackground = true;
                        t.Start();
                        break;
                    }

                case "go": {
                        try {
                            Process.Start(myGlobal.logFilePath);
                        } catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                        
                        break;
                    }
            }

        }

        #region sub function

        void load_setting() {
            myGlobal.mySetting = File.Exists(myGlobal.settingFileFullName) ? XmlHelper<SettingInformation>.FromXmlFile(myGlobal.settingFileFullName) : new SettingInformation();
        }

        void binding_data() {
            this.tab_setting.DataContext = myGlobal.mySetting;
            this.tab_runall.DataContext = myGlobal.myTesting;
            this.GridAbout.ItemsSource = listHist;
        }

        void scroll_log() {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (s, e) => {
                if (myGlobal.myTesting.totalResult.ToLower().Contains("waiting")) {
                    this.scr_logsystem.ScrollToEnd();
                }
            };
            timer.Start();
        }

        void load_combobox() {
            this.cbb_logtype.ItemsSource = new List<string>() { "logSystem" };
        }

        private class history {
            public string ID { get; set; }
            public string VERSION { get; set; }
            public string CONTENT { get; set; }
            public string DATE { get; set; }
            public string CHANGETYPE { get; set; }
            public string PERSON { get; set; }
        }
        List<history> listHist = new List<history>();

        void load_history() {

            listHist.Add(new history() {
                ID = "1",
                VERSION = "1.0.0.0",
                CONTENT = "- Xây dựng tool tự động cấu hình sản phẩm mesh làm dhcp server qua wifi.",
                DATE = "03/06/2020",
                CHANGETYPE = "Tạo mới",
                PERSON = "Hồ Đức Anh"
            });

        }

        #endregion
    }
}

using RFAttenuation.Function.Global;
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
using RFAttenuation.SubWind;
using RFAttenuation.Function.IO;
using RFAttenuation.Function.Excute;
using System.Windows.Threading;
using UtilityPack.Converter;
using System.IO;

namespace RFAttenuation.UserCtrl {
    /// <summary>
    /// Interaction logic for ucRunAll.xaml
    /// </summary>
    public partial class ucRunAll : UserControl {
        DispatcherTimer timer = null;
        int time_count = 0;

        public ucRunAll() {
            InitializeComponent();
            this.DataContext = myGlobal.testingContext;

            timer = new DispatcherTimer();
            int delay_time = 500;
            timer.Interval = TimeSpan.FromMilliseconds(delay_time);
            timer.Tick += (s, e) => {
                time_count++;
                myGlobal.testingContext.totalTime = myConverter.intToTimeSpan(time_count * delay_time);
                this.scrl_logsystem.ScrollToEnd();
                this.scrl_loginstrument.ScrollToEnd();
                this.scrl_logtable.ScrollToEnd();
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.sp_table.Children.Clear();
            Thread t = new Thread(new ThreadStart(() => {
                timer.Start();
                myGlobal.testingContext.Init();
                measureAttenuation ma = new measureAttenuation();
                bool r = ma.Excute(sp_table);
                bool ___ = r ? myGlobal.testingContext.Pass() : myGlobal.testingContext.Fail();
                
                //save log file
                var lf = new logFile();
                lf.ToTXTFile();

                //save file pathloss
                if (r) {
                    var BH0 = new pathLossHelper.pathItem() { PathName = "BH0_LP", PathID = "0", AdditionalLoss = "0", Cal_Date = "7/2/2013 3:55 PM", TesterID = "CNALLURI07291", CalCable_PathName = "NA", UseCalCable = "false" };
                    var BH1 = new pathLossHelper.pathItem() { PathName = "BH1_LP", PathID = "0", AdditionalLoss = "0", Cal_Date = "7/2/2015 3:55 PM", TesterID = "CNALLURI07291", CalCable_PathName = "NA", UseCalCable = "false" };

                    foreach (var item in myGlobal.listFinalResult) {
                        BH0.dataItems.Add(new pathLossHelper.dataItem() { Frequency = item.Frequency, Delta = "0", Value = item.AT1AVG, });
                        BH1.dataItems.Add(new pathLossHelper.dataItem() { Frequency = item.Frequency, Delta = "0", Value = item.AT2AVG, });
                    }

                    pathLossHelper plh = new pathLossHelper(myGlobal.settingContext.filePathloss);
                    plh.ToXML(new List<pathLossHelper.pathItem>() { BH0, BH1 });
                }

                //save remain file
                File.WriteAllText(myGlobal.calibInfo.remainFile, myGlobal.calibInfo.Value);

                timer.Stop();
            }));
            t.IsBackground = true;
            t.Start();
        }
    }
}

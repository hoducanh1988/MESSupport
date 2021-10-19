using System;
using System.Collections.Generic;
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
using System.Diagnostics;
using System.IO;
using RFAttenuation.Function.Global;

namespace RFAttenuation.UserCtrl {
    /// <summary>
    /// Interaction logic for ucLog.xaml
    /// </summary>
    public partial class ucLog : UserControl {

        public ucLog() {
            InitializeComponent();
            this.cbb_datatype.ItemsSource = new List<string>() { "Log Data", "File Pathloss", "File Setting" };
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            try {
                if (cbb_datatype.SelectedValue == null) return;
                string data_type = cbb_datatype.SelectedValue.ToString();
                switch (data_type.ToLower()) {
                    case "log data": {
                            string logdir = $"{AppDomain.CurrentDomain.BaseDirectory}\\Log\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                            if (Directory.Exists(logdir) == false) Directory.CreateDirectory(logdir);
                            Process.Start(logdir);
                            break; 
                        }
                    case "file pathloss": { 
                            if (File.Exists(myGlobal.settingContext.filePathloss) == true) {
                                Process.Start(myGlobal.settingContext.filePathloss);
                            }
                            break; 
                        }
                    case "file setting": { 
                            if (File.Exists(myGlobal.settingFileFullName) == true) {
                                Process.Start(myGlobal.settingFileFullName);
                            }
                            break; 
                        }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

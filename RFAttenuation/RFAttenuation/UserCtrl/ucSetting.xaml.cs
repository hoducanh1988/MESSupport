using Microsoft.Win32;
using RFAttenuation.Function.Custom;
using RFAttenuation.Function.Global;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using UtilityPack.IO;

namespace RFAttenuation.UserCtrl {
    /// <summary>
    /// Interaction logic for ucSetting.xaml
    /// </summary>
    public partial class ucSetting : UserControl {
        public ucSetting() {
            InitializeComponent();
            if (File.Exists(myGlobal.settingFileFullName)) myGlobal.settingContext = XmlHelper<settingDataBinding>.FromXmlFile(myGlobal.settingFileFullName);
            else myGlobal.settingContext = new settingDataBinding();
            this.cbb_instrtype.ItemsSource = new List<string>() { "MT8870A", "E6640A" };
            this.DataContext = myGlobal.settingContext;
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
                            myGlobal.settingContext.filePathloss = dlg.FileName;
                        }
                        break;
                    }
                case "save_setting": {
                        XmlHelper<settingDataBinding>.ToXmlFile(myGlobal.settingContext, myGlobal.settingFileFullName); //save setting to xml file
                        MessageBox.Show("Success.", "Save Setting", MessageBoxButton.OK, MessageBoxImage.Information);
                        break; 
                    }
            }
        }
    }
}

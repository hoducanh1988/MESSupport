using RFAttenuation.Function.Custom;
using RFAttenuation.Function.Excute;
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
using UtilityPack.IO;

namespace RFAttenuation.UserCtrl {
    /// <summary>
    /// Interaction logic for ucKit1.xaml
    /// </summary>
    public partial class ucKit : UserControl {

        public bool isAbort = false;
        public bool isStart = false;

        public ucKit(string kit_name) {
            InitializeComponent();
            addComboboxItemSource();
            this.lbl_KitName.Content = kit_name;
            this.DataContext = myGlobal.settingContext;
        }

        private void addComboboxItemSource() {
            foreach (var wp in this.sp_kit.Children) {
                if (wp is WrapPanel) {
                    foreach (var cbb in (wp as WrapPanel).Children) {
                        if (cbb is ComboBox) {
                            if ((cbb as ComboBox).Tag.Equals("port") == true) {
                                (cbb as ComboBox).ItemsSource = new List<string>() { "PORT1", "PORT2", "PORT3", "PORT4" };
                            }
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            switch ((sender as Button).Content.ToString().ToLower()) {
                case "start": { 
                        isStart = true;
                        XmlHelper<settingDataBinding>.ToXmlFile(myGlobal.settingContext, myGlobal.settingFileFullName); //save setting to xml file
                        break; 
                    }
                case "abort": { isAbort = true; break; }
            }
        }
    }
}

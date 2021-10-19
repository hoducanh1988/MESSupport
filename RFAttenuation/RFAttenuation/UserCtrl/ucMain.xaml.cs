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
using System.IO;
using RFAttenuation.Function.Global;

namespace RFAttenuation.UserCtrl {
    /// <summary>
    /// Interaction logic for ucMain.xaml
    /// </summary>
    public partial class ucMain : UserControl {

        List<Label> listLabel = null;

        ucAbout mainAbout = new ucAbout();
        ucHelp mainHelp = new ucHelp();
        ucLog mainLog = new ucLog();
        ucSetting mainSetting = new ucSetting();
        ucRunAll mainRunAll = new ucRunAll();


        public ucMain() {
            InitializeComponent();
            listLabel = new List<Label>() { label_runall, label_setting, label_log, label_help, label_about };
            AddControl(sp_suyhao);
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                AddControl(sender as StackPanel);
            }
        }

        private void AddControl(StackPanel sp) {
            foreach (var l in listLabel) l.FontWeight = FontWeights.Normal;
            foreach (var l in sp.Children) {
                if (l is Label) {
                    (l as Label).FontWeight = FontWeights.Bold;
                    this.grid_content.Children.Clear();
                    switch ((l as Label).Name.ToString()) {
                        case "label_runall": { this.grid_content.Children.Add(mainRunAll); break; }
                        case "label_setting": { this.grid_content.Children.Add(mainSetting); break; }
                        case "label_log": { this.grid_content.Children.Add(mainLog); break; }
                        case "label_help": { this.grid_content.Children.Add(mainHelp); break; }
                        case "label_about": { this.grid_content.Children.Add(mainAbout); break; }
                    }
                }
            }
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e) {
            this.grid_main.ColumnDefinitions[0].Width = new GridLength(250, GridUnitType.Pixel);
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e) {
            this.grid_main.ColumnDefinitions[0].Width = new GridLength(50, GridUnitType.Pixel);
        }
    }
}

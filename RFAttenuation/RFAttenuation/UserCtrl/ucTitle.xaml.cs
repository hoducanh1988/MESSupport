using RFAttenuation.Function.Global;
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

namespace RFAttenuation.UserCtrl {
    /// <summary>
    /// Interaction logic for ucTitle.xaml
    /// </summary>
    public partial class ucTitle : UserControl {
        public ucTitle() {
            InitializeComponent();
            this.DataContext = myGlobal.mainContext;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) {
            Border bd = sender as Border;
            string bd_tag = (string)bd.Tag;

            switch (bd_tag) {
                case "minimize": { myGlobal.mainContext.isMinimize = true; break; }
                case "maximize": { myGlobal.mainContext.isMaximize = true; break; }
                case "close": { myGlobal.mainContext.isClose = true; break; }
            }
        }
    }
}

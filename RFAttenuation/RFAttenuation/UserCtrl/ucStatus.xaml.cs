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
    /// Interaction logic for ucStatus.xaml
    /// </summary>
    public partial class ucStatus : UserControl {
        public ucStatus() {
            InitializeComponent();
            this.DataContext = myGlobal.mainContext;
        }
    }
}

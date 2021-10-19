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
using MeshSwitchToLanMode.Function;

namespace MeshSwitchToLanMode.UserCtrl {
    /// <summary>
    /// Interaction logic for ucLogItem.xaml
    /// </summary>
    public partial class ucLogItem : UserControl {
        public ucLogItem() {
            InitializeComponent();
            this.DataContext = myGlobal.myTesting;
        }
    }
}

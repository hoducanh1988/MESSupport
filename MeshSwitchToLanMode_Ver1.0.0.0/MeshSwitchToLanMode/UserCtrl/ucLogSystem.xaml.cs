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
using System.Windows.Threading;
using MeshSwitchToLanMode.Function;

namespace MeshSwitchToLanMode.UserCtrl {
    /// <summary>
    /// Interaction logic for ucLogSystem.xaml
    /// </summary>
    public partial class ucLogSystem : UserControl {
        public ucLogSystem() {
            InitializeComponent();
            this.DataContext = myGlobal.myTesting;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (s, e) => {
                if (myGlobal.myTesting.totalResult == "Waiting")
                    Scr_LogSystem.ScrollToEnd();
            };
            timer.Start();
        }
    }
}

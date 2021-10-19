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
using System.Windows.Shapes;
using RFAttenuation.Function.Global;

namespace RFAttenuation.SubWind {
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window {
        public ResultWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            string b_content = (string)b.Content;

            switch (b_content.ToLower()) {
                case "retry": {
                        myGlobal.isRetry = true;
                        break;
                    }
                case "next": {
                        myGlobal.isRetry = false;
                        break;
                    }
            }
            this.Close();

        }
    }
}

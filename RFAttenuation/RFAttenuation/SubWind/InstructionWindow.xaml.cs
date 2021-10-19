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
using RFAttenuation.UserCtrl;
using RFAttenuation.Function.Global;
using System.Windows.Threading;

namespace RFAttenuation.SubWind {
    /// <summary>
    /// Interaction logic for InstructionWindow.xaml
    /// </summary>
    public partial class InstructionWindow : Window {

        DispatcherTimer timer = null;
        public bool isStart = false;
        public bool isAbort = false;

        public InstructionWindow(myParameter.StepMeasurement sm) {
            InitializeComponent();
            this.grid_main.Children.Clear();
            switch (sm) {
                case myParameter.StepMeasurement.Kit1: {
                        ucKit kit1 = new ucKit("Kit 1");
                        this.grid_main.Children.Add(kit1);
                        break; 
                    }
                case myParameter.StepMeasurement.Kit2: {
                        ucKit kit2 = new ucKit("Kit 2");
                        this.grid_main.Children.Add(kit2);
                        break;
                    }
                case myParameter.StepMeasurement.First: {
                        ucAtt att = new ucAtt("lần 1");
                        this.grid_main.Children.Add(att);
                        break;
                    }
                case myParameter.StepMeasurement.Second: {
                        ucAtt att = new ucAtt("lần 2");
                        this.grid_main.Children.Add(att);
                        break;
                    }
                case myParameter.StepMeasurement.Third: {
                        ucAtt att = new ucAtt("lần 3");
                        this.grid_main.Children.Add(att);
                        break;
                    }
                case myParameter.StepMeasurement.Fourth: {
                        ucAtt att = new ucAtt("lần 4");
                        this.grid_main.Children.Add(att);
                        break;
                    }
                case myParameter.StepMeasurement.Fifth: {
                        ucAtt att = new ucAtt("lần 5");
                        this.grid_main.Children.Add(att);
                        break;
                    }
                case myParameter.StepMeasurement.Sixth: {
                        ucAtt att = new ucAtt("lần 6");
                        this.grid_main.Children.Add(att);
                        break;
                    }
                case myParameter.StepMeasurement.Seventh: {
                        ucAtt att = new ucAtt("lần 7");
                        this.grid_main.Children.Add(att);
                        break;
                    }
                case myParameter.StepMeasurement.Eighth: {
                        ucAtt att = new ucAtt("lần 8");
                        this.grid_main.Children.Add(att);
                        break;
                    }
                case myParameter.StepMeasurement.Ninth: {
                        ucAtt att = new ucAtt("lần 9");
                        this.grid_main.Children.Add(att);
                        break;
                    }
                case myParameter.StepMeasurement.Tenth: {
                        ucAtt att = new ucAtt("lần 10");
                        this.grid_main.Children.Add(att);
                        break;
                    }

            }


            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += (s, e) => {
                foreach (var c in this.grid_main.Children) {
                    if (c is ucKit) {
                        if ((c as ucKit).isStart == true || (c as ucKit).isAbort == true) {
                            isStart = (c as ucKit).isStart;
                            isAbort = (c as ucKit).isAbort;
                            this.Close();
                        }
                    }
                    if (c is ucAtt) {
                        if ((c as ucAtt).isStart == true || (c as ucAtt).isAbort == true) {
                            isStart = (c as ucAtt).isStart;
                            isAbort = (c as ucAtt).isAbort;
                            this.Close();
                        }
                    }
                }
            };
            timer.Start();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                switch ((sender as Border).Tag) {
                    case "move": { this.DragMove();break; }
                    case "close": { this.Close(); break; }
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            timer.Stop();
        }
    
    }
}

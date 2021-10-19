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
using System.Windows.Xps.Packaging;

namespace RFAttenuation.UserCtrl {
    /// <summary>
    /// Interaction logic for ucHelp.xaml
    /// </summary>
    public partial class ucHelp : UserControl {
        public ucHelp() {
            InitializeComponent();

            if (System.IO.File.Exists(myGlobal.helpFileFullName)) {
                XpsDocument xpsDocument = new XpsDocument(myGlobal.helpFileFullName, System.IO.FileAccess.Read);
                FixedDocumentSequence fds = xpsDocument.GetFixedDocumentSequence();
                docViewer.Document = fds;
            }
        }
    }
}

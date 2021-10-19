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
    /// Interaction logic for ucAbout.xaml
    /// </summary>
    public partial class ucAbout : UserControl {

        private class history {
            public string ID { get; set; }
            public string VERSION { get; set; }
            public string CONTENT { get; set; }
            public string DATE { get; set; }
            public string TYPE { get; set; }
            public string PERSON { get; set; }
        }
        List<history> listHist = new List<history>();


        public ucAbout() {
            InitializeComponent();

            listHist.Add(new history() {
                ID = "1",
                VERSION = "MES001VN0U0001",
                CONTENT = "- Xây dựng phần mềm đo suy hao RF trạm calib wlan sử dụng mạch calibration kit.\n" + 
                          "- Update chức năng hiển thị kết quả sau khi đo xong.\n" + 
                          "- Update chức năng retry cho từng hạng mục.\n" + 
                          "- Update chức năng không cần đo lại suy hao kit ở những lần tiếp theo.\n" + 
                          "- Update chức năng hiển thị bôi màu đỏ với những giá trị suy hao ngoài ngưỡng.",
                DATE = "24/02/2021",
                TYPE = "Tạo mới",
                PERSON = "Hồ Đức Anh"
            });

            this.GridAbout.ItemsSource = listHist;
        }
    }
}

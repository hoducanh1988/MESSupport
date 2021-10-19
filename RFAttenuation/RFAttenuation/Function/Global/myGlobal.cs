using RFAttenuation.Function.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFAttenuation.Function.Global {
    public class myGlobal {

        public static string settingFileFullName = string.Format("{0}setting.xml", AppDomain.CurrentDomain.BaseDirectory);
        public static string helpFileFullName = string.Format("{0}help.xps", AppDomain.CurrentDomain.BaseDirectory);
        public static inputCalibInfo calibInfo = new inputCalibInfo();

        public static mainDataBinding mainContext = new mainDataBinding();
        public static settingDataBinding settingContext = null;
        public static testingDataBinding testingContext = new testingDataBinding();

        public static List<kitItemResult> listKit1Result = new List<kitItemResult>();
        public static List<kitItemResult> listKit2Result = new List<kitItemResult>();

        public static List<attItemResult> listAtt1Result = new List<attItemResult>();
        public static List<attItemResult> listAtt2Result = new List<attItemResult>();
        public static List<attItemResult> listAtt3Result = new List<attItemResult>();
        public static List<attItemResult> listAtt4Result = new List<attItemResult>();
        public static List<attItemResult> listAtt5Result = new List<attItemResult>();
        public static List<attItemResult> listAtt6Result = new List<attItemResult>();
        public static List<attItemResult> listAtt7Result = new List<attItemResult>();
        public static List<attItemResult> listAtt8Result = new List<attItemResult>();
        public static List<attItemResult> listAtt9Result = new List<attItemResult>();
        public static List<attItemResult> listAtt10Result = new List<attItemResult>();

        public static List<finalItemResult> listFinalResult = new List<finalItemResult>();

        public static bool isRetry = false;

    }
}

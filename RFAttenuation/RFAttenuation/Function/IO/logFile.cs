using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using RFAttenuation.Function.Global;
using RFAttenuation.Function.Custom;

namespace RFAttenuation.Function.IO {
    public class logFile {

        private string logdir = $"{AppDomain.CurrentDomain.BaseDirectory}\\Log\\{DateTime.Now.ToString("yyyy-MM-dd")}";

        public logFile() {
            if (Directory.Exists(logdir) == false) Directory.CreateDirectory(logdir);
        }

        public bool ToTXTFile() {
            try {
                string log_file = $"{logdir}\\{myGlobal.testingContext.ID}.txt";
                using (var sw = new StreamWriter(log_file, true, Encoding.Unicode)) {
                    sw.WriteLine(this.appInfoToString());
                    sw.WriteLine(this.settingToString());
                    sw.WriteLine(this.logItemToString());
                    sw.WriteLine(this.logTableToString());
                    sw.WriteLine(this.logSystemToString());
                    sw.WriteLine(this.logInstrumentToString());
                }
                return true;
            }
            catch { return false; }
        }

        private string appInfoToString() {
            string data = "> Thông tin app info: \n";
            data += "------------------------------------------------------------------\n";
            data += myGlobal.mainContext.stationName + "\n";
            data += myGlobal.mainContext.productName + "\n";
            data += myGlobal.mainContext.appTitle + "\n";
            return data;
        }

        private string settingToString() {
            string data = "> Thông tin setting phần mềm: \n";
            data += "------------------------------------------------------------------\n";
            PropertyInfo[] protys = myGlobal.settingContext.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var pr in protys) {
                data += $"{pr.Name}={pr.GetValue(myGlobal.settingContext, null)}\n";
            }
            return data;
        }

        private string logItemToString() {
            string data = "> Thông tin log item: \n";
            data += "------------------------------------------------------------------\n";
            data += $"ID: {myGlobal.testingContext.ID}\n";
            data += $"{"Đo suy hao Kit 1".PadRight(30, ' ')}{myGlobal.testingContext.kit1Result.PadRight(10, ' ')}\n";
            data += $"{"Đo suy hao Kit 2".PadRight(30, ' ')}{myGlobal.testingContext.kit2Result.PadRight(10, ' ')}\n";
            data += $"{"Đo suy hao trạm lần 1".PadRight(30, ' ')}{myGlobal.testingContext.firstResult.PadRight(10, ' ')}\n";
            data += $"{"Đo suy hao trạm lần 2".PadRight(30, ' ')}{myGlobal.testingContext.secondResult.PadRight(10, ' ')}\n";
            data += $"{"Đo suy hao trạm lần 3".PadRight(30, ' ')}{myGlobal.testingContext.thirdResult.PadRight(10, ' ')}\n";
            data += $"{"Đo suy hao trạm lần 4".PadRight(30, ' ')}{myGlobal.testingContext.fourthResult.PadRight(10, ' ')}\n";
            data += $"{"Đo suy hao trạm lần 5".PadRight(30, ' ')}{myGlobal.testingContext.fifthResult.PadRight(10, ' ')}\n";
            data += $"{"Đo suy hao trạm lần 6".PadRight(30, ' ')}{myGlobal.testingContext.sixthResult.PadRight(10, ' ')}\n";
            data += $"{"Đo suy hao trạm lần 7".PadRight(30, ' ')}{myGlobal.testingContext.seventhResult.PadRight(10, ' ')}\n";
            data += $"{"Đo suy hao trạm lần 8".PadRight(30, ' ')}{myGlobal.testingContext.eighthResult.PadRight(10, ' ')}\n";
            data += $"{"Đo suy hao trạm lần 9".PadRight(30, ' ')}{myGlobal.testingContext.ninthResult.PadRight(10, ' ')}\n";
            data += $"{"Đo suy hao trạm lần 10".PadRight(30, ' ')}{myGlobal.testingContext.tenthResult.PadRight(10, ' ')}\n";
            data += $"Error rate: {myGlobal.testingContext.errorRate}\n";
            data += $"Total time: {myGlobal.testingContext.totalTime}\n";
            data += $"Total result: {myGlobal.testingContext.totalResult}\n";
            return data;
        }

        private string logTableToString() {
            string data = "> Thông tin log table: \n";
            data += "------------------------------------------------------------------\n";
            List<List<kitItemResult>> listKit = new List<List<kitItemResult>>() { myGlobal.listKit1Result, myGlobal.listKit2Result };
            List<List<attItemResult>> listAtt = new List<List<attItemResult>>() { myGlobal.listAtt1Result, myGlobal.listAtt2Result, myGlobal.listAtt3Result, myGlobal.listAtt4Result, myGlobal.listAtt5Result, myGlobal.listAtt6Result, myGlobal.listAtt7Result, myGlobal.listAtt8Result, myGlobal.listAtt9Result, myGlobal.listAtt10Result };
            int idx = 0;
            foreach (var lis in listKit) {
                idx++;
                if (lis.Count > 0) {
                    data += $"ĐO SUY HAO KIT {idx}:\n";
                    data += $"{"Frequency".PadLeft(20, ' ')}{"Power Transmit".PadLeft(20, ' ')}{"Power1".PadLeft(20, ' ')}{"Power2".PadLeft(20, ' ')}{"Power3".PadLeft(20, ' ')}{"AVG".PadLeft(20, ' ')}{"Connector".PadLeft(20, ' ')}{"Attenuation".PadLeft(20, ' ')}\n";
                    foreach (var item in lis) {
                        data += $"{item.ToString()}\n";
                    }
                }
            }
            idx = 0;
            foreach (var lis in listAtt) {
                idx++;
                if (lis.Count > 0) {
                    data += $"ĐO SUY HAO TRẠM LẦN THỨ {idx}:\n";
                    data += $"{"Frequency".PadLeft(20, ' ')}" +
                            $"{"Power Transmit".PadLeft(20, ' ')}" +
                            $"{"Connector".PadLeft(20, ' ')}" +
                            $"{"Antenna1Kit1_Power1".PadLeft(20, ' ')}" +
                            $"{"Antenna1Kit1_Power2".PadLeft(20, ' ')}" +
                            $"{"Antenna1Kit1_Power3".PadLeft(20, ' ')}" +
                            $"{"Antenna1Kit1_AVG".PadLeft(20, ' ')}" +
                            $"{"Kit1Power".PadLeft(20, ' ')}" +
                            $"{"Antenna2Kit2_Power1".PadLeft(20, ' ')}" +
                            $"{"Antenna2Kit2_Power2".PadLeft(20, ' ')}" +
                            $"{"Antenna2Kit2_Power3".PadLeft(20, ' ')}" +
                            $"{"Antenna2Kit2_AVG".PadLeft(20, ' ')}" +
                            $"{"Kit2Power".PadLeft(20, ' ')}" +
                            $"{"Antenna2".PadLeft(20, ' ')}\n";

                    foreach (var item in lis) {
                        data += $"{item.ToString()}\n";
                    }
                }
            }

            data += "GIÁ TRỊ SUY HAO TRUNG BÌNH:\n";
            data += $"{"Frequency".PadLeft(20, ' ')}" +
                    $"{"Antenna1AVG".PadLeft(20, ' ')}" +
                    $"{"Antenna1Power1".PadLeft(20, ' ')}" +
                    $"{"Antenna1Power2".PadLeft(20, ' ')}" +
                    $"{"Antenna1Power3".PadLeft(20, ' ')}" +
                    $"{"Antenna1Power4".PadLeft(20, ' ')}" +
                    $"{"Antenna1Power5".PadLeft(20, ' ')}" +
                    $"{"Antenna1Power6".PadLeft(20, ' ')}" +
                    $"{"Antenna1Power7".PadLeft(20, ' ')}" +
                    $"{"Antenna1Power8".PadLeft(20, ' ')}" +
                    $"{"Antenna1Power9".PadLeft(20, ' ')}" +
                    $"{"Antenna1Power10".PadLeft(20, ' ')}" +
                    $"{"Antenna2AVG".PadLeft(20, ' ')}" +
                    $"{"Antenna2Power1".PadLeft(20, ' ')}" +
                    $"{"Antenna2Power2".PadLeft(20, ' ')}" +
                    $"{"Antenna2Power3".PadLeft(20, ' ')}" +
                    $"{"Antenna2Power4".PadLeft(20, ' ')}" +
                    $"{"Antenna2Power5".PadLeft(20, ' ')}" +
                    $"{"Antenna2Power6".PadLeft(20, ' ')}" +
                    $"{"Antenna2Power7".PadLeft(20, ' ')}" +
                    $"{"Antenna2Power8".PadLeft(20, ' ')}" +
                    $"{"Antenna2Power9".PadLeft(20, ' ')}" +
                    $"{"Antenna2Power10".PadLeft(20, ' ')}\n";

            foreach (var item in myGlobal.listFinalResult) {
                data += $"{item.ToString()}\n";
            }
            return data;
        }

        private string logSystemToString() {
            string data = "> Thông tin log system: \n";
            data += "------------------------------------------------------------------\n";
            data += $"{myGlobal.testingContext.logSystem}\n";
            return data;
        }

        private string logInstrumentToString() {
            string data = "> Thông tin log instrument: \n";
            data += "------------------------------------------------------------------\n";
            data += $"{myGlobal.testingContext.logInstrument}\n";
            return data;
        }

    }
}

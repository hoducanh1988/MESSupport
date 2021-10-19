using RFAttenuation.Function.Custom;
using RFAttenuation.Function.Global;
using RFAttenuation.Function.Instrument;
using RFAttenuation.Function.IO;
using RFAttenuation.SubWind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;
using System.IO;

namespace RFAttenuation.Function.Excute {
    public class measureAttenuation {

        List<pathLossHelper.dataItem> listChannel = null;
        settingDataBinding mySetting = myGlobal.settingContext;
        testingDataBinding myTesting = myGlobal.testingContext;

        public measureAttenuation() {
            pathLossHelper plh = new pathLossHelper(myGlobal.settingContext.filePathloss);
            listChannel = plh.FromXML().FirstOrDefault().dataItems;
        }

        public bool Excute(StackPanel spTable) {
            bool ret = true, r = false;
            if (listChannel == null || listChannel.Count == 0) goto END;
            myTesting.Wait();

            //load kit 1 result
            if (File.Exists("listKit1Result.txt")) {
                myGlobal.listKit1Result.Clear();
                string[] buffer = File.ReadAllLines("listKit1Result.txt");
                foreach (var b in buffer) {
                    string[] bff = b.Split(',');
                    kitItemResult k = new kitItemResult() {
                        Frequency = bff[0],
                        PowerTransmit = bff[1],
                        Power1 = bff[2],
                        Power2 = bff[3],
                        Power3 = bff[4],
                        AVG = bff[5],
                        Connector = bff[6],
                        Attenuation = bff[7]
                    };
                    myGlobal.listKit1Result.Add(k);
                }
            }

            //load kit 2 result
            if (File.Exists("listKit2Result.txt")) {
                myGlobal.listKit2Result.Clear();
                string[] buffer = File.ReadAllLines("listKit2Result.txt");
                foreach (var b in buffer) {
                    string[] bff = b.Split(',');
                    kitItemResult k = new kitItemResult() {
                        Frequency = bff[0],
                        PowerTransmit = bff[1],
                        Power1 = bff[2],
                        Power2 = bff[3],
                        Power3 = bff[4],
                        AVG = bff[5],
                        Connector = bff[6],
                        Attenuation = bff[7]
                    };
                    myGlobal.listKit2Result.Add(k);
                }
            }


            //Đo suy hao calibration kit
            if (myTesting.isMeasureKit == false) {
                if (myGlobal.listKit1Result.Count == 0 || myGlobal.listKit2Result.Count == 0) {
                    r = DoSuyHaoKit(spTable);
                    if (r == false) { ret = false; goto END; }
                }
            }
            else {
                r = DoSuyHaoKit(spTable);
                if (r == false) { ret = false; goto END; }
            }

            //Đo suy hao trạm
            r = DoSuyHaoTram(spTable);
            if (r == false) { ret = false; goto END; }

            //Tính giá trị trung bình 5 lần đo đầu tiên
            r = TinhGiaTriDoTrungBinh5Lan();

            //Hiển thị giá trị đo 5 lần đầu tiên
            r = HienThiGiaTriTrungBinh5Lan(spTable);

            //Phán định kết quả đo 5 lần đầu tiên
            ret = PhanDinhKetQuaDo5lan();
            if (ret) goto END;

            //Nếu kết quả đo 5 lần đầu tiên bị FAIL
            ret = CheckKetQuaDo5Lan();
            if (!ret) goto END;

            //Đo 5 lần tiếp theo
            r = DoSuyHaoTram5LanTiepTheo(spTable);
            if (r == false) { ret = false; goto END; }

            //Tính giá trị trung bình 10 lần đo đầu tiên
            r = TinhGiaTriDoTrungBinh10Lan();

            //Hiển thị giá trị đo 10 lần đầu tiên
            r = HienThiGiaTriTrungBinh10Lan(spTable);

            //Phán định kết quả đo 10 lần đầu tiên
            ret = PhanDinhKetQuaDo10lan();

        END:
            return ret;
        }

        private bool CalKit(List<kitItemResult> listKitResult, string step_name, string index_name, StackPanel spTable) {
            bool ret = false, isConnected = false;
            IInstrument equipment = null;
            string transmitPort = "", receivePort = "", power = "";

            myTesting.logSystem += $"Đo suy hao Calibration Kit {index_name}\n";
            myTesting.logSystem += $"-------------------------------------------------------------------\n";

            transmitPort = mySetting.portTransmitterKit;
            switch (index_name) {
                case "1": {
                        receivePort = mySetting.portReceiverKit1;
                        break;
                    }
                case "2": {
                        receivePort = mySetting.portReceiverKit2;
                        break;
                    }
            }
            power = mySetting.powerTransmit;

            PropertyInfo proty = myGlobal.testingContext.GetType().GetProperty(step_name);
            proty.SetValue(myGlobal.testingContext, "Waiting...");

            try {
                //connect to instrument
                myTesting.logSystem += $"Kết nối tới máy đo {mySetting.instrumentType},{mySetting.GPIB}\n";
                switch (mySetting.instrumentType.ToLower()) {
                    case "e6640a": { equipment = new E6640A<testingDataBinding>(myTesting, mySetting.GPIB, out ret); break; }
                    case "mt8870a": { equipment = new MT8870A<testingDataBinding>(myTesting, mySetting.GPIB, out ret); break; }
                }
                isConnected = ret;
                myTesting.logSystem += $"Kết quả: {isConnected}\n";
                if (!ret) goto END;

                //Chuyển máy đo sang mode đo suy hao
                if (mySetting.instrumentType.ToLower() == "mt8870a") {
                    myTesting.logSystem += $"Chuyển máy đo sang mode đo suy hao\n";
                    ret = equipment.config_Attenuator_Total(transmitPort, receivePort, power);
                    myTesting.logSystem += $"Kết quả: {ret}\n";
                    Thread.Sleep(1000);
                    myTesting.logSystem += $"Sleep 1000 ms\n";
                }

                //Đo suy hao
                listKitResult.Clear();
                myTesting.logSystem += $"Xóa danh sách lưu kết quả suy hao.\n";

                foreach (var ch in listChannel) {
                    kitItemResult kit = new kitItemResult();
                    kit.Frequency = ch.Frequency;
                    kit.PowerTransmit = power;
                    kit.Connector = mySetting.Connector;

                    //Điều khiển máy đo phát tín hiệu wifi
                    myTesting.logSystem += $"Điều khiển máy đo phát tín hiệu wifi ở tần số: {ch.Frequency} MHz, công suất: {power} dBm, port phát: {transmitPort}.\n";
                    equipment.config_Attenuator_Transmitter(ch.Frequency, power, transmitPort);

                    //Đọc giá trị công suất wifi thu được (tính trung bình 3 lần)
                    int count = 0;
                    List<double> listValue = new List<double>();
                RE_GET_POWER:
                    count++;
                    //Điều khiển máy đo đọc công suất wifi thu được
                    myTesting.logSystem += $"Điều khiển máy đo đọc công suất thu về ở tần số: {ch.Frequency} MHz, port thu: {receivePort}.\n";
                    string data = equipment.config_Attenuator_Receiver(ch.Frequency, receivePort);
                    myTesting.logSystem += $"Giá trị đo lần {count}: {data} dBm\n";
                    bool r = false;
                    double value;
                    r = double.TryParse(data, out value);
                    if (!r) {
                        if (count < 3) goto RE_GET_POWER;
                    }
                    else listValue.Add(value);

                    switch (count) {
                        case 1: { kit.Power1 = Math.Round(value, 2).ToString(); break; }
                        case 2: { kit.Power2 = Math.Round(value, 2).ToString(); break; }
                        case 3: { kit.Power3 = Math.Round(value, 2).ToString(); break; }
                    }

                    if (count < 3) goto RE_GET_POWER;
                    kit.AVG = Math.Round(listValue.Average(), 2).ToString();
                    myTesting.logSystem += $"Giá trị đo trung bình: {kit.AVG} dBm\n";

                    if (listValue.Count == 0) data = double.MaxValue.ToString();
                    else data = Math.Round(double.Parse(power) - listValue.Average(), 2).ToString();

                    kit.Attenuation = Math.Round(double.Parse(kit.PowerTransmit) - double.Parse(kit.AVG) - double.Parse(mySetting.Connector), 2).ToString();
                    myTesting.logSystem += $"Giá trị suy hao connector: {mySetting.Connector} dBm\n";
                    myTesting.logSystem += $"Giá trị suy hao kit {index_name}: {kit.Attenuation} dBm\n";
                    listKitResult.Add(kit);
                }


            }
            catch {
                ret = false;
                goto END;
            }

        END:
            if (equipment != null) equipment.Close();
            proty.SetValue(myGlobal.testingContext, "Completed");
            App.Current.Dispatcher.Invoke(new Action(() => {
                Label lb = new Label();
                lb.FontSize = 15;
                lb.FontWeight = FontWeights.SemiBold;
                lb.Margin = new Thickness(0, 20, 0, 0);
                lb.Content = $"+ ĐO SUY HAO KIT {index_name}: ";
                DataGrid dg1 = new DataGrid();
                dg1.CanUserAddRows = dg1.CanUserDeleteRows = dg1.CanUserReorderColumns = dg1.CanUserResizeColumns = dg1.CanUserResizeRows = dg1.CanUserSortColumns = false;
                dg1.ItemsSource = listKitResult;
                spTable.Children.Add(lb);
                spTable.Children.Add(dg1);

                myGlobal.mainContext.Opacity = 0.5;
                ResultWindow rw = new ResultWindow();
                rw.Title = $"+ ĐO SUY HAO KIT {index_name}: ";
                DataGrid dg2 = new DataGrid();
                dg2.CanUserAddRows = dg2.CanUserDeleteRows = dg2.CanUserReorderColumns = dg2.CanUserResizeColumns = dg2.CanUserResizeRows = dg2.CanUserSortColumns = false;
                dg2.ItemsSource = listKitResult;
                rw.sp_table.Children.Add(dg2);
                rw.ShowDialog();
                myGlobal.mainContext.Opacity = 1;

            }));
            return ret;
        }

        private bool CalAtt(List<attItemResult> listAttResult, string step_name, string index_name, StackPanel spTable) {
            bool ret = false, isConnected = false;
            IInstrument equipment = null;
            string transmitPort = "", receivePort1 = "", receivePort2 = "", power = "";

            myTesting.logSystem += $"Đo suy hao trạm lần thứ {index_name}\n";
            myTesting.logSystem += $"-------------------------------------------------------------------\n";

            transmitPort = mySetting.portTransmitterAtt;
            receivePort1 = mySetting.portReceiverAtt1;
            receivePort2 = mySetting.portReceiverAtt2;
            power = mySetting.powerTransmit;


            PropertyInfo proty = myGlobal.testingContext.GetType().GetProperty(step_name);
            proty.SetValue(myGlobal.testingContext, "Waiting...");

            try {
                //connect to instrument
                myTesting.logSystem += $"Kết nối tới máy đo {mySetting.instrumentType},{mySetting.GPIB}\n";
                switch (mySetting.instrumentType.ToLower()) {
                    case "e6640a": { equipment = new E6640A<testingDataBinding>(myTesting, mySetting.GPIB, out ret); break; }
                    case "mt8870a": { equipment = new MT8870A<testingDataBinding>(myTesting, mySetting.GPIB, out ret); break; }
                }
                isConnected = ret;
                myTesting.logSystem += $"Kết quả: {isConnected}\n";
                if (!ret) goto END;


                //Do suy hao antenna1
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++//
                //Chuyển máy đo sang mode đo suy hao
                if (mySetting.instrumentType.ToLower() == "mt8870a") {
                    myTesting.logSystem += $"Chuyển máy đo sang mode đo suy hao\n";
                    ret = equipment.config_Attenuator_Total(transmitPort, receivePort1, power);
                    myTesting.logSystem += $"Kết quả: {ret}\n";
                    Thread.Sleep(1000);
                    myTesting.logSystem += $"Sleep 1000 ms\n";
                }

                //Đo suy hao
                listAttResult.Clear();
                myTesting.logSystem += $"Xóa danh sách lưu kết quả suy hao.\n";

                foreach (var ch in listChannel) {
                    attItemResult att = new attItemResult();
                    att.Frequency = ch.Frequency;
                    att.PowerTransmit = power;
                    att.Connector = mySetting.Connector;
                    att.Kit1Power = myGlobal.listKit1Result.Where(x => x.Frequency.Equals(ch.Frequency)).FirstOrDefault().Attenuation;
                    att.Kit2Power = myGlobal.listKit2Result.Where(x => x.Frequency.Equals(ch.Frequency)).FirstOrDefault().Attenuation;

                    //Điều khiển máy đo phát tín hiệu wifi
                    equipment.config_Attenuator_Transmitter(ch.Frequency, power, transmitPort);
                    myTesting.logSystem += $"Điều khiển máy đo phát tín hiệu wifi ở tần số: {ch.Frequency} MHz, công suất: {power} dBm, port phát: {transmitPort}.\n";

                    //Đọc giá trị công suất wifi thu được (tính trung bình 3 lần)
                    int count = 0;
                    List<double> listValue = new List<double>();
                RE_GET_POWER:
                    count++;
                    myTesting.logSystem += $"Điều khiển máy đo đọc công suất thu về ở tần số: {ch.Frequency} MHz, port thu: {receivePort1}.\n";
                    string data = equipment.config_Attenuator_Receiver(ch.Frequency, receivePort1);
                    myTesting.logSystem += $"Giá trị đo lần {count}: {data} dBm\n";

                    bool r = false;
                    double value;
                    r = double.TryParse(data, out value);
                    if (!r) {
                        if (count < 3) goto RE_GET_POWER;
                    }
                    else listValue.Add(value);

                    switch (count) {
                        case 1: { att.Antenna1Kit1__Power1 = Math.Round(value, 2).ToString(); break; }
                        case 2: { att.Antenna1Kit1__Power2 = Math.Round(value, 2).ToString(); break; }
                        case 3: { att.Antenna1Kit1__Power3 = Math.Round(value, 2).ToString(); break; }
                    }

                    if (count < 3) goto RE_GET_POWER;
                    att.Antenna1Kit1__AVG = Math.Round(listValue.Average(), 2).ToString();
                    myTesting.logSystem += $"Giá trị đo trung bình: {att.Antenna1Kit1__AVG} dBm\n";

                    if (listValue.Count == 0) data = double.MaxValue.ToString();
                    else data = Math.Round(double.Parse(power) - listValue.Average(), 2).ToString();

                    att.Antenna1 = Math.Round(double.Parse(att.PowerTransmit) - double.Parse(att.Antenna1Kit1__AVG) - double.Parse(att.Kit1Power) - double.Parse(mySetting.Connector), 2).ToString();
                    myTesting.logSystem += $"Giá trị suy hao connector: {mySetting.Connector} dBm\n";
                    myTesting.logSystem += $"Giá trị suy hao antenna1: {att.Antenna1} dBm\n";

                    listAttResult.Add(att);

                    var fir = myGlobal.listFinalResult.Where(x => x.Frequency.Equals(att.Frequency)).FirstOrDefault();
                    PropertyInfo pi = fir.GetType().GetProperty($"AT1Power{index_name}");
                    pi.SetValue(fir, att.Antenna1);
                }


                //Do suy hao antenna2
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++//
                //Chuyển máy đo sang mode đo suy hao
                if (mySetting.instrumentType.ToLower() == "mt8870a") {
                    myTesting.logSystem += $"Chuyển máy đo sang mode đo suy hao\n";
                    equipment.config_Attenuator_Total(transmitPort, receivePort2, power);
                    myTesting.logSystem += $"Kết quả: {ret}\n";
                    Thread.Sleep(1000);
                    myTesting.logSystem += $"Sleep 1000 ms\n";
                }

                //Đo suy hao
                foreach (var ch in listChannel) {
                    attItemResult att = listAttResult.Where(x => x.Frequency.Equals(ch.Frequency)).FirstOrDefault();

                    //Yêu cầu máy đo phát tín hiệu wifi
                    equipment.config_Attenuator_Transmitter(ch.Frequency, power, transmitPort);
                    myTesting.logSystem += $"Điều khiển máy đo phát tín hiệu wifi ở tần số: {ch.Frequency} MHz, công suất: {power} dBm, port phát: {transmitPort}.\n";

                    //Đọc giá trị công suất wifi thu được (tính trung bình 3 lần)
                    int count = 0;
                    List<double> listValue = new List<double>();
                RE_GET_POWER:
                    count++;
                    myTesting.logSystem += $"Điều khiển máy đo đọc công suất thu về ở tần số: {ch.Frequency} MHz, port thu: {receivePort2}.\n";
                    string data = equipment.config_Attenuator_Receiver(ch.Frequency, receivePort2);
                    myTesting.logSystem += $"Giá trị đo lần {count}: {data} dBm\n";

                    bool r = false;
                    double value;
                    r = double.TryParse(data, out value);
                    if (!r) {
                        if (count < 3) goto RE_GET_POWER;
                    }
                    else listValue.Add(value);

                    switch (count) {
                        case 1: { att.Antenna2Kit2__Power1 = Math.Round(value, 2).ToString(); break; }
                        case 2: { att.Antenna2Kit2__Power2 = Math.Round(value, 2).ToString(); break; }
                        case 3: { att.Antenna2Kit2__Power3 = Math.Round(value, 2).ToString(); break; }
                    }

                    if (count < 3) goto RE_GET_POWER;
                    att.Antenna2Kit2__AVG = Math.Round(listValue.Average(), 2).ToString();
                    myTesting.logSystem += $"Giá trị đo trung bình: {att.Antenna2Kit2__AVG} dBm\n";

                    if (listValue.Count == 0) data = double.MaxValue.ToString();
                    else data = Math.Round(double.Parse(power) - listValue.Average(), 2).ToString();

                    att.Antenna2 = Math.Round(double.Parse(att.PowerTransmit) - double.Parse(att.Antenna2Kit2__AVG) - double.Parse(att.Kit2Power) - double.Parse(mySetting.Connector), 2).ToString();
                    myTesting.logSystem += $"Giá trị suy hao connector: {mySetting.Connector} dBm\n";
                    myTesting.logSystem += $"Giá trị suy hao antenna1: {att.Antenna2} dBm\n";

                    var fir = myGlobal.listFinalResult.Where(x => x.Frequency.Equals(att.Frequency)).FirstOrDefault();
                    PropertyInfo pi2 = fir.GetType().GetProperty($"AT2Power{index_name}");
                    pi2.SetValue(fir, att.Antenna2);
                }
            }
            catch {
                ret = false;
                goto END;
            }

        END:
            if (equipment != null) equipment.Close();
            proty.SetValue(myGlobal.testingContext, "Completed");
            App.Current.Dispatcher.Invoke(new Action(() => {
                Label lb = new Label();
                lb.FontSize = 15;
                lb.FontWeight = FontWeights.SemiBold;
                lb.Margin = new Thickness(0, 20, 0, 0);
                lb.Content = $"+ ĐO SUY HAO TRẠM LẦN {index_name}: ";
                DataGrid dg1 = new DataGrid();
                dg1.CanUserAddRows = dg1.CanUserDeleteRows = dg1.CanUserReorderColumns = dg1.CanUserResizeColumns = dg1.CanUserResizeRows = dg1.CanUserSortColumns = false;
                dg1.ItemsSource = listAttResult;
                spTable.Children.Add(lb);
                spTable.Children.Add(dg1);

                myGlobal.mainContext.Opacity = 0.5;
                ResultWindow rw = new ResultWindow();
                rw.Title = $"+ ĐO SUY HAO TRẠM LẦN {index_name}: ";
                DataGrid dg2 = new DataGrid();
                dg2.CanUserAddRows = dg2.CanUserDeleteRows = dg2.CanUserReorderColumns = dg2.CanUserResizeColumns = dg2.CanUserResizeRows = dg2.CanUserSortColumns = false;
                dg2.ItemsSource = listAttResult;
                rw.sp_table.Children.Add(dg2);
                rw.ShowDialog();
                myGlobal.mainContext.Opacity = 1;

            }));
            return ret;
        }

        private bool DoSuyHaoKit(StackPanel spTable) {
            bool ret = true, r = false, isRun = true;
            try {
                List<myParameter.StepMeasurement> listKitStep = new List<myParameter.StepMeasurement>() { myParameter.StepMeasurement.Kit1, myParameter.StepMeasurement.Kit2 };
                List<List<kitItemResult>> listKitItem = new List<List<kitItemResult>>() { myGlobal.listKit1Result, myGlobal.listKit2Result };
                List<string> listKitStepName = new List<string>() { "kit1Result", "kit2Result" };
                List<string> listKitStepIndex = new List<string>() { "1", "2" };
                for (int i = 0; i < 2; i++) {
                    App.Current.Dispatcher.Invoke(new Action(() => {
                        isRun = false;
                        myGlobal.mainContext.Opacity = 0.5;
                        InstructionWindow iw = new InstructionWindow(listKitStep[i]);
                        iw.ShowDialog();
                        isRun = iw.isStart;
                        myGlobal.mainContext.Opacity = 1;
                    }));
                    if (isRun) {
                    RE:
                        r = CalKit(listKitItem[i], listKitStepName[i], listKitStepIndex[i], spTable);
                        //save kit item to file
                        if (r) {
                            using (var sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "listKit" + (i + 1).ToString() + "Result" + ".txt" )) {
                                foreach (var item in listKitItem[i]) {
                                    sw.WriteLine(item.ToText());
                                }
                            }
                        }
                        if (myGlobal.isRetry == true) goto RE;
                    }
                    else {
                        PropertyInfo proty = myGlobal.testingContext.GetType().GetProperty(listKitStepName[i]);
                        proty.SetValue(myGlobal.testingContext, "Abort");
                    }
                    if (!r) ret = false;
                }
            }
            catch { ret = false; }

            return ret;
        }

        private bool DoSuyHaoTram(StackPanel spTable) {
            bool ret = true, r = false, isRun = true;
            try {
                List<myParameter.StepMeasurement> listAttStep = new List<myParameter.StepMeasurement>() { myParameter.StepMeasurement.First, myParameter.StepMeasurement.Second, myParameter.StepMeasurement.Third, myParameter.StepMeasurement.Fourth, myParameter.StepMeasurement.Fifth };
                List<List<attItemResult>> listAttItem = new List<List<attItemResult>>() { myGlobal.listAtt1Result, myGlobal.listAtt2Result, myGlobal.listAtt3Result, myGlobal.listAtt4Result, myGlobal.listAtt5Result };
                List<string> listAttStepName = new List<string>() { "firstResult", "secondResult", "thirdResult", "fourthResult", "fifthResult" };
                List<string> listAttStepIndex = new List<string>() { "1", "2", "3", "4", "5" };
                myGlobal.listFinalResult.Clear();
                foreach (var ch in listChannel) {
                    finalItemResult fir = new finalItemResult();
                    fir.Frequency = ch.Frequency;
                    myGlobal.listFinalResult.Add(fir);
                }

                for (int i = 0; i < 5; i++) {
                    App.Current.Dispatcher.Invoke(new Action(() => {
                        isRun = false;
                        myGlobal.mainContext.Opacity = 0.5;
                        InstructionWindow iw = new InstructionWindow(listAttStep[i]);
                        iw.ShowDialog();
                        isRun = iw.isStart;
                        myGlobal.mainContext.Opacity = 1;
                    }));
                    if (isRun) {
                    RE:
                        r = CalAtt(listAttItem[i], listAttStepName[i], listAttStepIndex[i], spTable);
                        if (myGlobal.isRetry == true) goto RE;
                    }
                    else {
                        PropertyInfo proty = myGlobal.testingContext.GetType().GetProperty(listAttStepName[i]);
                        proty.SetValue(myGlobal.testingContext, "Abort");
                    }
                    if (!r) { ret = false; }
                }
            }
            catch { ret = false; }
            return ret;
        }

        private bool TinhGiaTriDoTrungBinh5Lan() {
            try {
                foreach (var item in myGlobal.listFinalResult) {
                    List<double> listAt1 = new List<double>() { double.Parse(item.AT1Power1), double.Parse(item.AT1Power2), double.Parse(item.AT1Power3), double.Parse(item.AT1Power4), double.Parse(item.AT1Power5) };
                    item.AT1AVG = listAt1.Average().ToString();
                    List<double> listAt2 = new List<double>() { double.Parse(item.AT2Power1), double.Parse(item.AT2Power2), double.Parse(item.AT2Power3), double.Parse(item.AT2Power4), double.Parse(item.AT2Power5) };
                    item.AT2AVG = listAt2.Average().ToString();
                }
                return true;
            }
            catch { return false; }
        }

        private bool HienThiGiaTriTrungBinh5Lan(StackPanel spTable) {
            try {
                App.Current.Dispatcher.Invoke(new Action(() => {
                    Label lb = new Label();
                    lb.FontSize = 15;
                    lb.FontWeight = FontWeights.SemiBold;
                    lb.Margin = new Thickness(0, 20, 0, 0);
                    lb.Content = $"+ GIÁ TRỊ SUY HAO TRUNG BÌNH: ";
                    DataGrid dg = new DataGrid();
                    dg.CanUserAddRows = dg.CanUserDeleteRows = dg.CanUserReorderColumns = dg.CanUserResizeColumns = dg.CanUserResizeRows = dg.CanUserSortColumns = false;
                    dg.ItemsSource = myGlobal.listFinalResult;
                    spTable.Children.Add(lb);
                    spTable.Children.Add(dg);
                }));
                return true;
            }
            catch { return false; }
        }

        private bool PhanDinhKetQuaDo5lan() {
            bool ret = false, re1 = true, re2 = true, re3 = true, re4 = true, re5 = true;
            int max_value = 5, error_count = 0;
            try {
                foreach (var item in myGlobal.listFinalResult) {
                    bool re = false;
                    re = powerIsOK(item.AT1Power1, item.AT1AVG, item.AT2Power1, item.AT2AVG);
                    if (re == false) re1 = false;
                    re = powerIsOK(item.AT1Power2, item.AT1AVG, item.AT2Power2, item.AT2AVG);
                    if (re == false) re2 = false;
                    re = powerIsOK(item.AT1Power3, item.AT1AVG, item.AT2Power3, item.AT2AVG);
                    if (re == false) re3 = false;
                    re = powerIsOK(item.AT1Power4, item.AT1AVG, item.AT2Power4, item.AT2AVG);
                    if (re == false) re4 = false;
                    re = powerIsOK(item.AT1Power5, item.AT1AVG, item.AT2Power5, item.AT2AVG);
                    if (re == false) re5 = false;
                }

                myTesting.firstResult = re1 ? "Passed" : "Failed";
                if (re1 == false) error_count++;
                myTesting.secondResult = re2 ? "Passed" : "Failed";
                if (re2 == false) error_count++;
                myTesting.thirdResult = re3 ? "Passed" : "Failed";
                if (re3 == false) error_count++;
                myTesting.fourthResult = re4 ? "Passed" : "Failed";
                if (re4 == false) error_count++;
                myTesting.fifthResult = re5 ? "Passed" : "Failed";
                if (re5 == false) error_count++;

                myGlobal.testingContext.errorRate = ((error_count * 100) / max_value).ToString();
                ret = (100 - int.Parse(myGlobal.testingContext.errorRate)) >= int.Parse(mySetting.passRate);
                return ret;

            }
            catch { return false; }
        }

        private bool CheckKetQuaDo5Lan() {
            bool ret = true;
            foreach (var item in myGlobal.listFinalResult) {
                bool re = false;
                re = powerIsValid(item.AT1Power1, item.AT1AVG, item.AT2Power1, item.AT2AVG);
                if (re == false) ret = false;
                re = powerIsValid(item.AT1Power2, item.AT1AVG, item.AT2Power2, item.AT2AVG);
                if (re == false) ret = false;
                re = powerIsValid(item.AT1Power3, item.AT1AVG, item.AT2Power3, item.AT2AVG);
                if (re == false) ret = false;
                re = powerIsValid(item.AT1Power4, item.AT1AVG, item.AT2Power4, item.AT2AVG);
                if (re == false) ret = false;
                re = powerIsValid(item.AT1Power5, item.AT1AVG, item.AT2Power5, item.AT2AVG);
                if (re == false) ret = false;
            }
            return ret;
        }

        private bool powerIsOK(string at1power, string at1avg, string at2power, string at2avg) {
            double tor = double.Parse(mySetting.torStandard);
            bool ret = Math.Abs(double.Parse(at1power) - double.Parse(at1avg)) <= tor &&
                       Math.Abs(double.Parse(at2power) - double.Parse(at2avg)) <= tor;
            if (ret == false) return false;

            ret = double.Parse(at1power) > 0 && double.Parse(at1avg) > 0 && double.Parse(at2power) > 0 && double.Parse(at2avg) > 0;
            return ret;
        }

        private bool powerIsValid(string at1power, string at1avg, string at2power, string at2avg) {
            bool res = false;
            double tor = double.Parse(mySetting.torMax);
            res = double.Parse(at1power) > 0 && double.Parse(at1avg) > 0 && double.Parse(at2power) > 0 && double.Parse(at2avg) > 0;
            if (res == false) return false;

            res = Math.Abs(double.Parse(at1power) - double.Parse(at1avg)) <= tor &&
                  Math.Abs(double.Parse(at2power) - double.Parse(at2avg)) <= tor;

            return res;
        }

        private bool DoSuyHaoTram5LanTiepTheo(StackPanel spTable) {
            bool ret = true, r = false, isRun = true;
            try {
                List<myParameter.StepMeasurement> listAttStep = new List<myParameter.StepMeasurement>() { myParameter.StepMeasurement.Sixth, myParameter.StepMeasurement.Seventh, myParameter.StepMeasurement.Eighth, myParameter.StepMeasurement.Ninth, myParameter.StepMeasurement.Tenth };
                List<List<attItemResult>> listAttItem = new List<List<attItemResult>>() { myGlobal.listAtt6Result, myGlobal.listAtt7Result, myGlobal.listAtt8Result, myGlobal.listAtt9Result, myGlobal.listAtt10Result };
                List<string> listAttStepName = new List<string>() { "sixthResult", "seventhResult", "eighthResult", "ninthResult", "tenthResult" };
                List<string> listAttStepIndex = new List<string>() { "6", "7", "8", "9", "10" };

                for (int i = 0; i < 5; i++) {
                    App.Current.Dispatcher.Invoke(new Action(() => {
                        isRun = false;
                        myGlobal.mainContext.Opacity = 0.5;
                        InstructionWindow iw = new InstructionWindow(listAttStep[i]);
                        iw.ShowDialog();
                        isRun = iw.isStart;
                        myGlobal.mainContext.Opacity = 1;
                    }));
                    if (isRun) r = CalAtt(listAttItem[i], listAttStepName[i], listAttStepIndex[i], spTable);
                    else {
                        PropertyInfo proty = myGlobal.testingContext.GetType().GetProperty(listAttStepName[i]);
                        proty.SetValue(myGlobal.testingContext, "Abort");
                    }
                    if (!r) { ret = false; }
                }
            }
            catch { ret = false; }
            return ret;
        }

        private bool TinhGiaTriDoTrungBinh10Lan() {
            try {
                foreach (var item in myGlobal.listFinalResult) {
                    List<double> listAt1 = new List<double>() { double.Parse(item.AT1Power1), double.Parse(item.AT1Power2), double.Parse(item.AT1Power3), double.Parse(item.AT1Power4), double.Parse(item.AT1Power5), double.Parse(item.AT1Power6), double.Parse(item.AT1Power7), double.Parse(item.AT1Power8), double.Parse(item.AT1Power9), double.Parse(item.AT1Power10) };
                    item.AT1AVG = listAt1.Average().ToString();
                    List<double> listAt2 = new List<double>() { double.Parse(item.AT2Power1), double.Parse(item.AT2Power2), double.Parse(item.AT2Power3), double.Parse(item.AT2Power4), double.Parse(item.AT2Power5), double.Parse(item.AT2Power6), double.Parse(item.AT2Power7), double.Parse(item.AT2Power8), double.Parse(item.AT2Power9), double.Parse(item.AT2Power10) };
                    item.AT2AVG = listAt2.Average().ToString();
                }
                return true;
            }
            catch { return false; }
        }

        private bool HienThiGiaTriTrungBinh10Lan(StackPanel spTable) {
            try {
                App.Current.Dispatcher.Invoke(new Action(() => {
                    Label lb = new Label();
                    lb.FontSize = 15;
                    lb.FontWeight = FontWeights.SemiBold;
                    lb.Margin = new Thickness(0, 20, 0, 0);
                    lb.Content = $"+ GIÁ TRỊ SUY HAO TRUNG BÌNH 10 LẦN: ";
                    DataGrid dg = new DataGrid();
                    dg.CanUserAddRows = dg.CanUserDeleteRows = dg.CanUserReorderColumns = dg.CanUserResizeColumns = dg.CanUserResizeRows = dg.CanUserSortColumns = false;
                    dg.ItemsSource = myGlobal.listFinalResult;
                    spTable.Children.Add(lb);
                    spTable.Children.Add(dg);

                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromMilliseconds(500);
                    timer.Tick += (s, e) => {
                        for (int row = 0; row < myGlobal.listFinalResult.Count; row++) {
                            var item = myGlobal.listFinalResult[row];
                            for (int antenna = 1; antenna <= 2; antenna++) {
                                for (int idx = 1; idx <= 10; idx++) {
                                    ChangeCellForeGround(dg, row, item, antenna, idx);
                                }
                            }
                        }
                        timer.Stop();
                    };
                    timer.Start();

                }));
                return true;
            }
            catch { return false; }
        }

        private bool PhanDinhKetQuaDo10lan() {
            bool ret = false, re1 = true, re2 = true, re3 = true, re4 = true, re5 = true, re6 = true, re7 = true, re8 = true, re9 = true, re10 = true;
            int max_value = 10, error_count = 0;
            try {
                foreach (var item in myGlobal.listFinalResult) {
                    bool re = false;
                    re = powerIsOK(item.AT1Power1, item.AT1AVG, item.AT2Power1, item.AT2AVG);
                    if (re == false) re1 = false;
                    re = powerIsOK(item.AT1Power2, item.AT1AVG, item.AT2Power2, item.AT2AVG);
                    if (re == false) re2 = false;
                    re = powerIsOK(item.AT1Power3, item.AT1AVG, item.AT2Power3, item.AT2AVG);
                    if (re == false) re3 = false;
                    re = powerIsOK(item.AT1Power4, item.AT1AVG, item.AT2Power4, item.AT2AVG);
                    if (re == false) re4 = false;
                    re = powerIsOK(item.AT1Power5, item.AT1AVG, item.AT2Power5, item.AT2AVG);
                    if (re == false) re5 = false;
                    re = powerIsOK(item.AT1Power6, item.AT1AVG, item.AT2Power6, item.AT2AVG);
                    if (re == false) re6 = false;
                    re = powerIsOK(item.AT1Power7, item.AT1AVG, item.AT2Power7, item.AT2AVG);
                    if (re == false) re7 = false;
                    re = powerIsOK(item.AT1Power8, item.AT1AVG, item.AT2Power8, item.AT2AVG);
                    if (re == false) re8 = false;
                    re = powerIsOK(item.AT1Power9, item.AT1AVG, item.AT2Power9, item.AT2AVG);
                    if (re == false) re9 = false;
                    re = powerIsOK(item.AT1Power10, item.AT1AVG, item.AT2Power10, item.AT2AVG);
                    if (re == false) re10 = false;
                }

                myTesting.firstResult = re1 ? "Passed" : "Failed";
                if (re1 == false) error_count++;
                myTesting.secondResult = re2 ? "Passed" : "Failed";
                if (re2 == false) error_count++;
                myTesting.thirdResult = re3 ? "Passed" : "Failed";
                if (re3 == false) error_count++;
                myTesting.fourthResult = re4 ? "Passed" : "Failed";
                if (re4 == false) error_count++;
                myTesting.fifthResult = re5 ? "Passed" : "Failed";
                if (re5 == false) error_count++;
                myTesting.sixthResult = re6 ? "Passed" : "Failed";
                if (re6 == false) error_count++;
                myTesting.seventhResult = re7 ? "Passed" : "Failed";
                if (re7 == false) error_count++;
                myTesting.eighthResult = re8 ? "Passed" : "Failed";
                if (re8 == false) error_count++;
                myTesting.ninthResult = re9 ? "Passed" : "Failed";
                if (re9 == false) error_count++;
                myTesting.tenthResult = re10 ? "Passed" : "Failed";
                if (re10 == false) error_count++;

                myGlobal.testingContext.errorRate = ((error_count * 100) / max_value).ToString();
                ret = (100 - int.Parse(myGlobal.testingContext.errorRate)) >= int.Parse(mySetting.passRate);
                return ret;

            }
            catch { return false; }
        }


        public DataGridCell GetCell(int rowIndex, int columnIndex, DataGrid dg) {
            DataGridRow row = dg.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            DataGridCellsPresenter p = GetVisualChild<DataGridCellsPresenter>(row);
            DataGridCell cell = p.ItemContainerGenerator.ContainerFromIndex(columnIndex) as DataGridCell;
            return cell;
        }

        static T GetVisualChild<T>(Visual parent) where T : Visual {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++) {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null) {
                    child = GetVisualChild<T>(v);
                }
                if (child != null) {
                    break;
                }
            }
            return child;
        }

        private void ChangeCellForeGround(DataGrid dg, int row, finalItemResult item, int antenna, int item_index) {
            int col = 0;
            string power = (string)item.GetType().GetProperty($"AT{antenna}Power{item_index}").GetValue(item, null);
            string avg = (string)item.GetType().GetProperty($"AT{antenna}AVG").GetValue(item, null);
            col = antenna == 1 ? (item_index + 1) : (item_index + 12);

            if (string.IsNullOrEmpty(power) == true || string.IsNullOrWhiteSpace(power) == true) return;
            if (string.IsNullOrEmpty(avg) == true || string.IsNullOrWhiteSpace(avg) == true) return;

            bool x = Math.Abs(double.Parse(power) - double.Parse(avg)) <= double.Parse(myGlobal.settingContext.torStandard);

            if (!x) {
                DataGridCell cell = GetCell(row, col, dg);
                cell.FontWeight = FontWeights.SemiBold;
                cell.Foreground = new SolidColorBrush(Colors.Red);
            }
        }


    }
}

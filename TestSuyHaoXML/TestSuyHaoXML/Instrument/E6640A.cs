/* 
/* 17/4/2015: Add thêm hàm gửi từng lệnh tới thiết bị
 * Ghi ket qua vao logfile
 * Cho phep chay song song voi DUT
 */
//
//Namspace hỗ trợ giao tiếp với thư viện .DLL
using System.Reflection;
using System.Media;
using System;
using System.Text;
using System.Threading;
using System.Net;
using System.Drawing;
using System.Windows.Forms;
using NationalInstruments.VisaNS; //Using .NET VISA DRIVER
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.Data;
using System.IO;
//using System.NullReferenceException;


namespace E6640A_VNPT
{
    public class E6640A_VISA
    {
        /*----------------------------------------------------------*/
        string g_logfilePath = @"WIFI_LOGFILE_E6640A.LOG";
        
        private MessageBasedSession mbSession;
        public int minEquip_DELAY = 50;
        public int avgEquip_DELAY = 100;
        public int maxEquip_DELAY = 200;
        public string logfile = "";

       // int TimeOutMs = 400;
        //Các biến dữ liệu kiểu decimal chứa thông số UpperLimit & LowerLimit chuẩn WLAN
        public Decimal PeakPWLow_decimal;
        public Decimal PeakPWUp_decimal;
        public Decimal AvgPWLow_decimal;
        public Decimal AvgPWUp_decimal;
        public Decimal EVMallUp_decimal;
        public Decimal EVMDataUp_decimal;
        public Decimal EVMPilotUp_decimal;
        public Decimal MaxAmpI_decimal;
        public Decimal MaxFreqErr_decimal;
        public Decimal MaxIQ_decimal;
        public Decimal MaxPhaseI_decimal;
        public Decimal MaxSymbolCLKErr_decimal;
        public Decimal MaxPhaseErr_decimal;

        //---Flag kiểm tra trạng thái cấu hình máy đo đã hoàn thành hay chưa
        private bool config_done = false;

        public bool check_config_done
        {
            get { return config_done; }
            set { config_done = value; }
        }

        /*----------------------------------------------------------*/
        public E6640A_VISA(string MeasureEquip_IP)
        {
            try
            {
                mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(MeasureEquip_IP);
                //Hàm dùng để vào máy đo, để thực hiện cấu hình
            }
            catch
            {
                    //Nếu vòng try sai thì báo lỗi ở đây
                    MessageBox.Show("[E6640A_VISA]Không kết nối được với máy đo IP= " + MeasureEquip_IP);
                    saveLogfile(g_logfilePath, "[E6640A_VISA]Không kết nối được với máy đo IP= " + MeasureEquip_IP + " \n");                
            };
        }
        /*--------------Device Clear Operation-----------------update 12/05/2015---------------------------*/
        public void ClearCommand()
        {
            mbSession.Clear();
        }
        /*----------------------------------------------------------*/
        /*----------------------------------------------------------*/
        public void Close()
        {
            mbSession.Dispose();
        }
        /*----------------------------------------------------------*/
        //Thay thế ký tự \n --> lệnh NewLine
        // \\n chính là ký tự "\n"
        private string ReplaceCommonEscapeSequences(string s)
        {
            return s.Replace("\\n", "\n").Replace("\\r", "\r");
        }

        private string InsertCommonEscapeSequences(string s)
        {
            return s.Replace("\n", "\\n").Replace("\r", "\\r");
        }

        //// Add them 17/4/2015
        public void oneCommand(string command)
        {
            try
            {
                checkBusyState(mbSession, "*ESR?");
                checkBusyState(mbSession, "*OPC?");
                mbSession.Write(command);
            }
            catch
            { };
        }

        /*-----------------Cấu hình cho thiết bị đo E6640A-----------------------------------------------
     * - string[] preSetting_CMWCommand: Chứa lệnh cấu hình chung cho thiết bị đo
     * - string[] channel_to_Freq: Chứa tần số của các kênh từ 1 --> 12 
     */
        //Each frequency is corresponding to a frequency channel, [ex: 2412000000 ~ channel 1 ]
        string[] E6640A_channel_to_Freq = {     "2412000000"       //1
                                                ,"2417000000"      //2
                                                ,"2422000000"      //3
                                                ,"2427000000"      //4
                                                ,"2432000000"      //5
                                                ,"2437000000"      //6
                                                ,"2442000000"      //7
                                                ,"2447000000"      //8
                                                ,"2452000000"      //9
                                                ,"2457000000"      //10
                                                ,"2462000000"      //11
                                                ,"2467000000"      //12
                                   };
        //Cấu hình máy đo
        public bool config_HT20(byte counter, string channel, string RFinput)
        {
            bool enable_nSISO_Testing = false;
            try
            {
                if (counter == 0)
                {
                    saveLogfile(g_logfilePath, "[BOL] Khởi tạo lần đầu cho thiết bị!\n");
                    mbSession.Write("INST:SEL WLAN\n");
                    //Thread.Sleep(avgEquip_DELAY);
                    checkBusyState(mbSession, "*ESR?");
                    checkBusyState(mbSession, "*OPC?");
                    mbSession.Write(":RAD:STAN N20\n");//Đặt băng thông là 20
                    //Thread.Sleep(avgEquip_DELAY);
                    checkBusyState(mbSession, "*ESR?");
                    checkBusyState(mbSession, "*OPC?");
                    mbSession.Write(":FEED:RF:PORT " + RFinput + "\n");//Gián port RF mình muốn dùng lên đây
                    //Thread.Sleep(avgEquip_DELAY);
                    checkBusyState(mbSession, "*ESR?");
                    checkBusyState(mbSession, "*OPC?");
                    mbSession.Write("CORR:IMP 50\n");//??????
                    //Thread.Sleep(avgEquip_DELAY);
                    checkBusyState(mbSession, "*ESR?");
                    checkBusyState(mbSession, "*OPC?");
                    mbSession.Write(":INITiate:EVM\n");//EVM
                    //Thread.Sleep(avgEquip_DELAY);
                    checkBusyState(mbSession, "*ESR?");
                    checkBusyState(mbSession, "*OPC?");
                    mbSession.Write(":SENSe:POWer:RF:RANGe:OPTimize IMMediate\n");//???????
                    //Thread.Sleep(maxEquip_DELAY);
                    checkBusyState(mbSession, "*ESR?");
                    checkBusyState(mbSession, "*OPC?");
                    mbSession.Write(":INIT:CONT 0\n");//??????????
                }
                //Channel number ~ Frequency value
                int channel_int = Int16.Parse(channel); //Convert string Channel to int channel --> Frequency  
                saveLogfile(g_logfilePath, "[BOL] Tần số được test: " + E6640A_channel_to_Freq[channel_int - 1] + "Hz - Kênh " + channel + " \n");
                mbSession.Write("FREQ:CENT " + E6640A_channel_to_Freq[channel_int - 1] + "\n");
                enable_nSISO_Testing = true;
                config_done = true;
                saveLogfile(g_logfilePath, "[E6640A] Đã cấu hình cho E6640A  \n");
            }
            catch //(Exception a)
            {
                enable_nSISO_Testing = false;
                config_done = true;
                //MessageBox.Show("Fail at WLAN_Config: " + a.Message); 
                saveLogfile(g_logfilePath, "[E6640A]ERROR CODE: [Equip_Config] \n Error tai qua trinh cau hinh cho thiet bi do E6640A \n");
            }
            return enable_nSISO_Testing;
        }

        /*
      * Hàm kiểm tra giá trị đo được là Pass/Fail bằng việc so sánh với giá trị LowerLimit và UpperLimit
      * Tham sô đầu vào:
      * - Decimal value: Giá trị đo được
      * - Decimal lowerlimit: Giá trị LowerLimit
      * - Decimal upperlimit: Giá trị UpperLimit
      * Tham số trong hàm:
      * - g_testPass_SUM: Tổng số thông số của tất cả các bài test pass
      * - g_testFail_SUM: Tổng số thông số của tất cả các bài test fail
      */
        public bool check_PassFail_limit(Decimal value, Decimal lowerlimit, Decimal upperlimit)
        {
            bool result = false;
            if ((value <= upperlimit) && (value >= lowerlimit))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        //
        /*-------Testing for WLAN 802.11n - OFDM Modulation------------------------------------------------*/
        /* ------Thực hiện đo WLAN 802.11n SISO---------------------------- 
        * Tham số đầu vào:
        * - RichTexBox g_logfilePath: Hiển thị ở giao diện chính
        * - E6640A
        * - string testItems: Danh sách các thông số cần đo
        * - string strInAtten: Giá trị suy hao input  (--->RF port)
        * - string strOutAtten: Giá trị suy hao ouput (RF port--->)
        * Các biến trong hàm: 
        * - Thread.Sleep(timedelay): Đợi một khoảng timedelay [ms]
        */
        //Cấu hình bài test
        public bool testing_HT20(string testItems)
        {
            bool wifiTesting_result = true;
            try
            {
                string result_Value = string.Empty;
                //-----Thay đổi giá trị Statistic count để lấy được kết quả đo chính xác-------------------------------------------------- 
                //-----Sau khi thay đổi giá trị này, phần mềm phải tạo lại 1 phiên kết nối mới với máy đo để đọc được kết quả đo-----------   
                //Đọc kết quả đo điều chế OFDM   
                //Thread.Sleep(maxEquip_DELAY * 5); //comment on Thusday 26 Novem 2015
                checkBusyState(mbSession, "*ESR?");
                checkBusyState(mbSession, "*OPC?");
                mbSession.Write(":INIT:CONT 0");
                checkBusyState(mbSession, "*ESR?");
                checkBusyState(mbSession, "*OPC?");
                mbSession.Write(":SENSe:POWer:RF:RANGe:OPTimize IMMediate\n");
                checkBusyState(mbSession, "*ESR?");
                checkBusyState(mbSession, "*OPC?");
                result_Value = mbSession.Query("READ:EVM?");
                result_Value = InsertCommonEscapeSequences(result_Value);
                //saveLogfile(g_logfilePath, "READ:EVM? ==>[" + result_Value + "]\n");
                if (result_Value.Trim() == "")//Nếu như chuỗi result_Value rỗng thì đưa ra FAIL
                {
                    saveLogfile(g_logfilePath, "  FAIL\n");
                    wifiTesting_result = false;
                    saveLogfile(g_logfilePath, "[E6640A] [FAIL] Giá trị kết quả trả về = null \n");
                    return wifiTesting_result;
                }
                //---------Hiển thị kết quả đo nếu như kết quả là hợp lệ---------------------- 
                try
                {
                    string[] MODulation_Value = result_Value.Split(new Char[] { ',' });
                    Decimal measureResult;
                    //Thông số được lựa chọn trong bài test
                    if (testItems.Contains("AvgPower"))
                    {
                        measureResult = Decimal.Parse(MODulation_Value[19], System.Globalization.NumberStyles.Float);
                        //Hien thi ket qua do duoc                                 
                        saveLogfile(g_logfilePath, "- Average Power = " + measureResult.ToString("0.####") + " dBm");
                        //Kiểm tra xem nó FAIL hay PASS bằng cách dung hàm check đũng viết từ trên
                        if (check_PassFail_limit(measureResult, AvgPWLow_decimal, AvgPWUp_decimal))
                        {
                            saveLogfile(g_logfilePath, "  PASS\n");
                        }
                        else
                        {
                            saveLogfile(g_logfilePath, "  FAIL\n");
                            wifiTesting_result = false;
                            saveLogfile(g_logfilePath, "[E6640A] FAIL CODE: [WIFI_TXPower]\n \n Fail Tai Bai Test 'Average Power' \n");
                            return wifiTesting_result;
                        }
                    }

                    //Thông số được lựa chọn trong bài test
                    if (testItems.Contains("EVMall"))
                    {
                        measureResult = Decimal.Parse(MODulation_Value[1], System.Globalization.NumberStyles.Float);
                        saveLogfile(g_logfilePath, "- EVM All Carriers = " + measureResult.ToString("0.####") + " dB");
                        if (check_PassFail_limit(measureResult, -500, EVMallUp_decimal))
                        {
                            saveLogfile(g_logfilePath, "  PASS\n");
                        }
                        else
                        {
                            saveLogfile(g_logfilePath, "  FAIL\n");
                            wifiTesting_result = false;
                            saveLogfile(g_logfilePath, "[E6640A] FAIL CODE: [WIFI_EVMAll]\n \n Fail Tai Bai Test 'EVM All Data' \n");
                            return wifiTesting_result;
                        }
                    }

                    //Thông số được lựa chọn trong bài test
                    //EVM data carriers
                    if (testItems.Contains("EVMdata"))
                    {
                        measureResult = Decimal.Parse(MODulation_Value[29], System.Globalization.NumberStyles.Float);
                        saveLogfile(g_logfilePath, "- EVM Data Carriers = " + measureResult.ToString("0.####") + " dB");
                        if (check_PassFail_limit(measureResult, -500, EVMDataUp_decimal))
                        {
                            saveLogfile(g_logfilePath, "  PASS\n");
                        }
                        else
                        {
                            saveLogfile(g_logfilePath, "  FAIL\n");
                            wifiTesting_result = false;
                            saveLogfile(g_logfilePath, "[E6640A] FAIL CODE: [WIFI_EVMData]\n \n Fail Tai Bai Test 'EVM Data Carriers' \n");
                            return wifiTesting_result;
                        }
                    }

                    //Thông số được lựa chọn trong bài test
                    //Gia tri EVM pilot carriers
                    if (testItems.Contains("EVMpilot"))
                    {
                        measureResult = Decimal.Parse(MODulation_Value[27], System.Globalization.NumberStyles.Float);
                        saveLogfile(g_logfilePath, "- EVM Pilot Carriers = " + measureResult.ToString("0.####") + " dB");
                        if (check_PassFail_limit(measureResult, -500, EVMPilotUp_decimal))
                        {
                            saveLogfile(g_logfilePath, "  PASS\n");
                        }
                        else
                        {
                            saveLogfile(g_logfilePath, "  FAIL\n");
                            wifiTesting_result = false;
                            saveLogfile(g_logfilePath, "[E6640A] FAIL CODE: [WIFI_EVMPilot]\n \n Fail Tai Bai Test 'EVM Pilot Carriers' \n");
                            return wifiTesting_result;
                        }
                    }

                    //Thông số được lựa chọn trong bài test
                    //FreqError
                    if (testItems.Contains("FreErr"))
                    {
                        measureResult = Decimal.Parse(MODulation_Value[7], System.Globalization.NumberStyles.Float);
                        saveLogfile(g_logfilePath, "- Center Frequency Error = " + measureResult.ToString("0.####") + " Hz");
                        if (check_PassFail_limit(measureResult, -MaxFreqErr_decimal, MaxFreqErr_decimal))
                        {
                            saveLogfile(g_logfilePath, "  PASS\n");
                        }
                        else
                        {
                            saveLogfile(g_logfilePath, "  FAIL\n");
                            wifiTesting_result = false;
                            saveLogfile(g_logfilePath, "[E6640A] FAIL CODE: [WIFI_Freq]\n \n Fail Tai Bai Test 'Center Frequency Error' \n");
                            return wifiTesting_result;
                        }
                    }

                    //Thông số được lựa chọn trong bài test
                    //Gia tri Symbol clock error
                    if (testItems.Contains("SylClkErr"))
                    {
                        measureResult = Decimal.Parse(MODulation_Value[11], System.Globalization.NumberStyles.Float);
                        saveLogfile(g_logfilePath, "- Symbol clock error = " + measureResult.ToString("0.####") + " ppm");
                        if (check_PassFail_limit(measureResult, -MaxSymbolCLKErr_decimal, MaxSymbolCLKErr_decimal))
                        {
                            saveLogfile(g_logfilePath, "  PASS\n");
                        }
                        else
                        {
                            saveLogfile(g_logfilePath, "  FAIL\n");
                            wifiTesting_result = false;
                            saveLogfile(g_logfilePath, "[E6640A] FAIL CODE: [WIFI_SymbolClock]\n \n Fail Tai Bai Test 'Symbol Clock Error' \n");
                            return wifiTesting_result;
                        }
                    }

                    //Thông số được lựa chọn trong bài test
                    //Gia tri LO Leakage (dB)?
                    if (testItems.Contains("IQIm"))
                    {
                        measureResult = Decimal.Parse(MODulation_Value[13], System.Globalization.NumberStyles.Float);
                        saveLogfile(g_logfilePath, "- IQ Offset = " + measureResult.ToString("0.####") + " dB");
                        if (check_PassFail_limit(measureResult, -500, MaxIQ_decimal))
                        {
                            saveLogfile(g_logfilePath, "  PASS\n");
                        }
                        else
                        {
                            saveLogfile(g_logfilePath, "  FAIL\n");
                            wifiTesting_result = false;
                            saveLogfile(g_logfilePath, "[E6640A] FAIL CODE: [WIFI_IQOffset]\n \n Fail Tai Bai Test 'IQ Offset' \n");
                            return wifiTesting_result;
                        }
                    }

                    //Thông số được lựa chọn trong bài test
                    //Gia tri Amplitude imbalance (dB) (GainImbal?)
                    if (testItems.Contains("AmpIm"))
                    {
                        measureResult = Decimal.Parse(MODulation_Value[15], System.Globalization.NumberStyles.Float);
                        saveLogfile(g_logfilePath, "- Gain Imbalance (Amplitude imbalance) = " + measureResult.ToString("0.####") + " dB");
                        if (check_PassFail_limit(measureResult, -500, MaxAmpI_decimal))
                        {
                            saveLogfile(g_logfilePath, "  PASS\n");
                        }
                        else
                        {
                            saveLogfile(g_logfilePath, "  FAIL\n");
                            wifiTesting_result = false;
                            saveLogfile(g_logfilePath, "[E6640A] FAIL CODE: [WIFI_GainImbal]\n \n Fail Tai Bai Test 'Gain Imbalance' \n");
                            return wifiTesting_result;
                        }
                    }

                    //Thông số được lựa chọn trong bài test
                    //Gia tri QuadError (Phase Imbalance?)
                    if (testItems.Contains("PhaseIm"))
                    {
                        measureResult = Decimal.Parse(MODulation_Value[17], System.Globalization.NumberStyles.Float);
                        saveLogfile(g_logfilePath, "- Quadrature Error (Phase Imbalance) = " + measureResult.ToString("0.####") + " Deg");
                        if (check_PassFail_limit(measureResult, -MaxPhaseI_decimal, MaxPhaseI_decimal))
                        {
                            saveLogfile(g_logfilePath, "  PASS\n");
                        }
                        else
                        {
                            saveLogfile(g_logfilePath, "  FAIL\n");
                            wifiTesting_result = false;
                            saveLogfile(g_logfilePath, "[E6640A] FAIL CODE: [WIFI_QuadError]\n \n Fail Tai Bai Test 'Quadrature Error' \n");
                            return wifiTesting_result;
                        }
                    }
                }
                catch
                {
                    wifiTesting_result = false;
                    saveLogfile(g_logfilePath, "[E6640A] FAIL CODE: [WIFI_Processing] \n \n Loi! xay ra trong qua trinh doc ket qua tu E6640A \n");
                    return wifiTesting_result;
                }
                return wifiTesting_result;
            }
            catch //(Exception Ex)
            {
                //MessageBox.Show(Ex.ToString());
                wifiTesting_result = false;
                saveLogfile(g_logfilePath, "[E6640A] ERROR CODE: [WIFI_Testing] \n \n Loi! xay ra tai qua trinh test Wifi \n");
                return wifiTesting_result;
            }

        }

        public bool checkBusyState(MessageBasedSession mbSession, string command) //OLD-PANDA
        {
            string OK = "0";
            string Command_Line;
            string responseString = "1";
            byte counter = 0;

            if ((command == "*ESE?") || (command == "*ESR?"))
                OK = "0";
            else
                if ((command == "*OPC?") || (command == ":STAT:SRW:MEAS?"))
                    OK = "1";

            while ((responseString.Trim() != OK) && (counter < 500))
            {
                Command_Line = ReplaceCommonEscapeSequences(command + "\n");
                responseString = mbSession.Query(Command_Line);
                //"trim do not work
                //trim only trim space but what you have is /n/r
                //effectively a carriage return that cannot be removed by trime". WeeWen said.
                responseString = responseString.Substring(0, 1);
                Thread.Sleep(500);
                counter++;
            }

            if (counter == 1)
                return false;
            else
                return true;
        }

        // Xóa thông báo trong Logfile
        public void ClearLog()
        {
            try
            {
                File.WriteAllText(g_logfilePath, String.Empty);
            }
            catch
            {
                MessageBox.Show("Không xóa được logfile của E6640A");
            }
        }
        // Lấy thông báo từ Logfile
        public string GetLog()
        {
            string readConfigFile = "";
            if (File.Exists(g_logfilePath))
            {
                StreamReader StreamR = File.OpenText(g_logfilePath);
                readConfigFile = StreamR.ReadToEnd();
                StreamR.Close();
            }
            return readConfigFile;
        }
        /*----------------------------------------------------------*/
        public void LimitTable(decimal PeakPWLow, decimal PeakPWUp, decimal AvgPWLow, decimal AvgPWUp,
                               decimal EVMallUp, decimal EVMDataUp, decimal EVMPilotUp, decimal MaxAmpI,
                               decimal MaxFreqErr, decimal MaxIQ, decimal MaxPhaseI, decimal MaxSymbolCLKErr,
                               decimal MaxPhaseErr)
        {
            //Update limit table 
            this.PeakPWLow_decimal = PeakPWLow;
            this.PeakPWUp_decimal = PeakPWUp;
            this.AvgPWLow_decimal = AvgPWLow;
            this.AvgPWUp_decimal = AvgPWUp;
            this.EVMallUp_decimal = EVMallUp;
            this.EVMDataUp_decimal = EVMDataUp;
            this.EVMPilotUp_decimal = EVMPilotUp;
            this.MaxAmpI_decimal = MaxAmpI;
            this.MaxFreqErr_decimal = MaxFreqErr;
            this.MaxIQ_decimal = MaxIQ;
            this.MaxPhaseI_decimal = MaxPhaseI;
            this.MaxSymbolCLKErr_decimal = MaxSymbolCLKErr;
            this.MaxPhaseErr_decimal = MaxPhaseErr;
            // 
        }
        /*----------------------------------------------------------*/
        /*----------------------*/
        //
        private static void saveLogfile(string pathfile, string content)// 
        {
            try
            {
                if (File.Exists(pathfile))
                {
                    StreamWriter StreamW = File.AppendText(pathfile);
                    StreamW.Write(content);
                    StreamW.Close();
                }
                else
                {
                    StreamWriter StreamW = null;
                    StreamW = File.CreateText(pathfile);
                    StreamW.Write(content);
                    StreamW.Close();
                }
            }
            catch
            {
                MessageBox.Show("Loi save logfile");
            }
            /*---------Lưu logfile kết quả test---------*/
        }
        /*------------------------------------------------------------*/
        public bool config_HT20_RxTest_Transmitter(string frequency, string power, string port)// string packet_number, string waveform_file
        {
            bool enable_nSISO_Testing = false;
            try
            {

                //saveLogfile(g_logfilePath, "[BOL] Khởi tạo lần đầu cho thiết bị!\n");
                // Cấu hình khối Source
                //mbSession.Write(":SOURce:FREQuency " + frequency + "\n");
                //Thread.Sleep(50);
                //mbSession.Write(":SOURce:POWer " + power + "\n");
                //mbSession.Write(":SOURce:PRESet" + "\n");
                //Thread.Sleep(50);
                //mbSession.Write(":SOUR:RAD:ARB:LOAD \"D:\\\\Waveform\\\\" + "001122334455_54M.wfm" + "\"" + "\n");
                //mbSession.Write(":SOUR:RAD:ARB:LOAD \"D:\\\\Waveform\\\\" + "001122334455_H2M7.wfm" + "\"" + "\n");
                //mbSession.Write(":SOUR:LIST:NUMB:STEP 3" + "\n");
                //Thread.Sleep(50);
                //mbSession.Write(":SOUR:LIST:STEP1:SET IMM,0.00000E+00,NONE,DOWN," + frequency + "MHz" + ",-1.2000000E+02,\"CW\",TIME,1.0000E-03,0,1" + "\n");
                //Thread.Sleep(50);
                //mbSession.Write(":SOUR:LIST:STEP2:SET INT,0.00000E+00,NONE,DOWN," + frequency + "MHz" + "," + power + "dBm" + ",\"001122334455_54M.wfm\",COUN," + packet_number + ",0,1" + "\n");
                // Tại sao có STEP1 và STEP2??? 1 cái chỉ có FER còn 1 cái lại có cả FER và PW
                //Thread.Sleep(50);
                //mbSession.Write(":SOUR:LIST:STEP3:SET INT,1.00E-03,NONE,DOWN," + frequency + "MHz" + ",-1.2000000E+02,\"CW\",TIME,1.0000E-03,1,1" + "\n");
                //??? STEP3 dùng để làm gì?
                //mbSession.Write(":SOUR:LIST:STEP1:SET IMM, 1ms, NONE, DOWN, " + frequency + "MHz, " + power + "dBm, \"001122334455_54M.wfm\", COUN, " + packet_number + ", OFF, 255" + "\n");
                //mbSession.Write(":SOUR:LIST:STEP1:SET IMM, 1ms, NONE, DOWN, " + frequency + "MHz, " + power + "dBm, \"" + waveform_file + "\", COUN, " + packet_number + ", OFF, 255" + "\n");
                //Thread.Sleep(50);
                //mbSession.Write(":SOUR:LIST:STEP2:SET IMM, 1ms, NONE, DOWN, 1000 MHz, -100 dBm, \"Off\", TIME, 1, 0, 1" + "\n");
                //Thread.Sleep(50);
                mbSession.Write(":SOURce:PRESet" + "\n");
                Thread.Sleep(50);
                mbSession.Write("INST:SEL WLAN" +"\n"); // cau lenh chi den WLAN
                mbSession.Write(":FEED:RF:PORT:OUTP "+ port + "\n");
                mbSession.Write(":SOUR:LIST:STEP1:SET:AMPL " + power + "dBm" + "\n");
                mbSession.Write(":SOUR:LIST:STEP1:SET:CNFR " + frequency + "MHz" + "\n");
                mbSession.Write(":SOUR:LIST:STEP1:SET:WAV \"CW\"" + "\n");
                mbSession.Write(":SOUR:LIST:STEP1:SET:DUR:TYPE CONT" + "\n");
                mbSession.Write(":SOUR:LIST ON" + "\n");//??         
                mbSession.Write(":SOUR:LIST:TRIG" + "\n");//??

            }
            catch //(Exception a)
            {
                enable_nSISO_Testing = false;
                config_done = true;
                //MessageBox.Show("Fail at WLAN_Config: " + a.Message); 
                saveLogfile(g_logfilePath, "[E6640A]ERROR CODE: [Equip_Config] \n Error tai qua trinh cau hinh cho thiet bi do E6640A \n");
            }
            return enable_nSISO_Testing;
        }


        public void config_HT20_RxTest_OUTPUT_OFF()
        {
            mbSession.Write(":OUTPut OFF\n");
        }
        public bool config_HT20_RxTest_Receiver(string frequency,string port)
        {
            bool enable_nSISO_Testing = false;
            //bool wifiTesting_result1 = true;
            try
            {
                //string result_Value1 = string.Empty;
                //mbSession.Write(":SOURce:PRESet" + "\n");
                //Thread.Sleep(50);
                mbSession.Write(":FEED:RF:PORT " +port+ "\n");
                mbSession.Write("FREQ:CENT " + frequency + "MHz" + "\n");
             
            }
            catch //(Exception a)
            {
                //MessageBox.Show(Ex.ToString());
                enable_nSISO_Testing = false;
                saveLogfile(g_logfilePath, "[E6640A] ERROR CODE: [WIFI_Testing] \n \n Loi! xay ra tai qua trinh test Wifi \n");
            }
            return enable_nSISO_Testing;   
        }
            
        public string HienThi()
        {
            //bool wifiTesting_result1 = true;
            try
            {
                string result_Value1 = string.Empty;
                //mbSession.Write("FETCh:CHPower:CHPower?" + "\n");
                result_Value1 = mbSession.Query("READ:CHPower1?");
                Thread.Sleep(100);
                result_Value1 = InsertCommonEscapeSequences(result_Value1);
                return result_Value1;

            }
            catch (Exception Ex)
            {
                //wifiTesting_result1 = false;
                saveLogfile(g_logfilePath, "[E6640A] FAIL CODE: [WIFI_Processing] \n \n Loi! xay ra trong qua trinh doc ket qua tu E6640A \n");
                return "FAIL - " + Ex.ToString();
            }
        }
    }
}
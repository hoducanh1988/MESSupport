using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFAttenuation.Function.Instrument {

    public abstract class IInstrument {

        #region khai bao bien
        protected MessageBasedSession mbSession;
        protected string g_logfilePath;
        public int minEquip_DELAY = 50;
        public int avgEquip_DELAY = 100;
        public int maxEquip_DELAY = 200;
        public string logfile = "";
        protected string Cable_Loss = "";

        protected int TimeOutMs = 400;
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

        public string Frequency_Error;
        public string Power_measured;
        public string EVM_measured;

        public string g_MainConfigFilePath = @"Config\Main-Config.cfg";

        //---Flag kiểm tra trạng thái cấu hình máy đo đã hoàn thành hay chưa
        protected bool config_done = false;
        public bool check_config_done {
            get { return config_done; }
            set { config_done = value; }
        }

        #endregion

        #region abstract function

        public abstract bool config_Attenuator_Total(string transmitPort, string receivePort, string power);
        public abstract void config_Attenuator_Transmitter(string frequency, string power, string transmitPort);
        public abstract string config_Attenuator_Receiver(string frequency, string receivePort);
        public abstract bool config_Instrument_Total(string port, string Standard, ref string error);
        public abstract bool config_Instrument_Channel(string channel_Freq, ref string error);
        public abstract string config_Instrument_get_FreqErr(string Trigger, string wifi);
        public abstract string config_Instrument_get_Power(string Trigger, string wifi);
        public abstract string config_Instrument_get_TotalResult(string Trigger, string wifi);
        public abstract bool config_HT20_RxTest_MAC(string channel, string power, string packet_number, string waveform_file, string port);
        public abstract string config_Instrument_get_EVM(string Trigger, string wifi);
        public abstract bool config_Instrument_Port(string port, ref string error);
        public abstract bool config_Instrument_OutputPort(string port, ref string error);
        #endregion

        #region Cac ham su dung chung


        public void ClearCommand() {
            mbSession.Clear();
        }

        public void Close() {
            if (mbSession != null) mbSession.Dispose();
        }

        protected string ReplaceCommonEscapeSequences(string s) {
            return s.Replace("\\n", "\n").Replace("\\r", "\r");
        }

        protected string InsertCommonEscapeSequences(string s) {
            return s.Replace("\n", "\\n").Replace("\r", "\\r");
        }

        protected bool checkBusyState(MessageBasedSession mbSession, string command) {
            string OK = "0";
            string Command_Line;
            string responseString = "1";
            byte counter = 0;

            if ((command == "*ESE?") || (command == "*ESR?"))
                OK = "0";
            else
                if ((command == "*OPC?") || (command == ":STAT:SRW:MEAS?"))
                OK = "1";

            while ((responseString.Trim() != OK) && (counter < 255)) {
                Command_Line = ReplaceCommonEscapeSequences(command + "\n");
                responseString = mbSession.Query(Command_Line);
                responseString = responseString.Substring(0, 1);
                Thread.Sleep(250);
                counter++;
            }

            if (counter == 1)
                return false;
            else
                return true;
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
        public bool check_PassFail_limit(Decimal value, Decimal lowerlimit, Decimal upperlimit) {
            bool result = false;
            if ((value <= upperlimit) && (value >= lowerlimit)) {
                result = true;
            }
            else {
                result = false;
            }
            return result;
        }

        #endregion

    }


}

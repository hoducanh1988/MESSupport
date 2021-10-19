using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFAttenuation.Function.Instrument {

    public class E6640A<T> : IInstrument where T : class, new() {
        T t = null;

        public E6640A(T _t, string MeasureEquip_IP, out bool result) {
            try {
                this.t = _t;
                mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(MeasureEquip_IP);
                result = true;
            }
            catch (Exception ex) {
                result = false;
            };
        }

        #region measure attenuator

        public override bool config_Attenuator_Total(string transmitPort, string receivePort, string power) {
            return true;
        }

        public override void config_Attenuator_Transmitter(string frequency, string power, string transmitPort)// string packet_number, string waveform_file
       {
            try {
                transmitPort = string.Format("RFIO{0}", transmitPort.Replace("RFIO", "").Replace("PORT", ""));
                this.Write(":SOURce:PRESet" + "\n");
                Thread.Sleep(50);
                this.Write("INST:SEL WLAN" + "\n"); // cau lenh chi den WLAN
                this.Write(":FEED:RF:PORT:OUTP " + transmitPort + "\n");
                this.Write(":SOUR:LIST:STEP1:SET:AMPL " + power + "dBm" + "\n");
                this.Write(":SOUR:LIST:STEP1:SET:CNFR " + frequency + "MHz" + "\n");
                this.Write(":SOUR:LIST:STEP1:SET:WAV \"CW\"" + "\n");
                this.Write(":SOUR:LIST:STEP1:SET:DUR:TYPE CONT" + "\n");
                this.Write(":SOUR:LIST ON" + "\n");//??         
                this.Write(":SOUR:LIST:TRIG" + "\n");//??
            }
            catch { }
        }

        public override string config_Attenuator_Receiver(string frequency, string receivePort) {
            try {
                receivePort = string.Format("RFIO{0}", receivePort.Replace("RFIO", "").Replace("PORT", ""));
                this.Write(":FEED:RF:PORT " + receivePort + "\n");
                this.Write("FREQ:CENT " + frequency + "MHz" + "\n");
                mbSession.Write("READ:CHPower1?");
                Thread.Sleep(100);
                string result_Value1 = this.Read();
                result_Value1 = InsertCommonEscapeSequences(result_Value1);
                result_Value1 = result_Value1.Split(',')[0];
                return result_Value1;
            }
            catch //(Exception a)
            {
                return null;
            }
        }

        #endregion


        //------------------ Hàm thiết lập cấu hình cho máy đo lần đầu tiên ---------------------
        public override bool config_Instrument_Total(string port, string Standard, ref string error) {
            bool enable_nSISO_Testing = false;
            try {
                string _wifiStandard = "";
                switch (Standard) {
                    case "g": { _wifiStandard = "GDO"; break; }
                    case "b": { _wifiStandard = "BG"; break; }
                    case "a": { _wifiStandard = "AG"; break; }
                    case "n20": { _wifiStandard = "N20"; break; }
                    case "n40": { _wifiStandard = "N40"; break; }
                    case "ac20": { _wifiStandard = "AC20"; break; }
                    case "ac40": { _wifiStandard = "AC40"; break; }
                    case "ac80": { _wifiStandard = "AC80"; break; }
                    case "ac160": { _wifiStandard = "AC160"; break; }
                }
                string PortName = string.Format("RFIO{0}", port.Replace("RFIO", "").Replace("PORT", ""));

                this.Write("*CLS\n");
                this.Write("*RST\n");
                this.Write(":SYSTem:PRESet\n"); //Preset máy đo
                this.Write(":FEED:RF:PORT " + PortName + "\n"); //Chọn RF input
                this.Write("INST:SEL WLAN\n"); //Thiết lập chế độ WLAN
                this.Write(":RAD:STAN " + _wifiStandard + "\n"); //Thiết lập chuẩn b/g/n kèm bandwidth
                this.Write(":INITiate:EVM\n"); //Thiết lập đo ở chế độ EVM

                enable_nSISO_Testing = true;
            }
            catch (Exception ex) {
                error = ex.ToString();
                enable_nSISO_Testing = false;
                config_done = true;
            }
            return enable_nSISO_Testing;
        }


        //------------------ Hàm thiết lập tần số kênh ---------------------
        public override bool config_Instrument_Channel(string channel_Freq, ref string error) {
            bool enable_nSISO_Testing = false;
            try {
                this.Write("FREQ:CENT " + channel_Freq + "\n");
                enable_nSISO_Testing = true;
                config_done = true;
            }
            catch (Exception ex) {
                error = ex.ToString();
                enable_nSISO_Testing = false;
                config_done = true;
            }
            return enable_nSISO_Testing;
        }


        //------------------ Hàm thiết lập cổng RFIO ---------------------
        public override bool config_Instrument_Port(string port, ref string error) {
            try {
                string PortName = string.Format("RFIO{0}", port.Replace("RFIO", "").Replace("PORT", ""));
                this.Write(string.Format(":FEED:RF:PORT {0}\n", PortName));
                Thread.Sleep(1000);
                return true;
            }
            catch (Exception ex) {
                error = ex.ToString();
                return false;
            }
        }

        //------------------ Hàm thiết lập cổng output RFIO ---------------------
        public override bool config_Instrument_OutputPort(string port, ref string error) {
            try {
                string PortName = string.Format("RFIO{0}", port.Replace("RFIO", "").Replace("PORT", ""));
                this.Write(":SYSTem:PRESet\n"); //Preset máy đo
                //string PortName = string.Format("PORT{0}", port.Replace("RFIO", "").Trim());
                this.Write(string.Format(":FEED:RF:PORT:OUTP {0}\n", PortName));
                Thread.Sleep(1000);
                return true;
            }
            catch (Exception ex) {
                error = ex.ToString();
                return false;
            }
        }

        //------------------ Hàm đọc về độ lệch tần số ---------------------
        public override string config_Instrument_get_FreqErr(string Trigger, string wifi) {
            try {
                string result_Value = string.Empty;

                this.Write("TRIG:EVM:SOUR " + Trigger + "\n");
                this.Write(":POW:RANG 30.0\n");
                this.Write(":INIT:CONT 0\n");
                mbSession.Write("READ:EVM?");
                result_Value = this.Read();
                result_Value = InsertCommonEscapeSequences(result_Value);
                if (result_Value.Trim() == "") {
                    return null;
                }
                else {
                    try {
                        string[] MODulation_Value = result_Value.Split(new Char[] { ',' });
                        Decimal measureResult = 0;
                        measureResult = Decimal.Parse(MODulation_Value[7], System.Globalization.NumberStyles.Float);
                        return measureResult.ToString();
                    }
                    catch {
                        return null;
                    }
                }
            }
            catch {
                return null;
            }
        }


        //------------------ Hàm đọc về công suất ---------------------
        public override string config_Instrument_get_Power(string Trigger, string wifi) {
            try {
                string result_Value = string.Empty;
                this.Write("TRIG:EVM:SOUR " + Trigger + "\n");
                this.Write(":POW:RANG 30.0\n");
                this.Write(":INIT:CONT 0\n");
                mbSession.Write("READ:EVM?");
                result_Value = this.Read();
                result_Value = InsertCommonEscapeSequences(result_Value);
                if (result_Value.Trim() == "") {
                    return null;
                }
                else {
                    try {
                        string[] MODulation_Value = result_Value.Split(new Char[] { ',' });
                        Decimal measureResult = 0;
                        measureResult = Decimal.Parse(MODulation_Value[19], System.Globalization.NumberStyles.Float);
                        return measureResult.ToString();
                    }
                    catch {
                        return null;
                    }
                }
            }
            catch {
                return null;
            }
        }

        //------------------ Hàm đọc về EVM ---------------------
        public override string config_Instrument_get_EVM(string Trigger, string wifi) {
            try {
                string result_Value = string.Empty;
                this.Write("TRIG:EVM:SOUR " + Trigger + "\n");
                this.Write(":POW:RANG 30.0\n");
                this.Write(":INIT:CONT 0\n");
                this.Write("READ:EVM?");
                result_Value = this.Read();
                result_Value = InsertCommonEscapeSequences(result_Value);
                if (result_Value.Trim() == "") {
                    return null;
                }
                else {
                    try {
                        string[] MODulation_Value = result_Value.Split(new Char[] { ',' });
                        Decimal measureResult = 0;
                        measureResult = Decimal.Parse(MODulation_Value[1], System.Globalization.NumberStyles.Float);
                        return measureResult.ToString();
                    }
                    catch {
                        return null;
                    }
                }
            }
            catch {
                return null;
            }
        }


        //------------------ Hàm đọc tất cả các kết quả, rồi mới Split ra từng thông số ---------------------
        public override string config_Instrument_get_TotalResult(string Trigger, string wifi) {
            string result_Value = string.Empty;
            try {
                this.Write("TRIG:EVM:SOUR " + Trigger + "\n");
                this.Write(":POW:RANG 30.0\n");
                this.Write(":INIT:CONT 0\n");
                mbSession.Write("READ:EVM?");
                result_Value = this.Read();
                result_Value = InsertCommonEscapeSequences(result_Value);
                if (result_Value.Trim() == "") {
                    return "NULL";
                }
                else {
                    return result_Value;
                }
            }
            catch {
                return "ERROR";
            }
        }


        //------------------ Hàm đọc cấu hình máy đo phát TX ---------------------
        public override bool config_HT20_RxTest_MAC(string channel, string power, string packet_number, string waveform_file, string port) {
            bool enable_nSISO_Testing = false;
            try {

                string frequency = "";
                frequency = channel;
                this.Write(":SOURce:PRESet" + "\n");
                this.Write(":SOUR:RAD:ARB:LOAD \"D:\\\\Waveform\\\\" + waveform_file + "\"" + "\n");
                this.Write(":SOUR:LIST:NUMB:STEP 3" + "\n");   //Tạo 3 step
                this.Write(":SOUR:LIST:STEP1:SET IMM,0.00000E+00,NONE,DOWN," + frequency + "MHz" + ",-1.2000000E+02,\"CW\",TIME,1.0000E-03,0,1" + "\n");   //Thiết lập thông số cho step1
                this.Write(":SOUR:LIST:STEP2:SET INT,0.00000E+00,NONE,DOWN," + frequency + "MHz" + "," + power + "dBm" + ",\"" + waveform_file + "\",COUN," + packet_number + ",0,1" + "\n");   //Thiết lập thông số cho step2
                this.Write(":SOUR:LIST:STEP3:SET INT,1.00E-03,NONE,DOWN," + frequency + "MHz" + ",-1.2000000E+02,\"CW\",TIME,1.0000E-03,1,1" + "\n");  //Thiết lập thông số cho step3
                this.Write(":SOUR:LIST ON" + "\n");
                this.Write(":SOUR:LIST:TRIG" + "\n");  //Bắt đầu phát waveform
            }
            catch {
                enable_nSISO_Testing = false;
                config_done = true;
            }
            return enable_nSISO_Testing;
        }


        bool Write(string cmd) {
            int count = 0;
        RE:
            count++;
            mbSession.Write(cmd);
            PropertyInfo pi = t.GetType().GetProperty("logInstrument");
            string s = pi.GetValue(t, null) as string;
            s += cmd;
            pi.SetValue(t, Convert.ChangeType(s, pi.PropertyType), null);

            mbSession.Write(":SYSTem:ERRor?\n");
            string data = this.Read();
            bool r = data.ToLower().Contains("no error");
            if (!r) {
                if (count < 3) goto RE;
            }
            return r;
        }

        string Read() {
            string data = mbSession.ReadString();
            PropertyInfo pi = t.GetType().GetProperty("logInstrument");
            string s = (string)pi.GetValue(t, null);
            s += data;
            pi.SetValue(t, Convert.ChangeType(s, pi.PropertyType), null);
            return data;
        }
    }

}

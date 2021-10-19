using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFAttenuation.Function.Instrument {

    public class MT8870A<T> : IInstrument where T : class, new() {

        T t = null;

        public MT8870A(T _t, string MeasureEquip_IP, out bool result) {
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
            try {
                receivePort = string.Format("PORT{0}", receivePort.Replace("RFIO", "").Replace("PORT", ""));
                transmitPort = string.Format("PORT{0}", transmitPort.Replace("RFIO", "").Replace("PORT", ""));
                this.Write("*CLS\n");
                this.Write("*RST\n");
                this.Write(":SYST:LANG SCPI\n");
                this.Write(":INST:SEL SRW\n");
                this.Write(":CONF:SRW:SEGM:APP CW\n");
                this.Write(":SOUR:GPRF:GEN:MODE NORMAL\n");
                this.Write(":SOUR:GPRF:GEN:RFS:LEV " + power + "\n");
                this.Write(":ROUT:PORT:CONN:DIR " + receivePort + "," + transmitPort + "\n"); // Thiết lập cấu hình PORT: PORT1 – VSA, PORT3 - VSG
                this.Write(":SOUR:GPRF:GEN:BBM CW\n");// Thiết lập VSG ở chế độ phát CW
                this.Write(":SOUR:GPRF:GEN:ARB:NOIS:STAT OFF\n");
                return true;
            }
            catch { return false; }
        }


        public override void config_Attenuator_Transmitter(string frequency, string power, string transmitPort) {
            try {
                // Mỗi lần phát tín hiệu cần gửi những lệnh dưới
                this.Write(":SOUR:GPRF:GEN:RFS:FREQ " + frequency + "000000HZ\n");      // Thiết lập tần số phát
                Thread.Sleep(10);
                this.Write(":SOUR:GPRF:GEN:STAT ON\n");               // Phát công suất
                Thread.Sleep(200);
            }
            catch {
            }
        }

        public override string config_Attenuator_Receiver(string frequency, string receivePort) {
            string result = "";
            try {

                // mbSession.Write(":CONF:SRW:SEGM:PORT " + receivePort + "\n");           // Lệnh cấu hình PORT cho VSA
                this.Write(":CONF:SRW:FREQ " + frequency + "000000HZ\n");      // Thiết lập tần số thu
                Thread.Sleep(10);
                this.Write(":CONF:SRW:ALEV:TIME 0.005\n");
                Thread.Sleep(10);
                this.Write(":INIT:SRW:ALEV\n"); //Thiết lập Power lever là auto level
                Thread.Sleep(100);
                mbSession.Write(":FETC:SRW:SUMM:CW:POW? 1\n");    // Lệnh đo công suất: -12.25 là công suất trung bình)(1, 0, -12.25, -12.08, 1, 1)
                Thread.Sleep(100);
                result = this.Read();
                Thread.Sleep(10);
                //this.Write(":SOUR:GPRF:GEN:STAT OFF\n");			//Lệnh OFF công suất
                string[] buffer = result.Split(',');
                if (buffer.Length != 6) return "";
                else result = buffer[2];
                return result;
            }
            catch {
                return result;
            }
        }

        #endregion

        #region New

        public override bool config_Instrument_Total(string port, string Standard, ref string error) {
            try {
                bool r = false;
                string _wifiStandard = "";
                switch (Standard) {
                    case "b": {
                            _wifiStandard = "DSSS";
                            break;
                        }
                    default: {
                            _wifiStandard = "OFDM";
                            break;
                        }
                }

                string PortName = string.Format("PORT{0}", port.Replace("RFIO", "").Replace("PORT", ""));
                r = this.Write("*RST\n"); if (!r) return false;
                r = this.Write(":CONF:SRW:SEGM:CLE\n"); if (!r) return false;
                r = this.Write(string.Format(":CONF:SRW:SEGM:APP AUTO{0}\n", _wifiStandard)); if (!r) return false;
                r = this.Write(":CONF:SRW:TRIG LEVEL\n"); if (!r) return false;
                r = this.Write(":CONF:SRW:TDEL -1E-05\n"); if (!r) return false;
                r = this.Write(":CONF:SRW:TLEV -15\n"); if (!r) return false;
                r = this.Write(":CONF:SRW:CAPT:MODE PACKET\n"); if (!r) return false;
                r = this.Write(":CONF:SRW:TTIM 1\n"); if (!r) return false;
                r = this.Write(":CONF:SRW:PACK 20\n"); if (!r) return false;
                r = this.Write(_wifiStandard == "DSSS" ? ":CONF:SRW:WLB:FTYP GAUSSIAN\n" : ":CONF:SRW:OFDM:CEST FULLPACKET\n"); if (!r) return false;
                r = this.Write(":CONF:SRW:SEL:WLAN:POW ON\n"); if (!r) return false;
                r = this.Write(":CONF:SRW:SEL:WLAN:EVM ON\n"); if (!r) return false;
                r = this.Write(":CONF:SRW:POW 20\n"); if (!r) return false;
                r = this.Write(string.Format(":ROUT:PORT:CONN:DIR {0},{1}\n", PortName, PortName)); if (!r) return false;
                r = this.Write(":CONF:SRW:ALEV:TIME 0.01\n"); if (!r) return false;
                return r;
            }
            catch (Exception ex) {
                error = ex.ToString();
                return false;
            }
        }

        public override bool config_Instrument_Channel(string channel_Freq, ref string error) {
            try {
                bool r = this.Write(":CONF:SRW:FREQ " + channel_Freq + "\n"); //Thiết lập chế độ WLAN
                return r;
            }
            catch (Exception ex) {
                error = ex.ToString();
                return false;
            }
        }

        public override bool config_Instrument_Port(string port, ref string error) {
            try {
                string PortName = string.Format("PORT{0}", port.Replace("RFIO", "").Replace("PORT", ""));
                bool r = this.Write(string.Format(":ROUT:PORT:CONN:DIR {0},{1}\n", PortName, PortName));
                mbSession.Write(":ROUT:PORT:CONN:DIR?\n");
                string data = this.Read();
                string pattern = string.Format("{0},{0}", PortName);
                r = data.ToLower().Contains(pattern.ToLower());
                return r;
            }
            catch (Exception ex) {
                error = ex.ToString();
                return false;
            }
        }

        public override bool config_Instrument_OutputPort(string port, ref string error) {
            try {
                string PortName = string.Format("PORT{0}", port.Replace("RFIO", "").Replace("PORT", ""));
                bool r = this.Write(string.Format(":ROUT:PORT:CONN:DIR {0},{1}\n", PortName, PortName));
                mbSession.Write(":ROUT:PORT:CONN:DIR?\n");
                string data = this.Read();
                string pattern = string.Format("{0},{0}", PortName);
                r = data.ToLower().Contains(pattern.ToLower());
                return r;
            }
            catch (Exception ex) {
                error = ex.ToString();
                return false;
            }
        }

        public override string config_Instrument_get_FreqErr(string Trigger, string wifi) {
            try {
                bool r = false;
                r = this.Write(":INIT:SRW:ALEV\n");
                r = this.Write("*WAI\n");
                mbSession.Write(":CONF:SRW:POW?\n");
                this.Read();
                r = this.Write(":INIT:SRW\n");
                r = this.Write("*WAI\n");

                int counter = 0;
                while (true) {
                    mbSession.Write(":STAT:SRW:MEAS?\n");
                    string x = this.Read();
                    if (x.Replace("\r", "").Replace("\n", "").Trim() == "1") break;
                    Thread.Sleep(100);
                    if (counter >= 30) return "";
                }

                counter = 0;
            REP:
                counter++;
                mbSession.Write(string.Format(":FETC:SRW:SUMM:WLAN:{0}:EVM? 1\n", wifi == "b" ? "DSSS" : "OFDM"));
                string tmpStr = this.Read();
                string[] buffer = tmpStr.Split(',');
                double data = 0;
                bool ret = false;
                try {
                    ret = double.TryParse(wifi == "b" ? buffer[10] : buffer[7], out data);
                }
                catch { }
                if (ret == false) {
                    if (counter < 3) goto REP;
                }
                return wifi == "b" ? buffer[10] : buffer[7];
            }
            catch {
                return "";
            }
        }


        public override string config_Instrument_get_Power(string Trigger, string wifi) {
            try {
                bool r = this.Write(":INIT:SRW:ALEV\n");
                this.Write("*WAI\n");
                mbSession.Write(":CONF:SRW:POW?\n");
                this.Read();
                this.Write(":INIT:SRW\n");
                this.Write("*WAI\n");

                int counter = 0;
                while (true) {
                    counter++;
                    mbSession.Write(":STAT:SRW:MEAS?\n");
                    string x = this.Read();
                    if (x.Replace("\r", "").Replace("\n", "").Trim() == "1") break;
                    Thread.Sleep(100);
                    if (counter >= 30) return "";
                }

                counter = 0;
            REP:
                counter++;
                mbSession.Write(":FETC:SRW:SUMM:WLAN:POW? 1,1\n");
                string tmpStr = this.Read();
                string[] buffer = tmpStr.Split(',');
                double data = 0;
                bool ret = false;
                try {
                    ret = double.TryParse(buffer[6], out data);
                }
                catch { }
                if (ret == false) {
                    if (counter < 3) goto REP;
                }
                return buffer[6];
            }
            catch {
                return "";
            }
        }


        public override string config_Instrument_get_EVM(string Trigger, string wifi) {
            try {
                bool r = this.Write(":INIT:SRW:ALEV\n");
                this.Write("*WAI\n");
                mbSession.Write(":CONF:SRW:POW?\n");
                this.Read();
                r = this.Write(":INIT:SRW\n");
                r = this.Write("*WAI\n");

                int counter = 0;
                while (true) {
                    mbSession.Write(":STAT:SRW:MEAS?\n");
                    string x = this.Read();
                    if (x.Replace("\r", "").Replace("\n", "").Trim() == "1") break;
                    Thread.Sleep(100);
                    if (counter >= 30) return "";
                }

                counter = 0;
            REP:
                counter++;
                mbSession.Write(string.Format(":FETC:SRW:SUMM:WLAN:{0}:EVM? 1\n", wifi == "b" ? "DSSS" : "OFDM"));
                string tmpStr = this.Read();
                string[] buffer = tmpStr.Split(',');
                double data = 0;
                bool ret = false;
                try {
                    ret = double.TryParse(wifi == "b" ? buffer[7] : buffer[12], out data);
                }
                catch { }
                if (ret == false) {
                    if (counter < 3) goto REP;
                }
                return wifi == "b" ? buffer[7] : buffer[12];
            }
            catch {
                return "";
            }
        }


        //------------------ Hàm đọc tất cả các kết quả, rồi mới Split ra từng thông số ---------------------
        public override string config_Instrument_get_TotalResult(string Trigger, string wifi) {
            try {
                string[] temp = new string[] { "0", "EVM", "2", "3", "4", "5", "6", "Freq", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "Power" };
                bool r = this.Write(":INIT:SRW:ALEV\n");
                r = this.Write("*WAI\n");
                mbSession.Write(":CONF:SRW:POW?\n");
                this.Read();
                r = this.Write(":INIT:SRW\n");
                r = this.Write("*WAI\n");

                int counter = 0;
                while (true) {
                    mbSession.Write(":STAT:SRW:MEAS?\n");
                    string x = this.Read();
                    if (x.Replace("\r", "").Replace("\n", "").Trim() == "1") break;
                    Thread.Sleep(100);
                    if (counter >= 30) return "";
                }

                counter = 0;
                double data = 0;
                bool ret = false;
            REP1:
                counter++;
                mbSession.Write(":FETC:SRW:SUMM:WLAN:POW? 1,1\n");
                string tmpStr = this.Read();
                string[] buffer = tmpStr.Split(',');
                temp[19] = buffer[6]; //Power
                try {
                    ret = double.TryParse(buffer[6], out data);
                }
                catch { }
                if (ret == false) {
                    if (counter < 3) goto REP1;
                }

                counter = 0;
                data = 0;
                ret = false;
            REP2:
                counter++;
                mbSession.Write(string.Format(":FETC:SRW:SUMM:WLAN:{0}:EVM? 1\n", wifi == "b" ? "DSSS" : "OFDM"));
                tmpStr = this.Read();
                buffer = tmpStr.Split(',');
                temp[7] = wifi == "b" ? buffer[9] : buffer[7]; //Freq Error
                temp[1] = wifi == "b" ? buffer[7] : buffer[12]; //EVM
                try {
                    ret = double.TryParse(temp[7], out data) && double.TryParse(temp[1], out data);
                }
                catch { }
                if (ret == false) {
                    if (counter < 3) goto REP2;
                }

                string result = "";
                for (int i = 0; i < temp.Length; i++) {
                    result += temp[i] + ",";
                }
                return result;
            }
            catch {
                return "";
            }
        }

        public override bool config_HT20_RxTest_MAC(string channel, string power, string packet_number, string waveform_file, string Port) {
            try {
                string subStr = "";
                string PortName = string.Format("PORT{0}", Port.Replace("RFIO", "").Replace("PORT", ""));
                bool r = this.Write(":SOUR:GPRF:GEN:MODE NORMAL\n");
                r = this.Write(":SOUR:GPRF:GEN:ARB:FILE:LOAD 'ZERO_200000000Hz_100000p'\n");
                r = this.Write(string.Format(":ROUT:PORT:CONN:DIR {0},{1}\n", PortName, PortName));
                r = this.Write(":SOUR:GPRF:GEN:SEQ:REIN 0\n");
                r = this.Write(":SOUR:GPRF:GEN:STAT 1\n");
                r = this.Write(":SOUR:GPRF:GEN:BBM CW\n");
                r = this.Write(":SOUR:GPRF:GEN:RFS:LEV -100\n");
                r = this.Write(string.Format(":SOUR:GPRF:GEN:ARB:FILE:LOAD '{0}'\n", waveform_file));
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:GEN:SST 1,1,2\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:GEN:REP 1,SINGLE\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:GEN:GOTO 1,1\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:ENDC 1,1,TRIGGER\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:ENDC 1,2,SNUMBER\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:TRIG:SOUR 1,1,WFGEND\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:TRIG:SOUR 1,2,WFGEND\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:TRIG:DEL 1,1,0.000\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:TRIG:DEL 1,2,0.000\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:NSLC 1,1,NSEGMENT\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:NSLC 1,2,LOOP\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:BBM 1,1,ARB\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:BBM 1,2,ARB\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:WCTR 1,1,OFF\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:WCTR 1,2,OFF\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:WAV:GEN:DM:POL 1,NORMAL\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:RX:LEV 1,2,-100.0DBM\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:WAV:PATT:SEL 1,1,'ZERO_200000000Hz_100000p',1\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:WAV:ENDC 1,1,REPEAT\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:WAV:IREP 1,1,1\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:WAV:GETR 1,1,0\n");
                r = this.Write(string.Format(":SOUR:GPRF:GEN:SEQ:WAV:PATT:SEL 1,2,'{0}',1\n", waveform_file));
                r = this.Write(":SOUR:GPRF:GEN:SEQ:WAV:ENDC 1,2,REPEAT\n");
                r = this.Write(string.Format(":SOUR:GPRF:GEN:SEQ:WAV:IREP 1,2,{0}\n", packet_number));
                r = this.Write(":SOUR:GPRF:GEN:SEQ:WAV:GETR 1,2,1\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:WAV:PATT:SEL 1,1,'ZERO_200000000Hz_100000p',1\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:WAV:ENDC 1,3,REPEAT\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:WAV:GETR 1,3,0\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:WAV:PATT:DEL 1,4\n");
                r = this.Write(":SOUR:GPRF:GEN:SEQ:COMB:PATT 1,1\n");
                r = this.Write(":SOUR:GPRF:GEN:MODE SEQUENCE\n");
                r = this.Write(string.Format(":SOUR:GPRF:GEN:SEQ:RX:LEV 1,1,{0}DBM\n", power));
                r = this.Write(string.Format(":SOUR:GPRF:GEN:SEQ:RX:FREQ 1,1,{0}000000HZ\n", channel));
                r = this.Write(string.Format(":SOUR:GPRF:GEN:SEQ:RX:FREQ 1,2,{0}000000HZ\n", channel));
                r = this.Write(string.Format(":SOUR:GPRF:GEN:SEQ:RX:OUTP:STAT 1,1,{0}\n", PortName));
                r = this.Write(string.Format(":SOUR:GPRF:GEN:SEQ:RX:OUTP:STAT 1,2,{0}\n", PortName));
                int counter = 0;
                while (counter < 10) {
                    counter++;
                    mbSession.Write(":SOUR:GPRF:GEN:SEQ:RX:ERR? 1\n");
                    subStr = this.Read().Replace("\r", "").Replace("\n", "").Trim();
                    if (subStr == "0,0,0") counter = 199;
                    else Thread.Sleep(100);
                }
                if (counter != 199) return false;

                r = this.Write(":SOUR:GPRF:GEN:SEQ:EXEC\n");

                counter = 0;
                while (counter < 10) {
                    counter++;
                    mbSession.Write(":SOUR:GPRF:GEN:SEQ:STAT?\n");
                    subStr = this.Read().Replace("\r", "").Replace("\n", "").Trim();
                    if (subStr == "0") counter = 199;
                    else Thread.Sleep(100);
                }
                if (counter != 199) return false;

                return true;
            }
            catch {
                return false;
            }
        }


        #endregion

        bool Write(string cmd) {
            int count = 0;
        RE:
            count++;
            mbSession.Write(cmd);
            PropertyInfo pi = t.GetType().GetProperty("logInstrument");
            string s = pi.GetValue(t, null) as string;
            s += cmd;
            pi.SetValue(t, s, null);

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
            string s = pi.GetValue(t, null) as string;
            s += data;
            pi.SetValue(t, s, null);
            return data;
        }

    }


}

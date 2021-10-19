using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NationalInstruments.VisaNS;

namespace MeshDemoCalibrationBoard.Function {
    public class MT8870A : IInstrument {

        private MessageBasedSession mbSession;

        public MT8870A(string gpib_port, string power_transmit, string receive_port, string transmission_port) : base(gpib_port, power_transmit, receive_port, transmission_port) {
            try {
                // g_logfilePath = @"WIFI_LOGFILE_MT8870.LOG";
                mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(gpib_port);
                //-------------------------------------Global------------------------------------------------------//
                mbSession.Write("*CLS\n");
                Thread.Sleep(10);
                mbSession.Write("*RST\n");
                Thread.Sleep(10);
                mbSession.Write(":SYST:LANG SCPI\n");
                Thread.Sleep(10);//Lệnh thiết lập chế độ SCPI
                mbSession.Write(":INST:SEL SRW\n");
                Thread.Sleep(10);
                mbSession.Write(":CONF:SRW:SEGM:APP CW\n");
                Thread.Sleep(10);
                //-------------------------------------VSG------------------------------------------------------------//
                mbSession.Write(":SOUR:GPRF:GEN:MODE NORMAL\n");
                Thread.Sleep(10);
                mbSession.Write(":SOUR:GPRF:GEN:RFS:LEV " + power_transmit + "\n");           // Thiết lập công suất phát (-10dBm)//+ "DBM
                Thread.Sleep(10);
                mbSession.Write(":ROUT:PORT:CONN:DIR " + receive_port + "," + transmission_port + "\n"); // Thiết lập cấu hình PORT: PORT1 – VSA, PORT3 - VSG
                Thread.Sleep(10);
                mbSession.Write(":SOUR:GPRF:GEN:BBM CW\n");// Thiết lập VSG ở chế độ phát CW
                Thread.Sleep(10);
                mbSession.Write(":SOUR:GPRF:GEN:ARB:NOIS:STAT OFF\n");
                Thread.Sleep(10);
                isConnected = true;
            }
            catch {
                isConnected = false;
                System.Windows.MessageBox.Show("[MT8870A_VISA]Không kết nối được với máy đo IP= " + gpib_port);
            };
        }


        //----------------------Cau hinh Phat--------------------------------------------//
        public override void config_HT20_TxTest_Transmitter(string frequency) {
            try {
                // Mỗi lần phát tín hiệu cần gửi những lệnh dưới
                mbSession.Write(":SOUR:GPRF:GEN:RFS:FREQ " + frequency + "000000HZ\n");      // Thiết lập tần số phát
                Thread.Sleep(10);
                mbSession.Write(":SOUR:GPRF:GEN:STAT ON\n"); // Phát công suất
                Thread.Sleep(200);
            }
            catch {
            }
        }

        public override string config_HT20_RxTest_Receiver(string frequency) {
            string reusult = "";
            try {

                // mbSession.Write(":CONF:SRW:SEGM:PORT " + receivePort + "\n");           // Lệnh cấu hình PORT cho VSA
                mbSession.Write(":CONF:SRW:FREQ " + frequency + "000000HZ\n");      // Thiết lập tần số thu
                Thread.Sleep(10);
                mbSession.Write(":CONF:SRW:ALEV:TIME 0.005\n");
                Thread.Sleep(10);
                mbSession.Write(":INIT:SRW:ALEV\n"); //Thiết lập Power lever là auto level
                Thread.Sleep(100);
                mbSession.Write(":FETC:SRW:SUMM:CW:POW? 1\n");    // Lệnh đo công suất: -12.25 là công suất trung bình)(1, 0, -12.25, -12.08, 1, 1)
                Thread.Sleep(100);
                reusult = mbSession.ReadString();
                Thread.Sleep(10);
                mbSession.Write(":SOUR:GPRF:GEN:STAT OFF\n");			//Lệnh OFF công suất
                return reusult;
            }
            catch {
                return reusult;
            }
        }

        public override void Dispose() {
            mbSession.Dispose();
        }


    }
}

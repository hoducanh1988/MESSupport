using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using NationalInstruments.VisaNS; //Using .NET VISA DRIVER

namespace MT8870A_VNPT
{


    public class MT8870A_VISA
    {
        private MessageBasedSession mbSession;
        string g_logfilePath = @"WIFI_LOGFILE_MT8870A.LOG";


        public MT8870A_VISA(string MeasureEquip_IP, string power, string receivePort, string transmissionPort)
        {
            try
            {
                // g_logfilePath = @"WIFI_LOGFILE_MT8870.LOG";
                mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(MeasureEquip_IP);
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
                mbSession.Write(":SOUR:GPRF:GEN:RFS:LEV " + power + "\n");           // Thiết lập công suất phát (-10dBm)//+ "DBM
                Thread.Sleep(10);
                mbSession.Write(":ROUT:PORT:CONN:DIR " + receivePort + "," + transmissionPort + "\n"); // Thiết lập cấu hình PORT: PORT1 – VSA, PORT3 - VSG
                Thread.Sleep(10);
                mbSession.Write(":SOUR:GPRF:GEN:BBM CW\n");// Thiết lập VSG ở chế độ phát CW
                Thread.Sleep(10);
                mbSession.Write(":SOUR:GPRF:GEN:ARB:NOIS:STAT OFF\n");
                Thread.Sleep(10);
            }
            catch
            {
                MessageBox.Show("[MT8870A_VISA]Không kết nối được với máy đo IP= " + MeasureEquip_IP);
                saveLogfile(g_logfilePath, "[MT8870A_VISA]Không kết nối được với máy đo IP= " + MeasureEquip_IP + " \n");//TCPIP0::192.168.88.2::inst0::INSTR

            };
        }

        //----------------------Cau hinh Phat--------------------------------------------//
        public void config_HT20_RxTest_Transmitter(string frequency)
        {
            try
            {                
                // Mỗi lần phát tín hiệu cần gửi những lệnh dưới
                mbSession.Write(":SOUR:GPRF:GEN:RFS:FREQ " + frequency + "000000HZ\n");      // Thiết lập tần số phát
                Thread.Sleep(10);
                mbSession.Write(":SOUR:GPRF:GEN:STAT ON\n"); // Phát công suất
                Thread.Sleep(200);
            }
            catch
            {
            }
        }

        public string config_HT20_RxTest_Receiver(string frequency)
        {
            string reusult = "";
            try
            {

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
            catch
            {
                return reusult;
            }
        }


        public string HienThi()
        {
            try
            {
                string result_Value1;
                result_Value1 = mbSession.ReadString();
                Thread.Sleep(100);
                return result_Value1;

            }
            catch
            {
                saveLogfile(g_logfilePath, "[MT8870] FAIL CODE: [WIFI_Processing] \n \n Loi! xay ra trong qua trinh doc ket qua tu MT8870 \n");
                return "";
            }
        }
        private void saveLogfile(string pathfile, string content)
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

    }

}

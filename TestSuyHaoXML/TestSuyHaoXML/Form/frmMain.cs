using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using ToolTestRx;
using E6640A_VNPT;
using MT8870A_VNPT;
using System.Xml;
using System.IO;
using System.Windows.Threading;

namespace TestSuyHaoXML
{
    public partial class frmMain : Form
    {
        frmSetting _frmSetting;
        E6640A_VISA E6640AVISA;
        MT8870A_VISA MT8870AVISA;

        public frmMain()
        {
            InitializeComponent();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            ShowInstrument();
            cbxAttenuatorStype.Items.Add("Suy Hao Trạm Test");
            cbxAttenuatorStype.Items.Add("Suy Hao Master");
            cbxAttenuatorStype.SelectedIndex = 0;
        }
        private void BtnSetting_Click(object sender, EventArgs e)
        {
            _frmSetting = new frmSetting();
            _frmSetting.EventInstrumentChange += frmSetting_EventInstrumentChange;
            _frmSetting.ShowDialog();
        }
        void ShowInstrument()
        {
            GlobalData.confInfor.Get();
            lblInstrument.Text = "Máy đo: " + GlobalData.confInfor.InstrumentStype;
            lblVisaAddress.Text = "Địa Chỉ Visa: " + GlobalData.confInfor.IP;
            lblReceivePort.Text = "Port nhận: " + GlobalData.confInfor.ReceivePort;
            lblTrasmissonPort.Text = "Port phát: " + GlobalData.confInfor.TransmissionPort + "        Power: " + GlobalData.confInfor.TransmissionPower + "(dBm)";
        }
        private void frmSetting_EventInstrumentChange()
        {
            ShowInstrument();
        }
        void EnableControl()
        {
            btnSetting.Enabled = true;
            cbxAttenuatorStype.Enabled = true;
            btnOpenFile.Enabled = true;
            btnOpenMasterFile.Enabled = true;
            btnSave.Enabled = true;
            btnRFAttenuator.Enabled = true;
            cbxWriteSelected.Enabled = true;
            cbxMasterWriteSelected.Enabled = true;
        }
        void DisableControl()
        {
            btnSetting.Enabled = false;
            cbxAttenuatorStype.Enabled = false;
            btnOpenFile.Enabled = false;
            btnOpenMasterFile.Enabled = false;

            btnSave.Enabled = false;
            btnRFAttenuator.Enabled = false;
            cbxWriteSelected.Enabled = false;
            cbxMasterWriteSelected.Enabled = false;

        }
        private void BtnRFAttenuator_Click(object sender, EventArgs e)
        {
            DisableControl();
            if (GlobalData.confInfor.InstrumentStype == "Anritsu_MT8870A")
            {
                CalculateAttenuatorByMT8870A();
            }
            else if (GlobalData.confInfor.InstrumentStype == "Keysight_E6640A")
            {
                CalculateAttenuatorByE6640A();
            }
        }
        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "xml file|*.xml";
            openfile.ShowDialog();
            if (openfile.FileName != "")
            {
                GlobalData.buttonStatus = ButtonStatus.STATIONPRESS;
                txtLink.Text = openfile.FileName;
                ShowFile();
            }
        }
        private void BtnOpenMasterFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "xml file|*.xml";
            openfile.ShowDialog();
            if (openfile.FileName != "")
            {
                GlobalData.buttonStatus = ButtonStatus.MASTERSTAION;
                txtMasterLink.Text = openfile.FileName;
                ShowFile();
            }
        }
        void CalculateAttenuatorByMT8870A()
        {
            ThreadStart t1 = delegate
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                //-------------local variable---------------------------------//
                List<Attenuator> lstAttenuatorTmp = new List<Attenuator>();
                string pathNameTmp = "";
                string resultString = "";
                string attenuatorResult = "";
                double attenuatorResult1 = 0;
                double finalAttenuator = 0;
                double average = 0;

                lblResult.Text = "NONE";
                rtbProcess.Text = "";                

                if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)//2
                {
                    lstAttenuatorTmp = GlobalData.lstAttenuatorFromFile;
                    GlobalData.lstAttenuatorResult = new List<Attenuator>();
                    try
                    {
                        pathNameTmp = cbxWriteSelected.SelectedItem.ToString().Trim();
                    }
                    catch
                    {
                    }

                }
                else//master
                {
                    lstAttenuatorTmp = GlobalData.lstMasterAttenuatorFromFile;
                    GlobalData.lstAttenuatorMasterResult = new List<Attenuator>(); //1
                    try
                    {
                        pathNameTmp = cbxMasterWriteSelected.SelectedItem.ToString().Trim();
                    }
                    catch
                    {
                    }
                }
                //-------------------------------------------------------//          
                if ((GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT) && ((GlobalData.lstAttenuatorFromFile.Count <= 0) || (GlobalData.lstMasterAttenuatorFromFile.Count <= 0)))//tinh suy hao tram
                {
                    lblResult.Text = "FALSE";
                    EnableControl();
                    ShowRTB.SetTextRTB(rtbProcess, "Không Load Được File Suy Hao Trạm Hoặc File Suy Hao Master, Xin Hãy Kiểm Tra Lại Đường Dẫn!\n");
                    MessageBox.Show("Không Load Được File Suy Hao Trạm Hoặc File Suy Hao Master, Xin Hãy Kiểm Tra Lại Đường Dẫn!");
                }
                else if ((GlobalData.attenuatorStyle == AttenuatorStyle.MASTER) && (GlobalData.lstMasterAttenuatorFromFile.Count <= 0))//Tính Suy Hao Master
                {
                    lblResult.Text = "FALSE";
                    EnableControl();
                    ShowRTB.SetTextRTB(rtbProcess, "Không Load Được File Master, Xin Hãy Kiểm Tra Lại Đường Dẫn!\n");
                    MessageBox.Show("Không Load Được File Master, Xin Hãy Kiểm Tra Lại Đường Dẫn!");
                }
                else
                {
                    MT8870AVISA = new MT8870A_VISA(GlobalData.confInfor.IP, GlobalData.confInfor.TransmissionPower, GlobalData.confInfor.ReceivePort, GlobalData.confInfor.TransmissionPort);
                    ShowRTB.SetTextRTB(rtbProcess, "***************** Bắt Đầu *****************" + "\n");
                    foreach (var lst in lstAttenuatorTmp)//6
                    {
                        if (lst.PathName == pathNameTmp)//7
                        {
                            average = 0;
                            for (int i = 0; i < int.Parse(GlobalData.confInfor.AttenuatorTimes); i++)
                            {
                                ShowRTB.SetTextRTB(rtbProcess, "****** Tần Số: " + lst.Frequency + " Lần Thứ: " + (i + 1).ToString() + " ******" + "\n");

                                Attenuator masterAttenuator = new Attenuator();
                                ShowRTB.SetTextRTB(rtbProcess, "************ Phía bên phát ************" + "\n");
                                ShowRTB.SetTextRTB(rtbProcess, "Tần số phát                : " + lst.Frequency + "MHz" + "\n");
                                ShowRTB.SetTextRTB(rtbProcess, "Công suất phát của máy đo  : " + GlobalData.confInfor.TransmissionPower + "dBm" + "\n");
                                if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)// Chế độ tính suy hao trạm Test
                                {
                                    masterAttenuator = FindAttenuatorFromMasster(lst.Frequency);
                                    if (masterAttenuator.Value == null)
                                    {
                                        ShowRTB.SetTextRTB(rtbProcess, "KHÔNG TÌM ĐƯỢC TẦN SỐ " + lst.Frequency + " MHZ TRONG FILE MASTER\n");
                                        lblResult.Text = "FALSE";
                                        if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)//2
                                        {
                                            GlobalData.lstAttenuatorResult = new List<Attenuator>();
                                        }
                                        else
                                        {
                                            GlobalData.lstAttenuatorMasterResult = new List<Attenuator>();
                                        }
                                        goto Final;
                                    }
                                    ShowRTB.SetTextRTB(rtbProcess, "Suy hao dây master      : " + masterAttenuator.Value + "dBm" + "\n");
                                }
                                MT8870AVISA.config_HT20_RxTest_Transmitter(lst.Frequency);
                                ShowRTB.SetTextRTB(rtbProcess, "********** Kết quả bên thu *********** \n");
                                ShowRTB.SetTextRTB(rtbProcess, "Tần số thu              : " + lst.Frequency + "MHz" + "\n");
                                resultString = MT8870AVISA.config_HT20_RxTest_Receiver(lst.Frequency);//1,0,-22.35,-22.18,1,1
                                try
                                {
                                    attenuatorResult = resultString.Split(',')[2].Trim();
                                    attenuatorResult1 = double.Parse(attenuatorResult);
                                }
                                catch
                                {
                                    ShowRTB.SetTextRTB(rtbProcess, "Bị Lỗi Khi Đọc Giá Trị Suy Hao\n");
                                    lblResult.Text = "FALSE";
                                    if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)//2
                                    {
                                        GlobalData.lstAttenuatorResult = new List<Attenuator>();
                                    }
                                    else
                                    {
                                        GlobalData.lstAttenuatorMasterResult = new List<Attenuator>();
                                    }
                                    goto Final;//finalAttenuator
                                }

                                ShowRTB.SetTextRTB(rtbProcess, "Công suất bên thu: " + attenuatorResult + "dBm \n");
                                ShowRTB.SetTextRTB(rtbProcess, "Công suất phát: " + GlobalData.confInfor.TransmissionPower.ToString() + "dBm Tại tần số: " + lst.Frequency + "MHZ Tổng suy hao: " + (double.Parse(GlobalData.confInfor.TransmissionPower) - attenuatorResult1).ToString() + "dBm\n");
                                if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)
                                {
                                    finalAttenuator = double.Parse(GlobalData.confInfor.TransmissionPower) - attenuatorResult1 - double.Parse(masterAttenuator.Value);
                                    finalAttenuator = Math.Round(finalAttenuator, 2);
                                    ShowRTB.SetTextRTB(rtbProcess, "Suy Hao Trạm = Phát - Thu - master = " + finalAttenuator + "dBm \n");
                                }
                                else // master
                                {
                                    finalAttenuator = double.Parse(GlobalData.confInfor.TransmissionPower) - attenuatorResult1 + double.Parse(GlobalData.confInfor.ConnectorAttenuator);
                                    ShowRTB.SetTextRTB(rtbProcess, "Suy Hao Connector= " + GlobalData.confInfor.ConnectorAttenuator + "dBm \n");
                                    ShowRTB.SetTextRTB(rtbProcess, "Suy Hao Master = Phát - Thu + Connector = " + finalAttenuator + "dBm \n");
                                }
                                ShowRTB.SetTextRTB(rtbProcess, "************************************** \n ");
                                average += finalAttenuator;
                            }
                            int tmp = int.Parse(GlobalData.confInfor.AttenuatorTimes);
                            if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)//2
                            {
                                GlobalData.lstAttenuatorResult.Add(new Attenuator() { PathName = lst.PathName, Frequency = lst.Frequency, Value = Math.Round(average / tmp, 2).ToString(), Delta = lst.Delta });
                            }
                            else
                            {
                                GlobalData.lstAttenuatorMasterResult.Add(new Attenuator() { PathName = lst.PathName, Frequency = lst.Frequency, Value = Math.Round(average / tmp, 2).ToString(), Delta = lst.Delta });

                            }
                            ShowRTB.SetTextRTB(rtbProcess, "---Giá trị suy hao trung bình " + tmp.ToString() + " lần đo = " + Math.Round(average / tmp, 2).ToString() + "dBm ---\n\n");
                            Thread.Sleep(100);
                        }
                    }
                    if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)//2
                    {
                        ShowSubFile(GlobalData.lstAttenuatorResult, "Giá Trị Suy Hao Trạm Tính Được", dvwAttenuator, gbxStationAttenuator, cbxWriteSelected);
                    }
                    else
                    {
                        ShowSubFile(GlobalData.lstAttenuatorMasterResult, "Giá Trị Suy Hao Master Tính Được", dvwMasterAttenuator, gbxMasterAttenuator, cbxMasterWriteSelected);
                    }
                    lblResult.Text = "PASS";
                Final:
                    
                    Thread.Sleep(1);
                    EnableControl();
                }
            };
            Thread t2 = new Thread(t1);
            t2.IsBackground = true;
            t2.Start();
        }
        void CalculateAttenuatorByE6640A()
        {
            ThreadStart t1 = delegate
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                //-------------local variable---------------------------------//
                string global, global1, global2, gx, fx;
                double rx = 0, nx = 0, suyhao = 0, suyhao1 = 0, Result = 0;
                int mx;
                string global_temp;
                double average = 0;

                List<Attenuator> lstAttenuatorTmp = new List<Attenuator>();
                string pathNameTmp = "";

                lblResult.Text = "NONE";
                rtbProcess.Text = "";


                if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)//2
                {

                    lstAttenuatorTmp = GlobalData.lstAttenuatorFromFile;
                    GlobalData.lstAttenuatorResult = new List<Attenuator>();

                    try
                    {
                        pathNameTmp = cbxWriteSelected.SelectedItem.ToString().Trim();
                    }
                    catch
                    {

                    }
                }
                else//master
                {
                    lstAttenuatorTmp = GlobalData.lstMasterAttenuatorFromFile;
                    GlobalData.lstAttenuatorMasterResult = new List<Attenuator>(); //1

                    try
                    {
                        pathNameTmp = cbxMasterWriteSelected.SelectedItem.ToString().Trim();
                    }
                    catch
                    {

                    }

                }
                //-------------------------------------------------------//              

                if ((GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT) && ((GlobalData.lstAttenuatorFromFile.Count <= 0) || (GlobalData.lstMasterAttenuatorFromFile.Count <= 0)))//tinh suy hao tram
                {
                    lblResult.Text = "FALSE";
                    EnableControl();
                    ShowRTB.SetTextRTB(rtbProcess, "Không Load Được File Suy Hao Trạm Hoặc File Suy Hao Master, Xin Hãy Kiểm Tra Lại Đường Dẫn!\n");
                    MessageBox.Show("Không Load Được File Suy Hao Trạm Hoặc File Suy Hao Master, Xin Hãy Kiểm Tra Lại Đường Dẫn!");
                }
                else if ((GlobalData.attenuatorStyle == AttenuatorStyle.MASTER) && (GlobalData.lstMasterAttenuatorFromFile.Count <= 0))//Tính Suy Hao Master
                {
                    lblResult.Text = "FALSE";
                    EnableControl();
                    ShowRTB.SetTextRTB(rtbProcess, "Không Load Được File Master, Xin Hãy Kiểm Tra Lại Đường Dẫn!\n");
                    MessageBox.Show("Không Load Được File Master, Xin Hãy Kiểm Tra Lại Đường Dẫn!");
                }
                else
                {
                    E6640AVISA = new E6640A_VISA(GlobalData.confInfor.IP);
                    ShowRTB.SetTextRTB(rtbProcess, "***************** Bắt Đầu *****************" + "\n");
                    foreach (var lst in lstAttenuatorTmp)
                    {
                        if (lst.PathName == pathNameTmp)//7
                        {
                            average = 0;
                            for (int i = 0; i < int.Parse(GlobalData.confInfor.AttenuatorTimes); i++)
                            {
                                ShowRTB.SetTextRTB(rtbProcess, "****** Tần Số: " + lst.Frequency + " Lần Thứ: " + (i + 1).ToString() + " ******" + "\n");
                                Attenuator masterAttenuator = new Attenuator();
                                ShowRTB.SetTextRTB(rtbProcess, "************ Phía bên phát ************" + "\n");
                                ShowRTB.SetTextRTB(rtbProcess, "Tần số phát                : " + lst.Frequency + "MHz" + "\n");
                                ShowRTB.SetTextRTB(rtbProcess, "Công suất phát của máy đo  : " + GlobalData.confInfor.TransmissionPower + "dBm" + "\n");
                                if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)
                                {
                                    masterAttenuator = FindAttenuatorFromMasster(lst.Frequency);
                                    if (masterAttenuator.Value == null)
                                    {
                                        ShowRTB.SetTextRTB(rtbProcess, "KHÔNG TÌM ĐƯỢC TẦN SỐ " + lst.Frequency + "MHZ TRONG FILE MASTER\n");
                                        lblResult.Text = "FALSE";
                                        if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)//2
                                        {
                                            GlobalData.lstAttenuatorResult = new List<Attenuator>();
                                        }
                                        else
                                        {
                                            GlobalData.lstAttenuatorMasterResult = new List<Attenuator>();
                                        }
                                        goto Final;
                                    }
                                    ShowRTB.SetTextRTB(rtbProcess, "Suy hao dây master      : " + masterAttenuator.Value + "dBm" + "\n");
                                }
                                
                                E6640AVISA.config_HT20_RxTest_Transmitter(lst.Frequency, GlobalData.confInfor.TransmissionPower.ToString(), GlobalData.confInfor.TransmissionPort);
                                Thread.Sleep(200);
                                ShowRTB.SetTextRTB(rtbProcess, "********** Kết quả bên thu *********** \n");
                                ShowRTB.SetTextRTB(rtbProcess, "Tần số thu              : " + lst.Frequency + "MHz" + "\n");
                                E6640AVISA.config_HT20_RxTest_Receiver(lst.Frequency, GlobalData.confInfor.ReceivePort);
                                global = E6640AVISA.HienThi();// 3.557310104E-01,3.557278514E-01\n
                                global_temp = global.Split(',')[0];//3.557310104E-01
                                global1 = global_temp.Split('E')[0];//3.557310104
                                global2 = global_temp.Split('E')[1];//-01
                                gx = global2.Substring(0, 1);
                                try
                                {
                                    fx = global2.Split('+')[1];
                                }
                                catch
                                {
                                    //fx = global2.Split('-')[1].Substring(0,2);
                                    ShowRTB.SetTextRTB(rtbProcess, "[E6640A_VISA]Không kết nối được với máy đo IP= " + GlobalData.confInfor.IP + " \n");
                                    lblResult.Text = "FALSE";
                                    if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)//2
                                    {
                                        GlobalData.lstAttenuatorResult = new List<Attenuator>();
                                    }
                                    else
                                    {
                                        GlobalData.lstAttenuatorMasterResult = new List<Attenuator>();
                                    }
                                    goto Final;
                                }
                                rx = float.Parse(global1);
                                mx = int.Parse(fx);
                                if (gx == "+")
                                {
                                    nx = rx * HamPow(10, mx);
                                    ShowRTB.SetTextRTB(rtbProcess, "Công suất bên thu: " + nx.ToString() + "dBm \n");
                                    suyhao = float.Parse(GlobalData.confInfor.TransmissionPower.ToString()) - nx;
                                    suyhao1 = Math.Round(suyhao, 2);
                                    ShowRTB.SetTextRTB(rtbProcess, "Công suất phát: " + GlobalData.confInfor.TransmissionPower.ToString() + "dBm Tại tần số: " + lst.Frequency + "MHZ Tổng suy hao: " + suyhao1 + " dBm\n");
                                }
                                else if (gx == "-")
                                {
                                    nx = rx / HamPow(10, mx);
                                    ShowRTB.SetTextRTB(rtbProcess, nx.ToString() + "dBm \n");
                                    suyhao = float.Parse(GlobalData.confInfor.TransmissionPower.ToString()) - nx;
                                    suyhao1 = Math.Round(suyhao, 2);
                                    ShowRTB.SetTextRTB(rtbProcess, "Công suất phát: " + GlobalData.confInfor.TransmissionPower.ToString() + "dBm Tại tần số: " + lst.Frequency + "MHZ Tổng suy hao: " + suyhao1 + " dBm\n");
                                }
                                if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)
                                {
                                    Result = suyhao - double.Parse(masterAttenuator.Value);
                                    Result = Math.Round(Result, 2);
                                    ShowRTB.SetTextRTB(rtbProcess, "Suy Hao Trạm = Phát - Thu - master = " + Result + "dBm\n");
                                }
                                else // master
                                {
                                    Result = suyhao1 + double.Parse(GlobalData.confInfor.ConnectorAttenuator);
                                    ShowRTB.SetTextRTB(rtbProcess, "Suy Hao Connector= " + GlobalData.confInfor.ConnectorAttenuator + "dBm \n");
                                    ShowRTB.SetTextRTB(rtbProcess, "Suy Hao Master(dBm) = Phát - Thu + Connector= " + Result + "dBm\n");
                                }
                                ShowRTB.SetTextRTB(rtbProcess, "************************************** \n \n ");
                                average += Result;
                            }
                            int tmp = int.Parse(GlobalData.confInfor.AttenuatorTimes);
                            if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)//2
                            {
                                GlobalData.lstAttenuatorResult.Add(new Attenuator() { PathName = lst.PathName, Frequency = lst.Frequency, Value = Math.Round(average / tmp, 2).ToString(), Delta = lst.Delta });
                            }
                            else
                            {
                                GlobalData.lstAttenuatorMasterResult.Add(new Attenuator() { PathName = lst.PathName, Frequency = lst.Frequency, Value = Math.Round(average / tmp, 2).ToString(), Delta = lst.Delta });

                            }
                            ShowRTB.SetTextRTB(rtbProcess, "---Giá trị suy hao trung bình " + tmp.ToString() + " lần đo = " + Math.Round(average / tmp, 2).ToString() + "dBm ---\n\n");
                            Thread.Sleep(100);
                        }
                    }
                    if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)//2
                    {
                        ShowSubFile(GlobalData.lstAttenuatorResult, "Giá Trị Suy Hao Trạm Tính Được", dvwAttenuator, gbxStationAttenuator, cbxWriteSelected);
                    }
                    else
                    {
                        ShowSubFile(GlobalData.lstAttenuatorMasterResult, "Giá Trị Suy Hao Master Tính Được", dvwMasterAttenuator, gbxMasterAttenuator, cbxMasterWriteSelected);
                    }
                    lblResult.Text = "PASS";
                Final:

                    Thread.Sleep(1);
                    EnableControl();
                }
            };
            Thread t2 = new Thread(t1);
            t2.IsBackground = true;
            t2.Start();
        }
        Attenuator FindAttenuatorFromMasster(string Frequency)
        {
            Attenuator attenuator = new Attenuator();
            foreach (var lst in GlobalData.lstMasterAttenuatorFromFile)
            {
                if (Frequency == lst.Frequency)
                {
                    attenuator = lst;
                    break;
                }
            }
            return attenuator;
        }

        void ShowFile()//khi tải lên
        {
            lblResult.Text = "NONE";
            string fileName = "";
            List<string> lstPathName = new List<string>();

            if (GlobalData.buttonStatus == ButtonStatus.STATIONPRESS)
            {
                fileName = txtLink.Text;
                GlobalData.lstAttenuatorResult = new List<Attenuator>();
                GlobalData.lstAttenuatorFromFile = new List<Attenuator>();
                GlobalData.lstAttenuatorFromFile = XMLData.LoadFile(fileName, out lstPathName);
                cbxWriteSelected.DataSource = lstPathName;
                ShowSubFile(GlobalData.lstAttenuatorFromFile, "Giá Trị Suy Hao Trạm Được Mở Từ File", dvwAttenuator, gbxStationAttenuator, cbxWriteSelected);
            }
            else//master
            {
                fileName = txtMasterLink.Text;
                GlobalData.lstAttenuatorMasterResult = new List<Attenuator>();
                GlobalData.lstMasterAttenuatorFromFile = new List<Attenuator>();
                GlobalData.lstMasterAttenuatorFromFile = XMLData.LoadFile(fileName, out lstPathName);
                cbxMasterWriteSelected.DataSource = lstPathName;
                ShowSubFile(GlobalData.lstMasterAttenuatorFromFile, "Giá Trị Suy Hao Master Được Mở Từ File", dvwMasterAttenuator, gbxMasterAttenuator, cbxMasterWriteSelected);
            }
        }
        void ShowSubFile(List<Attenuator> lstAntenuator, string textGbxAttenuator, DataGridView gvw, GroupBox gbx, ComboBox cbx)// Show trong datagrid view
        {
            gbx.Text = textGbxAttenuator;
            //-----------------------------Show file in DatagridView ----------------------------//
            gvw.Invoke(new MethodInvoker(delegate ()
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PathName");
                dt.Columns.Add("Frequency");
                dt.Columns.Add("Value");
                dt.Columns.Add("Delta");
                foreach (var lst in lstAntenuator)
                {
                    if (lst.PathName == cbx.SelectedItem.ToString().Trim())
                    {
                        dt.Rows.Add(lst.PathName, lst.Frequency, lst.Value, lst.Delta);
                    }
                }
                Thread.Sleep(100);
                gvw.DataSource = dt;
            }));

        }
        private void CbxWriteSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalData.lstAttenuatorResult = new List<Attenuator>();
            ShowSubFile(GlobalData.lstAttenuatorFromFile, "Giá Trị Suy Hao Trạm Được Mở Từ File", dvwAttenuator, gbxStationAttenuator, cbxWriteSelected);
        }
        private void CbxMasterWriteSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalData.lstAttenuatorMasterResult = new List<Attenuator>();
            ShowSubFile(GlobalData.lstMasterAttenuatorFromFile, "Giá Trị Suy Hao Master Được Mở Từ File", dvwMasterAttenuator, gbxMasterAttenuator, cbxMasterWriteSelected);
        }
        public int HamPow(int num, int exp)
        {
            int ket_qua = 1;
            int i;
            for (i = 1; i <= exp; i++)
                ket_qua = ket_qua * num;
            return ket_qua;
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (GlobalData.attenuatorStyle == AttenuatorStyle.DEFAULT)
            {
                if (GlobalData.lstAttenuatorResult.Count <= 0)
                {
                    MessageBox.Show("Bạn Chưa Thực Hiện Tính Suy Hao!");
                }
                else if (XMLData.SaveFile(GlobalData.lstAttenuatorResult, txtLink.Text.Trim()))
                {
                    List<string> lst;
                    GlobalData.lstAttenuatorFromFile = XMLData.LoadFile(txtLink.Text.Trim(), out lst);//

                    MessageBox.Show("Lưu File Thàng Công!");
                }
                else
                {
                    MessageBox.Show("Lưu File Không Thàng Công!");
                }
            }
            else
            {
                if (GlobalData.lstAttenuatorMasterResult.Count <= 0)
                {
                    MessageBox.Show("Bạn Chưa Thực Hiện Tính Suy Hao Master!");
                }
                else if (XMLData.SaveFile(GlobalData.lstAttenuatorMasterResult, txtMasterLink.Text.Trim()))
                {
                    List<string> lst;
                    GlobalData.lstMasterAttenuatorFromFile = XMLData.LoadFile(txtMasterLink.Text.Trim(), out lst);
                    MessageBox.Show("Lưu File Thàng Công!");
                }
                else
                {
                    MessageBox.Show("Lưu File Không Thàng Công!");
                }
            }
        }
        private void BtnAbout_Click(object sender, EventArgs e)
        {
            var frm = new frmAbout();
            frm.ShowDialog();
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void CbxAttenuatorStype_SelectedIndexChanged(object sender, EventArgs e)
        {

            GlobalData.lstAttenuatorResult = new List<Attenuator>();
            GlobalData.lstAttenuatorMasterResult = new List<Attenuator>();


            if (cbxAttenuatorStype.SelectedItem.ToString().Trim() == "Suy Hao Trạm Test")
            {
                btnRFAttenuator.Text = "Tính Suy Hao Trạm Test";
                GlobalData.attenuatorStyle = AttenuatorStyle.DEFAULT;
                txtLink.Visible = true;
                btnOpenFile.Visible = true;
                GlobalData.buttonStatus = ButtonStatus.MASTERSTAION;
                ShowFile();
                GlobalData.buttonStatus = ButtonStatus.STATIONPRESS;
                ShowFile();
            }
            else
            {
                GlobalData.attenuatorStyle = AttenuatorStyle.MASTER;//
                txtLink.Visible = false;
                btnOpenFile.Visible = false;
                btnRFAttenuator.Text = "Tính Suy Hao Master";
                cbxWriteSelected.DataSource = new List<string>();
                dvwAttenuator.DataSource = new DataTable();
                GlobalData.lstAttenuatorFromFile = new List<Attenuator>();
                GlobalData.buttonStatus = ButtonStatus.MASTERSTAION;
                ShowFile();

            }
        }

        private void RtbProcess_TextChanged(object sender, EventArgs e)
        {
            rtbProcess.SelectionStart = rtbProcess.Text.Length;
            rtbProcess.ScrollToCaret();
        }

    }
}

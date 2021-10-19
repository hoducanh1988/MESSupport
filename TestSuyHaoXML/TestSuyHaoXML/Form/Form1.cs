using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ToolTestRx;
using E6640A_VNPT;
using System.IO;
using System.Threading;


namespace TestSuyHaoXML
{
    public partial class Form1 : Form
    {
        E6640A_VISA E6640AVISA;
        
        public Form1()
        {
            InitializeComponent();
            
            this.cbDay.Items.Add("BH0_LP");
            this.cbDay.Items.Add("BH1_LP");
            this.cbDay.Items.Add("BH2_LP");
            this.cbDay.Items.Add("BH3_LP");
            this.cbDay.SelectedIndex = 0;
            this.cbIP.Items.Add("TCPIP0::192.168.1.2::inst0::INSTR");
            this.cbIP.Items.Add("TCPIP0::192.168.1.2::inst1::INSTR");
            this.cbIP.SelectedIndex = 0;

            txtPow.Text = "-20";
        }
        public static int HamPow(int num, int exp)
        {
            int ket_qua = 1;
            int i;
            for (i = 1; i <= exp; i++)
                ket_qua = ket_qua * num;
            return ket_qua;
        }
        private void btnHienthi_Click(object sender, EventArgs e)
        {
            ThreadStart t1 = delegate
            {
                E6640AVISA = new E6640A_VISA(cbIP.SelectedItem.ToString());
                Control.CheckForIllegalCrossThreadCalls = false;
                dgv.Rows.Clear();
                dgv.Refresh();
                //label4.Text = null;
                label4.Text = "NONE";
                XmlDocument doc = new XmlDocument();
                XmlElement root;
                //string fileName = @"C:\Users\LEHAIYEN\Desktop\Suyhao_A.Doan\TestSuyHaoXML\TestSuyHaoXML\pathloss_vnpt.xml";
                string fileName = txtLink.Text;
                doc.Load(fileName);
                root = doc.DocumentElement;
                rtbOutput.Text = null;

                string global, global1, global2, gx, fx;
                double rx, nx, suyhao, suyhao1;
                int mx;
                string global_temp;
                //CODE DUNG HIEN THI NGON
                //dgv.Rows.Clear();
                //dgv.ColumnCount = 3;
                //XmlNodeList ds = root.SelectNodes("Path");
                //foreach (XmlNode item in ds)
                //{
                //    string Name = item.SelectSingleNode("PathName").InnerText;
                //    if (Name == cbDay.SelectedItem.ToString())
                //    {
                //        XmlNodeList ds1 = item.SelectNodes("DataList/Data");
                //        int i = 0;
                //        foreach (XmlNode item1 in ds1)
                //        {
                //            dgv.Rows.Add();
                //            dgv.Rows[i].Cells[0].Value = item1.SelectSingleNode("Frequency").InnerText;
                //            dgv.Rows[i].Cells[1].Value = item1.SelectSingleNode("Value").InnerText;
                //            dgv.Rows[i].Cells[2].Value = item1.SelectSingleNode("Delta").InnerText;
                //            i++;
                //        }
                //    }
                //}
                /////////////////////////////////////////
                //dgv.Rows.Clear();
                //dgv.ColumnCount = 3;
                XmlNodeList ds = root.SelectNodes("Path");
                foreach (XmlNode item in ds)
                {

                    string Name = item.SelectSingleNode("PathName").InnerText;
                    if (Name == cbDay.SelectedItem.ToString())
                    {
                        XmlNodeList ds1 = item.SelectNodes("DataList/Data");
                        //int i = 0;
                        foreach (XmlNode item1 in ds1)
                        {
                            string FRE;
                            FRE = item1.SelectSingleNode("Frequency").InnerText;
                            XmlNodeList ds2 = item.SelectNodes("DataList");
                            foreach (XmlNode item2 in ds2)
                            {
                                XmlNode datalist = item2.SelectSingleNode("Data[Frequency='" + FRE + "']");
                                if (datalist != null)
                                {
                                    XmlNode maDataNew = doc.CreateElement("Data");
                                    XmlElement frequency = doc.CreateElement("Frequency");
                                    frequency.InnerText = FRE;//gán giá trị cho mã sách
                                    maDataNew.AppendChild(frequency);//thêm masach vào trong sách(sach nhận masach là con)
                                    XmlElement pow = doc.CreateElement("Value");
                                    ShowRTB.SetTextRTB(rtbOutput, "********** Phía bên phát **********" + "\n");
                                    ShowRTB.SetTextRTB(rtbOutput, "Bắt đầu Test \n");
                                    ShowRTB.SetTextRTB(rtbOutput, "Tần số phát                : " + FRE + "MHz" + "\n");
                                    ShowRTB.SetTextRTB(rtbOutput, "Công suất phát của máy đo  : " + txtPow.Text + "dBm" + "\n");
                                    //E6640AVISA = new E6640A_VISA(cbIP.SelectedItem.ToString());
                                    E6640AVISA.config_HT20_RxTest_Transmitter(FRE, txtPow.Text,GlobalData.confInfor.TransmissionPort);//---------------tienpv
                                    Thread.Sleep(100);
                                    ShowRTB.SetTextRTB(rtbOutput, "Bắt đầu Test \n");
                                    ShowRTB.SetTextRTB(rtbOutput, "Tần số thu                : " + FRE + "MHz" + "\n");
                                    E6640AVISA = new E6640A_VISA(cbIP.SelectedItem.ToString());
                                    E6640AVISA.config_HT20_RxTest_Receiver(FRE,GlobalData.confInfor.ReceivePort);
                                    ShowRTB.SetTextRTB(rtbOutput, "********** Kết quả bên thu *********** \n");
                                    global = E6640AVISA.HienThi();
                                    global_temp = global.Split(',')[0];
                                    global1 = global_temp.Split('E')[0];
                                    global2 = global_temp.Split('E')[1];
                                    gx = global2.Substring(0, 1);
                                    fx = global2.Split('+')[1];
                                    rx = float.Parse(global1);
                                    mx = int.Parse(fx);
                                    if (gx == "+")
                                    {
                                        nx = rx * HamPow(10, mx);
                                        ShowRTB.SetTextRTB(rtbOutput, "Công suất bên thu: " + nx.ToString() + "dBm \n");
                                        suyhao = float.Parse(txtPow.Text) - nx;
                                        suyhao1 = Math.Round(suyhao, 2);
                                        ShowRTB.SetTextRTB(rtbOutput, "Công suất phát: " + txtPow.Text + " Tại tần số: " + FRE + " Suy hao: " + suyhao1 + "\n");
                                        pow.InnerText = suyhao1.ToString();//gán giá trị cho mã sách
                                        maDataNew.AppendChild(pow);//thêm masach vào trong sách(sach nhận masach là con)
                                    }
                                    else if (gx == "-")
                                    {
                                        nx = rx / HamPow(10, mx);
                                        ShowRTB.SetTextRTB(rtbOutput, nx.ToString() + "dBm \n");
                                        suyhao = float.Parse(txtPow.Text) - nx;
                                        suyhao1 = Math.Round(suyhao, 2);
                                        ShowRTB.SetTextRTB(rtbOutput, "Công suất phát: " + txtPow.Text + " Tại tần số: " + FRE + " Suy hao: " + suyhao1 + "\n");
                                        pow.InnerText = suyhao1.ToString();//gán giá trị cho mã sách
                                        maDataNew.AppendChild(pow);//thêm masach vào trong sách(sach nhận masach là con)                                 
                                    }
                                    ShowRTB.SetTextRTB(rtbOutput, "Chuyen sang kenh tiếp theo *********** \n \n ");
                                    XmlElement delta = doc.CreateElement("Delta");
                                    delta.InnerText = "0";//gán giá trị cho mã sách
                                    maDataNew.AppendChild(delta);//thêm masach vào trong sách(sach nhận masach là con)
                                    item2.ReplaceChild(maDataNew, datalist);
                                    doc.Save(fileName);
                                    Thread.Sleep(2000);
                                }
                            }
                        }
                    }

                }


                dgv.ColumnCount = 3;
                XmlNodeList ds3 = root.SelectNodes("Path");
                foreach (XmlNode item in ds3)
                {
                    string Name = item.SelectSingleNode("PathName").InnerText;
                    if (Name == cbDay.SelectedItem.ToString())
                    {
                        XmlNodeList ds4 = item.SelectNodes("DataList/Data");
                        int i = 0;
                        foreach (XmlNode item1 in ds4)
                        {
                            dgv.Rows.Add();
                            dgv.Rows[i].Cells[0].Value = item1.SelectSingleNode("Frequency").InnerText;
                            dgv.Rows[i].Cells[1].Value = item1.SelectSingleNode("Value").InnerText;
                            dgv.Rows[i].Cells[2].Value = item1.SelectSingleNode("Delta").InnerText;
                            i++;
                        }
                    }
                }
                label4.Text = "PASS";
            };
            Thread t2 = new Thread(t1);
            t2.IsBackground = true;
            t2.Start();
            ////
            //**********code dung chua chinh sửa hiển thị NONE***********//
            //////////////////////////////////////////////////////////////
            //dgv.Rows.Clear();
            //dgv.Refresh();
            ////label4.Text = null;
            //label4.Text = "NONE";
            //XmlDocument doc = new XmlDocument();
            //XmlElement root;
            ////string fileName = @"C:\Users\LEHAIYEN\Desktop\Suyhao_A.Doan\TestSuyHaoXML\TestSuyHaoXML\pathloss_vnpt.xml";
            //string fileName = txtLink.Text;
            //doc.Load(fileName);
            //root = doc.DocumentElement;
            //rtbOutput.Text = null;

            //string global, global1, global2, gx, fx;
            //double rx, nx, suyhao, suyhao1;
            //int mx;
            //string global_temp;
            ////CODE DUNG HIEN THI NGON
            ////dgv.Rows.Clear();
            ////dgv.ColumnCount = 3;
            ////XmlNodeList ds = root.SelectNodes("Path");
            ////foreach (XmlNode item in ds)
            ////{
            ////    string Name = item.SelectSingleNode("PathName").InnerText;
            ////    if (Name == cbDay.SelectedItem.ToString())
            ////    {
            ////        XmlNodeList ds1 = item.SelectNodes("DataList/Data");
            ////        int i = 0;
            ////        foreach (XmlNode item1 in ds1)
            ////        {
            ////            dgv.Rows.Add();
            ////            dgv.Rows[i].Cells[0].Value = item1.SelectSingleNode("Frequency").InnerText;
            ////            dgv.Rows[i].Cells[1].Value = item1.SelectSingleNode("Value").InnerText;
            ////            dgv.Rows[i].Cells[2].Value = item1.SelectSingleNode("Delta").InnerText;
            ////            i++;
            ////        }
            ////    }
            ////}
            ///////////////////////////////////////////
            ////dgv.Rows.Clear();
            ////dgv.ColumnCount = 3;
            //XmlNodeList ds = root.SelectNodes("Path");
            //foreach (XmlNode item in ds)
            //{

            //    string Name = item.SelectSingleNode("PathName").InnerText;
            //    if (Name == cbDay.SelectedItem.ToString())
            //    {
            //        XmlNodeList ds1 = item.SelectNodes("DataList/Data");
            //        //int i = 0;
            //        foreach (XmlNode item1 in ds1)
            //        {
            //            string FRE;
            //            FRE = item1.SelectSingleNode("Frequency").InnerText;
            //            XmlNodeList ds2 = item.SelectNodes("DataList");
            //            foreach (XmlNode item2 in ds2)
            //            {
            //                XmlNode datalist = item2.SelectSingleNode("Data[Frequency='" + FRE + "']");
            //                if (datalist != null)
            //                {
            //                    XmlNode maDataNew = doc.CreateElement("Data");
            //                    XmlElement frequency = doc.CreateElement("Frequency");
            //                    frequency.InnerText = FRE;//gán giá trị cho mã sách
            //                    maDataNew.AppendChild(frequency);//thêm masach vào trong sách(sach nhận masach là con)
            //                    XmlElement pow = doc.CreateElement("Value");
            //                    ShowRTB.SetTextRTB(rtbOutput, "********** Phía bên phát **********" + "\n");
            //                    ShowRTB.SetTextRTB(rtbOutput, "Bắt đầu Test \n");
            //                    ShowRTB.SetTextRTB(rtbOutput, "Tần số phát                : " + FRE + "MHz" + "\n");
            //                    ShowRTB.SetTextRTB(rtbOutput, "Công suất phát của máy đo  : " + txtPow.Text + "dBm" + "\n");
            //                    E6640AVISA = new E6640A_VISA(cbIP.SelectedItem.ToString());
            //                    E6640AVISA.config_HT20_RxTest_Transmitter(FRE, txtPow.Text);
            //                    Thread.Sleep(100);
            //                    ShowRTB.SetTextRTB(rtbOutput, "Bắt đầu Test \n");
            //                    ShowRTB.SetTextRTB(rtbOutput, "Tần số thu                : " + FRE + "MHz" + "\n");
            //                    E6640AVISA = new E6640A_VISA(cbIP.SelectedItem.ToString());
            //                    E6640AVISA.config_HT20_RxTest_Receiver(FRE);
            //                    ShowRTB.SetTextRTB(rtbOutput, "********** Kết quả bên thu *********** \n");
            //                    global = E6640AVISA.HienThi();
            //                    global_temp = global.Split(',')[0];
            //                    global1 = global_temp.Split('E')[0];
            //                    global2 = global_temp.Split('E')[1];
            //                    gx = global2.Substring(0, 1);
            //                    fx = global2.Split('+')[1];
            //                    rx = float.Parse(global1);
            //                    mx = int.Parse(fx);
            //                    if (gx == "+")
            //                    {
            //                        nx = rx * HamPow(10, mx);
            //                        ShowRTB.SetTextRTB(rtbOutput, "Công suất bên thu: " + nx.ToString() + "dBm \n");
            //                        suyhao = float.Parse(txtPow.Text) - nx;
            //                        suyhao1 = Math.Round(suyhao, 2);
            //                        ShowRTB.SetTextRTB(rtbOutput, "Công suất phát: " + txtPow.Text + " Tại tần số: " + FRE + " Suy hao: " + suyhao1 + "\n");
            //                        pow.InnerText = suyhao1.ToString();//gán giá trị cho mã sách
            //                        maDataNew.AppendChild(pow);//thêm masach vào trong sách(sach nhận masach là con)
            //                    }
            //                    else if (gx == "-")
            //                    {
            //                        nx = rx / HamPow(10, mx);
            //                        ShowRTB.SetTextRTB(rtbOutput, nx.ToString() + "dBm \n");
            //                        suyhao = float.Parse(txtPow.Text) - nx;
            //                        suyhao1 = Math.Round(suyhao, 2);
            //                        ShowRTB.SetTextRTB(rtbOutput, "Công suất phát: " + txtPow.Text + " Tại tần số: " + FRE + " Suy hao: " + suyhao1 + "\n");
            //                        pow.InnerText = suyhao1.ToString();//gán giá trị cho mã sách
            //                        maDataNew.AppendChild(pow);//thêm masach vào trong sách(sach nhận masach là con)                                 
            //                    }
            //                    ShowRTB.SetTextRTB(rtbOutput, "Chuyen sang kenh tiếp theo *********** \n \n ");
            //                    XmlElement delta = doc.CreateElement("Delta");
            //                    delta.InnerText = "0";//gán giá trị cho mã sách
            //                    maDataNew.AppendChild(delta);//thêm masach vào trong sách(sach nhận masach là con)
            //                    item2.ReplaceChild(maDataNew, datalist);
            //                    doc.Save(fileName);
            //                    Thread.Sleep(2000);

            //                }
            //            }
            //        }
            //    }

            //}


            //dgv.ColumnCount = 3;
            //XmlNodeList ds3 = root.SelectNodes("Path");
            //foreach (XmlNode item in ds3)
            //{
            //    string Name = item.SelectSingleNode("PathName").InnerText;
            //    if (Name == cbDay.SelectedItem.ToString())
            //    {
            //        XmlNodeList ds4 = item.SelectNodes("DataList/Data");
            //        int i = 0;
            //        foreach (XmlNode item1 in ds4)
            //        {
            //            dgv.Rows.Add();
            //            dgv.Rows[i].Cells[0].Value = item1.SelectSingleNode("Frequency").InnerText;
            //            dgv.Rows[i].Cells[1].Value = item1.SelectSingleNode("Value").InnerText;
            //            dgv.Rows[i].Cells[2].Value = item1.SelectSingleNode("Delta").InnerText;
            //            i++;
            //        }
            //    }
            //}
            //label4.Text = "PASS";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CODE DUNG HIEN THI NGON
            
            XmlDocument doc = new XmlDocument();
            XmlElement root;
            string fileName = txtLink.Text;
            doc.Load(fileName);
            root = doc.DocumentElement;
            dgv.Rows.Clear();
            dgv.ColumnCount = 3;
            XmlNodeList ds = root.SelectNodes("Path");
            foreach (XmlNode item in ds)
            {
                string Name = item.SelectSingleNode("PathName").InnerText;
                if (Name == cbDay.SelectedItem.ToString())
                {
                    XmlNodeList ds1 = item.SelectNodes("DataList/Data");
                    int i = 0;
                    foreach (XmlNode item1 in ds1)
                    {
                        dgv.Rows.Add();
                        dgv.Rows[i].Cells[0].Value = item1.SelectSingleNode("Frequency").InnerText;
                        dgv.Rows[i].Cells[1].Value = item1.SelectSingleNode("Value").InnerText;
                        dgv.Rows[i].Cells[2].Value = item1.SelectSingleNode("Delta").InnerText;
                        i++;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string link;
            OpenFileDialog openfile = new OpenFileDialog();
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "xml file|*.xml";
            op.ShowDialog();
            txtLink.Text = op.FileName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

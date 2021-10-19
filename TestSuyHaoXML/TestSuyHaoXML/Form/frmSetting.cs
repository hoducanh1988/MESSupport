using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSuyHaoXML
{
    public partial class frmSetting : Form
    {
        public delegate void InstrumentChange();
        public event InstrumentChange EventInstrumentChange;
        public frmSetting()
        {
            InitializeComponent();
            //-------------------------Init combobox----------------------------------------------//
            cbxInstrumentStyle.Items.Add("Anritsu_MT8870A");
            cbxInstrumentStyle.Items.Add("Keysight_E6640A");
            cbxInstrumentStyle.SelectedIndex = 0;
            cbxAttenuatorTimes.DataSource = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9","10"};
        }
        private void FrmSetting_Load(object sender, EventArgs e)
        {
            GlobalData.confInfor.Get();//Lấy Thông tin cấu hình
            cbxInstrumentStyle.SelectedItem = GlobalData.confInfor.InstrumentStype;
            txtVisaAddress.Text = GlobalData.confInfor.IP.Trim();
            cbxReceivePort.SelectedItem = GlobalData.confInfor.ReceivePort;
            cbxTransmissionPort.SelectedItem = GlobalData.confInfor.TransmissionPort;
            txtTransmissionPower.Text = GlobalData.confInfor.TransmissionPower.ToString().Trim();
            //txtLink.Text = GlobalData.confInfor.MasterFileLink;
            txtConnectorAttenuator.Text = GlobalData.confInfor.ConnectorAttenuator;
            cbxAttenuatorTimes.SelectedItem = GlobalData.confInfor.AttenuatorTimes;
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            double a = 0;
            if (txtTransmissionPower.Text == "" || (double.TryParse(txtTransmissionPower.Text, out a) == false))
            {
                lblSaveStatus.Text = "Kiểm Tra Lại Thông Tin: Công Suất Phát!";
            }
            else if (txtConnectorAttenuator.Text == "" || (double.TryParse(txtConnectorAttenuator.Text, out a) == false))
            {
                lblSaveStatus.Text = "Kiểm Tra Lại Thông Tin: Suy Hao Connector!";
            }
            else
            {
                GlobalData.confInfor = new ConfInfor(
                                cbxInstrumentStyle.SelectedItem.ToString().Trim(),
                                txtVisaAddress.Text.Trim(),
                                cbxReceivePort.SelectedItem.ToString().Trim(),
                                cbxTransmissionPort.SelectedItem.ToString().Trim(),
                                txtTransmissionPower.Text.Trim(),
                                "", cbxAttenuatorTimes.SelectedItem.ToString().Trim(),
                                txtConnectorAttenuator.Text.Trim());
                if (GlobalData.confInfor.SaveConfig())
                {
                    lblSaveStatus.Text = "Lưu thông số của máy đo thành công!";
                    EventInstrumentChange();
                }
                else
                {
                    lblSaveStatus.Text = "Lưu thông số của máy đo thất bại!";
                    EventInstrumentChange();
                }
            }
        }
        private void CbxInstrumentStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxInstrumentStyle.SelectedItem.ToString().Trim() == "Anritsu_MT8870A")
            {
                cbxReceivePort.DataSource = new List<string>() { "PORT1", "PORT2", "PORT3", "PORT4" };
                cbxTransmissionPort.DataSource = new List<string>() { "PORT1", "PORT2", "PORT3", "PORT4" };
            }
            else
            {
                cbxReceivePort.DataSource = new List<string>() { "RFIO1", "RFIO2" };//, "RFIO3", "RFIO4"
                cbxTransmissionPort.DataSource = new List<string>() { "RFIO1", "RFIO2" };//, "RFIO3", "RFIO4"
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuyHaoXML
{
    public class GlobalData
    {
        public static ConfInfor confInfor = new ConfInfor();
        public static List<Attenuator> lstAttenuatorFromFile = new List<Attenuator>();
        public static List<Attenuator> lstAttenuatorResult = new List<Attenuator>();

        public static List<Attenuator> lstAttenuatorMasterResult = new List<Attenuator>();
        public static List<Attenuator> lstMasterAttenuatorFromFile = new List<Attenuator>();
        public static AttenuatorStyle attenuatorStyle = AttenuatorStyle.DEFAULT;
        public static ButtonStatus buttonStatus = ButtonStatus.STATIONPRESS;
    }
    public enum AttenuatorStyle { MASTER, DEFAULT}
    public enum ButtonStatus { STATIONPRESS, MASTERSTAION }

    public class ConfInfor
    {
        string _instrumentStype;
        string _ip;
        string _receivePort;
        string _transmissionPort;
        string _transmissionPower;
        string _masterFileLink;
        string _attenuatorTimes;
        string _connectorAttenuator;
        public ConfInfor() {; }
        public ConfInfor(string instrumentStype, string ip, string receivePort, string transmissionPort, string transmissionPower,string masterFileLink, string attenuatorTimes, string connectorAttenuator)
        {
            this.InstrumentStype = instrumentStype;
            this.IP = ip;
            this.ReceivePort = receivePort;
            this.TransmissionPort = transmissionPort;
            this.TransmissionPower = transmissionPower;
            this.MasterFileLink = masterFileLink;
            this.AttenuatorTimes = attenuatorTimes;
            this.ConnectorAttenuator = connectorAttenuator;
        }
        public string InstrumentStype { get => _instrumentStype; set => _instrumentStype = value; }
        public string IP { get => _ip; set => _ip = value; }
        public string ReceivePort { get => _receivePort; set => _receivePort = value; }
        public string TransmissionPort { get => _transmissionPort; set => _transmissionPort = value; }
        public string TransmissionPower { get => _transmissionPower; set => _transmissionPower = value; }
        public string MasterFileLink { get => _masterFileLink; set => _masterFileLink = value; }
        public string AttenuatorTimes { get => _attenuatorTimes; set => _attenuatorTimes = value; }
        public string ConnectorAttenuator { get => _connectorAttenuator; set => _connectorAttenuator = value; }

        public bool SaveConfig()
        {
            try
            {
                Properties.Settings.Default.instrumentStyle = InstrumentStype;
                Properties.Settings.Default.ip = IP;
                Properties.Settings.Default.receivePort = ReceivePort;
                Properties.Settings.Default.transmissionPort = TransmissionPort;
                Properties.Settings.Default.transmissionPower = TransmissionPower;
                Properties.Settings.Default.masterFileLink = MasterFileLink;
                Properties.Settings.Default.connectorAtenuator = ConnectorAttenuator;
                Properties.Settings.Default.attenuatorTimes = AttenuatorTimes;

                Properties.Settings.Default.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public ConfInfor Get()
        {
            InstrumentStype = Properties.Settings.Default.instrumentStyle;
            IP = Properties.Settings.Default.ip;
            ReceivePort = Properties.Settings.Default.receivePort;
            TransmissionPort = Properties.Settings.Default.transmissionPort;
            TransmissionPower = Properties.Settings.Default.transmissionPower;
            MasterFileLink = Properties.Settings.Default.masterFileLink;
            ConnectorAttenuator = Properties.Settings.Default.connectorAtenuator;
            AttenuatorTimes= Properties.Settings.Default.attenuatorTimes;
            return this;
        }         
    }
    public class Attenuator
    {
        string _PathName;
        string _Frequency;
        string _Value;
        string _Delta;

        public string PathName { get => _PathName; set => _PathName = value; }
        public string Frequency { get => _Frequency; set => _Frequency = value; }
        public string Value { get => _Value; set => _Value = value; }
        public string Delta { get => _Delta; set => _Delta = value; }
        public Attenuator() {;}
        public  Attenuator(string pathName,string frequency,string value,string delta)
        {
            PathName = pathName;
            Frequency = frequency;
            Value = value;
            Delta = delta;
        }
    }

}

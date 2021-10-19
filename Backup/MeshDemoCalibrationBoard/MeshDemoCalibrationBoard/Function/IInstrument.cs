using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshDemoCalibrationBoard.Function {
    public abstract class IInstrument {

        public bool isConnected = false;
        public IInstrument(string gpib_port, string power_transmit, string receive_port, string transmission_port) { }
        public abstract void config_HT20_TxTest_Transmitter(string frequency);
        public abstract string config_HT20_RxTest_Receiver(string frequency);
        public abstract void Dispose();

    }
}

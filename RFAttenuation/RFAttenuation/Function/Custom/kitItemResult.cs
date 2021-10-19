using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFAttenuation.Function.Custom {

    public class kitItemResult {

        public kitItemResult() {
            Frequency = Power1 = Power2 = Power3 = AVG = PowerTransmit = Attenuation = Connector = "";
        }

        public string Frequency { get; set; }
        public string PowerTransmit { get; set; }
        public string Power1 { get; set; }
        public string Power2 { get; set; }
        public string Power3 { get; set; }
        public string AVG { get; set; }
        public string Connector { get; set; }
        public string Attenuation { get; set; }

        public string ToText() {
            return $"{Frequency},{PowerTransmit},{Power1},{Power2},{Power3},{AVG},{Connector},{Attenuation}";
        }


        public override string ToString() {
            return $"{Frequency.PadLeft(20, ' ')}" +
                   $"{PowerTransmit.PadLeft(20, ' ')}" +
                   $"{Power1.PadLeft(20, ' ')}" +
                   $"{Power2.PadLeft(20, ' ')}" +
                   $"{Power3.PadLeft(20, ' ')}" +
                   $"{AVG.PadLeft(20, ' ')}" +
                   $"{Connector.PadLeft(20, ' ')}" +
                   $"{Attenuation.PadLeft(20, ' ')}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFAttenuation.Function.Custom {
    public class attItemResult {

        public attItemResult() {
            Frequency = Antenna1Kit1__Power1 = Antenna1Kit1__Power2 = Antenna1Kit1__Power3 = Antenna1Kit1__AVG = PowerTransmit = Antenna1 = Connector = "";
            Antenna2Kit2__Power1 = Antenna2Kit2__Power2 = Antenna2Kit2__Power3 = Antenna2Kit2__AVG = Antenna2 = "";
        }

        public string Frequency { get; set; }
        public string PowerTransmit { get; set; }
        public string Connector { get; set; }
        
        public string Antenna1Kit1__Power1 { get; set; }
        public string Antenna1Kit1__Power2 { get; set; }
        public string Antenna1Kit1__Power3 { get; set; }
        public string Antenna1Kit1__AVG { get; set; }
        public string Kit1Power { get; set; }
        public string Antenna1 { get; set; }

        public string Antenna2Kit2__Power1 { get; set; }
        public string Antenna2Kit2__Power2 { get; set; }
        public string Antenna2Kit2__Power3 { get; set; }
        public string Antenna2Kit2__AVG { get; set; }
        public string Kit2Power { get; set; }
        public string Antenna2 { get; set; }

        public override string ToString() {
            return $"{Frequency.PadLeft(20, ' ')}" +
                   $"{PowerTransmit.PadLeft(20, ' ')}" +
                   $"{Antenna1Kit1__Power1.PadLeft(20, ' ')}" +
                   $"{Antenna1Kit1__Power2.PadLeft(20, ' ')}" +
                   $"{Antenna1Kit1__Power3.PadLeft(20, ' ')}" +
                   $"{Antenna1Kit1__AVG.PadLeft(20, ' ')}" +
                   $"{Kit1Power.PadLeft(20, ' ')}" +
                   $"{Antenna1.PadLeft(20, ' ')}" +
                   $"{Antenna2Kit2__Power1.PadLeft(20, ' ')}" +
                   $"{Antenna2Kit2__Power2.PadLeft(20, ' ')}" +
                   $"{Antenna2Kit2__Power3.PadLeft(20, ' ')}" +
                   $"{Antenna2Kit2__AVG.PadLeft(20, ' ')}" +
                   $"{Kit2Power.PadLeft(20, ' ')}" +
                   $"{Antenna2.PadLeft(20, ' ')}";
        }

    }
}

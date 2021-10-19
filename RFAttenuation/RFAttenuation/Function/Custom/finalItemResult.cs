using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFAttenuation.Function.Custom {
    public class finalItemResult {
        
        public finalItemResult() {
            Frequency =  "";
            AT1Power1 = AT1Power2 = AT1Power3 = AT1Power4 = AT1Power5 = AT1Power6 = AT1Power7 = AT1Power8 = AT1Power9 = AT1Power10 = AT1AVG = "";
            AT2Power1 = AT2Power2 = AT2Power3 = AT2Power4 = AT2Power5 = AT2Power6 = AT2Power7 = AT2Power8 = AT2Power9 = AT2Power10 = AT2AVG = "";
        }

        public string Frequency { get; set; }
        public string AT1AVG { get; set; }
        public string AT1Power1 { get; set; }
        public string AT1Power2 { get; set; }
        public string AT1Power3 { get; set; }
        public string AT1Power4 { get; set; }
        public string AT1Power5 { get; set; }
        public string AT1Power6 { get; set; }
        public string AT1Power7 { get; set; }
        public string AT1Power8 { get; set; }
        public string AT1Power9 { get; set; }
        public string AT1Power10 { get; set; }

        public string AT2AVG { get; set; }
        public string AT2Power1 { get; set; }
        public string AT2Power2 { get; set; }
        public string AT2Power3 { get; set; }
        public string AT2Power4 { get; set; }
        public string AT2Power5 { get; set; }
        public string AT2Power6 { get; set; }
        public string AT2Power7 { get; set; }
        public string AT2Power8 { get; set; }
        public string AT2Power9 { get; set; }
        public string AT2Power10 { get; set; }
        


        public override string ToString() {
            return $"{Frequency.PadLeft(20, ' ')}" +
                   $"{AT1AVG.PadLeft(20, ' ')}" +
                   $"{AT1Power1.PadLeft(20, ' ')}" +
                   $"{AT1Power2.PadLeft(20, ' ')}" +
                   $"{AT1Power3.PadLeft(20, ' ')}" +
                   $"{AT1Power4.PadLeft(20, ' ')}" +
                   $"{AT1Power5.PadLeft(20, ' ')}" +
                   $"{AT1Power6.PadLeft(20, ' ')}" +
                   $"{AT1Power7.PadLeft(20, ' ')}" +
                   $"{AT1Power8.PadLeft(20, ' ')}" +
                   $"{AT1Power9.PadLeft(20, ' ')}" +
                   $"{AT1Power10.PadLeft(20, ' ')}" +
                   $"{AT2AVG.PadLeft(20, ' ')}" +
                   $"{AT2Power1.PadLeft(20, ' ')}" +
                   $"{AT2Power2.PadLeft(20, ' ')}" +
                   $"{AT2Power3.PadLeft(20, ' ')}" +
                   $"{AT2Power4.PadLeft(20, ' ')}" +
                   $"{AT2Power5.PadLeft(20, ' ')}" +
                   $"{AT2Power6.PadLeft(20, ' ')}" +
                   $"{AT2Power7.PadLeft(20, ' ')}" +
                   $"{AT2Power8.PadLeft(20, ' ')}" +
                   $"{AT2Power9.PadLeft(20, ' ')}" +
                   $"{AT2Power10.PadLeft(20, ' ')}";
        }

    }
}

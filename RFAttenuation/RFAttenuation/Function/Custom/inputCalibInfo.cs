using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFAttenuation.Function.Custom {
    public class inputCalibInfo {

        public inputCalibInfo() {
            Product = "";
            remainFile = "";
            Value = "";
        }

        public string Product { get; set; }
        public string remainFile { get; set; }
        public string Value { get; set; }

    }
}

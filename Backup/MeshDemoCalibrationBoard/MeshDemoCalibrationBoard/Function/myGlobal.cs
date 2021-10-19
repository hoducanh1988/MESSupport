using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshDemoCalibrationBoard.Function {
    public class myGlobal {

        public static TestingInformation myTesting = new TestingInformation();
        public static SettingInformation mySetting = new SettingInformation();

        public static List<string> listFrequency = null;
        public static List<ResultInfo> listResult = null;

        static myGlobal() {
            listFrequency = new List<string>();
            listResult = new List<ResultInfo>();

            string[] buffer = mySetting.Frequencies.Split(',');
            foreach (var b in buffer) {
                if (string.IsNullOrEmpty(b) == false && string.IsNullOrWhiteSpace(b) == false) {
                    listFrequency.Add(b);
                    listResult.Add(new ResultInfo() { Freq = b });
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MeshConfigDhcpServer.Function.Global;
using MeshConfigDhcpServer.Function.Custom;

namespace MeshConfigDhcpServer.Function.IO {

    public class FileLog {

        TestingInformation testing = null;

        public FileLog(TestingInformation _testing) {
            if (!Directory.Exists(myGlobal.logFilePath)) Directory.CreateDirectory(myGlobal.logFilePath);
            this.testing = _testing;
        }


        public bool Save() {
            string f = string.Format("{0}\\{1}_{2}_{3}.txt", 
                                      myGlobal.logFilePath,
                                      testing.macAddress,
                                      DateTime.Now.ToString("yyyyMMdd_HHmmss"), 
                                      testing.totalResult);

            try {
                File.WriteAllText(f, testing.logSystem);
                return true;
            } catch { return false; }
        }

    }
}

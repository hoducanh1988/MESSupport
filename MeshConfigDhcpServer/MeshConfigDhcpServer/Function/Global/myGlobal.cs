using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeshConfigDhcpServer.Function.Custom;

namespace MeshConfigDhcpServer.Function.Global {
    public class myGlobal {

        public static string settingFileFullName = string.Format("{0}setting.xml", AppDomain.CurrentDomain.BaseDirectory);
        public static string helpFileFullName = string.Format("{0}guide.xps", System.AppDomain.CurrentDomain.BaseDirectory);
        public static string logFilePath = string.Format("{0}Log", AppDomain.CurrentDomain.BaseDirectory);

        public static SettingInformation mySetting = null;
        public static TestingInformation myTesting = new TestingInformation();

    }
}

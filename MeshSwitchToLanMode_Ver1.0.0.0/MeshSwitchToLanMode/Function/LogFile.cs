using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MeshSwitchToLanMode.Function {
    public class LogFile {

        string dir_log_system = AppDomain.CurrentDomain.BaseDirectory + "LogSystem";
        string dir_log_ssh = AppDomain.CurrentDomain.BaseDirectory + "LogSSH";
        string dir_date = DateTime.Now.ToString("yyyy-MM-dd");

        public LogFile() {
            if (Directory.Exists(string.Format("{0}\\{1}", dir_log_system, dir_date)) == false)
                Directory.CreateDirectory(string.Format("{0}\\{1}", dir_log_system, dir_date));

            if (Directory.Exists(string.Format("{0}\\{1}", dir_log_ssh, dir_date)) == false)
                Directory.CreateDirectory(string.Format("{0}\\{1}", dir_log_ssh, dir_date));
        }

        public bool saveLogSystem(string data, string mac, string result) {
            try {
                string f_name = string.Format("{0}_{1}_{2}.txt", mac, DateTime.Now.ToString("HHmmss"), result);
                File.WriteAllText(string.Format("{0}\\{1}\\{2}", dir_log_system, dir_date, f_name), data);
                return true;
            } catch {
                return false;
            }
        }

        public bool saveLogSsh(string data, string mac, string result) {
            try {
                string f_name = string.Format("{0}_{1}_{2}.txt", mac, DateTime.Now.ToString("HHmmss"), result);
                File.WriteAllText(string.Format("{0}\\{1}\\{2}", dir_log_ssh, dir_date, f_name), data);
                return true;
            }
            catch {
                return false;
            }
        }



    }
}

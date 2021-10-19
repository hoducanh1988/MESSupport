using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace pingNetwork {
    class Program {

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public static bool PingHost(string nameOrAddress) {
            System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
            PingOptions options = new PingOptions();
            // Use the default Ttl value which is 128, 
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted. 
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 60;
            try {
                PingReply reply = pingSender.Send(nameOrAddress, timeout, buffer, options);
                if (reply.Status == IPStatus.Success) {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch {
                return false;
            }
        }

        public static void Main(string[] args) {
            var handle = GetConsoleWindow();
            // Hide
            ShowWindow(handle, SW_HIDE);

            int count = 0;
            int count_pass = 0;
        STA:
            count = 0;
            Console.Clear();
            Console.WriteLine("Pinging 192.168.88.1 with 32 bytes of data:");
            //delete file result
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ping_pass.txt") == true) {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ping_pass.txt");
                Thread.Sleep(100);
            }

        PING:
            count++;
            if (count >= 10) goto STA;
            bool r = PingHost("192.168.88.1");
            string msg_result = r ? "Reply from 192.168.88.1: bytes=32 time<1ms TTL=64" : "Request time out";
            Console.WriteLine(msg_result);
            count_pass = r ? (count_pass + 1) : 0;
            if (count_pass >= 3) goto FINISH;
            else {
                Thread.Sleep(1000);
                goto PING;
            }

        FINISH:
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "ping_pass.txt", "1");
            Thread.Sleep(100);
        }

        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace callBackApplication {
    class Program {
        static void Main(string[] args) {

            string[] buffer = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "callback.txt");
            string path = buffer[0];
            Console.WriteLine(path);

            Thread.Sleep(1000);

            Process.Start(path);
            
        }
    }
}

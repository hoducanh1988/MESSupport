using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.IO.Ports;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace ToolTestRx
{
    class ShowRTB
    {
        public static void SetTextRTB(RichTextBox rtb_Rx, string text)
        {

            {
                //Them hieu ung mau sac...vv cho text hien thi
                if (text.Contains("FAIL") || text.Contains("Fail") || text.Contains("ERROR") || text.Contains("Bai Test Wifi Chua The Thuc Hien!"))
                {
                    rtb_Rx.SelectionColor = Color.Red;
                    //rtb_Rx.SelectionFont = new Font(rtb_Rx.SelectionFont, FontStyle.Bold);
                }
                else if (text.Contains("PASS") || text.Contains("Xong") || text.Contains("xong") || text.Contains("thanh cong") || text.Contains("Da Ping duoc") || text.Contains("OK"))
                {
                    rtb_Rx.SelectionColor = Color.LightGreen;
                    //rtb_Rx.SelectionFont = new Font(rtb_Rx.SelectionFont, FontStyle.Bold);
                }
                else if (text.Contains("Bai test thu:") || text.Contains("Do chuan") || text.Contains("Antena") || text.Contains("MAC Address="))
                {
                    rtb_Rx.SelectionColor = Color.Blue;
                    rtb_Rx.SelectionFont = new Font(rtb_Rx.SelectionFont, FontStyle.Bold);
                }
                else if (text.Contains("Không") || text.Contains("KHÔNG") )
                {
                    rtb_Rx.SelectionColor = Color.Red;
                    rtb_Rx.SelectionFont = new Font(rtb_Rx.SelectionFont, FontStyle.Bold);
                }
                else
                {
                    rtb_Rx.SelectionColor = Color.Blue;
                }
                //
                rtb_Rx.AppendText(text);
                rtb_Rx.Refresh();
            }
        }

    }
}

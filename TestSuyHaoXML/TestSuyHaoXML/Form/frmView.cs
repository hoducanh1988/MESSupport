using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TestSuyHaoXML
{
    public partial class frmView : Form
    {
        public frmView()
        {
            InitializeComponent();
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "xml file|*.xml";
            openfile.ShowDialog();
            txtLink.Text = openfile.FileName;
            ShowFile();
        }
        void ShowFile()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root;
            string fileName = txtLink.Text;
            doc.Load(fileName);
            root = doc.DocumentElement;
            dvwAttenuator.Rows.Clear();
            dvwAttenuator.ColumnCount = 3;
            XmlNodeList ds = root.SelectNodes("Path");
            foreach (XmlNode item in ds)
            {
                string Name = item.SelectSingleNode("PathName").InnerText;
               // if (Name == cbDay.SelectedItem.ToString())
                {
                    XmlNodeList ds1 = item.SelectNodes("DataList/Data");
                    int i = 0;
                    foreach (XmlNode item1 in ds1)
                    {
                        dvwAttenuator.Rows.Add();
                        dvwAttenuator.Rows[i].Cells[0].Value = item1.SelectSingleNode("Frequency").InnerText;
                        dvwAttenuator.Rows[i].Cells[1].Value = item1.SelectSingleNode("Value").InnerText;
                        dvwAttenuator.Rows[i].Cells[2].Value = item1.SelectSingleNode("Delta").InnerText;
                        i++;
                    }
                }
            }
        }

        private void TxtLink_TextChanged(object sender, EventArgs e)
        {
        }
        private void FrmView_Load(object sender, EventArgs e)
        {

        }
    }
}

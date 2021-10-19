using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TestSuyHaoXML
{
    public class XMLData
    {
        public static List<Attenuator> LoadFile(string fileName, out List<string> lstPathName)
        {
            List<Attenuator> ret = new List<Attenuator>();
            lstPathName = new List<string>();
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root;
                doc.Load(fileName);
                root = doc.DocumentElement;
                XmlNodeList ds = root.SelectNodes("Path");
                foreach (XmlNode item in ds)
                {
                    string Name = item.SelectSingleNode("PathName").InnerText;
                    lstPathName.Add(Name);
                    XmlNodeList ds1 = item.SelectNodes("DataList/Data");
                    foreach (XmlNode item1 in ds1)
                    {
                        Attenuator attenuator = new Attenuator(Name, item1.SelectSingleNode("Frequency").InnerText,
                        item1.SelectSingleNode("Value").InnerText, item1.SelectSingleNode("Delta").InnerText);
                        ret.Add(attenuator);
                    }
                }
                return ret;
            }
            catch
            {
                // Trường Hợp Mở File Không Đúng Định Dạng
                lstPathName = new List<string>();
                return new List<Attenuator>();
            }
        }
        public static bool SaveFile(List<Attenuator> lstAttenuator, string fileName)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root;
                doc.Load(fileName);
                root = doc.DocumentElement;
                XmlNodeList ds = root.SelectNodes("Path");
                foreach (XmlNode item in ds)//BH0_LP,BH1_LP....
                {
                    string Name = item.SelectSingleNode("PathName").InnerText;
                    XmlNodeList ds1 = item.SelectNodes("DataList/Data");
                    foreach (XmlNode item1 in ds1)//data1 -> data n
                    {
                        string Freq  = item1.SelectSingleNode("Frequency").InnerText;
                        XmlNodeList ds2 = item.SelectNodes("DataList");
                        foreach (XmlNode item2 in ds2)
                        {
                            XmlNode datalist = item2.SelectSingleNode("Data[Frequency='" + Freq + "']");
                            if (datalist != null)
                            {
                                foreach (var lst in lstAttenuator)
                                {
                                    if ((lst.PathName == Name) && (Freq == lst.Frequency))
                                    {
                                        XmlNode newNote = doc.CreateElement("Data");

                                        XmlElement frequency = doc.CreateElement("Frequency");
                                        frequency.InnerText = lst.Frequency;
                                        newNote.AppendChild(frequency);

                                        XmlElement pow = doc.CreateElement("Value");
                                        pow.InnerText = lst.Value;
                                        newNote.AppendChild(pow);

                                        XmlElement delta = doc.CreateElement("Delta");
                                        delta.InnerText = lst.Delta;
                                        newNote.AppendChild(delta);

                                        item2.ReplaceChild(newNote, datalist);
                                        doc.Save(fileName);
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex) 

            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

    }
}

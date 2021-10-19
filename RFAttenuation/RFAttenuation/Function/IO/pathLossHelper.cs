using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RFAttenuation.Function.IO {


    public class pathLossHelper {


        public class pathItem {

            public pathItem() {
                PathName = PathID = AdditionalLoss = Cal_Date = TesterID = CalCable_PathName = UseCalCable = "";
                dataItems = new List<dataItem>();
            }

            public string PathName { get; set; }
            public string PathID { get; set; }
            public string AdditionalLoss { get; set; }
            public string Cal_Date { get; set; }
            public string TesterID { get; set; }
            public string CalCable_PathName { get; set; }
            public string UseCalCable { get; set; }
            public List<dataItem> dataItems { get; set; }
        }

        public class dataItem {
            public dataItem() {
                Frequency = Value = Delta = "";
            }

            public string Frequency { get; set; }
            public string Value { get; set; }
            public string Delta { get; set; }

            public override string ToString() {
                return $"{Frequency.PadLeft(8, ' ')},{Value.PadLeft(8, ' ')},{Delta.PadLeft(8, ' ')}";
            }
        }

        string file_full_name = "";

        public pathLossHelper(string file_full_name) {
            this.file_full_name = file_full_name;
        }

        public List<pathItem> FromXML() {
            try {
                if (File.Exists(this.file_full_name) == false) return null;
                var results = XDocument.Load(this.file_full_name).Root.Descendants("Path")
                                .Select(x => new pathItem {
                                    PathName = x.Element("PathName").Value,
                                    PathID = x.Element("PathID").Value,
                                    AdditionalLoss = x.Element("AdditionalLoss").Value,
                                    Cal_Date = x.Element("Cal_Date").Value,
                                    TesterID = x.Element("TesterID").Value,
                                    CalCable_PathName = x.Element("CalCable_PathName").Value,
                                    UseCalCable = x.Element("UseCalCable").Value,
                                    dataItems = x.Descendants("DataList").Descendants("Data")
                                                 .Select(y => new dataItem {
                                                     Frequency = y.Element("Frequency").Value,
                                                     Value = y.Element("Value").Value,
                                                     Delta = y.Element("Delta").Value
                                                 }).ToList()
                                }).ToList();
                return results;
            }
            catch { return null; }
        }

        public bool ToXML(List<pathItem> listPathItem) {
            try {
                File.Delete(this.file_full_name);
                using (StreamWriter sw = new StreamWriter(this.file_full_name, true, UnicodeEncoding.UTF8)) {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"us-ascii\"?>");
                    sw.WriteLine("<PathList xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");

                    //write path here ***
                    #region write path
                    foreach (var p in listPathItem) {
                        sw.WriteLine("  <Path>");
                        sw.WriteLine($"    <PathName>{p.PathName}</PathName>");
                        sw.WriteLine($"    <PathID>{p.PathID}</PathID>");
                        sw.WriteLine($"    <AdditionalLoss>{p.AdditionalLoss}</AdditionalLoss>");
                        sw.WriteLine($"    <Cal_Date>{p.Cal_Date}</Cal_Date>");
                        sw.WriteLine($"    <TesterID>{p.TesterID}</TesterID>");
                        sw.WriteLine($"    <CalCable_PathName>{p.CalCable_PathName}</CalCable_PathName>");
                        sw.WriteLine($"    <UseCalCable>{p.UseCalCable}</UseCalCable>");
                        sw.WriteLine("    <DataList>");

                        //write data here ***
                        #region write data
                        foreach (var d in p.dataItems) {
                            sw.WriteLine("      <Data>");
                            sw.WriteLine($"        <Frequency>{d.Frequency}</Frequency>");
                            sw.WriteLine($"        <Value>{d.Value}</Value>");
                            sw.WriteLine($"        <Delta>{d.Delta}</Delta>");
                            sw.WriteLine("      </Data>");
                        }
                        #endregion
                        //***

                        sw.WriteLine("    </DataList>");
                        sw.WriteLine("  </Path>");
                    }
                    #endregion
                    //***

                    sw.WriteLine("</PathList>");
                }
                return true;
            }
            catch { return false; }
        }


        public bool ToXML(string file_name, List<pathItem> listPathItem) {
            try {
                File.Delete(file_name);
                using (StreamWriter sw = new StreamWriter(file_name, true, Encoding.UTF8)) {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"us-ascii\"?>");
                    sw.WriteLine("<PathList xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");

                    //write path here ***
                    #region write path
                    foreach (var p in listPathItem) {
                        sw.WriteLine("  <Path>");
                        sw.WriteLine($"    <PathName>{p.PathName}</PathName>");
                        sw.WriteLine($"    <PathID>{p.PathID}</PathID>");
                        sw.WriteLine($"    <AdditionalLoss>{p.AdditionalLoss}</AdditionalLoss>");
                        sw.WriteLine($"    <Cal_Date>{p.Cal_Date}</Cal_Date>");
                        sw.WriteLine($"    <TesterID>{p.TesterID}</TesterID>");
                        sw.WriteLine($"    <CalCable_PathName>{p.CalCable_PathName}</CalCable_PathName>");
                        sw.WriteLine($"    <UseCalCable>{p.UseCalCable}</UseCalCable>");
                        sw.WriteLine("    <DataList>");

                        //write data here ***
                        #region write data
                        foreach (var d in p.dataItems) {
                            sw.WriteLine("      <Data>");
                            sw.WriteLine($"        <Frequency>{d.Frequency}</Frequency>");
                            sw.WriteLine($"        <Value>{d.Value}</Value>");
                            sw.WriteLine($"        <Delta>{d.Delta}</Delta>");
                            sw.WriteLine("      </Data>");
                        }
                        #endregion
                        //***

                        sw.WriteLine("    </DataList>");
                        sw.WriteLine("  </Path>");
                    }
                    #endregion
                    //***

                    sw.WriteLine("</PathList>");
                }
                return true;
            }
            catch { return false; }
        }
    }


}

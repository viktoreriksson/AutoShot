using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace AutoShot {
    class Config {
        private string xmlPath;

        public static readonly int DelaySecondsDefault = 60;
        public static readonly long ImageQualityDefault = 85;
        public Config(string XmlPath) {
            xmlPath = XmlPath;
        }

        public bool ConfigExists() {
            return File.Exists(xmlPath);
        }

        public string ReadConfigVariable(string variablepath) {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            string variablevalue = xmlDocument.SelectSingleNode(variablepath).InnerText;
            return variablevalue;
        }

        public void WriteConfig(XElement xmlTree) {
            xmlTree.Save(xmlPath);
        }

        public void WriteDefaultConfig() {
            XElement xmlTree = new XElement("Config",
                new XElement("Options",
                    new XElement("ScreenshotDelaySeconds", DelaySecondsDefault),
                    new XElement("ScreenshotQuality", ImageQualityDefault)));
            xmlTree.Save(xmlPath);
        }
    }
}

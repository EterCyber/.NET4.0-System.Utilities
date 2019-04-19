namespace System.Utilities.Common
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml;

    public class XMLHelper
    {
        private static string _xmlPath;
        private static XmlElement _xmlRoot;
        private static XmlDocument k__BackingField;

        public XMLHelper(string xmlPath)
        {
            _xmlPath = xmlPath;
            this.LoadXmlDoc();
        }

        public int Check(string xpath)
        {
            return this.SelectNodeList(xpath).Count;
        }

        public void Create(string rootName, string encode)
        {
            this.CreateXmlDoc(rootName, encode);
        }

        public XmlElement CreateElec(string elecName, string elecValue)
        {
            return this.CreateElement(_xmlRoot, elecName, elecValue);
        }

        public XmlElement CreateElec(XmlElement xmlParentElec, string elecName, string elecValue)
        {
            if (xmlParentElec != null)
            {
                return this.CreateElement(xmlParentElec, elecName, elecValue);
            }
            return null;
        }

        private XmlElement CreateElement(XmlNode _xmldocSelect, string elecName, string elecValue)
        {
            if (_xmldocSelect != null)
            {
                XmlElement newChild = _xmlDoc.CreateElement(elecName);
                newChild.InnerText = elecValue;
                _xmldocSelect.AppendChild(newChild);
                return newChild;
            }
            return null;
        }

        private void CreateXmlDoc(string rootName, string encode)
        {
            if (!File.Exists(_xmlPath))
            {
                _xmlDoc = new XmlDocument();
                XmlDeclaration newChild = _xmlDoc.CreateXmlDeclaration("1.0", encode, null);
                _xmlDoc.AppendChild(newChild);
                _xmlRoot = _xmlDoc.CreateElement("", rootName, "");
                _xmlDoc.AppendChild(_xmlRoot);
            }
        }

        public static string FormatXml(string xmlString, string encode)
        {
            MemoryStream w = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(w, null);
            XmlDocument document = new XmlDocument();
            writer.Formatting = Formatting.Indented;
            document.LoadXml(xmlString);
            document.WriteTo(writer);
            writer.Flush();
            writer.Close();
            string str = Encoding.GetEncoding(encode).GetString(w.ToArray());
            w.Close();
            return str;
        }

        public void LoadXmlDoc()
        {
            if (File.Exists(_xmlPath))
            {
                _xmlDoc = new XmlDocument();
                _xmlDoc.Load(_xmlPath);
                _xmlRoot = _xmlDoc.DocumentElement;
            }
        }

        public static void Loop(XmlNodeList nodeList, Action<XmlNode> xmlNode)
        {
            if (nodeList != null)
            {
                foreach (XmlNode node in nodeList)
                {
                    xmlNode(node);
                }
            }
        }

        public void Save()
        {
            if (_xmlDoc != null)
            {
                _xmlDoc.Save(_xmlPath);
            }
        }

        public XmlNode Select(string xPath)
        {
            return _xmlDoc.SelectSingleNode(xPath);
        }

        public XmlNodeList SelectNodeList(string xPath)
        {
            return _xmlDoc.SelectNodes(xPath);
        }

        public void SetAttribute(XmlElement xElement, string attrName, string attrValue)
        {
            if (xElement != null)
            {
                xElement.SetAttribute(attrName, attrValue);
            }
        }

        public string ShowXml()
        {
            if (_xmlDoc != null)
            {
                return _xmlDoc.OuterXml;
            }
            return string.Empty;
        }

        private static XmlDocument _xmlDoc
        {
            [CompilerGenerated]
            get
            {
                return k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                k__BackingField = value;
            }
        }
    }
}


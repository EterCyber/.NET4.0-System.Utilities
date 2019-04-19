namespace System.Utilities.Common
{
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Xml;

    public class XML2Helper
    {
        private XmlDocument xmldoc;
        private string xmlPath;

        public XML2Helper(string version, Encoding encoding, string path)
        {
            this.xmldoc = new XmlDocument();
            this.xmlPath = path;
            if (!File.Exists(path))
            {
                if (!FileHelper.CreatePath(path))
                {
                    throw new ArgumentException(string.Format("创建XML保存路径：{0}失败！", path));
                }
                XmlDeclaration newChild = this.xmldoc.CreateXmlDeclaration(version, encoding.BodyName, null);
                this.xmldoc.AppendChild(newChild);
            }
            else
            {
                this.xmldoc.Load(path);
            }
        }

        public XML2Helper(string version, Encoding encoding, string path, string documentElementName)
        {
            this.xmldoc = new XmlDocument();
            this.xmlPath = path;
            if (!File.Exists(path))
            {
                if (!FileHelper.CreatePath(path))
                {
                    throw new ArgumentException(string.Format("创建XML保存路径：{0}失败！", path));
                }
                XmlDeclaration newChild = this.xmldoc.CreateXmlDeclaration(version, encoding.BodyName, null);
                this.xmldoc.AppendChild(newChild);
                this.CreateDocumentElement(documentElementName, string.Format("/{0}", documentElementName));
                this.Save();
            }
            else
            {
                this.xmldoc.Load(path);
            }
        }

        public XmlElement CreateChildElement(XmlElement parentElement, string elementName)
        {
            XmlElement newChild = this.xmldoc.CreateElement(elementName);
            parentElement.AppendChild(newChild);
            return newChild;
        }

        public XmlElement CreateChildElement(XmlElement parentElement, string elementName, string innerText)
        {
            XmlElement newChild = this.xmldoc.CreateElement(elementName);
            newChild.InnerText = innerText;
            parentElement.AppendChild(newChild);
            return newChild;
        }

        public XmlElement CreateChildElement(XmlElement parentElement, string elementName, string xpath, Action<XmlElement> setAttributeHanlder, Action<XmlElement> updateAttributeHanlder)
        {
            XmlNode node = this.xmldoc.SelectSingleNode(xpath);
            if (node == null)
            {
                XmlElement element = this.xmldoc.CreateElement(elementName);
                if (setAttributeHanlder != null)
                {
                    setAttributeHanlder(element);
                }
                parentElement.AppendChild(element);
                return element;
            }
            XmlElement element2 = (XmlElement) node;
            if (updateAttributeHanlder != null)
            {
                updateAttributeHanlder(element2);
            }
            return element2;
        }

        public XmlElement CreateDocumentElement(string elementName)
        {
            XmlElement newChild = this.xmldoc.CreateElement("", elementName, "");
            this.xmldoc.AppendChild(newChild);
            return newChild;
        }

        public XmlElement CreateDocumentElement(string elementName, string xpath)
        {
            XmlNode node = this.xmldoc.SelectSingleNode(xpath);
            if (node != null)
            {
                return (XmlElement) node;
            }
            return this.CreateDocumentElement(elementName);
        }

        public XmlElement CreateParentElement(XmlElement xmlDocument, string elementName, string xpath, Action<XmlElement> SetAttributeHanlder, Action<XmlElement> UpdateAttributeHanlder)
        {
            XmlNode node = this.xmldoc.SelectSingleNode(xpath);
            if (node == null)
            {
                XmlElement element = this.xmldoc.CreateElement("", elementName, "");
                if (SetAttributeHanlder != null)
                {
                    SetAttributeHanlder(element);
                }
                this.xmldoc.DocumentElement.AppendChild(element);
                return element;
            }
            XmlElement element2 = (XmlElement) node;
            if (UpdateAttributeHanlder != null)
            {
                UpdateAttributeHanlder(element2);
            }
            return element2;
        }

        public void Save()
        {
            this.xmldoc.Save(this.xmlPath);
        }

        public XmlNodeList SelectNodes(string xpath)
        {
            return this.xmldoc.SelectNodes(xpath);
        }

        public bool SelectSingleNode(string xpath)
        {
            XmlElement element = (XmlElement) this.xmldoc.SelectSingleNode(xpath);
            return (element != null);
        }

        public bool SelectSingleNode(string xpath, Action<XmlElement> updateNodeHanlder)
        {
            XmlElement element = (XmlElement) this.xmldoc.SelectSingleNode(xpath);
            if ((element != null) && (updateNodeHanlder != null))
            {
                updateNodeHanlder(element);
            }
            return (element != null);
        }

        public string ShowXml()
        {
            if (this.xmldoc != null)
            {
                return this.xmldoc.OuterXml;
            }
            return string.Empty;
        }

        public DataSet ToDataSet()
        {
            DataSet set2;
            try
            {
                using (XmlNodeReader reader = new XmlNodeReader(this.xmldoc))
                {
                    DataSet set = new DataSet();
                    set.ReadXml(reader);
                    set2 = set;
                }
            }
            catch (Exception)
            {
                set2 = null;
            }
            return set2;
        }
    }
}


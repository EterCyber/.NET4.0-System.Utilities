namespace System.Utilities.WinForm.Base
{
    using System;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class TreeViewSerializer
    {
        private const string XmlNodeImageIndexAtt = "imageindex";
        private const string XmlNodeTag = "node";
        private const string XmlNodeTagAtt = "tag";
        private const string XmlNodeTextAtt = "text";
        private string XmlSaveFullPath = string.Empty;

        public TreeViewSerializer(string path)
        {
            this.XmlSaveFullPath = path;
        }

        public void DeserializeTreeView(TreeView treeView)
        {
            XmlTextReader reader = null;
            try
            {
                treeView.BeginUpdate();
                reader = new XmlTextReader(this.XmlSaveFullPath);
                TreeNode parent = null;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "node")
                        {
                            TreeNode node2 = new TreeNode();
                            bool isEmptyElement = reader.IsEmptyElement;
                            int attributeCount = reader.AttributeCount;
                            if (attributeCount > 0)
                            {
                                for (int i = 0; i < attributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);
                                    this.SetAttributeValue(node2, reader.Name, reader.Value);
                                }
                            }
                            if (parent != null)
                            {
                                parent.Nodes.Add(node2);
                            }
                            else
                            {
                                treeView.Nodes.Add(node2);
                            }
                            if (!isEmptyElement)
                            {
                                parent = node2;
                            }
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Name == "node")
                        {
                            parent = parent.Parent;
                        }
                    }
                    else if (reader.NodeType != XmlNodeType.XmlDeclaration)
                    {
                        if (reader.NodeType == XmlNodeType.None)
                        {
                            return;
                        }
                        if (reader.NodeType == XmlNodeType.Text)
                        {
                            parent.Nodes.Add(reader.Value);
                        }
                    }
                }
            }
            finally
            {
                treeView.EndUpdate();
                reader.Close();
            }
        }

        public void LoadXmlFileInTreeView(TreeView treeView, string fileName)
        {
            XmlTextReader reader = null;
            try
            {
                treeView.BeginUpdate();
                reader = new XmlTextReader(fileName);
                TreeNode parent = new TreeNode(fileName);
                treeView.Nodes.Add(parent);
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        bool isEmptyElement = reader.IsEmptyElement;
                        StringBuilder builder = new StringBuilder();
                        builder.Append(reader.Name);
                        int attributeCount = reader.AttributeCount;
                        if (attributeCount > 0)
                        {
                            builder.Append(" ( ");
                            for (int i = 0; i < attributeCount; i++)
                            {
                                if (i != 0)
                                {
                                    builder.Append(", ");
                                }
                                reader.MoveToAttribute(i);
                                builder.Append(reader.Name);
                                builder.Append(" = ");
                                builder.Append(reader.Value);
                            }
                            builder.Append(" ) ");
                        }
                        if (isEmptyElement)
                        {
                            parent.Nodes.Add(builder.ToString());
                        }
                        else
                        {
                            parent = parent.Nodes.Add(builder.ToString());
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        parent = parent.Parent;
                    }
                    else if (reader.NodeType != XmlNodeType.XmlDeclaration)
                    {
                        if (reader.NodeType == XmlNodeType.None)
                        {
                            return;
                        }
                        if (reader.NodeType == XmlNodeType.Text)
                        {
                            parent.Nodes.Add(reader.Value);
                        }
                    }
                }
            }
            finally
            {
                treeView.EndUpdate();
                reader.Close();
            }
        }

        private void SaveNodes(TreeNodeCollection nodesCollection, XmlTextWriter textWriter)
        {
            for (int i = 0; i < nodesCollection.Count; i++)
            {
                TreeNode node = nodesCollection[i];
                textWriter.WriteStartElement("node");
                textWriter.WriteAttributeString("text", node.Text);
                textWriter.WriteAttributeString("imageindex", node.ImageIndex.ToString());
                if (node.Tag != null)
                {
                    textWriter.WriteAttributeString("tag", node.Tag.ToString());
                }
                if (node.Nodes.Count > 0)
                {
                    this.SaveNodes(node.Nodes, textWriter);
                }
                textWriter.Formatting = Formatting.Indented;
                textWriter.WriteEndElement();
            }
        }

        public void SerializeTreeView(TreeView treeView)
        {
            XmlTextWriter textWriter = new XmlTextWriter(this.XmlSaveFullPath, Encoding.UTF8);
            textWriter.WriteStartDocument();
            textWriter.WriteStartElement("TreeView");
            this.SaveNodes(treeView.Nodes, textWriter);
            textWriter.WriteEndElement();
            textWriter.Close();
        }

        private void SetAttributeValue(TreeNode node, string propertyName, string value)
        {
            if (propertyName == "text")
            {
                node.Text = value;
            }
            else if (propertyName == "imageindex")
            {
                node.ImageIndex = int.Parse(value);
            }
            else if (propertyName == "tag")
            {
                node.Tag = value;
            }
        }
    }
}


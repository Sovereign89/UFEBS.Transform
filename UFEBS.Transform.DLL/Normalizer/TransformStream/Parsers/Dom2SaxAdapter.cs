using System;
using System.IO;
using System.Xml;
using Org.Xml.Sax;

namespace Normalizer.TransformStream.Parsers
{
    internal class Dom2SaxAdapter : IXmlReader
    {
        private const string defaultAttributeType = "CDATA";
        private const string xmlnsNamespace = "http://www.w3.org/2000/xmlns/";
        private XmlNode node;
        private IErrorHandler errorHandler;
        private IEntityResolver entityResolver;
        private IDtdHandler dtdHandler;
        private IContentHandler contentHandler;
        private int level;

        public IContentHandler ContentHandler
        {
            get => this.contentHandler;
            set
            {
                this.contentHandler = value != null ? value : throw new ArgumentNullException(nameof(value), "Null content handler");
            }
        }

        public IErrorHandler ErrorHandler
        {
            get => this.errorHandler;
            set
            {
                this.errorHandler = value != null ? value : throw new ArgumentNullException(nameof(value), "Null error handler");
            }
        }

        public IDtdHandler DtdHandler
        {
            get => this.dtdHandler;
            set
            {
                this.dtdHandler = value != null ? value : throw new ArgumentNullException(nameof(value), "Null DTD handler");
            }
        }

        public IEntityResolver EntityResolver
        {
            get => this.entityResolver;
            set
            {
                this.entityResolver = value != null ? value : throw new ArgumentNullException(nameof(value), "Null entity resolver handler");
            }
        }

        internal Dom2SaxAdapter(XmlNode node) => this.node = node;

        public void Parse(string data) => this.Parse();

        public void Parse(Stream data) => this.Parse();

        public void Parse()
        {
            this.level = 0;
            this.ProcessNode(this.node);
        }

        public bool GetFeature(string featureName) => false;

        public void SetFeature(string featureName, bool value)
        {
        }

        public object GetProperty(string propertyName) => (object)null;

        public void SetProperty(string propertyName, object value)
        {
        }

        private void ProcessNode(XmlNode node)
        {
            if (node.NodeType == XmlNodeType.Attribute)
                return;
            if (node.NodeType == XmlNodeType.CDATA)
            {
                this.ProcessText(node as XmlCharacterData);
            }
            else
            {
                if (node.NodeType == XmlNodeType.Comment)
                    return;
                if (node.NodeType == XmlNodeType.Document)
                {
                    this.ProcessDocument(node as XmlDocument);
                }
                else
                {
                    if (node.NodeType == XmlNodeType.DocumentFragment || node.NodeType == XmlNodeType.DocumentType)
                        return;
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        ++this.level;
                        this.ProcessElement(node as XmlElement);
                        --this.level;
                    }
                    else
                    {
                        if (node.NodeType == XmlNodeType.EndElement || node.NodeType == XmlNodeType.EndEntity || node.NodeType == XmlNodeType.Entity)
                            return;
                        if (node.NodeType == XmlNodeType.EntityReference)
                        {
                            this.ProcessEntityReference(node as XmlEntityReference);
                        }
                        else
                        {
                            if (node.NodeType == XmlNodeType.None || node.NodeType == XmlNodeType.Notation)
                                return;
                            if (node.NodeType == XmlNodeType.ProcessingInstruction)
                                this.ProcessProcessingInstruction(node as XmlProcessingInstruction);
                            else if (node.NodeType == XmlNodeType.SignificantWhitespace)
                                this.ProcessText(node as XmlCharacterData);
                            else if (node.NodeType == XmlNodeType.Text)
                                this.ProcessText(node as XmlCharacterData);
                            else if (node.NodeType == XmlNodeType.Whitespace)
                            {
                                this.ProcessText(node as XmlCharacterData);
                            }
                            else
                            {
                                int nodeType = (int)node.NodeType;
                            }
                        }
                    }
                }
            }
        }

        private void ProcessDocument(XmlDocument document)
        {
            if (this.ContentHandler != null)
                this.ContentHandler.StartDocument();
            this.ProcessChildNodes(document.ChildNodes);
            if (this.ContentHandler == null)
                return;
            this.ContentHandler.EndDocument();
        }

        private void ProcessElement(XmlElement element)
        {
            this.SendStartPrefixMappingEvents(element);
            if (this.ContentHandler != null)
            {
                AttributeList attributes = this.GetAttributes(element);
                this.ContentHandler.StartElement(element.NamespaceURI, element.LocalName, element.Name, (IAttributeList)attributes);
            }
            this.ProcessChildNodes(element.ChildNodes);
            if (this.ContentHandler != null)
                this.ContentHandler.EndElement(element.NamespaceURI, element.LocalName, element.Name);
            this.SendEndPrefixMappingEvents(element);
        }

        private void ProcessText(XmlCharacterData text)
        {
            if (this.level == 0)
                return;
            char[] charArray = text.Data.ToCharArray();
            if (this.ContentHandler == null)
                return;
            this.ContentHandler.Characters(charArray, 0, charArray.Length);
        }

        private void ProcessProcessingInstruction(XmlProcessingInstruction pi)
        {
            if (this.ContentHandler == null)
                return;
            this.ContentHandler.ProcessingInstruction(pi.Target, pi.Data);
        }

        private void ProcessEntityReference(XmlEntityReference reference)
        {
            char[] charArray = reference.InnerText.ToCharArray();
            if (this.ContentHandler == null)
                return;
            this.ContentHandler.Characters(charArray, 0, charArray.Length);
        }

        private void ProcessChildNodes(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
                this.ProcessNode(node);
        }

        private void SendStartPrefixMappingEvents(XmlElement element)
        {
            foreach (XmlAttribute attribute in (XmlNamedNodeMap)element.Attributes)
            {
                if (attribute.LocalName == "xmlns")
                    this.StartPrefixMapping("", attribute.Value);
                else if (attribute.Prefix.StartsWith("xmlns"))
                    this.StartPrefixMapping(attribute.LocalName, attribute.Value);
            }
        }

        private void StartPrefixMapping(string prefix, string uri)
        {
            if (this.ContentHandler == null || prefix == null)
                return;
            this.ContentHandler.StartPrefixMapping(prefix, uri);
        }

        private void SendEndPrefixMappingEvents(XmlElement element)
        {
            foreach (XmlAttribute attribute in (XmlNamedNodeMap)element.Attributes)
            {
                if (attribute.LocalName == "xmlns")
                    this.EndPrefixMapping("");
                else if (attribute.Prefix.StartsWith("xmlns"))
                    this.EndPrefixMapping(attribute.LocalName);
            }
        }

        private void EndPrefixMapping(string prefix)
        {
            if (this.ContentHandler == null)
                return;
            this.ContentHandler.EndPrefixMapping(prefix);
        }

        private AttributeList GetAttributes(XmlElement element)
        {
            AttributeList attributes = new AttributeList();
            foreach (XmlAttribute attribute in (XmlNamedNodeMap)element.Attributes)
            {
                if (!this.IsNamespaceDeclaration(attribute.NamespaceURI, attribute.Name))
                    attributes.AddAttribute(attribute.NamespaceURI, attribute.LocalName, attribute.Name, "CDATA", attribute.Value);
            }
            return attributes;
        }

        private bool IsNamespaceDeclaration(string uri, string qName)
        {
            return uri == "http://www.w3.org/2000/xmlns/" && qName.StartsWith("xmlns");
        }
    }
}

using Normalizer.TransformStream.Utils;
using Org.Xml.Sax;
using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace Normalizer.TransformStream.Parsers
{
    internal class Sax2SaxAdapter : IXmlReader
  {
    private const string defaultAttributeType = "CDATA";
    private const string xmlnsNamespace = "http://www.w3.org/2000/xmlns/";
    private IErrorHandler errorHandler;
    private IEntityResolver entityResolver;
    private IDtdHandler dtdHandler;
    private IContentHandler contentHandler;
    private XmlReader reader;
    private Stack prefixesStack;
    private int level;

    public IContentHandler ContentHandler
    {
      get => this.contentHandler;
      set
      {
        this.contentHandler = value != null ? value : throw new ArgumentNullException(nameof (value), "Null content handler");
      }
    }

    public IErrorHandler ErrorHandler
    {
      get => this.errorHandler;
      set
      {
        this.errorHandler = value != null ? value : throw new ArgumentNullException(nameof (value), "Null error handler");
      }
    }

    public IDtdHandler DtdHandler
    {
      get => this.dtdHandler;
      set
      {
        this.dtdHandler = value != null ? value : throw new ArgumentNullException(nameof (value), "Null DTD handler");
      }
    }

    public IEntityResolver EntityResolver
    {
      get => this.entityResolver;
      set
      {
        this.entityResolver = value != null ? value : throw new ArgumentNullException(nameof (value), "Null entity resolver handler");
      }
    }

    internal Sax2SaxAdapter(XmlReader reader)
    {
      this.reader = reader;
      this.prefixesStack = new Stack();
    }

    public void Parse(string data) => this.DoParse();

    public void Parse(Stream data) => this.DoParse();

    public void Parse() => this.DoParse();

    public bool GetFeature(string featureName) => false;

    public void SetFeature(string featureName, bool value)
    {
    }

    public object GetProperty(string propertyName) => (object) null;

    public void SetProperty(string propertyName, object value)
    {
    }

    private void DoParse()
    {
      this.level = 0;
      if (this.ContentHandler != null)
        this.ContentHandler.StartDocument();
      while (this.reader.Read())
      {
        if (this.reader.NodeType != XmlNodeType.Attribute)
        {
          if (this.reader.NodeType == XmlNodeType.CDATA)
            this.ProcessText(this.reader.Value);
          else if (this.reader.NodeType != XmlNodeType.Comment && this.reader.NodeType != XmlNodeType.Document && this.reader.NodeType != XmlNodeType.DocumentFragment && this.reader.NodeType != XmlNodeType.DocumentType)
          {
            if (this.reader.NodeType == XmlNodeType.Element)
            {
              ++this.level;
              string namespaceUri1 = this.reader.NamespaceURI;
              string localName1 = this.reader.LocalName;
              string name1 = this.reader.Name;
              AttributeList attributes = new AttributeList();
              PrefixMappingPairList pairList = new PrefixMappingPairList();
              if (this.reader.HasAttributes)
              {
                for (int i = 0; i < this.reader.AttributeCount; ++i)
                {
                  this.reader.MoveToAttribute(i);
                  string namespaceUri2 = this.reader.NamespaceURI;
                  string localName2 = this.reader.LocalName;
                  string name2 = this.reader.Name;
                  string type = "CDATA";
                  string uri = this.reader.Value;
                  if (!this.IsNamespaceDeclaration(namespaceUri2, name2))
                    attributes.AddAttribute(namespaceUri2, localName2, name2, type, uri);
                  else if (localName2 == "xmlns")
                    pairList.Add(new PrefixMappingPair(uri, ""));
                  else
                    pairList.Add(new PrefixMappingPair(uri, localName2));
                }
              }
              this.reader.MoveToElement();
              this.ProcessElement(namespaceUri1, localName1, name1, attributes, pairList);
              if (this.reader.IsEmptyElement)
              {
                this.ProcessEndElement(this.reader.NamespaceURI, this.reader.LocalName, this.reader.Name);
                --this.level;
              }
            }
            else if (this.reader.NodeType == XmlNodeType.EndElement)
            {
              this.ProcessEndElement(this.reader.NamespaceURI, this.reader.LocalName, this.reader.Name);
              --this.level;
            }
            else if (this.reader.NodeType != XmlNodeType.EndEntity && this.reader.NodeType != XmlNodeType.Entity)
            {
              if (this.reader.NodeType == XmlNodeType.EntityReference)
                this.reader.ResolveEntity();
              else if (this.reader.NodeType != XmlNodeType.None && this.reader.NodeType != XmlNodeType.Notation)
              {
                if (this.reader.NodeType == XmlNodeType.ProcessingInstruction)
                  this.ProcessProcessingInstruction(this.reader.Name, this.reader.Value);
                else if (this.reader.NodeType == XmlNodeType.SignificantWhitespace)
                  this.ProcessText(this.reader.Value);
                else if (this.reader.NodeType == XmlNodeType.Text)
                  this.ProcessText(this.reader.Value);
                else if (this.reader.NodeType == XmlNodeType.Whitespace)
                {
                  this.ProcessText(this.reader.Value);
                }
                else
                {
                  int nodeType = (int) this.reader.NodeType;
                }
              }
            }
          }
        }
      }
      if (this.ContentHandler == null)
        return;
      this.ContentHandler.EndDocument();
    }

    private void ProcessText(string text)
    {
      if (this.level == 0)
        return;
      char[] charArray = text.ToCharArray();
      if (this.ContentHandler == null)
        return;
      this.ContentHandler.Characters(charArray, 0, charArray.Length);
    }

    private void ProcessProcessingInstruction(string target, string data)
    {
      if (this.ContentHandler == null)
        return;
      this.ContentHandler.ProcessingInstruction(target, data);
    }

    private void ProcessElement(
      string uri,
      string localName,
      string qName,
      AttributeList attributes,
      PrefixMappingPairList pairList)
    {
      this.SendStartPrefixMappingEvents(pairList);
      if (this.ContentHandler != null)
        this.ContentHandler.StartElement(uri, localName, qName, (IAttributeList) attributes);
      this.prefixesStack.Push((object) pairList);
    }

    private void ProcessEndElement(string uri, string localName, string qName)
    {
      if (this.ContentHandler != null)
        this.ContentHandler.EndElement(uri, localName, qName);
      this.SendEndPrefixMappingEvents();
    }

    private void SendStartPrefixMappingEvents(PrefixMappingPairList mappedPrefixes)
    {
      for (int index = 0; index < mappedPrefixes.Count; ++index)
        this.StartPrefixMapping(mappedPrefixes.GetPrefix(index), mappedPrefixes.GetUri(index));
    }

    private void SendEndPrefixMappingEvents()
    {
      PrefixMappingPairList prefixMappingPairList = (PrefixMappingPairList) this.prefixesStack.Pop();
      for (int index = 0; index < prefixMappingPairList.Count; ++index)
        this.EndPrefixMapping(prefixMappingPairList.GetPrefix(index));
    }

    private void StartPrefixMapping(string prefix, string uri)
    {
      if (this.ContentHandler == null || prefix == null)
        return;
      this.ContentHandler.StartPrefixMapping(prefix, uri);
    }

    private void EndPrefixMapping(string prefix)
    {
      if (this.ContentHandler == null || prefix == null)
        return;
      this.ContentHandler.EndPrefixMapping(prefix);
    }

    private bool IsNamespaceDeclaration(string uri, string qName)
    {
      return uri == "http://www.w3.org/2000/xmlns/" && qName.StartsWith("xmlns");
    }
  }
}

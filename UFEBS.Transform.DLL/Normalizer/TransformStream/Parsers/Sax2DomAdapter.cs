using Normalizer.TransformStream.Utils;
using Org.Xml.Sax;
using System.Collections;
using System.Xml;

namespace Normalizer.TransformStream.Parsers
{
  public class Sax2DomAdapter : XmlFilterSkeleton
  {
    private const string xmlnsNamespace = "http://www.w3.org/2000/xmlns/";
    private XmlDocument doc;
    private XmlNode currentNode;
    private ArrayList namespaces;

    public XmlDocument Document => this.doc;

    public Sax2DomAdapter(IXmlReader parent)
      : base(parent)
    {
      this.namespaces = new ArrayList();
    }

    public override void StartDocument()
    {
      this.doc = new XmlDocument();
      this.currentNode = (XmlNode) this.doc;
      base.StartDocument();
    }

    public override void EndDocument() => base.EndDocument();

    public override void Characters(char[] chars, int start, int length)
    {
      this.currentNode.AppendChild((XmlNode) this.doc.CreateTextNode(new string(chars, start, length)));
      base.Characters(chars, start, length);
    }

    public override void StartElement(
      string namespaceURI,
      string localName,
      string qName,
      IAttributeList attributes)
    {
      XmlElement element = this.doc.CreateElement(qName, namespaceURI);
      foreach (object obj in this.namespaces)
      {
        PrefixMappingPair prefixMappingPair = obj as PrefixMappingPair;
        XmlAttribute attribute = this.doc.CreateAttribute("xmlns:" + prefixMappingPair.Prefix, "http://www.w3.org/2000/xmlns/");
        attribute.Value = prefixMappingPair.Uri;
        element.Attributes.Append(attribute);
      }
      this.namespaces.Clear();
      for (int index = 0; index < attributes.Length; ++index)
      {
        XmlAttribute attribute = this.doc.CreateAttribute(attributes.GetQName(index), attributes.GetUri(index));
        attribute.Value = attributes.GetValue(index);
        element.Attributes.Append(attribute);
      }
      this.currentNode.AppendChild((XmlNode) element);
      this.currentNode = (XmlNode) element;
      base.StartElement(namespaceURI, localName, qName, attributes);
    }

    public override void EndElement(string namespaceURI, string localName, string qName)
    {
      this.currentNode = this.currentNode.ParentNode;
      base.EndElement(namespaceURI, localName, qName);
    }

    public override void IgnorableWhitespace(char[] chars, int start, int length)
    {
      this.currentNode.AppendChild((XmlNode) this.doc.CreateWhitespace(new string(chars, start, length)));
      base.IgnorableWhitespace(chars, start, length);
    }

    public override void ProcessingInstruction(string target, string data)
    {
      this.currentNode.AppendChild((XmlNode) this.doc.CreateProcessingInstruction(target, data));
      base.ProcessingInstruction(target, data);
    }

    public override void StartPrefixMapping(string prefix, string uri)
    {
      this.namespaces.Add((object) new PrefixMappingPair(uri, prefix));
      base.StartPrefixMapping(prefix, uri);
    }
  }
}

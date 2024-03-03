using Normalizer.TransformStream.Utils;
using Org.Xml.Sax;
using System.IO;
using System.Text;

namespace Normalizer.TransformStream
{
  internal class StreamPrinter : XmlFilterSkeleton
  {
    private TextWriter writer;
    private PrefixMappingPairList mappingList;

    internal StreamPrinter(IXmlReader parent, TextWriter writer)
      : base(parent)
    {
      this.writer = writer;
      this.mappingList = new PrefixMappingPairList();
    }

    public override void StartElement(
      string namespaceURI,
      string localName,
      string qName,
      IAttributeList attributes)
    {
      this.Write(string.Format("<{0}", (object) qName));
      for (int index = 0; index < this.mappingList.Count; ++index)
        this.Write(string.Format(" xmlns:{0}=\"{1}\"", (object) this.mappingList.GetPrefix(index), (object) this.mappingList.GetUri(index)));
      for (int index = 0; index < attributes.Length; ++index)
        this.Write(string.Format(" {0}=\"{1}\"", (object) attributes.GetQName(index), (object) this.TransformAttributeValue(attributes.GetValue(index))));
      this.Write(">");
      this.mappingList.Clear();
      base.StartElement(namespaceURI, localName, qName, attributes);
    }

    public override void EndElement(string namespaceURI, string localName, string qName)
    {
      this.Write(string.Format("</{0}>", (object) qName));
      base.EndElement(namespaceURI, localName, qName);
    }

    public override void Characters(char[] chars, int start, int length)
    {
      for (int index = start; index < start + length; ++index)
      {
        char ch = chars[index];
        switch (ch)
        {
          case '\t':
            this.Write("&#x9;");
            break;
          case '\n':
            this.Write("&#xA;");
            break;
          case '\r':
            this.Write("&#xD;");
            break;
          case '&':
            this.Write("&amp;");
            break;
          case '<':
            this.Write("&lt;");
            break;
          default:
            this.Write(ch);
            break;
        }
      }
      base.Characters(chars, start, length);
    }

    public override void ProcessingInstruction(string target, string data)
    {
      if (data != null && data.Length > 0)
        this.Write(string.Format("<?{0} {1}?>", (object) target, (object) data));
      else
        this.Write(string.Format("<?{0}?>", (object) target));
      base.ProcessingInstruction(target, data);
    }

    public override void StartPrefixMapping(string prefix, string uri)
    {
      this.mappingList.Add(new PrefixMappingPair(uri, prefix));
      base.StartPrefixMapping(prefix, uri);
    }

    private string TransformAttributeValue(string attributeValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < attributeValue.Length; ++index)
      {
        char ch = attributeValue[index];
        switch (ch)
        {
          case '\t':
            stringBuilder.Append("&#x9;");
            break;
          case '\n':
            stringBuilder.Append("&#xA;");
            break;
          case '\r':
            stringBuilder.Append("&#xD;");
            break;
          case '"':
            stringBuilder.Append("&quot;");
            break;
          case '&':
            stringBuilder.Append("&amp;");
            break;
          case '<':
            stringBuilder.Append("&lt;");
            break;
          case '>':
            stringBuilder.Append("&gt;");
            break;
          default:
            stringBuilder.Append(ch);
            break;
        }
      }
      return stringBuilder.ToString();
    }

    private void Write(string value) => this.writer.Write(value);

    private void Write(char value) => this.writer.Write(value);
  }
}

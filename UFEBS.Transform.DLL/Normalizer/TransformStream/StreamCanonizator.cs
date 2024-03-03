using Normalizer.TransformStream.Utils;
using Org.Xml.Sax;
using System.Collections;
using System.IO;
using System.Text;

namespace Normalizer.TransformStream
{
  internal class StreamCanonizator : XmlFilterSkeleton
  {
    private const int MAX_ENTITY_LENGTH = 10;
    private Stream output;
    private StreamCanonizatorStack stack;
    private char[] tmpBuffer;
    private int offset;

    internal StreamCanonizator(IXmlReader parent, Stream output)
      : base(parent)
    {
      this.output = output;
      this.stack = new StreamCanonizatorStack();
      this.offset = 0;
      this.tmpBuffer = new char[265];
    }

    private void HardDumpBuffer()
    {
      byte[] bytes = Encoding.UTF8.GetBytes(this.tmpBuffer, 0, this.offset);
      this.output.Write(bytes, 0, bytes.Length);
      this.offset = 0;
    }

    private void Print(char ch)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(new char[1]
      {
        ch
      });
      this.output.Write(bytes, 0, bytes.Length);
    }

    private void Print(string s)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(s);
      this.output.Write(bytes, 0, bytes.Length);
    }

    private void SafeDumpBuffer()
    {
      if (this.offset <= this.tmpBuffer.Length - 10)
        return;
      this.HardDumpBuffer();
    }

    private void Print(int b) => this.Print((char) b);

    private void Add(string str)
    {
      for (int index = 0; index < str.Length; ++index)
        this.Add(str[index]);
    }

    private void Add(char c)
    {
      this.tmpBuffer[this.offset++] = c;
      this.SafeDumpBuffer();
    }

    public override void Characters(char[] chars, int start, int length)
    {
      for (int index = start; index < start + length; ++index)
      {
        char c = chars[index];
        switch (c)
        {
          case '\r':
            this.Add("&#xD;");
            break;
          case '&':
            this.Add("&amp;");
            break;
          case '<':
            this.Add("&lt;");
            break;
          case '>':
            this.Add("&gt;");
            break;
          default:
            this.Add(c);
            break;
        }
        this.SafeDumpBuffer();
      }
      this.HardDumpBuffer();
      base.Characters(chars, start, length);
    }

    public override void EndDocument()
    {
      this.output.Flush();
      this.stack.PopLevel();
      base.EndDocument();
    }

    public override void EndElement(string uri, string localName, string qName)
    {
      this.Add('<');
      this.Add('/');
      for (int index = 0; index < qName.Length; ++index)
      {
        this.Add(qName[index]);
        this.SafeDumpBuffer();
      }
      this.Add('>');
      this.HardDumpBuffer();
      this.stack.PopLevel();
      base.EndElement(uri, localName, qName);
    }

    public override void ProcessingInstruction(string target, string data)
    {
      if (this.stack.IsBeforeRootElementPlace())
        this.Print(10);
      this.Print("<?");
      this.Print(target);
      if (data != string.Empty && data != null)
      {
        this.Print(" ");
        this.Print(data);
      }
      this.Print("?>");
      if (this.stack.IsAfterRootElementPlace())
        this.Print(10);
      base.ProcessingInstruction(target, data);
    }

    public override void StartDocument()
    {
      this.stack.NewLevel();
      this.stack.AddToTopList(new PrefixMappingPair("", ""));
      base.StartDocument();
    }

    public override void StartPrefixMapping(string prefix, string uri)
    {
      this.stack.AddPrefixMappingEventPair(new PrefixMappingPair(uri, prefix));
      base.StartPrefixMapping(prefix, uri);
    }

    public override void StartElement(
      string uri,
      string localName,
      string qName,
      IAttributeList attributes)
    {
      this.stack.StartElementEvent();
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < attributes.Length; ++index)
        arrayList.Add((object) index);
      CanonicalizeAttributeComparer attributeComparer = new CanonicalizeAttributeComparer(attributes);
      arrayList.Sort((IComparer) attributeComparer);
      AttributeList attributeList = new AttributeList();
      for (int index1 = 0; index1 < attributes.Length; ++index1)
      {
        int index2 = (int) arrayList[index1];
        attributeList.AddAttribute(attributes.GetUri(index2), attributes.GetLocalName(index2), attributes.GetQName(index2), attributes.GetType(index2), attributes.GetValue(index2));
      }
      this.Print("<");
      this.Print(qName);
      for (int topListIndex = 0; topListIndex < this.stack.Top().Count; ++topListIndex)
      {
        this.Print(" ");
        this.Print("xmlns");
        string prefix = this.stack.GetPrefix(topListIndex);
        if (prefix != "" && prefix != null)
          this.Print(":" + prefix);
        this.Print("=\"");
        this.Print(this.stack.GetUri(topListIndex));
        this.Print("\"");
      }
      for (int index = 0; index < attributeList.Length; ++index)
      {
        this.Print(" ");
        this.Print(attributeList.GetQName(index));
        this.Print("=");
        this.Print("\"");
        this.PrintAttributeValue(attributeList.GetValue(index));
        this.Print("\"");
      }
      this.Print(">");
      base.StartElement(uri, localName, qName, attributes);
    }

    private void PrintAttributeValue(string v)
    {
      for (int index = 0; index < v.Length; ++index)
      {
        char ch = v[index];
        switch (ch)
        {
          case '\t':
            this.Print("&#x9;");
            break;
          case '\n':
            this.Print("&#xA;");
            break;
          case '\r':
            this.Print("&#xD;");
            break;
          case '"':
            this.Print("&quot;");
            break;
          case '&':
            this.Print("&amp;");
            break;
          case '<':
            this.Print("&lt;");
            break;
          default:
            this.Print(ch);
            break;
        }
      }
    }
  }
}

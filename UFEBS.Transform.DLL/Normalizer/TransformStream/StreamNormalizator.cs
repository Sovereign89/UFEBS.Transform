using Normalizer.TransformStream.Utils;
using Org.Xml.Sax;
using System.ComponentModel;
using System.Text;

namespace Normalizer.TransformStream
{
  internal class StreamNormalizator : XmlFilterSkeleton
  {
    private ElementViewStack stack;
    private bool dumpBuffer;
    private StreamNormalizator.ElementsHistory history;
    private StringBuilder buffer;

    internal StreamNormalizator(IXmlReader parent)
      : base(parent)
    {
      this.stack = new ElementViewStack();
      this.dumpBuffer = false;
      this.history = new StreamNormalizator.ElementsHistory();
      this.buffer = new StringBuilder();
    }

    private bool CanDumpBuffer(char[] buffer, int start, int length)
    {
      for (int index = start; index < start + length; ++index)
      {
        switch (buffer[index])
        {
          case '\t':
          case '\n':
          case '\r':
          case ' ':
            continue;
          default:
            return true;
        }
      }
      return false;
    }

    private void FlushBuffer()
    {
      if (!this.dumpBuffer)
        return;
      char[] charArray = this.buffer.ToString().ToCharArray();
      base.Characters(charArray, 0, charArray.Length);
      this.buffer.Remove(0, this.buffer.Length);
    }

    private void SendPrefixMappingEvents(IElementView view)
    {
      for (int index = 0; index < view.GetNamespaceCount(); ++index)
        base.StartPrefixMapping(view.GetNamespacePrefix(index), view.GetNamespaceUri(index));
    }

    private void SendStartElementEvent(string uri, IElementView view)
    {
      string localName = view.LocalName;
      string qname = view.QName;
      IAttributeList attributes = view.Attributes;
      base.StartElement(uri, localName, qname, attributes);
    }

    public override void Characters(char[] ch, int start, int length)
    {
      if (this.dumpBuffer || this.CanDumpBuffer(ch, start, length))
        this.dumpBuffer = true;
      if (this.dumpBuffer)
      {
        this.FlushBuffer();
        base.Characters(ch, start, length);
      }
      else
        this.buffer.Append(ch, start, length);
    }

    public override void EndElement(string uri, string localName, string qName)
    {
      IElementView elementView = this.stack.Top();
      this.history.EndElement(ref this.dumpBuffer);
      this.FlushBuffer();
      string localName1 = elementView.LocalName;
      string qname = elementView.QName;
      base.EndElement(uri, localName1, qname);
      for (int index = 0; index < elementView.GetNamespaceCount(); ++index)
        base.EndPrefixMapping(elementView.GetNamespacePrefix(index));
      this.stack.Pop();
      this.dumpBuffer = false;
      this.buffer.Remove(0, this.buffer.Length);
    }

    public override void EndPrefixMapping(string prefix)
    {
    }

    public override void StartElement(
      string uri,
      string localName,
      string qName,
      IAttributeList attributes)
    {
      this.stack.Push((IElementView) new NormalizedElementView());
      IElementView view = this.stack.Top();
      this.history.StartElement();
      view.SetElement(uri, localName, qName, attributes);
      this.SendPrefixMappingEvents(view);
      this.SendStartElementEvent(uri, view);
      this.dumpBuffer = false;
      this.buffer.Remove(0, this.buffer.Length);
    }

    public override void StartPrefixMapping(string prefix, string uri)
    {
    }

    public override void ProcessingInstruction(string target, string data)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class ElementsHistory
    {
      private bool lastIsStartElement;

      internal void StartElement() => this.lastIsStartElement = true;

      internal void EndElement(ref bool dumpBuffer)
      {
        if (this.lastIsStartElement)
          dumpBuffer = true;
        this.lastIsStartElement = false;
      }
    }
  }
}

using System.Xml;

namespace CBR.UfebsStream.Headers
{
  public class UnknownHeader : Header
  {
    private string xmlString;
    private string name;
    private string namespaceUri;

    public string XmlString
    {
      get => this.xmlString;
      set => this.xmlString = value;
    }

    public override CBR.UfebsStream.QName Name => new CBR.UfebsStream.QName(this.name, this.namespaceUri);

    public UnknownHeader() => this.xmlString = (string) null;

    public UnknownHeader(string xmlString) => this.xmlString = xmlString;

    public override void Validate()
    {
    }

    public override object Clone() => (object) new UnknownHeader(this.xmlString);

    public override void SerializeTo(XmlWriter writer)
    {
      if (this.xmlString == null)
        return;
      writer.WriteRaw(this.xmlString);
    }

    public override void LoadFrom(XmlReader reader)
    {
      this.name = reader.LocalName;
      this.namespaceUri = reader.NamespaceURI;
      this.xmlString = reader.ReadOuterXml();
    }
  }
}

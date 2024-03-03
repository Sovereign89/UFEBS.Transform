using CBR.UfebsStream.Headers;
using System.Collections.Generic;
using System.Xml;

namespace CBR.UfebsStream.Producers
{
  public class HeadersProducer : IXmlContentProducer
  {
    private List<Header> headers;

    public HeadersProducer() => this.headers = new List<Header>();

    public HeadersProducer(params Header[] headers)
    {
      this.headers = new List<Header>();
      for (int index = 0; index < headers.Length; ++index)
        this.headers.Add(headers[index]);
    }

    public HeadersProducer(IEnumerable<Header> headers)
    {
      this.headers = new List<Header>();
      this.headers.AddRange(headers);
    }

    public void Add(Header header) => this.headers.Add(header);

    public void WriteTo(XmlWriter target)
    {
      if (this.headers.Count <= 0)
        return;
      target.WriteStartElement("soapenv", "Header", "http://www.w3.org/2003/05/soap-envelope");
      foreach (Header header in this.headers)
        header.SerializeTo(target);
      target.WriteEndElement();
    }
  }
}

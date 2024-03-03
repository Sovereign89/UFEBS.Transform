using System;
using System.Xml;

namespace CBR.UfebsStream.Producers
{
  public class MacValueProducer : IXmlContentProducer
  {
    private byte[] macValue;

    public MacValueProducer(byte[] macValue) => this.macValue = macValue;

    public MacValueProducer(string macValueBase64)
    {
      this.macValue = Convert.FromBase64String(macValueBase64);
    }

    public void WriteTo(XmlWriter target)
    {
      target.WriteElementString("dsig", "MACValue", "urn:cbr-ru:dsig:v1.1", Convert.ToBase64String(this.macValue));
    }
  }
}

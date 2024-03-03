using System.Xml;

namespace CBR.UfebsStream.Producers
{
  public class SoapEnvelopeProducer : IXmlContentProducer
  {
    private IXmlContentProducer headerCreator;
    private IXmlContentProducer bodyCreator;

    public SoapEnvelopeProducer(IXmlContentProducer headerCreator, IXmlContentProducer bodyCreator)
    {
      this.headerCreator = headerCreator;
      this.bodyCreator = bodyCreator;
    }

    public void WriteTo(XmlWriter target)
    {
      target.WriteStartElement("soapenv", "Envelope", "http://www.w3.org/2003/05/soap-envelope");
      if (this.headerCreator != null)
        this.headerCreator.WriteTo(target);
      target.WriteStartElement("soapenv", "Body", "http://www.w3.org/2003/05/soap-envelope");
      if (this.bodyCreator != null)
        this.bodyCreator.WriteTo(target);
      target.WriteEndElement();
      target.WriteEndElement();
    }
  }
}

using System.IO;
using System.Xml;

namespace CBR.UfebsStream.Producers
{
  public class SigEnvelopeProducer : IXmlContentProducer
  {
    private IXmlContentProducer sigContainerCreator;
    private IBinaryContentProducer sigObjectCreator;

    public SigEnvelopeProducer(
      IXmlContentProducer sigContainerCreator,
      IBinaryContentProducer sigObjectCreator)
    {
      this.sigContainerCreator = sigContainerCreator;
      this.sigObjectCreator = sigObjectCreator;
    }

    public void WriteTo(XmlWriter target)
    {
      target.WriteStartElement("sen", "SigEnvelope", "urn:cbr-ru:dsig:env:v1.1");
      target.WriteStartElement("sen", "SigContainer", "urn:cbr-ru:dsig:env:v1.1");
      this.sigContainerCreator.WriteTo(target);
      target.WriteEndElement();
      target.WriteStartElement("sen", "Object", "urn:cbr-ru:dsig:env:v1.1");
      this.sigObjectCreator.WriteTo((Stream) new EncodeBase64XmlStream(target));
      target.WriteEndElement();
      target.WriteEndElement();
    }
  }
}

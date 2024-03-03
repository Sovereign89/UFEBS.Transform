using System.Xml;

namespace CBR.UfebsStream.Producers
{
  public interface IXmlContentProducer
  {
    void WriteTo(XmlWriter target);
  }
}

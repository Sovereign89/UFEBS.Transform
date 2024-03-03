using System.IO;
using System.Xml;

namespace CBR.UfebsStream.Producers
{
  public class EmptyProducer : IXmlContentProducer, ITextContentProducer, IBinaryContentProducer
  {
    public void WriteTo(XmlWriter target)
    {
    }

    public void WriteTo(TextWriter target)
    {
    }

    public void WriteTo(Stream target)
    {
    }
  }
}

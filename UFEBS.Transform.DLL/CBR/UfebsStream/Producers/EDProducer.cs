using CBR.UfebsStream.ED.Serialization;
using System.IO;
using System.Xml;

namespace CBR.UfebsStream.Producers
{
  internal class EDProducer : IBinaryContentProducer, ITextContentProducer, IXmlContentProducer
  {
    private object document;
    private EDSerializerBase serializer;

    public EDProducer(object document, EDSerializerBase serializer)
    {
      this.document = document;
      this.serializer = serializer;
    }

    public void WriteTo(Stream target) => this.serializer.Serialize(target, this.document);

    public void WriteTo(TextWriter target) => this.serializer.Serialize(target, this.document);

    public void WriteTo(XmlWriter target) => this.serializer.Serialize(target, this.document);
  }
}

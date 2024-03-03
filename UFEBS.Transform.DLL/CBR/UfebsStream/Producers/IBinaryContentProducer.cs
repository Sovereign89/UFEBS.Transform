using System.IO;

namespace CBR.UfebsStream.Producers
{
  public interface IBinaryContentProducer
  {
    void WriteTo(Stream target);
  }
}

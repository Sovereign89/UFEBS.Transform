using System.IO;

namespace CBR.UfebsStream.Producers
{
  public interface ITextContentProducer
  {
    void WriteTo(TextWriter target);
  }
}

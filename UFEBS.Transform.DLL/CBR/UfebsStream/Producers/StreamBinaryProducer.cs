using System.IO;
using System.IO.Compression;

namespace CBR.UfebsStream.Producers
{
  public class StreamBinaryProducer : IBinaryContentProducer
  {
    private Stream source;
    private CBR.UfebsStream.CompressionMethod compressionMethod;

    public StreamBinaryProducer(Stream source)
    {
      this.source = source;
      this.compressionMethod = CBR.UfebsStream.CompressionMethod.None;
    }

    public StreamBinaryProducer(Stream source, CBR.UfebsStream.CompressionMethod compressionMethod)
    {
      this.source = source;
      this.compressionMethod = compressionMethod;
    }

    public void WriteTo(Stream target)
    {
      Stream stream = CompressionUtils.WrapStream(target, this.compressionMethod, CompressionMode.Compress, true);
      byte[] buffer = new byte[4096];
      int count;
      while ((count = this.source.Read(buffer, 0, buffer.Length)) > 0)
        stream.Write(buffer, 0, count);
      stream.Flush();
      if (this.compressionMethod == CBR.UfebsStream.CompressionMethod.None)
        return;
      stream.Close();
    }
  }
}

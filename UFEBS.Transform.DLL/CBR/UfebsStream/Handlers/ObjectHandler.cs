using System.IO;
using System.IO.Compression;
using System.Xml;

namespace CBR.UfebsStream.Handlers
{
  public class ObjectHandler : IKAHandler
  {
    private CBR.UfebsStream.CompressionMethod compressionMethod;

    public ObjectHandler() => this.compressionMethod = CBR.UfebsStream.CompressionMethod.None;

    public ObjectHandler(CBR.UfebsStream.CompressionMethod compressionMethod)
    {
      this.compressionMethod = compressionMethod;
    }

    public void Handle(XmlReader reader, ContentStorage content)
    {
      Stream stream = CompressionUtils.WrapStream(!reader.CanReadValueChunk ? (Stream) new DecodeBase64XmlStream(reader) : (Stream) new DecodeBase64XmlStreamValueChunk((TextReader) new XmlBase64FieldTextReader(reader)), this.compressionMethod, CompressionMode.Decompress, true);
      byte[] buffer = new byte[4096];
      int count;
      while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
        content.ObjectContent.Write(buffer, 0, count);
      if (this.compressionMethod == CBR.UfebsStream.CompressionMethod.None)
        return;
      stream.Close();
    }
  }
}

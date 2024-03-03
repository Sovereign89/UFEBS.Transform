using System.IO;
using System.IO.Compression;

namespace CBR.UfebsStream
{
  public class CompressionUtils
  {
    public static Stream WrapStream(
      Stream sourceStream,
      CompressionMethod method,
      CompressionMode mode,
      bool leaveOpen)
    {
      if (method == CompressionMethod.None)
        return sourceStream;
      if (method == CompressionMethod.GZip)
        return (Stream) new GZipStream(sourceStream, mode, leaveOpen);
      return method == CompressionMethod.Deflate ? (Stream) new DeflateStream(sourceStream, mode, leaveOpen) : sourceStream;
    }
  }
}

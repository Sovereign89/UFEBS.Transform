using System;
using System.IO;
using System.Xml;

namespace CBR.UfebsStream
{
  public class DecodeBase64XmlStream : Stream
  {
    private XmlReader reader;

    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    public override long Length
    {
      get => throw new NotSupportedException("The method or operation is not supported.");
    }

    public override long Position
    {
      get => throw new NotSupportedException("The method or operation is not supported.");
      set => throw new NotSupportedException("The method or operation is not supported.");
    }

    public DecodeBase64XmlStream(XmlReader reader) => this.reader = reader;

    public override void Flush()
    {
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      return this.reader.ReadElementContentAsBase64(buffer, offset, count);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override void SetLength(long value)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }
  }
}

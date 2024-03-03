using System;
using System.IO;
using System.Text;

namespace CBR.UfebsStream
{
  public class XDFilterStream : Stream
  {
    private Stream underlyingStream;

    public override bool CanRead => this.underlyingStream.CanRead;

    public override bool CanSeek => this.underlyingStream.CanSeek;

    public override bool CanWrite => this.underlyingStream.CanWrite;

    public override long Length => this.underlyingStream.Length;

    public override long Position
    {
      get => this.underlyingStream.Position;
      set => this.underlyingStream.Position = value;
    }

    public XDFilterStream(Stream underlyingStream)
    {
      if (underlyingStream == null)
        throw new ArgumentNullException(nameof (underlyingStream));
      this.underlyingStream = underlyingStream.CanWrite ? underlyingStream : throw new ArgumentException("Нижележащий поток не допускает запись", nameof (underlyingStream));
    }

    public override void Flush() => this.underlyingStream.Flush();

    public override int Read(byte[] buffer, int offset, int count)
    {
      return this.underlyingStream.Read(buffer, offset, count);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      return this.underlyingStream.Seek(offset, origin);
    }

    public override void SetLength(long value) => this.underlyingStream.SetLength(value);

    public override void Write(byte[] buffer, int offset, int count)
    {
      for (int index = offset; index < offset + count; ++index)
      {
        if (buffer[index] == (byte) 13)
        {
          byte[] bytes = Encoding.UTF8.GetBytes("&#xD;");
          this.underlyingStream.Write(bytes, 0, bytes.Length);
        }
        else
          this.underlyingStream.WriteByte(buffer[index]);
      }
    }
  }
}

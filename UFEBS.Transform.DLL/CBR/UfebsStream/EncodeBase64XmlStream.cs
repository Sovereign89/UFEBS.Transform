using System;
using System.IO;
using System.Xml;

namespace CBR.UfebsStream
{
  public class EncodeBase64XmlStream : Stream
  {
    private byte[] tempBuffer;
    private int bytesBuffered;
    private XmlWriter writer;

    public override bool CanRead => false;

    public override bool CanSeek => false;

    public override bool CanWrite => true;

    public override long Length => throw new NotSupportedException();

    public override long Position
    {
      get => throw new NotSupportedException();
      set => throw new NotSupportedException();
    }

    public EncodeBase64XmlStream(XmlWriter writer)
    {
      this.writer = writer;
      this.tempBuffer = new byte[3];
      this.bytesBuffered = 0;
    }

    public override void Flush()
    {
      this.writer.WriteRaw(Convert.ToBase64String(this.tempBuffer, 0, this.bytesBuffered, Base64FormattingOptions.None));
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException();
    }

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    public override void SetLength(long value) => throw new NotSupportedException();

    public override void Write(byte[] buffer, int offset, int count)
    {
      int num1 = count + this.bytesBuffered;
      int length1 = num1 / 3 * 3;
      int num2 = num1 - length1;
      int length2 = length1 - this.bytesBuffered;
      if (length1 != 0)
      {
        byte[] numArray = new byte[length1];
        Array.Copy((Array) this.tempBuffer, 0, (Array) numArray, 0, this.bytesBuffered);
        Array.Copy((Array) buffer, offset, (Array) numArray, this.bytesBuffered, length2);
        this.writer.WriteRaw(Convert.ToBase64String(numArray, 0, numArray.Length, Base64FormattingOptions.None));
        Array.Copy((Array) buffer, offset + length2, (Array) this.tempBuffer, 0, count - length2);
      }
      else
      {
        if (length2 < 0)
          length2 = 0;
        Array.Copy((Array) buffer, offset + length2, (Array) this.tempBuffer, this.bytesBuffered, count - length2);
      }
      this.bytesBuffered = num2;
    }
  }
}

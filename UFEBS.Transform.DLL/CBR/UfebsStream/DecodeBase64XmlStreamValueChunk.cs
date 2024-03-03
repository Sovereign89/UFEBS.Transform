using System;
using System.IO;

namespace CBR.UfebsStream
{
  public class DecodeBase64XmlStreamValueChunk : Stream
  {
    private byte[] buffered;
    private int bufferedLength;
    private int bufferedOffset;
    private TextReader textReader;

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

    public DecodeBase64XmlStreamValueChunk(TextReader reader)
    {
      this.buffered = new byte[3072];
      this.bufferedLength = 0;
      this.bufferedOffset = 0;
      this.textReader = reader;
    }

    public override void Flush()
    {
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      int destinationIndex = offset;
      int length = count;
      while (true)
      {
        do
        {
          if (this.bufferedLength - this.bufferedOffset == 0)
            this.FillBuffer();
          if (this.bufferedLength != 0)
          {
            if (this.bufferedLength - this.bufferedOffset >= length)
              goto label_8;
          }
          else
            goto label_7;
        }
        while (this.bufferedLength - this.bufferedOffset >= length);
        Array.Copy((Array) this.buffered, this.bufferedOffset, (Array) buffer, destinationIndex, this.bufferedLength - this.bufferedOffset);
        destinationIndex += this.bufferedLength - this.bufferedOffset;
        length -= this.bufferedLength - this.bufferedOffset;
        this.bufferedOffset = this.bufferedLength;
      }
label_7:
      return count - length;
label_8:
      Array.Copy((Array) this.buffered, this.bufferedOffset, (Array) buffer, destinationIndex, length);
      this.bufferedOffset += length;
      int num = destinationIndex + length;
      return count;
    }

    private void FillBuffer()
    {
      char[] chArray = new char[4096];
      int length = this.textReader.Read(chArray, 0, chArray.Length);
      if (length != 0)
      {
        byte[] sourceArray;
        try
        {
          sourceArray = Convert.FromBase64CharArray(chArray, 0, length);
        }
        catch (Exception ex)
        {
          throw new FormatException("Неверные данные Base64", ex);
        }
        Array.Copy((Array) sourceArray, 0, (Array) this.buffered, 0, sourceArray.Length);
        this.bufferedLength = sourceArray.Length;
      }
      else
        this.bufferedLength = 0;
      this.bufferedOffset = 0;
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

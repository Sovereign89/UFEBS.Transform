using CBR.UfebsStream.Headers;
using System.Collections.Generic;
using System.IO;

namespace CBR.UfebsStream
{
  public class ContentStorage
  {
    protected Dictionary<QName, Header> headers;
    protected byte[] macValue;
    protected Stream objectContent;

    public byte[] MacValue
    {
      get => this.macValue;
      set => this.macValue = value;
    }

    public Dictionary<QName, Header> Headers => this.headers;

    public Stream ObjectContent => this.objectContent;

    internal ContentStorage()
    {
      this.headers = new Dictionary<QName, Header>();
      this.macValue = (byte[]) null;
      this.objectContent = (Stream) new MemoryStream();
    }

    internal ContentStorage(Stream objectContent)
    {
      this.headers = new Dictionary<QName, Header>();
      this.macValue = (byte[]) null;
      this.objectContent = objectContent;
    }

    public void AddHeader(QName headerName, Header header)
    {
      if (this.headers.ContainsKey(headerName))
        throw new DuplicateHeaderException("Обнаружен повторный заголовок", headerName);
      this.headers.Add(headerName, header);
    }
  }
}

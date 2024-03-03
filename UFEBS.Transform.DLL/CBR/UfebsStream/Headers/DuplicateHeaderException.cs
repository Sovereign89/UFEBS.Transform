using System;

namespace CBR.UfebsStream.Headers
{
  public class DuplicateHeaderException : Exception
  {
    private QName headerName;

    public QName HeaderName => this.headerName;

    public DuplicateHeaderException(string message)
      : base(message)
    {
      this.headerName = new QName("null", (string) null);
    }

    public DuplicateHeaderException(string message, QName headerName)
      : base(message)
    {
      this.headerName = (QName) headerName.Clone();
    }
  }
}

using System;

namespace CBR.UfebsStream.Headers
{
  public class ValidateHeaderException : Exception
  {
    private Header header;
    private string paramName;

    public Header Header => this.header;

    public string ParamName => this.paramName;

    public ValidateHeaderException(string message, string paramName, Header header)
      : base(message)
    {
      this.paramName = paramName;
      this.header = (Header) header.Clone();
    }
  }
}

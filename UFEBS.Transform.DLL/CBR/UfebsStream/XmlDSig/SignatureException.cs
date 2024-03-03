using System;

namespace CBR.UfebsStream.XmlDSig
{
  public class SignatureException : Exception
  {
    public SignatureException()
    {
    }

    public SignatureException(string message)
      : base(message)
    {
    }

    public SignatureException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}

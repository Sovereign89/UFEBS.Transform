using System;
using System.Xml;

namespace CBR.UfebsStream.Handlers
{
  public class MacValueHandler : IKAHandler
  {
    public void Handle(XmlReader reader, ContentStorage content)
    {
      byte[] currentMacValue = Convert.FromBase64String(reader.ReadElementContentAsString());
      content.MacValue = content.MacValue == null ? currentMacValue : throw new DuplicateMacValueException(content.MacValue, currentMacValue);
    }
  }
}

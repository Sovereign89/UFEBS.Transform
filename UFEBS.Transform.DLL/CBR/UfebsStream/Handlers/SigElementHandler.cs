using System.Xml;

namespace CBR.UfebsStream.Handlers
{
  public class SigElementHandler : IKAHandler
  {
    public void Handle(XmlReader reader, ContentStorage content) => reader.ReadOuterXml();
  }
}

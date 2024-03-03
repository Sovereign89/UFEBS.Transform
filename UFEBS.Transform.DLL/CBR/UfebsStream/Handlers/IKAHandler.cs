using System.Xml;

namespace CBR.UfebsStream.Handlers
{
  public interface IKAHandler
  {
    void Handle(XmlReader reader, ContentStorage content);
  }
}

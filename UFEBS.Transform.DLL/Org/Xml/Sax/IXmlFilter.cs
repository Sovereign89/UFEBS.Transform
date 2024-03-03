namespace Org.Xml.Sax
{
  public interface IXmlFilter : IXmlReader
  {
    IXmlReader Parent { get; set; }
  }
}

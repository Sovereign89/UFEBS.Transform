namespace Org.Xml.Sax
{
  public interface ILocator
  {
    string PublicID { get; }

    string SystemID { get; }

    int LineNumber { get; }

    int ColumnNumber { get; }
  }
}

namespace Org.Xml.Sax
{
  public interface IAttributeList
  {
    int Length { get; }

    int GetIndex(string qName);

    int GetIndex(string uri, string localName);

    string GetLocalName(int index);

    string GetQName(int index);

    string GetUri(int index);

    string GetValue(int index);

    string GetValue(string qName);

    string GetValue(string uri, string localName);

    string GetType(int index);

    string GetType(string qName);

    string GetType(string uri, string localName);
  }
}

namespace Org.Xml.Sax
{
    public interface IContentHandler
    {
        void StartDocument();

        void EndDocument();

        void StartElement(
          string namespaceURI,
          string localName,
          string qName,
          IAttributeList attributes);

        void EndElement(string namespaceURI, string localName, string qName);

        void ProcessingInstruction(string target, string data);

        void StartPrefixMapping(string prefix, string uri);

        void EndPrefixMapping(string prefix);

        void SkippedEntity(string name);

        void Characters(char[] chars, int start, int length);

        void IgnorableWhitespace(char[] chars, int start, int length);

        void SetDocumentLocator(ILocator locator);
    }
}

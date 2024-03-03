using Org.Xml.Sax;
using System.ComponentModel;

namespace Normalizer.TransformStream.Utils
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IElementView
  {
    string LocalName { get; }

    string Uri { get; }

    string QName { get; }

    IAttributeList Attributes { get; }

    void SetElement(string uri, string localName, string qName, IAttributeList attributes);

    int GetNamespaceCount();

    string GetNamespaceUri(int index);

    string GetNamespacePrefix(int index);
  }
}

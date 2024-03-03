using Org.Xml.Sax;
using System.Collections;
using System.ComponentModel;

namespace Normalizer.TransformStream.Utils
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IElementViewState
  {
    AttributeList Attributes { get; }

    int AttributesCount { get; }

    string ElementQName { get; }

    string ElementUri { get; }

    string ElementLocalName { get; }

    ArrayList Namespaces { get; }

    int NamespacesCount { get; }

    string GetAttributeLocalName(int index);

    string GetAttributeQName(int index);

    string GetAttributeUri(int index);

    void AddNamespace(string uri);

    string GetNamespaceUri(int index);

    void SetAttributes(AttributeList attributes);

    void SetElementLocalName(string localName);

    void SetElementQName(string qName);

    void SetElementUri(string uri);

    void SetNamespaces(ArrayList namespaces);
  }
}

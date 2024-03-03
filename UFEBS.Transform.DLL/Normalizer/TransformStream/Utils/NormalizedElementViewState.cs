using Org.Xml.Sax;
using System.Collections;
using System.ComponentModel;

namespace Normalizer.TransformStream.Utils
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class NormalizedElementViewState : IElementViewState
  {
    private AttributeList attributes;
    private string localName;
    private string uri;
    private string qName;
    private ArrayList namespaces;

    public AttributeList Attributes => this.attributes;

    public int AttributesCount => this.attributes.Length;

    public string ElementQName => this.qName;

    public string ElementUri => this.uri;

    public string ElementLocalName => this.localName;

    public ArrayList Namespaces => this.namespaces;

    public int NamespacesCount => this.namespaces.Count;

    public NormalizedElementViewState()
    {
      this.attributes = new AttributeList();
      this.namespaces = new ArrayList();
    }

    public string GetAttributeLocalName(int index) => this.attributes.GetLocalName(index);

    public string GetAttributeQName(int index) => this.attributes.GetQName(index);

    public string GetAttributeUri(int index) => this.attributes.GetUri(index);

    public void AddNamespace(string uri) => this.namespaces.Add((object) uri);

    public string GetNamespaceUri(int index) => (string) this.namespaces[index];

    public void SetAttributes(AttributeList attributes) => this.attributes = attributes;

    public void SetElementLocalName(string localName) => this.localName = localName;

    public void SetElementQName(string qName) => this.qName = qName;

    public void SetElementUri(string uri) => this.uri = uri;

    public void SetNamespaces(ArrayList namespaces) => this.namespaces = namespaces;
  }
}

using Org.Xml.Sax;
using System.Collections;
using System.ComponentModel;

namespace Normalizer.TransformStream.Utils
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class NormalizedElementView : IElementView
  {
    private const string schemaInstanceNamespace = "http://www.w3.org/2001/XMLSchema-instance";
    protected NormalizedElementViewState state;
    private static readonly string[] droppedNames = new string[4]
    {
      "nil",
      "type",
      "schemaLocation",
      "noNamespaceSchemaLocation"
    };

    public string LocalName => this.state.ElementLocalName;

    public string Uri => this.state.ElementUri;

    public string QName => this.state.ElementQName;

    public IAttributeList Attributes => (IAttributeList) this.state.Attributes;

    public NormalizedElementView() => this.state = new NormalizedElementViewState();

    private bool IsEmpty(string prefix) => prefix == string.Empty;

    private string GetPrefixByUri(string uri)
    {
      for (int index = 0; index < this.GetNamespaceCount(); ++index)
      {
        if (this.state.GetNamespaceUri(index) == uri)
          return this.GetNamespacePrefix(index);
      }
      return "";
    }

    private void AddAttributeToState(IAttributeList attributes, int index)
    {
      string uri = attributes.GetUri(index);
      string qname = attributes.GetQName(index);
      string localName = attributes.GetLocalName(index);
      string str = attributes.GetValue(index);
      string type = attributes.GetType(index);
      this.AddUri(uri);
      this.state.Attributes.AddAttribute(uri, localName, qname, type, str);
    }

    private bool IsIgnorableNamespace(string uri) => this.IsEmpty(uri);

    private bool IsSchemaInstance(IAttributeList attributes, int index)
    {
      return attributes.GetUri(index) == "http://www.w3.org/2001/XMLSchema-instance";
    }

    private void AddUri(string uri)
    {
      if (this.IsIgnorableNamespace(uri))
        return;
      this.state.AddNamespace(uri);
    }

    private bool AttributeNameIsDroppedAttribute(IAttributeList attributes, int index)
    {
      for (int index1 = 0; index1 < NormalizedElementView.droppedNames.Length; ++index1)
      {
        if (NormalizedElementView.droppedNames[index1] == attributes.GetLocalName(index))
          return true;
      }
      return false;
    }

    private bool AllowAddAttribute(IAttributeList attributes, int index)
    {
      return !this.IsSchemaInstance(attributes, index) || !this.AttributeNameIsDroppedAttribute(attributes, index);
    }

    private string CreateQNameByPrefixAndLocalName(string prefix, string localName)
    {
      return this.IsEmpty(prefix) ? localName : prefix + ":" + localName;
    }

    protected void SetAttributeQName(int index)
    {
      string attributeUri = this.state.GetAttributeUri(index);
      string attributeLocalName = this.state.GetAttributeLocalName(index);
      string prefixAndLocalName = this.CreateQNameByPrefixAndLocalName(this.GetPrefixByUri(attributeUri), attributeLocalName);
      this.state.Attributes.SetQName(index, prefixAndLocalName);
    }

    private ArrayList UniqueSortNamespaces()
    {
      ArrayList arrayList = (ArrayList) this.state.Namespaces.Clone();
      arrayList.Sort();
      for (int index = 0; index < arrayList.Count; ++index)
      {
        while (index + 1 < arrayList.Count && arrayList[index] == arrayList[index + 1])
          arrayList.RemoveAt(index + 1);
      }
      return arrayList;
    }

    public void SetElement(string uri, string localName, string qName, IAttributeList attributes)
    {
      this.state.SetElementLocalName(localName);
      this.state.SetElementUri(uri);
      this.AddUri(uri);
      for (int index = 0; index < attributes.Length; ++index)
      {
        if (this.AllowAddAttribute(attributes, index))
          this.AddAttributeToState(attributes, index);
      }
      this.state.SetNamespaces(this.UniqueSortNamespaces());
      for (int index = 0; index < this.state.AttributesCount; ++index)
        this.SetAttributeQName(index);
      this.state.SetElementQName(this.CreateQNameByPrefixAndLocalName(this.GetPrefixByUri(uri), this.LocalName));
    }

    public int GetNamespaceCount() => this.state.NamespacesCount;

    public string GetNamespaceUri(int index) => this.state.GetNamespaceUri(index);

    public string GetNamespacePrefix(int index) => "n" + (index + 1).ToString();
  }
}

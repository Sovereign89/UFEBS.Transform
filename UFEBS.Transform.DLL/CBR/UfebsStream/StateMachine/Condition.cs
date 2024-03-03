using System.Xml;

namespace CBR.UfebsStream.StateMachine
{
  public class Condition
  {
    private string localName;
    private string namespaceUri;
    private NodeType nodeType;

    public Condition(string localName, string namespaceUri, NodeType nodeType)
    {
      this.localName = localName;
      this.namespaceUri = namespaceUri;
      this.nodeType = nodeType;
    }

    public bool IsMatch(XmlReader reader)
    {
      if (reader.ReadState != ReadState.Interactive || (reader.NodeType != XmlNodeType.Element || this.nodeType != NodeType.Start || reader.IsEmptyElement) && (reader.NodeType != XmlNodeType.EndElement || this.nodeType != NodeType.End || reader.IsEmptyElement) && (reader.NodeType != XmlNodeType.Element || this.nodeType != NodeType.Empty || !reader.IsEmptyElement) || this.localName != null && !(reader.LocalName == this.localName))
        return false;
      return this.namespaceUri == null || reader.NamespaceURI == this.namespaceUri;
    }
  }
}

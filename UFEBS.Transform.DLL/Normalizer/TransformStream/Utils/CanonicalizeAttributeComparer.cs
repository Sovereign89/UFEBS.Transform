using Org.Xml.Sax;
using System.Collections;
using System.ComponentModel;

namespace Normalizer.TransformStream.Utils
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class CanonicalizeAttributeComparer : IComparer
  {
    private IAttributeList attributes;

    public CanonicalizeAttributeComparer(IAttributeList attributes) => this.attributes = attributes;

    private int Compare(int leftIndex, int rightIndex)
    {
      if (leftIndex == rightIndex)
        return 0;
      int num1 = this.UriOrder(leftIndex, rightIndex);
      if (num1 != 0)
        return num1;
      int num2 = this.QNameOrder(leftIndex, rightIndex);
      if (num2 != 0)
        return num2;
      int num3 = this.LocalNameOrder(leftIndex, rightIndex);
      return num3 != 0 ? num3 : 0;
    }

    private int LocalNameOrder(int leftIndex, int rightIndex)
    {
      return string.Compare(this.attributes.GetLocalName(leftIndex), this.attributes.GetLocalName(rightIndex));
    }

    private int UriOrder(int leftIndex, int rightIndex)
    {
      return string.Compare(this.attributes.GetUri(leftIndex), this.attributes.GetUri(rightIndex));
    }

    private int QNameOrder(int leftIndex, int rightIndex)
    {
      return string.CompareOrdinal(this.attributes.GetQName(leftIndex), this.attributes.GetQName(rightIndex));
    }

    public int Compare(object x, object y) => this.Compare((int) x, (int) y);
  }
}

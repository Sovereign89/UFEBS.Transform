using System;
using System.Collections;
using System.ComponentModel;

namespace Normalizer.TransformStream.Utils
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class PrefixMappingPairComparer : IComparer
  {
    public int Compare(object x, object y)
    {
      PrefixMappingPair prefixMappingPair1 = x as PrefixMappingPair;
      PrefixMappingPair prefixMappingPair2 = y as PrefixMappingPair;
      if (x == null || y == null)
        throw new ArgumentException();
      return string.Compare(prefixMappingPair1.Prefix, prefixMappingPair2.Prefix);
    }
  }
}

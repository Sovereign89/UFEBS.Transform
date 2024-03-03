using System.Collections;
using System.ComponentModel;

namespace Normalizer.TransformStream.Utils
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class PrefixMappingPairList
  {
    private ArrayList items;

    public PrefixMappingPair this[int index] => (PrefixMappingPair) this.items[index];

    public int Count => this.items.Count;

    public PrefixMappingPairList() => this.items = new ArrayList();

    public void Add(PrefixMappingPair pair) => this.items.Add((object) pair);

    public void Clear() => this.items.Clear();

    public string GetPrefix(int index) => this[index].Prefix;

    public string GetUri(int index) => this[index].Uri;

    public void UniqueSort(IComparer comparer)
    {
      ArrayList arrayList = new ArrayList((ICollection) this.items);
      arrayList.Sort(comparer);
      this.items.Clear();
      object x = (object) null;
      for (int index = 0; index < arrayList.Count; ++index)
      {
        if (x == null || comparer.Compare(x, arrayList[index]) != 0)
        {
          x = arrayList[index];
          this.items.Add(x);
        }
      }
    }
  }
}

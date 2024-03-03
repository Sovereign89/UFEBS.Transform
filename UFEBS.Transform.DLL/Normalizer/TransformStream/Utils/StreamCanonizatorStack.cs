using System.Collections;
using System.ComponentModel;

namespace Normalizer.TransformStream.Utils
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class StreamCanonizatorStack
  {
    private bool isFirstPrefixMappingCall;
    private Stack stack;
    private bool isElementFound;

    public StreamCanonizatorStack()
    {
      this.isFirstPrefixMappingCall = true;
      this.stack = new Stack();
      this.isElementFound = false;
    }

    public void AddToTopList(PrefixMappingPair pair) => this.Top().Add(pair);

    public PrefixMappingPairList Top() => (PrefixMappingPairList) this.stack.Peek();

    public void NewLevel()
    {
      this.stack.Push((object) new PrefixMappingPairList());
      this.isFirstPrefixMappingCall = true;
    }

    public void PopLevel()
    {
      this.stack.Pop();
      this.isFirstPrefixMappingCall = true;
    }

    public void AddPrefixMappingEventPair(PrefixMappingPair pair)
    {
      if (this.isFirstPrefixMappingCall)
        this.NewLevel();
      this.isFirstPrefixMappingCall = false;
      if (this.IsIgnorePrefixMappingPair(pair))
        return;
      this.Top().Add(pair);
    }

    public bool IsIgnorePrefixMappingPair(PrefixMappingPair checkPair)
    {
      foreach (PrefixMappingPairList prefixMappingPairList in this.stack.ToArray())
      {
        for (int index = 0; index < prefixMappingPairList.Count; ++index)
        {
          if (prefixMappingPairList[index].Prefix == checkPair.Prefix)
            return prefixMappingPairList[index].Uri == checkPair.Uri;
        }
      }
      return false;
    }

    public string GetPrefix(int topListIndex) => this.Top().GetPrefix(topListIndex);

    public string GetUri(int topListIndex) => this.Top().GetUri(topListIndex);

    public int Size() => this.stack.Count;

    public void StartElementEvent()
    {
      if (this.isFirstPrefixMappingCall)
        this.NewLevel();
      this.isFirstPrefixMappingCall = true;
      this.isElementFound = true;
      this.Top().UniqueSort((IComparer) new PrefixMappingPairComparer());
    }

    public bool IsAfterRootElementPlace() => !this.isElementFound && this.Size() == 1;

    public bool IsBeforeRootElementPlace() => this.isElementFound && this.Size() == 1;
  }
}

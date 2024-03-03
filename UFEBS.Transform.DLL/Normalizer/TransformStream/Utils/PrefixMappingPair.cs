using System.ComponentModel;

namespace Normalizer.TransformStream.Utils
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class PrefixMappingPair
  {
    private string uri;
    private string prefix;

    public string Uri => this.uri;

    public string Prefix => this.prefix;

    public PrefixMappingPair(string uri, string prefix)
    {
      this.uri = uri;
      this.prefix = prefix;
    }

    public override bool Equals(object obj)
    {
      return obj is PrefixMappingPair prefixMappingPair && prefixMappingPair.Uri == this.Uri && prefixMappingPair.prefix == this.Prefix;
    }

    public override int GetHashCode() => this.prefix.GetHashCode() + this.uri.GetHashCode();
  }
}

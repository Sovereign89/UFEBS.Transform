using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Normalizer.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (Normalizer.Properties.Resources.resourceMan == null)
          Normalizer.Properties.Resources.resourceMan = new ResourceManager("Normalizer.Properties.Resources", typeof (Normalizer.Properties.Resources).Assembly);
        return Normalizer.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => Normalizer.Properties.Resources.resourceCulture;
      set => Normalizer.Properties.Resources.resourceCulture = value;
    }

    internal static string NoRulesFound
    {
      get => Normalizer.Properties.Resources.ResourceManager.GetString(nameof (NoRulesFound), Normalizer.Properties.Resources.resourceCulture);
    }
  }
}

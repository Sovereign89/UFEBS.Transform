using System.ComponentModel;
using System.Runtime.InteropServices;

namespace UFEBS.Transformer
{
    [ComVisible(true)]
    [Description("UFEBS.Transformer interface object")]
    public interface ITransformer
    {
        [DispId(1), Description("TransformXML file using canonicalization of C14N-2000 outdated specification")]
        string TransformXML(string FileNameIn);

        [DispId(2), Description("Saves a canonicalized XML file to destination name by UFEBS C14N-2000 specification standards")]
        void SaveXMLToFile(string FileNameIn, string FileNameOut);
    }
}

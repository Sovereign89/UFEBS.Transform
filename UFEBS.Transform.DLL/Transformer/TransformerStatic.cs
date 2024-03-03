using System.Collections.Generic;
using System.IO;
using System.Text;
using UFEBS.Canonicalizer;

namespace UFEBS.Transformer
{
    public static class TransformerStatic
    {
        [Transform.DllExport]
        public static string TransformXML(string FileNameIn)
        {
            using (FileStream inputStream = File.OpenRead(FileNameIn))
            {
                using (MemoryStream outputStream = new MemoryStream())
                {
                    List<byte[]> numArrayList = Canonicalization.SAX.Transform((Stream)inputStream, (Stream)outputStream);
                    inputStream.Close();

                    outputStream.Position = 0;
                    using (StreamReader reader = new StreamReader(outputStream, Encoding.UTF8))
                    {
                        string output = reader.ReadToEnd();
                        return output;
                    }
                }
            }
        }

        [Transform.DllExport]
        public static void SaveXMLToFile(string FileNameIn, string FileNameOut)
        {
            Canonicalization.SAX.Transform(FileNameIn, FileNameOut);
        }
    }
}

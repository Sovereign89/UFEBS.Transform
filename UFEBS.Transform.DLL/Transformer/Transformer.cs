using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UFEBS.Canonicalizer;

namespace UFEBS.Transformer
{
    [ComVisible(true)]
    public class Transformer: ITransformer
    {
        public Transformer(){}

        [ComVisible(true), Description("Returns a canonicalized XML file by UFEBS C14N-2000 specification standards")]
        public string TransformXML(string FileNameIn)
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

        [ComVisible(true), Description("Saves a canonicalized XML file to destination name by UFEBS C14N-2000 specification standards")]
        public void SaveXMLToFile(string FileNameIn, string FileNameOut)
        {
            Canonicalization.SAX.Transform(FileNameIn, FileNameOut);
        }
    }
}

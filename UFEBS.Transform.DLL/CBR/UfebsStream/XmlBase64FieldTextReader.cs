using System;
using System.IO;
using System.Xml;

namespace CBR.UfebsStream
{
  public class XmlBase64FieldTextReader : TextReader
  {
    private const int maxValidChar = 122;
    private XmlReader reader;
    private int lastCharacter;
    private static readonly string charsBase64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    private bool[] map;

    public XmlBase64FieldTextReader(XmlReader reader)
    {
      if (reader.NodeType == XmlNodeType.Element)
        reader.Read();
      this.reader = reader.NodeType == XmlNodeType.Text ? reader : throw new ArgumentException("XmlReader должен быть спозиционирован на элементе, содержащем текстовые данные в формате base64Binary", nameof (reader));
      this.lastCharacter = -1;
      this.map = XmlBase64FieldTextReader.ConstructMapBase64();
    }

    private static bool[] ConstructMapBase64()
    {
      bool[] flagArray = new bool[123];
      for (int index = 0; index < flagArray.Length; ++index)
        flagArray[index] = false;
      for (int index = 0; index < XmlBase64FieldTextReader.charsBase64.Length; ++index)
        flagArray[(int) XmlBase64FieldTextReader.charsBase64[index]] = true;
      return flagArray;
    }

    public override int Peek() => this.lastCharacter;

    public override int Read()
    {
      char[] buffer;
      do
      {
        buffer = new char[1];
        if (this.reader.ReadValueChunk(buffer, 0, 1) == 0)
          goto label_2;
      }
      while (!this.IsBase64Char(buffer[0]));
      goto label_3;
label_2:
      this.lastCharacter = -1;
      goto label_4;
label_3:
      this.lastCharacter = (int) buffer[0];
label_4:
      return this.lastCharacter;
    }

    private bool IsBase64Char(char c) => this.map[(int) c];
  }
}

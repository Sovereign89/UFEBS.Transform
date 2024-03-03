using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CBR.UfebsStream.ED.Serialization
{
  public abstract class EDSerializerBase
  {
    protected string ufebsEdNamespace;
    protected string ufebsCodeNamespace;
    protected XmlSerializerNamespaces nss;

    protected EDSerializerBase(string ufebsEdNamespace, string ufebsCodeNamespace)
    {
      this.ufebsCodeNamespace = ufebsCodeNamespace;
      this.ufebsEdNamespace = ufebsEdNamespace;
      this.nss = new XmlSerializerNamespaces();
      this.nss.Add("", ufebsEdNamespace);
    }

    public object Deserialize(Stream stream)
    {
      return stream != null ? this.Deserialize((XmlReader) new XmlTextReader(stream)
      {
        Normalization = true,
        EntityHandling = EntityHandling.ExpandEntities,
        WhitespaceHandling = WhitespaceHandling.All
      }) : throw new ArgumentNullException(nameof (stream));
    }

    public object Deserialize(TextReader reader)
    {
      return reader != null ? this.Deserialize((XmlReader) new XmlTextReader(reader)) : throw new ArgumentNullException(nameof (reader));
    }

    public object Deserialize(XmlReader reader)
    {
      return reader != null ? this.DeserializeInternal(reader) : throw new ArgumentNullException(nameof (reader));
    }

    protected abstract object DeserializeInternal(XmlReader reader);

    public void Serialize(Stream stream, object obj)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof (stream));
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      this.Serialize((XmlWriter) new XmlTextWriter((Stream) new XDFilterStream(stream), Encoding.GetEncoding("Windows-1251")), obj);
    }

    public void Serialize(TextWriter writer, object obj)
    {
      if (writer == null)
        throw new ArgumentNullException(nameof (writer));
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      this.Serialize((XmlWriter) new XmlTextWriter(writer), obj);
    }

    public void Serialize(XmlWriter writer, object obj)
    {
      if (writer == null)
        throw new ArgumentNullException(nameof (writer));
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      this.SerializeInternal(writer, obj);
      writer.Flush();
    }

    protected abstract void SerializeInternal(XmlWriter writer, object obj);

    protected static string GetWriteObjectMethodName(string ObjectName) => "Write_" + ObjectName;

    protected string GetRootElementName(XmlReader reader)
    {
      while (reader.Read())
      {
        if (reader.NodeType == XmlNodeType.Element)
          return reader.LocalName;
      }
      return string.Empty;
    }

    protected static string GetReadObjectMethodName(string ObjectName) => "Read_" + ObjectName;
  }
}

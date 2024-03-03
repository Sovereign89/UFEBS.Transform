using System;
using System.Xml;

namespace CBR.UfebsStream.Headers
{
  public abstract class Header : ICloneable
  {
    public abstract CBR.UfebsStream.QName Name { get; }

    public bool Validate(out string exceptionString)
    {
      try
      {
        this.Validate();
        exceptionString = (string) null;
        return true;
      }
      catch (ValidateHeaderException ex)
      {
        exceptionString = ex.Message;
        return false;
      }
    }

    public abstract void Validate();

    public abstract void SerializeTo(XmlWriter writer);

    public abstract void LoadFrom(XmlReader reader);

    public abstract object Clone();

    protected void CheckElement(XmlReader reader)
    {
      if (reader.NodeType != XmlNodeType.Element)
        throw new InvalidOperationException(string.Format("Xml-анализатор не спозиционирован на элементе {0} из пространства имен {1}. Текущий узел имеет тип {2}", (object) this.Name.LocalName, (object) this.Name.NamespaceURI, (object) reader.NodeType));
      if (reader.IsEmptyElement)
        throw new InvalidOperationException(string.Format("Xml-анализатор не спозиционирован на элементе {0} из пространства имен {1}. Текущий элемент пуст", (object) this.Name.LocalName, (object) this.Name.NamespaceURI));
      if (reader.LocalName != this.Name.LocalName || reader.NamespaceURI != this.Name.NamespaceURI)
        throw new InvalidOperationException(string.Format("Xml-анализатор не спозиционирован на элементе {0} из пространства имен {1}. Текущий элемент {2} из пространства имен {3}", (object) this.Name.LocalName, (object) this.Name.NamespaceURI, (object) reader.LocalName, (object) reader.NamespaceURI));
    }
  }
}

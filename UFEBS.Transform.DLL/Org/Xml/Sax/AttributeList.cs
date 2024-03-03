using System;
using System.Collections;
using System.ComponentModel;

namespace Org.Xml.Sax
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class AttributeList : IAttributeList, ICloneable
  {
    private ArrayList attributes;

    public int Length => this.attributes.Count;

    public AttributeList() => this.attributes = new ArrayList();

    public int GetIndex(string qName)
    {
      for (int index = 0; index < this.attributes.Count; ++index)
      {
        if ((this.attributes[index] as AttributeList.Attribute).qName == qName)
          return index;
      }
      throw new ArgumentException(string.Format("Атрибута с квалифицированным именем {0}нет в списке", (object) qName));
    }

    public int GetIndex(string uri, string localName)
    {
      for (int index = 0; index < this.attributes.Count; ++index)
      {
        AttributeList.Attribute attribute = this.attributes[index] as AttributeList.Attribute;
        if (attribute.uri == uri && attribute.localName == localName)
          return index;
      }
      throw new ArgumentException(string.Format("Атрибута с локальным именем {0}и пространством имен {1} нет в списке", (object) localName, (object) uri));
    }

    public string GetLocalName(int index)
    {
      if (this.CheckIndex(index))
        return (this.attributes[index] as AttributeList.Attribute).localName;
      throw new IndexOutOfRangeException(string.Format("Атрибута с индексом {0} нет в списке", (object) index));
    }

    public string GetQName(int index)
    {
      if (this.CheckIndex(index))
        return (this.attributes[index] as AttributeList.Attribute).qName;
      throw new IndexOutOfRangeException(string.Format("Атрибута с индексом {0} нет в списке", (object) index));
    }

    public string GetUri(int index)
    {
      if (this.CheckIndex(index))
        return (this.attributes[index] as AttributeList.Attribute).uri;
      throw new IndexOutOfRangeException(string.Format("Атрибута с индексом {0} нет в списке", (object) index));
    }

    public string GetValue(int index)
    {
      if (this.CheckIndex(index))
        return (this.attributes[index] as AttributeList.Attribute).value;
      throw new IndexOutOfRangeException(string.Format("Атрибута с индексом {0} нет в списке", (object) index));
    }

    public string GetValue(string qName)
    {
      for (int index = 0; index < this.attributes.Count; ++index)
      {
        AttributeList.Attribute attribute = this.attributes[index] as AttributeList.Attribute;
        if (attribute.qName == qName)
          return attribute.value;
      }
      throw new ArgumentException(string.Format("Атрибута с квалифицированным именем {0}нет в списке", (object) qName));
    }

    public string GetValue(string uri, string localName)
    {
      for (int index = 0; index < this.attributes.Count; ++index)
      {
        AttributeList.Attribute attribute = this.attributes[index] as AttributeList.Attribute;
        if (attribute.uri == uri && attribute.localName == localName)
          return attribute.value;
      }
      throw new ArgumentException(string.Format("Атрибута с локальным именем {0}и пространством имен {1} нет в списке", (object) localName, (object) uri));
    }

    public string GetType(int index)
    {
      if (this.CheckIndex(index))
        return (this.attributes[index] as AttributeList.Attribute).type;
      throw new IndexOutOfRangeException(string.Format("Атрибута с индексом {0} нет в списке", (object) index));
    }

    public string GetType(string qName)
    {
      for (int index = 0; index < this.attributes.Count; ++index)
      {
        AttributeList.Attribute attribute = this.attributes[index] as AttributeList.Attribute;
        if (attribute.qName == qName)
          return attribute.type;
      }
      throw new ArgumentException(string.Format("Атрибута с квалифицированным именем {0}нет в списке", (object) qName));
    }

    public string GetType(string uri, string localName)
    {
      for (int index = 0; index < this.attributes.Count; ++index)
      {
        AttributeList.Attribute attribute = this.attributes[index] as AttributeList.Attribute;
        if (attribute.uri == uri && attribute.localName == localName)
          return attribute.type;
      }
      throw new ArgumentException(string.Format("Атрибута с локальным именем {0}и пространством имен {1} нет в списке", (object) localName, (object) uri));
    }

    private bool CheckIndex(int index) => index >= 0 && index < this.attributes.Count;

    public object Clone()
    {
      AttributeList attributeList = new AttributeList();
      for (int index = 0; index < this.attributes.Count; ++index)
        attributeList.attributes.Add((this.attributes[index] as AttributeList.Attribute).Clone());
      return (object) attributeList;
    }

    public void Clear() => this.attributes.Clear();

    public void AddAttribute(
      string uri,
      string localName,
      string qName,
      string type,
      string value)
    {
      this.attributes.Add((object) new AttributeList.Attribute(uri, localName, qName, type, value));
    }

    public void SetAttribute(
      int index,
      string uri,
      string localName,
      string qName,
      string type,
      string value)
    {
      AttributeList.Attribute attribute = this.CheckIndex(index) ? this.attributes[index] as AttributeList.Attribute : throw new IndexOutOfRangeException(string.Format("Атрибута с индексом {0} нет в списке", (object) index));
      attribute.localName = localName;
      attribute.qName = qName;
      attribute.type = type;
      attribute.uri = uri;
      attribute.value = value;
    }

    public void RemoveAttribute(int index)
    {
      if (!this.CheckIndex(index))
        throw new IndexOutOfRangeException(string.Format("Атрибута с индексом {0} нет в списке", (object) index));
      this.attributes.RemoveAt(index);
    }

    public void SetLocalName(int index, string localName)
    {
      if (!this.CheckIndex(index))
        throw new IndexOutOfRangeException(string.Format("Атрибута с индексом {0} нет в списке", (object) index));
      (this.attributes[index] as AttributeList.Attribute).localName = localName;
    }

    public void SetUri(int index, string uri)
    {
      if (!this.CheckIndex(index))
        throw new IndexOutOfRangeException(string.Format("Атрибута с индексом {0} нет в списке", (object) index));
      (this.attributes[index] as AttributeList.Attribute).uri = uri;
    }

    public void SetQName(int index, string qName)
    {
      if (!this.CheckIndex(index))
        throw new IndexOutOfRangeException(string.Format("Атрибута с индексом {0} нет в списке", (object) index));
      (this.attributes[index] as AttributeList.Attribute).qName = qName;
    }

    public void SetType(int index, string type)
    {
      if (!this.CheckIndex(index))
        throw new IndexOutOfRangeException(string.Format("Атрибута с индексом {0} нет в списке", (object) index));
      (this.attributes[index] as AttributeList.Attribute).type = type;
    }

    public void SetValue(int index, string value)
    {
      if (!this.CheckIndex(index))
        throw new IndexOutOfRangeException(string.Format("Атрибута с индексом {0} нет в списке", (object) index));
      (this.attributes[index] as AttributeList.Attribute).value = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class Attribute : ICloneable
    {
      internal string uri;
      internal string localName;
      internal string qName;
      internal string type;
      internal string value;

      internal Attribute(string uri, string localName, string qName, string type, string value)
      {
        this.uri = uri;
        this.localName = localName;
        this.qName = qName;
        this.type = type;
        this.value = value;
      }

      public object Clone()
      {
        return (object) new AttributeList.Attribute(this.uri, this.localName, this.qName, this.type, this.value);
      }
    }
  }
}

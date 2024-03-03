using System;

namespace CBR.UfebsStream
{
  [Serializable]
  public class QName : ICloneable, IComparable
  {
    private string namespaceURI;
    private string localName;

    public string LocalName
    {
      get => this.localName;
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (LocalName));
        this.localName = !(value == string.Empty) ? value : throw new ArgumentException(nameof (LocalName));
      }
    }

    public string NamespaceURI
    {
      get => this.namespaceURI == null ? string.Empty : this.namespaceURI;
      set => this.namespaceURI = value;
    }

    public QName(string localName, string namespaceURI)
    {
      this.localName = localName != null ? localName : throw new ArgumentNullException(nameof (localName));
      this.namespaceURI = namespaceURI;
    }

    public static bool operator ==(QName lhs, QName rhs)
    {
      return object.Equals((object) lhs, (object) rhs);
    }

    public static bool operator !=(QName lhs, QName rhs)
    {
      return !object.Equals((object) lhs, (object) rhs);
    }

    public static bool operator >(QName lhs, QName rhs) => lhs.CompareTo((object) rhs) > 0;

    public static bool operator <(QName lhs, QName rhs) => lhs.CompareTo((object) rhs) < 0;

    public static bool operator >=(QName lhs, QName rhs) => lhs.CompareTo((object) rhs) >= 0;

    public static bool operator <=(QName lhs, QName rhs) => lhs.CompareTo((object) rhs) <= 0;

    public override bool Equals(object obj)
    {
      try
      {
        return this.CompareTo(obj) == 0;
      }
      catch (ArgumentException)
      {
        return false;
      }
    }

    public override int GetHashCode()
    {
      return this.localName.GetHashCode() ^ this.namespaceURI.GetHashCode();
    }

    public override string ToString()
    {
      return this.namespaceURI == null ? this.localName : "{" + this.namespaceURI + "}" + this.localName;
    }

    public object Clone() => (object) new QName(this.localName, this.namespaceURI);

    public int CompareTo(object obj)
    {
      if (obj == null)
        return 1;
      QName qname = !(obj.GetType() != typeof (QName)) ? (QName) obj : throw new ArgumentException();
      int num1 = this.localName.CompareTo(qname.LocalName);
      int num2 = this.NamespaceURI.CompareTo(qname.NamespaceURI);
      if (num1 != 0)
        return num1;
      return num2 != 0 ? num2 : 0;
    }
  }
}

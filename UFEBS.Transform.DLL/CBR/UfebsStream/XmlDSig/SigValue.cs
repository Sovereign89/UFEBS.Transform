using System;
using System.Xml;

namespace CBR.UfebsStream.XmlDSig
{
  public class SigValue
  {
    private byte[] signature;
    private string sigId;

    public byte[] Signature => this.signature;

    public string SigId => this.sigId;

    public SigValue(XmlElement element)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (element.NodeType != XmlNodeType.Element)
        throw new SignatureException("Переданный узел не является узлом типа \"элемент\"");
      if (element.LocalName != nameof (SigValue))
        throw new SignatureException("Локальное имя переданного узла отлично от SigValue");
      if (element.NamespaceURI != "urn:cbr-ru:dsig:v1.1")
        throw new SignatureException(string.Format("Пространство имен переданного узла {0} отлично от {1}", (object) element.NamespaceURI, (object) "urn:cbr-ru:dsig:v1.1"));
      try
      {
        this.signature = Convert.FromBase64String(element.InnerText);
      }
      catch (FormatException ex)
      {
        throw new SignatureException("Ошибка при декодировании значения ЗК из кодировки Base64", (Exception) ex);
      }
      this.CheckSignature(this.signature);
      XmlAttribute attribute = element.Attributes[nameof (SigId)];
      if (attribute == null)
        return;
      this.CheckSigID(attribute.InnerText);
      this.sigId = attribute.InnerText;
    }

    public SigValue(byte[] signature)
    {
      this.CheckSignature(signature);
      this.signature = signature;
      this.sigId = (string) null;
    }

    public SigValue(byte[] signature, string sigId)
    {
      this.CheckSignature(signature);
      this.signature = signature;
      this.CheckSigID(sigId);
      this.sigId = sigId;
    }

    private void CheckSigID(string valueToCheck)
    {
      if (valueToCheck == null)
        throw new ArgumentNullException("sigId");
      if (valueToCheck.Length != 2)
        throw new SignatureException(string.Format("Длина идентификатора ЗК {0} отлична от двух символов", (object) valueToCheck));
      if (!char.IsDigit(valueToCheck[0]) || !char.IsDigit(valueToCheck[1]))
        throw new SignatureException(string.Format("Идентификатор ЗК {0} должен состоять из двух цифровых символов", (object) valueToCheck));
    }

    private void CheckSignature(byte[] signature)
    {
      if (signature == null)
        throw new ArgumentNullException(nameof (signature));
      if (signature.Length == 0)
        throw new SignatureException("Длина ЗК не может быть равна нулю");
    }

    public void Serialize(XmlWriter writer)
    {
      if (writer == null)
        throw new ArgumentNullException(nameof (writer));
      if (writer.WriteState == WriteState.Closed)
        throw new ObjectDisposedException(nameof (writer));
      writer.WriteStartElement("dsig", nameof (SigValue), "urn:cbr-ru:dsig:v1.1");
      if (this.sigId != null)
        writer.WriteAttributeString("SigId", this.sigId);
      writer.WriteString(Convert.ToBase64String(this.signature));
      writer.WriteEndElement();
    }

    public XmlElement ToXmlElement()
    {
      XmlDocument xmlDocument = new XmlDocument();
      XmlElement element = xmlDocument.CreateElement("dsig", nameof (SigValue), "urn:cbr-ru:dsig:v1.1");
      XmlText textNode = xmlDocument.CreateTextNode(Convert.ToBase64String(this.signature));
      element.AppendChild((XmlNode) textNode);
      if (this.sigId != null)
      {
        XmlAttribute attribute = xmlDocument.CreateAttribute("SigId");
        attribute.Value = this.sigId;
        element.Attributes.Append(attribute);
      }
      return element;
    }
  }
}

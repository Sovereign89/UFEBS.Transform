using System.Xml;

namespace CBR.UfebsStream.Headers
{
  public class AcknowledgementInfo12 : Header
  {
    private int acknowledgementType;
    private string resultCode;
    private string resultText;

    public int AcknowledgementType
    {
      get => this.acknowledgementType;
      set => this.acknowledgementType = value;
    }

    public string ResultCode
    {
      get => this.resultCode;
      set => this.resultCode = value;
    }

    public string ResultText
    {
      get => this.resultText;
      set => this.resultText = value;
    }

    public override CBR.UfebsStream.QName Name
    {
      get => new CBR.UfebsStream.QName("AcknowledgementInfo", "urn:cbr-ru:msg:props:v1.2");
    }

    public AcknowledgementInfo12()
    {
      this.acknowledgementType = 0;
      this.resultCode = "0000";
      this.resultText = (string) null;
    }

    public AcknowledgementInfo12(int acknowledgementType, string resultCode, string resultText)
    {
      this.acknowledgementType = acknowledgementType;
      this.resultCode = resultCode;
      this.resultText = resultText;
    }

    public override void Validate()
    {
      if (this.acknowledgementType > 3)
        throw new ValidateHeaderException(string.Format("Значение поля AcknowledgementType содержит некорректное значение {0}. Допустимые значения - от 1 до 3", (object) this.acknowledgementType), "acknowledgementType", (Header) this);
      if (this.acknowledgementType < 1)
        throw new ValidateHeaderException(string.Format("Значение поля AcknowledgementType содержит некорректное значение {0}. Допустимые значения - от 1 до 3", (object) this.acknowledgementType), "acknowledgementType", (Header) this);
      if (string.IsNullOrEmpty(this.resultCode))
        throw new ValidateHeaderException("Значение поля ResultCode пусто", "resultCode", (Header) this);
      if (this.resultCode.Length != 4)
        throw new ValidateHeaderException(string.Format("Длина значение поля ResultCode отлична от 4"), "resultCode", (Header) this);
      if (!char.IsDigit(this.resultCode, 0) || !char.IsDigit(this.resultCode, 1) || !char.IsDigit(this.resultCode, 2) || !char.IsDigit(this.resultCode, 3))
        throw new ValidateHeaderException(string.Format("Значение поля ResultCode не соответствует ограничению \\d{4}"), "resultCode", (Header) this);
      if (this.resultText == null)
        return;
      if (this.resultText.Length > 3000)
        throw new ValidateHeaderException("Значение поля ResultText слишком велико. Допустимая длина - от 1 до 3000 символов", "resultText", (Header) this);
      if (this.resultText.Length < 1)
        throw new ValidateHeaderException("Значение поля ResultText слишком мало. Допустимая длина - от 1 до 3000 символов", "resultText", (Header) this);
    }

    public override object Clone()
    {
      return (object) new AcknowledgementInfo12(this.acknowledgementType, this.resultCode, this.resultText);
    }

    public override void SerializeTo(XmlWriter writer)
    {
      writer.WriteStartElement("props", "AcknowledgementInfo", this.Name.NamespaceURI);
      writer.WriteElementString("props", "AcknowledgementType", this.Name.NamespaceURI, XmlConvert.ToString(this.acknowledgementType));
      writer.WriteElementString("props", "ResultCode", this.Name.NamespaceURI, this.resultCode);
      if (this.resultText != null)
        writer.WriteElementString("props", "ResultText", this.Name.NamespaceURI, this.resultText);
      writer.WriteEndElement();
    }

    public override void LoadFrom(XmlReader reader)
    {
      this.CheckElement(reader);
      reader.Read();
      do
      {
        if (this.IfRequisite("AcknowledgementType", reader))
          this.acknowledgementType = int.Parse(reader.ReadElementContentAsString());
        if (this.IfRequisite("ResultCode", reader))
          this.resultCode = reader.ReadElementContentAsString();
        if (this.IfRequisite("ResultText", reader))
          this.resultText = reader.ReadElementContentAsString();
        if (reader.NodeType != XmlNodeType.Element && reader.NodeType != XmlNodeType.EndElement)
          reader.Read();
      }
      while (reader.NodeType != XmlNodeType.EndElement || !(reader.LocalName == this.Name.LocalName) || !(reader.NamespaceURI == this.Name.NamespaceURI));
      reader.Read();
    }

    private bool IfRequisite(string requisiteName, XmlReader reader)
    {
      return reader.NodeType == XmlNodeType.Element && reader.NamespaceURI == this.Name.NamespaceURI && reader.LocalName == requisiteName;
    }
  }
}

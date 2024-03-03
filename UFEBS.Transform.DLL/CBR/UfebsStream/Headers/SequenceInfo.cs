using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace CBR.UfebsStream.Headers
{
  public class SequenceInfo : Header
  {
    private int sequenceNumber;
    private DateTime sequenceDate;
    private string sequenceUIC;

    public int SequenceNumber
    {
      get => this.sequenceNumber;
      set => this.sequenceNumber = value;
    }

    public DateTime SequenceDate
    {
      get => this.sequenceDate;
      set => this.sequenceDate = value;
    }

    public string SequenceUIC
    {
      get => this.sequenceUIC;
      set => this.sequenceUIC = value;
    }

    public override CBR.UfebsStream.QName Name
    {
      get => new CBR.UfebsStream.QName(nameof (SequenceInfo), "urn:cbr-ru:msg:props:v1.2");
    }

    public SequenceInfo()
    {
      this.sequenceNumber = 0;
      this.sequenceDate = DateTime.Today;
      this.sequenceUIC = (string) null;
    }

    public SequenceInfo(int sequenceNumber, DateTime sequenceDate, string sequenceUIC)
    {
      this.sequenceNumber = sequenceNumber;
      this.sequenceDate = sequenceDate;
      this.sequenceUIC = sequenceUIC;
    }

    public override void Validate()
    {
      if (this.sequenceNumber < 1)
        throw new ValidateHeaderException("Значение параметра sequenceNumber меньше чем 1", "sequenceNumber", (Header) this);
      if (this.sequenceNumber > 1000000000)
        throw new ValidateHeaderException("Значение параметра sequenceNumber больше чем 1000000000", "sequenceNumber", (Header) this);
      Regex regex = new Regex("\\A\\d{10}\\Z");
      if (this.sequenceUIC != null && !regex.IsMatch(this.sequenceUIC))
        throw new ValidateHeaderException("Значение параметра sequenceNumber содержит неверное значение " + this.sequenceUIC, "sequenceUIC", (Header) this);
    }

    public override object Clone()
    {
      return (object) new SequenceInfo(this.sequenceNumber, this.sequenceDate, this.sequenceUIC);
    }

    public override void SerializeTo(XmlWriter writer)
    {
      writer.WriteStartElement("props", nameof (SequenceInfo), "urn:cbr-ru:msg:props:v1.2");
      writer.WriteElementString("props", "SequenceNumber", "urn:cbr-ru:msg:props:v1.2", XmlConvert.ToString(this.sequenceNumber));
      writer.WriteElementString("props", "SequenceDate", "urn:cbr-ru:msg:props:v1.2", this.sequenceDate.ToString("yyyy-MM-dd"));
      if (this.sequenceUIC != null)
        writer.WriteElementString("props", "SequenceUIC", "urn:cbr-ru:msg:props:v1.2", this.sequenceUIC);
      writer.WriteEndElement();
    }

    public override void LoadFrom(XmlReader reader)
    {
      this.CheckElement(reader);
      reader.Read();
      do
      {
        if (this.IfRequisite("SequenceNumber", reader))
          this.sequenceNumber = reader.ReadElementContentAsInt();
        if (this.IfRequisite("SequenceDate", reader))
          this.sequenceDate = reader.ReadElementContentAsDateTime();
        if (this.IfRequisite("SequenceUIC", reader))
          this.sequenceUIC = reader.ReadElementContentAsString();
        if (reader.NodeType != XmlNodeType.Element && reader.NodeType != XmlNodeType.EndElement)
          reader.Read();
      }
      while (reader.NodeType != XmlNodeType.EndElement || !(reader.LocalName == nameof (SequenceInfo)) || !(reader.NamespaceURI == "urn:cbr-ru:msg:props:v1.2"));
      reader.Read();
    }

    private bool IfRequisite(string requisiteName, XmlReader reader)
    {
      return reader.NodeType == XmlNodeType.Element && reader.NamespaceURI == "urn:cbr-ru:msg:props:v1.2" && reader.LocalName == requisiteName;
    }
  }
}

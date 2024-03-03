using System;
using System.Xml;

namespace CBR.UfebsStream.Headers
{
  public class MessageInfo12 : Header
  {
    private string to;
    private string from;
    private string appMessageID;
    private string messageID;
    private string correlationMessageID;
    private int messageType;
    private int priority;
    private string legacyTransportFileName;
    private DateTime? createTime;
    private DateTime? sendTime;
    private DateTime? receiveTime;
    private DateTime? acceptTime;
    private bool? ackRequest;

    public string To
    {
      get => this.to;
      set => this.to = value;
    }

    public string From
    {
      get => this.from;
      set => this.from = value;
    }

    public string AppMessageID
    {
      get => this.appMessageID;
      set => this.appMessageID = value;
    }

    public string MessageID
    {
      get => this.messageID;
      set => this.messageID = value;
    }

    public string CorrelationMessageID
    {
      get => this.correlationMessageID;
      set => this.correlationMessageID = value;
    }

    public int MessageType
    {
      get => this.messageType;
      set => this.messageType = value;
    }

    public int Priority
    {
      get => this.priority;
      set => this.priority = value;
    }

    public string LegacyTransportFileName
    {
      get => this.legacyTransportFileName;
      set => this.legacyTransportFileName = value;
    }

    public DateTime? CreateTime
    {
      get => this.createTime;
      set => this.createTime = value;
    }

    public DateTime? SendTime
    {
      get => this.sendTime;
      set => this.sendTime = value;
    }

    public DateTime? ReceiveTime
    {
      get => this.receiveTime;
      set => this.receiveTime = value;
    }

    public DateTime? AcceptTime
    {
      get => this.acceptTime;
      set => this.acceptTime = value;
    }

    public bool? AckRequest
    {
      get => this.ackRequest;
      set => this.ackRequest = value;
    }

    public override CBR.UfebsStream.QName Name
    {
      get => new CBR.UfebsStream.QName("MessageInfo", "urn:cbr-ru:msg:props:v1.2");
    }

    public MessageInfo12()
    {
      this.to = (string) null;
      this.from = (string) null;
      this.appMessageID = (string) null;
      this.messageID = (string) null;
      this.correlationMessageID = (string) null;
      this.messageType = 0;
      this.priority = 0;
      this.legacyTransportFileName = (string) null;
      this.createTime = new DateTime?();
      this.sendTime = new DateTime?();
      this.receiveTime = new DateTime?();
      this.acceptTime = new DateTime?();
      this.ackRequest = new bool?();
    }

    public MessageInfo12(
      string to,
      string from,
      string appMessageID,
      string messageID,
      string correlationMessageID,
      int messageType,
      int priority,
      string legacyTransportFileName,
      DateTime? createTime,
      DateTime? sendTime,
      DateTime? receiveTime,
      DateTime? acceptTime,
      bool? ackRequest)
    {
      this.to = to;
      this.from = from;
      this.appMessageID = appMessageID;
      this.messageID = messageID;
      this.correlationMessageID = correlationMessageID;
      this.messageType = messageType;
      this.priority = priority;
      this.legacyTransportFileName = legacyTransportFileName;
      this.createTime = createTime;
      this.sendTime = sendTime;
      this.receiveTime = receiveTime;
      this.acceptTime = acceptTime;
      this.ackRequest = ackRequest;
    }

    public override object Clone()
    {
      return (object) new MessageInfo12(this.to, this.from, this.appMessageID, this.messageID, this.correlationMessageID, this.messageType, this.priority, this.legacyTransportFileName, this.createTime, this.sendTime, this.receiveTime, this.acceptTime, this.ackRequest);
    }

    public override void Validate()
    {
      this.ValidateAddressInfoIDType(this.to, "to");
      this.ValidateAddressInfoIDType(this.from, "from");
      this.ValidateMessageIDType(this.appMessageID, "appMessageID");
      this.ValidateMessageIDType(this.messageID, "messageID");
      this.ValidateMessageIDType(this.correlationMessageID, "correlationMessageID");
      this.ValidateMessageType(this.messageType);
      this.ValidatePriority(this.priority);
    }

    private void ValidateAddressInfoIDType(string value, string parameterName)
    {
      if (value == null)
        throw new ValidateHeaderException(string.Format("Параметр {0} содержит значение null", (object) parameterName), parameterName, (Header) this);
    }

    private void ValidateMessageIDType(string value, string parameterName)
    {
      if (value == null)
        return;
      if (value.Length > 100)
        throw new ValidateHeaderException(string.Format("Параметр {0} неверной длины. Допустимый размер - от 1 до 100 символов", (object) parameterName), parameterName, (Header) this);
      if (value.Length < 1)
        throw new ValidateHeaderException(string.Format("Параметр {0} неверной длины. Допустимый размер - от 1 до 100 символов", (object) parameterName), parameterName, (Header) this);
    }

    private void ValidateMessageType(int value)
    {
      if (value > 5)
        throw new ValidateHeaderException(string.Format("Параметр messageType содержит неверное значение {0}. Допустимые значения - от 1 до 5", (object) value), "messageType", (Header) this);
      if (value < 1)
        throw new ValidateHeaderException(string.Format("Параметр messageType содержит неверное значение {0}. Допустимые значения - от 1 до 5", (object) value), "messageType", (Header) this);
    }

    private void ValidatePriority(int value)
    {
      if (value > 9)
        throw new ValidateHeaderException(string.Format("Параметр priority содержит неверное значение {0}. Допустимые значения - от 0 до 9", (object) value), "priority", (Header) this);
      if (value < 0)
        throw new ValidateHeaderException(string.Format("Параметр priority содержит неверное значение {0}. Допустимые значения - от 0 до 9", (object) value), "priority", (Header) this);
    }

    public override void SerializeTo(XmlWriter writer)
    {
      writer.WriteStartElement("props", "MessageInfo", this.Name.NamespaceURI);
      writer.WriteElementString("props", "To", this.Name.NamespaceURI, this.to);
      writer.WriteElementString("props", "From", this.Name.NamespaceURI, this.from);
      if (this.appMessageID != null)
        writer.WriteElementString("props", "AppMessageID", this.Name.NamespaceURI, this.appMessageID);
      if (this.messageID != null)
        writer.WriteElementString("props", "MessageID", this.Name.NamespaceURI, this.messageID);
      if (this.correlationMessageID != null)
        writer.WriteElementString("props", "CorrelationMessageID", this.Name.NamespaceURI, this.correlationMessageID);
      writer.WriteElementString("props", "MessageType", this.Name.NamespaceURI, XmlConvert.ToString(this.messageType));
      writer.WriteElementString("props", "Priority", this.Name.NamespaceURI, XmlConvert.ToString(this.priority));
      if (this.legacyTransportFileName != null)
        writer.WriteElementString("props", "LegacyTransportFileName", this.Name.NamespaceURI, this.legacyTransportFileName);
      DateTime dateTime;
      if (this.createTime.HasValue)
      {
        XmlWriter xmlWriter = writer;
        string namespaceUri = this.Name.NamespaceURI;
        dateTime = this.createTime.Value;
        string str = XmlConvert.ToString(dateTime.ToUniversalTime(), "yyyy-MM-ddTHH:mm:ssZ");
        xmlWriter.WriteElementString("props", "CreateTime", namespaceUri, str);
      }
      if (this.sendTime.HasValue)
      {
        XmlWriter xmlWriter = writer;
        string namespaceUri = this.Name.NamespaceURI;
        dateTime = this.sendTime.Value;
        string str = XmlConvert.ToString(dateTime.ToUniversalTime(), "yyyy-MM-ddTHH:mm:ssZ");
        xmlWriter.WriteElementString("props", "SendTime", namespaceUri, str);
      }
      if (this.receiveTime.HasValue)
      {
        XmlWriter xmlWriter = writer;
        string namespaceUri = this.Name.NamespaceURI;
        dateTime = this.receiveTime.Value;
        string str = XmlConvert.ToString(dateTime.ToUniversalTime(), "yyyy-MM-ddTHH:mm:ssZ");
        xmlWriter.WriteElementString("props", "ReceiveTime", namespaceUri, str);
      }
      if (this.acceptTime.HasValue)
      {
        XmlWriter xmlWriter = writer;
        string namespaceUri = this.Name.NamespaceURI;
        dateTime = this.acceptTime.Value;
        string str = XmlConvert.ToString(dateTime.ToUniversalTime(), "yyyy-MM-ddTHH:mm:ssZ");
        xmlWriter.WriteElementString("props", "AcceptTime", namespaceUri, str);
      }
      if (this.ackRequest.HasValue)
        writer.WriteElementString("props", "AckRequest", this.Name.NamespaceURI, XmlConvert.ToString(this.ackRequest.Value));
      writer.WriteEndElement();
    }

    public override void LoadFrom(XmlReader reader)
    {
      this.CheckElement(reader);
      reader.Read();
      do
      {
        if (this.IfRequisite("To", reader))
          this.to = reader.ReadElementContentAsString();
        if (this.IfRequisite("From", reader))
          this.from = reader.ReadElementContentAsString();
        if (this.IfRequisite("AppMessageID", reader))
          this.appMessageID = reader.ReadElementContentAsString();
        if (this.IfRequisite("MessageID", reader))
          this.messageID = reader.ReadElementContentAsString();
        if (this.IfRequisite("CorrelationMessageID", reader))
          this.correlationMessageID = reader.ReadElementContentAsString();
        if (this.IfRequisite("MessageType", reader))
          this.messageType = int.Parse(reader.ReadElementContentAsString());
        if (this.IfRequisite("Priority", reader))
          this.priority = int.Parse(reader.ReadElementContentAsString());
        if (this.IfRequisite("LegacyTransportFileName", reader))
          this.legacyTransportFileName = reader.ReadElementContentAsString();
        if (this.IfRequisite("CreateTime", reader))
          this.createTime = new DateTime?(reader.ReadElementContentAsDateTime().ToLocalTime());
        if (this.IfRequisite("SendTime", reader))
          this.sendTime = new DateTime?(reader.ReadElementContentAsDateTime().ToLocalTime());
        if (this.IfRequisite("ReceiveTime", reader))
          this.receiveTime = new DateTime?(reader.ReadElementContentAsDateTime().ToLocalTime());
        if (this.IfRequisite("AcceptTime", reader))
          this.acceptTime = new DateTime?(reader.ReadElementContentAsDateTime().ToLocalTime());
        if (this.IfRequisite("AckRequest", reader))
          this.ackRequest = new bool?(reader.ReadElementContentAsBoolean());
        if (reader.NodeType != XmlNodeType.Element && reader.NodeType != XmlNodeType.EndElement)
          reader.Read();
      }
      while (reader.NodeType != XmlNodeType.EndElement || !(reader.LocalName == "MessageInfo") || !(reader.NamespaceURI == this.Name.NamespaceURI));
      reader.Read();
    }

    protected bool IfRequisite(string requisiteName, XmlReader reader)
    {
      return reader.NodeType == XmlNodeType.Element && reader.NamespaceURI == this.Name.NamespaceURI && reader.LocalName == requisiteName;
    }
  }
}

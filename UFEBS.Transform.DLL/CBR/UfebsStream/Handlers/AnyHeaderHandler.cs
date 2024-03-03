using CBR.UfebsStream.Headers;
using System;
using System.Collections.Generic;
using System.Xml;

namespace CBR.UfebsStream.Handlers
{
  public class AnyHeaderHandler : IKAHandler
  {
    private Dictionary<CBR.UfebsStream.QName, Type> headers;

    public AnyHeaderHandler() => this.headers = new Dictionary<CBR.UfebsStream.QName, Type>();

    public void Handle(XmlReader reader, ContentStorage content)
    {
      CBR.UfebsStream.QName key = new CBR.UfebsStream.QName(reader.LocalName, reader.NamespaceURI);
      Header instance = (Header) Activator.CreateInstance(!this.headers.ContainsKey(key) ? typeof (UnknownHeader) : this.headers[key]);
      instance.LoadFrom(reader);
      content.AddHeader(instance.Name, instance);
    }

    public void RegisterHeader(string localName, string namespaceUri, Type headerType)
    {
      if (!headerType.IsSubclassOf(typeof (Header)))
        throw new ArgumentException("Переданный тип не является подклассом типа Header");
      CBR.UfebsStream.QName key = new CBR.UfebsStream.QName(localName, namespaceUri);
      if (this.headers.ContainsKey(key))
        throw new ArgumentException(string.Format("Заголовок {0} уже зарегистрирован", (object) key));
      this.headers.Add(key, headerType);
    }

    public void RegisterStandartHeaders()
    {
      this.RegisterHeader("MessageInfo", "urn:cbr-ru:msg:props:v1.2", typeof (MessageInfo12));
      this.RegisterHeader("MessageInfo", "urn:cbr-ru:msg:props:v1.1", typeof (MessageInfo11));
      this.RegisterHeader("SequenceInfo", "urn:cbr-ru:msg:props:v1.2", typeof (SequenceInfo));
      this.RegisterHeader("AcknowledgementInfo", "urn:cbr-ru:msg:props:v1.2", typeof (AcknowledgementInfo12));
      this.RegisterHeader("AcknowledgementInfo", "urn:cbr-ru:msg:props:v1.1", typeof (AcknowledgementInfo11));
    }
  }
}

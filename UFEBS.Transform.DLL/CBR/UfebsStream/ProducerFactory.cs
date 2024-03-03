using CBR.UfebsStream.ED.Serialization;
using CBR.UfebsStream.Headers;
using CBR.UfebsStream.Producers;
using System.Collections.Generic;
using System.IO;

namespace CBR.UfebsStream
{
  public sealed class ProducerFactory : AbstractSingleton<ProducerFactory>
  {
    private ProducerFactory()
    {
    }

    public IXmlContentProducer CreateSoapEnvelopeProducer(
      string fileName,
      byte[] macValue,
      params Header[] headers)
    {
      return this.CreateSoapEnvelopeProducer((Stream) new FileStream(fileName, FileMode.Open, FileAccess.Read), macValue, headers);
    }

    public IXmlContentProducer CreateSoapEnvelopeProducer(
      string fileName,
      byte[] macValue,
      IEnumerable<Header> headers)
    {
      return this.CreateSoapEnvelopeProducer((Stream) new FileStream(fileName, FileMode.Open, FileAccess.Read), macValue, headers);
    }

    public IXmlContentProducer CreateSoapEnvelopeProducer(
      Stream content,
      byte[] macValue,
      params Header[] headers)
    {
      return (IXmlContentProducer) new SoapEnvelopeProducer((IXmlContentProducer) new HeadersProducer(headers), (IXmlContentProducer) new SigEnvelopeProducer((IXmlContentProducer) new MacValueProducer(macValue), (IBinaryContentProducer) new StreamBinaryProducer(content)));
    }

    public IXmlContentProducer CreateSoapEnvelopeProducer(
      Stream content,
      byte[] macValue,
      IEnumerable<Header> headers)
    {
      return (IXmlContentProducer) new SoapEnvelopeProducer((IXmlContentProducer) new HeadersProducer(headers), (IXmlContentProducer) new SigEnvelopeProducer((IXmlContentProducer) new MacValueProducer(macValue), (IBinaryContentProducer) new StreamBinaryProducer(content)));
    }

    public IXmlContentProducer CreateSoapEnvelopeProducer(
      object ed,
      byte[] macValue,
      EDSerializerBase serializer,
      params Header[] headers)
    {
      return (IXmlContentProducer) new SoapEnvelopeProducer((IXmlContentProducer) new HeadersProducer(headers), (IXmlContentProducer) new SigEnvelopeProducer((IXmlContentProducer) new MacValueProducer(macValue), (IBinaryContentProducer) new EDProducer(ed, serializer)));
    }

    public IXmlContentProducer CreateSoapEnvelopeProducer(
      object ed,
      byte[] macValue,
      EDSerializerBase serializer,
      IEnumerable<Header> headers)
    {
      return (IXmlContentProducer) new SoapEnvelopeProducer((IXmlContentProducer) new HeadersProducer(headers), (IXmlContentProducer) new SigEnvelopeProducer((IXmlContentProducer) new MacValueProducer(macValue), (IBinaryContentProducer) new EDProducer(ed, serializer)));
    }

    public IXmlContentProducer CreateSigEnvelopeProducer(Stream content, byte[] macValue)
    {
      return (IXmlContentProducer) new SigEnvelopeProducer((IXmlContentProducer) new MacValueProducer(macValue), (IBinaryContentProducer) new StreamBinaryProducer(content));
    }

    public IXmlContentProducer CreateSigEnvelopeProducer(string fileName, byte[] macValue)
    {
      return this.CreateSigEnvelopeProducer((Stream) new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite), macValue);
    }

    public IXmlContentProducer CreateSigEnvelopeProducer(
      object ed,
      byte[] macValue,
      EDSerializerBase serializer)
    {
      return (IXmlContentProducer) new SigEnvelopeProducer((IXmlContentProducer) new MacValueProducer(macValue), (IBinaryContentProducer) new EDProducer(ed, serializer));
    }

    public IXmlContentProducer CreateHeadersProducer(params Header[] headers)
    {
      return (IXmlContentProducer) new HeadersProducer(headers);
    }

    public IXmlContentProducer CreateHeadersProducer(IEnumerable<Header> headers)
    {
      return (IXmlContentProducer) new HeadersProducer(headers);
    }

    public IXmlContentProducer CreateReceiptProducer(
      MessageInfo12 messageInfo,
      AcknowledgementInfo12 acknowledgementInfo)
    {
      return (IXmlContentProducer) new SoapEnvelopeProducer((IXmlContentProducer) new HeadersProducer(new Header[2]
      {
        (Header) messageInfo,
        (Header) acknowledgementInfo
      }), (IXmlContentProducer) new EmptyProducer());
    }
  }
}

using System.Collections.Generic;
using System.Xml;

namespace CBR.UfebsStream.Producers
{
  public class CompositeXmlProducer : IXmlContentProducer
  {
    private List<IXmlContentProducer> producerList;

    public CompositeXmlProducer() => this.producerList = new List<IXmlContentProducer>();

    public CompositeXmlProducer(params IXmlContentProducer[] producers)
    {
      this.producerList = new List<IXmlContentProducer>();
      for (int index = 0; index < producers.Length; ++index)
        this.producerList.Add(producers[index]);
    }

    public void Add(IXmlContentProducer producer) => this.producerList.Add(producer);

    public void WriteTo(XmlWriter target)
    {
      foreach (IXmlContentProducer producer in this.producerList)
        producer.WriteTo(target);
    }
  }
}

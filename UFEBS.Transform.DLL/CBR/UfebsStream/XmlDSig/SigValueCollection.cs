using System.Collections.Generic;
using System.Xml;

namespace CBR.UfebsStream.XmlDSig
{
  public class SigValueCollection
  {
    private List<SigValue> sigValues;

    public List<SigValue> SigValues => this.sigValues;

    public SigValueCollection() => this.sigValues = new List<SigValue>();

    public SigValueCollection(params XmlElement[] elements)
    {
      this.sigValues = new List<SigValue>();
      if (elements == null)
        return;
      for (int index = 0; index < elements.Length; ++index)
        this.sigValues.Add(new SigValue(elements[index]));
      if (elements.Length <= 1)
        return;
      foreach (SigValue sigValue in this.sigValues)
      {
        if (sigValue.SigId == null)
          throw new SignatureException("При наличии более чем одного ЗК идентификаторы ЗК являются обязательными");
      }
    }
  }
}

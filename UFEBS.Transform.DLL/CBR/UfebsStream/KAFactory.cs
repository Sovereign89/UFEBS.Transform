using CBR.UfebsStream.Handlers;
using CBR.UfebsStream.StateMachine;

namespace CBR.UfebsStream
{
  public sealed class KAFactory : AbstractSingleton<KAFactory>
  {
    private KAFactory()
    {
    }

    public KA CreateSoapEnvelopeKA()
    {
      KA soapEnvelopeKa = new KA();
      soapEnvelopeKa.AddState(new KAState("beforeEnvelope"));
      soapEnvelopeKa.AddState(new KAState("beforeHeader"));
      soapEnvelopeKa.AddState(new KAState("anyHeader"));
      soapEnvelopeKa.AddState(new KAState("afterHeader"));
      soapEnvelopeKa.AddState(new KAState("afterBody"));
      soapEnvelopeKa.AddState(new KAState("afterSigEnvelope"));
      soapEnvelopeKa.AddState(new KAState("afterSigContainer"));
      soapEnvelopeKa.AddState(new KAState("anySigElement"));
      soapEnvelopeKa.AddState(new KAState("beforeObject"));
      soapEnvelopeKa.AddState(new KAState("afterObject"));
      soapEnvelopeKa.AddState(new KAState("afterEndSigEnvelope"));
      soapEnvelopeKa.AddState(new KAState("afterEndBody"));
      soapEnvelopeKa.AddState(new KAState("afterEndEnvelope", true));
      soapEnvelopeKa.SetInitialState("beforeEnvelope");
      AnyHeaderHandler handler1 = new AnyHeaderHandler();
      handler1.RegisterStandartHeaders();
      MacValueHandler handler2 = new MacValueHandler();
      ObjectHandler handler3 = new ObjectHandler();
      soapEnvelopeKa.AddRule(new Rule(new Condition("Envelope", "http://www.w3.org/2003/05/soap-envelope", NodeType.Start), "beforeEnvelope", "beforeHeader"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("Header", "http://www.w3.org/2003/05/soap-envelope", NodeType.Start), "beforeHeader", "anyHeader"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("Header", "http://www.w3.org/2003/05/soap-envelope", NodeType.Empty), "beforeHeader", "afterHeader"));
      soapEnvelopeKa.AddRule(new Rule(new Condition((string) null, (string) null, NodeType.Start), "anyHeader", "anyHeader", (IKAHandler) handler1));
      soapEnvelopeKa.AddRule(new Rule(new Condition("Header", "http://www.w3.org/2003/05/soap-envelope", NodeType.End), "anyHeader", "afterHeader"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("Body", "http://www.w3.org/2003/05/soap-envelope", NodeType.Start), "afterHeader", "afterBody"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("Body", "http://www.w3.org/2003/05/soap-envelope", NodeType.Empty), "beforeHeader", "afterEndBody"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("Body", "http://www.w3.org/2003/05/soap-envelope", NodeType.Empty), "afterHeader", "afterEndBody"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("Body", "http://www.w3.org/2003/05/soap-envelope", NodeType.Start), "beforeHeader", "afterBody"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("SigEnvelope", "urn:cbr-ru:dsig:env:v1.1", NodeType.Start), "afterBody", "afterSigEnvelope"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("Body", "http://www.w3.org/2003/05/soap-envelope", NodeType.End), "afterBody", "afterEndBody"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("SigContainer", "urn:cbr-ru:dsig:env:v1.1", NodeType.Start), "afterSigEnvelope", "anySigElement"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("MACValue", "urn:cbr-ru:dsig:v1.1", NodeType.Start), "anySigElement", "anySigElement", 10, (IKAHandler) handler2));
      soapEnvelopeKa.AddRule(new Rule(new Condition("MACValue", "urn:cbr-ru:dsig:v1.1", NodeType.Empty), "anySigElement", "anySigElement"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("SigContainer", "urn:cbr-ru:dsig:env:v1.1", NodeType.End), "anySigElement", "beforeObject"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("Object", "urn:cbr-ru:dsig:env:v1.1", NodeType.Start), "beforeObject", "afterObject", (IKAHandler) handler3));
      soapEnvelopeKa.AddRule(new Rule(new Condition("Object", "urn:cbr-ru:dsig:env:v1.1", NodeType.Empty), "beforeObject", "afterObject"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("Object", "urn:cbr-ru:dsig:env:v1.1", NodeType.End), "afterObject", "afterObject"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("SigEnvelope", "urn:cbr-ru:dsig:env:v1.1", NodeType.End), "afterObject", "afterSigEnvelope"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("Body", "http://www.w3.org/2003/05/soap-envelope", NodeType.End), "afterSigEnvelope", "afterEndBody"));
      soapEnvelopeKa.AddRule(new Rule(new Condition("Envelope", "http://www.w3.org/2003/05/soap-envelope", NodeType.End), "afterEndBody", "afterEndEnvelope"));
      return soapEnvelopeKa;
    }

    public KA CreateSigEnvelopeKA()
    {
      KA sigEnvelopeKa = new KA();
      sigEnvelopeKa.AddState(new KAState("beforeSigEnvelope"));
      sigEnvelopeKa.AddState(new KAState("afterSigEnvelope"));
      sigEnvelopeKa.AddState(new KAState("afterSigContainer"));
      sigEnvelopeKa.AddState(new KAState("anySigElement"));
      sigEnvelopeKa.AddState(new KAState("beforeObject"));
      sigEnvelopeKa.AddState(new KAState("afterObject"));
      sigEnvelopeKa.AddState(new KAState("afterEndSigEnvelope", true));
      sigEnvelopeKa.SetInitialState("beforeSigEnvelope");
      MacValueHandler handler1 = new MacValueHandler();
      ObjectHandler handler2 = new ObjectHandler();
      sigEnvelopeKa.AddRule(new Rule(new Condition("SigEnvelope", "urn:cbr-ru:dsig:env:v1.1", NodeType.Start), "beforeSigEnvelope", "afterSigEnvelope"));
      sigEnvelopeKa.AddRule(new Rule(new Condition("SigContainer", "urn:cbr-ru:dsig:env:v1.1", NodeType.Start), "afterSigEnvelope", "anySigElement"));
      sigEnvelopeKa.AddRule(new Rule(new Condition("MACValue", "urn:cbr-ru:dsig:v1.1", NodeType.Start), "anySigElement", "anySigElement", 10, (IKAHandler) handler1));
      sigEnvelopeKa.AddRule(new Rule(new Condition("MACValue", "urn:cbr-ru:dsig:v1.1", NodeType.Empty), "anySigElement", "anySigElement"));
      sigEnvelopeKa.AddRule(new Rule(new Condition("SigContainer", "urn:cbr-ru:dsig:env:v1.1", NodeType.End), "anySigElement", "beforeObject"));
      sigEnvelopeKa.AddRule(new Rule(new Condition("Object", "urn:cbr-ru:dsig:env:v1.1", NodeType.Start), "beforeObject", "afterObject", (IKAHandler) handler2));
      sigEnvelopeKa.AddRule(new Rule(new Condition("Object", "urn:cbr-ru:dsig:env:v1.1", NodeType.Empty), "beforeObject", "afterObject"));
      sigEnvelopeKa.AddRule(new Rule(new Condition("Object", "urn:cbr-ru:dsig:env:v1.1", NodeType.End), "afterObject", "afterObject"));
      sigEnvelopeKa.AddRule(new Rule(new Condition("SigEnvelope", "urn:cbr-ru:dsig:env:v1.1", NodeType.End), "afterObject", "afterEndSigEnvelope"));
      return sigEnvelopeKa;
    }

    public KA CreateHeadersKA()
    {
      KA headersKa = new KA();
      headersKa.AddState(new KAState("beforeHeader"));
      headersKa.AddState(new KAState("anyHeader"));
      headersKa.AddState(new KAState("afterHeader", true));
      headersKa.SetInitialState("beforeHeader");
      AnyHeaderHandler handler = new AnyHeaderHandler();
      handler.RegisterStandartHeaders();
      headersKa.AddRule(new Rule(new Condition("Header", "http://www.w3.org/2003/05/soap-envelope", NodeType.Start), "beforeHeader", "anyHeader"));
      headersKa.AddRule(new Rule(new Condition("Header", "http://www.w3.org/2003/05/soap-envelope", NodeType.Empty), "beforeHeader", "afterHeader"));
      headersKa.AddRule(new Rule(new Condition((string) null, (string) null, NodeType.Start), "anyHeader", "anyHeader", (IKAHandler) handler));
      headersKa.AddRule(new Rule(new Condition("Header", "http://www.w3.org/2003/05/soap-envelope", NodeType.End), "anyHeader", "afterHeader"));
      return headersKa;
    }
  }
}

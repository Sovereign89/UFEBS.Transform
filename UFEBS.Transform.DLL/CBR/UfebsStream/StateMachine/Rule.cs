using CBR.UfebsStream.Handlers;
using System.Xml;

namespace CBR.UfebsStream.StateMachine
{
  public class Rule
  {
    private string fromState;
    private string toState;
    private Condition condition;
    private int priority;
    private IKAHandler handler;

    public string FromState => this.fromState;

    public string ToState => this.toState;

    public Condition Condition => this.condition;

    public int Priority => this.priority;

    public IKAHandler Handler
    {
      get => this.handler;
      set => this.handler = value;
    }

    public Rule(Condition condition, string fromState, string toState)
      : this(condition, fromState, toState, 0)
    {
    }

    public Rule(Condition condition, string fromState, string toState, int priority)
      : this(condition, fromState, toState, priority, (IKAHandler) null)
    {
    }

    public Rule(Condition condition, string fromState, string toState, IKAHandler handler)
      : this(condition, fromState, toState, 0, handler)
    {
    }

    public Rule(
      Condition condition,
      string fromState,
      string toState,
      int priority,
      IKAHandler handler)
    {
      this.condition = condition;
      this.fromState = fromState;
      this.toState = toState;
      this.priority = priority;
      this.handler = handler;
    }

    public void ApplyHandlers(XmlReader reader, ContentStorage content)
    {
      if (this.handler != null)
        this.handler.Handle(reader, content);
      else
        reader.Read();
    }
  }
}

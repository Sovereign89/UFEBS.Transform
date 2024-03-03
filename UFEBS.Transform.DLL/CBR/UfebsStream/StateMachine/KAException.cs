using System;

namespace CBR.UfebsStream.StateMachine
{
  public class KAException : Exception
  {
    private KAState state;

    public KAException(string message)
      : base(message)
    {
      this.state = (KAState) null;
    }

    public KAException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.state = (KAState) null;
    }

    public KAException(string message, KAState state)
      : base(message)
    {
      this.state = state;
    }

    public KAException(string message, KAState state, Exception innerException)
      : base(message, innerException)
    {
      this.state = state;
    }

    public override string ToString()
    {
      return this.state == null ? base.ToString() : "Состояние " + this.state.Description + "\n" + base.ToString();
    }
  }
}

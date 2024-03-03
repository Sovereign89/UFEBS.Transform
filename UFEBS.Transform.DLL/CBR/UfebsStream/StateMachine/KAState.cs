namespace CBR.UfebsStream.StateMachine
{
  public class KAState
  {
    private string id;
    private string description;
    private bool isEnd;

    public string ID
    {
      get => this.id;
      set => this.id = value;
    }

    public string Description
    {
      get => this.description;
      set => this.description = value;
    }

    public bool IsEnd
    {
      get => this.isEnd;
      set => this.isEnd = value;
    }

    public KAState(string id)
    {
      this.id = id;
      this.description = "Состояние " + id;
      this.isEnd = false;
    }

    public KAState(string id, bool isEnd)
    {
      this.id = id;
      this.description = "Состояние " + id;
      this.isEnd = isEnd;
    }

    public KAState(string id, string description)
    {
      this.id = id;
      this.description = description;
      this.isEnd = false;
    }

    public KAState(string id, string description, bool isEnd)
    {
      this.id = id;
      this.description = description;
      this.isEnd = isEnd;
    }
  }
}

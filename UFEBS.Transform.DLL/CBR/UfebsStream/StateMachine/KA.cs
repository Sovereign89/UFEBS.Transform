using Normalizer.Properties;
using System.Collections.Generic;
using System.Xml;

namespace CBR.UfebsStream.StateMachine
{
  public class KA
  {
    private Dictionary<string, KAState> states;
    private KAState current;
    private List<Rule> rules;

    public KAState Current
    {
      get => this.current;
      set => this.current = value;
    }

    public KAState this[string id]
    {
      get => this.states.ContainsKey(id) ? this.states[id] : (KAState) null;
    }

    public List<Rule> Rules => this.rules;

    internal KA()
    {
      this.states = new Dictionary<string, KAState>();
      this.rules = new List<Rule>();
      this.current = (KAState) null;
    }

    public void AddState(KAState state)
    {
      if (this.states.ContainsKey(state.ID))
        throw new KAException(string.Format("Множество состояний уже содержит элемент с идентификатором {0}", (object) state.ID));
      this.states.Add(state.ID, state);
    }

    public void AddRule(Rule rule)
    {
      if (!this.states.ContainsKey(rule.FromState))
        throw new KAException(string.Format("Множество состояний не содержит состояния {0}", (object) rule.FromState));
      if (!this.states.ContainsKey(rule.ToState))
        throw new KAException(string.Format("Множество состояний не содержит состояния {0}", (object) rule.ToState));
      this.rules.Add(rule);
    }

    public void SetInitialState(string id)
    {
      this.current = this.states.ContainsKey(id) ? this.states[id] : throw new KAException(string.Format("Множество состояний не содержит состояния {0}", (object) id));
    }

    public void ProcessReader(XmlReader reader, ContentStorage content)
    {
      if (this.current == null)
        throw new KAException("Не задано текущее состояние автомата");
      while (true)
      {
        while (reader.EOF || reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
        {
          if (!reader.EOF)
          {
            Rule rule = this.FindRule(this.current, reader);
            rule.ApplyHandlers(reader, content);
            this.current = this.states[rule.ToState];
          }
          else
          {
            if (this.current.IsEnd)
              return;
            throw new KAException("После чтения всего документа автомат оказался не в конечном состоянии", this.current);
          }
        }
        reader.Read();
      }
    }

    private Rule FindRule(KAState currentState, XmlReader reader)
    {
      List<Rule> ruleList = new List<Rule>();
      int num = int.MinValue;
      foreach (Rule rule in this.rules)
      {
        if (currentState.ID == rule.FromState && rule.Condition.IsMatch(reader))
        {
          if (rule.Priority > num)
          {
            ruleList.Clear();
            ruleList.Add(rule);
            num = rule.Priority;
          }
          else if (rule.Priority == num)
            ruleList.Add(rule);
        }
      }
      if (ruleList.Count == 1)
        return ruleList[0];
      if (ruleList.Count > 1)
        throw new KAException("Найдено более одного правила одинакового приоритета", currentState);
      throw new KAException(Resources.NoRulesFound, currentState);
    }
  }
}

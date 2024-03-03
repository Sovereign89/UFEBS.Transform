using System;
using System.Reflection;

namespace CBR.UfebsStream
{
  public class AbstractSingleton<T>
  {
    private static T instance;

    protected AbstractSingleton()
    {
    }

    public static T GetInstance()
    {
      if ((object) AbstractSingleton<T>.instance == null)
      {
        ConstructorInfo constructor = typeof (T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new Type[0], new ParameterModifier[0]);
        AbstractSingleton<T>.instance = !(constructor == (ConstructorInfo) null) ? (T) constructor.Invoke(new object[0]) : throw new ApplicationException(string.Format("Класс {0} не имеет закрытого конструктора без параметров", (object) typeof (T).FullName));
      }
      return AbstractSingleton<T>.instance;
    }
  }
}

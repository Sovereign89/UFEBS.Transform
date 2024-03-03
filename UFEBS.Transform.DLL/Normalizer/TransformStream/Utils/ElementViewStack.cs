using System.Collections;
using System.ComponentModel;

namespace Normalizer.TransformStream.Utils
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class ElementViewStack
  {
    private Stack stack;

    public ElementViewStack() => this.stack = new Stack();

    public void Push(IElementView item) => this.stack.Push((object) item);

    public void Pop() => this.stack.Pop();

    public IElementView Top() => (IElementView) this.stack.Peek();
  }
}

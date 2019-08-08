using System.Collections;
using System.Collections.Generic;

namespace Iterator
{
  public class ConcatEnumerable<TSource> : IIteratable<TSource>, IEnumerator<TSource>
  {
    private readonly IIteratable<TSource>[] iteratables;
    private TSource current;
    private int state = 0;
    private int currentIteratable = 0;
    private bool moveNext = false;

    public ConcatEnumerable(IIteratable<TSource> first, IIteratable<TSource> second)
    {
      iteratables = new IIteratable<TSource>[2];
      iteratables[0] = first;
      iteratables[1] = second;
    }

    public TSource Current { get{ return current; } }
    object IEnumerator.Current { get { return Current; } }

    public IEnumerator<TSource> GetEnumerator()
    {
      return this;
    }
    public bool MoveNext()
    {
      switch (state)
      {
        case 0:
          Reset();
          moveNext = iteratables[currentIteratable].GetEnumerator().MoveNext();
          state = 1;
          goto case 1;
        case 1:
          current = iteratables[currentIteratable].GetEnumerator().Current;
          if (currentIteratable == 0 && !moveNext)
          {
            currentIteratable = 1;
            goto case 2;
          }
          state = 2;
          if (!moveNext)
            state = 0;
          return moveNext;
        case 2:
          moveNext = iteratables[currentIteratable].GetEnumerator().MoveNext();
          goto case 1;
      }
      return false;
    }
    public void Dispose(){}
    public void Reset()
    {
      iteratables[0].GetEnumerator().Reset();
      iteratables[1].GetEnumerator().Reset();
      currentIteratable = 0;
    }
  }

}

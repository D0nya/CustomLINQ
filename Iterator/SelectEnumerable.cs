using System;
using System.Collections;
using System.Collections.Generic;

namespace Iterator
{
  public class SelectEnumerable<TSource, TRetrun>: IIteratable<TRetrun>, IEnumerator<TRetrun>
  {
    private readonly IIteratable<TSource> source;
    private readonly Func<TSource, TRetrun> selector;
    private TRetrun current;
    private bool moveNext = false;
    private int state = 0;

    public SelectEnumerable(IIteratable<TSource> source, Func<TSource, TRetrun> selector)
    {
      this.source = source;
      this.selector = selector;
    }

    public TRetrun Current { get { return current; } }
    object IEnumerator.Current { get { return Current; } }

    public IEnumerator<TRetrun> GetEnumerator()
    {
      return this;
    }
    public bool MoveNext()
    {
      switch (state)
      {
        case 0:
          Reset();
          moveNext = source.GetEnumerator().MoveNext();
          state = 1;
          goto case 1;
        case 1:
          current = selector(source.GetEnumerator().Current);
          state = 2;
          if (!moveNext)
            state = 0;
          return moveNext;
        case 2:
          moveNext = source.GetEnumerator().MoveNext();
          goto case 1;
      }
      return false;
    }
    public void Reset()
    {
      source.GetEnumerator().Reset();

    }
    public void Dispose(){}
  }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace Iterator
{
  public class MapEnumerable<TSource> : IIteratable<TSource>, IEnumerator<TSource>
  {
    private readonly IIteratable<TSource>  source;
    private readonly Func<TSource, TSource> func;
    private TSource current;
    private bool moveNext = true;
    private int state = 0;

    public MapEnumerable(IIteratable<TSource> source, Func<TSource, TSource> func)
    {
      this.source = source;
      this.func = func;
    }

    public TSource Current { get { return current; } }
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
          moveNext = source.GetEnumerator().MoveNext();
          state = 1;
          goto case 1;
        case 1:
          current = source.GetEnumerator().Current;
          current = func(current);
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
    public void Dispose(){}
    public void Reset()
    {
      source.GetEnumerator().Reset();
    }
  }
}

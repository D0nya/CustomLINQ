using System.Collections;
using System.Collections.Generic;

namespace Iterator
{
  public class CustomList<T> : IIteratable<T>, IEnumerator<T>
  {
    private List<T> list;
    private T current;
    private int state = 0;
    private int i;

    public CustomList()
    {
      list = new List<T>();
    }
    public int Count { get { return list.Count; } }
    public T Current { get { return current; } }
    object IEnumerator.Current { get { return current; } }

    public T this[int index]
    {
      get { return list[index]; }
      set { list[index] = value; }
    }
    public void AddRange(IEnumerable<T> array)
    {
      foreach (T item in array)
      {
        list.Add(item);
      }
    }
    public List<T> GetItems()
    {
      return list;
    }
    public void Add(T item)
    {
      list.Add(item);
    }
    public void Clear()
    {
      list.Clear();
    }
    public IEnumerator<T> GetEnumerator()
    {
      return this;
    }

    public bool MoveNext()
    {
      switch(state)
      {
        case 0:
          i = 0;
          state = 1;
          goto case 1;
        case 1:
          if (!(i < Count))
          {
            Reset();
            return false;
          }
          current = list[i];
          state = 2;
          return true;
        case 2:
          ++i;
          goto case 1;
      }
      return false;
    }

    public void Reset()
    {
      state = 0;
      i = 0;
      current = list[i];
    }
    public void Dispose()
    {
    }
  }
}

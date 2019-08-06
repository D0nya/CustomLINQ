using System.Collections.Generic;

namespace Iterator
{
  public class CustomList<T> : IIteratable<T>
  {
    private List<T> list;
    public CustomList()
    {
      list = new List<T>();
    }
    public void Add(T item)
    {
      list.Add(item);
    }
    public List<T> GetItems()
    {
      return list;
    }
    public void Clear()
    {
      list.Clear();
    }
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
    public IEnumerator<T> GetEnumerator()
    {
      return list.GetEnumerator();
    }
  }
}

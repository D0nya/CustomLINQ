using System;

namespace Iterator
{
  public static class IteratorExtensions 
  {
    public static IIteratable<TSource> Where<TSource>(this IIteratable<TSource> source, Func<TSource, bool> predicate)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      var ret = new WhereEnumerable<TSource>(source, predicate);
      return ret;
    }

    public static IIteratable<TSource> Map<TSource>(this IIteratable<TSource> source, Func<TSource, TSource> func)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      var ret = new MapEnumerable<TSource>(source, func);
      return ret;
    }

    public static int Count<TSource>(this IIteratable<TSource> source)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      int cnt = 0;
      foreach (TSource item in source)
      {
        cnt++;
      }
      return cnt;
    }

    public static bool Some<TSource>(this IIteratable<TSource> source, Func<TSource, bool> predicate)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      foreach (TSource item in source)
      {
        if (predicate(item))
          return true;
      }
      return false;
    }

    public static bool All<TSource>(this IIteratable<TSource> source, Func<TSource, bool> predicate)
    {
      if (source == null)
        throw new ArgumentNullException("source");

      foreach (TSource item in source)
      {
        if (!predicate(item))
          return false;
      }
      return true;
    }

    public static TSource[] ToArray<TSource>(this IIteratable<TSource> source)
    {
      TSource[] ret = new TSource[source.Count()];
      int i = 0;
      foreach (TSource item in source)
      {
        ret[i] = item;
        i++;
      }
      return ret;
    }

    public static TSource FirstOrDefault<TSource>(this IIteratable<TSource> source, Func<TSource, bool> predicate)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      foreach (TSource item in source)
      {
        if (predicate(item))
          return item;
      }
      return default;
    }

    public static IIteratable<TSource> Concat<TSource>(this IIteratable<TSource> first, IIteratable<TSource> second)
    {
      if (first == null)
        throw new ArgumentNullException("first");
      if (second == null)
        throw new ArgumentNullException("second");

      var ret = new ConcatEnumerable<TSource>(first, second);
      return ret;
    }

    public static IIteratable<TReturn> Select<TSource, TReturn>(this IIteratable<TSource> source, Func<TSource, TReturn> selector)
    {
      if (source == null)
        throw new ArgumentNullException("source");

      var ret = new SelectEnumerable<TSource, TReturn>(source, selector);

      return ret;
    }
  }
}

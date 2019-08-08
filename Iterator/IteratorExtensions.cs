using System;

namespace Iterator
{
  public static class IteratorExtensions 
  {
    /// <summary>
    /// Filters objects depending on <paramref name="predicate"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IIteratable<TSource> Where<TSource>(this IIteratable<TSource> source, Func<TSource, bool> predicate)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      var ret = new WhereEnumerable<TSource>(source, predicate);
      return ret;
    }
    /// <summary>
    /// Uses <paramref name="func"/> on every object in <paramref name="source"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IIteratable<TSource> Map<TSource>(this IIteratable<TSource> source, Func<TSource, TSource> func)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      var ret = new MapEnumerable<TSource>(source, func);
      return ret;
    }
    /// <summary>
    /// Returns number of objects in <paramref name="source"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Returns true if at least one object in <paramref name="source"/> satisfies the <paramref name="predicate"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Returns true if all object satisfies the <paramref name="predicate"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Conerts <typeparamref name="IIteratable"/> <paramref name="source"/> to <typeparamref name="TSource"/> array
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Returns first object in <paramref name="source"/> which satisfies the <paramref name="predicate"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Concatenates <paramref name="first"/> and <paramref name="second"/> IIteratables and returns new one
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    public static IIteratable<TSource> Concat<TSource>(this IIteratable<TSource> first, IIteratable<TSource> second)
    {
      if (first == null)
        throw new ArgumentNullException("first");
      if (second == null)
        throw new ArgumentNullException("second");

      var ret = new ConcatEnumerable<TSource>(first, second);
      return ret;
    }
    /// <summary>
    /// Returns new <typeparamref name="IIteratable"/> of <typeparamref name="TReturn"/> objects depending on <paramref name="selector"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TReturn">Type of selected objects</typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IIteratable<TReturn> Select<TSource, TReturn>(this IIteratable<TSource> source, Func<TSource, TReturn> selector)
    {
      if (source == null)
        throw new ArgumentNullException("source");

      var ret = new SelectEnumerable<TSource, TReturn>(source, selector);

      return ret;
    }
  }
}

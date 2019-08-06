using System;

namespace Iterator
{
  static class IteratorExtensions 
  {
    /// <summary>
    /// Returns the first value match the predicate or default if nothing found
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TSource FirstOrDefault<TSource>(this IIteratable<TSource> source, Func<TSource, bool> predicate)
    {
      if (source == null)
        return default;
      foreach (TSource item in source)
      {
        if (predicate(item))
          return item;
      }
      return default;
    }
    /// <summary>
    /// Returns IIteratable with elements, match the predicate
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IIteratable<TSource> Where<TSource>(this IIteratable<TSource> source, Func<TSource, bool> predicate)
    {
      if (source == null)
        return null;
      CustomList<TSource> result = new CustomList<TSource>();
      foreach (TSource item in source)
      {
        if (predicate(item))
          result.Add(item);
      }
      return result;
    }
    /// <summary>
    /// Returns IIteratable with elements, processed by the predicate
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IIteratable<TSource> Map<TSource>(this IIteratable<TSource> source, Func<TSource, TSource> predicate)
    {
      if (source == null)
        return null;
      CustomList<TSource> result = new CustomList<TSource>();
      foreach (TSource item in source)
      {
        result.Add(predicate(item));
      }
      return result;
    }
    /// <summary>
    /// Returns true, if there any elements match the predicate
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool Some<TSource>(this IIteratable<TSource> source, Func<TSource, bool> predicate)
    {
      if (source == null)
        return false;
      foreach (TSource item in source)
      {
        if (predicate(item))
          return true;
      }
      return false;
    }
    /// <summary>
    /// Returns true, if all elements match the predicate
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool All<TSource>(this IIteratable<TSource> source, Func<TSource, bool> predicate)
    {
      if (source == null)
        return true;
      foreach (TSource item in source)
      {
        if (!predicate(item))
          return false;
      }
      return true;
    }
    /// <summary>
    /// Returns number of elements in IITeratable
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int Count<TSource>(this IIteratable<TSource> source)
    {
      if (source == null)
        return 0;
      int count = 0;
      foreach (var item in source)
      {
        count++;
      }
      return count;
    }
    /// <summary>
    /// Concatenates two sequences
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    public static IIteratable<TSource> Concat<TSource>(this IIteratable<TSource> first, IIteratable<TSource> second)
    {
      if (first == null || second == null)
        throw new ArgumentNullException();
      CustomList<TSource> result = new CustomList<TSource>();
      foreach (TSource item in first)
      {
        result.Add(item);
      }
      foreach (TSource item in second)
      {
        result.Add(item);
      }
      return result;
    }
    /// <summary>
    /// Returns IIteratable with items selected by selector
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IIteratable<TResult> Select<TSource, TResult>(this IIteratable<TSource> source, Func<TSource, TResult> selector)
    {
      if (source == null)
        return null;
      CustomList<TResult> result = new CustomList<TResult>();
      foreach (var item in source)
      {
        result.Add(selector(item));
      }
      return result;
    }
    /// <summary>
    /// Converts IITeratable to <typeparamref name="TSource"/>[] array.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TSource[] ToArray<TSource>(this IIteratable<TSource> source)
    {
      if (source == null)
        return null;
      TSource[] array = new TSource[source.Count()];
      int i = 0;
      foreach (TSource item in source)
      {
        array[i] = item;
        i++;
      }
      return array;
    }
    /// <summary>
    /// Returns IIteratable ordered by selected key
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <returns></returns>
    public static IIteratable<TSource> OrderBy<TSource, TKey>(this IIteratable<TSource> source, Func<TSource, TKey> keySelector)
    {
      if (source == null)
        return null;

      var items = source.ToArray();
      var keys = source.Select(keySelector).ToArray();
      Array.Sort(keys, items);

      CustomList<TSource> result = new CustomList<TSource>();
      foreach (var item in items)
      {
        result.Add(item);
      }
      return result;
    }
  }
}

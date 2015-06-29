using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqExtensions
{
  public static class LinqExtensions
  {
    public static bool None<TSource>(this IEnumerable<TSource> source)
    {
      return !source.Any();
    }

    public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return !source.Any(predicate);
    }

    public static bool ContainsExactly<TSource>(this IEnumerable<TSource> source, int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("count");
      if (source == null)
        throw new ArgumentNullException("source");
      using (var e = source.GetEnumerator())
      {
        for (int i = 0; i < count; i++)
        {
          if (!e.MoveNext())
            return false;
        }
        return !e.MoveNext();
      }
    }

    public static bool ContainsExactly<TSource>(this IEnumerable<TSource> source, int count, Func<TSource, bool> predicate)
    {
      return source.Where(predicate).ContainsExactly(count);
    }

    public static bool ContainsMoreThan<TSource>(this IEnumerable<TSource> source, int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("elementCount");
      if (source == null)
        throw new ArgumentNullException("source");
      return source.Skip(count).Any();
    }

    public static bool ContainsMoreThan<TSource>(this IEnumerable<TSource> source, int count, Func<TSource, bool> predicate)
    {
      return source.Where(predicate).ContainsMoreThan(count);
    }

    public static IEnumerable<T> NullToEmpty<T>(this IEnumerable<T> source)
    {
      return source ?? EmptyEnumerable<T>.Instance;
    }

    internal class EmptyEnumerable<TElement>
    {
      static TElement[] instance;

      public static IEnumerable<TElement> Instance
      {
        get { return instance ?? (instance = new TElement[0]); }
      }
    }

  }
}

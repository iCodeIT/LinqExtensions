using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExtensions
{
  public static class LinqExtensions
  {
    public static bool None<TSource>(this IEnumerable<TSource> source) => !source.Any(); 

    public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => !source.Any(predicate);

    public static bool ContainsExactly<TSource>(this IEnumerable<TSource> source, int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof(count));
      if (source == null)
        throw new ArgumentNullException(nameof(source));
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
      => source.Where(predicate).ContainsExactly(count);

    public static bool ContainsMoreThan<TSource>(this IEnumerable<TSource> source, int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof(count));
      if (source == null)
        throw new ArgumentNullException(nameof(source));
      return source.Skip(count).Any();
    }

   public static bool ContainsMoreThan<TSource>(this IEnumerable<TSource> source, int count, Func<TSource, bool> predicate) =>
      source.Where(predicate).ContainsMoreThan(count);

    public static IEnumerable<T> NullToEmpty<T>(this IEnumerable<T> source) => source ?? EmptyEnumerable<T>.Instance;

    private static class EmptyEnumerable<TElement>
    {
      static TElement[] instance;

      public static IEnumerable<TElement> Instance => instance ?? (instance = new TElement[0]);
    }

    public static bool In<T>(this T element, IEnumerable<T> collection) => collection.Contains(element);

    public static TSource MinBy<TSource, TBy>(this IEnumerable<TSource> source, Func<TSource, TBy> selector)
    {
      return MinBy(source, selector, null);
    }

    public static TSource MinBy<TSource, TBy>(this IEnumerable<TSource> source, Func<TSource, TBy> selector, IComparer<TBy> comparer)
    {
      comparer = comparer ?? Comparer<TBy>.Default;
      return source.Aggregate((x, y) => comparer.Compare(selector(x), selector(y)) < 0 ? x : y);
    }

    public static TSource MaxBy<TSource, TBy>(this IEnumerable<TSource> source, Func<TSource, TBy> selector)
    {
      return MaxBy(source, selector, null);
    }

    public static TSource MaxBy<TSource, TBy>(this IEnumerable<TSource> source, Func<TSource, TBy> selector, IComparer<TBy> comparer)
    {
      comparer = comparer ?? Comparer<TBy>.Default;
      return source.Aggregate((x, y) => comparer.Compare(selector(x), selector(y)) > 0 ? x : y);
    }
  }
}

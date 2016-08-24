using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LinqExtensions
{
  public static class LinqDumper
  {
    public static bool ShowWhiteSpace { get; set; }

    public static IEnumerable<T> Dump<T>(this IEnumerable<T> input, bool enumerateAtOnce = true, string dumpName = "")
    {
      return Dump(input, item => item != null ? item.ToString() : "(null)", enumerateAtOnce, dumpName);
    }

    public static IEnumerable<T> Dump<T>(this IEnumerable<T> input, Func<T, string> toString, bool enumerateAtOnce = true, string dumpName = "")
    {
      if(enumerateAtOnce)
        return InternDump(input, toString, dumpName).ToList();
      return InternDump(input, toString, dumpName);
    }

    private static IEnumerable<T> InternDump<T>(this IEnumerable<T> input, Func<T, string> toString, string dumpName)
    {
      Debug.WriteLine("Dump:" + dumpName);
      foreach (T item in input)
      {
        Debug.WriteLine(dumpName + ": " + (ShowWhiteSpace ? '[' + toString(item) + ']' : toString(item)));
        yield return item;
      }
    }
  }
}

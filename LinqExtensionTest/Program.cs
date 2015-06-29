using System;
using System.Diagnostics;
using System.Linq;

using LinqExtensions;

namespace LinqExtensionTest
{
  // See output in debug output
  class Program
  {
    static void Main(string[] args)
    {
      var numbers = new[] { 2, 1, 5, 6, 9, 3, 7, 8, 4 };
      var evenNumbersAbove3EnumerateAtOnce = numbers.Dump(true, "numbers")
        .Where(x => x % 2 == 0).Dump(true, "Even")
        .Where(x => x > 3).Dump(true, "gt3")
        .Distinct().Dump(true, "Distinct")
        .OrderBy(x => x).Dump(true, "Ordered");

      Debug.WriteLine("------------------------------");

      var evenNumbersAbove3LazyEval = numbers.Dump(false, "numbers")
        .Where(x => x % 2 == 0).Dump(false, "Even")
        .Where(x => x > 3).Dump(false, "gt3")
        .Distinct().Dump(false, "Distinct")
        .OrderBy(x => x).Dump(false, "Ordered").ToList();

      Console.ReadKey();
    }
  }
}

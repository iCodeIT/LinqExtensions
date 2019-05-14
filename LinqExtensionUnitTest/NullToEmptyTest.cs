using System;
using System.Collections.Generic;
using System.Linq;
using LinqExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqExtensionUnitTest
{
  [TestClass]
  public class NullToEmptyTest
  {
    [TestMethod]
    public void Should_ReturnCollection_When_NotNull()
    {
      var collection = new List<int> { 1, 2, 3 };

      var result = collection.NullToEmpty();

      Assert.AreEqual(3, result.Count());
    }

    [TestMethod]
    public void Should_ReturnEmptyCollection_When_Null()
    {
      List<int> collection = null;

      var result = collection.NullToEmpty();

      Assert.IsNotNull(result);
      Assert.IsInstanceOfType(result, typeof(IEnumerable<int>));
      Assert.AreEqual(0, result.Count());
    }
  }
}

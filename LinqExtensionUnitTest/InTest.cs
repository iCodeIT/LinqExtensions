using System;
using LinqExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqExtensionUnitTest
{
  [TestClass]
  public class InTest
  {
    [TestMethod]
    public void Should_ReturnFalse_WhenObjectIsNotInCollection()
    {
      Assert.IsFalse(1.In(new[] { 2, 3, 4 }));
    }

    [TestMethod]
    public void Should_ReturnTrue_WhenObjectIsInCollection()
    {
      Assert.IsTrue(1.In(new[] { 1, 2, 3, 4 }));
    }
  }
}

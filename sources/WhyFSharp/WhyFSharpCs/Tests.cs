using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace WhyFSharpCs {
  [TestFixture]
  public class Tests {
    [Test]
    public void QuickSort() {
      var sorted = QuickSortHelper.QuickSort(new List<int> { 1, 5, 23, 18, 9, 1, 3 });

      Assert.AreEqual(new List<int> { 1, 1, 3, 5, 9, 18, 23}, sorted);
    }

    [Test]
    public void QuickSort2() {
      var sorted = QuickSortHelper2.QuickSort(new List<int> { 1, 5, 23, 18, 9, 1, 3 });

      Assert.AreEqual(new List<int> { 1, 1, 3, 5, 9, 18, 23 }, sorted);
    }
  }
}

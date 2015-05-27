using System;
using System.Collections.Generic;
using System.Linq;

public class QuickSortHelper2 {
  public static List<T> QuickSort<T>(List<T> values) where T : IComparable {
    if (!values.Any()) {
      return new List<T>();
    }

    var x = values.First();
    var xs = values.Skip(1);
    var smaller = xs.Where(a => a.CompareTo(x) < 0).ToList();
    var larger = xs.Where(a => a.CompareTo(x) >= 0).ToList();

    return QuickSort(smaller)
      .Concat(new[] { x })
      .Concat(QuickSort(larger))
      .ToList();
  }
}
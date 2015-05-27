using System;
using System.Collections.Generic;
using System.Linq;

public class QuickSortHelper {
  public static List<T> QuickSort<T>(List<T> values) where T : IComparable {
    if (values.Count == 0) {
      return new List<T>();
    }

    T firstElement = values[0];

    var smallerElements = new List<T>();
    var largerElements = new List<T>();
    for (int i = 1; i < values.Count; i++) {
      var elem = values[i];
      if (elem.CompareTo(firstElement) < 0) {
        smallerElements.Add(elem);
      }
      else {
        largerElements.Add(elem);
      }
    }

    var result = new List<T>();
    result.AddRange(QuickSort(smallerElements.ToList()));
    result.Add(firstElement);
    result.AddRange(QuickSort(largerElements.ToList()));
    return result;
  }
}
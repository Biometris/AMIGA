using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Helpers {
    public static class ListExtensionMethods {
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source, int n) {
            if (n == 0)
                yield return Enumerable.Empty<T>();


            int count = 1;
            foreach (T item in source) {
                foreach (var innerSequence in source.Skip(count).Combinations(n - 1)) {
                    yield return new T[] { item }.Concat(innerSequence);
                }
                count++;
            }
        }
    }
}

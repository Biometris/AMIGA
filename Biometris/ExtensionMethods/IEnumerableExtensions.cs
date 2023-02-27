using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biometris.ExtensionMethods {
    public static class IEnumerableExtensions {

        /// <summary>
        /// Returns all n-element unique combinations of elements of the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source, int n) {
            if (n == 0) {
                yield return Enumerable.Empty<T>();
            }
            int count = 1;
            foreach (T item in source) {
                foreach (var innerSequence in source.Skip(count).Combinations(n - 1)) {
                    yield return new T[] { item }.Concat(innerSequence);
                }
                count++;
            }
        }

        /// <summary>
        /// Alternative zip-operation that applies the specified operation item-wise to the elements
        /// of both lists. If the two lists are of unequal size, then it continues until the last
        /// element of the longest list is processed and returns the items of the longest list as
        /// a result of the operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static IEnumerable<T> Merge<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, T> operation) {
            using (var iter1 = first.GetEnumerator())
            using (var iter2 = second.GetEnumerator()) {
                while (iter1.MoveNext()) {
                    if (iter2.MoveNext()) {
                        yield return operation(iter1.Current, iter2.Current);
                    } else {
                        yield return iter1.Current;
                    }
                }
                while (iter2.MoveNext()) {
                    yield return iter2.Current;
                }
            }
        }

        /// <summary>
        /// A first or default implementation in which the default
        /// value can be given as argument.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="alternate"></param>
        /// <returns></returns>
        public static T FirstOr<T>(this IEnumerable<T> source, T alternate) {
            foreach (T t in source)
                return t;
            return alternate;
        }

        /// <summary>
        /// Prints the elements of the list as a table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col"></param>
        public static void Print<T>(this IEnumerable<T> col) {
            var sb = new StringBuilder();
            var propertyInfos = typeof(T).GetProperties();
            foreach (var info in propertyInfos) {
                sb.Append(info.Name + "\t");
            }
            sb.AppendLine();
            foreach (var item in col) {
                foreach (var info in propertyInfos) {
                    var value = info.GetValue(item, null) ?? "(null)";
                    sb.Append(value.ToString() + "\t");
                }
                sb.AppendLine();
            }
            System.Diagnostics.Debug.WriteLine(sb.ToString());
        }
    }
}

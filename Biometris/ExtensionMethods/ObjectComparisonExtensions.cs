using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Biometris.ExtensionMethods {
    public static class ObjectComparisonExtensions {

        /// <summary>
        /// Compares the publec properties of the instance with another instance and
        /// returns true if these properties are equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="to"></param>
        /// <param name="ignore"></param>
        /// <returns></returns>
        public static bool PublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore) where T : class {
            if (self != null && to != null) {
                var type = typeof(T);
                var ignoreList = new List<string>(ignore);
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => !typeof(IEnumerable).IsAssignableFrom(p.PropertyType))
                    .ToList();
                foreach (var pi in properties) {
                    if (!ignoreList.Contains(pi.Name)) {
                        var selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        var toValue = type.GetProperty(pi.Name).GetValue(to, null);
                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))) {
                            return false;
                        }
                    }
                }
                return true;
            }
            return self == to;
        }
    }
}

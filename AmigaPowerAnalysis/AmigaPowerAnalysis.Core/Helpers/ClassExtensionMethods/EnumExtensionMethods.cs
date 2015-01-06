using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AmigaPowerAnalysis.Helpers.ClassExtensionMethods {
    public static class EnumExtensionMethods {

        /// <summary>
        /// Returns the flags of a flagged enumerable.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Enum> GetFlags(this Enum type) {
            foreach (System.Enum value in System.Enum.GetValues(type.GetType())) {
                if (type.HasFlag(value) && Convert.ToInt64(value) != 0) {
                    yield return value;
                }
            }
        }

        /// <summary>
        /// Sets a flag for the enum type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="flag"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public static T SetFlag<T>(this Enum value, T flag, bool set) {
            Type underlyingType = Enum.GetUnderlyingType(value.GetType());
            dynamic valueAsInt = Convert.ChangeType(value, underlyingType);
            dynamic flagAsInt = Convert.ChangeType(flag, underlyingType);
            if (set) {
                valueAsInt |= flagAsInt;
            } else {
                valueAsInt &= ~flagAsInt;
            }
            return (T)valueAsInt;
        }

        /// <summary>
        /// Checks if the value contains the provided type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Has<T>(this System.Enum type, T value) {
            try {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Checks if the value is only the provided type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Is<T>(this System.Enum type, T value) {
            try {
                return (int)(object)type == (int)(object)value;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Returns the value of the target enum's display or description attribute. If not specified,
        /// the ToString() method's response is returned.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum value) {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var displayAttribute = value.GetDisplayAttribute();

            if (displayAttribute != null) {
                return displayAttribute.GetName();

            } else {
                var descriptionAttribute = value.GetDescriptionAttribute();

                if (descriptionAttribute != null) {
                    return descriptionAttribute.Description;
                } else {
                    return value.ToString();
                }
            }
        }

        /// <summary>
        /// Returns the value of the target enum's display attribute. If not specified,
        /// the ToString() method's response is returned.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DisplayAttribute GetDisplayAttribute(this Enum value) {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attributes.Length > 0) {
                return attributes[0];
            } else {
                return null;
            }
        }

        /// <summary>
        /// Returns the value of the target enum's display attribute. If not specified,
        /// the ToString() method's response is returned.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DescriptionAttribute GetDescriptionAttribute(this Enum value) {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0) {
                return attributes[0];
            } else {
                return null;
            }
        }
    }
}

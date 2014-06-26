using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Helpers.ClassExtensionMethods {
    public static class EnumExtensionMethods {

        /// <summary>
        /// Returns the flags of a flagged enumerable.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<System.Enum> GetFlags(this System.Enum type) {
            foreach (System.Enum value in System.Enum.GetValues(type.GetType())) {
                if (type.HasFlag(value) && Convert.ToInt64(value) != 0) {
                    yield return value;
                }
            }
        }

        // This is not exactly perfect, as it allows you to call GetFlags on any
        // struct type, which will throw an exception at runtime if the type isn't
        // an enum.
        public static IEnumerable<TEnum> GetFlags<TEnum>(this TEnum flags)
            where TEnum : struct {
            // Unfortunately this boxing/unboxing is the easiest way
            // to do this due to C#'s lack of a where T : enum constraint
            // (there are ways around this, but they involve some
            // frustrating code).
            int flagsValue = (int)(object)flags;

            foreach (int flag in Enum.GetValues(typeof(TEnum))) {
                if ((flagsValue & flag) == flag) {
                    // Once again: an unfortunate boxing/unboxing
                    // due to the lack of a where T : enum constraint.
                    yield return (TEnum)(object)flag;
                }
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
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0) {
                return attributes[0];
            } else {
                return null;
            }
        }
    }
}

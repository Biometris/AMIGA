using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Biometris.ExtensionMethods {
    public static class MemberInfoExtensionMethods {

        /// <summary>
        /// Gets the first attribute of the specified attribute type from the object member info.
        /// </summary>
        /// <typeparam name="T">The type of the attribute that we are looking for.</typeparam>
        /// <param name="member">The object member information in which we look for the attribute.</param>
        /// <param name="isRequired">Specifies whether the specified attribute is expected (and should be present).</param>
        /// <returns>The first attribute of the specified type.</returns>
        public static T GetAttribute<T>(this MemberInfo member, bool isRequired)
            where T : Attribute {
            var attribute = member.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            if (attribute == null && isRequired) {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The {0} attribute must be defined on member {1}",
                        typeof(T).Name,
                        member.Name));
            }
            return (T)attribute;
        }

        /// <summary>
        /// Gets "Name" or "ShortName" property of the DisplayAttribute. If both are unspecified gets the DisplayNameAttribute value. If no
        /// 'name' attributes are specified, the propertyname itself it returned.
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static string GetDisplayName(this MemberInfo memberInfo) {

            // Get display attribute Name
            var displayAttribute = memberInfo.GetAttribute<DisplayAttribute>(false);
            if (displayAttribute != null && !string.IsNullOrEmpty(displayAttribute.GetName())) {
                return displayAttribute.GetName();
            }

            // Get DisplayName attribute
            var displayNameAttr = memberInfo.GetAttribute<DisplayNameAttribute>(false);
            if (displayNameAttr != null) {
                return displayNameAttr.DisplayName;
            }

            // Get display attribute ShortName
            if (displayAttribute != null && !string.IsNullOrEmpty(displayAttribute.GetShortName())) {
                return displayAttribute.GetShortName();
            }

            return memberInfo.Name;
        }

        /// <summary>
        /// Gets the Description property of the DisplayAttribute or the Description property of the DescriptionAttribute.
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static string GetDescription(this MemberInfo memberInfo) {
            //Get display attribute
            var displayAttr = memberInfo.GetAttribute<DisplayAttribute>(false);
            if (displayAttr != null && !string.IsNullOrEmpty(displayAttr.GetDescription())) {
                return displayAttr.GetDescription();
            }

            //Get DisplayName attribute
            var descriptionAttribute = memberInfo.GetAttribute<DescriptionAttribute>(false);
            if (descriptionAttribute != null) {
                return descriptionAttribute.Description;
            }

            return null;
        }

        /// <summary>
        /// Gets "ShortName" property of the DisplayAttribute. If unspecified, the propertyname itself is returned.
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static string GetShortName(this MemberInfo memberInfo) {
            var displayAttr = memberInfo.GetAttribute<DisplayAttribute>(false);

            if (displayAttr != null && !string.IsNullOrEmpty(displayAttr.GetName())) {
                return displayAttr.GetName();
            }
            if (displayAttr != null && !string.IsNullOrEmpty(displayAttr.GetShortName())) {
                return displayAttr.GetShortName();
            }

            var displayNameAttr = memberInfo.GetAttribute<DisplayNameAttribute>(false);
            if (displayNameAttr != null) {
                return displayNameAttr.DisplayName;
            }

            return memberInfo.Name;
        }
    }
}

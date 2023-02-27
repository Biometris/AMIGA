using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometris.DataFileReading.PropertyMapping {
    public class IdentityMapper : IPropertyMapper {

        public IdentityMapper(string targetPropertyName) : base(targetPropertyName) {
        }

        public override void mapProperty<Target>(object rawValue, Target targetRecord) {
            var targetRecordType = typeof(Target);
            var targetPropertyType = targetRecordType.GetProperty(TargetPropertyName).PropertyType;
            var value = ChangeType(rawValue, targetPropertyType);
            targetRecordType.GetProperty(TargetPropertyName).SetValue(targetRecord, value, null);
        }

        /// <summary>
        /// Returns an Object with the specified Type and whose value is equivalent to the specified object.
        /// </summary>
        /// <param name="value">An Object that implements the IConvertible interface.</param>
        /// <param name="conversionType">The Type to which value is to be converted.</param>
        /// <returns>An object whose Type is conversionType (or conversionType's underlying type if conversionType
        /// is Nullable&lt;&gt;) and whose value is equivalent to value. -or- a null reference, if value is a null
        /// reference and conversionType is not a value type.</returns>
        /// <remarks>
        /// This method exists as a workaround to System.Convert.ChangeType(Object, Type) which does not handle
        /// nullables as of version 2.0 (2.0.50727.42) of the .NET Framework. The idea is that this method will
        /// be deleted once Convert.ChangeType is updated in a future version of the .NET Framework to handle
        /// nullable types, so we want this to behave as closely to Convert.ChangeType as possible.
        /// This method was written by Peter Johnson at:
        /// http://aspalliance.com/author.aspx?uId=1026.
        /// 
        /// NOTE: Gerie added a single line to handle conversion to any Enum type from value.ToString(). 
        /// </remarks>
        public static object ChangeType(object value, Type conversionType) {
            // Note: This if block was taken from Convert.ChangeType as is, and is needed here since we're
            // checking properties on conversionType below.
            if (conversionType == null) {
                throw new ArgumentNullException("conversionType");
            }

            // If it's not a nullable type, just pass through the parameters to Convert.ChangeType
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))) {
                // It's a nullable type, so instead of calling Convert.ChangeType directly which would throw a
                // InvalidCastException (per http://weblogs.asp.net/pjohnson/archive/2006/02/07/437631.aspx),
                // determine what the underlying type is
                // If it's null, it won't convert to the underlying type, but that's fine since nulls don't really
                // have a type--so just return null
                // Note: We only do this check if we're converting to a nullable type, since doing it outside
                // would diverge from Convert.ChangeType's behavior, which throws an InvalidCastException if
                // value is null and conversionType is a value type.
                if (value == null || value == DBNull.Value) {
                    return null;
                }

                // It's a nullable type, and not null, so that means it can be converted to its underlying type,
                // so overwrite the passed-in conversion type with this underlying type
                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            } else if (conversionType.BaseType == typeof(Enum)) {
                // Added by Gerie: handle conversion to Enum
                return Enum.Parse(conversionType, value.ToString(), true);
            }

            // Now that we've guaranteed conversionType is something Convert.ChangeType can handle (i.e. not a
            // nullable type), pass the call on to Convert.ChangeType
            return System.Convert.ChangeType(value, conversionType);
        }
    }
}

using System;
using System.Reflection;

namespace Biometris.DataFileReader {
    public sealed class ColumnMapping {

        /// <summary>
        /// The column index of the source.
        /// </summary>
        public int SourceColumnIndex { get; set; }

        /// <summary>
        /// The column header name of the source.
        /// </summary>
        public string SourceColumnHeaderName { get; set; }

        /// <summary>
        /// The target property.
        /// </summary>
        public PropertyInfo TargetProperty { get; set; }

        /// <summary>
        /// The mapped column definition.
        /// </summary>
        public ColumnDefinition ColumnDefinition { get; set; }

        /// <summary>
        /// The target type of the mapped property.
        /// </summary>
        public Type TargetType {
            get {
                return TargetProperty.PropertyType;
            }
        }

        /// <summary>
        /// Returns whether this column mapping maps a dynamic column.
        /// </summary>
        public bool IsMultiColumn {
            get {
                return ColumnDefinition.IsMultiColumn;
            }
        }

        /// <summary>
        /// Returns whether this column mapping maps a dynamic column.
        /// </summary>
        public bool IsDynamic {
            get {
                return ColumnDefinition.IsDynamic;
            }
        }
    }
}

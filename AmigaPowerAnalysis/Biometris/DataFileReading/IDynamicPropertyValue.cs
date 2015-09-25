namespace Biometris.DataFileReader {
    public interface IDynamicPropertyValue {

        /// <summary>
        /// The name/key of the dynamic property.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The string value of the property.
        /// </summary>
        string RawValue { get; set; }

    }
}

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Biometris.ExtensionMethods {
    public static class SerializationExtensions {

        /// <summary>
        /// Serializes the object of the generic type to the specified file
        /// in binary format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="filename"></param>
        public static void ToBinaryFile<T>(this T obj, string filename) {
            using (var stream = File.Open(filename, FileMode.Create)) {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, obj);
                stream.Close();
            }
        }

        /// <summary>
        /// Loads the specified binary file and deserializes it into an object
        /// of the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static T FromBinaryFile<T>(string filename) {
            using (var stream = File.Open(filename, FileMode.Open)) {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                var obj = (T)binaryFormatter.Deserialize(stream);
                return obj;
            }
        }

        /// <summary>
        /// Serializes the object with an xml serializer and writes it to
        /// a file with the specified filename.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="filename"></param>
        public static void ToXmlFile<T>(this T obj, string filename) {
            var serializer = new XmlSerializer(typeof(T));
            using (var file = new StreamWriter(filename, false, Encoding.Unicode)) {
                serializer.Serialize(file, obj);
            }
        }

        /// <summary>
        /// Checks whether the file with the given filename can be deserialized
        /// as the provided generic type using the xml serializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool CanReadFromXmlFile<T>(string filename) {
            using (var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read)) {
                var serializer = new XmlSerializer(typeof(T));
                XmlReader xmlReader = new XmlTextReader(fileStream);
                return serializer.CanDeserialize(xmlReader);
            }
        }

        /// <summary>
        /// Deserializes the file with the given filename to the specified generic
        /// and returns the deserialized object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static T FromXmlFile<T>(string filename) {
            using (var reader = new StreamReader(filename)) {
                var serializer = new XmlSerializer(typeof(T));
                var obj = (T)serializer.Deserialize(reader);
                return obj;
            }
        }
    }
}

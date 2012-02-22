using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace UseAbilities.XML.Serialization
{
    public static class SerializationUtility
    {
        /// <summary>
        /// Method for serialization properties with DataMember attribute in class with attribute DataContract
        /// </summary>
        /// <typeparam name="T">The class serializable object</typeparam>
        /// <param name="xmlObject">Serializable object</param>
        /// <param name="fileName">File name of XML where the object will be serialized</param>
        public static void Serialize<T>(T xmlObject, string fileName)
        {
            var fileStream = File.Create(fileName);
            var serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(fileStream, xmlObject);
            fileStream.Close();
        }

        /// <summary>
        /// Method for deserialization object from XML file
        /// </summary>
        /// <typeparam name="T">The class serializable object</typeparam>
        /// <param name="fileName">File name of XML where the  the object will be deserialized</param>
        /// <returns></returns>
        public static T Deserialize<T>(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException();

            var fileStream = new FileStream(fileName, FileMode.Open);
            var reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas());
            var deserializer = new DataContractSerializer(typeof(T));
            var xmlObject = (T)deserializer.ReadObject(reader, true);
            reader.Close();
            fileStream.Close();

            return xmlObject;
        }
    }
}

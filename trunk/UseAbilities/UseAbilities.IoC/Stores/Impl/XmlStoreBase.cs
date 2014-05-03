using System.IO;
using UseAbilities.XML.Serialization;

namespace UseAbilities.IoC.Stores.Impl
{
    public class XmlStoreBase<T> : FileStoreBase<T>
    {
        public override string FileName { get; set; }

        public override T Load()
        {
            return Load(FileName);
        }

        public override T Load(string fileName)
        {
            if (File.Exists(fileName)) throw new FileNotFoundException(fileName);

            return SerializationUtility.Deserialize<T>(fileName);
        }

        public override void Save(T storeObject)
        {
            Save(storeObject, FileName);
        }

        public override void Save(T storeObject, string fileName)
        {
            SerializationUtility.Serialize(storeObject, fileName);
        }
    }
}

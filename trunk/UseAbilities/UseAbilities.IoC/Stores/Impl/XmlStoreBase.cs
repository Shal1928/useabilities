using System.IO;
using UseAbilities.XML.Serialization;

namespace UseAbilities.IoC.Stores.Impl
{
    public class XmlStoreBase<T> : AbstractFileStore<T>
    {
        public override T Load()
        {
            return Load(GetFileName());
        }

        public override T Load(string fileName)
        {
            if (File.Exists(fileName)) throw new FileNotFoundException(fileName);

            return SerializationUtility.Deserialize<T>(fileName);
        }

        public override void Save(T storeObject)
        {
            Save(storeObject, GetFileName());
        }

        public override void Save(T storeObject, string fileName)
        {
            SerializationUtility.Serialize(storeObject, fileName);
        }
    }
}

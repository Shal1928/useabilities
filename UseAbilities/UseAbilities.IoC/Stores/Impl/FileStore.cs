using System.IO;

namespace UseAbilities.IoC.Stores.Impl
{
    public class FileStore : AFileStore<byte[]>
    {
        public override string FileName { get; set; }

        public override byte[] Load()
        {
            return Load(FileName);
        }

        public override byte[] Load(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException(fileName);

            byte[] loadObject;
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var bytes = new byte[fileStream.Length];
                var numBytesToRead = (int)fileStream.Length;
                var numBytesRead = 0;

                while (numBytesToRead > 0)
                {
                    var n = fileStream.Read(bytes, numBytesRead, numBytesToRead);
                    if (n == 0) break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }

                loadObject = bytes;
            }

            return loadObject;
        }

        public override void Save(byte[] storeObject)
        {
            Save(storeObject, FileName);
        }

        public override void Save(byte[] storeObject, string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                fileStream.Write(storeObject, 0, storeObject.Length);
            }
        }
    }
}

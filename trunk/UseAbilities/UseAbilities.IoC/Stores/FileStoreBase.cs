namespace UseAbilities.IoC.Stores
{
    public abstract class FileStoreBase<T> : IFileStore<T>
    {
        public abstract string FileName { get; set; }
        public abstract T Load(string fileName);
        public abstract void Save(T storeObject, string fileName);
        public abstract void Save(T storeObject);
        public abstract T Load();
    }
}

namespace UseAbilities.IoC.Stores
{
    public abstract class AFileStore<T> : IKeyStore<T, string>
    {
        public abstract string FileName { get; set; }
        public abstract T Load(string fileName);
        public abstract void Save(T storeObject, string fileName);
        public abstract void Save(T storeObject);
        public abstract T Load();
    }
}

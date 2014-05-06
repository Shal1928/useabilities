namespace UseAbilities.IoC.Stores
{
    public abstract class AbstractFileStore<T> : AbstractFileReadStore<T>, IFileStore<T>
    {
        public abstract void Save(T storeObject);
        public abstract void Save(T storeObject, string key);
    }

    public abstract class AbstractFileReadStore<T> : IFileReadStore<T>
    {
        public abstract T Load();
        public abstract T Load(string key);

        private string _fileName;
        public void SetFileName(string fileName)
        {
            _fileName = fileName;
        }

        public string GetFileName()
        {
            return _fileName;
        }
    }
}

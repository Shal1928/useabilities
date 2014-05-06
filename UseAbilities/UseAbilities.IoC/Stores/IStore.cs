namespace UseAbilities.IoC.Stores
{
    public interface IStore<T> : IReadStore<T>
    {
        void Save(T storeObject);
    }

    public interface IKeyStore<T, TKey> : IKeyReadStore<T, TKey>, IStore<T>
    {
        void Save(T storeObject, TKey key);
    }

    public interface IFileStore<T> : IKeyStore<T, string>, IFileReadStore<T>
    {
        //
    }
}

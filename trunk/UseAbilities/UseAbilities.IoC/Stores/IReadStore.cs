namespace UseAbilities.IoC.Stores
{
    public interface IReadStore<T>
    {
        T Load();
    }

    public interface IKeyReadStore<T, TKey> : IReadStore<T>
    {
        T Load(TKey key);
    }

    public interface IFileReadStore<T> : IKeyReadStore<T, string>
    {
        void SetFileName(string fileName);
        string GetFileName();
    }
}

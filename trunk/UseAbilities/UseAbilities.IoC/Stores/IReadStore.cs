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
}

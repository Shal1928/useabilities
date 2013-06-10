namespace UseAbilities.IoC.Stores
{
    public interface IStore<T> : IReadStore<T>
    {
        void Save(T storeObject);
    }
}

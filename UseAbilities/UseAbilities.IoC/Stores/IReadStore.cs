namespace UseAbilities.IoC.Stores
{
    public interface IReadStore<T>
    {
        T Load();
        T Load(int key);
    }
}

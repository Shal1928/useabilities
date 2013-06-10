namespace UseAbilities.IoC.Stores
{
    public interface IFileStore<T> : IStore<T>
    {
        string FileName
        {
            get;
            set;
        }
    }
}

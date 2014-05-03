﻿namespace UseAbilities.IoC.Stores
{
    public interface IStore<T> : IReadStore<T>
    {
        void Save(T storeObject);
    }

    public interface IKeyStore<T, TKey> : IKeyReadStore<T, TKey>
    {
        void Save(T storeObject, TKey key);
    }
}
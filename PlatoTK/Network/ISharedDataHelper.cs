using System;

namespace PlatoTK.Network
{
    public interface ISharedDataHelper
    {
        TModel ReadSharedData<TModel>(string collection, string key, bool allowNull = true) where TModel : class, new();

        void WriteSharedData<TModel>(string collection, string key, TModel data) where TModel : class, new();

        void RemoveSharedData(string collection, string key);

        void RemoveCollection(string collection, string key);

        void CreateCollection(string collection, Action<CollectionChangeArgs> onCollectionChanged = null);

        void ObserveCollection(string collection, Action<CollectionChangeArgs> onCollectionChanged);
    }
}

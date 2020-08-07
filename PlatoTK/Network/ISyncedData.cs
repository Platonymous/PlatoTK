using System;

namespace PlatoTK.Network
{
    public interface ISyncedData : IDisposable
    {
        void Set(string key, string value, int index = 0);
        void Set(string key, int value);
        void Set(string key, float value);
        void Set(string key, bool value);
        void Set(string key, long value);
        void Set<T>(string key, T value) where T : class, new();
        string Get(string key);
        T Get<T>(string key);
        bool TryGet<T>(string key, out T value);

        void Update();

        void Reparse();

        void StopListener();
        void StartListener();

        string DataString { get; }
    }
}

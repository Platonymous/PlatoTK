using System;
using System.Collections.Generic;

namespace PlatoTK.Network
{
    internal class SyncedData : ISyncedData
    {
        private Dictionary<string, string> Data { get; }

        private Netcode.NetString Field { get; }

        public string DataString => Utils.Serialization.DataToString(Data);

        private bool Listening { get; set; } = false;

        public SyncedData(Netcode.NetString field)
        {
            Data = new Dictionary<string, string>();
            Field = field;
            ParseDataString(field.Get());
            StartListener();
        }

        private void SaveProperty_fieldChangeEvent(Netcode.NetString field, string oldValue, string newValue)
        {
            ParseDataString(newValue);
        }

        private void ParseDataString(string newValue)
        {
            Data.Clear();
            var data = Utils.Serialization.ParseDataString(newValue);
            foreach (string key in data.Keys)
                Data.Add(key, data[key]);

                Update();
        }

        public void Reparse()
        {
            ParseDataString(Field.Value);
        }


        public void Update()
        {
            if (Utils.Serialization.DataToString(Data) is string newValue && Field.Get() != newValue)
                Field.Set(newValue);
        }

        public void Set(string key, string value, int index = 0)
        {
            UpdateData(key, value);
        }

        public void Set(string key, int value)
        {
            UpdateData(key, value.ToString());
        }

        public void Set(string key, float value)
        {
            UpdateData(key, value.ToString());
        }

        public void Set(string key, bool value)
        {
            UpdateData(key, value.ToString());
        }

        public void Set(string key, long value)
        {
            UpdateData(key, value.ToString());
        }

        public void Set<T>(string key, T value) where T : class, new()
        {
            UpdateData(key, Utils.Serialization.SerializeValue<T>(value));
        }

        public string Get(string key)
        {
            if (Data.ContainsKey(key))
                return Data[key];

            return null;
        }

        private void UpdateData(string key, string value)
        {
            if (Data.ContainsKey(key) && Data[key] != value)
            {
                Data[key] = value;
                Update();
                return;
            }
            else if (!Data.ContainsKey(key))
            {
                Data.Add(key, value);
                Update();
            }
        }

        public bool TryGet<T>(string key, out T value)
        {
            if (Data.ContainsKey(key)
                && Data[key] is string s
                && !string.IsNullOrEmpty(s))
            {
                value = Get<T>(key);
                return true;
            }
            value = default;
            return false;
        }

        public T Get<T>(string key)
        {
            if (Data.ContainsKey(key)
                && Data[key] is string s
                && !string.IsNullOrEmpty(s))
            {
                if (typeof(T) == typeof(string))
                    return (T)(object)s;

                if (typeof(T) == typeof(bool))
                {
                    if (bool.TryParse(s, out bool b))
                        return (T)(object)b;

                    return (T)(object)false;
                }

                if (typeof(T) == typeof(int))
                {
                    if (int.TryParse(s, out int i))
                        return (T)(object)i;

                    return (T)(object)0;
                }

                if (typeof(T) == typeof(long))
                {
                    if (long.TryParse(s, out long i))
                        return (T)(object)i;

                    return (T)(object)0L;
                }

                if (typeof(T) == typeof(float))
                {
                    if (float.TryParse(s, out float i))
                        return (T)(object)i;

                    return (T)(object)0f;
                };

                return Utils.Serialization.DeserializeValue<T>(s);
            }

            return default;
        }

        public void StopListener()
        {
            if (Listening)
            {
                Field.fieldChangeEvent -= SaveProperty_fieldChangeEvent;
                Listening = false;
            }
        }

        public void StartListener()
        {
            if (Listening)
            {
                Field.fieldChangeEvent += SaveProperty_fieldChangeEvent;
                Listening = true;
            }
        }

        public void Dispose()
        {
            StopListener();
        }
    }
}

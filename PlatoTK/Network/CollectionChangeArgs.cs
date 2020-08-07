using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatoTK.Network
{
    public class CollectionChangeArgs
    {
        public readonly string Key;

        public readonly long ChangedBy;

        public readonly bool WasRemoved;

        protected readonly string _oldValue;

        protected readonly string _newValue;

        public CollectionChangeArgs() { }

        public TModel GetOldValue<TModel>()
        {
            return Utils.Serialization.DeserializeValue<TModel>(_oldValue);
        }

        public TModel GetNewValue<TModel>()
        {
            return Utils.Serialization.DeserializeValue<TModel>(_newValue);
        }

        public CollectionChangeArgs(string key, long changedBy, bool wasRemoved, string oldValue, string newValue)
        {
            Key = key;
            ChangedBy = changedBy;
            WasRemoved = wasRemoved;
            _oldValue = oldValue;
            _newValue = newValue;
        }
    }
}

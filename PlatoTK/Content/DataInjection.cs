using System;

namespace PlatoTK.Content
{
    internal abstract class DataInjection : AssetInjection<string>
    {
        public string[] ValueParts => Value.Split(Utils.Serialization.innerValueSeperator);
        public abstract Type GetKeyType { get; }

        public abstract T GetKey<T>();

        public DataInjection(
            IPlatoHelper helper,
            string assetName, 
            string value, 
            InjectionMethod method,
            string conditions = ""
            )
            : base(helper,assetName,value, method,conditions)
        {
           
        }
    }

    internal class DataInjection<TKey> : DataInjection
    {
        public readonly TKey Key;
        public override Type GetKeyType => typeof(TKey);

        

        public DataInjection(
            IPlatoHelper helper,
            string assetName, 
            TKey key, 
            string value,
            InjectionMethod method, 
            string conditions = "")
            : base(helper, assetName, value, method, conditions)
        {
            Key = key;
        }

        public DataInjection(
           IPlatoHelper helper,
           string assetName,
            TKey key,
            InjectionMethod method,
           string conditions = "",
           params string[] values)
            : this(helper, assetName, key, string.Join(Utils.Serialization.innerValueSeperator.ToString(), values), method, conditions)
        {

        }

        public override T GetKey<T>()
        {
            if (typeof(T) == typeof(TKey))
                return (T) (object) Key;

            return default;
        }
    }
}

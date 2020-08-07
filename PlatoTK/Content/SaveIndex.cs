using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using PlatoTK.APIs;

namespace PlatoTK.Content
{
    public interface ISaveIndexHandle
    {
        string Id { get; }
        string Value { get; }
        int Index { get; }
    }

    public interface ISaveIndex : ISaveIndexHandle
    {
        void ValidateIndex(int tryForceIndex = -1);
        bool TryAddToContentPatcher();
    }

    internal class SaveIndex : ISaveIndex
    {
        private readonly int MinIndex; 
        private readonly Func<ISaveIndexHandle, bool> Validator;
        private readonly Action<ISaveIndexHandle> Injector;
        private readonly Func<IDictionary<int, string>> DataLoader;
        private readonly IPlatoHelper Helper;

        public string Id { get; }
        public string Value
        {
            get
            {
                var dict = LoadData();
                if (Index >= MinIndex && dict.ContainsKey(Index))
                    return dict[Index];

                return "";
            }

        }
        public int Index { get; private set; }
        
        public SaveIndex(string id, 
            Func<IDictionary<int,string>> loadData, 
            Func<ISaveIndexHandle, bool> validateValue, 
            Action<ISaveIndexHandle> injectValue,
            IPlatoHelper helper, 
            int minIndex = 13000)
        {
            Helper = helper;
            Id = id;
            Validator = validateValue;
            Injector = injectValue;
            DataLoader = loadData;
            MinIndex = minIndex;
            Index = GetNewIndex();
            Injector?.Invoke(this);
            Validator?.Invoke(this);
        }

        public SaveIndex(string id, 
            string dataSource, 
            Func<ISaveIndexHandle, bool> validateValue, 
            Action<ISaveIndexHandle> injectValue, 
            IPlatoHelper helper, 
            int minIndex = 13000)
            : this(id,() => Game1.content.Load<Dictionary<int, string>>(dataSource), validateValue,injectValue, helper, minIndex)
        {
         
        }

        public void ValidateIndex(int tryForceIndex = -1)
        {
            var dict = LoadData();

            if (tryForceIndex >= MinIndex && tryForceIndex != Index)
            {
                if (!dict.ContainsKey(tryForceIndex))
                {
                    Index = tryForceIndex;
                    Injector?.Invoke(this);
                }
                else
                    Index = GetNewIndex();
            }

            if (Validator?.Invoke(this) ?? true)
            {
                Index = Index;
                return;
            }

            Index = GetNewIndex();
            Injector?.Invoke(this);
        }

        private IDictionary<int, string> LoadData()
        {
            return DataLoader?.Invoke() ?? new Dictionary<int, string>();
        }

        private int GetNewIndex()
        {
            return Math.Max(MinIndex, (LoadData()?.Keys.Max() + 1) ?? 0);
        }

        public bool TryAddToContentPatcher()
        {
            if (!Helper.ModHelper.ModRegistry.IsLoaded("Pathoschild.ContentPatcher"))
                return false;

            var api = Helper.ModHelper.ModRegistry.GetApi<IContentPatcher>("Pathoschild.ContentPatcher");
            api.RegisterToken(Helper.ModHelper.ModRegistry.Get(Helper.ModHelper.ModRegistry.ModID).Manifest, Id, () =>
            {
                ValidateIndex();
                return new[] { Id };
            });

            return true;
        }
    }
}

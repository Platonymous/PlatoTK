using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using xTile;

namespace PlatoTK.Content
{
    internal class InjectionHelper : InnerHelper, IInjectionHelper
    {
        private static readonly HashSet<AssetInjector> Injected = new HashSet<AssetInjector>();

        private AssetInjector Injector => Injected.FirstOrDefault(i => i.Mod == Helper.ModHelper.ModRegistry.ModID);

        public InjectionHelper(IPlatoHelper helper)
            : base(helper)
        {
            if (!Injected.Any(i => i.Mod == helper.ModHelper.ModRegistry.ModID))
                Injected.Add(new AssetInjector(helper));
        }

        public void InjectLoad<TAsset>(string assetName, TAsset asset, string conditions = "", IConditionsProvider provider = null)
        {
            Injector.AddInjection(new ObjectInjection<TAsset>(Helper, assetName, asset, InjectionMethod.Load, conditions, provider));
        }
        
        public void InjectDataInsert(string assetName, int key, string value, string conditions = "", IConditionsProvider provider = null)
        {
            Injector.AddInjection(new DataInjection<int>(Helper,assetName,key, value, InjectionMethod.Replace, conditions,provider));
        }

        public void InjectDataPatch(string assetName, int key, string conditions = "", IConditionsProvider provider = null, params string[] values)
        {
            Injector.AddInjection(new DataInjection<int>(Helper, assetName, key, InjectionMethod.Merge, conditions, provider, values));
        }

        public void InjectDataInsert(string assetName, string key, string value, string conditions = "", IConditionsProvider provider = null)
        {
            Injector.AddInjection(new DataInjection<string>(Helper, assetName, key, value, InjectionMethod.Replace, conditions, provider));
        }

        public void InjectDataPatch(string assetName, string key, string conditions = "", IConditionsProvider provider = null, params string[] values)
        {
            Injector.AddInjection(new DataInjection<string>(Helper, assetName, key, InjectionMethod.Merge, conditions, provider, values));
        }

        public void InjectPatch(string assetName, Texture2D asset, bool overlay = false, Rectangle? sourceArea = null, Rectangle? targetArea = null, string conditions = "", IConditionsProvider provider = null)
        {
            Injector.AddInjection(new TextureInjection(Helper, assetName, asset, overlay ? InjectionMethod.Overlay : InjectionMethod.Merge, sourceArea, targetArea, conditions, provider));
        }

        public void InjectPatch(string assetName, Map asset, Rectangle? sourceArea = null, Rectangle? targetArea = null, string conditions = "", IConditionsProvider provider = null, bool removeEmpty = false)
        {
            Injector.AddInjection(new MapInjection(Helper, assetName, asset, removeEmpty ? InjectionMethod.Merge : InjectionMethod.Overlay, sourceArea, targetArea, conditions, provider));
        }
    }
}

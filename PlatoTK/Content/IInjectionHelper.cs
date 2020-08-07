using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using xTile;

namespace PlatoTK.Content
{
    public interface IInjectionHelper
    {
        void InjectLoad<TAsset>(string assetName, TAsset asset, string conditions = "", IConditionsProvider provider = null);
        void InjectDataInsert(string assetName, int key, string value, string conditions = "", IConditionsProvider provider = null);
        void InjectDataPatch(string assetName, int key, string conditions = "", IConditionsProvider provider = null, params string[] values);
        void InjectDataInsert(string assetName, string key, string value, string conditions = "", IConditionsProvider provider = null);
        void InjectDataPatch(string assetName, string key, string conditions = "", IConditionsProvider provider = null, params string[] values);
        void InjectPatch(string assetName, Texture2D asset, bool overlay = false, Rectangle? sourceArea = null, Rectangle? targetArea = null, string conditions = "", IConditionsProvider provider = null);
        void InjectPatch(string assetName, Map asset, Rectangle? sourceArea = null, Rectangle? targetArea = null, string conditions = "", IConditionsProvider provider = null, bool removeEmpty = false);

    }
}

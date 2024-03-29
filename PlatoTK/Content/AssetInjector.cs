﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace PlatoTK.Content
{
    internal enum InjectionMethod
    {
        Replace,
        Merge,
        Overlay,
        Load
    }

    internal class AssetInjector
    {
        private readonly HashSet<AssetInjection> Injections;
        private readonly IPlatoHelper Helper;

        internal string Mod => Helper?.ModHelper.ModRegistry.ModID;

        public AssetInjector(IPlatoHelper helper)
        {
            Helper = helper;
            Injections = new HashSet<AssetInjection>();
            helper.ModHelper.Events.Content.AssetRequested += OnAssetRequested;
        }

        internal void AddInjection(AssetInjection assetInjection)
        {
            Injections.Add(assetInjection);
        }

        private void OnAssetRequested(object sender, AssetRequestedEventArgs e)
        {
            AssetInjection[] injections = Injections
                .Where(p => e.NameWithoutLocale.IsEquivalentTo(p.AssetName) && p.ConditionsMet)
                .ToArray();

            if (injections.Any())
            {
                var loaders = injections.Where(p => p.Method is InjectionMethod.Load).ToArray();
                var editors = injections.Where(p => p.Method is not InjectionMethod.Load).ToArray();

                if (loaders.Any())
                    e.LoadFrom(() => this.Load(loaders), AssetLoadPriority.Medium);

                if (editors.Any())
                    e.Edit(asset => this.Edit(asset, editors));
            }
        }

        private void Edit(IAssetData asset, AssetInjection[] editors)
        {
            foreach (var injection in editors)
                if (injection is DataInjection dataInjection)
                {
                    if (dataInjection.GetKeyType == typeof(int))
                        asset.ReplaceWith(injectData<int>(asset, dataInjection));
                    else if (dataInjection.GetKeyType == typeof(string))
                        asset.ReplaceWith(injectData<string>(asset, dataInjection));
                }
                else if (injection is TextureInjection textureInjection)
                {
                    if (textureInjection.Method == InjectionMethod.Replace)
                        asset.ReplaceWith(textureInjection.Value);
                    else if (textureInjection.Method == InjectionMethod.Merge || textureInjection.Method == InjectionMethod.Overlay)
                        asset.AsImage().PatchImage(
                            textureInjection.Value,
                            textureInjection.SourceArea,
                            textureInjection.TargetArea,
                            textureInjection.Method == InjectionMethod.Overlay ? PatchMode.Overlay : PatchMode.Replace);
                }
                else if (injection is MapInjection mapInjection)
                {
                    if (mapInjection.Method == InjectionMethod.Merge)
                        asset.AsMap().PatchMap(mapInjection.Value, mapInjection.SourceArea, mapInjection.TargetArea);
                    if (mapInjection.Method == InjectionMethod.Overlay)
                    {
                        asset.ReplaceWith(Helper.Content.Maps.PatchMapArea(asset.AsMap().Data,
                             mapInjection.Value, mapInjection.TargetArea.HasValue ?
                             new Point(mapInjection.TargetArea.Value.X, mapInjection.TargetArea.Value.Y) : Point.Zero, mapInjection.SourceArea, true, false));
                    }
                    else if (mapInjection.Method == InjectionMethod.Replace)
                        asset.ReplaceWith(mapInjection.Value);
                }
                else if (injection is ObjectInjection objectInjection)
                    asset.ReplaceWith(objectInjection.Value);
        }

        private IDictionary<TKey, string> injectData<TKey>(IAssetData asset, DataInjection injection)
        {
            var dict = asset.AsDictionary<TKey, string>().Data;
            if (dict.ContainsKey(injection.GetKey<TKey>()))
            {
                if (injection.Method == InjectionMethod.Merge || injection.Method == InjectionMethod.Overlay)
                {
                    string[] currentValues = dict[injection.GetKey<TKey>()].Split(Utils.Serialization.innerValueSeperator);
                    string[] newValues = injection.ValueParts;
                    List<string> addedValues = new List<string>();
                    for (int i = 0; i < newValues.Length; i++)
                        if (currentValues.Length > i)
                            currentValues[i] = string.IsNullOrEmpty(newValues[i]) ? currentValues[i] : newValues[i];
                        else
                            addedValues.Add(newValues[i]);

                    if (addedValues.Count > 0)
                    {
                        List<string> finalValues = new List<string>(currentValues);
                        finalValues.AddRange(addedValues);
                        currentValues = finalValues.ToArray();
                    }

                    dict[injection.GetKey<TKey>()] = string.Join(Utils.Serialization.innerValueSeperator.ToString(), currentValues);
                }
                else if (injection.Method == InjectionMethod.Replace)
                    dict[injection.GetKey<TKey>()] = injection.Value;
            }
            else
                dict.Add(injection.GetKey<TKey>(), injection.Value);

            return dict;
        }

        private object Load(AssetInjection[] loaders)
        {
            object result = null;
            foreach (var injection in loaders)
            {
                switch (injection)
                {
                    case DataInjection dataInjection:
                        if (dataInjection.GetKeyType == typeof(int))
                        {
                            if (result is Dictionary<int, string> dict)
                                dict.Add(dataInjection.GetKey<int>(), dataInjection.Value);
                            else
                                result = (new Dictionary<int, string>() { { dataInjection.GetKey<int>(), dataInjection.Value } });
                        }
                        else if (dataInjection.GetKeyType == typeof(string))
                        {
                            if (result is Dictionary<string, string> dict)
                                dict.Add(dataInjection.GetKey<string>(), dataInjection.Value);
                            else
                                result = (new Dictionary<string, string>() { { dataInjection.GetKey<string>(), dataInjection.Value } });
                        }
                        break;

                    case TextureInjection textureInjection:
                        result = textureInjection.Value;
                        break;

                    case MapInjection mapInjection:
                        result = mapInjection.Value;
                        break;

                    case ObjectInjection objectInjection:
                        result = objectInjection.Value;
                        break;
                }
            }

            return result;
        }

    }
}

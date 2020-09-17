using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using xTile.Layers;
using xTile.ObjectModel;
using Harmony;
using PlatoTK;

namespace MapTK.MapExtras
{
    internal class ExtraLayersHandler
    {
        internal static readonly Dictionary<Layer, List<Layer>> DrawBeforeCache = new Dictionary<Layer, List<Layer>>();
        internal static readonly Dictionary<Layer, List<Layer>> DrawAfterCache = new Dictionary<Layer, List<Layer>>();
        internal const string UseProperty = "@As";
        internal const string UseConditionProperty = "@As_Conditions";
        internal static IPlatoHelper Plato { get; set; }

        public ExtraLayersHandler(IModHelper helper)
        {
            Plato = helper.GetPlatoHelper();
            helper.Events.GameLoop.SaveLoaded += InitializeExtraLayers;
            helper.Events.GameLoop.SaveCreated += InitializeExtraLayers;
            helper.Events.Player.Warped += Player_Warped;

            var harmony = HarmonyInstance.Create("Platonymous.MapTK.ExtraLayers");
            harmony.Patch(AccessTools.Method(typeof(GameLocation), "reloadMap"),null,new HarmonyMethod(typeof(ExtraLayersHandler),nameof(AfterMapReload)));
        }

        internal static void AfterMapReload(GameLocation __instance)
        {
            __instance?.Map.Layers
                .ToList()
                .ForEach(AttachLayerDrawHandlers);
        }

        private void Player_Warped(object sender, WarpedEventArgs e)
        {
            DrawBeforeCache.Clear();
            DrawAfterCache.Clear();

            e.NewLocation?.Map.Layers
                .ToList()
                .ForEach(AttachLayerDrawHandlers);
        }

        private void InitializeExtraLayers(object sender, EventArgs e)
        {
            DrawBeforeCache.Clear();
            DrawAfterCache.Clear();

            Game1.locations
                .Select(l => l.Map)
                .SelectMany(m => m.Layers)
                .ToList()
                .ForEach(AttachLayerDrawHandlers);
        }

        internal static void AttachLayerDrawHandlers(Layer layer)
        {
            layer.AfterDraw -= Layer_AfterDraw;
            layer.BeforeDraw -= Layer_BeforeDraw;
            layer.AfterDraw += Layer_AfterDraw;
            layer.BeforeDraw += Layer_BeforeDraw;
        }

        private static void Layer_BeforeDraw(object sender, LayerEventArgs layerEventArgs)
        {
            Layer_Draw(layerEventArgs, "DrawBefore", DrawBeforeCache);
        }

        private static void Layer_AfterDraw(object sender, LayerEventArgs layerEventArgs)
        {
            Layer_Draw(layerEventArgs, "DrawAfter", DrawAfterCache);
        }

        private static void Layer_Draw(LayerEventArgs layerEventArgs, string drawProperty, Dictionary<Layer, List<Layer>> cache)
        {
            if (cache.TryGetValue(layerEventArgs.Layer, out List<Layer> extraLayers))
            {
                extraLayers.ForEach((extralayer) => DrawExtraLayer(extralayer, layerEventArgs));
                return;
            }

            cache.Add(layerEventArgs.Layer, new List<Layer>());

            layerEventArgs.Layer.Map.Layers
                .Where(l => l.Properties.TryGetValue(UseProperty, out PropertyValue value) 
                            && value.ToString().Split(' ') is string[] p &&
                            p.Length >= 2
                            && p[0] == drawProperty
                            && p[1] == layerEventArgs.Layer.Id
                            && (!l.Properties.TryGetValue(UseConditionProperty, out PropertyValue conditions) || Plato.CheckConditions(conditions.ToString(), l))
                            )
                .ToList()
                .ForEach((l) => {
                    if (!cache[layerEventArgs.Layer].Contains(l))
                        cache[layerEventArgs.Layer].Add(l);
                });

            Layer_Draw(layerEventArgs, drawProperty, cache);
        }

        private static void DrawExtraLayer(Layer layer, LayerEventArgs layerEventArgs)
        {
            if(layer.Id != "Front")
                layer.Draw(Game1.mapDisplayDevice, layerEventArgs.Viewport, new xTile.Dimensions.Location(0, 0), false, Game1.pixelZoom);
        }

    }
}

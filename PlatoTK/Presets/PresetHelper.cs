using System;

namespace PlatoTK.Presets
{
    internal class PresetHelper : IPresetHelper
    {
        internal IPlatoHelper Helper;

        public PresetHelper(IPlatoHelper helper)
        {
            Helper = helper;
        }

        public void RegisterArcade(string id, string name, string objectName, Action start, string sprite, string iconForMobilePhone)
        {
            ArcadeMachinePreset.Add(new ArcadeMachineSpecs(name, Helper.ModHelper.ModRegistry.ModID + id, objectName, start, sprite, iconForMobilePhone), Helper);
        }
    }
}

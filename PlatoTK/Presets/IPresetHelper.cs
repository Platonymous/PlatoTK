using System;

namespace PlatoTK.Presets
{
    public interface IPresetHelper
    {
        void RegisterArcade(string id, string name, string objectName, Action start, string sprite, string iconForMobilePhone);
    }
}

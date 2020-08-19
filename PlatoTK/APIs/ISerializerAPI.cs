using StardewModdingAPI;
using System;
using System.Collections.Generic;

namespace PlatoTK.APIs
{
    public interface ISerializerAPI
    {
        void AddPreSerialization(IManifest manifest, Func<object, object> preserializer);
        void AddPostDeserialization(IManifest manifest, Func<object, object> postserializer);
        Dictionary<string, string> ParseDataString(object o);
    }
}

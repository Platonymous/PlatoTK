using System;
using System.Collections.Generic;

namespace PlatoTK.Content
{
    public interface IContentHelper
    {
        IInjectionHelper Injections { get; }
        ITextureHelper Textures { get; }

        ISaveIndex GetSaveIndex(string id,
            Func<IDictionary<int, string>> loadData,
            Func<ISaveIndexHandle, bool> validateValue,
            Action<ISaveIndexHandle> injectValue,
            int minIndex = 13000);

        ISaveIndex GetSaveIndex(string id,
            string dataSource,
            Func<ISaveIndexHandle, bool> validateValue,
            Action<ISaveIndexHandle> injectValue,
            int minIndex = 13000);

        IMapHelper Maps { get; }
    }
}

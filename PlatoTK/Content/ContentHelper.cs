using System;
using System.Collections.Generic;

namespace PlatoTK.Content
{
    internal class ContentHelper : InnerHelper, IContentHelper
    {
        public IInjectionHelper Injections { get; }

        public ITextureHelper Textures { get; }

        public IMapHelper Maps { get; }

        public ContentHelper(IPlatoHelper helper)
            : base(helper)
        {
            Textures = new TextureHelper(helper);
            Injections = new InjectionHelper(helper);
            Maps = new MapHelper(helper);
        }

        public ISaveIndex GetSaveIndex(string id, Func<IDictionary<int, string>> loadData, Func<ISaveIndexHandle, bool> validateValue, Action<ISaveIndexHandle> injectValue, int minIndex = 13000)
        {
            return new SaveIndex(id, loadData, validateValue, injectValue, Helper, minIndex);
        }

        public ISaveIndex GetSaveIndex(string id, string dataSource, Func<ISaveIndexHandle, bool> validateValue, Action<ISaveIndexHandle> injectValue, int minIndex = 13000)
        {
            return new SaveIndex(id, dataSource, validateValue, injectValue, Helper, minIndex);
        }
    }
}

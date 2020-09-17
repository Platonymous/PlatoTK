using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatoTK.Content
{
    internal abstract class PatchableInjection<TAsset> : AssetInjection<TAsset>
    {
        public readonly Rectangle? SourceArea;

        public readonly Rectangle? TargetArea;
        public PatchableInjection(
            IPlatoHelper helper,
            string assetName,
            TAsset value,
            InjectionMethod method,
            Rectangle? sourceArea = null,
            Rectangle? targetArea = null,
            string conditions = "")
            : base(helper, assetName, value, method, conditions)
        {
            SourceArea = sourceArea;
            TargetArea = targetArea;
        }
    }
}

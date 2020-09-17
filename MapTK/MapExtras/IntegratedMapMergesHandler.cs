using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapTK.MapExtras
{
    internal class IntegratedMapMergesHandler
    {

        public IntegratedMapMergesHandler(IModHelper helper)
        {
            helper.Content.AssetEditors.Add(new IntegratedMapEditsAssetEditor(helper));
        }
    }
}

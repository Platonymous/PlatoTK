using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace MapTK.MapExtras
{
    internal class IntegratedMapMergesHandler
    {
        private readonly IntegratedMapEditsAssetEditor AssetEditor;

        public IntegratedMapMergesHandler(IModHelper helper)
        {
            this.AssetEditor = new IntegratedMapEditsAssetEditor(helper);
            helper.Events.Content.AssetRequested += OnAssetRequested;
        }

        public void OnAssetRequested(object sender, AssetRequestedEventArgs e)
        {
            this.AssetEditor.OnAssetRequested(e);
        }
    }
}

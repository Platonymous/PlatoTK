﻿using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace MapTK.MapExtras
{
    class GameAssetLoader
    {
        private IModHelper helper;

        public GameAssetLoader(IModHelper helper)
        {
            this.helper = helper;
        }

        public void OnAssetRequested(AssetRequestedEventArgs e)
        {
            if (e.DataType == typeof(Texture2D))
            {
                string[] path = e.Name.Name.Split(new[] { Path.DirectorySeparatorChar, '/', '\\' }, count: 3);

                switch (path[0])
                {
                    case "GameContent":
                        e.LoadFrom(() => helper.GameContent.Load<Texture>(string.Join(Path.DirectorySeparatorChar.ToString(), path.Skip(1))), AssetLoadPriority.Medium);
                        break;

                    case "Maps" when path.Length > 1 && path[1] == "GameContent":
                        e.LoadFrom(() => helper.GameContent.Load<Texture>(string.Join(Path.DirectorySeparatorChar.ToString(), path.Skip(2))), AssetLoadPriority.Medium);
                        break;
                }
            }
        }
    }
}

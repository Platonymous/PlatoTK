using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace PlatoTK.Content
{
    internal class PlatoSpritebatch<TSpriteBatch>
    {
        private readonly TSpriteBatch SpriteBatch;

        private readonly bool IsAndroid;

        private readonly IPlatoHelper Helper;

        public PlatoSpritebatch(IPlatoHelper helper, TSpriteBatch spriteBatch)
        {
            Helper = helper;
            SpriteBatch = spriteBatch;
            IsAndroid = Constants.TargetPlatform == GamePlatform.Android;
        }
    }
}

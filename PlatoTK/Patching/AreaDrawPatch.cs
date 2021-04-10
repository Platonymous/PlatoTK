using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlatoTK.Patching
{
    internal class AreaDrawPatch
    {
        public readonly Func<Texture2D,bool> Texture;

        public readonly Texture2D Patch;

        public readonly Rectangle SourceArea;

        public readonly Func<Rectangle> TargetArea;

        public readonly string Id;

        public AreaDrawPatch(string id, Texture2D texture, Func<Texture2D,bool> predicate ,Texture2D patch, Func<Rectangle> targetArea = null, Rectangle? sourceArea = null)
        {
            Id = id;
            Patch = patch;
            Texture = predicate;
            Texture2D cTexture = texture;
            SourceArea = sourceArea.HasValue ? sourceArea.Value : new Rectangle(0, 0, patch.Width, patch.Height);
            TargetArea = targetArea == null ? () => new Rectangle(0, 0, cTexture.Width, cTexture.Height) : targetArea;
        }
    }
}

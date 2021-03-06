﻿using BmFont;
using Microsoft.Xna.Framework.Graphics;
using PlatoTK.UI.Components;
using PlatoTK.UI.Styles;
using StardewValley.Menus;
using System.Collections.Generic;

namespace PlatoTK.UI
{
    public interface IUIHelper
    {
        void RegisterStyle(IStyle style);

        void RegisterComponent(IComponent component);

        IWrapper LoadFromFile(string layoutPath, string id = "");

        bool TryGetStyle(string propertyName, out IStyle style, string option = "");

        bool TryGetComponent(string componentName, out IComponent component);

        IClickableMenu OpenMenu(IWrapper wrapper);

        List<Texture2D> LoadFontPages(FontFile fontFile, string assetName);

        FontFile LoadFontFile(string assetName);

        Dictionary<char, FontChar> ParseCharacterMap(FontFile fontFile);

        SpriteFont LoadSpriteFont(string assetName);
    }
}

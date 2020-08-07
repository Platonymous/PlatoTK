﻿using PlatoTK.Content;
using PlatoTK.Harmony;
using PlatoTK.Network;
using PlatoTK.UI;
using StardewValley;
using System;

namespace PlatoTK
{
    public interface IPlatoHelper
    {
        ISharedDataHelper SharedData { get; }
        IHarmonyHelper Harmony { get; }
        IContentHelper Content { get; }
        IUIHelper UI { get; }
        StardewModdingAPI.IModHelper ModHelper { get; }
        DelayedAction SetDelayedAction(int delay, Action action);
    }
}
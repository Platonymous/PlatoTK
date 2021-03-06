﻿using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using xTile.Layers;

namespace PlatoTK.Events
{
    internal interface IPlatoEventsHelperInternal : IPlatoEventsHelper
    {
        void HandleAnswer(Response answer, Action callback, string lastQuestionKey);

        void HandleQuestion(string question, List<Response> choices, Action<string> setQuestion, Action<Response> addResponse, Action<Response> removeResponse, bool isTV, Action callback);

        void HandleChannelSelection(string name, TV tvInstance, Action callback);

        void HandleEventCommand(string[] commands, Event eventInstance, GameTime time, GameLocation location, Action callback, bool post);

        void HandleTileAction(string[] commands, Farmer who, GameLocation location, Point position, Action<bool> callback);
    }
}

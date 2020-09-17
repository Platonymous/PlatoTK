using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;

namespace PlatoTK.Events
{
    public interface IPlatoEventsHelper
    {
        event EventHandler<IQuestionRaisedEventArgs> QuestionRaised;
        event EventHandler<IQuestionAnsweredEventArgs> QuestionAnswered;
        event EventHandler<ITVChannelSelectedEventArgs> TVChannelSelected;
        event EventHandler<ICallingEventCommandEventArgs> CallingEventCommand;
        event EventHandler<ICalledEventCommandEventArgs> CalledEventCommand;
        event EventHandler<ICallingTileActionEventArgs> CallingTileAction;

        void HandleAnswer(Response answer, Action callback, string lastQuestionKey);
        void HandleQuestion(string question, List<Response> choices, Action<string> setQuestion, Action<Response> addResponse, Action<Response> removeResponse, bool isTV, Action callback);
        void HandleChannelSelection(string name, TV tvInstance, Action callback);
        void HandleEventCommand(string[] commands, Event eventInstance, GameTime time, GameLocation location, Action callback, bool post);
        void HandleTileAction(string[] commands, Farmer who, GameLocation location, Point position, Action<bool> callback);
    }
}

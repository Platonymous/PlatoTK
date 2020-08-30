using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlatoTK.Events
{
    public interface IPlatoEventsHelper
    {
        event EventHandler<IQuestionRaisedEventArgs> QuestionRaised;
        event EventHandler<IQuestionAnsweredEventArgs> QuestionAnswered;
        event EventHandler<ITVChannelSelectedEventArgs> TVChannelSelected;
    }

    internal interface IPlatoEventsHelperInternal : IPlatoEventsHelper
    {
        void HandleAnswer(Response answer, Action callback, string lastQuestionKey);

        void HandleQuestion(string question, List<Response> choices, Action<string> setQuestion, Action<Response> addResponse, Action<Response> removeResponse, bool isTV, Action callback);

        void HandleChannelSelection(string name, TV tvInstance, Action callback);

    }

    public interface ITVChannelSelectedEventArgs
    {
        TV TVInstance { get; }
        string ChannelName { get; }
        Vector2 ScreenPosition { get; }
        float ScreenLayerDepth { get; }
        float OverlayLayerDepth { get; }
        float Scale { get; }

        void ShowScene(TemporaryAnimatedSprite screen, TemporaryAnimatedSprite overlay, string dialogue, Action nextAction);

        void TurnOffTV();

        void PreventDefault();
    }

    public interface IQuestionRaisedEventArgs
    {
        string Question { get; }

        string LastQuestionKey { get; }
        List<Response> Choices { get; }
        bool IsTV { get; }
        void RemoveResponse(Response respone);
        void AddResponse(Response response);
        void SetQuestion(string Question);
        void PaginateResponses();
    }

    public interface IQuestionAnsweredEventArgs
    {
        Response Answer { get; }

        string LastQuestionKey { get; }

        void PreventDefault();
    }
}

using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using PlatoTK.Reflection;
using StardewModdingAPI;

namespace PlatoTK.Events
{
    internal class PlatoEventsHelper : IPlatoEventsHelperInternal
    {
        public event EventHandler<IQuestionRaisedEventArgs> QuestionRaised;
        public event EventHandler<IQuestionAnsweredEventArgs> QuestionAnswered;
        public event EventHandler<ITVChannelSelectedEventArgs> TVChannelSelected;

        public PlatoEventsHelper()
        {
            Patching.EventPatches.InitializePatch();
        }

        public void HandleAnswer(Response answer, Action callback, string lastQuestionKey)
        {
            QuestionAnswered?.Invoke(this, new QuestionAnsweredEventArgs(answer, callback, lastQuestionKey));
        }

        public void HandleQuestion(string question, List<Response> choices, Action<string> setQuestion, Action<Response> addResponse, Action<Response> removeResponse, bool isTV, Action callback)
        {
            QuestionRaised?.Invoke(this, new QuestionRaisedEventArgs(question, choices, setQuestion, addResponse, removeResponse, isTV, callback));
        }

        public void HandleChannelSelection(string name, TV tvInstance, Action callback)
        {
            TVChannelSelected?.Invoke(this, new TVChannelSelectedEventArgs(name, tvInstance, callback));
        }
    }

    internal class TVChannelSelectedEventArgs : ITVChannelSelectedEventArgs
    {
        internal Action Callback { get; }

        public TV TVInstance { get; }

        public string ChannelName { get; }

        public Vector2 ScreenPosition => TVInstance.getScreenPosition();

        public float ScreenLayerDepth => (float)((TVInstance.boundingBox.Bottom - 1) / 10000.0 + 9.99999974737875E-06);

        public float OverlayLayerDepth => (float)((TVInstance.boundingBox.Bottom - 1) / 10000.0 + 1.99999994947575E-05);

        public float Scale => TVInstance.getScreenSizeModifier();

        public TVChannelSelectedEventArgs(string name, TV tvInstance, Action callback)
        {
            ChannelName = name;
            TVInstance = tvInstance;
            Callback = callback;
        }

        public void ShowScene(TemporaryAnimatedSprite screen, TemporaryAnimatedSprite screenOverlay, string dialogue, Action nextAction)
        {
            TVInstance.SetFieldValue("screen",screen);
            TVInstance.SetFieldValue("screenOverlay", screenOverlay);
            
            if(!string.IsNullOrEmpty(dialogue))
                Game1.drawObjectDialogue(Game1.parseText(dialogue));

            Game1.afterDialogues = new Game1.afterFadeFunction(nextAction);
        }

        public void TurnOffTV()
        {
            TVInstance.turnOffTV();
        }

        public void PreventDefault()
        {
            Callback();
        }
    }

    internal class QuestionAnsweredEventArgs : IQuestionAnsweredEventArgs
    {
        public Response Answer { get; }

        public string LastQuestionKey { get; }

        internal readonly Action Callback;

        public QuestionAnsweredEventArgs(Response answer, Action callback, string lastQuestionKey)
        {
            Answer = answer;
            Callback = callback;
            LastQuestionKey = lastQuestionKey;
        }

        public void PreventDefault()
        {
            Callback();
        }
    }

    internal class QuestionRaisedEventArgs : IQuestionRaisedEventArgs
    {
        internal Action Callback { get; }
        public string Question { get; }
        public string LastQuestionKey => Game1.currentLocation.lastQuestionKey;

        public bool IsTV { get; }
        
        public List<Response> Choices { get; }

        internal readonly Action<string> SetQuestionCallback;
        internal readonly Action<Response> AddResponseCallback;
        internal readonly Action<Response> RemoveResponseCallback;

        public QuestionRaisedEventArgs(string question, List<Response> choices, Action<string> setQuestion, Action<Response> addResponse, Action<Response> removeResponse, bool isTV, Action callback)
        {
            Question = question;
            SetQuestionCallback = setQuestion;
            AddResponseCallback = addResponse;
            RemoveResponseCallback = removeResponse;
            Choices = choices;
            IsTV = isTV;
            Callback = callback;
        }

        public void RemoveResponse(Response respone)
        {
            RemoveResponseCallback(respone);
        }

        public void AddResponse(Response response)
        {
            AddResponseCallback(response);
        }

        public void SetQuestion(string question)
        {
            SetQuestionCallback(question);
        }

        public void PaginateResponses()
        {
            Callback();
        }
    }
}

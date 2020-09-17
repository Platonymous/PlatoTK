using StardewValley;
using System;

namespace PlatoTK.Events
{
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
}

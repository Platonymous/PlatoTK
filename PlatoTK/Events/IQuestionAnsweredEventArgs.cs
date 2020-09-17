using StardewValley;

namespace PlatoTK.Events
{
    public interface IQuestionAnsweredEventArgs
    {
        Response Answer { get; }

        string LastQuestionKey { get; }

        void PreventDefault();
    }
}

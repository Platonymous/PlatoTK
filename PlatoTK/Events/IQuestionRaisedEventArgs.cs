using StardewValley;
using System.Collections.Generic;

namespace PlatoTK.Events
{
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
}

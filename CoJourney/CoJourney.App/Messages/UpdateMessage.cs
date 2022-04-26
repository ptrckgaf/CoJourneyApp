using CoJourney.BL.Models;

namespace CoJourney.App.Messages
{
    public record UpdateMessage<T> : Message<T>
        where T : IModel
    {
    }
}

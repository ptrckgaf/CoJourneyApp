using CoJourney.BL.Models;

namespace CoJourney.App.Messages
{
    public record AddedMessage<T> : Message<T>
        where T : IModel
    {
    }
}
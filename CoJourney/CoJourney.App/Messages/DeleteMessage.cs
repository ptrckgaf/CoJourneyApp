using CoJourney.BL.Models;

namespace CoJourney.App.Messages
{
    public record DeleteMessage<T> : Message<T>
        where T : IModel
    {
    }
}
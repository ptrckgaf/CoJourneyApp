using CoJourney.BL.Models;

namespace CoJourney.App.Messages
{
    public record LoadMessage<T> : Message<T>
        where T : IModel
    {
    }
}

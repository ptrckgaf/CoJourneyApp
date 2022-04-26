using CoJourney.BL.Models;

namespace CoJourney.App.Messages
{
    public record NewMessage<T> : Message<T>
        where T : IModel
    {
    }
}

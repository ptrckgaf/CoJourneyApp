using CoJourney.BL.Models;

namespace CoJourney.App.Messages
{
    public record SelectedMessage<T> : Message<T>
        where T : IModel
    {
    }
}

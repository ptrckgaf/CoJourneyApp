namespace CoJourney.App.Factories
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
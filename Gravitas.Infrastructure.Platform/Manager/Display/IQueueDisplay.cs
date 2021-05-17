namespace Gravitas.Infrastructure.Platform.Manager.Display
{
    public interface IQueueDisplay
    {
        bool WriteText(string text, int time);
    }
}
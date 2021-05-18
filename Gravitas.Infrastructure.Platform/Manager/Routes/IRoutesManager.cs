namespace Gravitas.Infrastructure.Platform.Manager.Routes
{
    public interface IRoutesManager
    {
        bool IsNodeNext(int ticketId, int nodeId, out string errorMessage);
    }
}
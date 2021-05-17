namespace Gravitas.Infrastructure.Platform.Manager
{
    public interface IRoutesManager
    {
        bool IsNodeNext(long ticketId, long nodeId, out string errorMessage);
    }
}
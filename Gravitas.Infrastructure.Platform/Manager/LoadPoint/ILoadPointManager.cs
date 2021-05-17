namespace Gravitas.Infrastructure.Platform.Manager.LoadPoint
{
    public interface ILoadPointManager
    {
        bool ConfirmLoadGuide(long ticketId, string employeeId);
        bool ConfirmLoadPoint(long ticketId, string employeeId);
    }
}
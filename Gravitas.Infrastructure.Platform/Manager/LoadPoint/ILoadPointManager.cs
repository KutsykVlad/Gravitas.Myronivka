namespace Gravitas.Infrastructure.Platform.Manager.LoadPoint
{
    public interface ILoadPointManager
    {
        bool ConfirmLoadGuide(int ticketId, string employeeId);
        bool ConfirmLoadPoint(int ticketId, string employeeId);
    }
}
using Gravitas.Model;
using Gravitas.Model.DomainModel.Queue.DAO;

namespace Gravitas.DAL
{
    public interface IQueueRegisterRepository : IBaseRepository<GravitasDbContext>
    {
        void Register(QueueRegister newRegistration);
        void CalledFromQueue(long ticketContainerId);
        bool IsAllowedToEnter(long ticketContainerId);
        bool SMSAlreadySent(long ticketContainerId);
        void OnSMSSending(long ticketContainerId);
        void RemoveFromQueue(long ticketContainerId);
    }
}
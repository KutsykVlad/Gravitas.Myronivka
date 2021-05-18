using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.Queue.DAO;

namespace Gravitas.DAL.Repository.Queue
{
    public interface IQueueRegisterRepository : IBaseRepository
    {
        void Register(QueueRegister newRegistration);
        void CalledFromQueue(int ticketContainerId);
        bool IsAllowedToEnter(int ticketContainerId);
        bool SMSAlreadySent(int ticketContainerId);
        void OnSMSSending(int ticketContainerId);
        void RemoveFromQueue(int ticketContainerId);
    }
}
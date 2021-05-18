using System.Linq;
using Gravitas.Model.DomainModel.PackingTare.DTO;

namespace Gravitas.DAL.Repository.PackingTare
{
    public interface IPackingTareRepository
    {
        IQueryable<PackingTareVm> GetTareList();
        IQueryable<TicketPackingTareVm> GetTicketTareList(int ticketId);
        bool Add(PackingTareVm model);
        void Remove(int id);
        void RemoveTicketTare(int ticketId);
        float GetTareWeight(int tareId);
        bool AddTicketTare(int ticketId, int tareId, int count);
    }
}
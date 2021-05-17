using System.Collections.Generic;
using System.Linq;
using Gravitas.Model.DomainModel.PackingTare.DTO;

namespace Gravitas.DAL.Repository.PackingTare
{
    public interface IPackingTareRepository
    {
        IQueryable<PackingTareVm> GetTareList();
        IQueryable<TicketPackingTareVm> GetTicketTareList(long ticketId);
        bool Add(PackingTareVm model);
        void Remove(long id);
        void RemoveTicketTare(long ticketId);
        float GetTareWeight(long tareId);
        bool AddTicketTare(long ticketId, long tareId, int count);
    }
}
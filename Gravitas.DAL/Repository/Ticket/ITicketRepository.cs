using System.Collections.Generic;
using Gravitas.Model;

namespace Gravitas.DAL
{
    public interface ITicketRepository : IBaseRepository<GravitasDbContext>
    {
        Ticket GetTicketInContainer(long containerId, int ticketStatus);

        Model.Dto.TicketItems GetTicketItems(long containerId);

        TicketContainer NewTicketContainer();
        Ticket NewTicket(long ticketContainerId);

        IEnumerable<TicketFile> GetTicketFiles(long ticketId);
        IEnumerable<TicketFile> GetTicketFilesByType(int typeId);
    }
}
using System.Collections.Generic;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Ticket.DAO;
using TicketStatus = Gravitas.Model.DomainValue.TicketStatus;

namespace Gravitas.DAL.Repository.Ticket
{
    public interface ITicketRepository : IBaseRepository
    {
        Model.DomainModel.Ticket.DAO.Ticket GetTicketInContainer(int ticketContainerId, TicketStatus ticketStatus);
        Model.Dto.TicketItems GetTicketItems(int ticketContainerId);
        TicketContainer NewTicketContainer();
        Model.DomainModel.Ticket.DAO.Ticket NewTicket(int ticketContainerId);
        IEnumerable<TicketFile> GetTicketFiles(int ticketId);
        IEnumerable<TicketFile> GetTicketFilesByType(int typeId);
    }
}
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.Dto;
using TicketContainerStatus = Gravitas.Model.DomainValue.TicketContainerStatus;
using TicketFileType = Gravitas.Model.DomainValue.TicketFileType;
using TicketStatus = Gravitas.Model.DomainValue.TicketStatus;

namespace Gravitas.DAL.Repository.Ticket
{
    public class TicketRepository : BaseRepository, ITicketRepository
    {
        private readonly GravitasDbContext _context;

        public TicketRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public Model.DomainModel.Ticket.DAO.Ticket GetTicketInContainer(int containerId, TicketStatus ticketStatus)
        {
            switch (ticketStatus)
            {
                case TicketStatus.New:
                    return GetTicketInContainer_New(containerId);
                case TicketStatus.Blank: break;
                case TicketStatus.ToBeProcessed:
                    return GetTicketInContainer_ToBeProcessed(containerId);
                case TicketStatus.Completed: break;
                case TicketStatus.Canceled: break;
            }

            // First attempt
            var ticket = (from t in _context.Tickets
                    where t.TicketContainerId == containerId && t.StatusId == ticketStatus
                    orderby t.OrderNo descending 
                    select t)
                    .FirstOrDefault();
            return ticket;
        }

        public TicketItems GetTicketItems(int containerId)
        {
            var dao = (from i in _context.Tickets
                       join s in _context.SingleWindowOpDatas on i.Id equals s.TicketId into singleWindowJoin
                       from singleWindowOpData in singleWindowJoin.DefaultIfEmpty()
                       join p in _context.Products on singleWindowOpData.ProductId equals p.Id into productJoin
                       from product in productJoin.DefaultIfEmpty()
                       where i.TicketContainerId == containerId
                       select new TicketItem
                       {
                           Id = i.Id,
                           StatusId = i.StatusId,
                           StatusName = i.TicketStatus.Name,
                           SupplyCode = singleWindowOpData != null ? singleWindowOpData.SupplyCode : null,
                           Product = product != null ? product.ShortName : string.Empty
                       }).ToList();

            var dto = new TicketItems {Items = dao};

            return dto;
        }

        public TicketContainer NewTicketContainer()
        {
            var ticketContainer = new TicketContainer
            {
                StatusId = TicketContainerStatus.Active
            };
            Add<TicketContainer, int>(ticketContainer);
            return ticketContainer;
        }

        public Model.DomainModel.Ticket.DAO.Ticket NewTicket(int ticketContainerId)
        {
            var ticketContainer = _context.TicketContainers.AsNoTracking().First(x => x.Id == ticketContainerId);

            var ticket = new Model.DomainModel.Ticket.DAO.Ticket
            {
                TicketContainerId = ticketContainer.Id,
                StatusId = TicketStatus.New,
                OrderNo = ticketContainer.TicketSet.Any()
                    ? ticketContainer.TicketSet.Max(t => t.OrderNo) + 1
                    : 1
            };
            Add<Model.DomainModel.Ticket.DAO.Ticket, int>(ticket);

            return ticket;
        }

        public IEnumerable<TicketFile> GetTicketFiles(int ticketId) => _context.TicketFiles.Where(item => item.TicketId == ticketId).ToList();

        public IEnumerable<TicketFile> GetTicketFilesByType(TicketFileType typeId) => _context.TicketFiles.Where(item => item.TypeId == typeId).ToList();

        private Model.DomainModel.Ticket.DAO.Ticket GetTicketInContainer_New(int containerId)
        {
            var ticket = _context.Tickets
                .AsNoTracking()
                .Where(t => t.TicketContainerId == containerId && t.StatusId == TicketStatus.New)
                .OrderByDescending(t => t.OrderNo)
                .FirstOrDefault();

            if (ticket != null)
            {
                var ticketsToBeCanceled = _context.Tickets
                    .AsNoTracking()
                    .Where(t => t.TicketContainerId == containerId && t.StatusId == TicketStatus.New && t.Id != ticket.Id)
                    .ToList();

                foreach (var t in ticketsToBeCanceled)
                {
                    t.StatusId = TicketStatus.Canceled;
                    Update<Model.DomainModel.Ticket.DAO.Ticket, int>(t);
                }
            }

            return ticket;
        }

        private Model.DomainModel.Ticket.DAO.Ticket GetTicketInContainer_ToBeProcessed(int containerId)
        {
            var ticket = _context.Tickets
                .AsNoTracking()
                .Where(t => t.TicketContainerId == containerId && t.StatusId == TicketStatus.ToBeProcessed)
                .OrderBy(t => t.OrderNo)
                .FirstOrDefault();

            if (ticket != null) return ticket;

            ticket = _context.Tickets
                .AsNoTracking()
                .Where(t => t.TicketContainerId == containerId && t.StatusId == TicketStatus.Blank)
                .OrderBy(t => t.OrderNo)
                .FirstOrDefault();

            if (ticket != null)
            {
                ticket.StatusId = TicketStatus.ToBeProcessed;
                Update<Model.DomainModel.Ticket.DAO.Ticket, int>(ticket);
            }

            return ticket;
        }
    }
}
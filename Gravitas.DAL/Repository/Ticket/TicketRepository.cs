using System.Collections.Generic;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.Dto;

namespace Gravitas.DAL
{
    public class TicketRepository : BaseRepository<GravitasDbContext>, ITicketRepository
    {
        private readonly GravitasDbContext _context;

        public TicketRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public Ticket GetTicketInContainer(long containerId, int ticketStatus)
        {
            switch (ticketStatus)
            {
                case Dom.Ticket.Status.New:
                    return GetTicketInContainer_New(containerId);
                case Dom.Ticket.Status.Blank: break;
                case Dom.Ticket.Status.ToBeProcessed:
                    return GetTicketInContainer_ToBeProcessed(containerId);
                case Dom.Ticket.Status.Completed: break;
                case Dom.Ticket.Status.Canceled: break;
            }

            // First attempt
            var ticket = (from t in _context.Tickets
                    where t.ContainerId == containerId && t.StatusId == ticketStatus
                    orderby t.OrderNo descending 
                    select t)
                    .FirstOrDefault();
            return ticket;
        }

        public TicketItems GetTicketItems(long containerId)
        {
            var dao = (from i in _context.Tickets
                       join s in _context.SingleWindowOpDatas on i.Id equals s.TicketId into singleWindowJoin
                       from singleWindowOpData in singleWindowJoin.DefaultIfEmpty()
                       join p in _context.Products on singleWindowOpData.ProductId equals p.Id into productJoin
                       from product in productJoin.DefaultIfEmpty()
                       where i.ContainerId == containerId
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
                StatusId = Dom.TicketContainer.Status.Active
            };
            Add<TicketContainer, long>(ticketContainer);
            return ticketContainer;
        }

        public Ticket NewTicket(long ticketContainerId)
        {
            var ticketContainer = _context.TicketContainers.AsNoTracking().First(x => x.Id == ticketContainerId);

            var ticket = new Ticket
            {
                ContainerId = ticketContainer.Id,
                StatusId = Dom.Ticket.Status.New,
                OrderNo = ticketContainer.TicketSet.Any()
                    ? ticketContainer.TicketSet.Max(t => t.OrderNo) + 1
                    : 1
            };
            Add<Ticket, long>(ticket);

            return ticket;
        }

        public IEnumerable<TicketFile> GetTicketFiles(long ticketId) => _context.TicketFiles.Where(item => item.TicketId == ticketId).ToList();

        public IEnumerable<TicketFile> GetTicketFilesByType(int typeId) => _context.TicketFiles.Where(item => item.TypeId == typeId).ToList();

        private Ticket GetTicketInContainer_New(long containerId)
        {
            var ticket = _context.Tickets
                .AsNoTracking()
                .Where(t => t.ContainerId == containerId && t.StatusId == Dom.Ticket.Status.New)
                .OrderByDescending(t => t.OrderNo)
                .FirstOrDefault();

            if (ticket != null)
            {
                var ticketsToBeCanceled = _context.Tickets
                    .AsNoTracking()
                    .Where(t => t.ContainerId == containerId && t.StatusId == Dom.Ticket.Status.New && t.Id != ticket.Id)
                    .ToList();

                foreach (var t in ticketsToBeCanceled)
                {
                    t.StatusId = Dom.Ticket.Status.Canceled;
                    Update<Ticket, long>(t);
                }
            }

            return ticket;
        }

        private Ticket GetTicketInContainer_ToBeProcessed(long containerId)
        {
            var ticket = _context.Tickets
                .AsNoTracking()
                .Where(t => t.ContainerId == containerId && t.StatusId == Dom.Ticket.Status.ToBeProcessed)
                .OrderBy(t => t.OrderNo)
                .FirstOrDefault();

            if (ticket != null) return ticket;

            ticket = _context.Tickets
                .AsNoTracking()
                .Where(t => t.ContainerId == containerId && t.StatusId == Dom.Ticket.Status.Blank)
                .OrderBy(t => t.OrderNo)
                .FirstOrDefault();

            if (ticket != null)
            {
                ticket.StatusId = Dom.Ticket.Status.ToBeProcessed;
                Update<Ticket, long>(ticket);
            }

            return ticket;
        }
    }
}
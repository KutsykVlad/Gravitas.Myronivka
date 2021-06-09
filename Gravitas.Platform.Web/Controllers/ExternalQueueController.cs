using System.Linq;
using System.Web.Mvc;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Queue;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.Queue.Infrastructure;
using Gravitas.Model.DomainModel.Queue.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel.Queue;

namespace Gravitas.Platform.Web.Controllers
{
    public class ExternalQueueController : Controller
    {
        private readonly IQueueRegisterRepository _queueRegisterRepository;
        private readonly IQueueInfrastructure _queueInfrastructure;
        private readonly IConnectManager _connectManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly GravitasDbContext _context;

        public ExternalQueueController(IQueueRegisterRepository queueRegisterRepository, 
            IQueueInfrastructure queueInfrastructure, 
            IConnectManager connectManager, 
            ITicketRepository ticketRepository, 
            GravitasDbContext context)
        {
            _queueRegisterRepository = queueRegisterRepository;
            _queueInfrastructure = queueInfrastructure;
            _connectManager = connectManager;
            _ticketRepository = ticketRepository;
            _context = context;
        }

        [HttpGet]
        public ActionResult List()
        {
            var vm = new ExternalQueueVm
            {
                Items =  _queueRegisterRepository.GetQuery<QueueRegister, int>().Select(s=> new ExternalQueueItemVm()
                {
                    TicketContainerId = s.TicketContainerId, 
                    TrailerPlate = s.TrailerPlate,
                    TruckPlate = s.TruckPlate,
                    PhoneNumber = s.PhoneNumber,
                    RouteId = s.RouteTemplateId, 
                    IsAllowedToEnterTerritory = s.IsAllowedToEnterTerritory
                })
            };
            return View("List", vm);
        }
        
        public ActionResult Call(int ticketContainerId)
        {
            _queueInfrastructure.ImmediateEntrance(ticketContainerId);
            var ticketId = _ticketRepository.GetTicketInContainer(ticketContainerId, TicketStatus.ToBeProcessed)?.Id;
            var card = _context.Cards.First(x => x.TicketContainerId == ticketContainerId);
            _connectManager.SendSms(SmsTemplate.EntranceApprovalSms, ticketId, cardId: card.Id);
            return RedirectToAction("PreRegisteredQueue");
        }

        [HttpGet]
        public ActionResult PreRegisteredQueue() => View("FilteredList/List");
    }
}

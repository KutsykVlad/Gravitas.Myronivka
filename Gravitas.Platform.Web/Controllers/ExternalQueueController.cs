using System.Linq;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.DAL.Repository.Queue;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.Queue.Infrastructure;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Queue.DAO;
using Gravitas.Platform.Web.ViewModel.Queue;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Platform.Web.Controllers
{
    public class ExternalQueueController : Controller
    {
        private readonly IQueueRegisterRepository _queueRegisterRepository;
        private readonly IQueueInfrastructure _queueInfrastructure;
        private readonly IConnectManager _connectManager;
        private readonly ITicketRepository _ticketRepository;

        public ExternalQueueController(IQueueRegisterRepository queueRegisterRepository, 
            IQueueInfrastructure queueInfrastructure, 
            IConnectManager connectManager, 
            ITicketRepository ticketRepository)
        {
            _queueRegisterRepository = queueRegisterRepository;
            _queueInfrastructure = queueInfrastructure;
            _connectManager = connectManager;
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        public ActionResult List()
        {
            var vm = new ExternalQueueVm
            {
                Items =  _queueRegisterRepository.GetQuery<QueueRegister, long>().Select(s=> new ExternalQueueItemVm()
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
        
        public ActionResult Call(long ticketContainerId)
        {
            _queueInfrastructure.ImmediateEntrance(ticketContainerId);
            var ticketId = _ticketRepository.GetTicketInContainer(ticketContainerId, Dom.Ticket.Status.ToBeProcessed)?.Id;
            _connectManager.SendSms(Dom.Sms.Template.EntranceApprovalSms, ticketId);
            return RedirectToAction("PreRegisteredQueue");
        }

        [HttpGet]
        public ActionResult PreRegisteredQueue() => View("FilteredList/List");
    }
}

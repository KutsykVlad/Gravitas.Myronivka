using System;
using System.Linq;
using System.Web.Mvc;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.OwnTransport;
using Gravitas.Infrastructure.Platform.ApiClient.Devices;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.OwnTransport.DTO;
using Gravitas.Model.DomainValue;

namespace Gravitas.Platform.Web.Controllers
{
    public class OwnTransportController : Controller
    {
        private readonly IOwnTransportRepository _ownTransportRepository;
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly GravitasDbContext _context;

        public OwnTransportController(IOwnTransportRepository ownTransportRepository, 
            IOpRoutineManager opRoutineManager, 
            GravitasDbContext context)
        {
            _ownTransportRepository = ownTransportRepository;
            _opRoutineManager = opRoutineManager;
            _context = context;
        }

        public ActionResult Get()
        {
            var items = _ownTransportRepository.GetList().ToList();
            return View(items);
        }

        public ActionResult Remove(int id)
        {
            _ownTransportRepository.Remove(id);
            return RedirectToAction("Get");
        }

        public ActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Add(OwnTransportVm vm)
        {
            var rfidState = (RfidObidRwState) DeviceSyncManager.GetDeviceState(10000300);
            
            var card = _context.Cards.FirstOrDefault(e => 
                e.Id.Equals(rfidState.InData.Rifd, StringComparison.CurrentCultureIgnoreCase));

            if (card != null && _opRoutineManager.IsRfidCardValid(out var cardErrMsg, card, CardType.TicketCard))
            {
                vm.Card = card.Id;
                if (ModelState.IsValid)
                {
                    var isAdded = _ownTransportRepository.Add(vm);
                    if (isAdded) return RedirectToAction("Get");
                }
            }
        
            return View(vm);
        }
    }
}
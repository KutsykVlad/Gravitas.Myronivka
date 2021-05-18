using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.OwnTransport;
using Gravitas.Infrastructure.Platform.ApiClient.Devices;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.OwnTransport.DTO;
using Gravitas.Model.Dto;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Platform.Web.Controllers
{
    public class OwnTransportController : Controller
    {
        private readonly IOwnTransportRepository _ownTransportRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly GravitasDbContext _context;

        public OwnTransportController(IOwnTransportRepository ownTransportRepository, 
            IDeviceRepository deviceRepository, 
            ICardRepository cardRepository, 
            IOpRoutineManager opRoutineManager, 
            GravitasDbContext context)
        {
            _ownTransportRepository = ownTransportRepository;
            _deviceRepository = deviceRepository;
            _cardRepository = cardRepository;
            _opRoutineManager = opRoutineManager;
            _context = context;
        }

        public ActionResult Get()
        {
            var items = _ownTransportRepository.GetList().ToList();
            return View(items);
        }

        public ActionResult Remove(long id)
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

            if (card != null && _opRoutineManager.IsRfidCardValid(out var cardErrMsg, card, Dom.Card.Type.TicketCard))
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
using System.Web.Mvc;
using Gravitas.DAL.Repository.Card;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel.SelfServiceLaboratory;

namespace Gravitas.Platform.Web.Controllers
{
    public class SelfServiceLaboratoryController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IOpDataRepository _opDataRepository;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly ICardRepository _cardRepository;

        public SelfServiceLaboratoryController(ITicketRepository ticketRepository, 
            IOpDataRepository opDataRepository, 
            IExternalDataRepository externalDataRepository, 
            ICardRepository cardRepository)
        {
            _ticketRepository = ticketRepository;
            _opDataRepository = opDataRepository;
            _externalDataRepository = externalDataRepository;
            _cardRepository = cardRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetailByTicketContainer(int id)
        {
            var ticketId = _ticketRepository.GetTicketInContainer(id, TicketStatus.Processing)?.Id;
            var labOpData = _opDataRepository.GetLastOpData<LabFacelessOpData>(ticketId, null);

            var vm = new SelfServiceLaboratoryDetailVm();

            var singleWindowOpData = _opDataRepository.GetLastOpData<SingleWindowOpData>(ticketId, null);
            if (singleWindowOpData != null)
            {
                vm.ProductName = _externalDataRepository.GetProductDetail(singleWindowOpData.ProductId.Value)?.ShortName ??
                                 string.Empty;
                vm.PartnerName = singleWindowOpData.CarrierId.HasValue
                    ? _externalDataRepository.GetPartnerDetail(singleWindowOpData.CarrierId.Value)?.ShortName
                    : singleWindowOpData.CustomPartnerName ?? string.Empty;

                if (singleWindowOpData.IsThirdPartyCarrier)
                {
                    vm.TransportNo = singleWindowOpData.TransportNumber;
                    vm.TrailerNo = singleWindowOpData.TrailerNumber;
                }
                else
                {
                    vm.TransportNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId.Value)?.RegistrationNo ?? string.Empty;
                    vm.TrailerNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId.Value)?.RegistrationNo ?? string.Empty;
                }
            }

            if (labOpData != null)
            {
                vm.HumidityValue = labOpData.HumidityValue;
                vm.ImpurityValue = labOpData.ImpurityValue;
                vm.EffectiveValue = labOpData.EffectiveValue;
                vm.IsInfectioned = labOpData.InfectionedClassId;
                vm.Comment = labOpData.Comment;
            }
            
            vm.CardNumber = _cardRepository.GetContainerCardNo(id);
            
            return View(vm);
        }
    }
}
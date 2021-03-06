using System.Linq;
using System.Net;
using System.Web.Mvc;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Platform.Manager.OpData;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class CentralLaboratorySamplesController : Controller
    {

        private readonly INodeRepository _nodeRepository;
        private readonly IOpDataManager _opDataManager;
        private readonly GravitasDbContext _context;

        public CentralLaboratorySamplesController(INodeRepository nodeRepository, 
            IOpDataManager opDataManager, 
            GravitasDbContext context)
        {
            _nodeRepository = nodeRepository;
            _opDataManager = opDataManager;
            _context = context;
        }
        #region 01_Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(int nodeId)
        {
            var routineData = new CentralLabolatorySamples.IdleVm { NodeId = nodeId };
            return PartialView("../OpRoutine/CentralLabolatorySamples/01_Idle", routineData);
        }
        #endregion

        #region 02_CentralLabSampleBindTray
        [HttpGet, ChildActionOnly]
        public ActionResult CentralLabSampleBindTray(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var node = _nodeRepository.GetNodeDto(nodeId);
            if (node?.Context.TicketId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var data = _opDataManager.GetBasicTicketData(node.Context.TicketId.Value);
            var routineData = new CentralLabolatorySamples.CentralLabSampleBindTrayVm
            {
                NodeId = nodeId.Value,
                ProductName = data.ProductName,
                IsThirdPartyCarrier = data.IsThirdPartyCarrier,
                CardNumber = _context.Cards.FirstOrDefault(x => 
                    x.TicketContainerId == node.Context.TicketContainerId 
                    && x.TypeId == CardType.TicketCard)?.No.ToString(),
                TransportNo = data.TransportNo,
                TrailerNo = data.TrailerNo
            };

            return PartialView("../OpRoutine/CentralLabolatorySamples/02_CentralLabSampleBindTray", routineData);
        }
        #endregion

        #region 03_CentralLabSampleAddOpVisa

        [HttpGet, ChildActionOnly]
        public ActionResult CentralLabSampleAddOpVisa(int nodeId)
        {
            var routineData = new CentralLabolatorySamples.CentralLabSampleAddOpVisaVm { NodeId = nodeId };
            return PartialView("../OpRoutine/CentralLabolatorySamples/03_CentralLabAddOpVisa", routineData);
        }

        #endregion
    }
}
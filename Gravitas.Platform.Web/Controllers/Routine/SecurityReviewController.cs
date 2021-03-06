using System.Net;
using System.Web.Mvc;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Platform.Manager.OpData;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class SecurityReviewController : Controller
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IOpDataManager _opDataManager;

        public SecurityReviewController(INodeRepository nodeRepository,
            IOpDataManager opDataManager)
        {
            _nodeRepository = nodeRepository;
            _opDataManager = opDataManager;
        }

        #region 01_Idle

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Idle(int nodeId)
        {
            var routineData = new SecurityReviewVms.IdleVm {NodeId = nodeId};
            return PartialView("../OpRoutine/SecurityReview/01_Idle", routineData);
        }

        #endregion

        #region 02_AddOperationVisa

        [HttpGet]
        [ChildActionOnly]
        public ActionResult AddOperationVisa(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SecurityReviewVms.AddOperationVisaVm
            {
                NodeId = nodeId.Value
            };
            
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketId != null)
                routineData.TruckBaseInfo = _opDataManager.GetBasicTicketData(nodeDto.Context.TicketId.Value);

            return PartialView("../OpRoutine/SecurityReview/02_AddOperationVisa", routineData);
        }

        #endregion
    }
}
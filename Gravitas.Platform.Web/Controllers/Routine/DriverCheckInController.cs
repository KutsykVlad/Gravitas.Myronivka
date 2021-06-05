using System.Web.Mvc;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.ViewModel.OpRoutine.DriverCheckIn;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class DriverCheckInController : Controller
    {
        private readonly IOpRoutineWebManager _opRoutineWebManager;

        public DriverCheckInController(IOpRoutineWebManager opRoutineWebManager)
        {
            _opRoutineWebManager = opRoutineWebManager;
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Idle(int nodeId)
        {
            var vm = new DriverCheckInVms.IdleVm { NodeId = nodeId };
            return PartialView("../OpRoutine/DriverCheckIn/01_Idle", vm);
        }
        
        public void CheckIn(int nodeId)
        {
            _opRoutineWebManager.DriverCheckIn_Idle_CheckIn(nodeId);
        }
        
        [HttpGet]
        [ChildActionOnly]
        public ActionResult AddDriver(int nodeId)
        {
            var vm = new DriverCheckInVms.AddDriverVm { NodeId = nodeId };
            return PartialView("../OpRoutine/DriverCheckIn/02_AddDriver", vm);
        }
        
        public void DriverCheckIn_AddDriver(DriverCheckInVms.AddDriverVm model)
        {
            _opRoutineWebManager.DriverCheckIn_AddDriver(model);
        }
        
        public void CheckIn_Return(int nodeId)
        {
            _opRoutineWebManager.DriverCheckIn_CheckIn_Idle(nodeId);
        }
        
        [HttpGet]
        [ChildActionOnly]
        public ActionResult DriverInfoCheck(int nodeId)
        {
            var vm = _opRoutineWebManager.GetDriverInfoCheckModel(nodeId);
            return PartialView("../OpRoutine/DriverCheckIn/03_DriverInfoCheck", vm);
        }
        
        public void DriverCheckIn_DriverInfoCheck(DriverCheckInVms.DriverInfoCheckVm model)
        {
            _opRoutineWebManager.DriverCheckIn_DriverInfoCheck(model);
        }
        
        [HttpGet]
        [ChildActionOnly]
        public ActionResult RegistrationConfirm(int nodeId)
        {
            var vm = _opRoutineWebManager.GetRegistrationConfirm(nodeId);
            return PartialView("../OpRoutine/DriverCheckIn/04_RegistrationConfirm", vm);
        }

    }
}
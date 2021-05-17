using System;
using Gravitas.Model;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
//        public LoadCheckPointVms.GetTareValue LoadCheckPoint_GetTareValue_GetVm(long nodeId)
//        {
//            var vm = new LoadCheckPointVms.GetTareValue {NodeId = nodeId};
//
//            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
//            if (nodeDto?.Context.TicketId == null) return vm;
//
//            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId);
//            vm.TareValue = singleWindowOpData.PackingWeightValue;
//
//            return vm;
//        }
        
//        public void LoadCheckPoint_GetTareValue_Confirm(LoadCheckPointVms.GetTareValue vm)
//        {
//            var nodeDto = _nodeRepository.GetNodeDto(vm.NodeId);
//            if (nodeDto?.Context.TicketId == null) return;
//            
//            if (vm.TareValue.HasValue)
//            {
//                var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId);
//                singleWindowOpData.PackingWeightValue = vm.TareValue;
//                _opDataRepository.Update<SingleWindowOpData, Guid>(singleWindowOpData);
//            }
//            
//            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadCheckPoint.State.AddOperationVisa;
//            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
//        }
//
//        public void LoadCheckPoint_AddOperationVisa_Back(long nodeId)
//        {
//            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
//            if (nodeDto?.Context.OpRoutineStateId == null) return;
//
//            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadCheckPoint.State.GetTareValue;
//            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
//        }
    }
}
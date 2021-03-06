namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public bool SecurityIn_Entry_Cancelation(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityIn.State.Idle;
            _nodeRepository.ClearNodeProcessingMessage(nodeId);
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            return UpdateNodeContext(nodeId, nodeDto.Context);
        }

        public void SecurityIn_CheckOwnTransport_Next(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityIn.State.AddOperationVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void SecurityIn_CheckOwnTransport_Reject(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityIn.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
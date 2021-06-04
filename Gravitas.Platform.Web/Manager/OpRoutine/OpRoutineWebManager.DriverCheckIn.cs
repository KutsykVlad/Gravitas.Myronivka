namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public void DriverCheckIn_Idle_CheckIn(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.DriverCheckIn.State.AddDriver;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public void DriverCheckIn_CheckIn_Idle(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.DriverCheckIn.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public void DriverCheckIn_CheckIn_DriverInfoCheck(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.DriverCheckIn.State.DriverInfoCheck;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public void DriverCheckIn_DriverInfoCheck_RegistrationConfirm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.DriverCheckIn.State.RegistrationConfirm;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
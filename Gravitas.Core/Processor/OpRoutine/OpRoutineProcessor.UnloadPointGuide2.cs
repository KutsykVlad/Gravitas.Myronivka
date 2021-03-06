using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.UnloadPoint;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainValue;

namespace Gravitas.Core.Processor.OpRoutine
{
    class UnloadPointGuide2OpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly IConnectManager _connectManager;
        private readonly IUserManager _userManager;
        private readonly IUnloadPointManager _unloadPointManager;

        public UnloadPointGuide2OpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            IConnectManager connectManager,
            IUserManager userManager,
            IUnloadPointManager unloadPointManager) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _connectManager = connectManager;
            _userManager = userManager;
            _unloadPointManager = unloadPointManager;
        }

        public override void Process()
        {
            ReadDbData();

            switch (NodeDetails.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.UnloadPointGuide2.State.Idle:
                    break;
                case Model.DomainValue.OpRoutine.UnloadPointGuide2.State.BindUnloadPoint:
                    break;
                case Model.DomainValue.OpRoutine.UnloadPointGuide2.State.AddOpVisa:
                    AddOperationVisa(NodeDetails);
                    break;
            }
        }

        private void AddOperationVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.TicketId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var unloadResult = _unloadPointManager.ConfirmUnloadGuide(nodeDetailsDto.Context.TicketId.Value, card.EmployeeId.Value);
            if (!unloadResult) return;

            _connectManager.SendSms(SmsTemplate.DestinationPointApprovalSms, nodeDetailsDto.Context.TicketId, cardId: card.Id);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointGuide2.State.Idle;
            nodeDetailsDto.Context.OpDataId = null;
            nodeDetailsDto.Context.OpProcessData = null;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
    }
}
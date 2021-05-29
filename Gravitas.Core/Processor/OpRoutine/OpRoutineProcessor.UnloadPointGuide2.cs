using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.UnloadPoint;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
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

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null)
            {
                return false;
            }

            var rfidValid = config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader);
            return rfidValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(NodeDetailsDto))
            {
                return;
            }

            switch (NodeDetailsDto.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.UnloadPointGuide2.State.Idle:
                    break;
                case Model.DomainValue.OpRoutine.UnloadPointGuide2.State.BindUnloadPoint:
                    break;
                case Model.DomainValue.OpRoutine.UnloadPointGuide2.State.AddOpVisa:
                    AddOperationVisa(NodeDetailsDto);
                    break;
            }
        }

        private void AddOperationVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.TicketId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var unloadResult = _unloadPointManager.ConfirmUnloadGuide(nodeDetailsDto.Context.TicketId.Value, card.EmployeeId);
            if (!unloadResult) return;

            if (!_connectManager.SendSms(SmsTemplate.DestinationPointApprovalSms, nodeDetailsDto.Context.TicketId))
            {
                Logger.Error("Sms hasn`t been sent");
            }

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointGuide2.State.Idle;
            nodeDetailsDto.Context.OpDataId = null;
            nodeDetailsDto.Context.OpProcessData = null;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
    }
}
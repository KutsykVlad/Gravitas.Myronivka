using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.UnloadPoint;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Dom = Gravitas.Model.DomainValue.Dom;

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

            var rfidValid = config.Rfid.ContainsKey(Dom.Node.Config.Rfid.TableReader);
            return rfidValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto))
            {
                return;
            }

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Dom.OpRoutine.UnloadPointGuide2.State.Idle:
                    break;
                case Dom.OpRoutine.UnloadPointGuide2.State.BindUnloadPoint:
                    break;
                case Dom.OpRoutine.UnloadPointGuide2.State.AddOpVisa:
                    AddOperationVisa(_nodeDto);
                    break;
            }
        }

        private void AddOperationVisa(Node nodeDto)
        {
            if (nodeDto?.Context?.TicketId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;

            var unloadResult = _unloadPointManager.ConfirmUnloadGuide(nodeDto.Context.TicketId.Value, card.EmployeeId);
            if (!unloadResult) return;

            if (!_connectManager.SendSms(Dom.Sms.Template.DestinationPointApprovalSms, nodeDto.Context.TicketId))
            {
                Logger.Error("Sms hasn`t been sent");
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointGuide2.State.Idle;
            nodeDto.Context.OpDataId = null;
            nodeDto.Context.OpProcessData = null;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
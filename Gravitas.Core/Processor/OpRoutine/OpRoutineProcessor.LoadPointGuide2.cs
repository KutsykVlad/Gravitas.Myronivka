using System.Linq;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.LoadPoint;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.Dto;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class LoadPointGuide2OpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly IConnectManager _connectManager;
        private readonly IUserManager _userManager;
        private readonly ILoadPointManager _loadPointManager;

        public LoadPointGuide2OpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            IConnectManager connectManager, 
            IUserManager userManager, 
            ILoadPointManager loadPointManager) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository
                )
        {
            _connectManager = connectManager;
            _userManager = userManager;
            _loadPointManager = loadPointManager;
        }

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            return config != null;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Dom.OpRoutine.LoadPointGuide2.State.Idle:
                    break;
                case Dom.OpRoutine.LoadPointGuide2.State.BindLoadPoint:
                    break;
                case Dom.OpRoutine.LoadPointGuide2.State.AddOpVisa:
                    AddOperationVisa(_nodeDto);
                    break;
            }
        }

        private void AddOperationVisa(Node nodeDto)
        {
            if (nodeDto?.Context?.TicketContainerId == null || nodeDto.Context.TicketId == null) return;

            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDto.Context.TicketId.Value);
            if (ticket == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;
               
            var loadResultConfirm = _loadPointManager.ConfirmLoadGuide(nodeDto.Context.TicketId.Value, card.EmployeeId);
            if (!loadResultConfirm) return;

            if (!_connectManager.SendSms(Dom.Sms.Template.DestinationPointApprovalSms, nodeDto.Context.TicketId))
            {
                Logger.Error("Sms hasn`t been sent");
            }

            nodeDto.Context.OpDataId = null;
            nodeDto.Context.OpProcessData = null;
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointGuide2.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
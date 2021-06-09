using System.Linq;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.LoadPoint;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainValue;

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


        public override void Process()
        {
            ReadDbData();

            switch (NodeDetails.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.LoadPointGuide2.State.Idle:
                    break;
                case Model.DomainValue.OpRoutine.LoadPointGuide2.State.BindLoadPoint:
                    break;
                case Model.DomainValue.OpRoutine.LoadPointGuide2.State.AddOpVisa:
                    AddOperationVisa(NodeDetails);
                    break;
            }
        }

        private void AddOperationVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.TicketContainerId == null || nodeDetailsDto.Context.TicketId == null) return;

            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.TicketId.Value);
            if (ticket == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;
               
            var loadResultConfirm = _loadPointManager.ConfirmLoadGuide(nodeDetailsDto.Context.TicketId.Value, card.EmployeeId.Value);
            if (!loadResultConfirm) return;

            if (!_connectManager.SendSms(SmsTemplate.DestinationPointApprovalSms, nodeDetailsDto.Context.TicketId, cardId: card.Id))
            {
                Logger.Error("Sms hasn`t been sent");
            }

            nodeDetailsDto.Context.OpDataId = null;
            nodeDetailsDto.Context.OpProcessData = null;
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.LoadPointGuide2.State.Idle;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
    }
}
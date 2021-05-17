using System;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.Dto;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class MixedFeedManageOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly IUserManager _userManager;

        public MixedFeedManageOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            IUserManager userManager) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _userManager = userManager;
        }

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null) return false;
            var rfidValid = config.Rfid.ContainsKey(Dom.Node.Config.Rfid.TableReader);
            return rfidValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Dom.OpRoutine.MixedFeedManage.State.Workstation:
                    break;
                case Dom.OpRoutine.MixedFeedManage.State.Edit:
                    break;
                case Dom.OpRoutine.MixedFeedManage.State.AddOperationVisa:
                    AddOperationVisa(_nodeDto);
                    break;
            }
        }

        private void AddOperationVisa(Node nodeDto)
        {
            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Зміна показників силоса",
                MixedFeedSiloId = nodeDto.Context.OpProcessData,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Dom.OpRoutine.MixedFeedManage.State.AddOperationVisa
            };
            _nodeRepository.Add<OpVisa, long>(visa);
            
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedManage.State.Workstation;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            nodeDto.Context.OpProcessData = null;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
        }
    }
}
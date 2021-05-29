using System;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpVisa.DAO;

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
            var rfidValid = config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader);
            return rfidValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(NodeDetailsDto)) return;

            switch (NodeDetailsDto.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.MixedFeedManage.State.Workstation:
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedManage.State.Edit:
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedManage.State.AddOperationVisa:
                    AddOperationVisa(NodeDetailsDto);
                    break;
            }
        }

        private void AddOperationVisa(NodeDetails nodeDetailsDto)
        {
            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Зміна показників силоса",
                MixedFeedSiloId = nodeDetailsDto.Context.OpProcessData,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedManage.State.AddOperationVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);
            
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedManage.State.Workstation;
            nodeDetailsDto.Context.TicketContainerId = null;
            nodeDetailsDto.Context.TicketId = null;
            nodeDetailsDto.Context.OpDataId = null;
            nodeDetailsDto.Context.OpProcessData = null;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
        }
    }
}
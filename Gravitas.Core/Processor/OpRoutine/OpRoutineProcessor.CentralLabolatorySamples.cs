using System;
using System.Linq;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Card;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainValue;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;

namespace Gravitas.Core.Processor.OpRoutine
{
    class CentralLaboratorySamplesOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly ICardRepository _cardRepository;
        private readonly IRoutesManager _routesManager;
        private readonly IUserManager _userManager;
        private readonly ICardManager _cardManager;

        public CentralLaboratorySamplesOpRoutineProcessor(IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository, 
            INodeRepository nodeRepository, 
            IOpDataRepository opDataRepository,
            ICardRepository cardRepository,
            IRoutesManager routesManager, 
            IUserManager userManager, 
            ICardManager cardManager) : 
            base(opRoutineManager, deviceManager, deviceRepository, nodeRepository, opDataRepository)
        {
            _cardRepository = cardRepository;
            _routesManager = routesManager;
            _userManager = userManager;
            _cardManager = cardManager;
        }

        public override void Process()
        {
            ReadDbData();

            switch (NodeDetails.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.Idle:
                    GetTicketCard(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleBindTray:
                    SampleBindTray(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleAddOpVisa:
                    SampleAddOpVisa(NodeDetails);
                    break;
            }
        }

        #region 01_Idle
        
        private void GetTicketCard(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Config == null) return;

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDetailsDto);
            if (card == null) return;

            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDetailsDto.Id, out var errorMessage))
            {
                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error, errorMessage));
                return;
            }

            var opData = _opDataRepository.GetLastOpData<CentralLabOpData>(card.Ticket.Id, null);
            if (opData != null && opData.StateId != OpDataState.Canceled && opData.StateId != OpDataState.Rejected)
            {
                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error, "Картка оброблена, або знаходиться в обробці."));
                return;
            }
            
            _cardManager.SetRfidValidationDO(true, nodeDetailsDto);

            opData = new CentralLabOpData
            {
                NodeId = nodeDetailsDto.Id,
                TicketId = card.Ticket.Id,
                TicketContainerId = card.Ticket.TicketContainerId,
                StateId = OpDataState.Init,
                SampleCheckInDateTime = DateTime.Now
            };
            _cardRepository.Add<CentralLabOpData, Guid>(opData);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleBindTray;
            nodeDetailsDto.Context.TicketContainerId = card.Ticket.TicketContainerId;
            nodeDetailsDto.Context.TicketId = card.Ticket.Id;
            nodeDetailsDto.Context.OpDataId = opData.Id;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        #endregion

        #region 02_CentralLabSampleBindTray

        private void SampleBindTray(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.TicketContainerId == null) return;

            var card = _cardManager.GetLaboratoryTrayOnTableReader(nodeDetailsDto);
            if (card == null) return;

            if (card.TicketContainerId.HasValue)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Warning, "Лоток закріплений за іншим автомобілем."));
                return;
            }
            
            card.TicketContainerId = nodeDetailsDto.Context.TicketContainerId;
            _cardRepository.Update<Card, string>(card);

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleAddOpVisa;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        #endregion

        #region 03_CentralLabSampleAddOpVisa

        private void SampleAddOpVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var centralLabOpData = _context.CentralLabOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (centralLabOpData == null)
            {
                return;
            }

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Відбір проби.",
                CentralLaboratoryOpData = centralLabOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleAddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);
            
            centralLabOpData.StateId = OpDataState.Processing;
            _nodeRepository.Update<CentralLabOpData, Guid>(centralLabOpData);

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.Idle;
            nodeDetailsDto.Context.TicketId = null;
            nodeDetailsDto.Context.OpDataId = null;
            nodeDetailsDto.Context.TicketContainerId = null;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        #endregion
    }
}

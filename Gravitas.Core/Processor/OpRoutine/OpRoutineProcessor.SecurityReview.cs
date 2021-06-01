using System;
using System.Linq;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using TicketStatus = Gravitas.Model.DomainValue.TicketStatus;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class SecurityReviewOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly ICardManager _cardManager;
        private readonly IQueueManager _queue;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IRoutesManager _routesManager;
        private readonly IUserManager _userManager;
        private readonly ITicketRepository _ticketRepository;

        public SecurityReviewOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ITicketRepository ticketRepository,
            IRoutesManager routesManager,
            IQueueManager queue,
            IRoutesInfrastructure routesInfrastructure,
            ICardManager cardManager, 
            IUserManager userManager) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _ticketRepository = ticketRepository;
            _routesManager = routesManager;
            _queue = queue;
            _routesInfrastructure = routesInfrastructure;
            _cardManager = cardManager;
            _userManager = userManager;
        }

        public override void Process()
        {
            ReadDbData();

            switch (NodeDetails.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.SecurityReview.State.Idle:
                    Idle(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.SecurityReview.State.AddOperationVisa:
                    AddOperationVisa(NodeDetails);
                    break;
            }
        }

        private void Idle(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error, @"Хибний контекст вузла"));
                return;
            }

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDetailsDto);
            if (card == null) return;

            var supplyCode = _context.SingleWindowOpDatas.Where(x => x.TicketId == card.Ticket.Id)
                                                              .Select(c => c.SupplyCode)
                                                              .FirstOrDefault();

            if (supplyCode != TechRoute.SupplyCode)
            {

                if (!_queue.IsAllowedEnterTerritory(card.Ticket.Id))
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                        new NodeProcessingMsgItem(ProcessingMsgType.Error, @"Не маєте права заходити без виклику по черзі."));

                    _cardManager.SetRfidValidationDO(false, nodeDetailsDto);
                    return;
                }
            }
            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDetailsDto.Id, out var errorMessage))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error, errorMessage));
                
                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);
                return;
            }

            var securityReviewOpData = new SecurityCheckReviewOpData
            {
                StateId = OpDataState.Init,
                NodeId = _nodeId,
                TicketId = card.Ticket.Id,
                CheckInDateTime = DateTime.Now,
                TicketContainerId = card.Ticket.TicketContainerId
            };
            _ticketRepository.Add<SecurityCheckReviewOpData, Guid>(securityReviewOpData);
            
            card.Ticket.StatusId = TicketStatus.Processing;
            _ticketRepository.Update<Ticket, int>(card.Ticket);
            _routesInfrastructure.MoveForward(card.Ticket.Id, nodeDetailsDto.Id);
            
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityReview.State.AddOperationVisa;
            nodeDetailsDto.Context.TicketContainerId = card.Ticket.TicketContainerId;
            nodeDetailsDto.Context.TicketId = card.Ticket.Id;
            nodeDetailsDto.Context.OpDataId = securityReviewOpData.Id;
            
            _cardManager.SetRfidValidationDO(true, nodeDetailsDto);

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void AddOperationVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context?.OpDataId == null)
            {
                return;
            }
            var card = _userManager.GetValidatedUsersCardByOnGateReader(nodeDetailsDto);
            if (card == null) return;

            var securityReviewOpData = _context.SecurityCheckReviewOpDatas
                .FirstOrDefault(d=> d.Id == nodeDetailsDto.Context.OpDataId.Value); 
            if (securityReviewOpData == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Дозвіл на заїзд",
                SecurityCheckReviewOpDataId = securityReviewOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityReview.State.AddOperationVisa
            };

            _nodeRepository.Add<OpVisa, int>(visa);
            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            securityReviewOpData.StateId = OpDataState.Processed;
            securityReviewOpData.CheckOutDateTime = DateTime.Now;
            _context.SaveChanges();
            
            _cardManager.SetRfidValidationDO(true, nodeDetailsDto);
            
            nodeDetailsDto.Context.OpProcessData = null;
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityReview.State.Idle;
            nodeDetailsDto.Context.TicketContainerId = null;
            nodeDetailsDto.Context.TicketId = null;
            nodeDetailsDto.Context.OpDataId = null;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
    }
}
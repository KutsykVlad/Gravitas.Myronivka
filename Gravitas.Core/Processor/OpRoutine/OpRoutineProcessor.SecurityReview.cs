using System;
using System.Linq;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.Dto;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

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

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null) return false;

            var rfidValid = config.Rfid.ContainsKey(Dom.Node.Config.Rfid.OnGateReader);
            
            return rfidValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Dom.OpRoutine.SecurityReview.State.Idle:
                    Idle(_nodeDto);
                    break;
                case Dom.OpRoutine.SecurityReview.State.AddOperationVisa:
                    AddOperationVisa(_nodeDto);
                    break;
            }
        }

        private void Idle(Node nodeDto)
        {
            if (nodeDto.Context == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, @"Хибний контекст вузла"));
                return;
            }

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDto);
            if (card == null) return;

            var supplyCode = _context.SingleWindowOpDatas.Where(x => x.TicketId == card.Ticket.Id)
                                                              .Select(c => c.SupplyCode)
                                                              .FirstOrDefault();

            if (supplyCode != Dom.SingleWindowOpData.TechnologicalSupplyCode)
            {

                if (!_queue.IsAllowedEnterTerritory(card.Ticket.Id))
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                        new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, @"Не маєте права заходити без виклику по черзі."));

                    _cardManager.SetRfidValidationDO(false, nodeDto);
                    return;
                }
            }
            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDto.Id, out var errorMessage))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, errorMessage));
                
                _cardManager.SetRfidValidationDO(false, nodeDto);
                return;
            }

            var securityReviewOpData = new SecurityCheckReviewOpData
            {
                StateId = Dom.OpDataState.Init,
                NodeId = _nodeId,
                TicketId = card.Ticket.Id,
                CheckInDateTime = DateTime.Now,
                TicketContainerId = card.Ticket.ContainerId
            };
            _ticketRepository.Add<SecurityCheckReviewOpData, Guid>(securityReviewOpData);
            
            card.Ticket.StatusId = Dom.Ticket.Status.Processing;
            _ticketRepository.Update<Ticket, long>(card.Ticket);
            _routesInfrastructure.MoveForward(card.Ticket.Id, nodeDto.Id);
            
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.SecurityReview.State.AddOperationVisa;
            nodeDto.Context.TicketContainerId = card.Ticket.ContainerId;
            nodeDto.Context.TicketId = card.Ticket.Id;
            nodeDto.Context.OpDataId = securityReviewOpData.Id;
            
            _cardManager.SetRfidValidationDO(true, nodeDto);

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void AddOperationVisa(Node nodeDto)
        {
            if (nodeDto.Context?.OpDataId == null)
            {
                return;
            }
            var card = _userManager.GetValidatedUsersCardByOnGateReader(nodeDto);
            if (card == null) return;

            var securityReviewOpData = _context.SecurityCheckReviewOpDatas
                .FirstOrDefault(d=> d.Id == nodeDto.Context.OpDataId.Value); 
            if (securityReviewOpData == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Дозвіл на заїзд",
                SecurityCheckReviewOpDataId = securityReviewOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Dom.OpRoutine.SecurityReview.State.AddOperationVisa
            };

            _nodeRepository.Add<OpVisa, long>(visa);
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            securityReviewOpData.StateId = Dom.OpDataState.Processed;
            securityReviewOpData.CheckOutDateTime = DateTime.Now;
            _context.SaveChanges();
            
            _cardManager.SetRfidValidationDO(true, nodeDto);
            
            nodeDto.Context.OpProcessData = null;
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.SecurityReview.State.Idle;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
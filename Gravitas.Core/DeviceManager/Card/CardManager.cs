using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.Manager.RfidObidRwAutoAnswer;
using Gravitas.Core.Manager.RfidZebraFx9500;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.EmployeeRoles;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Model;
using Gravitas.Model.DomainModel.EmployeeRoles.DTO;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainValue;

namespace Gravitas.Core.DeviceManager.Card
{
    public class CardManager : ICardManager
    {
        private readonly IEmployeeRolesRepository _employeeRolesRepository;
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly IDeviceManager _deviceManager;
        private readonly GravitasDbContext _context;
        private readonly IRfidObidRwManager _rfidObidRwManager;
        private readonly IRfidZebraFx9500Manager _rfidZebraFx9500Manager;

        public CardManager(IOpRoutineManager opRoutineManager,
            ITicketRepository ticketRepository,
            IEmployeeRolesRepository employeeRolesRepository, 
            IDeviceManager deviceManager, 
            GravitasDbContext context,
            IRfidObidRwManager rfidObidRwManager, 
            IRfidZebraFx9500Manager rfidZebraFx9500Manager)
        {
            _opRoutineManager = opRoutineManager;
            _ticketRepository = ticketRepository;
            _employeeRolesRepository = employeeRolesRepository;
            _deviceManager = deviceManager;
            _context = context;
            _rfidObidRwManager = rfidObidRwManager;
            _rfidZebraFx9500Manager = rfidZebraFx9500Manager;
        }

        

        public CardReadResult GetTruckCardByOnGateReader(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.IsEmergency) return null;
            
            var rfidConfig = nodeDetailsDto.Config.Rfid[NodeData.Config.Rfid.OnGateReader];

            var rfid = _rfidObidRwManager.GetCard(rfidConfig.DeviceId);
            if (string.IsNullOrEmpty(rfid))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(ProcessingMsgType.Info, "Очікування картки"));
                return null;
            }

            var card = _context.Cards.AsNoTracking().FirstOrDefault(e => e.Id.Equals(rfid, StringComparison.CurrentCultureIgnoreCase));
            if (card?.TicketContainerId == null)
            {
                SetRfidValidationDO(false, nodeDetailsDto);
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                    ProcessingMsgType.Warning, "Контейнер не містить інформації."));
                return null;
            }

            var ticket = nodeDetailsDto.Id == (long)NodeIdValue.SecurityIn1 || nodeDetailsDto.Id == (long)NodeIdValue.SecurityIn2
                                                                      || nodeDetailsDto.Id == (long)NodeIdValue.SecurityReview1
                ? _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, TicketStatus.ToBeProcessed)
                ?? _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, TicketStatus.Processing)
                : _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, TicketStatus.Processing)
                ?? _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, TicketStatus.ToBeProcessed);


            if (ticket == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                    ProcessingMsgType.Warning, $"У даного контейнера ({card.TicketContainerId}) немає потрібного тікета."));
                SetRfidValidationDO(false, nodeDetailsDto);
                return null;
            }

            return new CardReadResult
            {
                Ticket = ticket,
                Id = card.Id
            };
        }

        public CardReadResult GetTruckCardByZebraReader(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.IsEmergency) return null;

            CardReadResult result = null;
            if (nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.LongRangeReader2))
            {
                result = GetCardReadResultFromZebra(nodeDetailsDto.Id, nodeDetailsDto.Config.Rfid[NodeData.Config.Rfid.LongRangeReader2]);
                if (result?.Ticket != null)
                {
                    return result;
                }
            }

            if (nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.LongRangeReader))
            {
                result = GetCardReadResultFromZebra(nodeDetailsDto.Id, nodeDetailsDto.Config.Rfid[NodeData.Config.Rfid.LongRangeReader]);
            }

            return result;
        }
        
        public (CardReadResult card, bool input) GetTruckCardByZebraReaderDirection(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.IsEmergency) return (null, false);

            CardReadResult result = null;
            if (nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.LongRangeReader2))
            {
                result = GetCardReadResultFromZebra(nodeDetailsDto.Id, nodeDetailsDto.Config.Rfid[NodeData.Config.Rfid.LongRangeReader2]);
                if (result?.Ticket != null)
                {
                    return (result, false);
                }
            }

            if (nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.LongRangeReader))
            {
                result = GetCardReadResultFromZebra(nodeDetailsDto.Id, nodeDetailsDto.Config.Rfid[NodeData.Config.Rfid.LongRangeReader]);
            }

            return (result, true);
        }

        private CardReadResult GetCardReadResultFromZebra(int nodeId, NodeConfig.RfidConfig config)
        {
            var tags = _rfidZebraFx9500Manager.GetCard(config.DeviceId);
            if (tags == null || tags.Count == 0)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId, new NodeProcessingMsgItem(ProcessingMsgType.Info, "Очікування картки"));
                return null;
            }

            var cardList = new List<Model.DomainModel.Card.DAO.Card>();
            Model.DomainModel.Card.DAO.Card card;
            foreach (var tag in tags)
            {
                card = _context.Cards.AsNoTracking().FirstOrDefault(e =>
                    e.IsActive
                    && e.TypeId == CardType.TransportCard
                    && e.TicketContainerId != null
                    && e.Id.Equals(tag, StringComparison.CurrentCultureIgnoreCase));

                if (card != null)
                    cardList.Add(card);
            }

            if (!cardList.Any())
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(ProcessingMsgType.Warning,
                        $@"Міток, що відповідають критеріям відбору в полі дії зчитувача не виявлено. Всього виявлено {tags.Count} міток"));
                return null;
            }

            if (cardList.Count > 1)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(ProcessingMsgType.Warning,
                        $@"Виявлено більше 1 мітки. Всього виявлено {tags.Count} міток"));
                return null;
            }

            card = cardList.SingleOrDefault();
            
            if (card?.TicketContainerId == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"Картка не прив'язана до автомобіля."));
                return null;
            }

            var ticket = _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, TicketStatus.Processing);
            return new CardReadResult
            {
                Ticket = ticket
            };
        }

        public Model.DomainModel.Card.DAO.Card GetLaboratoryTrayOnTableReader(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.IsEmergency) return null;
            
            nodeDetailsDto.Config.Rfid.TryGetValue(NodeData.Config.Rfid.TableReader, out var rfidConfig);
            if (rfidConfig == null) return null;

            var rfid = _rfidObidRwManager.GetCard(rfidConfig.DeviceId);

            if (string.IsNullOrWhiteSpace(rfid))
                return null;

            var card = _context.Cards.AsNoTracking().FirstOrDefault(e =>
                e.Id.Equals(rfid, StringComparison.CurrentCultureIgnoreCase));
            if (!_opRoutineManager.IsRfidCardValid(out var errMsgItem, card, CardType.LaboratoryTray))
            {
                if (!string.IsNullOrEmpty(errMsgItem.Text)) _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, errMsgItem);
                return null;
            }

            return card;
        }

        public async void SetRfidValidationDO(bool isValid, NodeDetails nodeDetailsDto)
        {
            if (!nodeDetailsDto.Config.DO.ContainsKey(NodeData.Config.DO.RfidCheckFirst) 
                || !nodeDetailsDto.Config.DO.ContainsKey(NodeData.Config.DO.RfidCheckSecond)) return;

            _deviceManager.SetOutput(
                isValid
                    ? nodeDetailsDto.Config.DO[NodeData.Config.DO.RfidCheckFirst]
                    : nodeDetailsDto.Config.DO[NodeData.Config.DO.RfidCheckSecond], true);

            await Task.Delay(3000);
        
            _deviceManager.SetOutput(nodeDetailsDto.Config.DO[NodeData.Config.DO.RfidCheckSecond], false);
            await Task.Delay(500);
            _deviceManager.SetOutput(nodeDetailsDto.Config.DO[NodeData.Config.DO.RfidCheckFirst], false);
        }
        
        public bool IsMasterEmployeeCard(Model.DomainModel.Card.DAO.Card card, int nodeId)
        {
            var employeeRoles = _employeeRolesRepository.GetEmployeeRoles(card.EmployeeId.Value);
            return IsAdminCard(card, employeeRoles) || employeeRoles.Items.Any(x => x.Nodes.Contains(nodeId));
        }

        public bool IsLaboratoryEmployeeCard(Model.DomainModel.Card.DAO.Card card, int nodeId)
        {
            var employeeRoles = _employeeRolesRepository.GetEmployeeRoles(card.EmployeeId.Value);
            return IsAdminCard(card, employeeRoles) || employeeRoles.Items.Any(x => x.Nodes.Contains(nodeId));
        }

        public bool IsAdminCard(Model.DomainModel.Card.DAO.Card card, RolesDto employeeRoles = null)
        {
            if (employeeRoles == null) employeeRoles = _employeeRolesRepository.GetEmployeeRoles(card.EmployeeId.Value);
            return employeeRoles.Items.Any(x => x.RoleId == (long) UserRole.Admin);
        }
    }
}
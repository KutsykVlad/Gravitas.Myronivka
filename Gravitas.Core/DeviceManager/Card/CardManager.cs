using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.ApiClient.Devices;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Model;
using Gravitas.Model.DomainValue;
using Gravitas.Model.Dto;
using Node = Gravitas.Model.Dto.Node;

namespace Gravitas.Core.DeviceManager.Card
{
    public class CardManager : ICardManager
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IEmployeeRolesRepository _employeeRolesRepository;
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly IDeviceManager _deviceManager;
        private readonly GravitasDbContext _context;

        public CardManager(IDeviceRepository deviceRepository,
            IOpRoutineManager opRoutineManager,
            ITicketRepository ticketRepository,
            IEmployeeRolesRepository employeeRolesRepository, 
            IDeviceManager deviceManager, 
            GravitasDbContext context)
        {
            _deviceRepository = deviceRepository;
            _opRoutineManager = opRoutineManager;
            _ticketRepository = ticketRepository;
            _employeeRolesRepository = employeeRolesRepository;
            _deviceManager = deviceManager;
            _context = context;
        }

        

        public CardReadResult GetTruckCardByOnGateReader(Node nodeDto)
        {
            if (nodeDto.IsEmergency) return null;
            
            var rfidConfig = nodeDto.Config.Rfid[Dom.Node.Config.Rfid.OnGateReader];

            var rfidObidRwState = (RfidObidRwState) Program.GetDeviceState(rfidConfig.DeviceId);
            if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, rfidObidRwState, TimeSpan.FromSeconds(rfidConfig.Timeout)))
            {
                if (!string.IsNullOrEmpty(errMsgItem?.Text))
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, errMsgItem);
                }
                return null;
            }

            var card = _context.Cards.AsNoTracking().FirstOrDefault(e => e.Id.Equals(rfidObidRwState.InData.Rifd, StringComparison.CurrentCultureIgnoreCase));
            if (card != null && card.IsOwn)
            {
                return new CardReadResult
                {
                    Id = card.Id,
                    IsOwn = true
                };
            }

            if (card?.TicketContainerId == null)
            {
                SetRfidValidationDO(false, nodeDto);
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Warning, "Контейнер не містить інформації."));
                return null;
            }

            var ticket = nodeDto.Id == (long)NodeIdValue.SecurityIn1 || nodeDto.Id == (long)NodeIdValue.SecurityIn2
                                                                      || nodeDto.Id == (long)NodeIdValue.SecurityReview1
                ? _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, Dom.Ticket.Status.ToBeProcessed)
                ?? _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, Dom.Ticket.Status.Processing)
                : _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, Dom.Ticket.Status.Processing)
                ?? _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, Dom.Ticket.Status.ToBeProcessed);


            if (ticket == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Warning, $"У даного контейнера ({card.TicketContainerId}) немає потрібного тікета."));
                SetRfidValidationDO(false, nodeDto);
                return null;
            }

            return new CardReadResult
            {
                Ticket = ticket,
                Id = card.Id
            };
        }

        public CardReadResult GetTruckCardByZebraReader(Node nodeDto)
        {
            if (nodeDto.IsEmergency) return null;

            CardReadResult result = null;
            if (nodeDto.Config.Rfid.ContainsKey(Dom.Node.Config.Rfid.LongRangeReader2))
            {
                result = GetCardReadResultFromZebra(nodeDto.Id, nodeDto.Config.Rfid[Dom.Node.Config.Rfid.LongRangeReader2]);
                if (result?.Ticket != null)
                {
                    return result;
                }
            }

            if (nodeDto.Config.Rfid.ContainsKey(Dom.Node.Config.Rfid.LongRangeReader))
            {
                result = GetCardReadResultFromZebra(nodeDto.Id, nodeDto.Config.Rfid[Dom.Node.Config.Rfid.LongRangeReader]);
            }

            return result;
        }
        
        public (CardReadResult card, bool input) GetTruckCardByZebraReaderDirection(Node nodeDto)
        {
            if (nodeDto.IsEmergency) return (null, false);

            CardReadResult result = null;
            if (nodeDto.Config.Rfid.ContainsKey(Dom.Node.Config.Rfid.LongRangeReader2))
            {
                result = GetCardReadResultFromZebra(nodeDto.Id, nodeDto.Config.Rfid[Dom.Node.Config.Rfid.LongRangeReader2]);
                if (result?.Ticket != null)
                {
                    return (result, false);
                }
            }

            if (nodeDto.Config.Rfid.ContainsKey(Dom.Node.Config.Rfid.LongRangeReader))
            {
                result = GetCardReadResultFromZebra(nodeDto.Id, nodeDto.Config.Rfid[Dom.Node.Config.Rfid.LongRangeReader]);
            }

            return (result, true);
        }

        private CardReadResult GetCardReadResultFromZebra(long nodeId, NodeConfig.RfidConfig config)
        {
            var zebraRfidState = (RfidZebraFx9500AntennaState) Program.GetDeviceState(config.DeviceId);
            if (!_deviceRepository.IsDeviceStateValid(out var errMsg, zebraRfidState, TimeSpan.FromSeconds(config.Timeout)))
            {
                if (!string.IsNullOrEmpty(errMsg?.Text)) _opRoutineManager.UpdateProcessingMessage(nodeId, errMsg);
                return null;
            }

            var cardList = new List<Model.Card>();
            Model.Card card;
            foreach (var keyValuePair in zebraRfidState.InData.TagList)
            {
                card = _context.Cards.AsNoTracking().FirstOrDefault(e =>
                    e.IsActive
                    && e.TypeId == Dom.Card.Type.TransportCard
                    && e.TicketContainerId != null
                    && e.Id.Equals(keyValuePair.Key, StringComparison.CurrentCultureIgnoreCase));

                if (card != null)
                    cardList.Add(card);
            }

            if (!cardList.Any())
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning,
                        $@"Міток, що відповідають критеріям відбору в полі дії зчитувача не виявлено. Всього виявлено {zebraRfidState.InData.TagList.Count} міток"));
                return null;
            }

            if (cardList.Count > 1)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning,
                        $@"Виявлено більше 1 мітки. Всього виявлено {zebraRfidState.InData.TagList.Count} міток"));
                return null;
            }

            card = cardList.SingleOrDefault();
            if (card != null && card.IsOwn)
            {
                return new CardReadResult
                {
                    IsOwn = true
                };
            }
            
            if (card?.TicketContainerId == null || card.IsOwn)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, @"Картка не прив'язана до автомобіля."));
                return null;
            }

            var ticket = _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, Dom.Ticket.Status.Processing);
            return new CardReadResult
            {
                Ticket = ticket
            };
        }

        public Model.Card GetLaboratoryTrayOnTableReader(Node nodeDto)
        {
            if (nodeDto.IsEmergency) return null;
            
            nodeDto.Config.Rfid.TryGetValue(Dom.Node.Config.Rfid.TableReader, out var rfidConfig);
            if (rfidConfig == null) return null;

            var rfidObidRwState = (RfidObidRwState) Program.GetDeviceState(rfidConfig.DeviceId);

            if (rfidObidRwState?.InData == null
                || string.IsNullOrWhiteSpace(rfidObidRwState.InData.Rifd)
                || rfidObidRwState.LastUpdate == null
                || DateTime.Now - rfidObidRwState.LastUpdate.Value > TimeSpan.FromSeconds(rfidConfig.Timeout))
                return null;

            var card = _context.Cards.AsNoTracking().FirstOrDefault(e =>
                e.Id.Equals(rfidObidRwState.InData.Rifd, StringComparison.CurrentCultureIgnoreCase));
            if (!_opRoutineManager.IsRfidCardValid(out var errMsgItem, card, Dom.Card.Type.LaboratoryTray))
            {
                if (!string.IsNullOrEmpty(errMsgItem.Text)) _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, errMsgItem);
                return null;
            }

            return card;
        }

        public async void SetRfidValidationDO(bool isValid, Node nodeDto)
        {
            if (!nodeDto.Config.DO.ContainsKey(Dom.Node.Config.DO.RfidCheckFirst) 
                || !nodeDto.Config.DO.ContainsKey(Dom.Node.Config.DO.RfidCheckSecond)) return;

            _deviceManager.SetOutput(
                isValid
                    ? nodeDto.Config.DO[Dom.Node.Config.DO.RfidCheckFirst]
                    : nodeDto.Config.DO[Dom.Node.Config.DO.RfidCheckSecond], true);

            await Task.Delay(3000);
        
            _deviceManager.SetOutput(nodeDto.Config.DO[Dom.Node.Config.DO.RfidCheckSecond], false);
            await Task.Delay(500);
            _deviceManager.SetOutput(nodeDto.Config.DO[Dom.Node.Config.DO.RfidCheckFirst], false);
        }
        
        public bool IsMasterEmployeeCard(Model.Card card, long nodeId)
        {
            var employeeRoles = _employeeRolesRepository.GetEmployeeRoles(card.EmployeeId);
            return IsAdminCard(card, employeeRoles) || employeeRoles.Items.Any(x => x.Nodes.Contains(nodeId));
        }

        public bool IsLaboratoryEmployeeCard(Model.Card card, long nodeId)
        {
            var employeeRoles = _employeeRolesRepository.GetEmployeeRoles(card.EmployeeId);
            return IsAdminCard(card, employeeRoles) || employeeRoles.Items.Any(x => x.Nodes.Contains(nodeId));
        }

        public bool IsAdminCard(Model.Card card, RolesDto employeeRoles = null)
        {
            if (employeeRoles == null) employeeRoles = _employeeRolesRepository.GetEmployeeRoles(card.EmployeeId);
            return employeeRoles.Items.Any(x => x.RoleId == (long) UserRole.Admin);
        }
    }
}
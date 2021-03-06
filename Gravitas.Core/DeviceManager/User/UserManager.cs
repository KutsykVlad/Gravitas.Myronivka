using System;
using System.Linq;
using Gravitas.Core.DeviceManager.Card;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Device;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;

namespace Gravitas.Core.DeviceManager.User
{
    public class UserManager : IUserManager
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly ICardManager _cardManager;
        private readonly GravitasDbContext _context;

        public UserManager(IDeviceRepository deviceRepository,
            IOpRoutineManager opRoutineManager,
            ICardManager cardManager, 
            GravitasDbContext context)
        {
            _deviceRepository = deviceRepository;
            _opRoutineManager = opRoutineManager;
            _cardManager = cardManager;
            _context = context;
        }

        public Model.DomainModel.Card.DAO.Card GetValidatedUsersCardByTableReader(NodeDetails nodeDetailsDto)
        {
            return GetValidatedUsersCard(nodeDetailsDto, NodeData.Config.Rfid.TableReader);
        }

        public Model.DomainModel.Card.DAO.Card GetValidatedUsersCardByOnGateReader(NodeDetails nodeDetailsDto)
        {
            return GetValidatedUsersCard(nodeDetailsDto, NodeData.Config.Rfid.OnGateReader);
        }

        private Model.DomainModel.Card.DAO.Card GetValidatedUsersCard(NodeDetails nodeDetailsDto, string readerType)
        {
            var rfidConfigs = nodeDetailsDto.Config.Rfid.Where(x => x.Key.Contains(readerType)).ToList();
            if (rfidConfigs.Count == 0) return null;
            foreach (var rfidConfig in rfidConfigs)
            {
                var rfidObidRwState = (RfidObidRwState) Program.GetDeviceState(rfidConfig.Value.DeviceId);
                if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, rfidObidRwState, TimeSpan.FromSeconds(rfidConfig.Value.Timeout)))
                {
                    if (!string.IsNullOrEmpty(errMsgItem?.Text))
                    {
                        _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, errMsgItem);
                        _cardManager.SetRfidValidationDO(false, nodeDetailsDto);
                    }
                    continue;
                }

                var card = _context.Cards.AsNoTracking().FirstOrDefault(e =>
                    e.Id.Equals(rfidObidRwState.InData.Rifd, StringComparison.CurrentCultureIgnoreCase));

                var isCardValid = IsCardValid(out errMsgItem, card, nodeDetailsDto.Id);
                if (!isCardValid)
                {
                    if (!string.IsNullOrEmpty(errMsgItem?.Text))
                    {
                        _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, errMsgItem);
                        _cardManager.SetRfidValidationDO(false, nodeDetailsDto);
                    }
                    continue;
                }

                return card;
            }

            return null;
        }

        private bool IsCardValid(out NodeProcessingMsgItem msgItem, Model.DomainModel.Card.DAO.Card card, int nodeId)
        {
            if (!_opRoutineManager.IsEmployeeBindedRfidCardValid(out var errMsgItem, card))
            {
                msgItem = errMsgItem;
                return false;
            }

            if (!_opRoutineManager.IsEmployeeSignValid(out errMsgItem, card, nodeId))
            {
                msgItem = errMsgItem;
                return false;
            }

            msgItem = null;
            return true;
        }
    }
}
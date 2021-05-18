using System;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.Model.DomainModel.Queue.DAO;
using Gravitas.Model.DomainValue;
using NLog;

namespace Gravitas.DAL.Repository.Queue
{
    public class QueueRegisterRepository : BaseRepository, IQueueRegisterRepository
    {
        private readonly GravitasDbContext _context;
        private readonly ExternalDataRepository _externalDataRepository;
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public QueueRegisterRepository(GravitasDbContext context, ExternalDataRepository externalDataRepository) : base(context)
        {
            _context = context;
            _externalDataRepository = externalDataRepository;
        }

        public void CalledFromQueue(int ticketContainerId)
        {
            Logger.Trace($"CalledFromQueue. TicketContainerId:{ticketContainerId}");
            var item = GetSingleOrDefault<QueueRegister, int>(r => r.TicketContainerId == ticketContainerId);
            if (item == null)
            {
                return;
            }

            try
            {
                if (!item.IsAllowedToEnterTerritory)
                {
                    item.IsSMSSend = false;
                    item.IsAllowedToEnterTerritory = true;
                    item.SMSTimeAllowed = DateTime.Now;
                    Update<QueueRegister, int>(item);
                }
            }
            catch (Exception exception)
            {
                Logger.Error($"CalledFromQueue. Exception:{exception.Message}");
            }
        }

        public bool IsAllowedToEnter(int ticketContainerId)
        {
            var item = GetSingleOrDefault<QueueRegister, int>(r => r.TicketContainerId == ticketContainerId);
            return item != null && item.IsAllowedToEnterTerritory;
        }

        public bool SMSAlreadySent(int ticketContainerId)
        {
            var item = GetSingleOrDefault<QueueRegister, int>(r => r.TicketContainerId == ticketContainerId);
            return item != null && item.IsSMSSend;
        }

        public void OnSMSSending(int ticketContainerId)
        {
            Logger.Trace($"Sending SMS flag. TicketContainerId:{ticketContainerId}");
            var item = GetSingleOrDefault<QueueRegister, int>(r => r.TicketContainerId == ticketContainerId);
            if (item == null) return;
            if (!item.IsSMSSend)
            {
                item.IsSMSSend = true;
                Update<QueueRegister, int>(item);
            }
        }

        public void Register(QueueRegister newRegistration)
        {
            Logger.Trace(
                $"QueueRegister. Container {newRegistration.TicketContainerId}. Plate  {newRegistration.TruckPlate}, Registered {newRegistration.RegisterTime}");
         
            var item = GetSingleOrDefault<QueueRegister, int>(r => r.TicketContainerId == newRegistration.TicketContainerId, true);
            if (item == null)
            {
                //Not registered yet
                if (!IsValid(newRegistration)) FillTruckInfoFromSingleWindow(newRegistration);
                Logger.Trace($"QueueRegister. New registration. {newRegistration.TicketContainerId}");
                AddOrUpdate<QueueRegister, int>(newRegistration);
            }
            else
            {
                Logger.Trace($"QueueRegister. Existing registration. Updating fields: {newRegistration.TicketContainerId}");
                //update fields
                FillTruckInfoFromSingleWindow(item);
                item.IsAllowedToEnterTerritory = newRegistration.IsAllowedToEnterTerritory;
                AddOrUpdate<QueueRegister, int>(item);
            }
         
        }

        public void RemoveFromQueue(int ticketContainerId)
        {
            lock (this)
            {
                var item = GetSingleOrDefault<QueueRegister, int>(r => r.TicketContainerId == ticketContainerId);
                if (item == null) return;

                Update<QueueRegister, int>(item);
                Delete<QueueRegister, int>(item);
            }
        }

        private bool IsValid(QueueRegister newRegistration)
        {
            return !(string.IsNullOrEmpty(newRegistration.PhoneNumber) || string.IsNullOrEmpty(newRegistration.TruckPlate));
        }

        private void FillTruckInfoFromSingleWindow(QueueRegister newRegistration)
        {
            var tickets = _context.Tickets.Where(t =>
                    t.TicketContainerId == newRegistration.TicketContainerId &&
                    (t.StatusId == TicketStatus.Blank ||
                     t.StatusId == TicketStatus.New ||
                     t.StatusId == TicketStatus.Processing ||
                     t.StatusId == TicketStatus.ToBeProcessed))
                .Select(t => t.Id).ToList();

            Logger.Debug($"FillTruckInfoFromSingleWindow: {string.Join(", ", tickets)}");
            
            if (tickets.Count > 0)
            {
                var t = tickets.Last();
                var singleWindow = _context.SingleWindowOpDatas.AsNoTracking().FirstOrDefault(s => s.TicketId == t);
                if (singleWindow == null) return;
                newRegistration.PhoneNumber = singleWindow.ContactPhoneNo;

                if (singleWindow.IsThirdPartyCarrier)
                {
                    newRegistration.TruckPlate = singleWindow.HiredTransportNumber;
                    newRegistration.TrailerPlate = singleWindow.HiredTrailerNumber;
                }
                else
                {
                    newRegistration.TruckPlate =
                        _externalDataRepository.GetFixedAssetDetail(singleWindow?.TransportId)?.RegistrationNo ?? string.Empty;
                    newRegistration.TrailerPlate =
                        _externalDataRepository.GetFixedAssetDetail(singleWindow?.TrailerId)?.RegistrationNo ?? string.Empty;
                }
            }
        }
    }
}
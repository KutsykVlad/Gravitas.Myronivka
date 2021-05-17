using System;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Queue.DAO;
using NLog;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.DAL
{
    public class QueueRegisterRepository : BaseRepository<GravitasDbContext>, IQueueRegisterRepository
    {
        private readonly GravitasDbContext _context;
        private readonly ExternalDataRepository _externalDataRepository;
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public QueueRegisterRepository(GravitasDbContext context, ExternalDataRepository externalDataRepository) : base(context)
        {
            _context = context;
            _externalDataRepository = externalDataRepository;
        }

        public void CalledFromQueue(long ticketContainerId)
        {
            Logger.Trace($"CalledFromQueue. TicketContainerId:{ticketContainerId}");
            var item = GetSingleOrDefault<QueueRegister, long>(r => r.TicketContainerId == ticketContainerId);
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
                    Update<QueueRegister, long>(item);
                }
            }
            catch (Exception exception)
            {
                Logger.Error($"CalledFromQueue. Exception:{exception.Message}");
            }
        }

        public bool IsAllowedToEnter(long ticketContainerId)
        {
            var item = GetSingleOrDefault<QueueRegister, long>(r => r.TicketContainerId == ticketContainerId);
            return item != null && item.IsAllowedToEnterTerritory;
        }

        public bool SMSAlreadySent(long ticketContainerId)
        {
            var item = GetSingleOrDefault<QueueRegister, long>(r => r.TicketContainerId == ticketContainerId);
            return item != null && item.IsSMSSend;
        }

        public void OnSMSSending(long ticketContainerId)
        {
            Logger.Trace($"Sending SMS flag. TicketContainerId:{ticketContainerId}");
            var item = GetSingleOrDefault<QueueRegister, long>(r => r.TicketContainerId == ticketContainerId);
            if (item == null) return;
            if (!item.IsSMSSend)
            {
                item.IsSMSSend = true;
                Update<QueueRegister, long>(item);
            }
        }

        public void Register(QueueRegister newRegistration)
        {
            Logger.Trace(
                $"QueueRegister. Container {newRegistration.TicketContainerId}. Plate  {newRegistration.TruckPlate}, Registered {newRegistration.RegisterTime}");
         
            var item = GetSingleOrDefault<QueueRegister, long>(r => r.TicketContainerId == newRegistration.TicketContainerId, true);
            if (item == null)
            {
                //Not registered yet
                if (!IsValid(newRegistration)) FillTruckInfoFromSingleWindow(newRegistration);
                Logger.Trace($"QueueRegister. New registration. {newRegistration.TicketContainerId}");
                AddOrUpdate<QueueRegister, long>(newRegistration);
            }
            else
            {
                Logger.Trace($"QueueRegister. Existing registration. Updating fields: {newRegistration.TicketContainerId}");
                //update fields
                FillTruckInfoFromSingleWindow(item);
                item.IsAllowedToEnterTerritory = newRegistration.IsAllowedToEnterTerritory;
                AddOrUpdate<QueueRegister, long>(item);
            }
         
        }

        public void RemoveFromQueue(long ticketContainerId)
        {
            lock (this)
            {
                var item = GetSingleOrDefault<QueueRegister, long>(r => r.TicketContainerId == ticketContainerId);
                if (item == null) return;

                Update<QueueRegister, long>(item);
                Delete<QueueRegister, long>(item);
            }
        }

        private bool IsValid(QueueRegister newRegistration)
        {
            return !(string.IsNullOrEmpty(newRegistration.PhoneNumber) || string.IsNullOrEmpty(newRegistration.TruckPlate));
        }

        private void FillTruckInfoFromSingleWindow(QueueRegister newRegistration)
        {
            var tickets = _context.Tickets.Where(t =>
                    t.ContainerId == newRegistration.TicketContainerId &&
                    (t.StatusId == Dom.Ticket.Status.Blank ||
                     t.StatusId == Dom.Ticket.Status.New ||
                     t.StatusId == Dom.Ticket.Status.Processing ||
                     t.StatusId == Dom.Ticket.Status.ToBeProcessed))
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
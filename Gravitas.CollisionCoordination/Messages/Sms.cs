using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Model.DomainValue;
using NLog;

namespace Gravitas.CollisionCoordination.Messages
{
    public class Sms : IMessage
    {
        private readonly IConnectManager _connectManager;
        private readonly GravitasDbContext _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly int _ticketId;

        public Sms(IConnectManager connectManager, int ticketId, GravitasDbContext context)
        {
            _connectManager = connectManager;
            _ticketId = ticketId;
            _context = context;
        }

        public bool Send(List<string> contacts, int? templateId)
        {
            if (contacts == null) return false;
            
            var ticket = _context.Tickets.First(x => x.Id == _ticketId);
            var card = _context.Cards.First(x => x.TicketContainerId == ticket.TicketContainerId);

            foreach (var contact in contacts)
            {
                if (contact == null) continue;
                try
                {
                    _connectManager.SendSms(SmsTemplate.RequestForQualityApprovalSms, _ticketId, contact, cardId: card.Id);
                    _logger.Info($"Collision coordination: sms send. Phone={contact}");
                }
                catch (Exception e)
                {
                    _logger.Error($"Collision coordination: sms send error. Phone={contact}, Exception={e}");
                }
               
            }

            return true;
        }
    }
}
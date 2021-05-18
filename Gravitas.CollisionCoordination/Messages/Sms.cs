using System;
using System.Collections.Generic;
using System.Web.Http.Filters;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Model;
using NLog;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.CollisionCoordination.Messages
{
    public class Sms : IMessage
    {
        private readonly IConnectManager _connectManager;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly long _ticketId;

        public Sms(IConnectManager connectManager, long ticketId)
        {
            _connectManager = connectManager;
            _ticketId = ticketId;
        }

        public bool Send(List<string> contacts, int? templateId)
        {
            if (contacts == null) return false;
            
            foreach (var contact in contacts)
            {
                if (contact == null) continue;
                try
                {
                    _connectManager.SendSms(Dom.Sms.Template.RequestForQualityApprovalSms, _ticketId, contact);
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
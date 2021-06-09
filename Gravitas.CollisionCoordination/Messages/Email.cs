using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Gravitas.CollisionCoordination.Manager.LaboratoryData;
using Gravitas.CollisionCoordination.Models;
using Gravitas.DAL.DbContext;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;
using NLog;

namespace Gravitas.CollisionCoordination.Messages
{
    public class Email : IMessage
    {
        private readonly IConnectManager _connectManager;
        private readonly ILaboratoryDataManager _laboratoryDataManager;
        private readonly GravitasDbContext _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly int _ticketId;
        private readonly List<string> _contactData;

        public Email(IConnectManager connectManager,
            ILaboratoryDataManager laboratoryDataManager, int ticketId, List<string> contactData,
            GravitasDbContext context)
        {
            _connectManager = connectManager;
            _laboratoryDataManager = laboratoryDataManager;
            _ticketId = ticketId;
            _contactData = contactData;
            _context = context;
        }

        public bool Send(List<string> contacts, int? templateId = null)
        {
            if (contacts == null) return false;

            var laboratoryDataVm = templateId == (int) EmailTemplate.CentralLaboratoryCollisionApproval
                ? _laboratoryDataManager.CollectCentralLaboratoryData(_ticketId, contacts.First() ?? "")
                : _laboratoryDataManager.CollectData(_ticketId, contacts.First() ?? "");

            laboratoryDataVm = AddContactData(laboratoryDataVm, _contactData);
            
            foreach (var contact in contacts)
            {
                if (contact == null) continue;
                if (contact == contacts.First())
                {
                    laboratoryDataVm.DisplayButtons = "block";
                }
                else
                {
                    laboratoryDataVm.DisplayButtons = "none";
                    laboratoryDataVm.ApproveLink = "#";
                    laboratoryDataVm.DisapproveLink = "#";
                }
                try
                {
                    var ticket = _context.Tickets.First(x => x.Id == _ticketId);
                    var card = _context.Cards.FirstOrDefault(x => x.TicketContainerId == ticket.TicketContainerId);
                    _connectManager.SendEmail(EmailTemplate.CollisionApproval, contact, laboratoryDataVm, laboratoryDataVm.LabolatoryFilePath, card.Id);
                    _logger.Info($"Collision coordination: email send. Address={contact}");
                }
                catch (Exception e)
                {
                    _logger.Error($"Collision coordination: email send error. Address={contact}, Exception={e}");
                }
            }

            return true;
        }

        private LaboratoryDataVm AddContactData(LaboratoryDataVm laboratoryDataVm, List<string> contactData)
        {
            if (contactData.Count < 6) return laboratoryDataVm;
            laboratoryDataVm.Email1 = contactData[0];
            laboratoryDataVm.Phone1 = contactData[1];
            laboratoryDataVm.Email2 = contactData[2];
            laboratoryDataVm.Phone2 = contactData[3];
            laboratoryDataVm.Email3 = contactData[4];
            laboratoryDataVm.Phone3 = contactData[5];
            return laboratoryDataVm;
        }
    }
}
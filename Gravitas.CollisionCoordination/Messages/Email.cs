using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.CollisionCoordination.Manager.LaboratoryData;
using Gravitas.CollisionCoordination.Models;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Model;
using NLog;
using Dom = Gravitas.Model.DomainValue.Dom;
using LabFacelessOpData = Gravitas.Model.DomainModel.OpData.TDO.Detail.LabFacelessOpData;

namespace Gravitas.CollisionCoordination.Messages
{
    public class Email : IMessage
    {
        private readonly IConnectManager _connectManager;
        private readonly ILaboratoryDataManager _laboratoryDataManager;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly long _ticketId;
        private readonly List<string> _contactData;

        public Email(IConnectManager connectManager,
            ILaboratoryDataManager laboratoryDataManager, long ticketId, List<string> contactData)
        {
            _connectManager = connectManager;
            _laboratoryDataManager = laboratoryDataManager;
            _ticketId = ticketId;
            _contactData = contactData;
        }

        public bool Send(List<string> contacts, int? templateId = null)
        {
            if (contacts == null) return false;

            var laboratoryDataVm = templateId == Dom.Email.Template.CentralLaboratoryCollisionApproval
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
                    _connectManager.SendEmail(templateId ?? Dom.Email.Template.CollisionApproval, contact, laboratoryDataVm, laboratoryDataVm.LabolatoryFilePath);
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
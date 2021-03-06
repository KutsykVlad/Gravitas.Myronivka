using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Platform.Manager.Camera;
using Gravitas.Infrastructure.Platform.Manager.OpVisa;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Node.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpCameraImage;
using Gravitas.Model.DomainModel.OpData.DAO;
using CardType = Gravitas.Model.DomainValue.CardType;

namespace Gravitas.Infrastructure.Platform.Manager.OpRoutine
{
    public class OpRoutineManager : IOpRoutineManager
    {
        private readonly ICameraManager _cameraManager;
        private readonly INodeRepository _nodeRepository;
        private readonly IVisaValidationManager _visaValidationManager;
        private readonly GravitasDbContext _context;

        public OpRoutineManager(
            INodeRepository nodeRepository,
            IVisaValidationManager visaValidationManager,
            ICameraManager cameraManager, 
            GravitasDbContext context)
        {
            _nodeRepository = nodeRepository;
            _visaValidationManager = visaValidationManager;
            _cameraManager = cameraManager;
            _context = context;
        }

        public bool IsEmployeeBindedRfidCardValid(out NodeProcessingMsgItem errMsgItem, Card card,
            bool isEmployeeBinded = true)
        {
            if (!IsRfidCardValid(out errMsgItem, card, CardType.EmployeeCard)) return false;

            if (isEmployeeBinded && card.EmployeeId == null)
            {
                errMsgItem = new NodeProcessingMsgItem(ProcessingMsgType.Warning,
                    @"Картка не містить інформації про користувача");
                return false;
            }

            if (!isEmployeeBinded && card.EmployeeId != null)
            {
                errMsgItem = new NodeProcessingMsgItem(ProcessingMsgType.Warning,
                    $@"Картку вже зарезрововано за користувачем. Id:{card.EmployeeId}");
                return false;
            }

            return true;
        }

        public bool IsRfidCardValid(out NodeProcessingMsgItem errMsgItem, Card card, CardType cardTypeId)
        {
            if (!IsRfidCardValid(out errMsgItem, card)) return false;

            if (cardTypeId != card.TypeId)
            {
                errMsgItem = new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"Хибний тип картки");
                return false;
            }

            return true;
        }

        public bool IsRfidCardValid(out NodeProcessingMsgItem errMsgItem, Card card)
        {
            if (card == null)
            {
                errMsgItem =
                    new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"Картку не ідентифіковано");
                return false;
            }

            if (!card.IsActive)
            {
                errMsgItem = new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"Картка не активна");
                return false;
            }

            errMsgItem = null;
            return true;
        }

        public bool IsEmployeeSignValid(out NodeProcessingMsgItem errMsgItem, Card card, int nodeId)
        {
            if (!_visaValidationManager.ValidateEmployeeAccess(nodeId, card.EmployeeId.Value))
            {
                errMsgItem =
                    new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"У робітника нема прав підписувати даний вузол");
                return false;
            }

            errMsgItem = null;
            return true;
        }

        public void LogNonStandardOp(Model.DomainModel.Node.TDO.Detail.NodeDetails nodeDetailsDto, NonStandartOpData opData)
        {
            var cameraImagesList = _cameraManager.GetSnapshots(nodeDetailsDto.Config);

            _nodeRepository.Add<NonStandartOpData, Guid>(opData);

            foreach (var camImageId in cameraImagesList)
            {
                var camImage = _context.OpCameraImages.First(x => x.Id == camImageId);
                camImage.NonStandartOpDataId = opData.Id;

                _nodeRepository.Update<OpCameraImage, int>(camImage);
            }
        }

        public void UpdateProcessingMessage(int nodeId, NodeProcessingMsgItem msgItem)
        {
            _context.NodeProcessingMessages.Add(new NodeProcessingMessage
            {
                NodeId = nodeId,
                Message = msgItem.Text,
                TypeId = msgItem.TypeId,
                DateTime = DateTime.Now
            });
            _context.SaveChanges();
        }

        public void UpdateProcessingMessage(IEnumerable<int> nodeIds, NodeProcessingMsgItem msgItem)
        {
            foreach (var nodeId in nodeIds) UpdateProcessingMessage(nodeId, msgItem);
        }
    }
}
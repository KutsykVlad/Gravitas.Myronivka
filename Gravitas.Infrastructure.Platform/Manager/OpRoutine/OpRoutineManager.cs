using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpCameraImage;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.Dto;
using Dom = Gravitas.Model.DomainValue.Dom;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.Infrastructure.Platform.Manager.OpRoutine
{
    public class OpRoutineManager : IOpRoutineManager
    {
        private readonly ICameraManager _cameraManager;
        private readonly INodeRepository _nodeRepository;
        private readonly IVisaValidationManager _visaValidationManager;

        public OpRoutineManager(
            INodeRepository nodeRepository,
            IVisaValidationManager visaValidationManager,
            ICameraManager cameraManager)
        {
            _nodeRepository = nodeRepository;
            _visaValidationManager = visaValidationManager;
            _cameraManager = cameraManager;
        }

        public bool IsEmployeeBindedRfidCardValid(out NodeProcessingMsgItem errMsgItem, Card card,
            bool isEmployeeBinded = true)
        {
            if (!IsRfidCardValid(out errMsgItem, card, Dom.Card.Type.EmployeeCard)) return false;

            if (isEmployeeBinded && card.EmployeeId == null)
            {
                errMsgItem = new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning,
                    @"Картка не містить інформації про користувача");
                return false;
            }

            if (!isEmployeeBinded && card.EmployeeId != null)
            {
                errMsgItem = new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning,
                    $@"Картку вже зарезрововано за користувачем. Id:{card.EmployeeId}");
                return false;
            }

            return true;
        }

        public bool IsRfidCardValid(out NodeProcessingMsgItem errMsgItem, Card card, int cardTypeId)
        {
            if (!IsRfidCardValid(out errMsgItem, card)) return false;

            if (cardTypeId != card.TypeId)
            {
                errMsgItem = new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, @"Хибний тип картки");
                return false;
            }

            return true;
        }

        public bool IsRfidCardValid(out NodeProcessingMsgItem errMsgItem, Card card)
        {
            if (card == null)
            {
                errMsgItem =
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, @"Картку не ідентифіковано");
                return false;
            }

            if (!card.IsActive)
            {
                errMsgItem = new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, @"Картка не активна");
                return false;
            }

            errMsgItem = null;
            return true;
        }

        public bool IsEmployeeSignValid(out NodeProcessingMsgItem errMsgItem, Card card, long nodeId)
        {
            if (!_visaValidationManager.ValidateEmployeeAccess(nodeId, card.EmployeeId))
            {
                errMsgItem =
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, @"У робітника нема прав підписувати даний вузол");
                return false;
            }

            errMsgItem = null;
            return true;
        }

        public void LogNonStandardOp(Node nodeDto, NonStandartOpData opData)
        {
            var cameraImagesList = _cameraManager.GetSnapshots(nodeDto.Config);

            _nodeRepository.Add<NonStandartOpData, Guid>(opData);

            foreach (var camImageId in cameraImagesList)
            {
                var camImage = _nodeRepository.GetEntity<OpCameraImage, long>(camImageId);
                camImage.NonStandartOpDataId = opData.Id;

                _nodeRepository.Update<OpCameraImage, long>(camImage);
            }
        }

        public void UpdateProcessingMessage(long nodeId, NodeProcessingMsgItem msgItem)
        {
            var isMessageChanged = _nodeRepository.GetNodeDto(nodeId).ProcessingMessage.Items?.FirstOrDefault()?.Text != msgItem.Text;
            if (isMessageChanged) _nodeRepository.UpdateNodeProcessingMessage(nodeId, msgItem);
            if (isMessageChanged) SignalRInvoke.UpdateProcessingMessage(nodeId);
        }

        public void UpdateProcessingMessage(IEnumerable<long> nodeIds, NodeProcessingMsgItem msgItem)
        {
            foreach (var nodeId in nodeIds) UpdateProcessingMessage(nodeId, msgItem);
        }
    }
}
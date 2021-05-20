﻿using System;
using System.Linq;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Card;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainValue;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.Core.Processor.OpRoutine
{
    class CentralLaboratorySamplesOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly ICardRepository _cardRepository;
        private readonly IRoutesManager _routesManager;
        private readonly IUserManager _userManager;
        private readonly ICardManager _cardManager;

        public CentralLaboratorySamplesOpRoutineProcessor(IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository, 
            INodeRepository nodeRepository, 
            IOpDataRepository opDataRepository,
            ICardRepository cardRepository,
            IRoutesManager routesManager, 
            IUserManager userManager, 
            ICardManager cardManager) : 
            base(opRoutineManager, deviceManager, deviceRepository, nodeRepository, opDataRepository)
        {
            _cardRepository = cardRepository;
            _routesManager = routesManager;
            _userManager = userManager;
            _cardManager = cardManager;
        }

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null) return false;

            bool rfidValid = config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader) 
                             && config.Rfid.ContainsKey(NodeData.Config.Rfid.OnGateReader);

            return rfidValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.Idle:
                    GetTicketCard(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleBindTray:
                    SampleBindTray(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleAddOpVisa:
                    SampleAddOpVisa(_nodeDto);
                    break;
            }
        }

        #region 01_Idle
        
        private void GetTicketCard(Node nodeDto)
        {
            if (nodeDto.Config == null) return;

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDto);
            if (card == null) return;

            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDto.Id, out var errorMessage))
            {
                _cardManager.SetRfidValidationDO(false, nodeDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, errorMessage));
                return;
            }

            var opData = _opDataRepository.GetLastOpData<CentralLabOpData>(card.Ticket.Id, null);
            if (opData != null && opData.StateId != OpDataState.Canceled && opData.StateId != OpDataState.Rejected)
            {
                _cardManager.SetRfidValidationDO(false, nodeDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, "Картка оброблена, або знаходиться в обробці."));
                return;
            }
            
            _cardManager.SetRfidValidationDO(true, nodeDto);

            opData = new CentralLabOpData
            {
                NodeId = nodeDto.Id,
                TicketId = card.Ticket.Id,
                TicketContainerId = card.Ticket.TicketContainerId,
                StateId = OpDataState.Init,
                SampleCheckInDateTime = DateTime.Now
            };
            _cardRepository.Add<CentralLabOpData, Guid>(opData);

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleBindTray;
            nodeDto.Context.TicketContainerId = card.Ticket.TicketContainerId;
            nodeDto.Context.TicketId = card.Ticket.Id;
            nodeDto.Context.OpDataId = opData.Id;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion

        #region 02_CentralLabSampleBindTray

        private void SampleBindTray(Node nodeDto)
        {
            if (nodeDto?.Context?.TicketContainerId == null) return;

            var card = _cardManager.GetLaboratoryTrayOnTableReader(nodeDto);
            if (card == null) return;

            if (card.TicketContainerId.HasValue)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Warning, "Лоток закріплений за іншим автомобілем."));
                return;
            }
            
            card.TicketContainerId = nodeDto.Context.TicketContainerId;
            _cardRepository.Update<Card, string>(card);

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleAddOpVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion

        #region 03_CentralLabSampleAddOpVisa

        private void SampleAddOpVisa(Node nodeDto)
        {
            if (nodeDto?.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;

            var centralLabOpData = _context.CentralLabOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (centralLabOpData == null)
            {
                return;
            }

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Відбір проби.",
                CentralLaboratoryOpData = centralLabOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleAddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);
            
            centralLabOpData.StateId = OpDataState.Processing;
            _nodeRepository.Update<CentralLabOpData, Guid>(centralLabOpData);

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.Idle;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            nodeDto.Context.TicketContainerId = null;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion
    }
}

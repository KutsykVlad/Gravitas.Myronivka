﻿using System;
using System.Linq;
using System.Threading;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.Manager.Camera;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainModel.OwnTransport.DAO;
using Gravitas.Model.DomainValue;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class SecurityOutOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly ICameraManager _cameraManager;
        private readonly ICardManager _cardManager;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IRoutesManager _routesManager;
        private readonly IUserManager _userManager;
        private readonly ITicketRepository _ticketRepository;

        public SecurityOutOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ICameraManager cameraManager,
            ITicketRepository ticketRepository,
            IRoutesManager routesManager,
            IRoutesInfrastructure routesInfrastructure,
            ICardManager cardManager, 
            IUserManager userManager) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _cameraManager = cameraManager;
            _ticketRepository = ticketRepository;
            _routesManager = routesManager;
            _routesInfrastructure = routesInfrastructure;
            _cardManager = cardManager;
            _userManager = userManager;
        }

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null) return false;

            var rfidValid = config.Rfid.ContainsKey(NodeData.Config.Rfid.OnGateReader)
                            && config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader);
            var cameraValid = config.Camera.ContainsKey(NodeData.Config.Camera.Camera1);

            return rfidValid && cameraValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(NodeDetailsDto)) return;

            switch (NodeDetailsDto.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.SecurityOut.State.Idle:
                    WatchBarrier(NodeDetailsDto);
                    Idle(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.CheckOwnTransport:
                    WatchBarrier(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.ShowOperationsList:
                    WatchBarrier(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.EditStampList:
                    WatchBarrier(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.AddRouteControlVisa:
                    WatchBarrier(NodeDetailsDto);
                    AddRouteControlVisa(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.AddTransportInspectionVisa:
                    WatchBarrier(NodeDetailsDto);
                    AddTransportInspectionVisa(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.OpenBarrier:
                    OpenBarrier(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.GetCamSnapshot:
                    WatchBarrier(NodeDetailsDto);
                    GetCamSnapshot(NodeDetailsDto);
                    break;
            }
        }

        private void Idle(NodeDetails nodeDetailsDto)
        {
            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDetailsDto);
            if (card == null) return;

            if (card.IsOwn)
            {
                nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.AddTransportInspectionVisa;
                nodeDetailsDto.Context.OpProcessData =
                    _ticketRepository.GetFirstOrDefault<OwnTransport, int>(x => x.CardId == card.Id)?.Id;
                UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
                return;
            }

            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDetailsDto.Id, out var errorMessage))
            {
                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, errorMessage));
                return;
            }

            var securityCheckOutOpData = new SecurityCheckOutOpData
            {
                StateId = OpDataState.Init,
                NodeId = _nodeId,
                TicketId = card.Ticket.Id,
                CheckInDateTime = DateTime.Now,
                TicketContainerId = card.Ticket.TicketContainerId
            };
            _ticketRepository.Add<SecurityCheckOutOpData, Guid>(securityCheckOutOpData);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.ShowOperationsList;
            nodeDetailsDto.Context.TicketContainerId = card.Ticket.TicketContainerId;
            nodeDetailsDto.Context.TicketId = card.Ticket.Id;
            nodeDetailsDto.Context.OpDataId = securityCheckOutOpData.Id;

            if (!UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, @"Не валідна спроба зміни стану вузла."));
                return;
            }

            _cardManager.SetRfidValidationDO(true, nodeDetailsDto);

            _routesInfrastructure.MoveForward(card.Ticket.Id, nodeDetailsDto.Id);
        }

        private void WatchBarrier(NodeDetails nodeDetailsDto)
        {
            if (!nodeDetailsDto.Config.DO.ContainsKey(NodeData.Config.DI.Barrier)) return;

            var iBarrierConfig = nodeDetailsDto.Config.DI[NodeData.Config.DI.Barrier];
            var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

            if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, iBarrierState, TimeSpan.FromSeconds(3)))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, errMsgItem);
                return;
            }

            if (iBarrierState.InData.Value != true) return;

            var isNewEntryNeeded = !_context.NonStandartOpDatas
                .AsNoTracking()
                .Where(e => e.NodeId == nodeDetailsDto.Id && e.CheckInDateTime != null)
                .AsEnumerable()
                .Any(e =>
                    (DateTime.Now - e.CheckInDateTime.Value).TotalSeconds < GlobalConfigurationManager.NonStandartPassTimeout);

            if (!isNewEntryNeeded) return;

            _opRoutineManager.LogNonStandardOp(nodeDetailsDto, new NonStandartOpData
            {
                NodeId = nodeDetailsDto.Id,
                CheckInDateTime = DateTime.Now,
                StateId = OpDataState.Processed,
                Message = "Несанкціонований проїзд"
            });
        }

        public void AddRouteControlVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var securityCheckOutOpData = _context.SecurityCheckOutOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (securityCheckOutOpData == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Перевірка проходження маршруту",
                SecurityCheckOutOpDataId = securityCheckOutOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.AddRouteControlVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.AddTransportInspectionVisa;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        public void AddTransportInspectionVisa(NodeDetails nodeDetailsDto)
        {
            //            if (nodeDto?.Context?.OpDataId == null || nodeDto.Context.TicketId == null) return;

            var card = _userManager.GetValidatedUsersCardByOnGateReader(nodeDetailsDto);
            if (card == null) return;

            if (!nodeDetailsDto.Context.OpProcessData.HasValue)
            {
                var securityCheckOutOpData = _context.SecurityCheckOutOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
                if (securityCheckOutOpData == null) return;
                securityCheckOutOpData.StateId = OpDataState.Processed;
                securityCheckOutOpData.CheckOutDateTime = DateTime.Now;
                _context.SaveChanges();

                var visa = new OpVisa
                {
                    DateTime = DateTime.Now,
                    Message = "Зовнишній огляд проведено",
                    SecurityCheckOutOpDataId = securityCheckOutOpData.Id,
                    EmployeeId = card.EmployeeId,
                    OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.AddTransportInspectionVisa
                };
                _nodeRepository.Add<OpVisa, int>(visa);

                var next = _routesInfrastructure.GetNextNodes(nodeDetailsDto.Context.TicketId.Value);

                if (next?.Count==1 && next.First()==nodeDetailsDto.Id)
                {
                    var ticket = _context.Tickets.First(x => x.Id == nodeDetailsDto.Context.TicketId.Value);
                    ticket.StatusId = TicketStatus.Completed;
                    _context.SaveChanges();
                }
            }

            _cardManager.SetRfidValidationDO(true, nodeDetailsDto);

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.OpenBarrier;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void OpenBarrier(NodeDetails nodeDetailsDto)
        {
            if (!nodeDetailsDto.Config.DO.ContainsKey(NodeData.Config.DO.Barrier))
            {
                if (nodeDetailsDto.Id == (long)NodeIdValue.SecurityOut2)
                {
                    var cameraImagesListBottom = _cameraManager.GetSnapshots(nodeDetailsDto.Config);
                    foreach (var camImageIdBottom in cameraImagesListBottom)
                    {
                        var camImage = _context.OpCameraImages.First(x => x.Id == camImageIdBottom);
                        camImage.SecurityCheckOutOpDataId = nodeDetailsDto.Context.OpDataId;
                        _context.SaveChanges();
                    }
                }

                nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.GetCamSnapshot;
                UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
                return;
            }

            //            if (nodeDto.Context?.OpDataId == null)
            //                return;

            var iBarrierConfig = nodeDetailsDto.Config.DI[NodeData.Config.DI.Barrier];
            var oBarrierConfig = nodeDetailsDto.Config.DO[NodeData.Config.DO.Barrier];

            var oBarrierState = (DigitalOutState) Program.GetDeviceState(oBarrierConfig.DeviceId);

            if (oBarrierState.OutData == null) oBarrierState.OutData = new DigitalOutJsonState();

            var cameraImagesList = _cameraManager.GetSnapshots(nodeDetailsDto.Config);
            foreach (var camImageId in cameraImagesList)
            {
                var camImage = _context.OpCameraImages.First(x => x.Id == camImageId);
                camImage.SecurityCheckOutOpDataId = nodeDetailsDto.Context.OpDataId;
                _context.SaveChanges();
            }

            while (true)
            {
                Program.SetDeviceOutData(oBarrierState.Id, true);

                var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

                if (iBarrierState?.InData?.Value == true)
                {
                    break;
                }
                Thread.Sleep(1000);
            }

            oBarrierState = (DigitalOutState) Program.GetDeviceState(oBarrierConfig.DeviceId);

            if (oBarrierState.OutData == null) oBarrierState.OutData = new DigitalOutJsonState();

            while (true)
            {
                Program.SetDeviceOutData(oBarrierState.Id, false);

                var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

                if (iBarrierState?.InData?.Value == false) break;
                Thread.Sleep(1000);
            }

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.GetCamSnapshot;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void GetCamSnapshot(NodeDetails nodeDetailsDto)
        {
            Thread.Sleep(2000);

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.Idle;
            nodeDetailsDto.Context.TicketContainerId = null;
            nodeDetailsDto.Context.OpProcessData = null;
            nodeDetailsDto.Context.TicketId = null;
            nodeDetailsDto.Context.OpDataId = null;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
    }
}
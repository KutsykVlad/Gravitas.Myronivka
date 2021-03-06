using System;
using System.Linq;
using System.Threading;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class UnloadCheckPointOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly ICardManager _cardManager;
        private readonly IRoutesManager _routesManager;
        private readonly IRoutesInfrastructure _routesInfrastructure;

        public UnloadCheckPointOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            IRoutesManager routesManager,
            ICardManager cardManager, 
            IRoutesInfrastructure routesInfrastructure) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _routesManager = routesManager;
            _cardManager = cardManager;
            _routesInfrastructure = routesInfrastructure;
        }

        public override void Process()
        {
            ReadDbData();

            switch (NodeDetails.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.UnloadCheckPoint.State.Idle:
                    Idle(NodeDetails);
                    break;
            }
        }

        private void Idle(NodeDetails nodeDetailsDto)
        {
            var result = _cardManager.GetTruckCardByZebraReaderDirection(nodeDetailsDto);
            if (result.card == null) return;

            if (!_routesManager.IsNodeNext(result.card.Ticket.Id, nodeDetailsDto.Id, out var errorMessage))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(ProcessingMsgType.Error, errorMessage));
                return;
            }
            
            var opData = _opDataRepository.GetFirstOrDefault<UnloadPointOpData, Guid>(x =>
                x.TicketId == result.card.Ticket.Id && x.StateId != OpDataState.Processed);
            if (result.input)
            {
                if (opData != null)
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(ProcessingMsgType.Warning, "Авто знаходиться в обробці"));
                    return;
                }
                opData = new UnloadPointOpData
                {
                    StateId = OpDataState.Init, 
                    NodeId = _nodeId, 
                    TicketId = result.card.Ticket.Id, 
                    CheckInDateTime = DateTime.Now
                };
            }
            else
            {
                opData.CheckOutDateTime = DateTime.Now;
                opData.StateId = OpDataState.Processed;
                _routesInfrastructure.MoveForward(result.card.Ticket.Id, _nodeId);
            }
           
            _opDataRepository.AddOrUpdate<UnloadPointOpData, Guid>(opData);
            OpenBarrier(nodeDetailsDto);
        }
        
        private void OpenBarrier(NodeDetails nodeDetailsDto)
        {
            // Validate node context
            if (!nodeDetailsDto.Config.DI.ContainsKey(NodeData.Config.DI.Barrier)
                || !nodeDetailsDto.Config.DO.ContainsKey(NodeData.Config.DO.Barrier))
                return;

            var iBarrierConfig = nodeDetailsDto.Config.DI[NodeData.Config.DI.Barrier];
            var oBarrierConfig = nodeDetailsDto.Config.DO[NodeData.Config.DO.Barrier];

            Logger.Info($"UnloadPoint try to open barrier NodeId = {nodeDetailsDto.Id}, Device = {iBarrierConfig.DeviceId}");
            var startTime = DateTime.Now;
            
            Program.SetDeviceOutData(oBarrierConfig.DeviceId, true);
            
            Thread.Sleep(3000);
            while (true)
            {
                var iBarrierState = (DigitalInState) _deviceRepository.GetDeviceState(iBarrierConfig.DeviceId);

                if (iBarrierState?.InData?.Value == true)
                {
                    Thread.Sleep(2000);
                    Logger.Info($"UnloadPoint is opened barrier NodeId = {nodeDetailsDto.Id}, Device = {iBarrierConfig.DeviceId}");
                    break;
                }

                if (DateTime.Now > startTime.AddSeconds(20))
                {
                    Program.SetDeviceOutData(oBarrierConfig.DeviceId, false);

                    Logger.Info($"UnloadPoint barier timeout = {nodeDetailsDto.Id}, Device = {iBarrierConfig.DeviceId}");
                    _opRoutineManager.UpdateProcessingMessage(
                        _context.Nodes.Where(x => x.OrganizationUnitId == nodeDetailsDto.OrganisationUnitId)
                            .Select(x => x.Id)
                            .ToList(), 
                        new NodeProcessingMsgItem(ProcessingMsgType.Error, $"Помилка відкриття шлагбауму на {nodeDetailsDto.Name}"));

                    return;
                }
            }

            Program.SetDeviceOutData(oBarrierConfig.DeviceId, false);
            
            _nodeRepository.ClearNodeProcessingMessage(_nodeId);
        }
    }
}
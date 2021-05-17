using System;
using System.Linq;
using System.Threading;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.Dto;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using Node = Gravitas.Model.Dto.Node;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class LoadCheckPointOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly ICardManager _cardManager;
        private readonly IRoutesManager _routesManager;
        private readonly IRoutesInfrastructure _routesInfrastructure;

        public LoadCheckPointOpRoutineProcessor(
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

         public override bool ValidateNodeConfig(NodeConfig config)
        {
            return true;
            if (config == null) return false;
            var lrValid = config.Rfid.ContainsKey(Dom.Node.Config.Rfid.LongRangeReader);
            var lrValid2 = config.Rfid.ContainsKey(Dom.Node.Config.Rfid.LongRangeReader2);
            var doBarrier = config.Rfid.ContainsKey(Dom.Node.Config.DO.Barrier);
            var diBarrier = config.Rfid.ContainsKey(Dom.Node.Config.DI.Barrier);

            return lrValid && lrValid2 && doBarrier && diBarrier;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Dom.OpRoutine.LoadCheckPoint.State.Idle:
                    Idle(_nodeDto);
                    break;
            }
        }

        private void Idle(Node nodeDto)
        {
            var card = _cardManager.GetTruckCardByZebraReader(nodeDto);
            if (card == null) return;

            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDto.Id, out var errorMessage))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, errorMessage));
                return;
            }

            var opData = _opDataRepository.GetFirstOrDefault<LoadPointOpData, Guid>(x =>
                x.TicketId == card.Ticket.Id && x.StateId != Dom.OpDataState.Processed);
            if (opData == null)
            {
                opData = new LoadPointOpData
                {
                    StateId = Dom.OpDataState.Init, 
                    NodeId = _nodeId, 
                    TicketId = card.Ticket.Id, 
                    CheckInDateTime = DateTime.Now
                };
            }
            else
            {
                opData.CheckOutDateTime = DateTime.Now;
                opData.StateId = Dom.OpDataState.Processed;
                _routesInfrastructure.MoveForward(card.Ticket.Id, _nodeId);
            }
           
            _opDataRepository.AddOrUpdate<LoadPointOpData, Guid>(opData);
            OpenBarrier(nodeDto);
        }
        
        private void OpenBarrier(Node nodeDto)
        {
            // Validate node context
            if (!nodeDto.Config.DI.ContainsKey(Dom.Node.Config.DI.Barrier)
                || !nodeDto.Config.DO.ContainsKey(Dom.Node.Config.DO.Barrier))
                return;

            var iBarrierConfig = nodeDto.Config.DI[Dom.Node.Config.DI.Barrier];
            var oBarrierConfig = nodeDto.Config.DO[Dom.Node.Config.DO.Barrier];

            var oBarrierState = (DigitalOutState) _deviceRepository.GetDeviceState(oBarrierConfig.DeviceId);
            if (oBarrierState.OutData == null) oBarrierState.OutData = new DigitalOutJsonState();

            Logger.Info($"UnloadPoint try to open barrier NodeId = {nodeDto.Id}, Device = {iBarrierConfig.DeviceId}");
            var startTime = DateTime.Now;
            
            oBarrierState.OutData.Value = true;
            _deviceRepository.SetDeviceOutData(oBarrierState.Id, oBarrierState.OutData);
            
            Thread.Sleep(3000);
            while (true)
            {
                var iBarrierState = (DigitalInState) _deviceRepository.GetDeviceState(iBarrierConfig.DeviceId);

                if (iBarrierState?.InData?.Value == true)
                {
                    Thread.Sleep(2000);
                    Logger.Info($"UnloadPoint is opened barrier NodeId = {nodeDto.Id}, Device = {iBarrierConfig.DeviceId}");
                    break;
                }

                if (DateTime.Now > startTime.AddSeconds(20))
                {
                    oBarrierState.OutData.Value = false;
                    _deviceRepository.SetDeviceOutData(oBarrierState.Id, oBarrierState.OutData);
                    Logger.Info($"UnloadPoint barier timeout = {nodeDto.Id}, Device = {iBarrierConfig.DeviceId}");
                    _opRoutineManager.UpdateProcessingMessage(
                        _context.Nodes.Where(x => x.OrganisationUnitId == nodeDto.OrganisationUnitId)
                            .Select(x => x.Id)
                            .ToList(), 
                        new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, $"Помилка відкриття шлагбауму на {nodeDto.Name}"));

                    return;
                }
            }

            oBarrierState.OutData.Value = false;
            _deviceRepository.SetDeviceOutData(oBarrierState.Id, oBarrierState.OutData);
            
            _nodeRepository.ClearNodeProcessingMessage(_nodeId);
        }
    }
}
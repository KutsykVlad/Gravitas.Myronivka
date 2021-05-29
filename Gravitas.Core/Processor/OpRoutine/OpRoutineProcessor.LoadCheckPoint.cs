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
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

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
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.LoadCheckPoint.State.Idle:
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
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, errorMessage));
                return;
            }

            var opData = _opDataRepository.GetFirstOrDefault<LoadPointOpData, Guid>(x =>
                x.TicketId == card.Ticket.Id && x.StateId != OpDataState.Processed);
            if (opData == null)
            {
                opData = new LoadPointOpData
                {
                    StateId = OpDataState.Init, 
                    NodeId = _nodeId, 
                    TicketId = card.Ticket.Id, 
                    CheckInDateTime = DateTime.Now
                };
            }
            else
            {
                opData.CheckOutDateTime = DateTime.Now;
                opData.StateId = OpDataState.Processed;
                _routesInfrastructure.MoveForward(card.Ticket.Id, _nodeId);
            }
           
            _opDataRepository.AddOrUpdate<LoadPointOpData, Guid>(opData);
            OpenBarrier(nodeDto);
        }
        
        private void OpenBarrier(Node nodeDto)
        {
            // Validate node context
            if (!nodeDto.Config.DI.ContainsKey(NodeData.Config.DI.Barrier)
                || !nodeDto.Config.DO.ContainsKey(NodeData.Config.DO.Barrier))
                return;

            var iBarrierConfig = nodeDto.Config.DI[NodeData.Config.DI.Barrier];
            var oBarrierConfig = nodeDto.Config.DO[NodeData.Config.DO.Barrier];

            Logger.Info($"UnloadPoint try to open barrier NodeId = {nodeDto.Id}, Device = {iBarrierConfig.DeviceId}");
            var startTime = DateTime.Now;
            
            Program.SetDeviceOutData(oBarrierConfig.DeviceId, true);
            
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
                    Program.SetDeviceOutData(oBarrierConfig.DeviceId, false);
                    Logger.Info($"UnloadPoint barier timeout = {nodeDto.Id}, Device = {iBarrierConfig.DeviceId}");
                    _opRoutineManager.UpdateProcessingMessage(
                        _context.Nodes.Where(x => x.OrganizationUnitId == nodeDto.OrganisationUnitId)
                            .Select(x => x.Id)
                            .ToList(), 
                        new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, $"Помилка відкриття шлагбауму на {nodeDto.Name}"));

                    return;
                }
            }

            Program.SetDeviceOutData(oBarrierConfig.DeviceId, false);
            
            _nodeRepository.ClearNodeProcessingMessage(_nodeId);
        }
    }
}
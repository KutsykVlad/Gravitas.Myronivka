using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Gravitas.DAL;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.DAO;
using NLog;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.Platform.Web.Manager.Test
{
    public class TestManager : ITestManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        private static IDictionary<long, Task> _taskDictionary = new Dictionary<long, Task>();
        private readonly INodeRepository _nodeRepository;
        private readonly IDeviceRepository _deviceRepository;

        public TestManager(INodeRepository nodeRepository,
            IDeviceRepository deviceRepository)
        {
            _nodeRepository = nodeRepository;
            _deviceRepository = deviceRepository;

        }

        public bool WeighbridgeTestTask(long nodeId, int timeDelay, int executionTime)
        {
                var task = new Task(() => CreateTask(nodeId, timeDelay, executionTime, cancelTokenSource.Token));
                task.Start();
            if (_taskDictionary.ContainsKey(nodeId))
            {
                _taskDictionary[nodeId].Dispose();
                _taskDictionary[nodeId] = task;
            }
            else
            {
                _taskDictionary.Add(nodeId, task);
            }

            return true;

        }

        public bool TerminateWeighbridgeTestTask()
        {
            cancelTokenSource.Cancel();
            return true;
        }

        private void CreateTask(long nodeId, int timeDelay, int executionTime, CancellationToken token)
        {
            var beginTime = DateTime.Now;
            while (!token.IsCancellationRequested)
            {
                if((DateTime.Now - beginTime).TotalSeconds > executionTime)
                    return;

                var nodeDto = _nodeRepository.GetNodeDto(nodeId);
                
                switch (nodeDto.Context.OpRoutineStateId)
                {
                    case Dom.OpRoutine.Weighbridge.State.Idle:
                        if ((DateTime.Now - nodeDto.Context.LastStateChangeTime)?.TotalSeconds > timeDelay)
                        {
                            Imitate_Idle(nodeDto);
                        }
                        break;
                    case Dom.OpRoutine.Weighbridge.State.GetScaleZero:
                        Imitate_GetScaleZero(nodeDto);
                        break;
                    case Dom.OpRoutine.Weighbridge.State.OpenBarrierIn:
                        Imitate_OpenBarrierIn(nodeDto);
                        break;
                    case Dom.OpRoutine.Weighbridge.State.CheckScaleNotEmpty:
                        Imitate_CheckScaleNotEmpty(nodeDto);
                        break;
                    case Dom.OpRoutine.Weighbridge.State.GetTicketCard:
                        Imitate_GetTicketCard(nodeDto);
                        break;
                }
            }
        }

        private void Imitate_Idle(Node nodeDto)
        {

            _deviceRepository.Update<DeviceState, long>(new DeviceState()
            {
                Id = nodeDto.Config.DI[Dom.Node.Config.DI.LoopIncoming].DeviceId,
                InData = "{\"Value\":true}",
                LastUpdate = DateTime.Now
            });

            _deviceRepository.Update<DeviceState, long>(new DeviceState
            {
                Id = nodeDto.Config.DI[Dom.Node.Config.DI.LoopOutgoing].DeviceId,
                InData = "{\"Value\":true}",
                LastUpdate = DateTime.Now
            });
        }

        private void Imitate_GetScaleZero(Node nodeDto)
        {
            _deviceRepository.Update<DeviceState, long>(new DeviceState
            {
                Id = nodeDto.Config.Scale[Dom.Node.Config.Scale.Scale1].DeviceId,
                InData = "{\"Value\":0.0,\"IsZero\":false,\"IsImmobile\":true,\"IsGross\":true,\"IsScaleError\":false}",
                LastUpdate = DateTime.Now
            });
        }

        private void Imitate_OpenBarrierIn(Node nodeDto)
        {
            _deviceRepository.Update<DeviceState, long>(new DeviceState
            {
                Id = nodeDto.Config.DI[Dom.Node.Config.DI.LoopIncoming].DeviceId,
                InData = "{\"Value\":true}",
                LastUpdate = DateTime.Now
            });
        }

        private void Imitate_CheckScaleNotEmpty(Node nodeDto)
        {
            _deviceRepository.Update<DeviceState, long>(new DeviceState
            {
                Id = nodeDto.Config.Scale[Dom.Node.Config.Scale.Scale1].DeviceId,
                InData = "{\"Value\":1600.0,\"IsZero\":false,\"IsImmobile\":true,\"IsGross\":true,\"IsScaleError\":false}",
                LastUpdate = DateTime.Now
            });
        }

        private void Imitate_GetTicketCard(Node nodeDto)
        {

        }
    }
}
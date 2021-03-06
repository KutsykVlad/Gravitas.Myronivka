using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Gravitas.DeviceSync;
using Gravitas.DeviceSync.Common;
using Gravitas.DeviceSync.Manager;
using Gravitas.Model;
using Gravitas.Model.Dto;
using Newtonsoft.Json;
using NLog;

namespace Gravitas.Core.Manager
{
    internal class VkModuleSocket1Manager : IVkModuleSocket1Manager
    {
        private readonly long _deviceId;

        public VkModuleSocket1Manager(long deviceId)
        {
            _deviceId = deviceId;
        }

        public void SyncData(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    ReadDeviceState();
                }
                catch (Exception e)
                {
                    LogManager.GetCurrentClassLogger().Error( $"Exception: {e}, DeviceId: {_deviceId}");
                }

                Thread.Sleep(1000);
            }
        }

        private VkModuleI4O0State VkModuleI4O0XmlToJsonState(VkModule4In0OutStateXml xml)
        {
            VkModuleI4O0State json;

            if (xml != null)
                json = new VkModuleI4O0State
                {
                    LastUpdate = DateTime.Now,
                    ErrorCode = xml.ErrCode,
                    InData = new VkModuleI4O0InJsonState
                    {
                        DigitalIn = new Dictionary<int, DigitalInJsonState>
                        {
                            {1, new DigitalInJsonState {Value = xml.In0}},
                            {2, new DigitalInJsonState {Value = xml.In1}},
                            {3, new DigitalInJsonState {Value = xml.In2}},
                            {4, new DigitalInJsonState {Value = xml.In3}}
                        }
                    },
                    OutData = null
                };
            else
                json = new VkModuleI4O0State
                {
                    LastUpdate = DateTime.Now,
                    ErrorCode = Dom.Device.Status.ErrorCode.Timeout,
                    InData = new VkModuleI4O0InJsonState
                    {
                        DigitalIn = new Dictionary<int, DigitalInJsonState>
                        {
                            {1, new DigitalInJsonState {Value = false}},
                            {2, new DigitalInJsonState {Value = false}},
                            {3, new DigitalInJsonState {Value = false}},
                            {4, new DigitalInJsonState {Value = false}}
                        }
                    },
                    OutData = null
                };
            return json;
        }

//        private VkModule4In0OutStateXml VkModuleI4O0JsonToXmlState(VkModuleI4O0State json)
//        {
//            var xml = new VkModule4In0OutStateXml
//            {
//                In0 = false,
//                In1 = false,
//                In2 = false,
//                In3 = false
//            };
//
//            if (json == null) return xml;
//
//            if (json.InData != null)
//            {
//                if (json.InData.DigitalIn.TryGetValue(1, out var di0State)) xml.In0 = di0State.Value;
//                if (json.InData.DigitalIn.TryGetValue(2, out var di1State)) xml.In1 = di1State.Value;
//                if (json.InData.DigitalIn.TryGetValue(3, out var di2State)) xml.In2 = di2State.Value;
//                if (json.InData.DigitalIn.TryGetValue(4, out var di3State)) xml.In1 = di3State.Value;
//            }
//
//            return xml;
//        }

        // Parent -> Child
        private void PushChildDeviceInState()
        {
            if (!Program.Devices.ContainsKey(_deviceId))
            {
                return;
            }
            var parentDevice = Program.Devices[_deviceId];
            if (parentDevice?.ParamId == null
                || parentDevice?.StateId == null)
                return;

            var parentState = (VkModuleI4O0State) parentDevice.GetState();
            foreach (var childDevice in Program.Devices.Where(x => x.Value.ParentDeviceId == _deviceId).Select(x => x.Value))
            {
                if (childDevice.DeviceState == null)
                {
                    if (!Program.DeviceStates.ContainsKey(_deviceId))
                    {
                        Program.DeviceStates.TryAdd(_deviceId, new DeviceState());
                    }
                
                    childDevice.StateId = _deviceId;
                    Program.Devices[childDevice.Id] = childDevice;
                }
                
                if (childDevice.StateId == null
                    || childDevice.ParamId == null)
                    continue;

                var childState = Program.DeviceStates[childDevice.Id];
                var childParam = Program.DeviceParams[childDevice.Id];
                var digitalInOutParam = JsonConvert.DeserializeObject<DigitalInOutParam>(childParam.ParamJson);

                childState.ErrorCode = parentState.ErrorCode;
                childState.LastUpdate = parentState.LastUpdate;

                childState.InData = null;
                childState.OutData = null;

                if (childDevice.TypeId == Dom.Device.Type.DigitalIn
                    && parentState.InData.DigitalIn.TryGetValue(digitalInOutParam.No, out var inJsonState))
                {
                    childState.InData = JsonConvert.SerializeObject(inJsonState);
                    childState.OutData = null;
                }

                if (childDevice.TypeId == Dom.Device.Type.DigitalOut
                    && parentState.OutData.DigitalOut.TryGetValue(digitalInOutParam.No, out var outJsonState))
                {
                    childState.InData = null;
                    childState.OutData = JsonConvert.SerializeObject(outJsonState);
                }

                Program.DeviceStates[childState.Id] = childState;
            }
        }

        private void ReadDeviceState()
        {
            if (!Program.Devices.ContainsKey(_deviceId))
            {
                return;
            }
            var deviceParam = Program.DeviceParams[_deviceId];
            if (deviceParam == null) return;

            var param = JsonConvert.DeserializeObject<VkModuleSocketXParam>(deviceParam.ParamJson);
            if (param == null) return;

            if (!IPAddress.TryParse(param.IpAddress, out var ipAddress)) return;

            var device = Program.Devices[_deviceId];
            if (device.DeviceState == null)
            {
                if (!Program.DeviceStates.ContainsKey(_deviceId))
                {
                    Program.DeviceStates.TryAdd(_deviceId, new DeviceState());
                }
                
                device.StateId = _deviceId;
                Program.Devices[_deviceId] = device;
            }

            var xmlState = VkModuleRelayHelper.GetVkModuleRelayState<VkModule4In0OutStateXml>(ipAddress, param.Login, param.Password);

            var dbState = VkModuleI4O0XmlToJsonState(xmlState);
            dbState.Id = device.StateId.Value;
            Program.DeviceStates[_deviceId] = new DeviceState
            {
                Id = _deviceId,
                ErrorCode = dbState.ErrorCode,
                LastUpdate = dbState.LastUpdate,
                InData = dbState.InData?.ToJson()
            };

            PushChildDeviceInState();
        }
    }
}
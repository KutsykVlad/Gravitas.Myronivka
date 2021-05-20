using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Gravitas.Core.DeviceManager;
using Gravitas.Infrastrructure.Common;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Newtonsoft.Json;
using NLog;
using DeviceType = Gravitas.Model.DomainValue.DeviceType;

namespace Gravitas.Core.Manager.VkModuleSocket2
{
    public class VkModuleSocket2Manager : IVkModuleSocket2Manager
    {
        private readonly int _deviceId;

        public VkModuleSocket2Manager(int deviceId)
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
                    LogManager.GetCurrentClassLogger().Error($"Exception in {_deviceId}: {e.Message}");
                }

                Thread.Sleep(1000);
            }
        }

        private static string GetWebResponse(HttpWebRequest request)
        {
            string responseStr = null;

            try
            {
                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    var objStream = response.GetResponseStream();

                    if (response.StatusCode == HttpStatusCode.OK)
                        using (var stream = new MemoryStream())
                        {
                            var buffer = new byte[2048]; // read in chunks of 2KB
                            int bytesRead;
                            while ((bytesRead = objStream.Read(buffer, 0, buffer.Length)) > 0)
                                stream.Write(buffer, 0, bytesRead);
                            responseStr = Encoding.ASCII.GetString(stream.ToArray());
                        }
                }
            }
            catch
            {
                /*ignore*/
            }

            return responseStr;
        }

        private static VkModule2In2OutStateXml GetVkModuleSocket2IoState(IPAddress host, string user, string pass)
        {
            VkModule2In2OutStateXml state;

            var request = (HttpWebRequest) WebRequest.Create($"http://{host}/protect/status.xml");
            request.Credentials = new NetworkCredential(user, pass);
            request.Timeout = 5000;

            try
            {
                var responseStr = GetWebResponse(request);
                state = SerializationHelper.DeserializeFromString<VkModule2In2OutStateXml>(responseStr);
                state.ErrCode = 0;
            }
            catch
            {
                state = new VkModule2In2OutStateXml();
            }

            return state;
        }

        private static VkModule2In2OutStateXml ChangeVkModuleSocket2IoState(IPAddress host, string user, string pass,
            int outNo, int timeout = 0)
        {
            var request =
                (HttpWebRequest) WebRequest.Create($"http://{host}/protect/leds.cgi?led={outNo}&timeout={timeout}");
            request.Credentials = new NetworkCredential(user, pass);
            request.Timeout = 5000;

            GetWebResponse(request);

            var state = GetVkModuleSocket2IoState(host, user, pass);
            return state;
        }

        private static VkModule2In2OutStateXml SetVkModuleSocket2IoState(IPAddress host, string user, string pass,
            VkModule2In2OutStateXml newState)
        {
            var state = GetVkModuleSocket2IoState(host, user, pass);

            if (newState == null) return state;

            if (state != null && state.Out0 != newState.Out0) state = ChangeVkModuleSocket2IoState(host, user, pass, 0);
            if (state != null && state.Out1 != newState.Out1) state = ChangeVkModuleSocket2IoState(host, user, pass, 1);

            return state;
        }

        private VkModuleI2O2State VkModuleI2O2XmlToJsonState(VkModule2In2OutStateXml xml)
        {
            VkModuleI2O2State json;

            if (xml != null)
                json = new VkModuleI2O2State
                {
                    LastUpdate = DateTime.Now,
                    ErrorCode = xml.ErrCode,
                    InData = new VkModuleI2O2InJsonState
                    {
                        DigitalIn = new Dictionary<int, DigitalInJsonState>
                        {
                            {1, new DigitalInJsonState {Value = xml.In0}},
                            {2, new DigitalInJsonState {Value = xml.In1}}
                        }
                    },
                    OutData = new VkModuleI2O2OutJsonState
                    {
                        DigitalOut = new Dictionary<int, DigitalOutJsonState>
                        {
                            {1, new DigitalOutJsonState {Value = xml.Out0}},
                            {2, new DigitalOutJsonState {Value = xml.Out1}}
                        }
                    }
                };
            else
                json = new VkModuleI2O2State
                {
                    LastUpdate = DateTime.Now,
                    ErrorCode = 255,
                    InData = new VkModuleI2O2InJsonState
                    {
                        DigitalIn = new Dictionary<int, DigitalInJsonState>
                        {
                            {1, new DigitalInJsonState {Value = false}},
                            {2, new DigitalInJsonState {Value = false}}
                        }
                    },
                    OutData = new VkModuleI2O2OutJsonState
                    {
                        DigitalOut = new Dictionary<int, DigitalOutJsonState>
                        {
                            {1, new DigitalOutJsonState {Value = false}},
                            {2, new DigitalOutJsonState {Value = false}}
                        }
                    }
                };

            return json;
        }

        private VkModule2In2OutStateXml VkModuleI2O2JsonToXmlState(VkModuleI2O2State json)
        {
            var xml = new VkModule2In2OutStateXml
            {
                In0 = false,
                In1 = false,
                Out0 = false,
                Out1 = false
            };

            if (json == null) return xml;

            if (json.InData?.DigitalIn != null)
            {
                if (json.InData.DigitalIn.TryGetValue(1, out var di1State)) xml.In0 = di1State.Value;
                if (json.InData.DigitalIn.TryGetValue(2, out var di2State)) xml.In1 = di2State.Value;
            }

            if (json.OutData?.DigitalOut != null)
            {
                if (json.OutData.DigitalOut.TryGetValue(1, out var do1State)) xml.Out0 = do1State.Value;
                if (json.OutData.DigitalOut.TryGetValue(2, out var do2State)) xml.Out1 = do2State.Value;
            }

            return xml;
        }

        // Child -> Parent
        private void PullChildDeviceOutState()
        {
            if (!Program.Devices.ContainsKey(_deviceId)) return;
            var parentDevice = Program.Devices[_deviceId];
            if (parentDevice?.ParamId == null || parentDevice.StateId == null) return;

            var parentState = (VkModuleI2O2State) parentDevice.GetState();
            if (parentState == null) return;

            parentState.OutData = new VkModuleI2O2OutJsonState();
            var s = Program.Devices.Where(x => x.Value.ParentDeviceId == _deviceId).Select(x => x.Value).ToList();
            foreach (var childDevice in s)
            {
                if (childDevice.StateId == null
                    || childDevice.ParamId == null)
                    continue;

                var childState = Program.DeviceStates[childDevice.Id];
                var childParam = Program.DeviceParams[childDevice.ParamId.Value];
                if (childState.OutData == null
                    || childParam.ParamJson == null)
                    continue;

                var digitalOutJsonState = JsonConvert.DeserializeObject<DigitalOutJsonState>(childState.OutData);
                var digitalInOutParam = JsonConvert.DeserializeObject<DigitalInOutParam>(childParam.ParamJson);

                if (childDevice.TypeId == DeviceType.DigitalOut)
                {
                    if (!parentState.OutData.DigitalOut.ContainsKey(digitalInOutParam.No))
                        parentState.OutData.DigitalOut.Add(digitalInOutParam.No, new DigitalOutJsonState());
                    parentState.OutData.DigitalOut[digitalInOutParam.No].Value = digitalOutJsonState.Value;
                }
                
                Program.DeviceStates[_deviceId].OutData = JsonConvert.SerializeObject(parentState.OutData);
            }
        }

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

            var parentState = (VkModuleI2O2State) parentDevice.GetState();
            var s = Program.Devices.Where(x => x.Value.ParentDeviceId == _deviceId).Select(x => x.Value);
            foreach (var childDevice in s)
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
                childState.LastUpdate = DateTime.Now;

                childState.InData = null;

                if (childDevice.TypeId == DeviceType.DigitalIn
                    && parentState.InData.DigitalIn.TryGetValue(digitalInOutParam.No, out var inJsonState))
                    childState.InData = JsonConvert.SerializeObject(inJsonState);

                Program.DeviceStates[childState.Id] = childState;
            }
        }

        private void ReadDeviceState()
        {
            PullChildDeviceOutState();

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

            if (device.StateId == null) return;

            VkModuleI2O2State tmpDbState = null;
            var dbState = (VkModuleI2O2State) device.GetState();

            VkModule2In2OutStateXml xmlState;

            if (dbState == null)
            {
                xmlState = GetVkModuleSocket2IoState(ipAddress, param.Login, param.Password);

                tmpDbState = VkModuleI2O2XmlToJsonState(xmlState);

                dbState = new VkModuleI2O2State
                {
                    Id = _deviceId,
                    ErrorCode = tmpDbState.ErrorCode,
                    LastUpdate = DateTime.Now,
                    InData = tmpDbState.InData
                };
                Program.DeviceStates[_deviceId] = new DeviceState
                {
                    Id = _deviceId,
                    ErrorCode = dbState.ErrorCode,
                    LastUpdate = DateTime.Now,
                    InData = JsonConvert.SerializeObject(dbState.InData),
                    OutData = JsonConvert.SerializeObject(dbState.OutData)
                };
                return;
            }

            xmlState = VkModuleI2O2JsonToXmlState(dbState);

            xmlState = SetVkModuleSocket2IoState(ipAddress, param.Login, param.Password, xmlState);
            tmpDbState = VkModuleI2O2XmlToJsonState(xmlState);

            dbState.Id = _deviceId;
            dbState.ErrorCode = tmpDbState.ErrorCode;
            dbState.LastUpdate = DateTime.Now;
            dbState.InData = tmpDbState.InData;

            Program.DeviceStates[_deviceId].ErrorCode = dbState.ErrorCode;
            Program.DeviceStates[_deviceId].LastUpdate = dbState.LastUpdate;
            Program.DeviceStates[_deviceId].InData = JsonConvert.SerializeObject(dbState.InData);
            Program.DeviceStates[_deviceId].OutData = JsonConvert.SerializeObject(dbState.OutData);

            PushChildDeviceInState();
        }
    }
}
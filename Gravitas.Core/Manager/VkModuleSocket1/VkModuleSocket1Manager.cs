using System;
using System.Collections.Generic;
using System.Net;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Newtonsoft.Json;

namespace Gravitas.Core.Manager.VkModuleSocket1
{
    public class VkModuleSocket1Manager : IVkModuleSocket1Manager
    {
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
                    ErrorCode = 255,
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

        

        public bool GetValue(int deviceId)
        {
            if (!Program.Devices.ContainsKey(deviceId) || !Program.Devices[deviceId].ParentDeviceId.HasValue)
            {
                return false;
            }

            var parent = Program.Devices[Program.Devices[deviceId].ParentDeviceId.Value];
            var deviceParam = Program.DeviceParams[parent.Id];
            if (deviceParam == null) return false;

            var param = JsonConvert.DeserializeObject<VkModuleSocketXParam>(deviceParam.ParamJson);
            if (param == null) return false;

            if (!IPAddress.TryParse(param.IpAddress, out var ipAddress)) return false;

            var xmlState = VkModuleRelay.VkModuleRelayHelper.GetVkModuleRelayState<VkModule4In0OutStateXml>(ipAddress, param.Login, param.Password);

            var dbState = VkModuleI4O0XmlToJsonState(xmlState);
            return dbState.InData.DigitalIn[deviceId].Value;
        }
    }
}
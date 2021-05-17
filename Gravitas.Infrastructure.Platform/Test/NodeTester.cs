using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using Gravitas.DAL;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.Dto;
using Newtonsoft.Json;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Infrastructure.Platform.Test
{
    public class NodeTester : INodeTester
    {
        private readonly IDeviceRepository _deviceRepository;
        private static readonly HttpClient Client = new HttpClient();

        public NodeTester(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public void PutRfidCard(long deviceId, string cardId)
        {
            var device = _deviceRepository.GetEntity<Device, long>(deviceId);
            if (device == null || device.TypeId != Dom.Device.Type.RfidObidRw) return;

            if (device.DeviceState == null)
            {
                var devState = new DeviceState();
                _deviceRepository.AddOrUpdate<DeviceState, long>(devState);

                device.StateId = devState.Id;
                _deviceRepository.AddOrUpdate<Device, long>(device);
            }

            if (device.DeviceState == null) return;

            device.DeviceState.ErrorCode = 0;
            device.DeviceState.LastUpdate = DateTime.Now;
            device.DeviceState.InData = new RfidObidRwInJsonState
            {
                Rifd = cardId
            }.ToJson();
            _deviceRepository.AddOrUpdate<DeviceState, long>(device.DeviceState);
        }

        public void PutZebraCard(long deviceId, string cardId)
        {
            var device = _deviceRepository.GetEntity<Device, long>(deviceId);
            if (device?.DeviceParam == null) return;

            var deviceState = _deviceRepository.GetSingleOrDefault<DeviceState, long>(t => t.Id == device.StateId);
            if (deviceState == null)
            {
                deviceState = new DeviceState();
                _deviceRepository.Add<DeviceState, long>(deviceState);

                device.StateId = deviceState.Id;
                _deviceRepository.Update<Device, long>(device);
            }

            RfidZebraFx9500HeadState headState;
            try
            {
                headState = JsonConvert.DeserializeObject<RfidZebraFx9500HeadState>(deviceState.InData);
            }
            catch
            {
                headState = new RfidZebraFx9500HeadState
                {
                    InData = new RfidZebraFx9500HeadInJsonState
                    {
                        AntenaState = new Dictionary<int, RfidZebraFx9500AntennaInJsonState>()
                    }
                };
            }

            if (!headState.InData.AntenaState.TryGetValue(1, out var antennaState))
            {
                antennaState = new RfidZebraFx9500AntennaInJsonState
                {
                    TagList = new Dictionary<string, DateTime>()
                };
                headState.InData.AntenaState.Add(1, antennaState);
            }

            if (!antennaState.TagList.ContainsKey(cardId))
                antennaState.TagList.Add(cardId, DateTime.Now);
            else
                antennaState.TagList[cardId] = DateTime.Now;

            deviceState.ErrorCode = 0;
            deviceState.LastUpdate = DateTime.Now;
            deviceState.InData = JsonConvert.SerializeObject(headState);
            _deviceRepository.AddOrUpdate<DeviceState, long>(deviceState);
        }

        public void SendGetRequest(string url)
        {
            url = $"http://{HttpContext.Current.Request.Url.Authority}{url}";
            Client.GetStringAsync(url);
        }
        
        public void SendPostRequest(string url, Dictionary<string, string> obj)
        {
            var content = new FormUrlEncodedContent(obj);
            var response = Client.PostAsync($"http://{HttpContext.Current.Request.Url.Authority}{url}", content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
        }
    }
}
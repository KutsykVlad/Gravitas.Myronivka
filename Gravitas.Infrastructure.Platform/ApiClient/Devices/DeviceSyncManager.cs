using System;
using System.Net.Http;
using Gravitas.DAL.DeviceTransfer;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Base;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.Dto;
using Newtonsoft.Json;
using NLog;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Infrastructure.Platform.ApiClient.Devices
{
    public static class DeviceSyncManager
    {
        private static HttpClient Client = new HttpClient();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static BaseDeviceState GetDeviceState(long deviceId)
        {
            try
            {
                using (var requestMessage = new HttpRequestMessage())
                {
                    requestMessage.RequestUri = new Uri($"http://localhost:8090/DeviceSync/GetState?deviceId={deviceId}");
                    requestMessage.Method = HttpMethod.Get;

                    using (var response = Send(requestMessage))
                    {
                        if (response?.Content != null)
                        {
                            var responseJson = response.Content?.ReadAsStringAsync().GetAwaiter().GetResult();
                            var state = JsonConvert.DeserializeObject<DeviceStateTransfer>(responseJson);
                            return state != null ? GetDeviceState(state) : null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error($"DeviceSyncManager: GetDeviceState: Error while retrieving data: {e}, deviceId = {deviceId}");
            }

            return null;
        }

        private static HttpResponseMessage Send(HttpRequestMessage requestMessage)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                responseMessage = Client.SendAsync(requestMessage).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                _logger.Error($"DeviceSyncManager: Send: Error while sending Api request: {ex.Message}");
            }

            return responseMessage;
        }
        
        private static BaseDeviceState GetDeviceState(DeviceStateTransfer dev)
        {
            switch (dev.DeviceType)
            {
                case Dom.Device.Type.RelayVkmodule4In0Out:
                    return new VkModuleI4O0State
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = VkModuleI4O0InJsonState.FromJson(dev.InData),
                        OutData = VkModuleI4O0OutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.RelayVkmodule2In2Out:
                    return new VkModuleI2O2State
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = VkModuleI2O2InJsonState.FromJson(dev.InData),
                        OutData = VkModuleI2O2OutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.DigitalIn:
                    return new DigitalInState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = DigitalInJsonState.FromJson(dev.InData),
                        OutData = null
                    };
                case Dom.Device.Type.DigitalOut:
                    return new DigitalOutState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = null,
                        OutData = DigitalOutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.RfidObidRw:
                    return new RfidObidRwState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = RfidObidRwInJsonState.FromJson(dev.InData),
                        OutData = RfidObidRwOutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.RfidZebraFx9500Antenna:
                    return new RfidZebraFx9500AntennaState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = RfidZebraFx9500AntennaInJsonState.FromJson(dev.InData),
                        OutData = RfidZebraFx9500AntennaOutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.ScaleMettlerPT6S3:
                    return new ScaleState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = ScaleInJsonState.FromJson(dev.InData),
                        OutData = ScaleOutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.LabFoss:
                    return new LabFossState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = LabFossInJsonState.FromJson(dev.InData),
                        OutData = LabFossOutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.LabBruker:
                    return new LabBrukerState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = LabBrukerInJsonState.FromJson(dev.InData),
                        OutData = LabBrukerOutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.LabInfrascan:
                    return new LabInfrascanState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = LabInfrascanInJsonState.FromJson(dev.InData),
                        OutData = LabInfrascanOutJsonState.FromJson(dev.OutData)
                    };
                default: return null;
            }
        }
    }
}
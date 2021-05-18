using System;
using System.Net.Http;
using Gravitas.DAL.DeviceTransfer;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Base;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.DomainValue;
using Newtonsoft.Json;
using NLog;

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
                case DeviceType.RelayVkmodule4In0Out:
                    return new VkModuleI4O0State
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<VkModuleI4O0InJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<VkModuleI4O0OutJsonState>(dev.OutData)
                    };
                case DeviceType.RelayVkmodule2In2Out:
                    return new VkModuleI2O2State
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<VkModuleI2O2InJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<VkModuleI2O2OutJsonState>(dev.OutData)
                    };
                case DeviceType.DigitalIn:
                    return new DigitalInState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<DigitalInJsonState>(dev.InData),
                        OutData = null
                    };
                case DeviceType.DigitalOut:
                    return new DigitalOutState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = null,
                        OutData = JsonConvert.DeserializeObject<DigitalOutJsonState>(dev.OutData)
                    };
                case DeviceType.RfidObidRw:
                    return new RfidObidRwState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<RfidObidRwInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<RfidObidRwOutJsonState>(dev.OutData)
                    };
                case DeviceType.RfidZebraFx9500Antenna:
                    return new RfidZebraFx9500AntennaState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<RfidZebraFx9500AntennaInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<RfidZebraFx9500AntennaOutJsonState>(dev.OutData)
                    };
                case DeviceType.ScaleMettlerPT6S3:
                    return new ScaleState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<ScaleInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<ScaleOutJsonState>(dev.OutData)
                    };
                case DeviceType.LabFoss:
                    return new LabFossState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<LabFossInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<LabFossOutJsonState>(dev.OutData)
                    };
                case DeviceType.LabFoss2:
                    return new LabFossState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<LabFossInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<LabFossOutJsonState>(dev.OutData)
                    };
                case DeviceType.LabBruker:
                    return new LabBrukerState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<LabBrukerInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<LabBrukerOutJsonState>(dev.OutData)
                    };
                case DeviceType.LabInfrascan:
                    return new LabInfrascanState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<LabInfrascanInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<LabInfrascanOutJsonState>(dev.OutData)
                    };
                default: return null;
            }
        }
    }
}
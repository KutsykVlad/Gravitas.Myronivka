using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Base;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.DomainValue;
using Newtonsoft.Json;

namespace Gravitas.Core.DeviceManager
{
    public static class DeviceStateHelper
    {
        public static BaseDeviceState GetState(this Model.DomainModel.Device.DAO.Device device)
        {
            if (device?.StateId == null)
                return null;

            var daoDeviceState = Program.DeviceStates[device.Id];
            if (daoDeviceState == null)
                return null;

            switch (device.TypeId)
            {
                case DeviceType.RelayVkmodule4In0Out:
                    return new VkModuleI4O0State
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = JsonConvert.DeserializeObject<VkModuleI4O0InJsonState>(daoDeviceState.InData),
                        OutData = JsonConvert.DeserializeObject<VkModuleI4O0OutJsonState>(daoDeviceState.OutData)
                    };
                case DeviceType.RelayVkmodule2In2Out:
                    return new VkModuleI2O2State
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = JsonConvert.DeserializeObject<VkModuleI2O2InJsonState>(daoDeviceState.InData),
                        OutData = JsonConvert.DeserializeObject<VkModuleI2O2OutJsonState>(daoDeviceState.OutData)
                    };
                case DeviceType.DigitalIn:
                    return new DigitalInState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = JsonConvert.DeserializeObject<DigitalInJsonState>(daoDeviceState.InData),
                        OutData = null
                    };
                case DeviceType.DigitalOut:
                    return new DigitalOutState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = null,
                        OutData = JsonConvert.DeserializeObject<DigitalOutJsonState>(daoDeviceState.OutData)
                    };
                case DeviceType.RfidObidRw:
                    return new RfidObidRwState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = JsonConvert.DeserializeObject<RfidObidRwInJsonState>(daoDeviceState.InData),
                        OutData = JsonConvert.DeserializeObject<RfidObidRwOutJsonState>(daoDeviceState.OutData)
                    };
                case DeviceType.RfidZebraFx9500Antenna:
                    return new RfidZebraFx9500AntennaState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = JsonConvert.DeserializeObject<RfidZebraFx9500AntennaInJsonState>(daoDeviceState.InData),
                        OutData = JsonConvert.DeserializeObject<RfidZebraFx9500AntennaOutJsonState>(daoDeviceState.OutData)
                    };
                case DeviceType.ScaleMettlerPT6S3:
                    return new ScaleState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = JsonConvert.DeserializeObject<ScaleInJsonState>(daoDeviceState.InData),
                        OutData = JsonConvert.DeserializeObject<ScaleOutJsonState>(daoDeviceState.OutData)
                    };
                case DeviceType.LabFoss:
                    return new LabFossState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = JsonConvert.DeserializeObject<LabFossInJsonState>(daoDeviceState.InData),
                        OutData = JsonConvert.DeserializeObject<LabFossOutJsonState>(daoDeviceState.OutData)
                    };
                case DeviceType.LabFoss2:
                    return new LabFossState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = JsonConvert.DeserializeObject<LabFossInJsonState>(daoDeviceState.InData),
                        OutData = JsonConvert.DeserializeObject<LabFossOutJsonState>(daoDeviceState.OutData)
                    };
                case DeviceType.LabBruker:
                    return new LabBrukerState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = JsonConvert.DeserializeObject<LabBrukerInJsonState>(daoDeviceState.InData),
                        OutData = JsonConvert.DeserializeObject<LabBrukerOutJsonState>(daoDeviceState.OutData)
                    };
                case DeviceType.LabInfrascan:
                    return new LabInfrascanState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = JsonConvert.DeserializeObject<LabInfrascanInJsonState>(daoDeviceState.InData),
                        OutData = JsonConvert.DeserializeObject<LabInfrascanOutJsonState>(daoDeviceState.OutData)
                    };
                default: return null;
            }
        }
    }
}
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Base;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.Dto;
using Dom = Gravitas.Model.DomainValue.Dom;

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
                case Dom.Device.Type.RelayVkmodule4In0Out:
                    return new VkModuleI4O0State
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = VkModuleI4O0InJsonState.FromJson(daoDeviceState.InData),
                        OutData = VkModuleI4O0OutJsonState.FromJson(daoDeviceState.OutData)
                    };
                case Dom.Device.Type.RelayVkmodule2In2Out:
                    return new VkModuleI2O2State
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = VkModuleI2O2InJsonState.FromJson(daoDeviceState.InData),
                        OutData = VkModuleI2O2OutJsonState.FromJson(daoDeviceState.OutData)
                    };
                case Dom.Device.Type.DigitalIn:
                    return new DigitalInState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = DigitalInJsonState.FromJson(daoDeviceState.InData),
                        OutData = null
                    };
                case Dom.Device.Type.DigitalOut:
                    return new DigitalOutState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = null,
                        OutData = DigitalOutJsonState.FromJson(daoDeviceState.OutData)
                    };
                case Dom.Device.Type.RfidObidRw:
                    return new RfidObidRwState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = RfidObidRwInJsonState.FromJson(daoDeviceState.InData),
                        OutData = RfidObidRwOutJsonState.FromJson(daoDeviceState.OutData)
                    };
                case Dom.Device.Type.RfidZebraFx9500Antenna:
                    return new RfidZebraFx9500AntennaState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = RfidZebraFx9500AntennaInJsonState.FromJson(daoDeviceState.InData),
                        OutData = RfidZebraFx9500AntennaOutJsonState.FromJson(daoDeviceState.OutData)
                    };
                case Dom.Device.Type.ScaleMettlerPT6S3:
                    return new ScaleState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = ScaleInJsonState.FromJson(daoDeviceState.InData),
                        OutData = ScaleOutJsonState.FromJson(daoDeviceState.OutData)
                    };
                case Dom.Device.Type.LabFoss:
                    return new LabFossState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = LabFossInJsonState.FromJson(daoDeviceState.InData),
                        OutData = LabFossOutJsonState.FromJson(daoDeviceState.OutData)
                    };
                case Dom.Device.Type.LabBruker:
                    return new LabBrukerState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = LabBrukerInJsonState.FromJson(daoDeviceState.InData),
                        OutData = LabBrukerOutJsonState.FromJson(daoDeviceState.OutData)
                    };
                case Dom.Device.Type.LabInfrascan:
                    return new LabInfrascanState
                    {
                        Id = daoDeviceState.Id,
                        ErrorCode = daoDeviceState.ErrorCode,
                        LastUpdate = daoDeviceState.LastUpdate,
                        InData = LabInfrascanInJsonState.FromJson(daoDeviceState.InData),
                        OutData = LabInfrascanOutJsonState.FromJson(daoDeviceState.OutData)
                    };
                default: return null;
            }
        }
    }
}
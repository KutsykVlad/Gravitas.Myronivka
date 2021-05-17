using System;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Base;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.Dto;

namespace Gravitas.DAL
{
    public class DeviceRepository : BaseRepository<GravitasDbContext>, IDeviceRepository
    {
        private readonly GravitasDbContext _context;

        public DeviceRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public BaseDeviceState GetDeviceState(long devId)
        {
            var daoDevice = GetEntity<Device, long>(devId);
            if (daoDevice?.StateId == null)
                return null;

            var daoDeviceState = GetEntity<DeviceState, long>(daoDevice.StateId.Value);
            if (daoDeviceState == null)
                return null;

            switch (daoDevice.TypeId)
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
                case Dom.Device.Type.LabFoss2:
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

        public bool IsDeviceStateValid(out NodeProcessingMsgItem errMsgItem, BaseDeviceState baseDeviceState, TimeSpan? timeout = null)
        {
            var now = DateTime.Now;

            if (baseDeviceState == null)
            {
                errMsgItem = new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Error,
                    "Нулл-стан пристрою.");
                return false;
            }

            if (baseDeviceState.LastUpdate == null
                || timeout != null && now - baseDeviceState.LastUpdate.Value > timeout)
            {
                errMsgItem = new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Info,"");
                return false;
            }

            if (baseDeviceState.ErrorCode != 0)
            {
                errMsgItem = new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Warning,
                    "Утримуйте картку на считувачі");
                return false;
            }

            switch (baseDeviceState)
            {
                case DigitalInState deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Dom.Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case DigitalOutState deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Dom.Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case RfidObidRwState deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Dom.Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case RfidZebraFx9500AntennaState deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Dom.Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case RfidZebraFx9500HeadState deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Dom.Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case ScaleState deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Dom.Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case VkModuleI2O2State deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Dom.Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case VkModuleI4O0State deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Dom.Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
            }

            errMsgItem = null;
            return true;
        }

        public bool IsDeviceStateValid(out NodeProcessingMsgItem errMsgItem, long devId)
        {
            var baseDeviceState = GetDeviceState(devId);
            return IsDeviceStateValid(out errMsgItem, baseDeviceState);
        }

        public void SetDeviceInData<TInJson, TOutJson>(DeviceState<TInJson, TOutJson> dto)
            where TInJson : BaseJsonConverter<TInJson>
            where TOutJson : BaseJsonConverter<TOutJson>
        {
            var dao = GetEntity<DeviceState, long>(dto.Id);
            if (dao == null) return;

            dao.ErrorCode = dto.ErrorCode;
            dao.LastUpdate = dto.LastUpdate;
            dao.InData = dto.InData?.ToJson();

            Update<DeviceState, long>(dao);
        }

        public void SetDeviceOutData<TInJson, TOutJson>(DeviceState<TInJson, TOutJson> dto)
            where TInJson : BaseJsonConverter<TInJson>
            where TOutJson : BaseJsonConverter<TOutJson>
        {
            SetDeviceOutData(dto.Id, dto.OutData);
        }

        public void SetDeviceOutData<TOutJson>(long devId, TOutJson outData)
            where TOutJson : BaseJsonConverter<TOutJson>
        {
            var dao = GetEntity<DeviceState, long>(devId);
            if (dao == null) return;

            dao.OutData = outData?.ToJson();

            Update<DeviceState, long>(dao);
        }
    }
}
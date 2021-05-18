using System;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.DAL.Repository.Device;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Base;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.Dto;
using Newtonsoft.Json;
using DeviceType = Gravitas.Model.DomainValue.DeviceType;

namespace Gravitas.DAL
{
    public class DeviceRepository : BaseRepository, IDeviceRepository
    {
        private readonly GravitasDbContext _context;

        public DeviceRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public BaseDeviceState GetDeviceState(long devId)
        {
            var daoDevice = _context.Devices.FirstOrDefault(x => x.Id == devId);
            if (daoDevice?.StateId == null)
                return null;

            var daoDeviceState = _context.DeviceStates.FirstOrDefault(x => x.Id == daoDevice.StateId.Value);
            if (daoDeviceState == null)
                return null;

            switch (daoDevice.TypeId)
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

        public bool IsDeviceStateValid(out NodeProcessingMsgItem errMsgItem, BaseDeviceState baseDeviceState, TimeSpan? timeout = null)
        {
            var now = DateTime.Now;

            if (baseDeviceState == null)
            {
                errMsgItem = new NodeProcessingMsgItem(
                    Node.ProcessingMsg.Type.Error,
                    "Нулл-стан пристрою.");
                return false;
            }

            if (baseDeviceState.LastUpdate == null
                || timeout != null && now - baseDeviceState.LastUpdate.Value > timeout)
            {
                errMsgItem = new NodeProcessingMsgItem(Node.ProcessingMsg.Type.Info,"");
                return false;
            }

            if (baseDeviceState.ErrorCode != 0)
            {
                errMsgItem = new NodeProcessingMsgItem(
                    Node.ProcessingMsg.Type.Warning,
                    "Утримуйте картку на считувачі");
                return false;
            }

            switch (baseDeviceState)
            {
                case DigitalInState deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case DigitalOutState deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case RfidObidRwState deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case RfidZebraFx9500AntennaState deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case RfidZebraFx9500HeadState deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case ScaleState deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case VkModuleI2O2State deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Node.ProcessingMsg.Type.Error,
                            $"Пристрій {baseDeviceState.Id} в не валідному стані");
                        return false;
                    }

                    break;
                case VkModuleI4O0State deviceState:
                    if (deviceState.InData == null)
                    {
                        errMsgItem = new NodeProcessingMsgItem(
                            Node.ProcessingMsg.Type.Error,
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

        // public void SetDeviceInData<TInJson, TOutJson>(DeviceState<TInJson, TOutJson> dto)
        //     where TInJson : BaseJsonConverter<TInJson>
        //     where TOutJson : BaseJsonConverter<TOutJson>
        // {
        //     var dao = GetEntity<DeviceState, long>(dto.Id);
        //     if (dao == null) return;
        //
        //     dao.ErrorCode = dto.ErrorCode;
        //     dao.LastUpdate = dto.LastUpdate;
        //     dao.InData = dto.InData?.ToJson();
        //
        //     Update<DeviceState, long>(dao);
        // }
        //
        // public void SetDeviceOutData<TInJson, TOutJson>(DeviceState<TInJson, TOutJson> dto)
        //     where TInJson : BaseJsonConverter<TInJson>
        //     where TOutJson : BaseJsonConverter<TOutJson>
        // {
        //     SetDeviceOutData(dto.Id, dto.OutData);
        // }
        //
        // public void SetDeviceOutData<TOutJson>(long devId, TOutJson outData)
        //     where TOutJson : BaseJsonConverter<TOutJson>
        // {
        //     var dao = GetEntity<DeviceState, long>(devId);
        //     if (dao == null) return;
        //
        //     dao.OutData = outData?.ToJson();
        //
        //     Update<DeviceState, long>(dao);
        // }
    }
}
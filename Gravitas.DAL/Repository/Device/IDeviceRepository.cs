using System;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Base;
using Gravitas.Model.DomainModel.Node.TDO.Json;

namespace Gravitas.DAL.Repository.Device
{
    public interface IDeviceRepository : IBaseRepository
    {
        BaseDeviceState GetDeviceState(long devId);
        bool IsDeviceStateValid(out NodeProcessingMsgItem errMsgItem, long devId);

        bool IsDeviceStateValid(out NodeProcessingMsgItem errMsgItem, BaseDeviceState baseDeviceState,
            TimeSpan? timeout = null);

        // void SetDeviceInData<TInJson, TOutJson>(DeviceState<TInJson, TOutJson> dto);
        //
        // void SetDeviceOutData<TInJson, TOutJson>(DeviceState<TInJson, TOutJson> dto);
        //
        // void SetDeviceOutData<TOutJson>(long devId, TOutJson outData);
    }
}
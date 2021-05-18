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

        // void SetDeviceInData<TInJson, TOutJson>(DeviceState<TInJson, TOutJson> dto)
        //     where TInJson : BaseJsonConverter<TInJson>
        //     where TOutJson : BaseJsonConverter<TOutJson>;
        //
        // void SetDeviceOutData<TInJson, TOutJson>(DeviceState<TInJson, TOutJson> dto)
        //     where TInJson : BaseJsonConverter<TInJson>
        //     where TOutJson : BaseJsonConverter<TOutJson>;
        //
        // void SetDeviceOutData<TOutJson>(long devId, TOutJson outData)
        //     where TOutJson : BaseJsonConverter<TOutJson>;
    }
}
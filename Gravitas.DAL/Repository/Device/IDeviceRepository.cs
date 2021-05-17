using System;
using Gravitas.Model;
using Gravitas.Model.Dto;

namespace Gravitas.DAL
{
    public interface IDeviceRepository : IBaseRepository<GravitasDbContext>
    {
        BaseDeviceState GetDeviceState(long devId);
        bool IsDeviceStateValid(out NodeProcessingMsgItem errMsgItem, long devId);

        bool IsDeviceStateValid(out NodeProcessingMsgItem errMsgItem, BaseDeviceState baseDeviceState,
            TimeSpan? timeout = null);

        void SetDeviceInData<TInJson, TOutJson>(DeviceState<TInJson, TOutJson> dto)
            where TInJson : BaseJsonConverter<TInJson>
            where TOutJson : BaseJsonConverter<TOutJson>;

        void SetDeviceOutData<TInJson, TOutJson>(DeviceState<TInJson, TOutJson> dto)
            where TInJson : BaseJsonConverter<TInJson>
            where TOutJson : BaseJsonConverter<TOutJson>;

        void SetDeviceOutData<TOutJson>(long devId, TOutJson outData)
            where TOutJson : BaseJsonConverter<TOutJson>;
    }
}
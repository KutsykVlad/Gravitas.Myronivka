using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Newtonsoft.Json;
using NLog;
using Symbol.RFID3;

namespace Gravitas.Core.Manager.RfidZebraFx9500
{
    public class RfidZebraFx9500Manager : IRfidZebraFx9500Manager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private RFIDReader _rfid3;

        public List<string> GetCard(int deviceId)
        {
            var deviceParam = Program.DeviceParams[deviceId];
            if (deviceParam == null) return null;

            var param = JsonConvert.DeserializeObject<RfidZebraFx9500HeadParam>(deviceParam.ParamJson);
            if (param == null) return null;

            if (!IPAddress.TryParse(param.IpAddress, out _)) return null;

            try
            {
                _rfid3 = new RFIDReader(param.IpAddress, (uint) param.IpPort, (uint) param.Timeout);
                _rfid3.Connect();

                var tags = _rfid3.Actions.GetReadTags(1000);

                var hs = new Dictionary<string, TagData>();
                foreach (var tagData in tags) 
                {
                    if (!hs.ContainsKey(tagData.TagID))
                        hs.Add(tagData.TagID, tagData);
                }

                var result = hs.Select(tagData => tagData.Value.TagID).ToList();

                _rfid3.Actions.PurgeTags();
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(
                    $@"Device: {deviceId}. RFIDZebra error while connecting on {param.IpAddress}. Exception: {e}");
                Thread.Sleep(5000);
            }

            return null;
        }
    }
}
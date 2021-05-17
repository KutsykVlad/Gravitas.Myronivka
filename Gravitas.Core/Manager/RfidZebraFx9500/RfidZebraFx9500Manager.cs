using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.Dto;
using Newtonsoft.Json;
using NLog;
using Symbol.RFID3;

namespace Gravitas.Core.Manager.RfidZebraFx9500
{
    public class RfidZebraFx9500Manager : IRfidZebraFx9500Manager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly long _deviceId;
        private RFIDReader _rfid3;

        public RfidZebraFx9500Manager(long deviceId)
        {
            _deviceId = deviceId;
        }

        public void SyncData(CancellationToken token)
        {
            if (!Program.Devices.ContainsKey(_deviceId))
            {
                return;
            }
            var deviceParam = Program.DeviceParams[_deviceId];
            if (deviceParam == null) return;

            var param = JsonConvert.DeserializeObject<RfidZebraFx9500HeadParam>(deviceParam.ParamJson);
            if (param == null) return;

            if (!IPAddress.TryParse(param.IpAddress, out _)) return;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    _rfid3 = new RFIDReader(param.IpAddress, (uint) param.IpPort, (uint) param.Timeout);
                    _rfid3.Connect();
                    if (_rfid3.IsConnected)
                        Logger.Info($@"Device: {_deviceId}. RFIDZebra connected on {param.IpAddress}!");

                    // registering for read tag data notification
                    _rfid3.Events.ReadNotify += Events_ReadNotify;
                    _rfid3.Events.StatusNotify += Events_StatusNotify;
                    _rfid3.Actions.Inventory.Perform();
                }
                catch (Exception e)
                {
                    Logger.Error(
                        $@"Device: {_deviceId}. RFIDZebra error while connecting on {param.IpAddress}. Exception: {e}");
                    Thread.Sleep(5000);
                }

                while (_rfid3 != null && _rfid3.IsConnected) Thread.Sleep(1000);

                _rfid3 = null;
                Logger.Error($@"Device: {_deviceId}. RFIDZebra disconnected!!! ");
            }
        }

        // Status Notify handler
        public void Events_StatusNotify(object sender, Events.StatusEventArgs e)
        {
            Logger.Error($@"Device: {_deviceId}. RFIDZebra status changed to {e.StatusEventData.StatusEventType}.");
        }

        // Read Notify handler
        public void Events_ReadNotify(object sender, Events.ReadEventArgs e)
        {
            if (!Program.Devices.ContainsKey(_deviceId))
            {
                return;
            }
            var device = Program.Devices[_deviceId];
            if (device?.DeviceParam == null) return;

            var deviceState = Program.DeviceStates[device.StateId.Value];
            if (deviceState == null)
            {
                if (!Program.DeviceStates.ContainsKey(_deviceId))
                {
                    Program.DeviceStates.TryAdd(_deviceId, new DeviceState());
                }
                
                device.StateId = _deviceId;
                Program.Devices[_deviceId] = device;
            }

            RfidZebraFx9500HeadState headState;
            try
            {
                headState = JsonConvert.DeserializeObject<RfidZebraFx9500HeadState>(deviceState.InData);
            }
            catch
            {
                headState = new RfidZebraFx9500HeadState
                {
                    InData = new RfidZebraFx9500HeadInJsonState
                    {
                        AntenaState = new Dictionary<int, RfidZebraFx9500AntennaInJsonState>()
                    }
                };
            }

            //
            var tags = _rfid3.Actions.GetReadTags(10000);
            if (tags == null) return;
            headState.InData.AntenaState.Clear();
            foreach (var tagData in tags)
            {
                // Allocate antenna state obj
                ;
                if (!headState.InData.AntenaState.TryGetValue(tagData.AntennaID, out var antennaState))
                {
                    antennaState = new RfidZebraFx9500AntennaInJsonState {TagList = new Dictionary<string, DateTime>()};
                    headState.InData.AntenaState.Add(tagData.AntennaID, antennaState);
                }

                // Merge antenna tag list
                if (!antennaState.TagList.ContainsKey(tagData.TagID))
                    antennaState.TagList.Add(tagData.TagID, tagData.SeenTime.UTCTime.LastSeenTimeStamp.ToLocalTime());
                else
                    antennaState.TagList[tagData.TagID] = tagData.SeenTime.UTCTime.LastSeenTimeStamp.ToLocalTime();
            }

            // Update head device DB data
            Program.DeviceStates[deviceState.Id].ErrorCode = 0;
            Program.DeviceStates[deviceState.Id].LastUpdate = DateTime.Now;
            Program.DeviceStates[deviceState.Id].InData = JsonConvert.SerializeObject(headState);

            // Update antena devices DB data
            foreach (var childDevice in device.ChildDeviceSet)
            {
                RfidZebraFx9500AntennaParam childParamJson = null;
                try
                {
                    childParamJson =
                        JsonConvert.DeserializeObject<RfidZebraFx9500AntennaParam>(childDevice.DeviceParam.ParamJson);
                }
                catch
                {
                    /*ignore*/
                }

                if (childParamJson == null)
                    continue;

                if (!headState.InData.AntenaState.TryGetValue(childParamJson.AntennaId, out var antennaState)) continue;

                DeviceState childState;
                if (childDevice.StateId != null)
                {
                    childState = Program.DeviceStates[childDevice.Id];
                }
                else
                {
                    if (!Program.DeviceStates.ContainsKey(_deviceId))
                    {
                        Program.DeviceStates.TryAdd(_deviceId, new DeviceState());
                    }
                
                    childDevice.StateId = _deviceId;
                    Program.Devices[childDevice.Id] = childDevice;
                }

                Program.DeviceStates[childDevice.Id].ErrorCode = 0;
                Program.DeviceStates[childDevice.Id].LastUpdate = DateTime.Now;
                Program.DeviceStates[childDevice.Id].InData = JsonConvert.SerializeObject(antennaState);
            }

            var hs = new Dictionary<string, TagData>();
            foreach (var tagData in tags)
                if (!hs.ContainsKey(tagData.TagID))
                    hs.Add(tagData.TagID, tagData);

            var i = 1;
            foreach (var tagData in hs)
                Logger.Debug(
                    $@"Device {deviceState.Id} No.{i++:D3} Antena: {tagData.Value.AntennaID} Id: {tagData.Value.TagID}");

            _rfid3.Actions.PurgeTags();

            _rfid3.Actions.Inventory.Stop();
            // TODO: Move timeout to param
            Thread.Sleep(1000);
            _rfid3.Actions.Inventory.Perform();
        }
    }
}
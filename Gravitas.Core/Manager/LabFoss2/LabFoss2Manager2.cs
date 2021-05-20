using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.Infrastructure.Common.Helper;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Newtonsoft.Json;
using NLog;

namespace Gravitas.Core.Manager.LabFoss2
{
    public class LabFossManager2 : ILabFossManager2
    {
        private static class DataIndex
        {
            public static int AnalysisTime = 0;
            public static int ProductName = 1;
            public static int ProductCode = 2;
            public static int SampleType = 3;
            public static int SampleNumber = 4;
            public static int SampleComment = 5;
            public static int InstrumentName = 6;
            public static int InstrumentSerial = 7;
            public static int ProteinValue = 8;
            public static int Starch = 9;
            public static int HumidityValue = 10;
            public static int OilValue = 11;
        }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly DeviceRepository _deviceRepository;
        private readonly long _deviceId;
        private FileSystemWatcher _dirWatcher;
        private readonly GravitasDbContext _context;

        public LabFossManager2(DeviceRepository deviceRepository, long deviceId, GravitasDbContext context)
        {
            _deviceRepository = deviceRepository;
            _deviceId = deviceId;
            _context = context;
        }

        public void SyncData()
        {
            var device = _context.Devices.FirstOrDefault(x => x.Id == _deviceId);
            if (device?.DeviceParam == null)
            {
                return;
            }
            
            var param = JsonConvert.DeserializeObject<LabFossParam>(device.DeviceParam.ParamJson);
            if (param == null)
            {
                return;
            }
            
            if (!Directory.Exists(param.Directory))
            {
                return;
            }

            _dirWatcher = new FileSystemWatcher
            {
                Path = param.Directory,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "*.csv"
            };
            _dirWatcher.Changed += OnChanged;
            _dirWatcher.EnableRaisingEvents = true;

            Logger.Log(LogLevel.Info, $@"Device: {_deviceId}. Directory Watcher for device has been created. Device: ");
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                var data = FileHelper.ReadAllLines(e.FullPath, 5)?.Skip(1).FirstOrDefault()?.Split(';');

                if (data == null || data.Length < 11 || data.Length > 12)
                {
                    Logger.Log(LogLevel.Warn, $@"Device: {_deviceId}. Invalid file content.");
                    return;
                }

                var humidityIndex = data.Length == 12 ? DataIndex.HumidityValue : DataIndex.HumidityValue - 1;
                var oilIndex = data.Length == 12 ? DataIndex.OilValue : DataIndex.OilValue - 1;

                var fossInJsonState = new LabFoss2InJsonState
                {
                    AnalysisTime = DateTime.ParseExact(data[DataIndex.AnalysisTime], new[]
                    {
                        "dd.MM.yyyy H:mm:ss", "dd.MM.yyyy HH:mm:ss"
                    }, CultureInfo.InvariantCulture, DateTimeStyles.None),
                    ProductName = data[DataIndex.ProductName],
                    ProductCode = data[DataIndex.ProductCode],
                    SampleType = data[DataIndex.SampleType],
                    SampleNumber = data[DataIndex.SampleNumber],
                    SampleComment = data[DataIndex.SampleComment],
                    InstrumentName = data[DataIndex.InstrumentName],
                    InstrumentSerial = data[DataIndex.InstrumentSerial],
                    ProteinValue = double.Parse(data[DataIndex.ProteinValue].Replace(',', '.'), CultureInfo.InvariantCulture),
                    HumidityValue = double.Parse(data[humidityIndex].Replace(',', '.'), CultureInfo.InvariantCulture),
                    OilValue = double.Parse(data[oilIndex].Replace(',', '.'), CultureInfo.InvariantCulture)
                };

                Logger.Log(LogLevel.Info, $@"{JsonConvert.SerializeObject(fossInJsonState)}.");

                var device = _context.Devices.FirstOrDefault(x => x.Id == _deviceId);
                if (device?.DeviceParam == null)
                {
                    Logger.Log(LogLevel.Error, $@"Device: {_deviceId}. Device param was not found.");
                    return;
                }

                var deviceState = _context.DeviceStates.AsNoTracking().FirstOrDefault(t => t.Id == device.StateId);
                if (deviceState == null)
                {
                    deviceState = new DeviceState();
                    _deviceRepository.Add<DeviceState, int>(deviceState);

                    device.StateId = deviceState.Id;
                    _deviceRepository.Update<Device, int>(device);
                }

                // Update head device DB data
                deviceState.ErrorCode = 0;
                deviceState.LastUpdate = DateTime.Now;
                deviceState.InData = JsonConvert.SerializeObject(fossInJsonState);
                _deviceRepository.AddOrUpdate<DeviceState, int>(deviceState);

                Logger.Info($@"Device: {_deviceId}. State has been updated.");
            }
            catch (Exception ex)
            {
                Logger.Error($@"Device: {_deviceId}. Exception: {ex.Message}");
            }
        }
    }
}
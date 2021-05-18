using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.Infrastructure.Common.Helper;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.Dto;
using Newtonsoft.Json;
using NLog;

namespace Gravitas.Core.Manager.LabFoss
{
    public class LabFossManager : ILabFossManager
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
            public static int HumidityValue = 9;
            public static int OilValue = 10;
            public static int FibreValue = 11;
        }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly DeviceRepository _deviceRepository;
        private readonly long _deviceId;
        private FileSystemWatcher _dirWatcher;
        private readonly GravitasDbContext _context;

        public LabFossManager(DeviceRepository deviceRepository, long deviceId, GravitasDbContext context)
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

            Logger.Log(LogLevel.Info, $@"Device: {_deviceId}. Directory Watcher for device has been created. Device: {device.Id} - {device.Name}");
        }

        //Analysis Time;		Product Name;	Product Code;	Sample Type;	Sample Number;	Sample Comment;	Instrument Name;		Instrument Serial Number;	Влажность;	Protein;	Жир;	Fibre
        //23.08.2018 11:34:55;Зерновые;		GPlant;			Normal;			;				;				NIRS DA1650 Ladyzhyn;	91721504;					8,6;		30,78;		20,0;	6,62
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                var data = FileHelper.ReadAllLines(e.FullPath, 5)?.Skip(1)?.FirstOrDefault()?.Split(';');

                if (data == null ||
                    data.Length < 12)
                {
                    Logger.Log(LogLevel.Warn, $@"Device: {_deviceId}. Invalid file content.");
                    return;
                }

                var fossInJsonState = new LabFossInJsonState
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
                    HumidityValue = double.Parse(data[DataIndex.HumidityValue].Replace(',', '.'), CultureInfo.InvariantCulture),
                    ProteinValue = double.Parse(data[DataIndex.ProteinValue].Replace(',', '.'), CultureInfo.InvariantCulture),
                    OilValue = double.Parse(data[DataIndex.OilValue].Replace(',', '.'), CultureInfo.InvariantCulture),
                    FibreValue = double.Parse(data[DataIndex.FibreValue].Replace(',', '.'), CultureInfo.InvariantCulture),
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
                    _deviceRepository.Add<DeviceState, long>(deviceState);

                    device.StateId = deviceState.Id;
                    _deviceRepository.Update<Device, long>(device);
                }

                // Update head device DB data
                deviceState.ErrorCode = 0;
                deviceState.LastUpdate = DateTime.Now;
                deviceState.InData = fossInJsonState.ToJson();
                _deviceRepository.AddOrUpdate<DeviceState, long>(deviceState);

                Logger.Info($@"Device: {_deviceId}. State has been updated.");
            }
            catch (Exception ex)
            {
                Logger.Error($@"Device: {_deviceId}. Exception: {ex.Message}");
            }
        }
    }
}
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

namespace Gravitas.Core.Manager.LabBruker
{
    public class LabBrukerManager : ILabBrukerManager
    {
        private static class DataIndex
        {
            public static int SampleName = 0;
            public static int SampleMass = 1;
            public static int Result1 = 2;
            public static int Result2 = 3;
            public static int Unit = 4;
            public static int Batch = 5;
            public static int Calibration = 6;
            public static int AcquisitionDate = 7;
            public static int Comment = 8;
            public static int Valid = 9;
            public static int Outlier = 10;
            public static int User = 11;
        }

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly DeviceRepository _deviceRepository;
        private readonly long _deviceId;
        private FileSystemWatcher _dirWatcher;
        private readonly GravitasDbContext _context;

        public LabBrukerManager(DeviceRepository deviceRepository, long deviceId, GravitasDbContext context)
        {
            _deviceRepository = deviceRepository;
            _deviceId = deviceId;
            _context = context;
        }

        public void SyncData()
        {
            Device device = _context.Devices.FirstOrDefault(x => x.Id == _deviceId);
            if (device?.DeviceParam == null)
            {
                return;
            }

            LabFossParam param =
                JsonConvert.DeserializeObject<LabFossParam>(device.DeviceParam.ParamJson);
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

            Logger.Info($@"Directory Watcher for device has been created. Device: {device.Id} - {device.Name}");
        }

        //"SampleName",	"SampleMass",	"Result1",	"Result2",	"Unit","Batch",			"Calibration",	"AcquisitionDate",		"Comment","Valid","Outlier","User"
        //"10682",		18.271,			53.37,		5.00,		%,		"urogai_7_aa",	"SFS_dry_7_aa",	8/26/2018 12:55:37 PM,	"   ",OK,OK,"rudan"
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                var data = FileHelper.ReadAllLines(e.FullPath, 5)?.Skip(1)?.FirstOrDefault()?.Split(',');

                if (data == null ||
                    data.Length != 12)
                {
                    Logger.Warn($@"Device: {_deviceId}. Invalid file content.");
                    return;
                }

                var fossInJsonState = new LabBrukerInJsonState
                {
                    SampleName = data[DataIndex.SampleName].Replace("\"", ""),
                    SampleMass = double.Parse(data[DataIndex.SampleMass].Replace(',', '.'), CultureInfo.InvariantCulture),
                    Result1 = double.Parse(data[DataIndex.Result1].Replace(',', '.'), CultureInfo.InvariantCulture),
                    Result2 = double.Parse(data[DataIndex.Result2].Replace(',', '.'), CultureInfo.InvariantCulture),
                    Unit = data[DataIndex.Unit].Replace("\"", ""),
                    Batch = data[DataIndex.Batch].Replace("\"", ""),
                    Calibration = data[DataIndex.Calibration].Replace("\"", ""),
                    AcquisitionDate = DateTime.ParseExact(data[DataIndex.AcquisitionDate], "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                    Comment = data[DataIndex.Comment].Replace("\"", ""),
                    Valid = data[DataIndex.Valid],
                    Outlier = data[DataIndex.Outlier],
                    User = data[DataIndex.User].Replace("\"", "")
                };

                Device device = _context.Devices.FirstOrDefault(x => x.Id == _deviceId);
                if (device?.DeviceParam == null)
                {
                    Logger.Error($@"Device: {_deviceId}. Device param was not found.");
                    return;
                }

                DeviceState deviceState = _context.DeviceStates.AsNoTracking().FirstOrDefault(t => t.Id == device.StateId);

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
                Logger.Error(ex, $@"Device: {_deviceId}. Exception.");
            }
        }
    }
}
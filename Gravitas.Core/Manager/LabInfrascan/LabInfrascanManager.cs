using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Gravitas.DAL;
using Gravitas.Infrastructure.Common.Helper;
using Gravitas.Model;
using Gravitas.Model.Dto;
using Newtonsoft.Json;
using NLog;

namespace Gravitas.Core.Manager.LabInfrascan
{
    public class LabInfrascanManager : ILabInfrascanManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly long _deviceId;
        private readonly DeviceRepository _deviceRepository;
        private FileSystemWatcher _dirWatcher;
        private readonly GravitasDbContext _context;
        private readonly Regex _proteinRegex = new Regex(@"Белок\w+\.\d+");
        private readonly Regex _fatRegex = new Regex(@"Жир\w+\.\d+");
        private readonly Regex _humidityRegex = new Regex(@"Влага\w+\.\d+");
        private readonly Regex _celluloseRegex = new Regex(@"Клетчатка\w+\.\d+");
        private readonly Regex _dateRegex = new Regex(@"\d+\/\d+\/\d+\s\d+\:\d+\:\d+");

        public LabInfrascanManager(DeviceRepository deviceRepository, long deviceId, GravitasDbContext context)
        {
            _deviceRepository = deviceRepository;
            _deviceId = deviceId;
            _context = context;
        }

        public void SyncData()
        {
            var device = _context.Devices.FirstOrDefault(x => x.Id == _deviceId);
            if (device?.DeviceParam == null) return;

            var param = JsonConvert.DeserializeObject<LabFossParam>(device.DeviceParam.ParamJson);
            if (param == null) return;

            if (!Directory.Exists(param.Directory)) return;

            _dirWatcher = new FileSystemWatcher
            {
                Path = param.Directory, 
                NotifyFilter = NotifyFilters.LastWrite, 
                Filter = "*.txt"
            };
            _dirWatcher.Changed += OnChanged;
            _dirWatcher.EnableRaisingEvents = true;

            Logger.Info($@"Directory Watcher for device has been created. Device: {device.Id} - {device.Name}");
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                var str = FileHelper.ReadAllText(e.FullPath, 5);

                var dates = _dateRegex.Matches(str);
                str = str.Substring(str.IndexOf(dates[0].Value, StringComparison.InvariantCultureIgnoreCase), 
                    str.IndexOf(dates[1].Value, StringComparison.InvariantCultureIgnoreCase));
                str = str.Replace(" ", "");
                var protein = _proteinRegex.Match(str).Value.Replace("Белок", "");
                var fat = _fatRegex.Match(str).Value.Replace("Жир", "");
                var humidity = _humidityRegex.Match(str).Value.Replace("Влага", "");
                var cellulose = _celluloseRegex.Match(str).Value.Replace("Клетчатка", "");
                
                var infroscanInJsonState = new LabInfrascanInJsonState {
                    AnalysisTime = DateTime.ParseExact(dates[0].Value, @"dd/MM/yy HH:mm:ss", CultureInfo.InvariantCulture),
                    HumidityValue = !string.IsNullOrWhiteSpace(humidity) 
                        ? double.Parse(humidity, CultureInfo.InvariantCulture)
                        : (double?) null,
                    ProteinValue = !string.IsNullOrWhiteSpace(protein) 
                        ? double.Parse(protein, CultureInfo.InvariantCulture)
                        : (double?) null,
                    FatValue = !string.IsNullOrWhiteSpace(fat) 
                        ? double.Parse(fat, CultureInfo.InvariantCulture) 
                        : (double?) null,
                    CelluloseValue = !string.IsNullOrWhiteSpace(cellulose) 
                        ? double.Parse(cellulose, CultureInfo.InvariantCulture)
                        :  (double?) null
                };
                
                var device = _context.Devices.FirstOrDefault(x => x.Id == _deviceId);
                if (device?.DeviceParam == null) {
                    Logger.Log(LogLevel.Error, $@"Device: {_deviceId}. Device param was not found.");
                    return;
                }
                
                var deviceState = _context.DeviceStates.AsNoTracking().FirstOrDefault(t => t.Id == device.StateId);
                if (deviceState == null) {
                    deviceState = new DeviceState();
                    _deviceRepository.Add<DeviceState, long>(deviceState);

                    device.StateId = deviceState.Id;
                    _deviceRepository.Update<Device, long>(device);
                }
                
                // Update head device DB data
                deviceState.ErrorCode = 0;
                deviceState.LastUpdate = DateTime.Now;
                deviceState.InData = infroscanInJsonState.ToJson();
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
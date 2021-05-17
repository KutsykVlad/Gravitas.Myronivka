using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gravitas.DAL;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.Manager.Settings;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using NLog;
using Settings = Gravitas.Model.DomainModel.Settings.DAO.Settings;

namespace Gravitas.Core.Processor.QueueDisplay
{
    public class QueueDisplayProcessor : IQueueDisplayProcessor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IDeviceRepository _deviceRepository;
        private readonly ISettings _settings;
        private Infrastructure.Platform.Manager.Display.QueueDisplay _display;
        private readonly GravitasDbContext _context;


        public QueueDisplayProcessor(IDeviceRepository deviceRepository, 
            ISettings settings, 
            GravitasDbContext context)
        {
            _deviceRepository = deviceRepository;
            _settings = settings;
            _context = context;
        }

        public void ProcessLoop(CancellationToken token)
        {
            while (true)
            {
                try
                {
                    var t = Task.Run(Process, token);
                    t.Wait(token);
                }
                catch (Exception e)
                {
                    e.Data.Add("QueueTable", DateTime.Now);
                    Logger.Error(e);
                }

                if (token.IsCancellationRequested)
                    return;
            }
        }

        public void Config(long deviceId)
        {
            var device = DisplayParam.FromJson(_context.Devices.First(x => x.Id == deviceId).DeviceParam.ParamJson);
            _display =
                new Infrastructure.Platform.Manager.Display.QueueDisplay(device.IpAddress, device.IpPort, true);
        }

        private async Task Process()
        {
            var time = 15000;
            var text = $"{_deviceRepository.GetEntity<Settings, int>(1)?.QueueDisplayText} - Дозволений в'їзд: ";
            var trailers = _context.QueueRegisters.AsNoTracking().Where(t => t.IsAllowedToEnterTerritory).ToList();
            foreach (var trailer in trailers)
            {
                text += $"{trailer.TruckPlate}; ";
                time += GlobalConfigurationManager.TrailerDisplayTime;
            }

            if (_display.WriteText(text, time))
                await Task.Delay(time + 1000);
            else
                await Task.Delay(30000);
        }
    }
}
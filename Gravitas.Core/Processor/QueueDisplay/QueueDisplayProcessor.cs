using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gravitas.DAL.DbContext;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Newtonsoft.Json;
using NLog;

namespace Gravitas.Core.Processor.QueueDisplay
{
    public class QueueDisplayProcessor : IQueueDisplayProcessor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private Infrastructure.Platform.Manager.Display.QueueDisplay _display;
        private readonly GravitasDbContext _context;


        public QueueDisplayProcessor(GravitasDbContext context)
        {
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

        public void Config(int deviceId)
        {
            var device = JsonConvert.DeserializeObject<DisplayParam>(_context.Devices.First(x => x.Id == deviceId).DeviceParam.ParamJson);
            _display =
                new Infrastructure.Platform.Manager.Display.QueueDisplay(device.IpAddress, device.IpPort, true);
        }

        private async Task Process()
        {
            var time = 15000;
            var text = $"{_context.Settings.First().QueueDisplayText} - Дозволений в'їзд: ";
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